using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.SysClass
{
    [Serializable]
    public class HolePulsePoint
    {
        private float x;
        public float X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        private float y;
        public float Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public HolePulsePoint()
        {

        }

        public HolePulsePoint(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
