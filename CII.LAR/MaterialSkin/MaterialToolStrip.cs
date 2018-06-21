using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    public class MaterialToolStrip : ToolStrip, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        protected readonly AnimationManager _hoverAnimationManager;
        protected readonly AnimationManager _animationManager;

        public MaterialToolStrip ()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.Clear(Parent.BackColor);
            //282C35 
            using (SolidBrush sb =  new SolidBrush(Color.FromArgb(0x28, 0x2C, 0x35)))
                g.FillRectangle(sb, ClientRectangle);

            //draw border
            using (var borderPen = new Pen(Color.FromArgb(0x1C, 0x1F, 0x26), 2))
                g.DrawRectangle(borderPen, new Rectangle(1, 1, this.Size.Width - 2, this.Size.Height - 2));
            string text = "········";
            SizeF textSize = g.MeasureString(text,  this.Font);
            g.DrawString(text, this.Font, Brushes.Black, new PointF((this.Size.Width - textSize.Width ) / 2f, -2));
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (ToolStripItem subCtrl in this.Items)
            {
                if (subCtrl != null)
                {
                    MaterialToolStripButton mtsb = subCtrl as MaterialToolStripButton;
                    if (mtsb != null && mtsb.MouseState == MouseState.DOWN)
                    {
                        //border color 5A6170 
                        //insider color 1C1F26
                        Rectangle bounds = new Rectangle(mtsb.Bounds.X + 1, mtsb.Bounds.Y + 1, mtsb.Size.Width - 2, mtsb.Size.Height - 2);
                        using (Pen pen = new Pen(Color.FromArgb(0x5A, 0x61, 0x70), 2))
                            g.DrawRectangle(pen, bounds);
                        using (var sb = new SolidBrush(Color.FromArgb(0x1C, 0x1F, 0x26)))
                        {
                            PointF Location = new PointF(subCtrl.Bounds.Location.X + 7.5f, subCtrl.Bounds.Location.Y + 7.5f);
                            Region r1 = new Region(new RectangleF(Location, new SizeF(20, 20)));

                            Region r2 = new Region(bounds);
                            r2.Xor(r1);
                            g.FillRegion(sb, r2);
                            r1.Dispose();
                            r2.Dispose();
                            //g.FillRectangle(sb, mtsb.Bounds);
                        }
                    }
                }
            }

            foreach (ToolStripItem subCtrl in this.Items)
            {

                if (subCtrl.Image != null)
                {
                    PointF Location = new PointF(subCtrl.Bounds.Location.X + 7.5f, subCtrl.Bounds.Location.Y + 7.5f);
                    g.DrawImage(subCtrl.Image, new RectangleF(Location, new SizeF(20, 20)));
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode) return;
        }
    }
}
