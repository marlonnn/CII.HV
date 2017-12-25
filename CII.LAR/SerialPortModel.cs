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
                    LaserProtocolFactory.GetInstance().RxQueue.Push(new OriginalBytes(DateTime.Now, data));
                    LogHelper.GetLogger<SerialPortModel>().Error(string.Format("激光器接受到的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(data)));
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
                    MotorProtocolFactory.GetInstance().RxQueue.Push(new OriginalBytes(DateTime.Now, data));
                    LogHelper.GetLogger<SerialPortModel>().Error(string.Format("电机接受到的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(data)));
                }
                catch (System.Exception ex)
                {
                    LogHelper.GetLogger<SerialPortModel>().Error(string.Format("电机串口接收数据异常： {0}", ex.StackTrace));
                }
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
            if (laserComOpenEvent != null)
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
