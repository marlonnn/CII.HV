using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Line graphic object
    /// Author:Zhong Wen 2017/07/26
    /// </summary>
    public class DrawLine : DrawObject
    {
        protected PointF startDataPoint;
        protected PointF endDataPoint;
        private GraphicsPath areaPath = null;
        protected GraphicsPath AreaPath
        {
            get
            {
                return areaPath;
            }
            set
            {
                areaPath = value;
            }
        }
        protected Region areaRegion = null;
        protected Region AreaRegion
        {
            get
            {
                return areaRegion;
            }
            set
            {
                areaRegion = value;
            }
        }

        public override string Prefix
        {
            get
            {
                return "L";
            }
        }

        public DrawLine()
        {
            InitializeGraphicsProperties();
            this.ObjectType = ObjectType.Line;
            this.Statistics.Area = "null";
            this.RegisterUpdateStatisticsHandler();
        }

        public DrawLine(ZWPictureBox pictureBox, int x1, int y1, int x2, int y2) : this()
        {
            this.pictureBox = pictureBox;
            startDataPoint = new Point(x1, y1);
            endDataPoint = new Point(x2, y2);
            //this.GraphicsProperties.GraphicsPropertiesChangedHandler += pictureBox.GraphicsPropertiesChangedHandler;
        }

        private void InitializeGraphicsProperties()
        {
            this.GraphicsProperties = GraphicsPropertiesManager.GetPropertiesByName("Line");
            this.GraphicsProperties.DrawObject = this;
            this.GraphicsProperties.Color = Color.Yellow;
            this.GraphicsProperties.Alpha = (this.GraphicsProperties.Alpha == 0xFF || this.GraphicsProperties.Alpha == 0) ? 0xFF
                : this.GraphicsProperties.Alpha;
        }

        /// <summary>
        /// draw line graphic
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pictureBox"></param>
        public override void Draw(Graphics g, ZWPictureBox pictureBox)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(GraphicsProperties.Alpha, GraphicsProperties.Color), GraphicsProperties.PenWidth))
            {
                g.DrawLine(pen, startDataPoint.X, startDataPoint.Y, endDataPoint.X, endDataPoint.Y);
            }
        }

        public override RectangleF GetTextF(string name, Graphics g, int index)
        {
            SizeF sizeF = g.MeasureString(name, this.Font);
            return new RectangleF(startDataPoint.X - sizeF.Width /*- pictureBox.StartOffsetX*/, startDataPoint.Y - sizeF.Height / 2,
                sizeF.Width, sizeF.Height);
        }

        public override int HandleCount
        {
            get
            {
                return 2;
            }
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                case 2:
                    {
                        return Cursors.SizeAll;
                    }
                default:
                    {
                        return Cursors.Default;
                    }
            }
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(ZWPictureBox pictureBox, int handleNumber)
        {
            if (handleNumber == 1)
            {
                return Point.Ceiling(startDataPoint);
            }
            else
            {
                return Point.Ceiling(endDataPoint);
            }
        }

        public override void Move(ZWPictureBox pictureBox, int deltaX, int deltaY)
        {
            Point s = Point.Ceiling(startDataPoint), e = Point.Ceiling(endDataPoint);

            startDataPoint = new Point(s.X + deltaX, s.Y + deltaY);
            endDataPoint = new Point(e.X + deltaX, e.Y + deltaY);
        }

        /// <summary>
        /// Mouse move to new point
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void MoveHandleTo(ZWPictureBox pictureBox, Point point, int handleNumber)
        {
            if (handleNumber == 1)
            {
                startDataPoint = point;
            }
            else
            {
                endDataPoint = point;
            }
            this.Statistics.Circumference = GetCircumference(startDataPoint, endDataPoint);
            //Console.WriteLine(this.Statistics.Circumference);
        }

        private string GetCircumference(PointF startPoint, PointF endPoint)
        {
            float x = System.Math.Abs(endPoint.X - startPoint.X);
            float y = System.Math.Abs(endPoint.Y - startPoint.Y);
            return string.Format("{0:F2} {1}", Math.Sqrt(x * x + y * y) / UnitOfMeasureFactor, pictureBox.UnitOfMeasure.ToString());
        }

        public override bool HitTest(int nIndex, PointF dataPoint)
        {
            return false;
        }

        public override HitTestResult HitTestForSelection(ZWPictureBox pictureBox, Point point)
        {
            Rectangle rect = new Rectangle(Point.Ceiling(startDataPoint), new Size((int)(endDataPoint.X - startDataPoint.X), 1));
            rect.Inflate(0, this.SelectionHitTestWidth);
            return rect.Contains(point) ? new HitTestResult(ElementType.Gate, 0) : new HitTestResult(ElementType.Nothing, -1);
        }

    }
}
