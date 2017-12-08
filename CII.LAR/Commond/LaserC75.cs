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
    public class LaserC75Request : BaseRequest
    {
        /// <summary>
        /// 电流设定系数
        /// </summary>
        private int ld2_cof = 1;

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

        public override List<BasePackage> Encode()
        {
            List<BasePackage> bps = base.Encode();
            BasePackage bp1 = new BasePackage(0x8F, 0x75, new byte[] { 0x75, 0x00 });
            bps.Add(bp1);

            int digitalValue = (Currrent * 100 ) / ld2_cof;
            byte aa = (byte)(digitalValue / 128);
            byte bb = (byte)(digitalValue % 128);
            BasePackage bp2 = new BasePackage(0x80, 0x75, new byte[] { aa, bb, 0x00, 0x00, 0x00, 0x00 });
            bps.Add(bp2);
            return bps;
        }
    }

    /// <summary>
    /// 80 FF 00 00 00 FF
    /// </summary>
    public class LaserC75Response : BaseResponse
    {
        public LaserC75Response()
        {
            this.Type = 0x75;
        }

        public override List<BaseResponse> Decode(BasePackage bp, OriginalBytes obytes)
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
