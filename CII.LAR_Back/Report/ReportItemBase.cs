using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.ExpClass
{
    public class ReportItemBase : IComparable<ReportItemBase>
    {
        private Rectangle bounds;

        public Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }
            set
            {
                this.bounds = value;
            }
        }

        private bool resize;

        public bool Resize
        {
            get
            {
                return this.resize;
            }
            set
            {
                this.resize = value;
            }
        }

        private int level;

        public int Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }

        public int CompareTo(ReportItemBase other)
        {
            return Level.CompareTo(other.Level);
        }

        public virtual void UpdateContent()
        {

        }
    }
}
