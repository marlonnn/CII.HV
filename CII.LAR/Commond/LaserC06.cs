using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 查询LD脉冲宽度 最大值最小值
    /// </summary>
    public class LaserC06Request : LaserBaseRequest
    {
        public LaserC06Request()
        {
            this.Type = 0x06;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x06, new byte[] { 0x06, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 返回脉冲宽度的最大值和最小值
    /// </summary>
    public class LaserC06Response : LaserBaseResponse
    {
        /// <summary>
        /// 最小脉冲宽度
        /// </summary>
        private double minimumPulseWidth;
        public double MinimumPulseWidth
        {
            get { return this.minimumPulseWidth; }
            private set { this.minimumPulseWidth = value; }
        }

        /// <summary>
        /// 最大脉冲宽度
        /// </summary>
        private double maxmumPulseWidth;
        public double MaxmumPulseWidth
        {
            get { return this.maxmumPulseWidth; }
            private set { this.maxmumPulseWidth = value; }
        }

        public LaserC06Response()
        {
            this.Type = 0x06;
        }

        public override LaserBaseResponse Decode(OriginalBytes obytes)
        {
            base.Decode(obytes);
            //aa*128 + bb 最小脉冲宽度 T = data(单位ns)
            this.MinimumPulseWidth = obytes.Data[1] * 128 + obytes.Data[2];
            //cc*128 + dd 最大脉冲宽度 T = data(单位ns)
            this.MaxmumPulseWidth = obytes.Data[3] * 128 + obytes.Data[4];
            return this;

        }

        public override string ToString()
        {
            string ret = "";
            if (this != null)
            {
                ret = PrintOriginalData() + "\n" + string.Format("1480激光最小脉宽： {0}us， 最大脉宽： {1}us", this.MinimumPulseWidth, this.MaxmumPulseWidth);
            }
            return ret;
        }
    }
}
