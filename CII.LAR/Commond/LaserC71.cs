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
        public override BasePackage Encode()
        {
            BasePackage bp = base.Encode();
            bp.Type = 0x71;
            bp.AppData = new byte[] { 0x71, 0x00 };
            return bp;
        }
    }

    /// <summary>
    /// 强制开启或关闭激光器
    /// 80 FF 00 00 00 FF
    /// </summary>
    public class LaserC71Response : BaseResponse
    {
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
