using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class LaserBaseResponse
    {
        private byte type;
        public byte Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        /// <summary>
        /// 消息收到的时间
        /// </summary>
        public DateTime DtTime;

        /// <summary>
        /// 原始数据
        /// </summary>
        public OriginalBytes OriginalBytes;

        private byte oddCheck;
        public byte OddCheck
        {
            get { return this.oddCheck; }
            private set { this.oddCheck = value; }
        }

        public virtual LaserBaseResponse Decode(OriginalBytes obytes)
        {
            this.OriginalBytes = obytes;
            this.DtTime = DateTime.Now;
            return null;
        }

        protected string PrintOriginalData()
        {
            string originalData = "";
            if (this.OriginalBytes != null && this.OriginalBytes.Data != null)
            {
                originalData = string.Format("接收到的原始数据为： {0}", ByteHelper.Byte2ReadalbeXstring(this.OriginalBytes.Data));
            }
            return originalData;
        }
    }
}
