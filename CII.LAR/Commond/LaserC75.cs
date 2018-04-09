using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 设置红光驱动电流数字量
    /// </summary>
    public class LaserC75Request : LaserBaseRequest
    {
        /// <summary>
        /// 电流设定系数
        /// </summary>
        private int ld2_cof = 4930;

        /// <summary>
        /// 写入的电流数字量
        /// </summary>
        private int currrent;
        public int Currrent
        {
            get { return this.currrent; }
            private set { this.currrent = value; }
        }

        public LaserC75Request(int currrent)
        {
            this.currrent = currrent;
            this.Type = 0x75;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp1 = new LaserBasePackage(0x8F, 0x75, new byte[] { 0x75, 0x00 });
            bps.Add(bp1);

            int digitalValue = (Currrent * ld2_cof) / 100;
            byte aa = (byte)(digitalValue / 128);
            byte bb = (byte)(digitalValue % 128);
            LaserBasePackage bp2 = new LaserBasePackage(0x80, 0x75, new byte[] { aa, bb, 0x00, 0x00, 0x00, 0x00 });
            bps.Add(bp2);
            return bps;
        }
    }

    /// <summary>
    /// 80 FF 00 00 00 FF
    /// </summary>
    public class LaserC75Response : LaserBaseResponse
    {
        public LaserC75Response()
        {
            this.Type = 0x75;
        }

        public override List<LaserBaseResponse> Decode(LaserBasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);
            if (CheckResponse(obytes.Data))
            {
                LaserC75Response c75Response = new LaserC75Response();
                c75Response.DtTime = DateTime.Now;
                c75Response.OriginalBytes = obytes;
                return CreateOneList(c75Response);
            }
            else
            {
                return null;
            }
        }
    }
}
