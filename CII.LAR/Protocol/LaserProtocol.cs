using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class LaserProtocol
    {
        /// <summary>
        /// 接受数据包头
        /// </summary>
        private byte deMarkHead;

        /// <summary>
        /// 发送数据包头
        /// </summary>
        private byte enMarkHead;

        public byte[] Body;

        public LaserProtocol()
        {
            deMarkHead = 0x80;
            enMarkHead = 0x8F;
        }

        public LaserProtocol DePackage(byte[] data)
        {
            LaserProtocol lp = new LaserProtocol();
            if (data == null)
            {
                //LogHelper.GetLogger<LaserProtocol>().Error("通信层接收到数据包为空或者数据长度不足，丢弃。");
                return null;
            }
            if (data[0] != deMarkHead)
            {
                //LogHelper.GetLogger<LaserProtocol>().Error("通信层接收到数据包不是本应用需要接受的数据包，丢弃。");
                return null;
            }
            lp.Body = new byte[data.Length - 1];
            Array.Copy(data, 1, lp.Body, 0, data.Length - 1);
            return lp;
        }

        public byte[] EnPackage(LaserBasePackage bp)
        {
            if (bp.AppData == null)
            {
                //LogHelper.GetLogger<LaserProtocol>().Error("通信层待编码数据为空，丢弃。");
                return null;
            }
            byte[] enData = new byte[bp.AppData.Length + 2];
            enData[0] = bp.MarkHead;
            Array.Copy(bp.AppData, 0, enData, 1, bp.AppData.Length);
            byte oddCheck = 0x00;
            for (int i=1; i<enData.Length - 2; i ++)
            {
                oddCheck ^= enData[i];
            }
            enData[enData.Length - 1] = oddCheck;
            return enData;
        }
    }
}
