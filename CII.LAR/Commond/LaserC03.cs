using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    
    /// <summary>
    /// 查询1480激光温度
    /// </summary>
    public class LaserC03Request : BaseRequest
    {
        public override BasePackage Encode()
        {
            BasePackage bp = base.Encode();
            bp.Type = 0x03;
            bp.AppData = new byte[] { 0x03, 0x00 };
            return bp;
        }
    }

    public class LaserC03Response : BaseResponse
    {
        /// <summary>
        /// 温度数字量
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

            LaserC03Response c03Response = new LaserC03Response();
            c03Response.DtTime = DateTime.Now;
            c03Response.OriginalBytes = obytes;
            //aa*128 + bb = T 温度数字量 (data) T = data / 81.72 (℃)
            c03Response.Temperature = (obytes.Data[1] * 128 + obytes.Data[2]) / 81.72;
            return CreateOneList(c03Response);
        }
    }
}
