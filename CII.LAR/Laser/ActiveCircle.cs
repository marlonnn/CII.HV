using CII.LAR.DrawTools;
using CII.LAR.SysClass;
using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.Laser
{
    public enum InHoleType
    {
        StartHole,
        CenterHole,
        EndHole,
        Empty
    }

    public class ActiveCircle
    {
        private InHoleType holeType;
        public InHoleType HoleType
        {
            get { return this.holeType; }
            set
            {
                if (value != this.holeType)
                {
                    this.holeType = value;
                }
            }
        }

        public void UpdateHoleNum(int holes)
        {
            this.holesInfo.UpdateHoleNum(holes);
            if (shape == LaserShape.Line)
            {
                CalculateContinuousCircle();
            }
            else if (shape == LaserShape.Arc)
            {
                CalcAngle();
            }
        }
        public enum LaserShape
        {
            Line,
            Arc
        }
        private LaserShape shape;

        private HolesInfo holesInfo;
        public HolesInfo HolesInfo
        {
            get { return holesInfo; }
            set { this.holesInfo = value; }
        }

        private CircleData circleData;
        /// <summary>
        /// GraphicsPropertiesManager: include all the draw object graphics properties
        /// </summary>
        private GraphicsPropertiesManager graphicsPropertiesManager = GraphicsPropertiesManager.GraphicsManagerSingleInstance();
        public GraphicsPropertiesManager GraphicsPropertiesManager
        {
            get
            {
                return graphicsPropertiesManager;
            }
            set
            {
                graphicsPropertiesManager = value;
            }
        }

        /// <summary>
        /// GraphicsProperties of this draw object 
        /// </summary>
        private GraphicsProperties graphicsProperties;
        public GraphicsProperties GraphicsProperties
        {
            get
            {
                return graphicsProperties;
            }
            set
            {
                graphicsProperties = value;
            }
        }

        private Point startPoint;

        public void MoveStartPoint(Point p)
        {
            if (!p.IsEmpty)
            {
                this.startPoint = p;
                StartCircle = new Circle(p, InnerCircleSize);
            }
        }

        public void MoveEndPoint(Point p)
        {
            if (!p.IsEmpty)
            {
                this.endPoint = p;
                EndCircle = new Circle(p, InnerCircleSize);
            }
        }

        public Point StartPoint
        {
            get { return startPoint; }
            set
            {
                if (value != Point.Empty && clickCount % 2 == 1)
                {
                    startPoint = value;
                    StartCircle = new Circle(value, InnerCircleSize);
                }
            }
        }

        private Point endPoint;
        public Point EndPoint
        {
            get { return this.endPoint; }
            set
            {
                if (value != Point.Empty && clickCount % 2 != 0)
                {
                    endPoint = value;
                    EndCircle = new Circle(value, InnerCircleSize);
                }
            }
        }

        private Circle startCircle;

        public Circle StartCircle
        {
            get { return this.startCircle; }
            set { this.startCircle = value; }
        }

        private Circle endCircle;

        public Circle EndCircle
        {
            get { return this.endCircle; }
            set { this.endCircle = value; }
        }

        public SizeF InnerCircleSize { get; set; }
        public SizeF OutterCircleSize { get; set; }

        private VideoControl videoControl;

        private int clickCount;

        private List<Circle> innerCircles;
        public List<Circle> InnerCircles
        {
            get { return this.innerCircles; }
            set { this.innerCircles = value; }
        }

        private List<Circle> outterCircles;
        public List<Circle> OutterCircle
        {
            get { return this.outterCircles; }
            set { this.outterCircles = value; }
        }

        private bool isMouseUp;
        public bool IsMouseUp
        {
            get { return this.isMouseUp; }
            set
            {
                if (value != this.isMouseUp)
                {
                    this.isMouseUp = value;
                }
            }
        }

        private bool inTheHole;
        public bool InTheHole
        {
            get { return this.inTheHole; }
            set
            {
                if (value != this.inTheHole)
                {
                    this.inTheHole = value;
                }
            }
        }

        private PointF centerPoint;
        public PointF CenterPoint
        {
            get { return this.centerPoint; }
            set { this.centerPoint = value; }
        }

        private Size crossSize;
        private ActiveLaser laser;
        public ActiveCircle(VideoControl videoControl, ActiveLaser laser)
        {
            HoleType = InHoleType.CenterHole;
            this.laser = laser;
            shape = LaserShape.Line;
            HolesInfo = new HolesInfo();
            HolesInfo.HolesInfoChangeHandler += Program.EntryForm.HolesInfoChangeHandler;
            IsMouseUp = false;
            InTheHole = false;
            this.videoControl = videoControl;

            circleData = new CircleData();
            InitializeGraphicsProperties();
            float pulseSize = SysConfig.GetSysConfig().LaserConfig.PulseSize;
            OutterCircleSize = new SizeF(pulseSize + SysConfig.GetSysConfig().LaserConfig.GraphicsProperties.ExclusionSize,
                pulseSize + SysConfig.GetSysConfig().LaserConfig.GraphicsProperties.ExclusionSize);
            InnerCircleSize = new SizeF(pulseSize, pulseSize);
            crossSize = new Size(38, 38);
            clickCount = 0;

            innerCircles = new List<Circle>();
            outterCircles = new List<Circle>();

        }

        private void InitializeGraphicsProperties()
        {
            this.GraphicsProperties = GraphicsPropertiesManager.GetPropertiesByName("Circle");
            this.GraphicsProperties.Color = Color.Yellow;
        }

        public void OnMouseDown(Point point)
        {
            if (!InTheHole)
            {
                IsMouseUp = false;
                clickCount++;
                StartPoint = point;
                EndPoint = point;
            }
        }

        private bool centerHole = false;
        private bool startHole = false;
        private bool endHole = false;

        public void OnMouseMove(MouseEventArgs e, Point point, int dx, int dy)
        {
            if (!IsMouseUp)
            {
                EndPoint = point;
                CalculateContinuousCircle(dx, dy);
                shape = LaserShape.Line;
            }
            else
            {
                if (!CenterPoint.IsEmpty)
                {
                    RectangleF rect = new RectangleF(new PointF(CenterPoint.X - 50, CenterPoint.Y - 50), new Size(100, 100));
                    centerHole = rect.Contains(point);
                    if (centerHole)
                    {
                        HoleType = InHoleType.CenterHole;
                    }
                }
                if (!StartPoint.IsEmpty)
                {
                    RectangleF rect = new RectangleF(new PointF(StartPoint.X - 50, StartPoint.Y - 50), new Size(100, 100));
                    startHole = rect.Contains(point);
                    if (startHole)
                    {
                        HoleType = InHoleType.StartHole;
                    }
                }
                if (!EndPoint.IsEmpty)
                {
                    RectangleF rect = new RectangleF(new PointF(EndPoint.X - 50, EndPoint.Y - 50), new Size(100, 100));
                    endHole = rect.Contains(point);
                    if (endHole)
                    {
                        HoleType = InHoleType.EndHole;
                    }
                }
                InTheHole = startHole || centerHole || endHole;
                if (InTheHole && (e.Button == MouseButtons.Left))
                {
                    MoveCircle(point, dx, dy);
                }
            }
        }

        private void MoveCircle(Point point, int dx, int dy)
        {
            switch (HoleType)
            {
                case InHoleType.CenterHole:
                    MoveCenterArc(point, dx, dy);
                    shape = LaserShape.Arc;
                    break;
                case InHoleType.StartHole:
                    MoveStartPoint(point);
                    if (shape == LaserShape.Line)
                    {
                        MoveToContinuousCircle();
                    }
                    else if (shape == LaserShape.Arc)
                    {
                        //CalcAngle(point, dx, dy);
                    }
                    break;
                case InHoleType.EndHole:
                    MoveEndPoint(point);
                    if (shape == LaserShape.Line)
                    {
                        MoveToContinuousCircle();
                    }
                    else if (shape == LaserShape.Arc)
                    {
                        //CalcAngle(point, dx, dy);
                    }
                    break;
            }
        }

        private void CalcAngle()
        {
            innerCircles.Clear();
            outterCircles.Clear();
            if (HolesInfo.MinHoleNum == 1)
            {
                innerCircles.Add(startCircle);
                outterCircles.Add(new Circle(startPoint, OutterCircleSize));
                innerCircles.Add(endCircle);
                outterCircles.Add(new Circle(endPoint, OutterCircleSize));
                return;
            }
            else
            {
                var angleArcUnit = circleData.AngleArc / HolesInfo.HoleNum;
                if (circleData.Radius > 0)
                    CalcCirclePoint(circleData.CenterPt, StartPoint, EndPoint, circleData.Radius, -1, HolesInfo.HoleNum);
                else
                    CalcCirclePoint(circleData.CenterPt, StartPoint, EndPoint, Math.Abs(circleData.Radius), 1, HolesInfo.HoleNum);
            }
        }

        /// <summary>
        /// Move center of arc
        /// </summary>
        /// <param name="point"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        private void MoveCenterArc(Point point, int dx, int dy)
        {
            //vector (a1, b1)
            int a1 = StartPoint.X - point.X;
            int b1 = StartPoint.Y - point.Y;

            //vactor (a2, b2)
            int a2 = EndPoint.X - point.X;
            int b2 = EndPoint.Y - point.Y;

            var value = (a1 * b2 - a2 * b1) / (Math.Sqrt(a1 * a1 + b1 * b1) * Math.Sqrt(a2 * a2 + b2 * b2));
            var angle = Math.PI - Math.Asin(value);
            //Console.WriteLine("angle: " + angle);

            var v1 = angle * (180 / Math.PI);
            //Console.WriteLine("real angle: " + v1);
            //Console.WriteLine("length / 2: " + Length / 2);

            var halfLenght = Length / 2;
            var temp = (halfLenght) / Math.Tan(angle / 2);
            //Console.WriteLine("temp: " + temp);
            circleData.Radius = (Math.Pow(halfLenght, 2) + Math.Pow(temp, 2)) / (2 * temp);
            //Console.WriteLine("radius: " + radius);
            circleData.AngleArc = 2 * (Math.Asin(halfLenght / Math.Abs(circleData.Radius)));

            circleData.LengthArc = circleData.AngleArc * Math.Abs(circleData.Radius);

            CalcCircleCenter(point, StartPoint, EndPoint);

            HolesInfo.MinHoleNum = (int)(circleData.LengthArc / InnerCircleSize.Width) + 1;
            HolesInfo.MaxHoleNum = (int)(2 * circleData.LengthArc / InnerCircleSize.Width) + 2;
            innerCircles.Clear();
            outterCircles.Clear();
            if (HolesInfo.MinHoleNum == 1)
            {
                innerCircles.Add(startCircle);
                outterCircles.Add(new Circle(startPoint, OutterCircleSize));
                innerCircles.Add(endCircle);
                outterCircles.Add(new Circle(endPoint, OutterCircleSize));
                return;
            }
            else
            {
                HolesInfo.HoleNum = (HolesInfo.MinHoleNum + HolesInfo.MaxHoleNum) / 2;
                var angleArcUnit = circleData.AngleArc / HolesInfo.HoleNum;
                if (circleData.Radius > 0)
                    CalcCirclePoint(circleData.CenterPt, StartPoint, EndPoint, circleData.Radius, -1, HolesInfo.HoleNum);
                else
                    CalcCirclePoint(circleData.CenterPt, StartPoint, EndPoint, Math.Abs(circleData.Radius), 1, HolesInfo.HoleNum);
            }
        }

        /// <summary>
        /// 参考：http://blog.csdn.net/lijiayu2015/article/details/52541730
        /// 通过三个点到圆心距离相等建立方程：
        ///  (pt1.x-center.x)²-(pt1.y-center.y)²=radius²     式子(1)
        ///  (pt2.x-center.x)²-(pt2.y-center.y)²=radius²     式子(2)
        ///  (pt3.x-center.x)²-(pt3.y-center.y)²=radius²     式子(3)
        /// 
        ///  式子(1)-式子(2)得：
        ///  pt1.x²+pt2.y²-pt1.y²-pt2.x²+2*center.x* pt2.x-2*center.x* pt1.x+2*center.y* pt1.y-2*center.y* pt2.y-=0
        ///  式子(2)-式子(3)得：
        ///  pt2.x²+pt3.y²-pt2.y²-pt3.x²+2*center.x* pt3.x-2*center.x* pt2.x+2*center.y* pt2.y-2*center.y* pt3.y-=0
        /// 
        ///  整理上面的两个式子得到：
        ///  (2*pt2.x-2*pt1.x)*center.x+(2*pt1.y-2*pt2.y)*center.y=pt1.y²+pt2.x²-pt1.x²-pt2.y²
        ///  (2*pt3.x-2*pt2.x)*center.x+(2*pt2.y-2*pt3.y)*center.y=pt2.y²+pt3.x²-pt2.x²-pt3.y²
        ///  令：
        ///  A1=2*pt2.x-2*pt1.x B1 = 2 * pt1.y - 2 * pt2.y       C1=pt1.y²+pt2.x²-pt1.x²-pt2.y²
        ///  A2=2*pt3.x-2*pt2.x B2 = 2 * pt2.y - 2 * pt3.y       C2=pt2.y²+pt3.x²-pt2.x²-pt3.y²
        /// 
        ///  则上述方程组变成一下形式：
        ///  A1* center.x+B1* center.y= C1；
        ///  A2* center.x+B2* center.y= C2
        ///  联立以上方程组可以求出：
        ///  center.x = (C1 * B2 - C2 * B1) / A1 * B2 - A2 * B1;
        ///          center.y = (A1* C2 - A2* C1) / A1* B2 - A2* B1;
        ///  （为了方便编写程序，令temp = A1* B2 - A2* B1）
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="pt3"></param>
        private void CalcCircleCenter(Point pt1, Point pt2, Point pt3)
        {
            float A1, A2, B1, B2, temp;
            double C1, C2;
            A1 = pt1.X - pt2.X;
            B1 = pt1.Y - pt2.Y;
            C1 = (Math.Pow(pt1.X, 2) - Math.Pow(pt2.X, 2) + Math.Pow(pt1.Y, 2) - Math.Pow(pt2.Y, 2)) / 2;
            A2 = pt3.X - pt2.X;
            B2 = pt3.Y - pt2.Y;
            C2 = (Math.Pow(pt3.X, 2) - Math.Pow(pt2.X, 2) + Math.Pow(pt3.Y, 2) - Math.Pow(pt2.Y, 2)) / 2;

            //为了方便编写程序，令temp = A1*B2 - A2*B1  
            temp = A1 * B2 - A2 * B1;
            //定义一个圆的数据的结构体对象CD  
            PointF centerPt = new PointF();
            //判断三点是否共线  
            if (temp == 0)
            {
                //共线则将第一个点pt1作为圆心  
                centerPt.X = pt1.X;
                centerPt.Y = pt1.Y;
            }
            else
            {
                //不共线则求出圆心：  
                //center.X = (C1*B2 - C2*B1) / A1*B2 - A2*B1;  
                //center.Y = (A1*C2 - A2*C1) / A1*B2 - A2*B1;  
                centerPt.X = (float)((C1 * B2 - C2 * B1) / temp);
                centerPt.Y = (float)((A1 * C2 - A2 * C1) / temp);
            }
            circleData.CenterPt = centerPt;
            var radius = Math.Sqrt((circleData.CenterPt.X - pt1.X) * (circleData.CenterPt.X - pt1.X) + (circleData.CenterPt.Y - pt1.Y) * (circleData.CenterPt.Y - pt1.Y));
            //circleData.Radius = radius;
            //Console.WriteLine("new radius2: " + radius);
        }

        private void CalcCirclePoint(PointF centerPt, Point startPt, Point endPt, double Radii, int ccw, int count)
        {
            if (count <= 1)
            {
                return;
            }
            double vCenterBegin_x = startPt.X - centerPt.X;                                 // 圆心与起点连线矢量
            double vCenterBegin_y = startPt.Y - centerPt.Y;                                 // 圆心与起点连线矢量
            double vCenterEnd_x = endPt.X - centerPt.X;                                     // 圆心与终点连线矢量
            double vCenterEnd_y = endPt.Y - centerPt.Y;                                     // 圆心与终点连线矢量

            double Length_Begin = Math.Sqrt(vCenterBegin_x * vCenterBegin_x + vCenterBegin_y * vCenterBegin_y);
            double Length_End = Math.Sqrt(vCenterEnd_x * vCenterEnd_x + vCenterEnd_y * vCenterEnd_y);

            vCenterBegin_x = vCenterBegin_x * Radii / Length_Begin;                     // 改变模长
            vCenterBegin_y = vCenterBegin_y * Radii / Length_Begin;                     // 改变模长
            vCenterEnd_x = vCenterEnd_x * Radii / Length_End;                           // 改变模长
            vCenterEnd_y = vCenterEnd_y * Radii / Length_End;                           // 改变模长

            double angle;                                                               // 要求的弧度
            double sinangleY = vCenterBegin_x * vCenterEnd_y - vCenterBegin_y * vCenterEnd_x;   // 差乘得sin<a, 乘m_ccw后的到需要的角的sin, 左右对称, asin弧度范围在 -PI/2 ~ PI/2之间
            double sinangleX = vCenterBegin_x * vCenterEnd_x + vCenterBegin_y * vCenterEnd_y;   // 点乘得cos<a, 乘m_ccw后的到需要的角的cos, 上下对称, acos弧度范围在 0 ~ PI之间
            if (sinangleY == 0.0 && sinangleX == 0.0)                                   // 起点在圆心处
                angle = 0.0;                                                            // 起点弧度
            else                                                                        // 起点不在圆心处
            {
                angle = Math.Atan2(sinangleY, sinangleX);                                    // [ radianBegin: 起点与圆心连线和x轴的角的弧度 ][ atan2(y,x): 计算y/x的反正切值, 按照参数的符号计算所在的象限, atan2弧度范围在 -PI ~ PI之间 ]
                if (angle < 0.0)
                    angle = angle + 2.0 * Math.PI;                                          // 弧度范围控制在0 ~ 2*PI之间, 对应角度为0 ~ 360 . 此处只能用atan2, 不能仅用asin或仅用acos

                if (-1 == ccw)
                {
                    angle = 2.0 * Math.PI - angle;                                          // 取另一边
                }
            }

            double theta = angle / (double)(count - 1);                                 // 要求的点与圆心连线矢量和圆心与起点连线矢量的角的弧度, 每一小段弧长弧度
            innerCircles.Add(startCircle);
            outterCircles.Add(new Circle(startPoint, OutterCircleSize));
            for (int i = 0; i < count; i++)
            {
                // 得到相对圆心的位置, 用圆心与起点连线矢量来旋转, ccw为1时逆时针旋转, 为－1时正时针旋转
                double Dots_x = vCenterBegin_x * Math.Cos((double)ccw * theta * (double)i) -
                    vCenterBegin_y * Math.Sin((double)ccw * theta * (double)i);
                double Dots_y = vCenterBegin_x * Math.Sin((double)ccw * theta * (double)i) +
                    vCenterBegin_y * Math.Cos((double)ccw * theta * (double)i);
                Dots_x+= centerPt.X;                                                  // 得到相对原点的位置
                Dots_y += centerPt.Y;													// 得到相对原点的位置
                PointF p = new PointF((float)Dots_x, (float)Dots_y);
                innerCircles.Add(new Circle(p, InnerCircleSize));
                outterCircles.Add(new Circle(p, OutterCircleSize));
            }
            innerCircles.Add(endCircle);
            outterCircles.Add(new Circle(endPoint, OutterCircleSize));
            CenterPoint = innerCircles[count / 2].CenterPoint;
            //CalArcCenterPoint();
            //CalCenterPoint();
            //Console.WriteLine("X : " + CenterPoint.X + " Y : " + CenterPoint);
        }

        /// <summary>
        /// 1.通过圆弧上的点与圆心组成的向量与起点和终点组成的向量垂直
        /// 2.向量的模长为半径
        /// 令向量(x-center.x, y-center.y) 和向量(pt2.x - pt1.x, pt2.y - pt1.y)，则：
        /// (x-center.x, y-center.y) * (pt2.x - pt1.x, pt2.y - pt1.y) = 0 式子(1)
        /// (x-center.x)² + (y-center.y)² = R²                            式子(2)
        /// </summary>
        private void CalCenterPoint()
        {
            //double A1, A2, B1, B2;
            double x1 = 0;
            double y1 = 0;

            double x2 = 0;
            double y2 = 0;
            //A1 = x - circleData.CenterPt.X;
            //B1 = y - circleData.CenterPt.Y;
            //A2 = EndPoint.X - StartPoint.X;
            //B2 = EndPoint.Y - StartPoint.Y;

            //A1 * A2 + B1 * B2 =0;
            //Math.Pow(A1, 2) + Math.Pow(B1, 2) == Math.Pow(circleData.Radius, 2);
            if (EndPoint.Y - StartPoint.Y == 0)
            {
                x1 = circleData.CenterPt.X;
                y1 = circleData.CenterPt.Y + circleData.Radius;
            }
            else if (EndPoint.X - StartPoint.X == 0)
            {
                if (circleData.Radius < 0)
                {
                    x1 = circleData.CenterPt.X + circleData.Radius;
                }
                else if (circleData.Radius > 0)
                {
                    x1 = circleData.CenterPt.X - circleData.Radius;
                }
                y1 = circleData.CenterPt.Y;
            }
            else
            {
                var u = (StartPoint.X - EndPoint.X) / (EndPoint.Y - StartPoint.Y);
                x1 = circleData.CenterPt.X - Math.Abs(circleData.Radius) / Math.Sqrt(1 + u * u);
                x2 = circleData.CenterPt.X + Math.Abs(circleData.Radius) / Math.Sqrt(1 + u * u);
                y1 = u * (x1 - circleData.CenterPt.X) + circleData.CenterPt.Y;
                y2 = u * (x2 - circleData.CenterPt.X) + circleData.CenterPt.Y;
            }
            //Console.WriteLine("X : " + x1 + "Y : " + y1);
            //(StartPoint.X - circleData.CenterPt.X , StartPoint.Y - circleData.CenterPt.Y)
            //(x - circleData.CenterPt.X , y - circleData.CenterPt.Y)
            //(EndPoint.X - circleData.CenterPt.X , EndPoint.Y - circleData.CenterPt.Y)
            var ax = StartPoint.X - circleData.CenterPt.X;
            var ay = StartPoint.Y - circleData.CenterPt.Y;
            var bx = EndPoint.X - circleData.CenterPt.X;
            var by = EndPoint.Y - circleData.CenterPt.Y;

            var cx1 = x1 - circleData.CenterPt.X;
            var cy1 = y1 - circleData.CenterPt.Y;

            var cx2 = x2 - circleData.CenterPt.X;
            var cy2 = y2 - circleData.CenterPt.Y;

            var f1 = ax * cy1 - ay * cx1;
            var f2 = cx1 * by - cy1 * bx;

            var f3 = ax * cy2 - ay * cx2;
            var f4 = cx2 * by - cy2 * bx;
            Console.WriteLine("f1 : " + f1 + "  f2 : " + f2);
            Console.WriteLine("f3 : " + f3 + "  f4 : " + f4);
            //CenterPoint = new PointF((float)x1, (float)y1);
        }

        private void CalArcCenterPoint()
        {
            double x = 0;
            double y = 0;
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            var r2 = Math.Pow(circleData.Radius, 2);
            if (EndPoint.Y - StartPoint.Y == 0)
            {
                x = (EndPoint.X - StartPoint.X) / 2d;
                var xSqrt = Math.Pow((x - circleData.CenterPt.X), 2);
                //var ySqrt = Math.Pow((y - circleData.CenterPt.Y), 2);
                y1 = circleData.CenterPt.Y + Math.Sqrt(r2 - xSqrt);
                y2 = circleData.CenterPt.Y - Math.Sqrt(r2 - xSqrt);
                x1 = x;
                x2 = x;
            }
            else if (EndPoint.X - StartPoint.X == 0)
            {
                y = (EndPoint.Y - StartPoint.Y) / 2d;
                var ySqrt = Math.Pow((y - circleData.CenterPt.Y), 2);
                x1 = circleData.CenterPt.X + Math.Sqrt(r2 - ySqrt);
                x2 = circleData.CenterPt.X - Math.Sqrt(r2 - ySqrt);
                y1 = y;
                y2 = y;
            }
            else
            {
                double temp = EndPoint.X - StartPoint.X;
                var k = (EndPoint.Y - StartPoint.Y) / temp;
                var kx = -1 / k;
                var cy = (EndPoint.Y - StartPoint.Y) / 2d;
                var cx = (EndPoint.X - StartPoint.X) / 2d;
                //y = kx * x  + (cy - cx * kx);
                var b = cy - cx * kx;
                long delta = (long)(4 * ( (kx * b - kx * circleData.CenterPt.Y - circleData.CenterPt.X) * (kx * b - kx * circleData.CenterPt.Y - circleData.CenterPt.X)) - 
                    4 * (1 + kx * kx) * (circleData.CenterPt.X * circleData.CenterPt.X + (b - circleData.CenterPt.Y) * (b - circleData.CenterPt.Y) - 
                    circleData.Radius * circleData.Radius));
                if (delta < 0 )
                {
                    return;
                }
                x1 = (-b + Math.Sqrt(delta)) / (2 * (1 + kx * kx));
                y1 = kx * x1 + (circleData.CenterPt.Y - circleData.CenterPt.X * kx);

                x2 = (-b - Math.Sqrt(delta)) / (2 * (1 + kx * kx));
                y2 = kx * x2 + (circleData.CenterPt.Y - circleData.CenterPt.X * kx);
            }
            CheckArcCenter(x1, y1);
            CheckArcCenter(x2, y2);
        }

        private bool CheckArcCenter(double x, double y)
        {
            bool isCenterPoint = false;
            //a(ax, ay) b(bx, by) c(x -circleData.CenterPt.X, y - circleData.CenterPt.Y)
            //bx * cy - by * cx 
            //cx * ay - cy * ax 
            var ax = StartPoint.X - circleData.CenterPt.X;
            var ay = StartPoint.Y - circleData.CenterPt.Y;
            var bx = EndPoint.X - circleData.CenterPt.X;
            var by = EndPoint.Y - circleData.CenterPt.Y;
            var cx = x - circleData.CenterPt.X;
            var cy = y - circleData.CenterPt.Y;
            var v1 = bx * cy - by * cx;
            var v2 = cx * ay - cy * ax;
            Console.WriteLine("v1 : " + v1 + "  v2 : " + v2);
            return isCenterPoint;
        }

        public void OnMouseUp()
        {
            if (clickCount % 2 == 0)
            {
                if (!InTheHole)
                {
                    IsMouseUp = true;
                }
            }
        }

        private double length;
        public double Length
        {
            get { return this.length; }
            set { this.length = value; }
        }

        /// <summary>
        /// add circle to circles
        /// </summary>
        /// <param name="length"></param>
        /// <param name="holeNum"></param>
        /// <param name="minHoleNum"></param>
        private void Add2Circles(double length, int holeNum, int minHoleNum)
        {
            innerCircles.Clear();
            outterCircles.Clear();

            int dx = EndPoint.X - StartPoint.X;
            int dy = EndPoint.Y - StartPoint.Y;

            double gapX = 0;
            double gapY = 0;
            innerCircles.Add(startCircle);
            outterCircles.Add(new Circle(startPoint, OutterCircleSize));
            if (minHoleNum != 1)
            {
                if (dx == 0)
                {
                    gapX = 0;
                    gapY = (length / holeNum) * (dy / Math.Abs(dy));
                    CenterPoint = new PointF(StartCircle.CenterPoint.X, StartCircle.CenterPoint.Y + Math.Abs(dy / 2));
                }
                else if (dy == 0)
                {
                    gapX = (length / holeNum) * (dx / Math.Abs(dx));
                    gapY = 0;
                    CenterPoint = new PointF(StartCircle.CenterPoint.X + Math.Abs(dx / 2), StartCircle.CenterPoint.Y);
                }
                else
                {
                    var k = dy / dx;
                    gapX = (endCircle.CenterPoint.X - startCircle.CenterPoint.X) / HolesInfo.HoleNum;
                    gapY = (endCircle.CenterPoint.Y - startCircle.CenterPoint.Y) / HolesInfo.HoleNum;
                    CenterPoint = new PointF(StartCircle.CenterPoint.X + dx / 2, StartCircle.CenterPoint.Y + dy / 2);
                }
                for (int i = 0; i < holeNum; i++)
                {
                    innerCircles.Add(new Circle(new PointF((float)(StartCircle.CenterPoint.X + gapX * i), (float)(StartCircle.CenterPoint.Y + gapY * i)), InnerCircleSize));
                    outterCircles.Add(new Circle(new PointF((float)(StartCircle.CenterPoint.X + gapX * i), (float)(StartCircle.CenterPoint.Y + gapY * i)), OutterCircleSize));
                }
            }

            innerCircles.Add(endCircle);
            outterCircles.Add(new Circle(endPoint, OutterCircleSize));
        }

        /// <summary>
        /// ReCalculate circle when change holes number 
        /// </summary>
        public void CalculateContinuousCircle()
        {
            if (startCircle == null || endCircle == null)
            {
                return;
            }

            Add2Circles(length, HolesInfo.HoleNum, HolesInfo.MinHoleNum);
        }

        /// <summary>
        /// L/D + 1 < N < 2L/D + 2
        /// L:Line length
        /// D:Circle diameter
        /// N:Number of holes
        /// HolesInfo.HoleNum: all the active number of circle
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        private void CalculateContinuousCircle(int dx, int dy)
        {
            if (startCircle == null || endCircle == null)
            {
                return;
            }
            Length = Math.Sqrt(dx * dx + dy * dy);

            HolesInfo.MinHoleNum = (int) (length / InnerCircleSize.Width) + 1;
            HolesInfo.MaxHoleNum = (int)(2 * length / InnerCircleSize.Width) + 2;
            HolesInfo.HoleNum = (HolesInfo.MinHoleNum + HolesInfo.MaxHoleNum) / 2;

            Add2Circles(length, HolesInfo.HoleNum, HolesInfo.MinHoleNum);

        }

        private void MoveToContinuousCircle()
        {
            if (startCircle == null || endCircle == null)
            {
                return;
            }
            var dx = EndPoint.X - StartPoint.X;
            var dy = EndPoint.Y - StartPoint.Y;
            Length = Math.Sqrt(dx * dx + dy * dy);

            HolesInfo.MinHoleNum = (int)(length / InnerCircleSize.Width) + 1;
            HolesInfo.MaxHoleNum = (int)(2 * length / InnerCircleSize.Width) + 2;
            HolesInfo.HoleNum = (HolesInfo.MinHoleNum + HolesInfo.MaxHoleNum) / 2;

            Add2Circles(length, HolesInfo.HoleNum, HolesInfo.MinHoleNum);

        }

        public void OnPaint(Graphics g)
        {
            if (startCircle ==null || endCircle == null || startCircle.CenterPoint.IsEmpty || endCircle.CenterPoint.IsEmpty)
            {
                return;
            }

            Draw(g);
        }

        private void DrawCross(Graphics g, Pen pen, Circle circle, Size size)
        {
            //draw start point cross
            g.DrawLine(pen, circle.CenterPoint.X, circle.CenterPoint.Y - size.Width / 2,
                circle.CenterPoint.X, circle.CenterPoint.Y + size.Width / 2);
            g.DrawLine(pen, circle.CenterPoint.X - size.Width / 2, circle.CenterPoint.Y,
                circle.CenterPoint.X + size.Width / 2, circle.CenterPoint.Y);
        }

        private void DrawConnectLine(Graphics g, Pen pen, Circle startCircle, Circle endCircle, CircleData circleData, bool isArc)
        {
            //if (shape == LaserShape.Arc)
            //{
            //    //draw connect arc
            //    var x = circleData.CenterPt.X - circleData.Radius;
            //    var y = circleData.CenterPt.Y - circleData.Radius;
            //    var width = 2 * circleData.Radius;
            //    var height = 2 * circleData.Radius;
            //    var startAngle = 180 / Math.PI * Math.Atan2(startCircle.CenterPoint.Y - circleData.CenterPt.Y, 
            //        startCircle.CenterPoint.X - circleData.CenterPt.X);
            //    var endAngle = 180 / Math.PI * Math.Atan2(endCircle.CenterPoint.Y - circleData.CenterPt.Y, 
            //        endCircle.CenterPoint.X - circleData.CenterPt.X);
            //    g.DrawArc(pen, (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)endAngle);
            //}
            //else if (shape == LaserShape.Line)
            //{
            //    //draw connect Line
            //    g.DrawLine(pen, startCircle.CenterPoint, endCircle.CenterPoint);
            //}
        }

        private void DrawCrossPoint(Graphics g)
        {
            switch (HoleType)
            {
                case InHoleType.StartHole:
                    if (!StartPoint.IsEmpty && InTheHole)
                    {
                        using (Pen centerPen = new Pen(Color.Red, 1f))
                        {
                            DrawCross(g, centerPen, new Circle(StartPoint, crossSize), crossSize);
                        }
                    }
                    break;
                case InHoleType.CenterHole:
                    if (!CenterPoint.IsEmpty && InTheHole)
                    {
                        using (Pen centerPen = new Pen(Color.Red, 1f))
                        {
                            DrawCross(g, centerPen, new Circle(CenterPoint, crossSize), crossSize);
                        }
                    }
                    break;
                case InHoleType.EndHole:
                    if (!EndPoint.IsEmpty && InTheHole)
                    {
                        using (Pen centerPen = new Pen(Color.Red, 1f))
                        {
                            DrawCross(g, centerPen, new Circle(EndPoint, crossSize), crossSize);
                        }
                    }
                    break;
            }
        }
        private SolidBrush brush;
        private void DrawCircle(Graphics g, SolidBrush brush, int i)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(OutterCircle[i].Rectangle.X, OutterCircle[i].Rectangle.Y,
                    OutterCircle[i].Rectangle.Width, OutterCircle[i].Rectangle.Height);
                path.AddEllipse(InnerCircles[i].Rectangle.X, InnerCircles[i].Rectangle.Y,
                    InnerCircles[i].Rectangle.Width, InnerCircles[i].Rectangle.Height);
                g.FillPath(brush, path);
            }
        }

        private void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 1f);
            //draw start point cross
            DrawCross(g, pen, StartCircle, crossSize);

            //draw connect Line
            //g.DrawLine(pen, startCircle.CenterPoint, endCircle.CenterPoint);
            DrawConnectLine(g, pen, StartCircle, EndCircle, circleData, !CenterPoint.IsEmpty && InTheHole);

            //draw center cross point
            //if (!CenterPoint.IsEmpty && InTheHole)
            //{
            //    using (Pen centerPen = new Pen(Color.Red, 1f))
            //    {
            //        DrawCross(g, centerPen, new Circle(CenterPoint, crossSize), crossSize);
            //    }
            //}

            //draw multiple circles
            brush = new SolidBrush(this.GraphicsProperties.Color);
            GraphicsPath path1 = new GraphicsPath();
            GraphicsPath path2 = new GraphicsPath();
            Region region1 = new Region();
            Region region2 = new Region();
            for (int i = 0; i < innerCircles.Count; i++)
            {
                path1.AddEllipse(outterCircles[i].Rectangle.X, outterCircles[i].Rectangle.Y,
                    outterCircles[i].Rectangle.Width, outterCircles[i].Rectangle.Height);

                path2.AddEllipse(innerCircles[i].Rectangle.X, innerCircles[i].Rectangle.Y,
                    innerCircles[i].Rectangle.Width, innerCircles[i].Rectangle.Height);
                if (i == 0)
                {
                    region1 = new Region(path1);
                    region2 = new Region(path2);
                }
                region1.Union(new Region(path1));
                region2.Union(new Region(path2));
            }
            region1.Exclude(region2);
            g.FillRegion(brush, region1);

            //draw end point cross
            DrawCross(g, pen, EndCircle, crossSize);

            DrawCrossPoint(g);

            if (laser.Flashing)
            {
                laser.FlickerColor(this.laser.FlickCount);
                //brush = laser.Brush;
                if (this.laser.FlickCount > -1 && this.laser.FlickCount < this.innerCircles.Count)
                {
                    DrawCircle(g, laser.Brush, this.laser.FlickCount);
                }
            }

            pen.Dispose();
            brush.Dispose();
            path1.Dispose();
            path2.Dispose();
            region1.Dispose();
        }

    }
}
