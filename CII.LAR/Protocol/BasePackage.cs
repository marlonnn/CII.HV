using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class BasePackage
    {
        public byte Type;

        /// <summary>
        /// 除去帧头和最后一位奇偶校验位
        /// </summary>
        public byte[] AppData;
    }
}
