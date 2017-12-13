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
    public class ActiveLaser : BaseLaser
    {
        private Point mouseDownPoint;
        private Point endPoint;

        private ActiveCircle activeCircle;
        public ActiveLaser(ZWPictureBox pictureBox) : base()
        {
            this.pictureBox = pictureBox;
            activeCircle = new ActiveCircle(pictureBox, this);
            this.GraphicsProperties.GraphicsPropertiesChangedHandler += GraphicsPropertiesChangedHandler;
        }

        private void GraphicsPropertiesChangedHandler(DrawObject drawObject, GraphicsProperties graphicsProperties)
        {
            float pulseSize = SysConfig.GetSysConfig().LaserConfig.PulseSize;
            activeCircle.OutterCircleSize = new SizeF(this.GraphicsProperties.ExclusionSize + pulseSize,
                this.GraphicsProperties.ExclusionSize + pulseSize);
            activeCircle.InnerCircleSize = new SizeF(pulseSize, pulseSize);

            for (int i=0; i< activeCircle.InnerCircles.Count; i++)
            {
                activeCircle.InnerCircles[i] = new Circle(activeCircle.InnerCircles[i].CenterPoint, activeCircle.InnerCircleSize);
                activeCircle.OutterCircle[i] = new Circle(activeCircle.OutterCircle[i].CenterPoint, activeCircle.OutterCircleSize);
            }
            this.pictureBox.Invalidate();
        }

        public override void OnMouseDown(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            mouseDownPoint = e.Location;

            Point point = new Point((int)(e.X / pictureBox.Zoom - pictureBox.OffsetX), 
                (int)(e.Y / pictureBox.Zoom - pictureBox.OffsetY));

            activeCircle.OnMouseDown(point);
            this.pictureBox.Invalidate();
        }

        public override void OnMouseMove(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            Point point = new Point((int)(e.X / pictureBox.Zoom - pictureBox.OffsetX),
                (int)(e.Y / pictureBox.Zoom - pictureBox.OffsetY));

            Point mousePosNow = e.Location;

            endPoint = point;

            int dx = mousePosNow.X - mouseDownPoint.X;
            int dy = mousePosNow.Y - mouseDownPoint.Y;
            var length = Math.Sqrt(dx * dx + dy * dy);
            activeCircle.OnMouseMove(e, point, dx, dy);
            this.pictureBox.Invalidate();
        }

        public override void OnMouseUp(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            base.OnMouseUp(pictureBox, e);
            activeCircle.OnMouseUp();
            //this.pictureBox.Invalidate();
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
            _flickCount++;
            this.pictureBox.Invalidate();
            if (_flickCount == this.activeCircle.InnerCircles.Count)
            {
                Flashing = false;
            }
        }

        public void UpdateHoleNumber(int value)
        {
            activeCircle.UpdateHoleNum(value);
            this.pictureBox.Invalidate();
        }
    }
}
