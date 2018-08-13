using CII.LAR.Commond;
using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CII.LAR
{
    public class SerialPortManager
    {
        private static SerialPortManager spc;

        private SerialPort serialPort;
        public SerialPort SerialPort
        {
            get { return this.serialPort; }
            set { this.serialPort = value; }
        }

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

        public SerialPortManager()
        {
            InitializeDecoders();
            serialPort = new SerialPort();
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

        public static SerialPortManager GetInstance()
        {
            if (spc == null)
            {
                spc = new SerialPortManager();
            }
            return spc;
        }

        public byte[] Encode(LaserBaseRequest br)
        {
            var bytes = LaserProtocolFactory.GetInstance().LaserProtocol.EnPackage(br.Encode()[0]);
            return bytes;
        }

        public bool SerialPortOpen(string portName, String baudRate, string dataBits, string stopBits, string parity, string handshake)
        {
            bool isOpen = false;

            if (serialPort.IsOpen) Close();
            serialPort.PortName = portName;
            serialPort.BaudRate = Convert.ToInt32(baudRate);
            serialPort.DataBits = Convert.ToInt16(dataBits);
            if (handshake == "None")
            {
                serialPort.RtsEnable = true;
                serialPort.DtrEnable = true;
            }

            SerialPortEventArgs args = new SerialPortEventArgs();
            try
            {
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits);
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
                serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), handshake);
                serialPort.WriteTimeout = 1000; 
                serialPort.Open();
                args.isOpend = true;
                isOpen = true;
            }
            catch (System.Exception)
            {
                args.isOpend = false;
                isOpen = false;
            }
            return isOpen;
        }

        public byte[] SendData(List<byte[]> bytesList)
        {
            byte[] recData = null;
            if (serialPort.IsOpen)
            {
                try
                {
                    for (int i = 0; i < bytesList.Count; i++)
                    {
                        serialPort.Write(bytesList[i], 0, bytesList[i].Length);
                        LogHelper.GetLogger<SerialPortManager>().Error(string.Format("激光器发送的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(bytesList[i])));
                    }
                    Thread.Sleep(200);
                    while (true)
                    {
                        int dataLength = serialPort.BytesToRead;
                        if (dataLength == 0)
                            break;
                        byte[] data = new byte[dataLength];
                        serialPort.Read(data, 0, dataLength);

                        AssembleData(data, ref recData);
                        LogHelper.GetLogger<SerialPortManager>().Error("receive data: " + ByteHelper.Byte2ReadalbeXstring(recData));

                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger<SerialPortManager>().Error(string.Format("激光器串口发送数据异常2："));
                    LogHelper.GetLogger<SerialPortManager>().Error(ex.Message);
                    LogHelper.GetLogger<SerialPortManager>().Error(ex.StackTrace);
                    return null;
                }
            }

            return recData;
        }
        public byte[] SendData(byte[] bytes, bool catchLog = true)
        {
            byte[] recData = null;
            if (serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    if (catchLog) LogHelper.GetLogger<SerialPortManager>().Error("------>发送数据 : " + ByteHelper.Byte2ReadalbeXstring(bytes));

                    serialPort.Write(bytes, 0, bytes.Length);
                    Thread.Sleep(200);
                    while (true)
                    {
                        int dataLength = serialPort.BytesToRead;
                        if (dataLength == 0)
                            break;
                        byte[] data = new byte[dataLength];
                        serialPort.Read(data, 0, dataLength);
                        
                        AssembleData(data, ref recData);
                        if (catchLog) LogHelper.GetLogger<SerialPortManager>().Error("<------接受数据: " + ByteHelper.Byte2ReadalbeXstring(recData));

                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger<SerialPortManager>().Error(string.Format("激光器串口发送数据异常1："));
                    LogHelper.GetLogger<SerialPortManager>().Error(ex.Message);
                    LogHelper.GetLogger<SerialPortManager>().Error(ex.StackTrace);
                    return null;
                }
            }

            return recData;
        }

        public LaserBaseResponse LaserBaseResponse(byte[] sendData, byte[] recData)
        {
            LaserBaseResponse responseList = null;
            if (recData != null)
            {
                if (Decoders.ContainsKey(sendData[1]))
                {
                    Decoder = Decoders[sendData[1]];
                    if (Decoder != null)
                    {
                        responseList = Decoder.Decode(new OriginalBytes(DateTime.Now, recData));
                    }
                }
            }
            return responseList;
        }

        private byte[] buffer;

        private void AssembleData(byte[] rawData, ref byte[] finalData)
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
                            finalData = rawData;
                            buffer = null;
                            //LogHelper.GetLogger<SerialPortManager>().Error(string.Format("1.激光器接受的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(rawData)));
                        }
                        else
                        {
                            LogHelper.GetLogger<SerialPortManager>().Error(string.Format("激光器接受的原始数据异常1，数据为： {0}",
                                ByteHelper.Byte2ReadalbeXstring(rawData)));
                        }
                    }
                }
                else
                {
                    if (buffer == null)
                    {
                        if (rawData.Length > 0 && rawData[0] == 0x80)
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
                                finalData = tempData;
                                SetRemainData(rawData);
                                //LogHelper.GetLogger<SerialPortManager>().Error(string.Format("2.激光器接受的原始数据为： {0}",
                                //    ByteHelper.Byte2ReadalbeXstring(rawData)));
     
                            }
                            else
                            {
                                //数据异常，应该丢弃
                                SetRemainData(rawData);
                                LogHelper.GetLogger<SerialPortManager>().Error(string.Format("激光器接受的原始数据异常2，数据为： {0}",
    ByteHelper.Byte2ReadalbeXstring(rawData)));
                            }
                        }
                    }
                }
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

        public void Close()
        {
            if (serialPort.IsOpen)
            {
                SerialPortEventArgs args = new SerialPortEventArgs();
                args.isOpend = false;
                serialPort.Close();
            }
        }
    }
}
