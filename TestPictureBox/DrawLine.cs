using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPictureBox
{
    /// <summary>
    /// Line graphic object
    /// Author:Zhong Wen 2017/07/26
    /// </summary>
    public class DrawLine 
    {
        protected PointF startDataPoint;
        protected PointF endDataPoint;

        private PictureBox pictureBox;

        public string Prefix
        {
            get
            {
                return "L";
            }
        }

        public DrawLine(PictureBox pictureBox, int x1, int y1, int x2, int y2)
        {
            this.pictureBox = pictureBox;
            startDataPoint = new Point(x1, y1);
            endDataPoint = new Point(x2, y2);
        }

        /// <summary>
        /// draw line graphic
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pictureBox"></param>
        public void Draw(Graphics g, PictureBox pictureBox)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.Yellow, 2f))
            {
                g.DrawLine(pen, startDataPoint.X, startDataPoint.Y, endDataPoint.X, endDataPoint.Y);
            }
        }


        /// <summary>
        /// Mouse move to new point
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public void MoveHandleTo(Point point, int handleNumber)
        {
            if (handleNumber == 1)
            {
                startDataPoint = point;
            }
            else
            {
                endDataPoint = point;
            }
        }
    }
}
