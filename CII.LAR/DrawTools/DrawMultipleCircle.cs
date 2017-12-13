using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CII.LAR.UI;
using System.Drawing.Drawing2D;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Draw multiple hollow donut graphic
    /// Author:Zhong Wen 2017/10/10
    /// </summary>
    public class DrawMultipleCircle : DrawObject
    {
        private int count = 5;

        private Circle outterCircle = null;
        public Circle OutterCircle
        {
            get { return outterCircle; }
            private set
            {
                if (value != outterCircle)
                {
                    outterCircle = value;
                }
            }
        }

        private Circle innerCircle = null;
        public Circle InnerCircle
        {
            get { return innerCircle; }
            private set
            {
                if (value != innerCircle)
                {
                    innerCircle = value;
                }
            }
        }

        private List<Circle> outterCircles = null;
        public List<Circle> OutterCircles
        {
            get { return outterCircles; }
            set
            {
                if (value != outterCircles)
                {
                    outterCircles = value;
                }
            }
        }

        private List<Circle> innerCircles = null;
        public List<Circle> InnerCircles
        {
            get { return innerCircles; }
            set
            {
                if (value != innerCircles)
                {
                    innerCircles = value;
                }
            }
        }

        public PointF StartCenterPoint
        {
            get;
            set;
        }

        public PointF EndCenterPoint
        {
            get;
            set;
        }

        public Size OutterCircleSize { get; set; }

        public Size InnerCircleSize { get; set; }

        public DrawMultipleCircle()
        {
            InitializeGraphicsProperties();
            OutterCircleSize = new Size(50, 50);
            InnerCircleSize = new Size(30, 30);

            OutterCircles = new List<Circle>();
            InnerCircles = new List<Circle>();
        }

        public DrawMultipleCircle(ZWPictureBox pictureBox, PointF centerPoint) : this()
        {
            this.pictureBox = pictureBox;
            StartCenterPoint = centerPoint;
        }

        private void InitializeGraphicsProperties()
        {
            this.GraphicsProperties = GraphicsPropertiesManager.GetPropertiesByName("Circle");
            this.GraphicsProperties.Color = Color.Yellow;
        }

        public override void Draw(Graphics g, ZWPictureBox pictureBox)
        {
            
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            SolidBrush brush = new SolidBrush(this.GraphicsProperties.Color);

            GraphicsPath path1 = new GraphicsPath();
            GraphicsPath path2 = new GraphicsPath();
            Region region1 = new Region();
            Region region2 = new Region();
            for (int i= 0; i<count; i++)
            {
                path1.AddEllipse(OutterCircles[i].Rectangle.X, OutterCircles[i].Rectangle.Y,
                    OutterCircles[i].Rectangle.Width, OutterCircles[i].Rectangle.Height);

                path2.AddEllipse(InnerCircles[i].Rectangle.X, InnerCircles[i].Rectangle.Y,
                    InnerCircles[i].Rectangle.Width, InnerCircles[i].Rectangle.Height);
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
            brush.Dispose();
            path1.Dispose();
            path2.Dispose();
            region1.Dispose();
        }

        public override void MoveHandleTo(ZWPictureBox pictureBox, Point point, int handleNumber)
        {
            OutterCircles.Clear();
            InnerCircles.Clear();

            EndCenterPoint = point;
            float dx = EndCenterPoint.X - StartCenterPoint.X;
            float dy = EndCenterPoint.Y - StartCenterPoint.Y;

            var k = dy / dx;
            var length = Math.Sqrt(dx * dx + dy * dy);

            OutterCircles.Add(new Circle(StartCenterPoint, OutterCircleSize));
            InnerCircles.Add(new Circle(StartCenterPoint, InnerCircleSize));
            for (int i=1; i<count; i++)
            {
                float x = 0;
                float y = 0;
                if (dx == 0)
                {
                    x = StartCenterPoint.X;
                    if (dx < 0)
                    {
                        y = StartCenterPoint.Y - 20 * i;
                    }
                    else
                    {
                        y = StartCenterPoint.Y + 20 * i;
                    }
                }
                else if (dy == 0)
                {
                    if (dy < 0)
                    {
                        x = StartCenterPoint.X - 20 * i;
                    }
                    else
                    {
                        x = StartCenterPoint.X + 20 * i;
                    }
                    y = StartCenterPoint.Y;
                }
                else
                {
                    if ((dx > 0 && dy > 0) || (dx > 0 && dy < 0))
                    {
                        x = (float)(StartCenterPoint.X + 20 * i / Math.Sqrt(1 + k * k));
                        y = (float)(StartCenterPoint.Y + k * 20 * i / Math.Sqrt(1 + k * k));
                    }
                    else if ((dx < 0 && dy < 0) || (dx < 0 && dy > 0))
                    {
                        x = (float)(StartCenterPoint.X - 20 * i / Math.Sqrt(1 + k * k));
                        y = (float)(StartCenterPoint.Y - k * 20 * i / Math.Sqrt(1 + k * k));
                    }
                }
                OutterCircles.Add(new Circle(new PointF(x, y), OutterCircleSize));
                InnerCircles.Add(new Circle(new PointF(x, y), InnerCircleSize));
            }
        }

        public override bool HitTest(int nIndex, PointF dataPoint)
        {
            return false;
        }

        public override HitTestResult HitTestForSelection(ZWPictureBox pictureBox, Point point)
        {
            throw new NotImplementedException();
        }
    }
}
