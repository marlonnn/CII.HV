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

        public virtual List<MotorBaseResponse> Decode(CIIBasePackage bp, OriginalBytes obytes)
        {
            //if (obytes.Data.Length != 6)
            //{
            //    LogHelper.GetLogger<LaserBaseResponse>().Error(string.Format("消息类型为 : {0} 长度不足！", obytes.Data[1]));
            //    return null;
            //}
            return null;
        }

        protected List<MotorBaseResponse> CreateOneList(MotorBaseResponse br)
        {
            List<MotorBaseResponse> list = new List<MotorBaseResponse>();
            list.Add(br);
            return list;
        }
    }
}
