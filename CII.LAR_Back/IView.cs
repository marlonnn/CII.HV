using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR
{
    public delegate void SerialPortEventHandler(Object sender, SerialPortEventArgs e);

    public class SerialPortEventArgs : EventArgs
    {
        public bool isOpend = false;
        public Byte[] receivedBytes = null;
    }

    public interface IView
    {
        void SetController(IController controller);

        void LaserOpenComEvent(Object sender, SerialPortEventArgs e);

        void LaserCloseComEvent(Object sender, SerialPortEventArgs e);

        void MotorOpenComEvent(Object sender, SerialPortEventArgs e);

        void MotorCloseComEvent(Object sender, SerialPortEventArgs e);
    }
}
