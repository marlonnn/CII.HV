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
    public class LaserC74Request : LaserBaseRequest
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

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp1 = new LaserBasePackage(0x8F, 0x74, new byte[] { 0x74, 0x00 });
            bps.Add(bp1);

            LaserBasePackage bp2 = new LaserBasePackage(0x80, 0x74, new byte[] { 0x00, RedLight, 0x00, 0x00, 0x00, 0x00 });
            bps.Add(bp2);
            return bps;
        }
    }

    /// <summary>
    /// 80 FF 00 00 00 FF
    /// </summary>
    public class LaserC74Response : LaserBaseResponse
    {
        public LaserC74Response()
        {
            this.Type = 0x74;
        }

        public override List<LaserBaseResponse> Decode(LaserBasePackage bp, OriginalBytes obytes)
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
