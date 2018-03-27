using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class LaserBaseRequest
    {
        private byte type;
        public byte Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public LaserBaseRequest()
        {

        }

        public virtual List<LaserBasePackage> Encode()
        {
            return CreateOneList();
        }


        protected List<LaserBasePackage> CreateOneList()
        {
            List<LaserBasePackage> list = new List<LaserBasePackage>();
            return list;
        }
    }
}
