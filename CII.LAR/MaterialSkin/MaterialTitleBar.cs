using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    public class MaterialTitleBar : UserControl, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private const int STATUS_BAR_HEIGHT = 32;
        private const int STATUS_BAR_BUTTON_WIDTH = STATUS_BAR_HEIGHT;
        private const int ACTION_BAR_HEIGHT = 40;
        private Rectangle _minButtonBounds;
        private Rectangle _maxButtonBounds;
        private Rectangle _xButtonBounds;
        private Rectangle _statusBarBounds;
        private Rectangle _actionBarBounds;
        private MaterialToolButton btnClose;
        private MaterialToolButton btnMin;

        public bool MaximizeBox { get; set; }
        public bool MinimizeBox { get; set; }

        private Icon icon;
        [Description("Icon"), Category("MaterialTitleBar"), DefaultValue(typeof(Icon), "null")]
        public Icon Icon
        {
            get { return this.icon; }
            set
            {
                if (value != this.icon)
                {
                    this.icon = value;
                    InvokeInvalidate(value);
                    Invalidate();
                }
            }
        }


        private void InvokeInvalidate(Icon value)
        {
            if (!IsHandleCreated)
                return;
            try
            {
                this.Invoke((MethodInvoker)delegate { this.icon = value; });
            }
            catch { }
        }

        private void InitializeComponent()
        {
            this.btnMin = new CII.LAR.MaterialSkin.MaterialToolButton();
            this.btnClose = new CII.LAR.MaterialSkin.MaterialToolButton();
            this.SuspendLayout();
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMin.BtnType = CII.LAR.MaterialSkin.ButtonType.Min;
            this.btnMin.Depth = 0;
            this.btnMin.Icon = null;
            this.btnMin.Location = new System.Drawing.Point(453, 4);
            this.btnMin.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnMin.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnMin.Name = "btnMin";
            this.btnMin.Primary = false;
            this.btnMin.Size = new System.Drawing.Size(22, 22);
            this.btnMin.TabIndex = 1;
            this.btnMin.UseVisualStyleBackColor = true;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.BtnType = CII.LAR.MaterialSkin.ButtonType.Close;
            this.btnClose.Depth = 0;
            this.btnClose.Icon = null;
            this.btnClose.Location = new System.Drawing.Point(474, 4);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClose.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnClose.Name = "btnClose";
            this.btnClose.Primary = false;
            this.btnClose.Size = new System.Drawing.Size(22, 22);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MaterialTitleBar
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
            this.Controls.Add(this.btnMin);
            this.Controls.Add(this.btnClose);
            this.Name = "MaterialTitleBar";
            this.Size = new System.Drawing.Size(500, 32);
            this.ResumeLayout(false);

        }

        public MaterialTitleBar()
        {
            InitializeComponent();
            _statusBarBounds = new Rectangle(0, 0, Width, STATUS_BAR_HEIGHT);
            MaximizeBox = true;
            MinimizeBox = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _minButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - 3 * STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            _maxButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - 2 * STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            _xButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            _statusBarBounds = new Rectangle(0, 0, Width, STATUS_BAR_HEIGHT);
            _actionBarBounds = new Rectangle(0, STATUS_BAR_HEIGHT, Width, ACTION_BAR_HEIGHT);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(SkinManager.GetApplicationBackgroundColor());
            g.FillRectangle(SkinManager.ColorScheme.DarkPrimaryBrush, _statusBarBounds);

            bool showMin = MinimizeBox;
            bool showMax = MaximizeBox;

            using (var formButtonsPen = new Pen(SkinManager.ACTION_BAR_TEXT_SECONDARY, 2))
            {
                //// Minimize button.
                //if (showMin)
                //{
                //    int x = showMax ? _minButtonBounds.X : _maxButtonBounds.X;
                //    int y = showMax ? _minButtonBounds.Y : _maxButtonBounds.Y;

                //    g.DrawLine(
                //        formButtonsPen,
                //        x + (int)(_minButtonBounds.Width * 0.33),
                //        y + (int)(_minButtonBounds.Height * 0.66),
                //        x + (int)(_minButtonBounds.Width * 0.66),
                //        y + (int)(_minButtonBounds.Height * 0.66)
                //   );
                //}

                //// Maximize button
                //if (showMax)
                //{
                //    g.DrawRectangle(
                //        formButtonsPen,
                //        _maxButtonBounds.X + (int)(_maxButtonBounds.Width * 0.33),
                //        _maxButtonBounds.Y + (int)(_maxButtonBounds.Height * 0.36),
                //        (int)(_maxButtonBounds.Width * 0.39),
                //        (int)(_maxButtonBounds.Height * 0.31)
                //   );
                //}

                //// Close button
                ////if (ControlBox)
                //{
                //    g.DrawLine(
                //        formButtonsPen,
                //        _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.33),
                //        _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.33),
                //        _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.66),
                //        _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.66)
                //   );

                //    g.DrawLine(
                //        formButtonsPen,
                //        _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.66),
                //        _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.33),
                //        _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.33),
                //        _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.66));
                //}

                if (Icon != null)
                {
                    var iconRect = new Rectangle(8, 4, 24, 24);
                    g.DrawImage(Icon.ToBitmap(), iconRect);
                }

                //Form title

                g.DrawString(Text, SkinManager.PINGFANG_MEDIUM_16, SkinManager.ColorScheme.TextBrush,
                    new Rectangle(SkinManager.FORM_PADDING + 32, 0, Width, STATUS_BAR_HEIGHT),
                    new StringFormat { LineAlignment = StringAlignment.Center });
            }
        }

        public EventHandler CloseHandler;
        public EventHandler MinHandler;
        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseHandler?.Invoke(sender, e);
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            MinHandler?.Invoke(sender, e);
        }
    }
}
