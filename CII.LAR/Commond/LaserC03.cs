﻿using CII.LAR.Protocol;
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
    public class LaserC03Request : LaserBaseRequest
    {
        public LaserC03Request()
        {
            this.Type = 0x03;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x03, new byte[] { 0x03, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    public class LaserC03Response : LaserBaseResponse
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

        public LaserC03Response()
        {
            this.Type = 0x03;
        }

        public override LaserBaseResponse Decode(OriginalBytes obytes)
        {
            base.Decode(obytes);
            this.DtTime = DateTime.Now;
            this.OriginalBytes = obytes;
            //aa*128 + bb = T 温度数字量 (data) T = data / 81.72 (℃)
            this.Temperature = (obytes.Data[1] * 128 + obytes.Data[2]) / 81.72;
            return this;
        }

        public override string ToString()
        {
            string ret = "";
            if (this != null)
            {
                ret = PrintOriginalData() + "\n" + string.Format("1480激光器温度： {0}", this.Temperature);
            }
            return ret;
        }
    }
}
