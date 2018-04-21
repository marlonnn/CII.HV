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

        public override LaserBaseResponse Decode(OriginalBytes obytes)
        {
            base.Decode(obytes);

            this.DtTime = DateTime.Now;
            this.OriginalBytes = obytes;
            //aa*128 + bb
            this.Flag = obytes.Data[1] * 128 + obytes.Data[2];
            //cc*128 + dd = T 设备温度 = (T / 4096 *2500) / 10 (℃)
            this.Temperature = (((obytes.Data[3] * 128 + obytes.Data[4]) / 4096d) * 2500) / 10;
            return this;
        }

        public override string ToString()
        {
            string ret = "";
            if (this != null)
            {
                ret = PrintOriginalData() + "\n" + string.Format("状态位： {0}， 温度： {1}", this.Flag, this.Temperature);
            }
            return ret;
        }
    }
}
