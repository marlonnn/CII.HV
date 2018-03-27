using CII.LAR.Algorithm;
using CII.LAR.Commond;
using CII.LAR.DrawTools;
using CII.LAR.Protocol;
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
    public class ActiveLaser : BaseLaser
    {
        private Point mouseDownPoint;
        private Point endPoint;

        private ActiveCircle activeCircle;
        public ActiveCircle ActiveCircle
        {
            get { return this.activeCircle; }
        }

        public ActiveLaser(VideoControl videoControl) : base()
        {
            this.FlashTimer.Interval = 500;
            this.videoControl = videoControl;
            activeCircle = new ActiveCircle(videoControl, this);
            this.GraphicsProperties.GraphicsPropertiesChangedHandler += GraphicsPropertiesChangedHandler;
        }

        private void GraphicsPropertiesChangedHandler(DrawObject drawObject, GraphicsProperties graphicsProperties)
        {
            float pulseSize = Program.SysConfig.LaserConfig.PulseSize;
            activeCircle.OutterCircleSize = new SizeF(this.GraphicsProperties.ExclusionSize + pulseSize,
                this.GraphicsProperties.ExclusionSize + pulseSize);
            activeCircle.InnerCircleSize = new SizeF(pulseSize, pulseSize);

            for (int i=0; i< activeCircle.InnerCircles.Count; i++)
            {
                activeCircle.InnerCircles[i] = new Circle(activeCircle.InnerCircles[i].CenterPoint, activeCircle.InnerCircleSize);
                activeCircle.OutterCircle[i] = new Circle(activeCircle.OutterCircle[i].CenterPoint, activeCircle.OutterCircleSize);
            }
            this.videoControl.Invalidate();
        }

        public override void OnMouseDown(VideoControl videoControl, MouseEventArgs e)
        {
            mouseDownPoint = e.Location;

            activeCircle.OnMouseDown(e.Location);
            this.videoControl.Invalidate();
        }

        public override void OnMouseMove(VideoControl videoControl, MouseEventArgs e)
        {
            base.OnMouseMove(videoControl, e);
            Point mousePosNow = e.Location;
            int dx = mousePosNow.X - mouseDownPoint.X;
            int dy = mousePosNow.Y - mouseDownPoint.Y;
            activeCircle.OnMouseMove(e, e.Location, dx, dy);
            this.videoControl.Invalidate();
        }

        public override void OnMouseUp(VideoControl videoControl, MouseEventArgs e)
        {
            base.OnMouseUp(videoControl, e);
            activeCircle.OnMouseUp();
            this.videoControl.Invalidate();
        }

        public override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            activeCircle.OnPaint(g);
        }

        public void FlickerColor(int cycle)
        {
            //this.brush = new SolidBrush(cycle % 2 == 0 ? this.GraphicsProperties.Color : Color.Red);
            this.brush = new SolidBrush(Color.Red);
        }

        protected override void FlashTimer_Tick(object sender, EventArgs e)
        {
            if (Coordinate.GetCoordinate().MotionComplete)
            {
                _flickCount++;
                //if (_flickCount > 1)
                {
                    LaserProtocolFactory.GetInstance().SendMessage(new LaserC71Request());
                    //Thread.Sleep(10);
                }
                SendAlignmentMotorPoint();
                this.videoControl.Invalidate();
                if (_flickCount == this.activeCircle.InnerCircles.Count)
                {
                    Flashing = false;
                }
            }
        }

        private void SendAlignmentMotorPoint()
        {
            if (_flickCount >= 0 && _flickCount < ActiveCircle.InnerCircles.Count)
            {
                if (_flickCount > 1)
                {
                    Coordinate.GetCoordinate().SetMotorLastPoint(Point.Ceiling(ActiveCircle.InnerCircles[_flickCount - 1].CenterPoint));
                }
                Coordinate.GetCoordinate().SetMotorThisPoint(Point.Ceiling(ActiveCircle.InnerCircles[_flickCount].CenterPoint));
                Coordinate.GetCoordinate().SendAlignmentMotorPoint();
            }
        }

        public void UpdateHoleNumber(int value)
        {
            activeCircle.UpdateHoleNum(value);
            this.videoControl.Invalidate();
        }
    }
}
