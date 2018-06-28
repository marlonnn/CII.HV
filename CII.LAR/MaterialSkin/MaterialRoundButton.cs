using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    public class MaterialRoundButton : MaterialFlatButton
    {
        public MaterialRoundButton()
        {
            //this.ForeColor = SkinManager.GetLabelTextColor();
            this.Font = SkinManager.PINGFANG_MEDIUM_9;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(Parent.BackColor);

            //Hover
            Color c = SkinManager.GetFlatButtonHoverBackgroundColor();
            //using (Brush b = new SolidBrush(Color.FromArgb((int)(_hoverAnimationManager.GetProgress() * c.A), c.RemoveAlpha())))
            //    g.FillRectangle(b, ClientRectangle);

            //Ripple
            if (_animationManager.IsAnimating())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                for (var i = 0; i < _animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = _animationManager.GetProgress(i);
                    var animationSource = _animationManager.GetSource(i);

                    using (Brush rippleBrush = new SolidBrush(Color.FromArgb((int)(101 - (animationValue * 100)), Color.Black)))
                    {
                        var rippleSize = (int)(animationValue * Width * 2);
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                    }
                }
                g.SmoothingMode = SmoothingMode.None;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(this.ClientRectangle.X, this.ClientRectangle.Y, Bounds.Height - 1, Bounds.Height - 1, 90, 180);
            gp.AddLine(new Point(this.ClientRectangle.X + Bounds.Height / 2, this.ClientRectangle.Y), new Point(this.ClientRectangle.X + Bounds.Width - Bounds.Height / 2, this.ClientRectangle.Y));
            gp.AddArc(this.ClientRectangle.X + Bounds.Width - Bounds.Height, this.ClientRectangle.Y, Bounds.Height - 1, Bounds.Height - 1, 270, 180);
            gp.CloseAllFigures();
            using (Pen pen = new Pen(SkinManager.RoundButtonBorderColor, 1.5f))
                g.DrawPath(pen, gp);

            using (Brush b = new SolidBrush(Color.FromArgb((int)(_hoverAnimationManager.GetProgress() * c.A), c.RemoveAlpha())))
                g.FillPath(b, gp);
            gp.Dispose();

            //Icon
            var iconRect = new Rectangle(8, 6, 24, 24);

            if (string.IsNullOrEmpty(Text))
                // Center Icon
                iconRect.X += 2;

            if (Icon != null)
                g.DrawImage(Icon, iconRect);

            //Text
            var textRect = ClientRectangle;

            if (Icon != null)
            {
                //
                // Resize and move Text container
                //

                // First 8: left padding
                // 24: icon width
                // Second 4: space between Icon and Text
                // Third 8: right padding
                textRect.Width -= 8 + 24 + 4 + 8;

                // First 8: left padding
                // 24: icon width
                // Second 4: space between Icon and Text
                textRect.X += 8 + 24 + 4;
            }

            using (SolidBrush sb = new SolidBrush(SkinManager.GetLabelTextColor()))
            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.DrawString(Text, SkinManager.PINGFANG_MEDIUM_10, sb, textRect, sf );
            }
        }
    }
}
