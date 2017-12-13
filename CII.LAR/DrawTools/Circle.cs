using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Circle shape
    /// Author:Zhong Wen 2017/10/10
    /// </summary>
    public class Circle
    {
        public PointF CenterPoint
        {
            get;
            set;
        }

        public SizeF DrawAreaSize
        {
            get;
            set;
        }

        public RectangleF Rectangle
        {
            get
            {
                return new RectangleF(CenterPoint.X - DrawAreaSize.Width / 2f, CenterPoint.Y - DrawAreaSize.Width / 2f, DrawAreaSize.Width, DrawAreaSize.Height);
            }
        }

        public Circle(PointF centerPoint, SizeF drawAreaSize)
        {
            CenterPoint = centerPoint;
            DrawAreaSize = drawAreaSize;
        }
    }
}
