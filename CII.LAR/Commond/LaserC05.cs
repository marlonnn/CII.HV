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
    public class LaserC05Request : BaseRequest
    {
        public override BasePackage Encode()
        {
            BasePackage bp = base.Encode();
            bp.Type = 0x05;
            bp.AppData = new byte[] { 0x05, 0x00 };
            return bp;
        }
    }

    /// <summary>
    /// 返回脉冲宽度和重复频率
    /// </summary>
    public class LaserC05Response : BaseResponse
    {
        /// <summary>
        /// 最小脉冲宽度
        /// </summary>
        private double pulseWidth;
        public double PulseWidth
        {
            get { return this.pulseWidth; }
            private set { this.pulseWidth = value; }
        }

        /// <summary>
        /// 最大脉冲宽度
        /// </summary>
        private double repeatFrequency;
        public double RepeatFrequency
        {
            get { return this.repeatFrequency; }
            private set { this.repeatFrequency = value; }
        }

        public override List<BaseResponse> Decode(BasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);

            LaserC05Response c05Response = new LaserC05Response();
            c05Response.DtTime = DateTime.Now;
            c05Response.OriginalBytes = obytes;
            //aa*128 + bb 脉冲宽度 T = data * 10 (单位ns)
            c05Response.PulseWidth = (obytes.Data[1] * 128 + obytes.Data[2]) * 10;
            //cc*128 + dd 重复频率 T = data * 0.1 (单位KHZ)
            c05Response.RepeatFrequency = (obytes.Data[3] * 128 + obytes.Data[4]) * 0.1;
            return CreateOneList(c05Response);
        }
    }
}
