using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class BaseRequest
    {
        private byte type;
        public byte Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public BaseRequest()
        {

        }

        public virtual List<BasePackage> Encode()
        {
            return CreateOneList();
        }


        protected List<BasePackage> CreateOneList()
        {
            List<BasePackage> list = new List<BasePackage>();
            return list;
        }
    }
}
