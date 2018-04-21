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
    public class LaserC71Request : LaserBaseRequest
    {
        public LaserC71Request()
        {
            this.Type = 0x71;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x71, new byte[] { 0x71, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 强制开启或关闭激光器
    /// 80 FF 00 00 00 FF
    /// </summary>
    public class LaserC71Response : LaserBaseResponse
    {
        public LaserC71Response()
        {
            this.Type = 0x71;
        }

        public override LaserBaseResponse Decode(OriginalBytes obytes)
        {
            base.Decode(obytes);
            return this;
        }

        public override string ToString()
        {
            string ret = "";
            if (this != null)
            {
                ret = PrintOriginalData() + "\n";
            }
            return ret;
        }

    }
}
