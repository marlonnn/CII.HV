using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    
    /// <summary>
    /// 查询红光激光器电流
    /// </summary>
    public class LaserC04Request : LaserBaseRequest
    {
        public LaserC04Request()
        {
            this.Type = 0x04;
        }

        public override List<LaserBasePackage> Encode()
        {
            List<LaserBasePackage> bps = base.Encode();
            LaserBasePackage bp = new LaserBasePackage(0x8F, 0x04, new byte[] { 0x04, 0x00 });
            bps.Add(bp);
            return bps;
        }
    }

    public class LaserC04Response : LaserBaseResponse
    {
        /// <summary>
        /// 红光激光器电流数字量
        /// </summary>
        private double current;
        public double Current
        {
            get { return this.current; }
            private set { this.current = value; }
        }

        public LaserC04Response()
        {
            this.Type = 0x04;
        }

        public override LaserBaseResponse Decode(OriginalBytes obytes)
        {
            base.Decode(obytes);

            //cc*128 + dd = T 电流数字量 (data) T = (data / 4096) * 2500 (MA)
            this.Current = ((obytes.Data[3] * 128 + obytes.Data[4]) / 4096d) * 2500;
            return this;
        }

        public override string ToString()
        {
            string ret = "";
            if (this != null)
            {
                ret = PrintOriginalData() + "\n" + string.Format("红光激光数字电流： {0}mA", this.Current);
            }
            return ret;
        }
    }
}
