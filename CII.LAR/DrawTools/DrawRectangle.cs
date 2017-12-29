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
    /// Rectangle graphic object
    /// Author:Zhong Wen 2017/07/26
    /// </summary>
    public class DrawRectangle : DrawObject
    {
        protected float dataLeft;
        protected float dataRight;
        protected float dataTop;
        protected float dataBottom;

        private Rectangle rectangle;
        public DrawRectangle()
        {
            this.RegisterUpdateStatisticsHandler();
        }

        public DrawRectangle(VideoControl videoControl, int x, int y, int width, int height) : this()
        {
            this.videoControl = videoControl;
            InitializeGraphicsProperties();
            this.ObjectType = ObjectType.Rectangle;
            rectangle = new Rectangle(x, y, width, height);
            SetRectangle(rectangle);
            //this.GraphicsProperties.GraphicsPropertiesChangedHandler += videoControl.GraphicsPropertiesChangedHandler;
        }

        private void InitializeGraphicsProperties()
        {
            this.GraphicsProperties = GraphicsPropertiesManager.GetPropertiesByName("Rectangle");
            this.GraphicsProperties.Color = Color.BlueViolet;
            this.GraphicsProperties.DrawObject = this;
            this.GraphicsProperties.Alpha = (this.GraphicsProperties.Alpha == 0xFF || this.GraphicsProperties.Alpha == 0) ? 0xFF
                : this.GraphicsProperties.Alpha;
        }

        public override string Prefix
        {
            get
            {
                return "R";
            }
        }

        /// <summary>
        /// draw graphic object
        /// </summary>
        /// <param name="g"></param>
        /// <param name="videoControl"></param>
        public override void Draw(Graphics g, VideoControl videoControl)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle r = GetNormalizedRectangle(GetRectangle());
            using (Pen pen = new Pen(Color.FromArgb(GraphicsProperties.Alpha, GraphicsProperties.Color), GraphicsProperties.PenWidth))
            {
                if (IsMoving)
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                    pen.DashPattern = new float[] { 4.0F, 2.8F };
                }

                rectangle.Offset(MovingOffset);
                g.DrawRectangle(pen, r);
            }
        }

        public override RectangleF GetTextF(string name, Graphics g, int index)
        {
            SizeF sizeF = g.MeasureString(name, this.Font);
            return new RectangleF(rectangle.X - sizeF.Width, rectangle.Y - sizeF.Height,
                sizeF.Width, sizeF.Height);
        }

        public static Rectangle GetNormalizedRectangle(Rectangle r)
        {
            return GetNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }

        /// <summary>
        /// Get normalized rectangle when move left or top
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static Rectangle GetNormalizedRectangle(int x1, int y1, int x2, int y2)
        {
            if (x2 < x1)
            {
                int tmp = x2;
                x2 = x1;
                x1 = tmp;
            }

            if (y2 < y1)
            {
                int tmp = y2;
                y2 = y1;
                y1 = tmp;
            }

            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(VideoControl videoControl, int handleNumber)
        {
            int x, y, xCenter, yCenter;
            Rectangle rectangle = GetRectangle();

            xCenter = rectangle.X + rectangle.Width / 2;
            yCenter = rectangle.Y + rectangle.Height / 2;
            x = rectangle.X;
            y = rectangle.Y;

            switch (handleNumber)
            {
                case 1:
                    x = rectangle.X;
                    y = rectangle.Y;
                    break;
                case 2:
                    x = xCenter;
                    y = rectangle.Y;
                    break;
                case 3:
                    x = rectangle.Right;
                    y = rectangle.Y;
                    break;
                case 4:
                    x = rectangle.Right;
                    y = yCenter;
                    break;
                case 5:
                    x = rectangle.Right;
                    y = rectangle.Bottom;
                    break;
                case 6:
                    x = xCenter;
                    y = rectangle.Bottom;
                    break;
                case 7:
                    x = rectangle.X;
                    y = rectangle.Bottom;
                    break;
                case 8:
                    x = rectangle.X;
                    y = yCenter;
                    break;
            }

            return new Point(x, y);

        }

        public override void Move(VideoControl videoControl, int deltaX, int deltaY)
        {
            Rectangle rect = GetRectangle();
            SetRectangle(new Rectangle(rect.X + deltaX, rect.Y + deltaY, rect.Width, rect.Height));
        }

        /// <summary>
        /// Mouse move to new point
        /// </summary>
        /// <param name="videoControl"></param>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void MoveHandleTo(VideoControl videoControl, Point point, int handleNumber)
        {
            int left = rectangle.Left;
            int top = rectangle.Top;
            int right = rectangle.Right;
            int bottom = rectangle.Bottom;
            switch (handleNumber)
            {
                case 1:
                    left = point.X;
                    top = point.Y;
                    break;
                case 2:
                    top = point.Y;
                    break;
                case 3:
                    right = point.X;
                    top = point.Y;
                    break;
                case 4:
                    right = point.X;
                    break;
                case 5:
                    right = point.X;
                    bottom = point.Y;
                    break;
                case 6:
                    bottom = point.Y;
                    break;
                case 7:
                    left = point.X;
                    bottom = point.Y;
                    break;
                case 8:
                    left = point.X;
                    break;
            }
            rectangle = new Rectangle(left, top, right - left, bottom - top);
            SetRectangle(rectangle);
            this.Statistics.Area = GetArea();
            this.Statistics.Circumference = GetCircumference();
            Console.WriteLine("area:" + this.Statistics.Area);
            Console.WriteLine("Circumference:" + this.Statistics.Circumference);
            Console.WriteLine("Height:" + rectangle.Height);
        }

        private string GetCircumference()
        {
            var length = 2 * Math.Abs(rectangle.Width + rectangle.Height) / UnitOfMeasureFactor;
            return string.Format("{0:F2} {1}", length, videoControl.UnitOfMeasure.ToString());
        }

        private string GetArea()
        {
            var area = (rectangle.Width / UnitOfMeasureFactor) * (rectangle.Height / UnitOfMeasureFactor);
            return string.Format("{0:F2} {1}²", Math.Abs(area), videoControl.UnitOfMeasure.ToString());
        }

        private void SetRectangle(Rectangle r)
        {
            PointF dataLeftTop = new PointF(r.Left, r.Top);
            PointF dataRightBottom = new PointF(r.Right, r.Bottom);
            dataLeft = dataLeftTop.X;
            dataTop = dataLeftTop.Y;
            dataRight = dataRightBottom.X;
            dataBottom = dataRightBottom.Y;
        }

        protected Rectangle GetRectangle()
        {
            return ToDrawRectangle(dataLeft, dataRight, dataTop, dataBottom);
        }

        private Rectangle ToDrawRectangle(float left, float right, float top, float bottom)
        {
            Point leftTop = Point.Ceiling(new PointF(left, top));
            Point rightBottom = Point.Ceiling(new PointF(right, bottom));

            return new Rectangle(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);
        }

        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                    return Cursors.SizeNWSE;
                case 2:
                    return Cursors.SizeNS;
                case 3:
                    return Cursors.SizeNESW;
                case 4:
                    return Cursors.SizeWE;
                case 5:
                    return Cursors.SizeNWSE;
                case 6:
                    return Cursors.SizeNS;
                case 7:
                    return Cursors.SizeNESW;
                case 8:
                    return Cursors.SizeWE;
                default:
                    return Cursors.Default;
            }
        }

        public override bool HitTest(int nIndex, PointF dataPoint)
        {
            return dataPoint.X >= dataLeft && dataPoint.X <= dataRight
               && dataPoint.Y >= dataBottom && dataPoint.Y <= dataTop;
        }

        public override HitTestResult HitTestForSelection(VideoControl videoControl, Point point)
        {
            Rectangle rectGate = GetRectangle();
            Rectangle rectLarge = rectGate, rectSamll = rectGate;
            rectLarge.Inflate(SelectionHitTestWidth, SelectionHitTestWidth);
            rectSamll.Inflate(-SelectionHitTestWidth, -SelectionHitTestWidth);

            if (!rectSamll.Contains(point) && rectLarge.Contains(point))
                return new HitTestResult(ElementType.Gate, 0);

            return new HitTestResult(ElementType.Nothing, -1);
        }

        /// <summary>
        /// Get number of handles
        /// </summary>
        public override int HandleCount
        {
            get
            {
                return 8;
            }
        }
    }
}
