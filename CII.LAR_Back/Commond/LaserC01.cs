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
    public class LaserC01Request : LaserBaseRequest
    {
        public LaserC01Request()
        {
            this.Type = 0x01;
        }
        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x01, new byte[] { 0x01, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    public class LaserC01Response : LaserBaseResponse
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

        public LaserC01Response()
        {
            this.Type = 0x01;
        }

        public override List<LaserBaseResponse> Decode(LaserBasePackage bp, OriginalBytes obytes)
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
