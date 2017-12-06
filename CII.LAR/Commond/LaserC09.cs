using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    
    /// <summary>
    /// 查询红光激光器电流上限
    /// </summary>
    public class LaserC09Request : BaseRequest
    {
        public override BasePackage Encode()
        {
            BasePackage bp = base.Encode();
            bp.Type = 0x09;
            bp.AppData = new byte[] { 0x09, 0x00 };
            return bp;
        }
    }

    /// <summary>
    /// 返回红光激光器电流设置值
    /// </summary>
    public class LaserC09Response : BaseResponse
    {
        /// <summary>
        /// LD对应电流设定系数
        /// </summary>
        private float cof = 1F;
        public float COF
        {
            get { return this.cof; }
            private set { this.cof = value; }
        }

        /// <summary>
        /// 红光激光器电流设定值
        /// </summary>
        private double current;
        public double Current
        {
            get { return this.current; }
            private set { this.current = value; }
        }

        public override List<BaseResponse> Decode(BasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);

            LaserC09Response c09Response = new LaserC09Response();
            c09Response.DtTime = DateTime.Now;
            c09Response.OriginalBytes = obytes;
            //cc*128 + dd = T 红光激光器电流设定值数字量 (data) T = (data / 4096) * 2500 (MA)
            c09Response.Current = (obytes.Data[3] * 128 + obytes.Data[4]) * 100 / COF;
            return CreateOneList(c09Response);
        }
    }
}
