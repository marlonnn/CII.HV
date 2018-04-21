using CII.LAR.Commond;
using CII.LAR.Protocol;
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
        public delegate void SerialDataReceived(LaserBaseResponse baseResponse);
        public SerialDataReceived SerialDataReceivedHandler;

        private Dictionary<byte, LaserBaseResponse> decoders;
        public Dictionary<byte, LaserBaseResponse> Decoders
        {
            get { return this.decoders; }
            private set { this.decoders = value; }
        }

        private LaserBaseResponse decoder;
        public LaserBaseResponse Decoder
        {
            get { return this.decoder; }
            set { this.decoder = value; }
        }

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
            set
            {
                this.finalData = value;
                if (value != null)
                {
                    LaserBaseResponse responseList = Decoder.Decode(new OriginalBytes(DateTime.Now,value));
                    if (responseList != null) SerialDataReceivedHandler?.Invoke(responseList);
                    LogHelper.GetLogger<SerialPortCommunication>().Error(string.Format("激光器接受的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(value)));
                }
            }
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
            serialPort = new SerialPort();
            InitializeDecoders();
        }

        public byte[] Encode(LaserBaseRequest br)
        {
            var bytes = LaserProtocolFactory.GetInstance().LaserProtocol.EnPackage(br.Encode()[0]);
            return bytes;
        }

        private void InitializeDecoders()
        {
            Decoders = new Dictionary<byte, LaserBaseResponse>();
            Decoders[0x00] = new LaserC00Response();
            Decoders[0x01] = new LaserC01Response();
            Decoders[0x03] = new LaserC03Response();
            Decoders[0x04] = new LaserC04Response();
            Decoders[0x05] = new LaserC05Response();
            Decoders[0x06] = new LaserC06Response();
            Decoders[0x07] = new LaserC07Response();
            Decoders[0x08] = new LaserC08Response();
            Decoders[0x09] = new LaserC09Response();
            Decoders[0x0B] = new LaserC0BResponse();
            Decoders[0x0C] = new LaserC0CResponse();
            Decoders[0x70] = new LaserC70Response();
            Decoders[0x71] = new LaserC71Response();
            Decoders[0x72] = new LaserC72Response();
            Decoders[0x73] = new LaserC73Response();
            Decoders[0x74] = new LaserC74Response();
            Decoders[0x75] = new LaserC75Response();
            Decoders[0x76] = new LaserC76Response();
            //initialize default decoder
            Decoder = Decoders[0x00];
        }

        /// <summary>
        /// 发送消息之前必须先设置解码器
        /// </summary>
        /// <param name="type"></param>
        public void SetDecoder(byte type)
        {
            if (Decoders.ContainsKey(type))
            {
                Decoder = Decoders[type];
            }
        }

        private LaserBaseResponse GetLaserBaseResponse()
        {
            LaserBaseResponse br = null;
            if (Decoders.ContainsValue(Decoder))
            {
                br = Decoders.FirstOrDefault(d => d.Value.Type == Decoder.Type).Value;
            }
            return br;
        }

        public void SendData(byte[] bytes)
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    this.FinalData = null;
                    SetDecoder(bytes[1]);
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
                    for (int i =0; i< bytesList.Count; i++)
                    {
                        if (i == 0) SetDecoder(bytesList[0][1]);
                        serialPort.Write(bytesList[i], 0, bytesList[i].Length);
                        LogHelper.GetLogger<SerialPortCommunication>().Error(string.Format("激光器发送的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(bytesList[i])));

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
                            LogHelper.GetLogger<SerialPortCommunication>().Error(string.Format("激光器接受的原始数据异常，数据为： {0}", 
                                ByteHelper.Byte2ReadalbeXstring(rawData)));
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
                for (int i = 1; i < data.Length - 1; i++)
                {
                    oddCheck ^= data[i];
                }
            }
            return oddCheck;
        }

        public void Close()
        {
            if (serialPort.IsOpen)
            {
                SerialPortEventArgs args = new SerialPortEventArgs();
                args.isOpend = false;
                serialPort.Close();
                serialPort.DataReceived -= DataReceived;
                ComCloseEvent?.Invoke(this, args);
            }
        }
    }
}
