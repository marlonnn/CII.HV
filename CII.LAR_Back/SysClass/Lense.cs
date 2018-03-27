using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.SysClass
{
    /// <summary>
    /// Object lense
    /// Author: Zhong Wen 2017/09/23
    /// </summary>
    [Serializable]
    public class Lense
    {
        private int factor;
        public int Factor
        {
            get { return factor; }
            set { factor = value; }
        }
        public Lense(int factor)
        {
            this.factor = factor;
        }

        public override string ToString()
        {
            return string.Format("x{0}", factor);
        }
    }
}
