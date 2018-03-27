using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    /// <summary>
    /// 编码区
    /// </summary>
    public class CIICodeArea
    {
        /// <summary>
        /// 命令码
        /// </summary>
        private byte commandCode;
        public byte CommandCode
        {
            get { return this.commandCode; }
            set { this.commandCode = value; }
        }

        /// <summary>
        /// 附加码
        /// </summary>
        private byte additionalCode;
        public byte AdditionCode
        {
            get { return this.additionalCode; }
            set { this.additionalCode = value; }
        }

        public int Length
        {
            get { return BytesToInt2(DataLength, 0); }
        }

        /**  
       * byte数组中取int数值，本方法适用于(低位在后，高位在前)的顺序。和intToBytes2（）配套使用 
       */
        public static int BytesToInt2(byte[] src, int offset)
        {
            int value;
            value = (int)(((src[offset] & 0xFF) << 8)
                    | (src[offset + 1] & 0xFF));
            return value;
        }

        /// <summary>
        /// 数据长度
        /// </summary>
        private byte[] dataLength;
        public byte[] DataLength
        {
            get { return this.dataLength; }
            set { this.dataLength = value; }
        }

        /// <summary>
        /// 具体数据区内容
        /// </summary>
        private byte[] data;
        public byte[] Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        /// <summary>
        /// CRC16校验区
        /// </summary>
        private byte[] crc16Code;
        public byte[] CRC16Code
        {
            get { return this.crc16Code; }
            set { this.crc16Code = value; }
        }

        public CIICodeArea()
        {
            DataLength = new byte[2];
        }


    }
}
