using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    
    /// <summary>
    /// 查询红光激光器电流设定系数
    /// </summary>
    public class LaserC0BRequest : BaseRequest
    {
        public LaserC0BRequest()
        {
            this.Type = 0x0B;
        }

        public override List<BasePackage> Encode()
        {
            List<BasePackage> bps = base.Encode();
            BasePackage bp = new BasePackage(0x8F, 0x0B, new byte[] { 0x0B, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 返回红光激光器电流设定系数
    /// </summary>
    public class LaserC0BResponse : BaseResponse
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

        public LaserC0BResponse()
        {
            this.Type = 0x0B;
        }

        public override List<BaseResponse> Decode(BasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);

            LaserC0BResponse c0BResponse = new LaserC0BResponse();
            c0BResponse.DtTime = DateTime.Now;
            c0BResponse.OriginalBytes = obytes;
            //cc*128 + dd = T 红光激光器电流设定值系数
            c0BResponse.COF = obytes.Data[3] * 128 + obytes.Data[4];
            return CreateOneList(c0BResponse);
        }
    }
}
