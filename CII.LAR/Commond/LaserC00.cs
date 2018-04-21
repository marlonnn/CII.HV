using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 与模块建立链接
    /// </summary>
    public class LaserC00Request : LaserBaseRequest
    {
        public LaserC00Request()
        {
            this.Type = 0x00;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x00, new byte[] { 0x00, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 返回查询模块版本号和工作时间
    /// </summary>
    public class LaserC00Response : LaserBaseResponse
    {
        /// <summary>
        /// 版本号
        /// </summary>
        private byte versionNumber;
        public byte VersionNumber
        {
            get { return this.versionNumber; }
            private set { this.versionNumber = value; }
        }

        private byte hour;
        public byte Hour
        {
            get { return this.hour; }
            private set { this.hour = value; }
        }

        private byte month;
        public byte Month
        {
            get { return this.month; }
            private set { this.month = value; }
        }

        private byte second;
        public byte Second
        {
            get { return this.second; }
            private set { this.second = value; }
        }

        public LaserC00Response()
        {
            this.Type = 0x00;
        }

        public override LaserBaseResponse Decode(OriginalBytes obytes)
        {
            base.Decode(obytes);

            this.DtTime = DateTime.Now;
            this.OriginalBytes = obytes;
            this.VersionNumber = obytes.Data[1];
            this.Hour = obytes.Data[2];
            this.Month = obytes.Data[3];
            this.Second = obytes.Data[4];

            return this;
        }

        public override string ToString()
        {
            string ret = "";
            if (this != null)
            {
                ret = PrintOriginalData() + "\n" + string.Format("版本号： {0}， 时： {1}， 分： {2}， 秒： {3}", this.VersionNumber, this.Hour, this.Month, this.Second);
            }
            return ret;
        }
    }
}
