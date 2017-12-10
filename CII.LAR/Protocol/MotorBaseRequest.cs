using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class MotorBaseRequest
    {
        public MotorBaseRequest()
        {

        }

        public virtual List<CIIBasePackage> Encode()
        {
            return CreateOneList();
        }


        protected List<CIIBasePackage> CreateOneList()
        {
            List<CIIBasePackage> list = new List<CIIBasePackage>();
            return list;
        }
    }
}
