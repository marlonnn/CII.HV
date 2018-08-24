using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using CII.LAR.MaterialSkin;

namespace CII.LAR.UI
{
    /// <summary>
    /// Custom base control
    /// Author: Zhong Wen 2017/08/05
    /// </summary>
    public partial class BaseCtrl : UserControl, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public delegate void VideoKeyDown(KeyEventArgs e);
        public VideoKeyDown VideoKeyDownHandler;

        public delegate void UpdateSliderValue(float value);
        public UpdateSliderValue UpdateSliderValueHandler;

        protected ComponentResourceManager resources;

        /// <summary>
        /// title font
        /// </summary>
        protected Font font;

        /// <summary>
        /// title of this contrl
        /// </summary>
        protected string title = "Title";
        [Description("Title of this control"), Category("Appearance"), DefaultValue("Title")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        private int showIndex;
        public int ShowIndex
        {
            get
            {
                return showIndex;
            }
            set
            {
                this.showIndex = value;
            }
        }

        private CtrlType ctrlType;
        public CtrlType CtrlType
        {
            get { return this.ctrlType; }
            set
            {
                this.ctrlType = value;
            }
        }

        private Rectangle _statusBarBounds;
        private const int STATUS_BAR_HEIGHT = 20;
        private const int STATUS_BAR_BUTTON_WIDTH = STATUS_BAR_HEIGHT;
        private Rectangle _xButtonBounds;
        public BaseCtrl()
        {
            InitializeComponent();
            this.Load += BaseCtrl_Load;
            _statusBarBounds = new Rectangle(0, 0, Width, STATUS_BAR_HEIGHT);
            this.font = SkinManager.PINGFANG_MEDIUM_10;
            this.closeButton.Location = new Point(this.Width - this.closeButton.Width, (int)((STATUS_BAR_HEIGHT - this.closeButton.Size.Height) / 2f));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.closeButton.Location = new Point(this.Width - this.closeButton.Width, (int)((STATUS_BAR_HEIGHT - this.closeButton.Size.Height) / 2f));
            _statusBarBounds = new Rectangle(0, 0, Width, STATUS_BAR_HEIGHT);
            _xButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
        }

        private void BaseCtrl_Load(object sender, EventArgs e)
        {
            //if (Program.SysConfig != null)
            //{
            //    Program.SysConfig.PropertyChanged += SysConfig_PropertyChanged;
            //}
        }

        private void SysConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Program.SysConfig.GetPropertyName(() => Program.SysConfig.UICulture))
            {
                RefreshUI();
            }
        }

        public virtual void RefreshUI()
        {
            if (resources == null) return;
            foreach (var item in this.Controls)
            {
                MaterialGroupBox mgb = item as MaterialGroupBox;
                if (mgb != null)
                {
                    resources.ApplyResources(mgb, mgb.Name);
                    foreach (var subItem in mgb.Controls)
                    {
                        MaterialRoundButton subMrb = subItem as MaterialRoundButton;
                        if (subMrb != null)
                        {
                            resources.ApplyResources(subMrb, subMrb.Name);
                        }
                        MaterialLabel SubMl = subItem as MaterialLabel;
                        if (SubMl != null)
                        {
                            resources.ApplyResources(SubMl, SubMl.Name);
                        }
                    }
                }
                MaterialRoundButton mrb = item as MaterialRoundButton;
                if (mrb != null)
                {
                    resources.ApplyResources(mrb, mrb.Name);
                }
                MaterialLabel ml = item as MaterialLabel;
                if (ml != null)
                {
                    resources.ApplyResources(ml, ml.Name);
                }
            }
        }

        protected virtual void closeButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Enabled = false;
        }

        /// <summary>
        /// paint border and title
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            using (SolidBrush sb = new SolidBrush(SkinManager.BaseCtrlTitleColor))
            {
                g.FillRectangle(sb, _statusBarBounds);
                using (Pen pen = new Pen(SkinManager.BaseCtrlTitleColor, 1.0f))
                    g.DrawRectangle(pen, new Rectangle(1, 1, this.Width - 2, this.Height - 2));
            }

            //ADB8D0
            SizeF size = g.MeasureString(Title, font);
            using (SolidBrush sb = new SolidBrush(SkinManager.BaseCtrlTitleTextColor))
                e.Graphics.DrawString(Title, font, sb, 0, (_statusBarBounds.Height - size.Height) / 2f);
        }

        /// <summary>
        /// 初始化控件位置
        /// </summary>
        /// <param name="size"></param>
        public virtual void InitializeLocation(Size size)
        {
            this.Location = new Point(size.Width - this.Width - 20, 30);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (DelegateClass.GetDelegate().VideoKeyUpHandler != null)
            {
                DelegateClass.GetDelegate().VideoKeyUpHandler(e);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (DelegateClass.GetDelegate().VideoKeyDownHandler != null)
            {
                DelegateClass.GetDelegate().VideoKeyDownHandler(e);
            }
        }
    }
}
