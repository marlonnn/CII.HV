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
        public struct SEGMENT
        {

            #region "Members"
            public int X0;
            public int Y0;
            public int X1;
            public int Y1;
            public System.Drawing.Point P0
            {
                get { return new System.Drawing.Point(X0, Y0); }
                set
                {
                    X0 = value.X;
                    Y0 = value.Y;
                }
            }
            public System.Drawing.Point P1
            {
                get { return new System.Drawing.Point(X1, Y1); }
                set
                {
                    X1 = value.X;
                    Y1 = value.Y;
                }
            }
            #endregion

            #region "Constructors"
            public SEGMENT(SEGMENT aSEGMENT)
            {
                X0 = Y0 = X1 = Y1 = 0;
                try
                {
                    this.X0 = aSEGMENT.X0;
                    this.Y0 = aSEGMENT.Y0;
                    this.X1 = aSEGMENT.X1;
                    this.Y1 = aSEGMENT.Y1;
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger<SEGMENT>().Error(ex.Message);
                    LogHelper.GetLogger<SEGMENT>().Error(ex.StackTrace);
                }
            }
            public SEGMENT(int X0, int Y0, int X1, int Y1)
            {
                this.X0 = this.Y0 = this.X1 = this.Y1 = 0;
                try
                {
                    this.X0 = X0;
                    this.Y0 = Y0;
                    this.X1 = X1;
                    this.Y1 = Y1;
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger<SEGMENT>().Error(ex.Message);
                    LogHelper.GetLogger<SEGMENT>().Error(ex.StackTrace);
                }
            }
            public SEGMENT(Point P0, Point P1)
            {
                X0 = Y0 = X1 = Y1 = 0;
                try
                {
                    this.X0 = P0.X;
                    this.Y0 = P0.Y;
                    this.X1 = P1.X;
                    this.Y1 = P1.Y;
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger<SEGMENT>().Error(ex.Message);
                    LogHelper.GetLogger<SEGMENT>().Error(ex.StackTrace);
                }
            }
            #endregion

            #region "Member functions"
            public bool ContainsX(int XQuote)
            {
                //(Valido se P1 a sinistra di P0)
                if ((XQuote >= P0.X) && (XQuote <= P1.X))
                {
                    return true;
                }
                else
                {
                    //(Valido se P0 a sinistra di P1)
                    if ((XQuote >= P1.X) && (XQuote <= P0.X))
                    {
                        return true;
                    }
                }
                return false;
            }
            public System.Drawing.Point MediumPoint()
            {
                try
                {
                    System.Drawing.Point retVal = default(System.Drawing.Point);
                    retVal.X = (this.X0 + this.X1) / 2;
                    retVal.Y = (this.Y0 + this.Y1) / 2;
                    return retVal;
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger<SEGMENT>().Error(ex.Message);
                    LogHelper.GetLogger<SEGMENT>().Error(ex.StackTrace);
                    return Point.Empty;
                }
            }
            public static double SegmentModule(System.Drawing.Point P0, System.Drawing.Point P1)
            {
                try
                {
                    return System.Math.Sqrt(System.Math.Pow(P1.X - P0.X, 2) + System.Math.Pow(P1.Y - P0.Y, 2));
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger<SEGMENT>().Error(ex.Message);
                    LogHelper.GetLogger<SEGMENT>().Error(ex.StackTrace);
                    return 0;
                }
            }
            public double SegmentModule()
            {
                try
                {
                    return System.Math.Sqrt(System.Math.Pow(X1 - X0, 2) + System.Math.Pow(Y1 - Y0, 2));
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger<SEGMENT>().Error(ex.Message);
                    LogHelper.GetLogger<SEGMENT>().Error(ex.StackTrace);
                    return 0;
                }
            }
            /// <summary>
            /// Ritorna la direzione (angolo in radianti) del segmento ...
            /// </summary>
            /// <returns></returns>
            /// <remarks></remarks>
            public double SegmentDirection()
            {
                try
                {
                    double dblHyp = 0;
                    double dblSin = 0;
                    double RefX = 0;
                    double RefY = 0;
                    //Traslo il segmento in modo che parta dall'origine ...
                    RefX = X1 - X0;
                    RefY = -(Y1 - Y0);
                    //Memo: in Windows l'asse Y � invertito ...
                    //Riporto a sistema di coordinate standard per
                    //applicare formule trigonometriche ...

                    if ((RefY == 0))
                    {
                        //Segmento orizzontale ...
                        if ((RefX > 0))
                        {
                            //Angolo nullo ...
                            return 0;
                        }
                        else
                        {
                            //Angolo piatto ...
                            return System.Math.PI;
                        }
                    }

                    if ((RefX == 0))
                    {
                        //Segmento verticale ...
                        if ((RefY > 0))
                        {
                            return System.Math.PI / 2;
                        }
                        else
                        {
                            return -System.Math.PI / 2;
                        }
                    }

                    //Se sono arrivato fino a qui, l'angolo non � un multiplo di Pi/2 ...
                    if ((RefX > 0))
                    {
                        if ((RefY > 0))
                        {
                            //Primo quadrante ....
                            dblHyp = System.Math.Sqrt((RefX * RefX + RefY * RefY));
                            //Ipotenusa ...
                            dblSin = RefY / dblHyp;
                            return System.Math.Atan(dblSin / System.Math.Sqrt(-dblSin * dblSin + 1));
                        }
                        else
                        {
                            //Quarto quadrante ...
                            RefY = -RefY;
                            dblHyp = System.Math.Sqrt((RefX * RefX + RefY * RefY));
                            //Ipotenusa ...
                            dblSin = RefY / dblHyp;
                            return (2 * System.Math.PI) - System.Math.Atan(dblSin / System.Math.Sqrt(-dblSin * dblSin + 1));
                        }
                    }
                    else
                    {
                        if ((RefY > 0))
                        {
                            //Secondo quadrante ...
                            RefX = -RefX;
                            dblHyp = System.Math.Sqrt((RefX * RefX + RefY * RefY));
                            //Ipotenusa ...
                            dblSin = Convert.ToDouble(RefY) / dblHyp;
                            return -System.Math.Atan(dblSin / System.Math.Sqrt(-dblSin * dblSin + 1)) + System.Math.PI;
                        }
                        else
                        {
                            //Terzo quadrante ...
                            RefX = -RefX;
                            RefY = -RefY;
                            dblHyp = System.Math.Sqrt((RefX * RefX + RefY * RefY));
                            //Ipotenusa ...
                            dblSin = RefY / dblHyp;
                            return System.Math.Atan(dblSin / System.Math.Sqrt(-dblSin * dblSin + 1)) + System.Math.PI;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.GetLogger<SEGMENT>().Error(ex.Message);
                    LogHelper.GetLogger<SEGMENT>().Error(ex.StackTrace);
                    return 0;
                }
            }
            #endregion

            #region "Operators"

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;

                if (GetType() != obj.GetType())
                    return false;
                SEGMENT g = (SEGMENT)obj;
                if (this.GetHashCode() == g.GetHashCode())
                    return true;
                else
                    return false;
            }

            public static bool operator ==(SEGMENT S1, SEGMENT S2)
            {
                if ((S1.X0 == S2.X0) && (S1.X1 == S2.X1) && (S1.Y0 == S2.Y0) && (S1.Y1 == S2.Y1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static bool operator !=(SEGMENT S1, SEGMENT S2)
            {
                if ((S1.X0 != S2.X0) | (S1.X1 != S2.X1) | (S1.Y0 != S2.Y0) | (S1.Y1 != S2.Y1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            #endregion
        }

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

        private double CvRadToDeg(double RadAngle)
        {
            //espresso in gradi, 0-360
            return RadAngle * (180 / (System.Math.Atan(1) * 4));
        }
        private float _length;
        private float LineLength(Point p1, Point p2, float ScaleFactor = 1)
        {
            Rectangle r = NormalizeRect(p1, p2);
            _length = (float)(Math.Sqrt(Math.Pow(r.Width, 2) + Math.Pow(r.Height, 2)) / ScaleFactor);
            return _length;
        }
        private Rectangle NormalizeRect(Point p1, Point p2)
        {
            Rectangle r = new Rectangle();
            if (p1.X < p2.X)
            {
                r.X = p1.X;
                r.Width = p2.X - p1.X;
            }
            else
            {
                r.X = p2.X;
                r.Width = p1.X - p2.X;
            }
            if (p1.Y < p2.Y)
            {
                r.Y = p1.Y;
                r.Height = p2.Y - p1.Y;
            }
            else
            {
                r.Y = p2.Y;
                r.Height = p1.Y - p2.Y;
            }
            return r;
        }

        public static string strCutDecimals(double Value, int DesiredDecDigits)
        {
            // ERROR: Not supported in C#: OnErrorStatement

            return Convert.ToString(CutDecimals(Value, DesiredDecDigits));
        }
        public static double CutDecimals(double Value, int DesiredDecDigits)
        {
            double functionReturnValue = 0;
            try
            {
                if ((Value == double.NegativeInfinity || Value == double.PositiveInfinity))
                {
                    return Value;
                }
                if (DesiredDecDigits > 5)
                {
                    DesiredDecDigits = 5;
                }
                functionReturnValue = Convert.ToInt32(Value * Math.Pow(10, DesiredDecDigits)) / (Math.Pow(10, DesiredDecDigits));
            }
            catch (Exception ex)
            {
                functionReturnValue = Value;
            }
            return functionReturnValue;
        }

        private float Angle(Point p1, Point p2)
        {
            float _angle = (float)(Math.Atan((p1.Y - p2.Y) / (p1.X - p2.X)) * (180 / Math.PI));
            return _angle;
        }
        /// <summary>
        /// draw line graphic
        /// </summary>
        /// <param name="g"></param>
        /// <param name="richPictureBox"></param>
        public override void Draw(Graphics g, RichPictureBox richPictureBox)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            double CurrentAngle = new SEGMENT((int)startDataPoint.X, (int)startDataPoint.Y, (int)endDataPoint.X, (int)endDataPoint.Y).SegmentDirection();
            CurrentAngle = CvRadToDeg(CurrentAngle);
            if (CurrentAngle > 180)
            {
                CurrentAngle = CurrentAngle - 360;
            }
            using (Pen pen = new Pen(Color.FromArgb(GraphicsProperties.Alpha, GraphicsProperties.Color), GraphicsProperties.PenWidth))
            {
                if (IsMoving)
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                    pen.DashPattern = new float[] { 4.0F, 2.8F };
                }
                g.DrawLine(pen, startDataPoint.X + MovingOffset.X, startDataPoint.Y + MovingOffset.Y, 
                    endDataPoint.X + MovingOffset.X, endDataPoint.Y + MovingOffset.Y);
                int OriginCrossArmLength = 20;
                Point midPoint = new Point();
                PointF origin = new PointF(startDataPoint.X + MovingOffset.X, startDataPoint.Y + MovingOffset.Y);
                PointF last = new PointF(endDataPoint.X + MovingOffset.X, endDataPoint.Y + MovingOffset.Y);
                midPoint.X = Math.Min((int)origin.X, (int)last.X) + ((Math.Max((int)origin.X, (int)last.X) - Math.Min((int)origin.X, (int)endDataPoint.X)) / 2);
                midPoint.Y = Math.Min((int)origin.Y, (int)last.Y) + ((Math.Max((int)origin.Y, (int)last.Y) - Math.Min((int)origin.Y, (int)endDataPoint.Y)) / 2);
                g.DrawLine(pen, origin.X - OriginCrossArmLength, origin.Y, origin.X + OriginCrossArmLength, origin.Y);
                g.DrawLine(pen, origin.X, origin.Y - OriginCrossArmLength, origin.X, origin.Y + OriginCrossArmLength);
                g.DrawArc(pen, origin.X - OriginCrossArmLength, origin.Y - OriginCrossArmLength, 2 * OriginCrossArmLength, 2 * OriginCrossArmLength, 0, Convert.ToInt32(-CurrentAngle));
                using (System.Drawing.Drawing2D.Matrix mx = new System.Drawing.Drawing2D.Matrix())
                using (StringFormat sf = new StringFormat())
                {
                    string lineLength = (LineLength(Point.Ceiling(origin), Point.Ceiling(last))).ToString("F2");
                    string ls = strCutDecimals(CurrentAngle, 1);
                    SizeF l = g.MeasureString(ls, this.richPictureBox.Font, richPictureBox.ClientSize, sf);
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    //mx.Translate(midPoint.X, midPoint.Y);
                    //mx.Rotate(Angle(Point.Ceiling(origin), Point.Ceiling(last)));
                    //g.Transform = mx;
                    //Rectangle rt = new Rectangle(0, 0, (int)l.Width, (int)l.Height);
                    //rt.Inflate(3, 3);
                    //rt.Offset(-(int)(l.Width / 2), -(int)(l.Height / 2));
                    //using (SolidBrush backBrush = new SolidBrush(Color.FromArgb(GraphicsProperties.Alpha, GraphicsProperties.Color)))
                    //{
                    //    g.FillEllipse(backBrush, rt);
                    //}
                    using (SolidBrush brush = new SolidBrush(Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Text").Color))
                    using (Font font = new Font("Microsoft Sans Serif", GraphicsProperties.TextSize))
                    {
                        g.DrawString(ls + "°", font, brush, midPoint.X - 30, midPoint.Y, sf);
                    }
                }
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
            double x = richPictureBox.PixelToMicroscope(System.Math.Abs(endPoint.X - startPoint.X));
            double y = richPictureBox.PixelToMicroscope(System.Math.Abs(endPoint.Y - startPoint.Y));
            return string.Format("{0:F2} {1}", Math.Sqrt(x * x + y * y), richPictureBox.UnitOfMeasure.ToString());
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
