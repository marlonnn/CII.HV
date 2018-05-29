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
        public struct Vector2D
        {
            public double X;
            public double Y;
            public Vector2D(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }
        }
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

        public DrawLine(RichPictureBox richPictureBox, int x1, int y1, int x2, int y2) : this()
        {
            this.richPictureBox = richPictureBox;
            startDataPoint = new Point(x1, y1);
            endDataPoint = new Point(x2, y2);
            this.GraphicsProperties.GraphicsPropertiesChangedHandler += richPictureBox.GraphicsPropertiesChangedHandler;
        }

        private void InitializeGraphicsProperties()
        {
            this.GraphicsProperties = Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Line");
            this.GraphicsProperties.DrawObject = this;
            //this.GraphicsProperties.Color = Color.Yellow;
            this.GraphicsProperties.Alpha = (this.GraphicsProperties.Alpha == 0xFF || this.GraphicsProperties.Alpha == 0) ? 0xFF
                : this.GraphicsProperties.Alpha;
        }

        public override void DrawText(Graphics g, RichPictureBox richPictureBox)
        {
            SolidBrush brush = new SolidBrush(Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Text").Color);
            Font font = new Font("Microsoft Sans Serif", GraphicsProperties.TextSize);
            RectangleF r = GetTextF(this.Name, g, this.ID);
            r.Offset(MovingOffset);

            g.DrawString(this.Name, font, brush, r);
            brush.Dispose();
            font.Dispose();
        }

        /// <summary>
        /// draw line graphic
        /// </summary>
        /// <param name="g"></param>
        /// <param name="richPictureBox"></param>
        public override void Draw(Graphics g, RichPictureBox richPictureBox)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(GraphicsProperties.Alpha, GraphicsProperties.Color), GraphicsProperties.PenWidth))
            {
                if (IsMoving)
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                    pen.DashPattern = new float[] { 4.0F, 2.8F };
                }
                g.DrawLine(pen, startDataPoint.X + MovingOffset.X, startDataPoint.Y + MovingOffset.Y, 
                    endDataPoint.X + MovingOffset.X, endDataPoint.Y + MovingOffset.Y);
            }
        }

        public override RectangleF GetTextF(string name, Graphics g, int index)
        {
            Font font = new Font("Microsoft Sans Serif", GraphicsProperties.TextSize);
            SizeF sizeF = g.MeasureString(name, font);
            font.Dispose();
            return new RectangleF(startDataPoint.X - sizeF.Width /*- richPictureBox.StartOffsetX*/, startDataPoint.Y - sizeF.Height / 2,
                sizeF.Width, sizeF.Height);
        }

        public override int HandleCount
        {
            get
            {
                return 2;
            }
        }

        public override void Normalize()
        {
            endDataPoint.Y = startDataPoint.Y;

            if (startDataPoint.X > endDataPoint.X)  // make sure start point is left than end point
            {
                float temp = startDataPoint.X;
                startDataPoint.X = endDataPoint.X;
                endDataPoint.X = temp;
            }
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            //switch (handleNumber)
            //{
            //    case 1:
            //    case 2:
            //        {
            //            return Cursors.SizeAll;
            //        }
            //    default:
            //        {
            //            return Cursors.Default;
            //        }
            //}
            return Cursors.SizeWE;
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(RichPictureBox richPictureBox, int handleNumber)
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

        public override void Move(RichPictureBox richPictureBox, int deltaX, int deltaY)
        {
            Point s = Point.Ceiling(startDataPoint), e = Point.Ceiling(endDataPoint);

            startDataPoint = new Point(s.X + deltaX, s.Y + deltaY);
            endDataPoint = new Point(e.X + deltaX, e.Y + deltaY);
        }

        /// <summary>
        /// Mouse move to new point
        /// </summary>
        /// <param name="richPictureBox"></param>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void MoveHandleTo(RichPictureBox richPictureBox, Point point, int handleNumber)
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
            UpdateStatisticsInformation();
            //Console.WriteLine(this.Statistics.Circumference);
        }

        private string GetCircumference(PointF startPoint, PointF endPoint)
        {
            double x = PixelToMillimeter(System.Math.Abs(endPoint.X - startPoint.X));
            double y = PixelToMillimeter(System.Math.Abs(endPoint.Y - startPoint.Y));
            return string.Format("{0:F2} {1}", Math.Sqrt(x * x + y * y) / UnitOfMeasureFactor, richPictureBox.UnitOfMeasure.ToString());
        }

        public override bool HitTest(int nIndex, PointF dataPoint)
        {
            return nIndex == 0 && dataPoint.X >= startDataPoint.X && dataPoint.X <= endDataPoint.X;
        }

        public override HitTestResult HitTestForSelection(RichPictureBox richPictureBox, Point point)
        {
            return HitTestLine(startDataPoint, endDataPoint, point, 10) ? new HitTestResult(ElementType.Gate, 0) : new HitTestResult(ElementType.Nothing, -1);
        }

        #region MSDN Hit Test Lines and Curves 详情见：(https://msdn.microsoft.com/en-us/library/ms969920.aspx)
        /// <summary>
        /// 判断鼠标点是否在直线上
        /// </summary>
        /// <param name="pt0">起点</param>
        /// <param name="pt1">终点</param>
        /// <param name="ptMouse">鼠标垫</param>
        /// <param name="nWidth">直线宽度</param>
        /// <returns></returns>
        private bool HitTestLine(PointF pt0, PointF pt1, Point ptMouse, int nWidth)
        {
            Vector2D tt0, tt1;
            double dist;
            int nHalfWidth;
            //
            //Get the half width of the line to adjust for hit testing of wide lines.
            //
            nHalfWidth = (nWidth / 2 < 1) ? 1 : nWidth / 2;
            //
            //Convert the line into a vector using the two endpoints.
            //
            tt0 = Point2Vector2D(pt0, pt1);
            //
            //Convert the line from the left endpoint to the mouse point into a vector.
            //
            tt1 = Point2Vector2D(pt0, ptMouse);
            //
            //Obtain the distance of the point from the line.
            //
            dist = GetLengthOfNormal(tt1, tt0);
            //
            //Return TRUE if the distance of the point from the line is within the width 
            //of the line
            //
            return (dist >= -nHalfWidth && dist <= nHalfWidth);
        }

        /// <summary>
        /// 两点转换为向量
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        private Vector2D Point2Vector2D(PointF p0, PointF p1)
        {
            return new Vector2D(p1.X - p0.X, p1.Y - p0.Y);
        }

        /// <summary>
        /// 获取垂直向量分量长度
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private double GetLengthOfNormal(Vector2D a, Vector2D b)
        {
            Vector2D c, vNormal;
            //
            //Obtain projection vector.
            //
            //c = ((a * b)/(|b|^2))*b
            //
            c.X = b.X * (DotProduct(a, b) / DotProduct(b, b));
            c.Y = b.Y * (DotProduct(a, b) / DotProduct(b, b));
            //
            //Obtain perpendicular projection : e = a - c
            //
            vNormal = SubtractVetors(a, c);
            return VectorMagnitude(vNormal);
        }

        /// <summary>
        /// 向量点乘
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <returns></returns>
        private double DotProduct(Vector2D v0, Vector2D v1)
        {
            double dotProd = 0.0d;
            dotProd = v0.X * v1.X + v0.Y * v1.Y;
            return dotProd;
        }

        /// <summary>
        /// 向量分解
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <returns></returns>
        private Vector2D SubtractVetors(Vector2D v0, Vector2D v1)
        {
            return new Vector2D(v0.X - v1.X, v0.Y - v1.Y);
        }

        private double VectorMagnitude(Vector2D v0)
        {
            return Math.Sqrt(VectorSquared(v0));
        }

        private double VectorSquared(Vector2D v0)
        {
            return v0.X * v0.X + v0.Y * v0.Y;
        } 
        #endregion
    }
}
