using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 强制开关指令 
    /// </summary>
    public class LaserC71Request : BaseRequest
    {
        public LaserC71Request()
        {
            this.Type = 0x71;
        }

        public override List<BasePackage> Encode()
        {
            List<BasePackage> bps = base.Encode();
            BasePackage bp = new BasePackage(0x8F, 0x71, new byte[] { 0x71, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 强制开启或关闭激光器
    /// 80 FF 00 00 00 FF
    /// </summary>
    public class LaserC71Response : BaseResponse
    {
        public LaserC71Response()
        {
            this.Type = 0x71;
        }

        public override List<BaseResponse> Decode(BasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);
            if (CheckResponse(obytes.Data))
            {
                LaserC71Response c71Response = new LaserC71Response();
                c71Response.DtTime = DateTime.Now;
                c71Response.OriginalBytes = obytes;
                return CreateOneList(c71Response);
            }
            else
            {
                return null;
            }
        }
    }
}
