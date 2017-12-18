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
                    this.CurrentCircle = null;
                    if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                        this.pictureBox.Invalidate();
                }
            }
        }
        private List<Circle> circles;

        private Circle currentCircle;
        public Circle CurrentCircle
        {
            get { return this.currentCircle; }
            set { this.currentCircle = value; }
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
                    this.CurrentCircle = circles[value];
                    this.IsShowCross = false;
                    ButtonStateHandler?.Invoke(false);
                    this.pictureBox.ZoomFit();
                    if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                        this.pictureBox.Invalidate();
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
                    if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                        this.pictureBox.Invalidate();
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
        public AlignLaser(ZWPictureBox pictureBox) :base()
        {
            this.pictureBox = pictureBox;
            circles = new List<Circle>();
            string jsonConfig = JsonFile.ReadJsonConfigString();
            circles = JsonFile.GetConfigFromJsonText<List<Circle>>(jsonConfig);
        }

        public delegate void ButtonState(bool enable);
        public  ButtonState ButtonStateHandler;
        public override void OnMouseDown(ZWPictureBox pictureBox, MouseEventArgs e)
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
                    Count = 0;
                    ButtonStateHandler?.Invoke(true);
                }
            }
        }

        private bool IsClickLaser(Point point)
        {
            if (currentCircle != null)
            {
                RectangleF r = new RectangleF(currentCircle.CenterPoint.X - currentCircle.Rectangle.Width / 2, currentCircle.CenterPoint.Y - currentCircle.Rectangle.Height / 2, currentCircle.Rectangle.Width, currentCircle.Rectangle.Height);
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

        public override void OnMouseMove(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            base.OnMouseMove(pictureBox, e);
        }

        public override void OnMouseUp(ZWPictureBox pictureBox, MouseEventArgs e)
        {
            base.OnMouseUp(pictureBox, e);
        }

        public override void OnPaint(PaintEventArgs e)
        {
            if (CurrentCircle == null || CurrentCircle.Rectangle.IsEmpty)
            {
                return;
            }
            if (IsAlign)
            {
                Graphics g = e.Graphics;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawEllipse(new Pen(Color.Orange, 2f), CurrentCircle.Rectangle);
                Circle circle2 = new Circle(CurrentCircle.CenterPoint, 
                    new Size((int)(1.4 * CurrentCircle.Rectangle.Width), (int)(1.4 * CurrentCircle.Rectangle.Width)));
                Circle circle3 = new Circle(CurrentCircle.CenterPoint, 
                    new Size((int)(1.4 * circle2.Rectangle.Width), (int)(1.4 * circle2.Rectangle.Width)));
                g.DrawEllipse(new Pen(Color.Orange, 2f), circle2.Rectangle);
                g.DrawEllipse(new Pen(Color.Orange, 2f), circle3.Rectangle);
                if (IsShowCross)
                    DrawCross(g);
            }
        }

        private void DrawCross(Graphics g)
        {
            if (CurrentCircle.Rectangle.IsEmpty)
            {
                return;
            }

            g.DrawLine(new Pen(Color.Red, 1f),
                CurrentCircle.CenterPoint.X, CurrentCircle.CenterPoint.Y - CurrentCircle.Rectangle.Width,
                CurrentCircle.CenterPoint.X, CurrentCircle.CenterPoint.Y + CurrentCircle.Rectangle.Width );

            g.DrawLine(new Pen(Color.Red, 1f),
                CurrentCircle.CenterPoint.X - CurrentCircle.Rectangle.Width, CurrentCircle.CenterPoint.Y,
                CurrentCircle.CenterPoint.X + CurrentCircle.Rectangle.Width, CurrentCircle.CenterPoint.Y);
        }
    }
}
