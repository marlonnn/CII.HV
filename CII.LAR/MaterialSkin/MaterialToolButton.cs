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
    public enum ButtonType
    {
        Max,
        Min,
        Close
    }
    public class MaterialToolButton : Button, IMaterialControl
    {
        private ButtonType btnType;

        [Description("Icon"), Category("MaterialToolButton"), DefaultValue(typeof(ButtonType), "ButtonType.Normal")]
        public ButtonType BtnType
        {
            get { return this.btnType; }
            set
            {
                if (value != this.btnType)
                {
                    this.btnType = value;
                    InvokeInvalidate(value);
                    Invalidate();
                }
            }
        }

        private void InvokeInvalidate(ButtonType value)
        {
            if (!IsHandleCreated)
                return;
            try
            {
                this.Invoke((MethodInvoker)delegate { this.btnType = value; });
            }
            catch { }
        }
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public bool Primary { get; set; }

        protected readonly AnimationManager _animationManager;
        protected readonly AnimationManager _hoverAnimationManager;

        private SizeF _textSize;

        private Image _icon;
        public Image Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

        private const int STATUS_BAR_HEIGHT = 32;
        private const int STATUS_BAR_BUTTON_WIDTH = STATUS_BAR_HEIGHT;
        private const int ACTION_BAR_HEIGHT = 40;
        private Rectangle _minButtonBounds;
        private Rectangle _maxButtonBounds;
        private Rectangle _xButtonBounds;
        private Rectangle _statusBarBounds;
        private Rectangle _actionBarBounds;

        public MaterialToolButton()
        {
            Primary = false;

            _animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            _hoverAnimationManager = new AnimationManager
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            };

            _hoverAnimationManager.OnAnimationProgress += sender => Invalidate();
            _animationManager.OnAnimationProgress += sender => Invalidate();

            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoSize = false;
            Margin = new Padding(4, 6, 4, 6);
            Padding = new Padding(0);


            _minButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - 3 * STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            _maxButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - 2 * STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            _xButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            _statusBarBounds = new Rectangle(0, 0, Width, STATUS_BAR_HEIGHT);
            _actionBarBounds = new Rectangle(0, STATUS_BAR_HEIGHT, Width, ACTION_BAR_HEIGHT);
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                _textSize = CreateGraphics().MeasureString(value.ToUpper(), SkinManager.ROBOTO_MEDIUM_10);
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(Parent.BackColor);

            //Hover
            Color c = SkinManager.GetFlatButtonHoverBackgroundColor();
            using (Brush b = new SolidBrush(/*Color.FromArgb((int)(_hoverAnimationManager.GetProgress() * c.A), c.RemoveAlpha())*/c))
                g.FillRectangle(b, ClientRectangle);

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

            //Icon
            var iconRect = new Rectangle(1, 1, 20, 20);

            if (string.IsNullOrEmpty(Text))
                // Center Icon
                iconRect.X += 2;

            if (Icon != null)
                g.DrawImage(Icon, iconRect);

            //Text
            var textRect = ClientRectangle;

            using (var formButtonsPen = new Pen(SkinManager.ACTION_BAR_TEXT_SECONDARY, 2))
            {
                switch (BtnType)
                {
                    case ButtonType.Max:
                        int x = _maxButtonBounds.X;
                        int y = _maxButtonBounds.Y;
                        g.DrawRectangle(
                            formButtonsPen,
                            /*_maxButtonBounds.X + */(int)(this.Width * 0.33),
                            /*_maxButtonBounds.Y + */(int)(this.Height * 0.36),
                            (int)(this.Width * 0.39),
                            (int)(this.Height * 0.31)
                       );
                        break;
                    case ButtonType.Close:
                        g.DrawLine(
                            formButtonsPen,
                            /*_xButtonBounds.X + */(int)(this.Width * 0.33),
                            /*_xButtonBounds.Y + */(int)(this.Height * 0.33),
                            /*_xButtonBounds.X + */(int)(this.Width * 0.66),
                            /*_xButtonBounds.Y + */(int)(this.Height * 0.66)
                       );

                        g.DrawLine(
                            formButtonsPen,
                            /*_xButtonBounds.X + */(int)(this.Width * 0.66),
                            /*_xButtonBounds.Y + */(int)(this.Height * 0.33),
                            /*_xButtonBounds.X + */(int)(this.Width * 0.33),
                            /*_xButtonBounds.Y + */(int)(this.Height * 0.66));
                break;
                    case ButtonType.Min:
                        int xMin = _minButtonBounds.X;
                        int yMin = _minButtonBounds.Y;
                        g.DrawLine(
                            formButtonsPen, 4, 11, 18, 11);
                        break;
                }
            }
            using (SolidBrush sb = new SolidBrush(SkinManager.FontColor))
            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.DrawString(Text.ToUpper(), SkinManager.PINGFANG_MEDIUM_10, sb, textRect, sf);
            }
        }



        private Size GetPreferredSize()
        {
            return GetPreferredSize(new Size(0, 0));
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            // Provides extra space for proper padding for content
            var extra = 16;

            if (Icon != null)
                // 24 is for icon size
                // 4 is for the space between icon & text
                extra += 24 + 4;

            return new Size((int)Math.Ceiling(_textSize.Width) + extra, 36);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode) return;

            MouseState = MouseState.OUT;
            MouseEnter += (sender, args) =>
            {
                MouseState = MouseState.HOVER;
                _hoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                _hoverAnimationManager.StartNewAnimation(AnimationDirection.Out);
                Invalidate();
            };
            MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    MouseState = MouseState.DOWN;

                    _animationManager.StartNewAnimation(AnimationDirection.In, args.Location);
                    Invalidate();
                }
            };
            MouseUp += (sender, args) =>
            {
                MouseState = MouseState.HOVER;

                Invalidate();
            };
        }
    }
}
