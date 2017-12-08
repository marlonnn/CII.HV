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
    public class LaserC00Request : BaseRequest
    {
        public LaserC00Request()
        {
            this.Type = 0x00;
        }

        public override List<BasePackage> Encode()
        {
            List<BasePackage> bps = base.Encode();
            BasePackage bp = new BasePackage(0x8F, 0x00, new byte[] { 0x00, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 返回查询模块版本号和工作时间
    /// </summary>
    public class LaserC00Response : BaseResponse
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

        public override List<BaseResponse> Decode(BasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);

            LaserC00Response c00Response = new LaserC00Response();
            c00Response.DtTime = DateTime.Now;
            c00Response.OriginalBytes = obytes;
            c00Response.VersionNumber = obytes.Data[1];
            c00Response.Hour = obytes.Data[2];
            c00Response.Month = obytes.Data[3];
            c00Response.Second = obytes.Data[4];

            return CreateOneList(c00Response);
        }
    }
}
