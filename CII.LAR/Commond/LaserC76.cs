﻿using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 清除警告
    /// </summary>
    public class LaserC76Request : LaserBaseRequest
    {
        public LaserC76Request()
        {
            this.Type = 0x76;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x76, new byte[] { 0x76, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 80 FF 00 00 00 FF
    /// </summary>
    public class LaserC76Response : LaserBaseResponse
    {
        public LaserC76Response()
        {
            this.Type = 0x76;
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
