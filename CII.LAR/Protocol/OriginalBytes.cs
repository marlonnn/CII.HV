using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class OriginalBytes : Original
    {
        public byte[] Data { get; set; }

        public OriginalBytes()
        {
        }

        public OriginalBytes(DateTime dt,  byte[] msg)
        {
            RxTime = dt;
            Data = msg;
        }
    }
}
