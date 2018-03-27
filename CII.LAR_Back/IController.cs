using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR
{
    public class IController
    {
        private SerialPortModel model;
        private IView view;
        public IController(IView view)
        {
            model = new SerialPortModel();
            this.view = view;
            view.SetController(this);
            model.laserComCloseEvent += new SerialPortEventHandler(view.LaserCloseComEvent);
            model.laserComOpenEvent += new SerialPortEventHandler(view.LaserOpenComEvent);

            model.motorComCloseEvent += new SerialPortEventHandler(view.MotorCloseComEvent);
            model.motorComOpenEvent += new SerialPortEventHandler(view.MotorOpenComEvent);
        }

        /// <summary>
        /// Hex to byte
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                try
                {
                    raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }
                catch (System.Exception)
                {
                    //Do Nothing
                }

            }
            return raw;
        }

        /// <summary>
        /// Hex string to string
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static String Hex2String(String hex)
        {
            byte[] data = FromHex(hex);
            return Encoding.Default.GetString(data);
        }

        /// <summary>
        /// String to hex string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String String2Hex(String str)
        {
            Byte[] data = Encoding.Default.GetBytes(str);
            return BitConverter.ToString(data);
        }

        /// <summary>
        /// Hex string to bytes
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Byte[] Hex2Bytes(String hex)
        {
            return FromHex(hex);
        }

        /// <summary>
        /// Bytes to Hex String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static String Bytes2Hex(Byte[] bytes)
        {
            return BitConverter.ToString(bytes);
        }

        /// <summary>
        /// send bytes to serial port
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SendDataToLaserCom(Byte[] data)
        {
            return model.LaserSendData(data);
        }

        /// <summary>
        /// send bytes to serial port
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SendDataToMotorCom(Byte[] data)
        {
            return model.MotorSendData(data);
        }

        public void OpenLaserSerialPort(string portName, String baudRate,
            string dataBits, string stopBits, string parity, string handshake)
        {
            if (portName != null && portName != "")
            {
                model.LaserSerialPortOpen(portName, baudRate, dataBits, stopBits, parity, handshake);
            }
        }

        public void OpenMotorSerialPort(string portName, String baudRate,
            string dataBits, string stopBits, string parity, string handshake)
        {
            if (portName != null && portName != "")
            {
                model.MotorSerialPortOpen(portName, baudRate, dataBits, stopBits, parity, handshake);
            }
        }

        public void CloseLaserSerialPort()
        {
            model.CloseLaserSerialThread();
        }

        public void CloseMotorSerialPort()
        {
            model.CloseMotorSerialThread();
        }
    }
}
