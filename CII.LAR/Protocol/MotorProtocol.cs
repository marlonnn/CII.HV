using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class MotorProtocol
    {
        /// <summary>
        /// 接受数据， 编码区数据
        /// </summary>
        public byte[] CodeRegion;

        public MotorProtocol DePackage(byte[] data)
        {
            MotorProtocol mp = new MotorProtocol();
            if (data.Length < 8)
            {
                LogHelper.GetLogger<MotorProtocol>().Error("通信层待编码数据为空，丢弃。");
                return null;
            }
            mp.CodeRegion = new byte[data.Length - 8];
            Array.Copy(data, 6, mp.CodeRegion, 0, mp.CodeRegion.Length);
            return mp;
        }

        public byte[] EnPackage(CIIBasePackage bp)
        {
            if (bp.AppData == null)
            {
                LogHelper.GetLogger<MotorProtocol>().Error("通信层待编码数据为空，丢弃。");
                return null;
            }
            byte[] enData = new byte[bp.AppData.Length + 10];
            Array.Copy(bp.MarkHead, 0, enData, 0, 2);
            enData[2] = bp.DestLength;
            enData[3] = bp.DestRegion;
            enData[4] = bp.SourceLength;
            enData[5] = bp.SourceRegion;
            Array.Copy(bp.AppData, 0, enData, 6, bp.AppData.Length);
            byte[] temp = new byte[enData.Length - 6];
            Array.Copy(enData, 2, temp, 0, temp.Length);
            byte[] crc16 = BitConverter.GetBytes(CRC16.Compute(temp));
            Array.Copy(crc16, 0, enData, enData.Length - 4, 2);
            Array.Copy(bp.MarkTail, 0, enData, enData.Length - 2, 2);
            return enData;
        }
    }
}
