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
    public class LaserC0BRequest : LaserBaseRequest
    {
        public LaserC0BRequest()
        {
            this.Type = 0x0B;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x0B, new byte[] { 0x0B, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 返回红光激光器电流设定系数
    /// </summary>
    public class LaserC0BResponse : LaserBaseResponse
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

        public override LaserBaseResponse Decode(OriginalBytes obytes)
        {
            base.Decode(obytes);
            //cc*128 + dd = T 红光激光器电流设定值系数
            this.COF = obytes.Data[3] * 128 + obytes.Data[4];
            Program.SysConfig.LaserConfig.COF = this.COF;
            return this;
        }

        public override string ToString()
        {
            string ret = "";
            if (this != null)
            {
                ret = PrintOriginalData() + "\n" + string.Format("红光电流系数： {0}", this.COF);
            }
            return ret;
        }
    }
}
