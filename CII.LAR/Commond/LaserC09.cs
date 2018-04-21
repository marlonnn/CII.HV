using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    
    /// <summary>
    /// 查询红光激光器电流上限
    /// </summary>
    public class LaserC09Request : LaserBaseRequest
    {
        public LaserC09Request()
        {
            this.Type = 0x09;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x09, new byte[] { 0x09, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    /// <summary>
    /// 返回红光激光器电流设置值
    /// </summary>
    public class LaserC09Response : LaserBaseResponse
    {
        /// <summary>
        /// 红光激光器电流设定值
        /// </summary>
        private double current;
        public double Current
        {
            get { return this.current; }
            private set { this.current = value; }
        }

        public LaserC09Response()
        {
            this.Type = 0x09;
        }
        
        public override LaserBaseResponse Decode(OriginalBytes obytes)
        {
            base.Decode(obytes);

            //cc*128 + dd = T 红光激光器电流设定值数字量 (data) T = (data / 4096) * 2500 (MA)
            this.Current = (obytes.Data[3] * 128 + obytes.Data[4]) * 100 / Program.SysConfig.LaserConfig.COF;
            Program.SysConfig.LaserConfig.RedCurrent = this.Current;
            return this;
        }

        public override string ToString()
        {
            string ret = "";
            if (this != null)
            {
                ret = PrintOriginalData() + "\n" + string.Format("红光电流： {0}mA", this.Current);
            }
            return ret;
        }
    }
}
