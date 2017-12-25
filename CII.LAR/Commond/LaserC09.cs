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
    public class LaserC09Request : LaserBaseRequest
    {
        public LaserC09Request()
        {
            this.Type = 0x09;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x09, new byte[] { 0x09, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 返回红光激光器电流设置值
    /// </summary>
    public class LaserC09Response : LaserBaseResponse
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

        public LaserC09Response()
        {
            this.Type = 0x09;
        }
        
        public override List<LaserBaseResponse> Decode(LaserBasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);
            if (CheckResponse(obytes.Data))
            {
                LaserC09Response c09Response = new LaserC09Response();
                c09Response.DtTime = DateTime.Now;
                c09Response.OriginalBytes = obytes;
                //cc*128 + dd = T 红光激光器电流设定值数字量 (data) T = (data / 4096) * 2500 (MA)
                c09Response.Current = (obytes.Data[3] * 128 + obytes.Data[4]) * 100 / COF;
                return CreateOneList(c09Response);
            }
            else
            {
                return null;
            }
        }
    }
}
