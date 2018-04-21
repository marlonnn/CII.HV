using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 查询LD重复频率 最大值最小值
    /// </summary>
    public class LaserC07Request : LaserBaseRequest
    {
        public LaserC07Request()
        {
            this.Type = 0x07;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x07, new byte[] { 0x07, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 返回LD重复频率的最大值和最小值
    /// </summary>
    public class LaserC07Response : LaserBaseResponse
    {
        /// <summary>
        /// 最小重复频率
        /// </summary>
        private double minimumRepeatFrequency;
        public double MinimumRepeatFrequency
        {
            get { return this.minimumRepeatFrequency; }
            private set { this.minimumRepeatFrequency = value; }
        }

        /// <summary>
        /// 最大重复频率
        /// </summary>
        private double maxmumRepeatFrequency;
        public double MaxmumRepeatFrequency
        {
            get { return this.maxmumRepeatFrequency; }
            private set { this.maxmumRepeatFrequency = value; }
        }

        public LaserC07Response()
        {
            this.Type = 0x07;
        }

        public override LaserBaseResponse Decode(OriginalBytes obytes)
        {
            base.Decode(obytes);
            //aa*128 + bb 最小脉冲宽度 T = data * 0.1 (单位 KHZ)
            this.MinimumRepeatFrequency = (obytes.Data[1] * 128 + obytes.Data[2]) * 0.1;
            //cc*128 + dd 最大脉冲宽度 T = data * 0.1 (单位 KHZ)
            this.MaxmumRepeatFrequency = (obytes.Data[3] * 128 + obytes.Data[4]) * 0.1;
            return this;
        }
    }
}
