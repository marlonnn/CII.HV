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
        private Brush textBrush;
        private Pen borderPen;

        public MaterialGroupBox()
        {
            this.ForeColor = SkinManager.GetLabelTextColor();
            this.Font = SkinManager.PINGFANG_MEDIUM_9;
            textBrush = new SolidBrush(this.ForeColor);
            borderPen = new Pen(SkinManager.GroupBoxBorderColor, 1.5F);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Clear text and border
            e.Graphics.Clear(this.BackColor);
            //Brush textBrush = new SolidBrush(this.ForeColor);
            SizeF strSize = e.Graphics.MeasureString(this.Text, this.Font);
            Rectangle rect = new Rectangle(this.ClientRectangle.X,
                                           this.ClientRectangle.Y + (int)(strSize.Height / 2),
                                           this.ClientRectangle.Width - 1,
                                           this.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);
            // Draw text
            e.Graphics.DrawString(this.Text, this.Font, textBrush, this.Padding.Left, 0);

            // Drawing Border
            //Left
            e.Graphics.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
            //Right
            e.Graphics.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
            //Bottom
            e.Graphics.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
            //Top1
            e.Graphics.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + this.Padding.Left, rect.Y));
            //Top2
            e.Graphics.DrawLine(borderPen, new Point(rect.X + this.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));

            //borderPen.Dispose();
            //textBrush.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            borderPen.Dispose();
            textBrush.Dispose();
        }
    }
}
