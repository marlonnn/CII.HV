using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 查询模块状态信息和温度
    /// </summary>
    public class LaserC05Request : LaserBaseRequest
    {
        public LaserC05Request()
        {
            this.Type = 0x05;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x05, new byte[] { 0x05, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 返回脉冲宽度和重复频率
    /// </summary>
    public class LaserC05Response : LaserBaseResponse
    {
        /// <summary>
        /// 脉冲宽度
        /// </summary>
        private double pulseWidth;
        public double PulseWidth
        {
            get { return this.pulseWidth; }
            private set { this.pulseWidth = value; }
        }

        ///// <summary>
        ///// 最大脉冲宽度
        ///// </summary>
        //private double repeatFrequency;
        //public double RepeatFrequency
        //{
        //    get { return this.repeatFrequency; }
        //    private set { this.repeatFrequency = value; }
        //}

        public LaserC05Response()
        {
            this.Type = 0x05;
        }

        public override List<LaserBaseResponse> Decode(LaserBasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);

            LaserC05Response c05Response = new LaserC05Response();
            c05Response.DtTime = DateTime.Now;
            c05Response.OriginalBytes = obytes;
            //aa*128 + bb 脉冲宽度 T = data * 10 (单位ns)
            c05Response.PulseWidth = (obytes.Data[1] * 128 + obytes.Data[2]) * 10;
            //cc*128 + dd 重复频率 T = data * 0.1 (单位KHZ)
            //c05Response.RepeatFrequency = (obytes.Data[3] * 128 + obytes.Data[4]) * 0.1;
            return CreateOneList(c05Response);
        }
    }
}
