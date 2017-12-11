using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class MotorBaseRequest
    {
        protected CIICodeArea codeArea;
        public CIICodeArea CodeArea
        {
            get { return this.codeArea; }
            set { this.codeArea = value; }
        }


        public MotorBaseRequest()
        {
            CodeArea = new CIICodeArea();
        }

        public virtual CIIBasePackage Encode()
        {
            return null;
        }

        protected void CopyDataLenght()
        {
            byte[] length = ByteHelper.IntToBytes2(this.CodeArea.Data.Length, 2);
            Array.Copy(length, 0, this.CodeArea.DataLength, 0, 2);
        }
    }
}
