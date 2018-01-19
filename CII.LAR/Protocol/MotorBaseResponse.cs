using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class MotorBaseResponse
    {
        /// <summary>
        /// 消息收到的时间
        /// </summary>
        public DateTime DtTime;

        /// <summary>
        /// 原始数据
        /// </summary>
        public OriginalBytes OriginalBytes;

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
        /// 具体数据内容
        /// </summary>
        private byte[] data;
        public byte[] Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        private CIIBasePackage basePackage;
        public CIIBasePackage BasePackage
        {
            get { return this.basePackage; }
            set { this.basePackage = value; }
        }

        protected CIICodeArea codeArea;
        public CIICodeArea CodeArea
        {
            get { return this.codeArea; }
            set { this.codeArea = value; }
        }

        /// <summary>
        /// 写回应码
        /// </summary>
        private byte responseCode;
        public byte ResponseCode
        {
            get { return this.responseCode; }
            set { this.responseCode = value; }
        }

        public string GetResponseCode()
        {
            string code = "";
            switch (responseCode)
            {
                case 0x00:
                    code = "系统正常工作中";
                    break;
                case 0x11:
                    code = "系统设定中";
                    break;
                case 0x22:
                    code = "系统标定中";
                    break;
                case 0x33:
                    code = "系统初始化中";
                    break;
                case 0x44:
                    code = "系统自检中";
                    break;
                case 0x55:
                    code = "系统故障中";
                    break;
                case 0x66:
                    code = "系统调零中";
                    break;
                case 0x77:
                    code = "系统预标定中";
                    break;
                case 0x88:
                    code = "写命令成功";
                    break;
                case 0x99:
                    code = "网络忙(写命令失败)";
                    break;
                case 0xAA:
                    code = "写入数据非法或者超限";
                    break;
            }
            return code;
        }

        public MotorBaseResponse()
        {
            CodeArea = new CIICodeArea();
            CodeArea.CRC16Code = new byte[2];
        }

        public virtual MotorBaseResponse Decode(OriginalBytes obytes)
        {
            if (obytes.Data.Length < 10)
            {
                LogHelper.GetLogger<LaserBaseResponse>().Error(string.Format("消息类型为 : {0} 长度不足！", obytes.Data[1]));
                return null;
            }
            return null;
        }
    }
}
