using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 设置脉宽输入量数字量
    /// </summary>
    public class LaserC72Request : LaserBaseRequest
    {
        /// <summary>
        /// 脉宽的最小间隔为5us
        /// </summary>
        private double interval = 5;

        /// <summary>
        /// 写入的脉宽数字量
        /// </summary>
        private double pulseWidth;
        public double PulseWidth
        {
            get { return this.pulseWidth; }
            private set { this.pulseWidth = value; }
        }

        public LaserC72Request(double pulseWidth)
        {
            this.pulseWidth = pulseWidth;
            this.Type = 0x72;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp1 = new LaserBasePackage(0x8F, 0x72, new byte[] { 0x72, 0x00 });
            bps.Add(bp1);

            double digitalValue = PulseWidth * 10 /*/ interval*/;
            byte aa = (byte)(digitalValue / 128);
            byte bb = (byte)(digitalValue % 128);
            LaserBasePackage bp2 = new LaserBasePackage(0x80, 0x72, new byte[] { aa, bb, 0x00, 0x00, 0x00, 0x00 });
            bps.Add(bp2);
            return bps;
        }
    }

    /// <summary>
    /// 80 FF 00 00 00 FF
    /// </summary>
    public class LaserC72Response : LaserBaseResponse
    {
        public LaserC72Response()
        {
            this.Type = 0x72;
        }

        public override LaserBaseResponse Decode(OriginalBytes obytes)
        {
            base.Decode(obytes);
            return this;
        }
        public override string ToString()
        {
            string ret = "";
            if (this != null)
            {
                ret = PrintOriginalData() + "\n";
            }
            return ret;
        }
    }
}
