using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR
{
    public interface IView
    {
        void SetController(IController controller);

        void LaserOpenComEvent(Object sender, SerialPortEventArgs e);

        void LaserCloseComEvent(Object sender, SerialPortEventArgs e);

        void MotorOpenComEvent(Object sender, SerialPortEventArgs e);

        void MotorCloseComEvent(Object sender, SerialPortEventArgs e);
    }
}
