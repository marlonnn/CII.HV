using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    
    /// <summary>
    /// 查询红光激光器电流
    /// </summary>
    public class LaserC04Request : BaseRequest
    {
        public override BasePackage Encode()
        {
            BasePackage bp = base.Encode();
            bp.Type = 0x04;
            bp.AppData = new byte[] { 0x04, 0x00 };
            return bp;
        }
    }

    public class LaserC04Response : BaseResponse
    {
        /// <summary>
        /// 红光激光器电流数字量
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

            LaserC04Response c04Response = new LaserC04Response();
            c04Response.DtTime = DateTime.Now;
            c04Response.OriginalBytes = obytes;
            //cc*128 + dd = T 电流数字量 (data) T = (data / 4096) * 2500 (MA)
            c04Response.Current = ((obytes.Data[3] * 128 + obytes.Data[4]) / 4096) * 2500;
            return CreateOneList(c04Response);
        }
    }
}
