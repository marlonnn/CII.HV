using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    public class MaterialGroupBox :GroupBox, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public MaterialGroupBox()
        {
            this.ForeColor = SkinManager.GetLabelTextColor();
            this.Font = SkinManager.PINGFANG_MEDIUM_9;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            // Clear text and border
            g.Clear(this.BackColor);
            Brush textBrush = new SolidBrush(this.ForeColor);
            Pen borderPen = new Pen(Color.FromArgb(0x1C, 0x1F, 0x26), 1.5F);
            SizeF strSize = g.MeasureString(this.Text, this.Font);
            Rectangle rect = new Rectangle(this.ClientRectangle.X,
                                           this.ClientRectangle.Y + (int)(strSize.Height / 2),
                                           this.ClientRectangle.Width - 1,
                                           this.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);
            // Draw text
            g.DrawString(this.Text, this.Font, textBrush, this.Padding.Left, 0);

            // Drawing Border
            //Left
            g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
            //Right
            g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
            //Bottom
            g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
            //Top1
            g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + this.Padding.Left, rect.Y));
            //Top2
            g.DrawLine(borderPen, new Point(rect.X + this.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));

            borderPen.Dispose();

        }
    }
}
