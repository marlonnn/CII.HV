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
    public class LaserC08Request : BaseRequest
    {
        public LaserC08Request()
        {
            this.Type = 0x08;
        }

        public override List<BasePackage> Encode()
        {
            List<BasePackage> bps = base.Encode();
            BasePackage bp = new BasePackage(0x8F, 0x08, new byte[] { 0x08, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    public class LaserC08Response : BaseResponse
    {
        /// <summary>
        /// LD对应电流设定系数
        /// </summary>
        private float ld_cof = 1F;
        public float LD_COF
        {
            get { return this.ld_cof; }
            private set { this.ld_cof = value; }
        }
        /// <summary>
        /// 红光激光器电流上限数字量
        /// </summary>
        private double current;
        public double Current
        {
            get { return this.current; }
            private set { this.current = value; }
        }

        public LaserC08Response()
        {
            this.Type = 0x08;
        }

        public override List<BaseResponse> Decode(BasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);

            LaserC08Response c08Response = new LaserC08Response();
            c08Response.DtTime = DateTime.Now;
            c08Response.OriginalBytes = obytes;
            //cc*128 + dd = T 红光激光器电流上限数字量 (data) T = (data / 4096) * 2500 (MA)
            c08Response.Current = (obytes.Data[3] * 128 + obytes.Data[4]) * 100 / LD_COF;
            return CreateOneList(c08Response);
        }
    }
}
