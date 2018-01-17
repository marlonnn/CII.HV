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
    public class SerialPortModel
    {
        private SerialPort laserSerialPort = new SerialPort();
        private SerialPort motorSerialPort = new SerialPort();

        public event SerialPortEventHandler laserComOpenEvent = null;
        public event SerialPortEventHandler laserComCloseEvent = null;

        public event SerialPortEventHandler motorComOpenEvent = null;
        public event SerialPortEventHandler motorComCloseEvent = null;

        private Object thisLock = new Object();

        private void LaserDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (laserSerialPort.BytesToRead <= 0)
            {
                return;
            }

            //lock (thisLock)
            {
                int len = laserSerialPort.BytesToRead;
                Byte[] data = new Byte[len];
                try
                {
                    laserSerialPort.Read(data, 0, len);
                    var destData = CheckLarData(data, len);
                    if (destData != null)
                    {
                        LaserProtocolFactory.GetInstance().RxQueue.Push(new OriginalBytes(DateTime.Now, destData));
                        LogHelper.GetLogger<SerialPortModel>().Error(string.Format("激光器接受到的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(destData)));
                    }
                }
                catch (System.Exception ex)
                {
                    LogHelper.GetLogger<SerialPortModel>().Error(string.Format("激光器串口接收数据异常： {0}", ex.StackTrace));
                }
            }
        }

        private void MotorDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (motorSerialPort.BytesToRead <= 0)
            {
                return;
            }

            //lock (thisLock)
            {
                var breakstatus = motorSerialPort.BreakState;
                int len = motorSerialPort.BytesToRead;
                Byte[] data = new Byte[len];
                try
                {
                    motorSerialPort.Read(data, 0, len);
                    var datas= CheckData(data, len);
                    if (datas != null)
                    {
                        foreach (var rawData in datas)
                        {
                            var cleanData = Check88Data(rawData);

                            if (CheckDataValidate(cleanData))
                            {
                                MotorProtocolFactory.GetInstance().RxQueue.Push(new OriginalBytes(DateTime.Now, cleanData));
                                LogHelper.GetLogger<SerialPortModel>().Error(string.Format("完整数据为： {0}", ByteHelper.Byte2ReadalbeXstring(cleanData)));
                            }
                        }
                    }
                    //var destData = CheckCIIData(data, len);
                    //if (destData != null)
                    //{
                    //    foreach (var d in destData)
                    //    {
                    //        MotorProtocolFactory.GetInstance().RxQueue.Push(new OriginalBytes(DateTime.Now, d));
                    //        LogHelper.GetLogger<SerialPortModel>().Error(string.Format("电机接受到的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(d)));
                    //    }
                    //}
                }
                catch (System.Exception ex)
                {
                    LogHelper.GetLogger<SerialPortModel>().Error(string.Format("电机串口接收数据异常： {0}", ex.StackTrace));
                }
            }
        }

        private byte[] laserBuffer;

        /// <summary>
        /// 激光器数据有效性检查
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private byte[] CheckLarData(byte[] srcBytes, int length)
        {
            if (srcBytes != null)
            {
                if (srcBytes[0] == 0x80 && srcBytes[length - 1] == 0xFF)
                {
                    return srcBytes;
                }
                else if (srcBytes[0] == 0x80 && srcBytes[length - 1] != 0xFF)
                {
                    laserBuffer = new byte[length];
                    Array.Copy(srcBytes, 0, laserBuffer, 0, length);
                    return null;
                }
                else if (srcBytes[0] != 0x80 && srcBytes[length - 1] == 0xFF)
                {
                    byte[] newBytes = new byte[laserBuffer.Length + length];
                    Array.Copy(laserBuffer, 0, newBytes, 0, laserBuffer.Length);
                    Array.Copy(srcBytes, 0, newBytes, laserBuffer.Length, length);
                    return newBytes;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        private byte[] motorBuffer;

        private int tailCount = 0;

        private int headCount = 0;

        private bool hasHead = false;
        public bool HasHead
        {
            get { return this.hasHead; }
            set
            {
                if (value != this.hasHead)
                {
                    if (value)
                    {
                        if (headCount == 1)
                        {
                            headCount = 0;
                        }
                        headCount++;
                    }
                    this.hasHead = value;
                }
            }
        }

        private bool hasTail = false;
        public bool HasTail
        {
            get { return this.hasTail; }
            set
            {
                if (value != this.hasTail)
                {
                    if (value)
                    {
                        if (tailCount == 1)
                        {
                            tailCount = 0;
                        }
                        tailCount++;
                    }
                    this.hasTail = value;
                }
            }
        }

        private byte[] Check88Data(byte[] srcBytes)
        {
            List<int> indexs = new List<int>();

            if (srcBytes != null && srcBytes.Length > 10)
            {
                for (int i = 10; i < srcBytes.Length - 2; i++)
                {
                    if (srcBytes[i] == 0x88 && srcBytes[i -1] == 0x5D)
                    {
                        indexs.Add(i);
                    }
                }
            }

            List<byte> bs = new List<byte>(srcBytes);
            foreach (int index in indexs)
            {
                bs.RemoveAt(index);
            }
            return bs.ToArray();
        }

        /// <summary>
        /// 数据有效性检查，包含CRC16校验
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <returns></returns>
        private bool CheckDataValidate(byte[] srcBytes)
        {
            bool validate = false;
            int length = srcBytes.Length;
            if (srcBytes[0] == 0x5D && srcBytes[1] == 0x5B && srcBytes[length - 1] == 0x5D && srcBytes[length - 2] == 0x5D)
            {
                Byte uchCRCHi = 0xFF;            /*  High CRC Byte initialize */
                Byte uchCRCLo = 0xFF;            /*  Low CRC Byte initialize  */
                int uIndex;                      /*  CRC loop index */
                for (int i = 2; i < srcBytes.Length - 4; i++)
                {
                    uIndex = uchCRCHi ^ srcBytes[i];
                    uchCRCHi = (Byte)(uchCRCLo ^ CRC16.auchCRCHi[uIndex]);
                    uchCRCLo = CRC16.auchCRCLo[uIndex];
                }
                var crc = (UInt16)(uchCRCLo << 8 | uchCRCHi);
                byte[] crc16 = BitConverter.GetBytes(crc);
                if (crc16[0] == srcBytes[length - 4] && crc16[1] == srcBytes[length - 3])
                {
                    validate = true;
                }
            }
            return validate;
        }

        /// <summary>
        /// 数据完整性检查
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private List<byte[]> CheckData(byte[] srcBytes, int length)
        {
            List<byte[]> rawData = new List<byte[]>();
            if (srcBytes != null && length > 4)
            {
                for (int i=0; i<srcBytes.Length - 1; i++)
                {
                    HasHead |= (srcBytes[i] == 0x5D && srcBytes[i + 1] == 0x5B);
                    HasTail |= (srcBytes[i] == 0x5D && srcBytes[i + 1] == 0x5D);
                    if ((headCount == 1 && HasHead) && (tailCount == 1 && HasTail))
                    {
                        //5D 5B *** 5D 5B
                        HasHead = false;
                        HasTail = false;
                        int len = i + 2;
                        byte[] data = new byte[len];
                        Array.Copy(srcBytes, 0, data, 0, len);
                        rawData.Add(data);

                        if (len < length)
                        {
                            int overLen = length - len;
                            byte[] overData = new byte[overLen];
                            Array.Copy(srcBytes, len, overData, 0, overLen);
                            CheckData(overData, overLen);
                        }
                    }
                    else if ((headCount == 1 && HasHead) && !HasTail)
                    {
                        //5D 5B ***
                        int len = i + 2;
                        if (len == length)
                        {
                            HasHead = false;
                            HasTail = false;
                            motorBuffer = new byte[len];
                            Array.Copy(srcBytes, 0, motorBuffer, 0, len);
                        }

                        //if (len < length)
                        //{
                        //    int overLen = length - len;
                        //    byte[] overData = new byte[overLen];
                        //    Array.Copy(srcBytes, overLen, overData, 0, overLen);
                        //    CheckData(overData, overLen);
                        //}
                    }
                    else if (!HasHead && (tailCount == 1 && HasTail))
                    {
                        //*** 5D 5D
                        HasHead = false;
                        HasTail = false;

                        int len = i + 2;

                        if (len < length)
                        {
                            int overLen = length - len;
                            byte[] overData = new byte[overLen];
                            Array.Copy(srcBytes, overLen, overData, 0, overLen);
                            CheckData(overData, overLen);
                        }
                        else if (len == length)
                        {
                            if (motorBuffer != null && motorBuffer.Length != 0)
                            {
                                byte[] data = new byte[len + motorBuffer.Length];
                                Array.Copy(motorBuffer, 0, data, 0, motorBuffer.Length);
                                Array.Copy(srcBytes, 0, data, motorBuffer.Length, srcBytes.Length);
                                rawData.Add(data);
                            }
                        }
                    }
                }
            }
            return rawData;
        }

        /// <summary>
        /// 数据完整性检查
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private List<byte[]> CheckCIIData(byte[] srcBytes, int length)
        {
            if (srcBytes != null && length > 4)
            {
                List<byte[]> reBytes = new List<byte[]>();
                if (srcBytes[0] == 0x5D && srcBytes[1] == 0x5B && srcBytes[length - 1] == 0x5D && srcBytes[length - 2] == 0x5D)
                {
                    reBytes.Add(srcBytes);
                    return reBytes;
                }
                else if ((srcBytes[0] == 0x5D && srcBytes[1] == 0x5B) && (srcBytes[length - 1] != 0x5D && srcBytes[length - 2] != 0x5D))
                {
                    motorBuffer = new byte[length];
                    Array.Copy(srcBytes, 0, motorBuffer, 0, length);
                    return null;
                }
                else if ((srcBytes[0] != 0x5D && srcBytes[1] != 0x5B) && (srcBytes[length - 1] == 0x5D && srcBytes[length - 2] == 0x5D))
                {
                    int count = 0;
                    for (int i = 0; i < length; i++)
                    {
                        if (srcBytes[i] == 0x5D)
                        {
                            if (i + 1 < length)
                            {
                                if (srcBytes[i + 1] == 0x5D)
                                {
                                    count = i;
                                    break;
                                }
                            }
                        }
                    }
                    if (count + 2 == length)
                    {
                        byte[] newBytes = new byte[motorBuffer.Length + length];
                        Array.Copy(motorBuffer, 0, newBytes, 0, motorBuffer.Length);
                        Array.Copy(srcBytes, 0, newBytes, motorBuffer.Length, length);
                        reBytes.Add(newBytes);
                    }
                    else if (count + 2 < length)
                    {
                        int index = count + 1;
                        byte[] newBytes1 = new byte[motorBuffer.Length + index + 1];
                        Array.Copy(motorBuffer, 0, newBytes1, 0, motorBuffer.Length);
                        Array.Copy(srcBytes, 0, newBytes1, motorBuffer.Length, index + 1);

                        byte[] newBytes2 = new byte[length - (index + 1)];
                        Array.Copy(srcBytes, index + 1, newBytes2, 0, newBytes2.Length);
                        reBytes.Add(newBytes1);
                        reBytes.Add(newBytes2);
                    }
                    return reBytes;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// LaserSendData bytes to device
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public bool LaserSendData(Byte[] bytes)
        {
            if (!laserSerialPort.IsOpen)
            {
                //LogHelper.GetLogger<SerialPortModel>().Error("激光器串口未打开， 发送数据失败");
                return false;
            }

            try
            {
                laserSerialPort.Write(bytes, 0, bytes.Length);
                LogHelper.GetLogger<SerialPortModel>().Error(string.Format("激光器发送的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(bytes)));
            }
            catch (System.Exception ex)
            {
                LogHelper.GetLogger<SerialPortModel>().Error(string.Format("激光器串口发送数据异常： {0}", ex.StackTrace));
                return false; 
            }
            return true;
        }

        public bool MotorSendData(Byte[] bytes)
        {
            if (!motorSerialPort.IsOpen)
            {
                //LogHelper.GetLogger<SerialPortModel>().Error("电机串口未打开， 发送数据失败");
                return false;
            }

            try
            {
                motorSerialPort.Write(bytes, 0, bytes.Length);
                LogHelper.GetLogger<SerialPortModel>().Error(string.Format("电机发送的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(bytes)));
            }
            catch (System.Exception ex)
            {
                LogHelper.GetLogger<SerialPortModel>().Error(string.Format("电机串口发送数据异常： {0}", ex.StackTrace));
                return false;
            }
            return true; 
        }

        /// <summary>
        /// Open Laser Serial port
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <param name="parity"></param>
        /// <param name="handshake"></param>
        public void LaserSerialPortOpen(string portName, String baudRate,
            string dataBits, string stopBits, string parity,
            string handshake)
        {
            if (laserSerialPort.IsOpen)
            {
                CloseLaserSerialThread();
            }
            laserSerialPort.PortName = portName;
            laserSerialPort.BaudRate = Convert.ToInt32(baudRate);
            laserSerialPort.DataBits = Convert.ToInt16(dataBits);

            /**
             *  If the Handshake property is set to None the DTR and RTS pins 
             *  are then freed up for the common use of Power, the PC on which
             *  this is being typed gives +10.99 volts on the DTR pin & +10.99
             *  volts again on the RTS pin if set to true. If set to false 
             *  it gives -9.95 volts on the DTR, -9.94 volts on the RTS. 
             *  These values are between +3 to +25 and -3 to -25 volts this 
             *  give a dead zone to allow for noise immunity.
             *  http://www.codeproject.com/Articles/678025/Serial-Comms-in-Csharp-for-Beginners
             */
            if (handshake == "None")
            {
                //Never delete this property
                laserSerialPort.RtsEnable = true;
                laserSerialPort.DtrEnable = true;
            }

            SerialPortEventArgs args = new SerialPortEventArgs();
            try
            {
                laserSerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits);
                laserSerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
                laserSerialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), handshake);
                laserSerialPort.WriteTimeout = 1000; /*Write time out*/
                laserSerialPort.Open();
                laserSerialPort.DataReceived += new SerialDataReceivedEventHandler(LaserDataReceived);
                args.isOpend = true;
            }
            catch (System.Exception)
            {
                args.isOpend = false;
            }
            if (laserComOpenEvent != null)
            {
                laserComOpenEvent.Invoke(this, args);
            }

        }

        /// <summary>
        /// Open motor serial port
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <param name="parity"></param>
        /// <param name="handshake"></param>
        public void MotorSerialPortOpen(string portName, String baudRate,
            string dataBits, string stopBits, string parity,
            string handshake)
        {
            if (motorSerialPort.IsOpen)
            {
                CloseMotorSerialThread();
            }
            motorSerialPort.PortName = portName;
            motorSerialPort.BaudRate = Convert.ToInt32(baudRate);
            motorSerialPort.DataBits = Convert.ToInt16(dataBits);

            /**
             *  If the Handshake property is set to None the DTR and RTS pins 
             *  are then freed up for the common use of Power, the PC on which
             *  this is being typed gives +10.99 volts on the DTR pin & +10.99
             *  volts again on the RTS pin if set to true. If set to false 
             *  it gives -9.95 volts on the DTR, -9.94 volts on the RTS. 
             *  These values are between +3 to +25 and -3 to -25 volts this 
             *  give a dead zone to allow for noise immunity.
             *  http://www.codeproject.com/Articles/678025/Serial-Comms-in-Csharp-for-Beginners
             */
            if (handshake == "None")
            {
                //Never delete this property
                motorSerialPort.RtsEnable = true;
                motorSerialPort.DtrEnable = true;
            }

            SerialPortEventArgs args = new SerialPortEventArgs();
            try
            {
                motorSerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits);
                motorSerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
                motorSerialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), handshake);
                motorSerialPort.WriteTimeout = 1000; /*Write time out*/
                motorSerialPort.ReadTimeout = 1000;
                motorSerialPort.Open();
                motorSerialPort.DataReceived += new SerialDataReceivedEventHandler(MotorDataReceived);
                args.isOpend = true;
            }
            catch (System.Exception)
            {
                args.isOpend = false;
            }
            if (motorComOpenEvent != null)
            {
                motorComOpenEvent.Invoke(this, args);
            }

        }

        /// <summary>
        /// Close serial port
        /// </summary>
        public void CloseLaserSerialThread()
        {
            Thread closeThread = new Thread(new ThreadStart(CloseLaserSpThread));
            closeThread.Start();
        }

        public void CloseMotorSerialThread()
        {
            Thread closeThread = new Thread(new ThreadStart(CloseMotorSpThread));
            closeThread.Start();
        }

        /// <summary>
        /// Close serial port thread
        /// </summary>
        private void CloseLaserSpThread()
        {
            SerialPortEventArgs args = new SerialPortEventArgs();
            args.isOpend = false;
            try
            {
                laserSerialPort.Close(); //close the serial port
                laserSerialPort.DataReceived -= new SerialDataReceivedEventHandler(LaserDataReceived);
            }
            catch (Exception)
            {
                args.isOpend = true;
            }
            if (laserComCloseEvent != null)
            {
                laserComCloseEvent.Invoke(this, args);
            }

        }

        private void CloseMotorSpThread()
        {
            SerialPortEventArgs args = new SerialPortEventArgs();
            args.isOpend = false;
            try
            {
                motorSerialPort.Close(); //close the serial port
                motorSerialPort.DataReceived -= new SerialDataReceivedEventHandler(MotorDataReceived);
            }
            catch (Exception)
            {
                args.isOpend = true;
            }
            if (motorComCloseEvent != null)
            {
                motorComCloseEvent.Invoke(this, args);
            }
        }
    }
}
