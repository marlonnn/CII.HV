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

        private MaterialToolButton btnClose;
        private MaterialToolButton btnMin;
        private MaterialToolButton btnMax;

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
            this.btnMax = new CII.LAR.MaterialSkin.MaterialToolButton();
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
            this.btnMin.Location = new System.Drawing.Point(450, 4);
            this.btnMin.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnMin.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnMin.Name = "btnMin";
            this.btnMin.Primary = false;
            this.btnMin.Size = new System.Drawing.Size(22, 22);
            this.btnMin.TabIndex = 1;
            this.btnMin.UseVisualStyleBackColor = true;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMax.BtnType = CII.LAR.MaterialSkin.ButtonType.Max;
            this.btnMax.Depth = 0;
            this.btnMax.Icon = null;
            this.btnMax.Location = new System.Drawing.Point(450, 4);
            this.btnMax.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnMax.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnMax.Name = "btnMax";
            this.btnMax.Primary = false;
            this.btnMax.Size = new System.Drawing.Size(22, 22);
            this.btnMax.TabIndex = 1;
            this.btnMax.UseVisualStyleBackColor = true;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
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
            //this.Controls.Add(this.btnMax);
            this.Controls.Add(this.btnClose);
            this.Name = "MaterialTitleBar";
            this.Size = new System.Drawing.Size(500, 32);
            this.ResumeLayout(false);

        }

        public MaterialTitleBar()
        {
            InitializeComponent();
            //_statusBarBounds = new Rectangle(0, 0, Width, STATUS_BAR_HEIGHT);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            //g.Clear(SkinManager.GetApplicationBackgroundColor());
            //g.FillRectangle(SkinManager.ColorScheme.DarkPrimaryBrush, _statusBarBounds);

            if (Icon != null)
            {
                var iconRect = new Rectangle(8, 4, 24, 24);
                g.DrawImage(Icon.ToBitmap(), iconRect);
            }
            //Form title
            using (StringFormat sf = new StringFormat { LineAlignment = StringAlignment.Center })
            {
                g.DrawString(Text, SkinManager.PINGFANG_MEDIUM_16, SkinManager.ColorScheme.TextBrush, 
                    new Rectangle(SkinManager.FORM_PADDING + 32, 0, Width, STATUS_BAR_HEIGHT), sf);
            }
        }

        private const int STATUS_BAR_HEIGHT = 32;
        public EventHandler MaxHandler;
        public EventHandler CloseHandler;
        public EventHandler MinHandler;
        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseHandler?.Invoke(sender, e);
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            MaxHandler?.Invoke(sender, e);
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            MinHandler?.Invoke(sender, e);
        }
    }
}
