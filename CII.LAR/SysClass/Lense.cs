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
        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        private double factor;
        public double Factor
        {
            get { return factor; }
            set { factor = value; }
        }

        private float fineAdjustment;
        public float FineAdjustment
        {
            get { return this.fineAdjustment; }
            set { this.fineAdjustment = value; }
        }
        public Lense(double factor)
        {
            this.factor = factor;
            this.name = string.Format("x{0}", factor);
            this.fineAdjustment = 100;
        }

        public override string ToString()
        {
            return string.Format("x{0}", factor);
        }
    }
}
