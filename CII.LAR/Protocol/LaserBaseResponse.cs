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

        public virtual List<LaserBaseResponse> Decode(LaserBasePackage bp, OriginalBytes obytes)
        {
            if (obytes.Data.Length != 6)
            {
                LogHelper.GetLogger<LaserBaseResponse>().Error(string.Format("消息类型为 : {0} 长度不足！", obytes.Data[1]));
                return null;
            }

            byte oddCheck = obytes.Data[1];
            for (int i = 2; i < obytes.Data.Length - 2; i++)
            {
                oddCheck ^= obytes.Data[i];
            }
            if (oddCheck != obytes.Data[obytes.Data.Length - 2])
            {
                LogHelper.GetLogger<LaserBaseResponse>().Error(string.Format("消息类型为 : {0} 的奇偶校验位错误！", obytes.Data[1]));
                return null;
            }
            else
            {
                return Decode(bp, obytes);
            }
        }

        protected List<LaserBaseResponse> CreateOneList(LaserBaseResponse br)
        {
            List<LaserBaseResponse> list = new List<LaserBaseResponse>();
            list.Add(br);
            return list;
        }

        protected bool CheckResponse(byte[] data)
        {
            return true;
            //if (data[0] == 0x80 && data[1] == 0xFF && data[2] == 0x00 && data[3] == 0x00 && data[4] == 0x00 && data[5] == 0xFF)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}
