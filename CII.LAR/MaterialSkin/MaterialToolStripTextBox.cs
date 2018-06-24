using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    public class MaterialToolStripTextBox : ToolStripTextBox, IMaterialControl
    {
        #region Field
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private ControlState _state = ControlState.Normal;
        private Font _defaultFont = new Font("微软雅黑", 9);

        //当Text属性为空时编辑框内出现的提示文本
        private string _emptyTextTip;
        private Color _emptyTextTipColor = Color.DarkGray;

        #endregion

        #region Constructor

        public MaterialToolStripTextBox()
        {
            SetStyles();
            //this.Font = _defaultFont;
            this.Font = SkinManager.PINGFANG_MEDIUM_9;
            //this.AutoSize = true;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(0x24, 0x28, 0x30);
        }

        #endregion

        #region Properites

        [Description("当Text属性为空时编辑框内出现的提示文本")]
        public String EmptyTextTip
        {
            get { return _emptyTextTip; }
            set
            {
                if (_emptyTextTip != value)
                {
                    _emptyTextTip = value;
                    base.Invalidate();
                }
            }
        }

        [Description("获取或设置EmptyTextTip的颜色")]
        public Color EmptyTextTipColor
        {
            get { return _emptyTextTipColor; }
            set
            {
                if (_emptyTextTipColor != value)
                {
                    _emptyTextTipColor = value;
                    base.Invalidate();
                }
            }
        }

        private int _radius = 3;
        [Description("获取或设置圆角弧度")]
        public int Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                this.Invalidate();
            }
        }

        [Description("获取或设置是否可自定义改变大小")]
        public bool CustomAutoSize
        {
            get { return this.AutoSize; }
            set { this.AutoSize = value; }
        }


        #endregion

        #region Override

        protected override void OnMouseEnter(EventArgs e)
        {
            _state = ControlState.Highlight;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (_state == ControlState.Highlight && Focused)
            {
                _state = ControlState.Focus;
            }
            else if (_state == ControlState.Focus)
            {
                _state = ControlState.Focus;
            }
            else
            {
                _state = ControlState.Normal;
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                _state = ControlState.Highlight;
            }
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                if (this.Bounds.Contains(mevent.Location))
                {
                    _state = ControlState.Highlight;
                }
                else
                {
                    _state = ControlState.Focus;
                }
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _state = ControlState.Normal;
            base.OnLostFocus(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
            {
                _state = ControlState.Normal;
            }
            else
            {
                _state = ControlState.Disabled;
            }
            base.OnEnabledChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Paint(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_defaultFont != null)
                {
                    _defaultFont.Dispose();
                }
            }

            _defaultFont = null;
            base.Dispose(disposing);
        }

        #endregion

        #region Private

        private void SetStyles()
        {
            // TextBox由系统绘制，不能设置 ControlStyles.UserPaint样式
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //UpdateStyles();
        }

        private void Paint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //g.SmoothingMode = SmoothingMode.AntiAlias;
            //去掉 TextBox 四个角
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            SetWindowRegion(this.Width, this.Height);

            if (!Enabled)
            {
                _state = ControlState.Disabled;
            }

            switch (_state)
            {
                case ControlState.Normal:
                    DrawNormalTextBox(g);
                    break;
                case ControlState.Highlight:
                    DrawHighLightTextBox(g);
                    break;
                case ControlState.Focus:
                    DrawFocusTextBox(g);
                    break;
                case ControlState.Disabled:
                    DrawDisabledTextBox(g);
                    break;
                default:
                    break;
            }

            if (Text.Length == 0 && !string.IsNullOrEmpty(EmptyTextTip) && !Focused)
            {
                TextRenderer.DrawText(g, EmptyTextTip, Font, this.Bounds, EmptyTextTipColor, GetTextFormatFlags(HorizontalAlignment.Center, RightToLeft == RightToLeft.Yes));
            }
        }

        private void DrawNormalTextBox(Graphics g)
        {
            using (Pen borderPen = new Pen(Color.LightGray))
            {
                //g.DrawRectangle(borderPen, new Rectangle(this.Bounds.X, this.Bounds.Y, this.Bounds.Width - 1, this.Bounds.Height - 1));
                g.DrawPath(borderPen, DrawHelper.DrawRoundRect(this.Bounds.X, this.Bounds.Y, this.Bounds.Width - 1, this.Bounds.Height - 1, _radius));
            }
        }

        private void DrawHighLightTextBox(Graphics g)
        {
            using (Pen highLightPen = new Pen(ColorTable.HighLightColor))
            {
                //Rectangle drawRect = new Rectangle(this.Bounds.X, this.Bounds.Y, this.Bounds.Width - 1, this.Bounds.Height - 1);
                //g.DrawRectangle(highLightPen, drawRect);

                g.DrawPath(highLightPen, DrawHelper.DrawRoundRect(this.Bounds.X, this.Bounds.Y, this.Bounds.Width - 1, this.Bounds.Height - 1, _radius));

                //InnerRect
                //drawRect.Inflate(-1, -1);
                //highLightPen.Color = ColorTable.HighLighInnertColor;
                //g.DrawRectangle(highLightPen, drawRect);

                g.DrawPath(new Pen(ColorTable.HighLighInnertColor), DrawHelper.DrawRoundRect(this.Bounds.X, this.Bounds.Y, this.Bounds.Width - 1, this.Bounds.Height - 1, _radius));
            }
        }

        private void DrawFocusTextBox(Graphics g)
        {
            using (Pen focusedBorderPen = new Pen(ColorTable.HighLighInnertColor))
            {
                //g.DrawRectangle(focusedBorderPen,new Rectangle(this.Bounds.X,this.Bounds.Y,this.Bounds.Width - 1, this.Bounds.Height - 1));
                g.DrawPath(focusedBorderPen, DrawHelper.DrawRoundRect(this.Bounds.X, this.Bounds.Y, this.Bounds.Width - 1, this.Bounds.Height - 1, _radius));
            }
        }

        private void DrawDownTextBox(Graphics g)
        {
            using (Pen focusedBorderPen = new Pen(ColorTable.HighLighInnertColor))
            {
                //g.DrawRectangle(focusedBorderPen,new Rectangle(this.Bounds.X,this.Bounds.Y,this.Bounds.Width - 1, this.Bounds.Height - 1));
                g.DrawPath(focusedBorderPen, DrawHelper.DrawRoundRect(this.Bounds.X, this.Bounds.Y, this.Bounds.Width - 1, this.Bounds.Height - 1, _radius));
            }
        }

        private void DrawDisabledTextBox(Graphics g)
        {
            using (Pen disabledPen = new Pen(SystemColors.ControlDark))
            {
                //g.DrawRectangle(disabledPen,new Rectangle( this.Bounds.X,this.Bounds.Y, this.Bounds.Width - 1,this.Bounds.Height - 1));
                g.DrawPath(disabledPen, DrawHelper.DrawRoundRect(this.Bounds.X, this.Bounds.Y, this.Bounds.Width - 1, this.Bounds.Height - 1, _radius));
            }
        }

        private static TextFormatFlags GetTextFormatFlags(HorizontalAlignment alignment, bool rightToleft)
        {
            TextFormatFlags flags = TextFormatFlags.WordBreak |
                TextFormatFlags.SingleLine;
            if (rightToleft)
            {
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }

            switch (alignment)
            {
                case HorizontalAlignment.Center:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
                case HorizontalAlignment.Left:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case HorizontalAlignment.Right:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;
            }
            return flags;
        }

        #endregion

        public void SetWindowRegion(int width, int height)
        {
            //System.Drawing.Drawing2D.GraphicsPath FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            //Rectangle rect = new Rectangle(0, 0, width, height);
            //FormPath = GetRoundedRectPath(rect, _radius);
            //this.Region = new Region(FormPath);
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            //   左上角      
            path.AddArc(arcRect, 180, 90);
            //   右上角      
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            //   右下角      
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            //   左下角      
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
