using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 设置红光激光使能
    /// </summary>
    public class LaserC74Request : BaseRequest
    {
        /// <summary>
        /// 红光激光使能
        /// </summary>
        private byte redLight;
        public byte RedLight
        {
            get { return this.redLight; }
            private set { this.redLight = value; }
        }

        public LaserC74Request(byte redLight)
        {
            this.redLight = redLight;
            this.Type = 0x74;
        }

        public override List<BasePackage> Encode()
        {
            List<BasePackage> bps = base.Encode();
            BasePackage bp1 = new BasePackage(0x8F, 0x74, new byte[] { 0x74, 0x00 });
            bps.Add(bp1);

            BasePackage bp2 = new BasePackage(0x80, 0x74, new byte[] { 0x00, RedLight, 0x00, 0x00, 0x00, 0x00 });
            bps.Add(bp2);
            return bps;
        }
    }

    /// <summary>
    /// 80 FF 00 00 00 FF
    /// </summary>
    public class LaserC74Response : BaseResponse
    {
        public LaserC74Response()
        {
            this.Type = 0x74;
        }

        public override List<BaseResponse> Decode(BasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);
            if (CheckResponse(obytes.Data))
            {
                LaserC74Response c74Response = new LaserC74Response();
                c74Response.DtTime = DateTime.Now;
                c74Response.OriginalBytes = obytes;
                return CreateOneList(c74Response);
            }
            else
            {
                return null;
            }
        }
    }
}
