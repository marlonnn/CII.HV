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
    public class MaterialComboBox: ComboBox , IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public MaterialComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
            //CAD1E0
            this.HighlightColor = Color.FromArgb(0xCA, 0xD1, 0xE0);
            this.DrawItem += ColoredComboBox_DrawItem;
        }

        public void ColoredComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {

            //if ((e.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit)
            //    e.DrawBackground();
            Graphics g = e.Graphics;
            //if (e.Index >= 0 /*&& e.Index < colorDics.Count*/)  //if index is -1 do nothing
            {
                PaintComboBoxItem(e);
            }

            if (e.Index < 0)
                return;

            ComboBox combo = sender as ComboBox;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.FillRectangle(new SolidBrush(HighlightColor),
                                         e.Bounds);
            //else
            //    e.Graphics.FillRectangle(new SolidBrush(combo.BackColor),
            //                             e.Bounds);

            //e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font,
            //                      new SolidBrush(combo.ForeColor),
            //                      new Point(e.Bounds.X, e.Bounds.Y));

            e.DrawFocusRectangle();
        }

        public const int BorderWidth = 2;
        public Color HighlightColor { get; set; }
        private void PaintComboBoxItem(DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rectangle = new Rectangle(
                e.Bounds.X,
                e.Bounds.Y,
                e.Bounds.Width,
                e.Bounds.Height);

            using (SolidBrush sb = new SolidBrush(Color.Gray))
            {
                g.FillRectangle(sb, rectangle.X, rectangle.Y, this.ClientRectangle.Width, rectangle.Height);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            using (var brush = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
                e.Graphics.DrawRectangle(Pens.DarkGray, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xF)
            {
                RePaint();
            }
        }
        private Brush BorderBrush = new SolidBrush(Color.FromArgb(0x1A, 0x1E, 0x25));
        private Brush ArrowBrush = new SolidBrush(SystemColors.ControlText);
        private Brush DropButtonBrush = new SolidBrush(SystemColors.Control);
        private void RePaint()
        {
            Graphics g = this.CreateGraphics();
            //1A1E25
            using (Pen p = new Pen(Color.FromArgb(0x1A, 0x1E, 0x25)))
                g.FillRectangle(BorderBrush, this.ClientRectangle);

            //CAD1E0 
            using (Pen boardPen = new Pen(Color.FromArgb(0xCA, 0xD1, 0xE0)))
                g.DrawRectangle(boardPen, new Rectangle(1, 1, this.ClientRectangle.Width - 2, this.ClientRectangle.Height - 2));
                //Draw the background of the dropdown button
                //Rectangle rect = new Rectangle(this.Width - 17, 0, 17, this.Height);
                //g.FillRectangle(DropButtonBrush, rect);


            //Create the path for the arrow
            System.Drawing.Drawing2D.GraphicsPath pth = new System.Drawing.Drawing2D.GraphicsPath();
            PointF TopLeft = new PointF(this.Width - 13, (this.Height - 5) / 2);
            PointF TopRight = new PointF(this.Width - 6, (this.Height - 5) / 2);
            PointF Bottom = new PointF(this.Width - 9, (this.Height + 2) / 2);
            pth.AddLine(TopLeft, TopRight);
            pth.AddLine(TopRight, Bottom);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //Determine the arrow's color.
            if (this.DroppedDown)
            {
                ArrowBrush = new SolidBrush(SystemColors.HighlightText);
            }
            else
            {
                ArrowBrush = new SolidBrush(SystemColors.ControlText);
            }

            //Draw the arrow
            g.FillPath(ArrowBrush, pth);

        }
    }
}
