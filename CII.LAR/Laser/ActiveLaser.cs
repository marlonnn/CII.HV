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

        public ActiveLaser(RichPictureBox richPictureBox) : base()
        {
            this.FlashTimer.Interval = 1000;
            this.richPictureBox = richPictureBox;
            activeCircle = new ActiveCircle(richPictureBox, this);
            this.GraphicsProperties.GraphicsPropertiesChangedHandler += GraphicsPropertiesChangedHandler;
        }

        private void GraphicsPropertiesChangedHandler(DrawObject drawObject, GraphicsProperties graphicsProperties)
        {
            float pulseSize = Program.SysConfig.LaserConfig.PulseSize;
            activeCircle.OutterCircleSize = new SizeF(this.GraphicsProperties.ExclusionSize + pulseSize,
                this.GraphicsProperties.ExclusionSize + pulseSize);
            activeCircle.InnerCircleSize = new SizeF(pulseSize, pulseSize);

            for (int i = 0; i < activeCircle.InnerCircles.Count; i++)
            {
                activeCircle.InnerCircles[i] = new Circle(activeCircle.InnerCircles[i].CenterPoint, activeCircle.InnerCircleSize);
                activeCircle.OutterCircle[i] = new Circle(activeCircle.OutterCircle[i].CenterPoint, activeCircle.OutterCircleSize);
            }
            this.richPictureBox.Invalidate();
        }

        public override void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            circle = 0;
            CircleFire = true;
            PointF pf = new PointF(e.Location.X / richPictureBox.Zoom - richPictureBox.OffsetX, e.Location.Y / richPictureBox.Zoom - richPictureBox.OffsetY);
            if (richPictureBox.RestrictArea.CheckPointInRegion(pf)) return;
            //if (richPictureBox.RestrictArea.CheckPointInRegion(e.Location)) return;
            if (Program.EntryForm.Laser.Flashing) return;
            mouseDownPoint = Point.Ceiling(pf);

            activeCircle.OnMouseDown(mouseDownPoint);

            SendMotorPointOnMouseDown();
            this.richPictureBox.Invalidate();
        }

        public override void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            PointF pf = new PointF(e.Location.X / richPictureBox.Zoom - richPictureBox.OffsetX, e.Location.Y / richPictureBox.Zoom - richPictureBox.OffsetY);
            if (richPictureBox.RestrictArea.CheckPointInRegion(pf)) return;
            //if (richPictureBox.RestrictArea.CheckPointInRegion(e.Location)) return;
            if (Program.EntryForm.Laser.Flashing) return;
            base.OnMouseMove(richPictureBox, e);
            Point mousePosNow = Point.Ceiling(pf);
            int dx = mousePosNow.X - mouseDownPoint.X;
            int dy = mousePosNow.Y - mouseDownPoint.Y;
            activeCircle.OnMouseMove(e, mousePosNow, dx, dy);
            this.richPictureBox.Invalidate();
        }

        public override void OnMouseUp(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            PointF pf = new PointF(e.Location.X / richPictureBox.Zoom - richPictureBox.OffsetX, e.Location.Y / richPictureBox.Zoom - richPictureBox.OffsetY);
            if (richPictureBox.RestrictArea.CheckPointInRegion(pf)) return;
            //if (richPictureBox.RestrictArea.CheckPointInRegion(e.Location)) return;
            if (Program.EntryForm.Laser.Flashing) return;
            base.OnMouseUp(richPictureBox, e);
            activeCircle.OnMouseUp();
            this.richPictureBox.Invalidate();
        }

        public override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            activeCircle.OnPaint(g);
        }

        public override void ClearLaser()
        {
            activeCircle.StartCircle = null;
            activeCircle.EndCircle = null;
            ResetCircles();
            //activeCircle.StartCircle.CenterPoint = Point.Empty;
            //activeCircle.EndCircle.CenterPoint = Point.Empty;
        }

        public void FlickerColor(int cycle)
        {
            //this.brush = new SolidBrush(cycle % 2 == 0 ? this.GraphicsProperties.Color : Color.Red);
            this.brush = new SolidBrush(Color.Red);
        }

        private int circle = 0;
        public int Circle
        {
            get { return this.circle; }
            set { this.circle = value; }
        }

        private bool circleFire = true;
        public bool CircleFire
        {
            get { return this.circleFire; }
            set { this.circleFire = value; }
        }

        protected override void FlashTimer_Tick(object sender, EventArgs e)
        {
            if (Coordinate.GetCoordinate().MotionComplete)
            {
                if (Circle == 0)
                {
                    ContinueFire();
                }
                else
                {
                    if (circleFire)
                    {
                        SendMotorPointOnMouseDown();
                        circleFire = false;
                    }
                    else
                    {
                        ContinueFire();
                    }
                }
                this.richPictureBox.Invalidate();
            }
        }

        private void ContinueFire()
        {
            FlickCount++;
            if (_flickCount == this.activeCircle.InnerCircles.Count)
            {
                Flashing = false;
            }
            else
            {
                if (SerialPortManager.GetInstance() != null)
                {
                    LaserC71Request c71 = new LaserC71Request();
                    var bytes = SerialPortManager.GetInstance().Encode(c71);
                    SerialPortManager.GetInstance().SendData(bytes);
                }
                SendAlignmentMotorPoint();
            }
        }

        private void SendAlignmentMotorPoint()
        {
            if (_flickCount >= 0 && _flickCount < ActiveCircle.InnerCircles.Count - 1)
            {
                Coordinate.GetCoordinate().SetMotorLastPoint(Point.Ceiling(ActiveCircle.InnerCircles[FlickCount].CenterPoint));
                Coordinate.GetCoordinate().SetMotorThisPoint(Point.Ceiling(ActiveCircle.InnerCircles[FlickCount + 1].CenterPoint));
                Coordinate.GetCoordinate().SendAlignmentMotorPoint();
            }
        }

        private void SendMotorPointOnMouseDown()
        {
            if (Program.SysConfig.LiveMode)
            {
                Point point = /*GetMotorPoint(*/Point.Ceiling((new Circle(ActiveCircle.StartPoint, ActiveCircle.InnerCircleSize)).CenterPoint)/*)*/;
                Coordinate.GetCoordinate().SetMotorThisPoint(point);
                Coordinate.GetCoordinate().SendAlignmentMotorPoint();
            }
        }

        private PointF GetMotorPointF(PointF original)
        {
            return new PointF(original.X / this.richPictureBox.Zoom - this.richPictureBox.OffsetX, original.Y / this.richPictureBox.Zoom - this.richPictureBox.OffsetY);
        }

        private Point GetMotorPoint(PointF original)
        {
            return Point.Ceiling(GetMotorPointF(original));
        }
        public void ResetCircles()
        {
            ActiveCircle.InnerCircles.Clear();
            ActiveCircle.OutterCircle.Clear();
        }

        public void UpdateHoleNumber(int value)
        {
            activeCircle.UpdateHoleNum(value);
            this.richPictureBox.Invalidate();
        }
    }
}
