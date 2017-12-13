using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Laser
{
    /// <summary>
    /// Circle radius and center point
    /// </summary>
    public class CircleData
    {
        private double radius;
        public double Radius
        {
            get { return this.radius; }
            set { this.radius = value; }
        }
        private PointF centerPt;
        public PointF CenterPt
        {
            get { return this.centerPt; }
            set { this.centerPt = value; }
        }

        private double angleArc;
        public double AngleArc
        {
            get { return this.angleArc; }
            set { this.angleArc = value; }
        }

        private double lengthArc;
        public double LengthArc
        {
            get { return this.lengthArc; }
            set { this.lengthArc = value; }
        }

        public CircleData()
        {
            radius = 0;
            centerPt = new PointF();
        }
    }
}
