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

        public override List<LaserBaseResponse> Decode(LaserBasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);

            LaserC06Response c06Response = new LaserC06Response();
            c06Response.DtTime = DateTime.Now;
            c06Response.OriginalBytes = obytes;
            //aa*128 + bb 最小脉冲宽度 T = data * 10 (单位ns)
            c06Response.MinimumPulseWidth = (obytes.Data[1] * 128 + obytes.Data[2]) * 10;
            //cc*128 + dd 最大脉冲宽度 T = data * 10 (单位ns)
            c06Response.MaxmumPulseWidth = (obytes.Data[3] * 128 + obytes.Data[4]) * 10;
            return CreateOneList(c06Response);
        }
    }
}
