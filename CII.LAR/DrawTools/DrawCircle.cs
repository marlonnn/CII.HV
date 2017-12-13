using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CII.LAR.UI;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Draw hollow donut graphic
    /// Author:Zhong Wen 2017/07/27
    /// </summary>
    public class DrawCircle : DrawObject
    {
        private Timer timer;

        public int Interval
        {
            get
            {
                return this.timer.Interval;
            }
            set
            {
                this.timer.Interval = value;
            }
        }

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
        public PointF CenterPoint
        {
            get;
            set;
        }

        public Size OutterCircleSize { get; set; }

        public Size InnerCircleSize { get; set; }

        private bool flashing;
        private int flickCount;
        public bool Flashing
        {
            get { return flashing; }
            set
            {
                flickCount = 0;
                this.flashing = value;
                if (value)
                {
                    this.timer.Enabled = value;
                    this.timer.Start();
                    flickCount = -1;
                    this.timer_Tick(null, null);
                }
                else
                {
                    this.timer.Enabled = value;
                    this.timer.Stop();
                }
                if (this.pictureBox != null)
                    this.pictureBox.Invalidate();
            } 
        }

        private SolidBrush brush;
        public DrawCircle()
        {
            InitializeGraphicsProperties();
            InitializeTimer();
            OutterCircleSize = new Size(60, 60);
            InnerCircleSize = new Size(38, 38);
        }

        private void InitializeTimer()
        {
            this.timer = new Timer();
            this.timer.Interval = 300; // Each 150 miliseconds, the progress circle will be drawn again
            this.timer.Tick += new EventHandler(timer_Tick);
            //this.timer.Enabled = true;
            Flashing = true;
        }

        public DrawCircle(ZWPictureBox pictureBox, PointF centerPoint) : this()
        {
            this.pictureBox = pictureBox;
            CenterPoint = centerPoint;
            this.GraphicsProperties.GraphicsPropertiesChangedHandler += GraphicsPropertiesChangedHandler;
        }

        private void GraphicsPropertiesChangedHandler(DrawObject drawObject, GraphicsProperties graphicsProperties)
        {
            OutterCircleSize = new Size((60 + this.GraphicsProperties.ExclusionSize) * this.GraphicsProperties.TargetSize, 
                (60 + this.GraphicsProperties.ExclusionSize) * this.GraphicsProperties.TargetSize);
            InnerCircleSize = new Size(38 * this.GraphicsProperties.TargetSize, 38 * this.GraphicsProperties.TargetSize);
            OutterCircle = new Circle(CenterPoint, OutterCircleSize);
            InnerCircle = new Circle(CenterPoint, InnerCircleSize);
            pictureBox.GraphicsPropertiesChangedHandler(drawObject, graphicsProperties);
        }

        private void InitializeGraphicsProperties()
        {
            this.GraphicsProperties = GraphicsPropertiesManager.GetPropertiesByName("Circle");
            this.GraphicsProperties.Color = Color.Yellow;
        }

        private void timer_Tick(object sender, EventArgs e)
        {

            flickCount++;
            if (this.pictureBox != null)
            {
                this.pictureBox.Invalidate();
            }
            //if (flickCount == 6)
            //{
            //    Flashing = false;
            //}
        }

        public override void Draw(Graphics g, ZWPictureBox pictureBox)
        {
            if (OutterCircle == null)
            {
                OutterCircle = new Circle(CenterPoint, OutterCircleSize);
            }
            if (InnerCircle == null)
            {
                InnerCircle = new Circle(CenterPoint, InnerCircleSize);
            }
            //path for the outer and inner circles
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = new GraphicsPath())
            {
                if (Flashing)
                {
                    brush = new SolidBrush(this.flickCount % 2 == 1 ? this.GraphicsProperties.Color : Color.LightSalmon);
                }
                else
                {
                    brush = new SolidBrush(this.GraphicsProperties.Color);
                }

                path.AddEllipse(OutterCircle.Rectangle.X, OutterCircle.Rectangle.Y,
                    OutterCircle.Rectangle.Width, OutterCircle.Rectangle.Height);
                path.AddEllipse(InnerCircle.Rectangle.X, InnerCircle.Rectangle.Y,
                    InnerCircle.Rectangle.Width, InnerCircle.Rectangle.Height);
                g.FillPath(brush, path);
            }
            DrawCross(g);
            brush.Dispose();
        }

        private void DrawCross(Graphics g)
        {
            g.DrawLine(new Pen(Color.Black, 1f), 
                InnerCircle.CenterPoint.X, InnerCircle.CenterPoint.Y - InnerCircle.Rectangle.Width / 2,
                InnerCircle.CenterPoint.X, InnerCircle.CenterPoint.Y + InnerCircle.Rectangle.Width / 2);

            g.DrawLine(new Pen(Color.Black, 1f),
                InnerCircle.CenterPoint.X - InnerCircle.Rectangle.Width / 2, InnerCircle.CenterPoint.Y,
                InnerCircle.CenterPoint.X + InnerCircle.Rectangle.Width / 2, InnerCircle.CenterPoint.Y);
        }

        /// <summary>
        /// Mouse move to new point
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public override void MoveHandleTo(ZWPictureBox pictureBox, Point point, int handleNumber)
        {
        }


        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public override Point GetHandle(ZWPictureBox pictureBox, int handleNumber)
        {
            float x = 0, y = 0, xCenter, yCenter;

            RectangleF rect = outterCircle.Rectangle;
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

            return Point.Round(pts[0]);
        }

        public override bool HitTest(int nIndex, PointF dataPoint)
        {
            throw new NotImplementedException();
        }

        public override HitTestResult HitTestForSelection(ZWPictureBox pictureBox, Point point)
        {
            throw new NotImplementedException();
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
    }
}
