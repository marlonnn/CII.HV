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
    public class LaserC07Request : BaseRequest
    {
        public LaserC07Request()
        {
            this.Type = 0x07;
        }

        public override List<BasePackage> Encode()
        {
            List<BasePackage> bps = base.Encode();
            BasePackage bp = new BasePackage(0x8F, 0x07, new byte[] { 0x07, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 返回LD重复频率的最大值和最小值
    /// </summary>
    public class LaserC07Response : BaseResponse
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

        public override List<BaseResponse> Decode(BasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);

            LaserC07Response c07Response = new LaserC07Response();
            c07Response.DtTime = DateTime.Now;
            c07Response.OriginalBytes = obytes;
            //aa*128 + bb 最小脉冲宽度 T = data * 0.1 (单位 KHZ)
            c07Response.MinimumRepeatFrequency = (obytes.Data[1] * 128 + obytes.Data[2]) * 0.1;
            //cc*128 + dd 最大脉冲宽度 T = data * 0.1 (单位 KHZ)
            c07Response.MaxmumRepeatFrequency = (obytes.Data[3] * 128 + obytes.Data[4]) * 0.1;
            return CreateOneList(c07Response);
        }
    }
}
