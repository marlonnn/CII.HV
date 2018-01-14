using CII.LAR.Algorithm;
using CII.LAR.DrawTools;
using CII.LAR.SysClass;
using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.Laser
{
    public class FixedLaser : BaseLaser
    {
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

        public SizeF OutterCircleSize { get; set; }

        public SizeF InnerCircleSize { get; set; }

        public FixedLaser(VideoControl videoControl) : base()
        {
            this.videoControl = videoControl;
            float pulseSize = SysConfig.GetSysConfig().LaserConfig.PulseSize;
            OutterCircleSize = new SizeF(pulseSize + SysConfig.GetSysConfig().LaserConfig.GraphicsProperties.ExclusionSize, 
                pulseSize + SysConfig.GetSysConfig().LaserConfig.GraphicsProperties.ExclusionSize);
            InnerCircleSize = new SizeF(pulseSize, pulseSize);
            this.GraphicsProperties.GraphicsPropertiesChangedHandler += GraphicsPropertiesChangedHandler;
        }
        private void GraphicsPropertiesChangedHandler(DrawObject drawObject, GraphicsProperties graphicsProperties)
        {
            float pulseSize = SysConfig.GetSysConfig().LaserConfig.PulseSize;
            OutterCircleSize = new SizeF(this.GraphicsProperties.ExclusionSize + pulseSize,
                this.GraphicsProperties.ExclusionSize + pulseSize);
            InnerCircleSize = new SizeF(pulseSize, pulseSize);
            OutterCircle = new Circle(CenterPoint, OutterCircleSize);
            InnerCircle = new Circle(CenterPoint, InnerCircleSize);
            this.videoControl.Invalidate();
        }

        public override void OnMouseDown(VideoControl videoControl, MouseEventArgs e)
        {
            Point point = e.Location;
            CenterPoint = new PointF(point.X, point.Y);
            this.videoControl.Invalidate();
            Coordinate.GetCoordinate().SetMotorThisPoint(point);
        }

        public override void OnMouseMove(VideoControl videoControl, MouseEventArgs e)
        {
            base.OnMouseMove(videoControl, e);
        }

        public override void OnMouseUp(VideoControl videoControl, MouseEventArgs e)
        {
            base.OnMouseUp(videoControl, e);
        }

        private void FlickerColor(int cycle)
        {
            this.brush = new SolidBrush(cycle % 2 == 0 ? this.GraphicsProperties.Color : Color.Red);
        }

        public override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (CenterPoint.IsEmpty)
            {
                return;
            }
            Graphics g = e.Graphics;
            OutterCircle = new Circle(CenterPoint, OutterCircleSize);
            InnerCircle = new Circle(CenterPoint, InnerCircleSize);
            //path for the outer and inner circles
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = new GraphicsPath())
            {
                brush = new SolidBrush(this.GraphicsProperties.Color);
                if (Flashing)
                {
                    FlickerColor(this._flickCount);
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
            g.DrawLine(new Pen(Color.Black, this.GraphicsProperties.PenWidth),
                InnerCircle.CenterPoint.X, InnerCircle.CenterPoint.Y - 10 * this.GraphicsProperties.TargetSize,
                InnerCircle.CenterPoint.X, InnerCircle.CenterPoint.Y + 10 *this.GraphicsProperties.TargetSize);

            g.DrawLine(new Pen(Color.Black, this.GraphicsProperties.PenWidth),
                InnerCircle.CenterPoint.X - 10 * this.GraphicsProperties.TargetSize, InnerCircle.CenterPoint.Y,
                InnerCircle.CenterPoint.X + 10 * this.GraphicsProperties.TargetSize, InnerCircle.CenterPoint.Y);
        }
    }
}
