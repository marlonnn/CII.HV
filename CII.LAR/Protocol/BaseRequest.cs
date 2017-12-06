using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class BaseRequest
    {
        public virtual BasePackage Encode()
        {
            BasePackage bp = new BasePackage();
            return bp;
        }
    }
}
