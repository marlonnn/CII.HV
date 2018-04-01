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
    /// <summary>
    /// Drawing alignment laser
    /// Author: Zhong Wen 2017/10/19
    /// </summary>
    public class AlignLaser : BaseLaser
    {
        private bool isAlign;
        public bool IsAlign
        {
            get { return isAlign; }
            set
            {
                if (value != isAlign)
                {
                    isAlign = value;
                    this.AlignCircle = null;
                    this.richPictureBox.Invalidate();
                }
            }
        }
        private List<Circle> circles;

        private Circle alignCircle;
        public Circle AlignCircle
        {
            get { return this.alignCircle; }
            set { this.alignCircle = value; }
        }

        private Point clickPoint;
        public Point ClickPoint
        {
            get { return this.clickPoint; }
            private set { this.clickPoint = value; }
        }

        private int index;
        public int Index
        {
            get { return this.index; }
            set
            {
                if (value > -1 && value < 7)
                {
                    this.index = value;
                    this.AlignCircle = circles[value];
                    this.IsShowCross = false;
                    ButtonStateHandler?.Invoke(false);
                    this.richPictureBox.ZoomFit();
                    this.richPictureBox.Invalidate();
                }
            }
        }

        private bool isShowCross = false;
        public bool IsShowCross
        {
            get { return this.isShowCross; }
            set
            {
                if (value != this.isShowCross)
                {
                    this.isShowCross = value;
                    this.richPictureBox.Invalidate();
                }
            }
        }

        private int count = 0;
        public int Count
        {
            get { return this.count; }
            set
            {
                if (value != this.count)
                {
                    count = value;
                }
            }
        }

        public AlignLaser(RichPictureBox richPictureBox) : base()
        {
            this.richPictureBox = richPictureBox;
            circles = new List<Circle>();
            string jsonConfig = JsonFile.ReadJsonConfigString();
            circles = JsonFile.GetConfigFromJsonText<List<Circle>>(jsonConfig);
        }

        public delegate void ButtonState(bool enable);
        public  ButtonState ButtonStateHandler;
        public override void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {

            LaserAlignment laserAlignment = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserAlignment>(CtrlType.LaserAlignment);
            if (e.Button == MouseButtons.Left /*&& IsClickLaser(e.Location)*/ && laserAlignment.Index > -1)
            {
                count++;
                if (count == 1)
                {
                    ZoomHandler?.Invoke(e, true);
                    ButtonStateHandler?.Invoke(false);
                }
                else if (count == 2)
                {
                    IsShowCross = true;
                    ClickPoint = e.Location;
                    Count = 0;
                    ButtonStateHandler?.Invoke(true);
                    PointF pointF = new PointF(e.Location.X/* / richPictureBox.Zoom*/, e.Location.Y/* / richPictureBox.Zoom*/);
                    Coordinate.GetCoordinate().AddPoint(Index, pointF);
                    //Console.WriteLine("add point: " + pointF.ToString());
                }
                //Console.WriteLine(e.Location.ToString());
            }
        }

        private bool IsClickLaser(Point point)
        {
            if (alignCircle != null)
            {
                RectangleF r = new RectangleF(alignCircle.CenterPoint.X - alignCircle.Rectangle.Width / 2, alignCircle.CenterPoint.Y - alignCircle.Rectangle.Height / 2, alignCircle.Rectangle.Width, alignCircle.Rectangle.Height);
                if (r.Contains(point))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public delegate void Zoom(MouseEventArgs e, bool zoom);
        public Zoom ZoomHandler;

        public override void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            base.OnMouseMove(richPictureBox, e);
        }

        public override void OnMouseUp(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            base.OnMouseUp(richPictureBox, e);
        }

        public override void OnPaint(PaintEventArgs e)
        {
            if (AlignCircle == null || AlignCircle.Rectangle.IsEmpty)
            {
                return;
            }
            if (IsAlign)
            {
                Graphics g = e.Graphics;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                //g.ScaleTransform(this.richPictureBox.Zoom, this.richPictureBox.Zoom);
                //g.TranslateTransform(this.richPictureBox.OffsetX, this.richPictureBox.OffsetY);
                //Point point = Coordinate.GetCoordinate().P;
                //if (!point.IsEmpty)
                //{
                //    g.DrawLine(new Pen(Color.Red, 1f),
                //        point.X, point.Y - AlignCircle.Rectangle.Width,
                //        point.X, point.Y + AlignCircle.Rectangle.Width);

                //    g.DrawLine(new Pen(Color.Red, 1f),
                //        point.X - AlignCircle.Rectangle.Width, point.Y,
                //        point.X + AlignCircle.Rectangle.Width, point.Y);
                //}

                //g.DrawEllipse(new Pen(Color.Orange, 2f), new RectangleF(AlignCircle.Rectangle.X, AlignCircle.Rectangle.Y, AlignCircle.Rectangle.Width, AlignCircle.Rectangle.Height));
                //Circle circle2 = new Circle(AlignCircle.CenterPoint,
                //    new Size((int)(1.4 * AlignCircle.Rectangle.Width), (int)(1.4 * AlignCircle.Rectangle.Width)));
                //Circle circle3 = new Circle(AlignCircle.CenterPoint,
                //    new Size((int)(1.4 * circle2.Rectangle.Width), (int)(1.4 * circle2.Rectangle.Width)));
                //g.DrawEllipse(new Pen(Color.Orange, 2f), new RectangleF(circle2.Rectangle.X, circle2.Rectangle.Y, circle2.Rectangle.Width, circle2.Rectangle.Height));
                //g.DrawEllipse(new Pen(Color.Orange, 2f), new RectangleF(circle3.Rectangle.X, circle3.Rectangle.Y, circle3.Rectangle.Width, circle3.Rectangle.Height));
                //g.ResetTransform();
                if (IsShowCross)
                    DrawCross(g);
            }
        }

        public void PaintAlignment(Graphics g)
        {
            if (AlignCircle == null || AlignCircle.Rectangle.IsEmpty)
            {
                return;
            }
            if (IsAlign)
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawEllipse(new Pen(Color.Orange, 2f), AlignCircle.Rectangle);
                Circle circle2 = new Circle(AlignCircle.CenterPoint,
                    new Size((int)(1.4 * AlignCircle.Rectangle.Width), (int)(1.4 * AlignCircle.Rectangle.Width)));
                Circle circle3 = new Circle(AlignCircle.CenterPoint,
                    new Size((int)(1.4 * circle2.Rectangle.Width), (int)(1.4 * circle2.Rectangle.Width)));
                g.DrawEllipse(new Pen(Color.Orange, 2f), circle2.Rectangle);
                g.DrawEllipse(new Pen(Color.Orange, 2f), circle3.Rectangle);
                if (IsShowCross)
                    DrawCross(g);
            }
        }

        private void DrawCross(Graphics g)
        {
            if (AlignCircle.Rectangle.IsEmpty)
            {
                return;
            }
            var vx = ClickPoint.X;
            var vy = ClickPoint.Y ;
            //g.ScaleTransform(this.richPictureBox.Zoom, this.richPictureBox.Zoom);
            //g.TranslateTransform(this.richPictureBox.OffsetX, this.richPictureBox.OffsetY);
            g.DrawLine(new Pen(Color.Red, 1f),
                ClickPoint.X, ClickPoint.Y  - AlignCircle.Rectangle.Width,
                ClickPoint.X, ClickPoint.Y + AlignCircle.Rectangle.Width);

            g.DrawLine(new Pen(Color.Red, 1f),
                ClickPoint.X - AlignCircle.Rectangle.Width, ClickPoint.Y,
                ClickPoint.X + AlignCircle.Rectangle.Width, ClickPoint.Y);
            //g.ResetTransform();
        }
    }
}
