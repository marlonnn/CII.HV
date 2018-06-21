using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.DrawTools
{
    using PointFList = List<PointF>;
    using PointFEnumerator = IEnumerator<PointF>;
    using UI;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    /// <summary>
    /// Polygon graphic object
    /// Author:Zhong Wen 2017/07/26
    /// </summary>
    public class DrawPolygon : DrawLine
    {
        protected static System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(MainForm));

        protected static Cursor handleCursor = new Cursor(new System.IO.MemoryStream((byte[])resourceManager.GetObject("PolyHandle")));
        protected PointFList pointArray;
        private PointFList pointArrayProportion;

        protected Rectangle drawRectangle = Rectangle.Empty;
        private Size drawAreaSize; // store draw area size for data point hit test

        public int PointCount
        {
            get
            {
                return pointArray.Count;
            }
        }

        public DrawPolygon()
        {
            pointArray = new PointFList();
            pointArrayProportion = new PointFList();
            drawAreaSize = DefaultDrawAreaSize;
        }

        public DrawPolygon(RichPictureBox richPictureBox, List<PointF> dataPoints) : this()
        {
            pointArray = new PointFList(dataPoints);
            pointArrayProportion = new PointFList();

        }

        public DrawPolygon(RichPictureBox richPictureBox, int x1, int y1, int x2, int y2) : this()
        {
            this.richPictureBox = richPictureBox;
            pointArray = new PointFList();
            pointArrayProportion = new PointFList();

            AddPoint(richPictureBox, new Point(x1, y1), false);
            AddPoint(richPictureBox, new Point(x2, y2), false);
        }

        public override void Move(RichPictureBox richPictureBox, int deltaX, int deltaY)
        {
            int n = pointArray.Count;
            Point point;

            for (int i = 0; i < n; i++)
            {
                point = Point.Ceiling(pointArray[i]);
                point = new Point(point.X + deltaX, point.Y + deltaY);

                pointArray[i] = point;
            }
        }

        /// <summary>
        /// draw object
        /// </summary>
        /// <param name="g"></param>
        /// <param name="richPictureBox"></param>
        public override void Draw(Graphics g, RichPictureBox richPictureBox)
        {
            Point p1 = Point.Empty; // previous point
            Point p2 = Point.Empty; // current point

            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(GraphicsProperties.Color, GraphicsProperties.PenWidth))
            {
                PointFEnumerator enumerator = pointArray.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    p1 = Point.Ceiling(enumerator.Current);
                    p1.Offset(MovingOffset);
                }
                while (enumerator.MoveNext())
                {
                    p2 = Point.Ceiling(enumerator.Current);
                    p2.Offset(MovingOffset);
                    g.DrawLine(pen, p1, p2);
                    p1 = p2;
                }
                enumerator.Reset();
                if (enumerator.MoveNext())
                {
                    p2 = Point.Ceiling(enumerator.Current);
                    p2.Offset(MovingOffset);
                    g.DrawLine(pen, p1, p2);
                }
            }
        }

        public void AddPoint(RichPictureBox richPictureBox, Point point, bool checkClose)
        {
            bool addPoint = true;
            if (checkClose)
            {
                for (int i = 0; i < PointCount - 1; i++)   // does not check for last point
                {
                    Point des = Point.Ceiling(pointArray[i]);
                    if (CloseToPoint(point, des))
                    {
                        addPoint = false;
                    }
                }
            }
            if (addPoint)
            {
                pointArray.Add(point);
            }
        }

        public bool CloseToPoint(Point src, Point des)
        {
            return Math.Abs(src.X - des.X) <= 3 && Math.Abs(src.Y - des.Y) <= 3;
        }

        public override void DrawTracker(Graphics g, RichPictureBox richPictureBox)
        {
            if (!Selected)
            {
                return;
            }

            SolidBrush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Text").Color,
                Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Text").PenWidth);

            for (int i = 1; i <= HandleCount; i++)
            {
                Rectangle r = GetHandleRectangle(richPictureBox, i);
                if (i <= PointCount)
                {
                    r.Offset(MovingOffset);
                }
                else
                {
                    brush.Color = Color.DarkGray;
                }

                try
                {
                    g.DrawRectangle(pen, r);
                    g.FillRectangle(brush, r);
                }
                catch (System.Exception ex)
                {
                    Selected = false;
                }
            }
            pen.Dispose();
            brush.Dispose();
        }

        /// <summary>
        /// Invalidate object.
        /// When object is invalidated, path used for hit test
        /// is released and should be created again.
        /// </summary>
        protected void Invalidate()
        {
            if (AreaPath != null)
            {
                AreaPath.Dispose();
                AreaPath = null;
            }

            //if (AreaPen != null)
            //{
            //    AreaPen.Dispose();
            //    AreaPen = null;
            //}

            if (AreaRegion != null)
            {
                AreaRegion.Dispose();
                AreaRegion = null;
            }
        }

        public override void UpdateHitTestRegions()
        {
            // call this since we use draw points to do gate hit test
            CreateObjects();
        }

        /// <summary>
        /// Create graphic object used for hit test
        /// </summary>
        protected void CreateObjects()
        {
            Invalidate();   // invalidate every time, since draw area may resize
            if (AreaPath != null)
                return;

            // Create closed path which contains all polygon vertexes
            AreaPath = new GraphicsPath();

            PointF p1 = PointF.Empty;     // previous point
            PointF p2 = PointF.Empty;     // current point

            PointFEnumerator enumerator = pointArray.GetEnumerator();

            if (enumerator.MoveNext())
            {
                p1 = enumerator.Current;
            }

            while (enumerator.MoveNext())
            {
                p2 = enumerator.Current;

                AreaPath.AddLine(p1, p2);

                p1 = p2;
            }

            AreaPath.CloseFigure();

            // Create region from the path
            AreaRegion = new Region(AreaPath);
        }

        /// <summary>
        /// Hit test if dataPoint is in gate, only used for user operation like mouse operation
        /// </summary>
        /// <param name="nIndex">gate index</param>
        /// <param name="dataPoint"></param>
        /// <returns></returns>
        public override bool HitTest(int nIndex, PointF dataPoint)
        {
            try
            {
                return nIndex == 0 && AreaRegion != null && AreaRegion.IsVisible(dataPoint);
            }
            catch (System.Exception)
            {

            }
            return false;
        }

        public override HitTestResult HitTestForSelection(RichPictureBox richPictureBox, Point point0)
        {
            //transfer point according to const draw area size for hit test
            Point point = new Point(point0.X * richPictureBox.Width, point0.Y * richPictureBox.Height);
            GraphicsPath pathOut = AreaPath.Clone() as GraphicsPath;
            Pen pen = new Pen(Color.Black, SelectionHitTestWidth * 2);
            pathOut.Widen(pen);
            Region rOut = new Region(pathOut);
            bool result = false;
            try
            {
                result = rOut.IsVisible(point);
            }
            catch (System.Exception ex)
            {

            }
            pathOut.Dispose();
            pen.Dispose();
            rOut.Dispose();

            return result ? new HitTestResult(ElementType.Gate, 0) : new HitTestResult(ElementType.Nothing, -1);
        }

        public void MoveLastHandleTo(RichPictureBox richPictureBox, Point point)
        {
            if (PointCount == 0) return;

            if (PointCount > 3 && CloseToFirstPoint(richPictureBox, point))
            {
                pointArray[PointCount - 1] = pointArray[0];
            }
            else
            {
                pointArray[PointCount - 1] = point;
            }
            this.Statistics.Circumference = GetCircumference();
            this.Statistics.Area = "Unknown";
            //Console.WriteLine("Circumference:" + GetCircumference());
            UpdateStatisticsInformation();
        }

        private string GetCircumference()
        {
            Point p1 = Point.Empty; // previous point
            Point p2 = Point.Empty; // current point
            double sum = 0;
            PointFEnumerator enumerator = pointArray.GetEnumerator();
            if (enumerator.MoveNext())
            {
                p1 = Point.Ceiling(enumerator.Current);
                p1.Offset(MovingOffset);
            }
            while (enumerator.MoveNext())
            {
                p2 = Point.Ceiling(enumerator.Current);
                p2.Offset(MovingOffset);
                float x = System.Math.Abs(p2.X - p1.X);
                float y = System.Math.Abs(p2.Y - p1.Y);
                sum += Math.Sqrt(x * x + y * y) / UnitOfMeasureFactor;
                p1 = p2;
            }
            enumerator.Reset();
            if (enumerator.MoveNext())
            {
                p2 = Point.Ceiling(enumerator.Current);
                p2.Offset(MovingOffset);
            }
            return string.Format("{0:F2} {1}", sum, richPictureBox.UnitOfMeasure.ToString());
        }

        private double GetArea()
        {
            return 0;
        }

        public bool CloseToFirstPoint(RichPictureBox richPictureBox, Point point)
        {
            if (PointCount <= 0) return false;

            Point first = Point.Ceiling(pointArray[0]);

            return CloseToPoint(first, point);
        }

        /// <summary>
        /// Get handle rectangle by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Rectangle GetHandleRectangle(RichPictureBox richPictureBox, int handleNumber)
        {
            Point point = GetHandle(richPictureBox, handleNumber);

            return new Rectangle(point.X - 3, point.Y - 3, 6, 6);
        }

        public override Cursor GetHandleCursor(int handleNumber)
        {
            if (handleNumber <= PointCount)
            {
                return handleCursor;
            }
            else
            {
                handleNumber -= PointCount;
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
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(RichPictureBox richPictureBox, int handleNumber)
        {
            if (handleNumber < 1)
            {
                handleNumber = 1;
            }
            if (handleNumber <= PointCount)
            {
                return Point.Ceiling(pointArray[handleNumber - 1]);
            }
            else
            {
                int x, y, xCenter, yCenter;
                Rectangle rectangle = drawRectangle;

                if (rectangle == Rectangle.Empty)
                {
                    return Point.Empty;
                }

                xCenter = rectangle.X + rectangle.Width / 2;
                yCenter = rectangle.Y + rectangle.Height / 2;
                x = rectangle.X;
                y = rectangle.Y;

                handleNumber -= PointCount;
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
        }

        /// <summary>
        /// get handle point count
        /// </summary>
        public override int HandleCount
        {
            get
            {
                return pointArray.Count + 8;
            }
        }
    }
}
