using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR
{
    public class SerialPortCommunication
    {
        private SerialPort serialPort;
        public SerialPort SerialPort
        {
            get { return this.serialPort; }
            set { this.serialPort = value; }
        }

        private static SerialPortCommunication spc;

        public event SerialPortEventHandler ComOpenEvent = null;
        public event SerialPortEventHandler ComCloseEvent = null;

        private byte[] buffer;
        private byte[] finalData;
        public byte[] FinalData
        {
            get { return this.finalData; }
            set { this.finalData = value; }
        }

        public static SerialPortCommunication GetInstance()
        {
            if (spc == null)
            {
                spc = new SerialPortCommunication();
            }
            return spc;
        }

        public SerialPortCommunication()
        {

        }

        public void SendData(byte[] bytes)
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    this.FinalData = null;
                    serialPort.Write(bytes, 0, bytes.Length);
                    LogHelper.GetLogger<SerialPortCommunication>().Error(string.Format("激光器发送的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(bytes)));

                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger<SerialPortCommunication>().Error(string.Format("激光器串口发送数据异常1： {0}", ex.StackTrace));
                }
            }
        }

        public void SendData(List<byte[]> bytesList)
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    this.FinalData = null;
                    foreach (byte[] bytes in bytesList)
                    {
                        serialPort.Write(bytes, 0, bytes.Length);
                        LogHelper.GetLogger<SerialPortCommunication>().Error(string.Format("激光器发送的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(bytes)));
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger<SerialPortCommunication>().Error(string.Format("激光器串口发送数据异常2： {0}", ex.StackTrace));
                }
            }
        }

        public void SerialPortOpen(string portName, String baudRate, string dataBits, string stopBits, string parity, string handshake)
        {
            if (serialPort.IsOpen) Close();
            serialPort.PortName = portName;
            serialPort.BaudRate = Convert.ToInt32(baudRate);
            serialPort.DataBits = Convert.ToInt16(dataBits);
            if (handshake == "None")
            {
                //Never delete this property
                serialPort.RtsEnable = true;
                serialPort.DtrEnable = true;
            }

            SerialPortEventArgs args = new SerialPortEventArgs();
            try
            {
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits);
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
                serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), handshake);
                serialPort.WriteTimeout = 1000; /*Write time out*/
                serialPort.Open();
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
                args.isOpend = true;
            }
            catch (System.Exception)
            {
                args.isOpend = false;
            }
            ComOpenEvent?.Invoke(this, args);
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int dataLength = serialPort.BytesToRead;
            if (dataLength < 0) return;
            byte[] data = new byte[dataLength];
            serialPort.Read(data, 0, dataLength);
            AssembleData(data);
        }

        private void AssembleData(byte[] rawData)
        {
            if (rawData != null)
            {
                if (rawData.Length == 6)
                {
                    if (rawData[0] == 0x80)
                    {
                        //检查最后一位是否是校验位
                        byte oddCheck = GetOddCheckData(rawData);
                        if (oddCheck == rawData[rawData.Length - 1])
                        {
                            this.FinalData = rawData;
                            buffer = null;
                        }
                        else
                        {
                            LogHelper.GetLogger<SerialPortCommunication>().Error(string.Format("激光器接受的原始数据异常，数据为： {0}", ByteHelper.Byte2ReadalbeXstring(rawData)));
                        }
                    }
                }
                else
                {
                    if (buffer == null)
                    {
                        if (rawData[0] == 0x80)
                        {
                            buffer = rawData;
                        }
                    }
                    else
                    {
                        //与buffer中数据拼接
                        int length = rawData.Length;
                        int bufferLength = buffer.Length;
                        int sumDataLength = bufferLength + length;
                        if (sumDataLength < 6)
                        {
                            byte[] tempData = new byte[sumDataLength];
                            Array.Copy(buffer, 0, tempData, 0, bufferLength);
                            Array.Copy(rawData, 0, tempData, bufferLength, length);
                            buffer = tempData;
                        }
                        else if (sumDataLength >= 6)
                        {
                            byte[] tempData = new byte[6];
                            Array.Copy(buffer, 0, tempData, 0, bufferLength);
                            Array.Copy(rawData, 0, tempData, bufferLength, 6 - bufferLength);
                            byte oddCheck = GetOddCheckData(tempData);
                            if (oddCheck == tempData[tempData.Length - 1])
                            {
                                this.FinalData = tempData;
                                SetRemainData(rawData);
                            }
                            else
                            {
                                //数据异常，应该丢弃
                                SetRemainData(rawData);
                            }
                        }
                    }
                }
            }
        }

        private void SetRemainData(byte[] rawData)
        {
            int bufferLength = buffer.Length;
            int remainDataLength = rawData.Length - (6 - bufferLength);
            if (remainDataLength > 0)
            {
                buffer = new byte[remainDataLength];
                Array.Copy(rawData, 6 - bufferLength, buffer, 0, remainDataLength);
            }
        }

        private byte GetOddCheckData(byte[] data)
        {
            byte oddCheck = 0x00;
            if (data != null && data.Length == 6)
            {
                for (int i = 1; i < data.Length - 2; i++)
                {
                    oddCheck ^= data[i];
                }
            }
            return oddCheck;
        }

        public void Close()
        {
            SerialPortEventArgs args = new SerialPortEventArgs();
            args.isOpend = false;
            serialPort.Close();
            serialPort.DataReceived -= DataReceived;
            ComCloseEvent?.Invoke(this, args);
        }
    }
}
