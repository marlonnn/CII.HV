using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class MotorProtocol
    {
        public MotorProtocol DePackage(byte[] data)
        {
            MotorProtocol mp = new MotorProtocol();
            return mp;
        }

        public byte[] EnPackage(byte[] data)
        {
            byte[] enData = new byte[data.Length + 9];
            return enData;
        }
    }
}
