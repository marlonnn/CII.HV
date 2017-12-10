using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    /// <summary>
    /// CII数据通信传输内容
    /// 
    /// 帧头（0x5D, 0x5B） + 目标路径长度（0x01） + 目标地址区域( 0x21) + 源路径长度 (0x01) + 源地址区域 (0xFE) + 编码区 + 帧尾（0x5D, 0x5D ）
    /// 
    /// 编码区 ：
    /// 命令码 + 命令扩展码 + 通讯数据长度（两位） + 数据区 + 校验区（CRC16）
    /// 
    /// 命令扩展码:
    /// 读:0x55
    /// 写:0x66
    /// 读回应:0xAA
    /// 写回应:0x99
    /// 
    /// 钟文 2017/12/10
    /// </summary>
    public class CIIBasePackage
    {
        /// <summary>
        /// 帧头
        /// </summary>
        private byte[] markHead;
        public byte[] MarkHead
        {
            get { return this.markHead; }
            set { this.markHead = value; }
        }

        /// <summary>
        /// 目标地址长度
        /// </summary>
        private byte destLength;
        public byte DestLength
        {
            get { return this.destLength; }
            set { this.destLength = value; }
        }

        /// <summary>
        /// 目标地址区域
        /// </summary>
        private byte destRegion;
        public byte DestRegion
        {
            get { return this.destRegion; }
            set { this.destRegion = value; }
        }

        /// <summary>
        /// 源地址长度
        /// </summary>
        private byte sourceLength;
        public byte SourceLength
        {
            get { return this.sourceLength; }
            set { this.sourceLength = value; }
        }

        /// <summary>
        /// 源地址区域
        /// </summary>
        private byte sourceRegion;
        public byte SourceRegion
        {
            get { return this.sourceRegion; }
            set { this.sourceRegion = value; }
        }

        /// <summary>
        /// 出去帧头和帧尾数据 
        /// </summary>
        private byte[] appData;

        public byte[] AppData
        {
            get { return this.appData; }
            private set { this.appData = value; }
        }

        /// <summary>
        /// 帧尾
        /// </summary>
        private byte[] markTail;
        public byte[] MarkTail
        {
            get { return this.markTail; }
            set { this.markTail = value; }
        }

        public CIIBasePackage(byte[] appData)
        {
            this.markHead = new byte[] { 0x5D, 0x5B };
            this.DestLength = 0x01;
            this.DestRegion = 0x21;

            this.SourceLength = 0x01;
            this.SourceRegion = 0xFE;

            this.appData = appData;

            this.markTail = new byte[] { 0x5D, 0x5D };
        }
    }
}
