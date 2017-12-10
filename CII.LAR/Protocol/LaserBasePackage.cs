using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class LaserBasePackage
    {
        /// <summary>
        /// 发送数据包头
        /// </summary>
        private byte markHead;

        public byte MarkHead
        {
            get { return this.markHead; }
            private set { this.markHead = value; }
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        private byte type;
        public byte Type
        {
            get { return this.type; }
            private set { this.type = value; }
        }

        /// <summary>
        /// 除去帧头和最后一位奇偶校验位
        /// </summary>
        private byte[] appData;
        public byte[] AppData
        {
            get { return this.appData; }
            private set { this.appData = value; }
        }

        public LaserBasePackage() { }

        public LaserBasePackage(byte markHead, byte type, byte[] appData)
        {
            this.markHead = markHead;
            this.type = type;
            this.appData = appData;
        }
    }
}
