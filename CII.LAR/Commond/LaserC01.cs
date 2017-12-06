using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 查询模块状态信息和温度
    /// </summary>
    public class LaserC01Request : BaseRequest
    {
        public override BasePackage Encode()
        {
            BasePackage bp = base.Encode();
            bp.Type = 0x01;
            bp.AppData = new byte[] { 0x01, 0x00 };
            return bp;
        }
    }

    public class LaserC01Response : BaseResponse
    {
        /// <summary>
        /// 状态标志位
        /// </summary>
        private int flag;
        public int Flag
        {
            get { return this.flag; }
            private set { this.flag = value; }
        }

        /// <summary>
        /// 设备温度
        /// </summary>
        private double temperature;
        public double Temperature
        {
            get { return this.temperature; }
            private set { this.temperature = value; }
        }

        public override List<BaseResponse> Decode(BasePackage bp, OriginalBytes obytes)
        {
            base.Decode(bp, obytes);

            LaserC01Response c01Response = new LaserC01Response();
            c01Response.DtTime = DateTime.Now;
            c01Response.OriginalBytes = obytes;
            //aa*128 + bb
            c01Response.Flag = obytes.Data[1] * 128 + obytes.Data[2];
            //cc*128 + dd = T 设备温度 = (T / 4096 *2500) / 10 (℃)
            c01Response.Temperature = ((obytes.Data[3] * 128 + obytes.Data[4]) / 4096 * 2500) / 10;
            return CreateOneList(c01Response);
        }
    }
}
