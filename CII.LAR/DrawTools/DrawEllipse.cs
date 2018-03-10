using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CII.LAR.UI;
using System.Drawing.Drawing2D;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Ellipse graphic object
    /// Author:Zhong Wen 2017/07/26
    /// </summary>
    public class DrawEllipse : DrawObject
    {
        [NonSerialized]
        protected static System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(EntryForm));
        [NonSerialized]
        protected static Cursor handleCursor = new Cursor(new System.IO.MemoryStream((byte[])resourceManager.GetObject("PolyHandle")));

        private PointF startPoint;  // point at -a (in pixel)
        private PointF endPoint;    // point at a (in pixel)
        private double coeffcient;  // height / width

        private GraphicsPath areaPath = new GraphicsPath();
        private Region areaRegion = new Region();

        /// <summary>
        /// ellipse only for draw, need to be reset when ellipse for hit test changed. 
        /// May be no same as ellipse for hit test when drawArea.
        /// Size is not equal to default draw area size for hit test
        /// </summary>
        private Ellipse ellipseForDraw = null;

        /// <summary>
        /// ellipse for hit test
        /// </summary>
        private Ellipse EllipseForHit
        {
            get
            {
                return new Ellipse(startPoint, endPoint, coeffcient, drawAreaSize);
            }
        }

        private Matrix orgMatrix;

        public Matrix OrgMatrix
        {
            get { return orgMatrix ?? new Matrix(); }
            set { orgMatrix = value; }
        }

        private Matrix DrawMatrix
        {
            get
            {
                Matrix matrix = OrgMatrix.Clone();
                matrix.Translate(ellipseForDraw.Center.X, ellipseForDraw.Center.Y);
                matrix.Rotate(ellipseForDraw.Angle);

                return matrix;
            }
        }

        private Matrix HitTestMatrix
        {
            get
            {
                Ellipse ellipse = EllipseForHit;
                Matrix matrix = new Matrix();
                matrix.Translate(ellipse.Center.X, ellipse.Center.Y);
                matrix.Rotate(ellipse.Angle);
                return matrix;
            }
        }

        private Size drawAreaSize = DefaultDrawAreaSize; // store draw area size for data point hit test

        public DrawEllipse()
        {
            InitializeGraphicsProperties();
            this.RegisterUpdateStatisticsHandler();
        }

        private void InitializeGraphicsProperties()
        {
            this.GraphicsProperties = Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Ellipse");
            //this.GraphicsProperties.Color = Color.Orange;
            this.GraphicsProperties.DrawObject = this;
            this.GraphicsProperties.Alpha = (this.GraphicsProperties.Alpha == 0xFF || this.GraphicsProperties.Alpha == 0) ? 0xFF
                : this.GraphicsProperties.Alpha;
        }

        public DrawEllipse(VideoControl videoControl, int x1, int y1, int x2, int y2, double c) : this()
        {
            this.videoControl = videoControl;
            this.ObjectType = ObjectType.Ellipse;

            startPoint = new PointF(x1, y1);
            endPoint = new PointF(x2, y2);
            coeffcient = c;

            drawAreaSize = videoControl.Size;
            if (drawAreaSize != DefaultDrawAreaSize)
            {
                TransformLinear(DefaultDrawAreaSize.Width * 1.0 / drawAreaSize.Width, DefaultDrawAreaSize.Height * 1.0 / drawAreaSize.Height, 0, 0);
                drawAreaSize = DefaultDrawAreaSize;
            }
            this.GraphicsProperties.GraphicsPropertiesChangedHandler += videoControl.GraphicsPropertiesChangedHandler;

        }

        public void TransformLinear(double k, double h, double m, double n)
        {
            Ellipse.TransformLinear(ref startPoint, ref endPoint, ref coeffcient, k, h, m, n);
            ResetEllipseForDraw();
        }

        private void ResetEllipseForDraw()
        {
            ellipseForDraw = null;
        }

        public override string Prefix
        {
            get
            {
                return "E";
            }
        }

        /// <summary>
        /// draw ellipse graphic
        /// </summary>
        /// <param name="g"></param>
        /// <param name="videoControl"></param>
        public override void Draw(Graphics g, VideoControl videoControl)
        {
            if (ellipseForDraw == null)
            {
                var s = new PointF(startPoint.X * videoControl.Zoom, startPoint.Y * videoControl.Zoom);
                var e = new PointF(endPoint.X * videoControl.Zoom, endPoint.Y * videoControl.Zoom);
                var size = new Size((int)(drawAreaSize.Width * videoControl.Zoom), (int)(drawAreaSize.Height * videoControl.Zoom));
                ellipseForDraw = new Ellipse(s, e, coeffcient, size);
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(GraphicsProperties.Alpha, GraphicsProperties.Color), GraphicsProperties.PenWidth))
            {
                try
                {
                    OrgMatrix = g.Transform;
                    g.Transform = DrawMatrix;

                    g.TranslateTransform(MovingOffset.X, MovingOffset.Y, MatrixOrder.Append);

                    g.DrawEllipse(pen, ellipseForDraw.Rectangle);

                    g.Transform = OrgMatrix;
                }
                catch
                {
                    g.Transform = OrgMatrix;
                }
            }
        }

        public override RectangleF GetTextF(string name, Graphics g, int index)
        {
            SizeF sizeF = g.MeasureString(name, this.Font);
            return new RectangleF(startPoint.X - sizeF.Width, startPoint.Y - sizeF.Height,
                sizeF.Width, sizeF.Height);
        }

        public override void Move(VideoControl videoControl, int deltaX, int deltaY)
        {
            PointF ps = ellipseForDraw.StartPoint;
            PointF pe = ellipseForDraw.EndPoint;

            ellipseForDraw.StartPoint = new PointF(ps.X + deltaX, ps.Y + deltaY);
            ellipseForDraw.EndPoint = new PointF(pe.X + deltaX, pe.Y + deltaY);
        }

        /// <summary>
        /// Mouse move to new point
        /// </summary>
        /// <param name="videoControl"></param>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void MoveHandleTo(VideoControl videoControl, Point point, int handleNumber)
        {
            if (ellipseForDraw == null)
            {
                return;
            }
            if (handleNumber == 2 || handleNumber == 4)
            {
                handleNumber = handleNumber == 2 ? 4 : 2;
                double lenth = Ellipse.GetTwoPointLength(ellipseForDraw.Center, point) * 2;

                double coef = lenth / ellipseForDraw.Width;
                if (coef < 0.1) coef = 0.1;
                if (coef > 0.99) coef = 1;
                ellipseForDraw.Coeffcient = coef;

                UpdateEllipseForHit();

                return;
            }

            if (handleNumber == 1)
            {
                ellipseForDraw.StartPoint = point;
            }
            else
            {
                ellipseForDraw.EndPoint = point;
            }

            UpdateEllipseForHit();
            this.Statistics.Area = GetArea();
            this.Statistics.Circumference = GetCircumference();
            Console.WriteLine("area:" + GetArea());
            Console.WriteLine("Circumference:" + GetCircumference());
            Console.WriteLine("Height:" + ellipseForDraw.Rectangle.Height);
            Console.WriteLine("Width:" + ellipseForDraw.Rectangle.Width);
        }

        /// <summary>
        /// L=2πb+4(a-b)
        /// 半轴长（a）与短半轴长（b）
        /// </summary>
        /// <returns></returns>
        private string GetCircumference()
        {
            var length = (2 * Math.PI * (ellipseForDraw.Rectangle.Height / 2) + 
                4 * (ellipseForDraw.Rectangle.Width / 2 - ellipseForDraw.Rectangle.Height / 2)) / UnitOfMeasureFactor;
            return string.Format("{0:F2} {1}", length, videoControl.UnitOfMeasure.ToString());
        }

        /// <summary>
        /// S=πab
        /// 半轴长（a）与短半轴长（b）
        /// </summary>
        /// <returns></returns>
        private string GetArea()
        {
            var area = Math.PI * (ellipseForDraw.Rectangle.Width / (2 * UnitOfMeasureFactor)) * (ellipseForDraw.Rectangle.Height / (2 * UnitOfMeasureFactor));
            return string.Format("{0:F2} {1}²", area, videoControl.UnitOfMeasure.ToString());
        }

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(VideoControl videoControl, int handleNumber)
        {
            float x = 0, y = 0, xCenter, yCenter;

            RectangleF rect = ellipseForDraw.Rectangle;
            xCenter = rect.X + rect.Width / 2;
            yCenter = rect.Y + rect.Height / 2;

            switch (handleNumber)
            {
                case 1:
                    x = rect.X;
                    y = yCenter;
                    break;
                case 2:
                    x = xCenter;
                    y = rect.Y;
                    break;
                case 3:
                    x = rect.Right;
                    y = yCenter;
                    break;
                case 4:
                    x = xCenter;
                    y = rect.Bottom;
                    break;
            }

            PointF[] pts = new PointF[1];
            pts[0] = new PointF(x, y);

            DrawMatrix.TransformPoints(pts);

            return Point.Round(pts[0]);
        }

        /// <summary>
        /// Get number of handles
        /// </summary>
        public override int HandleCount
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Create graphic object used for hit test
        /// </summary>
        protected void CreateObjects()
        {
            if (areaPath != null)
            {
                areaPath.Dispose();
                areaPath = null;
            }

            if (areaRegion != null)
            {
                areaRegion.Dispose();
                areaRegion = null;
            }

            areaPath = new GraphicsPath();
            areaPath.AddEllipse(EllipseForHit.Rectangle);
            areaRegion = new Region(areaPath);
            areaRegion.Transform(HitTestMatrix);

        }

        /// <summary>
        /// update ellipse for hit
        /// </summary>
        private void UpdateEllipseForHit()
        {
            startPoint = ellipseForDraw.StartPoint;
            endPoint = ellipseForDraw.EndPoint;
            coeffcient = ellipseForDraw.Coeffcient;
            if (ellipseForDraw.DrawAreaSize != drawAreaSize && !ellipseForDraw.DrawAreaSize.IsEmpty)
            {
                Ellipse.TransformLinear(ref startPoint, ref endPoint, ref coeffcient, drawAreaSize.Width * 1.0 / ellipseForDraw.DrawAreaSize.Width, drawAreaSize.Height * 1.0 / ellipseForDraw.DrawAreaSize.Height, 0, 0);
            }
        }

        public override void UpdateHitTestRegions()
        {
            CreateObjects();
        }

        /// <summary>
        /// Hit test if dataPoint is in gate, only used for user operation like mouse operation
        /// </summary>
        /// <param name="nIndex">gate index</param>
        /// <param name="dataPoint"></param>
        /// <returns></returns>
        public override bool HitTest(int nIndex, PointF dataPoint)
        {
            return areaRegion.IsVisible(dataPoint);
        }

        public override HitTestResult HitTestForSelection(VideoControl videoControl, Point point0)
        {
            //transfer point according to const draw area size for hit test
            Point point = new Point(point0.X * drawAreaSize.Width / videoControl.Width, point0.Y * drawAreaSize.Height / videoControl.Height);

            GraphicsPath pathOut = areaPath.Clone() as GraphicsPath;
            Pen pen = new Pen(Color.Black, SelectionHitTestWidth * 2);
            try { pathOut.Widen(pen, HitTestMatrix); }
            catch (Exception) { }
            Region rOut = new Region(pathOut);

            bool result = rOut.IsVisible(point);
            pathOut.Dispose();
            pen.Dispose();
            rOut.Dispose();

            return result ? new HitTestResult(ElementType.Gate, 0) : new HitTestResult(ElementType.Nothing, -1);
        }

        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Cursor GetHandleCursor(int handleNumber)
        {
            return handleCursor;
        }

        /// <summary>
        /// inner ellipse class 
        /// </summary>
        public class Ellipse
        {
            public PointF StartPoint
            {
                get;
                set;
            }
            public PointF EndPoint
            {
                get;
                set;
            }
            public double Coeffcient
            {
                get;
                set;
            }
            public Size DrawAreaSize
            {
                get;
                set;
            }

            public Ellipse(PointF start, PointF end, double coef, Size drawAreaSize)
            {
                StartPoint = start;
                EndPoint = end;
                Coeffcient = coef;
                DrawAreaSize = drawAreaSize;
            }

            public PointF Center   // center point of ellipse
            {
                get { return new PointF((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2); }
            }

            public double Height      // length between b and -b (in pixel)
            {
                get { return Width * Coeffcient; }
            }

            public double Width
            {
                get { return GetTwoPointLength(StartPoint, EndPoint); }
            }

            public RectangleF Rectangle
            {
                get { return new RectangleF((float)-Width / 2, (float)-Height / 2, (float)Width, (float)Height); }
            }

            public float Angle
            {
                get
                {
                    return (float)(System.Math.Atan2(EndPoint.Y - StartPoint.Y, EndPoint.X - StartPoint.X) * (180 / System.Math.PI));
                }
            }

            #region Helper Functions

            public static double GetTwoPointLength(PointF p1, PointF p2)
            {
                return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
            }

            #endregion

            /// <summary>
            /// linear transform ellipse to new draw area size
            /// </summary>
            /// <param name="newDrawAreaSize"></param>
            public void TransformLinear(Size newDrawAreaSize)
            {
                if (newDrawAreaSize.IsEmpty) return;

                double k = newDrawAreaSize.Width * 1.0 / DrawAreaSize.Width;
                double h = newDrawAreaSize.Height * 1.0 / DrawAreaSize.Height;
                double m = 0;
                double n = 0;
                PointF start = StartPoint;
                PointF end = EndPoint;
                double coef = Coeffcient;
                TransformLinear(ref start, ref end, ref coef, k, h, m, n);
                StartPoint = start;
                EndPoint = end;
                Coeffcient = coef;
                DrawAreaSize = newDrawAreaSize;
            }

            /// <summary>
            /// linear transformations for ellipse, refer to page 1 of "Linear Transform of Ellipse.docx"
            /// </summary>
            /// <param name="start">point at -a</param>
            /// <param name="end">point at a</param>
            /// <param name="coef">height / width</param>
            /// <param name="k"></param>
            /// <param name="h"></param>
            /// <param name="m"></param>
            /// <param name="n"></param>
            public static void TransformLinear(ref PointF start, ref PointF end, ref double coef, double k, double h, double m, double n)
            {
                if (start == end) return;

                PointF oldStart = start;
                PointF oldEnd = end;
                double oldCoef = coef;

                double width = GetTwoPointLength(start, end);
                double height = width * coef;
                PointF center = new PointF((start.X + end.X) / 2, (start.Y + end.Y) / 2);

                double len = GetTwoPointLength(start, end);
                double cos = (end.X - start.X) / len;
                double sin = (end.Y - start.Y) / len;
                double a = width / 2, b = height / 2;
                PointF c = center;

                // calculate A, B, C of original ellipse
                double A = Math.Pow(cos / a, 2) + Math.Pow(sin / b, 2);
                double B = 2 * cos * sin * (1 / Math.Pow(a, 2) - 1 / Math.Pow(b, 2));
                double C = Math.Pow(sin / a, 2) + Math.Pow(cos / b, 2);

                // scale
                A = A / k / k;
                B = B / k / h;
                C = C / h / h;
                double b2 = -(A + C), c2 = A * C - B * B / 4;
                double temp = Math.Max(0, b2 * b2 - 4 * c2);    // precision issue may bring a negative number
                double r = (-b2 + Math.Sqrt(temp)) / 2;
                double t = (-b2 - Math.Sqrt(temp)) / 2;

                // calculate new ellipse's parameter
                b = Math.Sqrt(1 / r);
                a = Math.Sqrt(1 / t);
                double tempRatio = Math.Max(0, (A - r) / (t - r));
                cos = t == r ? 1 : Math.Sqrt(tempRatio) * (sin * cos > 0 ? 1 : -1);
                double tempSubcos = Math.Max(0, 1 - Math.Pow(cos, 2));
                sin = Math.Sqrt(tempSubcos);

                coef = b / a;

                if (Math.Abs(coef - 1) < 1e-6)
                {
                    start.X = (float)(oldStart.X * k + m);
                    start.Y = (float)(oldStart.Y * h + n);
                    end.X = (float)(oldEnd.X * k + m);
                    end.Y = (float)(oldEnd.Y * h + n);
                }
                else
                {
                    // translate
                    start.X = (float)(-a * cos + c.X * k + m);
                    start.Y = (float)(-a * sin + c.Y * h + n);
                    end.X = (float)(a * cos + c.X * k + m);
                    end.Y = (float)(a * sin + c.Y * h + n);

                    //remain start end order
                    if ((start.X > end.X && oldStart.X < oldEnd.X) || (start.X < end.X && oldStart.X > oldEnd.X))
                    {
                        PointF tempPoint = start;
                        start = end;
                        end = tempPoint;
                    }
                }

            }
        }

    }
}
