using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.ExpClass;

namespace CII.LAR.UI
{
    public partial class ReportCtrl : UserControl
    {
        private enum SelectionMode
        {
            None,
            NetSelection,   // group selection is active
            Move,           // object(s) are moves
            Size            // object is resized
        }
        public const int BORDER_SIZE = 6; //调整大小触模柄方框大小，也等于边框的大小  

        private double scaleFactor = 1;

        public double ScaleFactor
        {
            get { return scaleFactor; }
            set { scaleFactor = value; }
        }

        public bool Scaling
        {
            get { return Math.Abs(scaleFactor - 1) > 0.000001; }
        }

        protected Control subCtrl;

        protected bool isActive;

        public virtual bool IsActive
        {
            set
            {
                isActive = value;
                if (this.ParentForm == null)
                {
                    return;
                }
            }
            get
            {
                return this.isActive;
            }
        }

        protected Rectangle oldBounds;

        public Rectangle Oldbounds
        {
            get
            {
                return oldBounds;
            }
        }

        protected Color borderColor = Color.Black;

        protected bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                this.isSelected = value;
                if (this.ParentForm == null)
                {
                    return;
                }
                if (!isSelected && IsActive)
                {
                    IsActive = false;
                }

            }
        }
        public bool IsPrint { set; get; }
        public virtual void Scale(double factor)
        {
            if (factor < 0) return;

            ScaleFactor = factor;

            // hide sub ctrl in scaling mode
            subCtrl.Visible = !Scaling;

            // set ctrl not active
            if (Scaling) IsActive = false;
        }

        public ReportItemBase ReportItem
        {
            get;
            protected set;
        }

        protected ReportCtrl()
        {
            InitializeComponent();
        }

        public ReportCtrl(ReportItemBase reportItem) : this()
        {
            ReportItem = reportItem;
            AllowDrop = true;
            this.Load += new EventHandler(ReportCtrl_Load);
            this.MouseMove += new MouseEventHandler(ReportCtrl_MouseMove);
            this.MouseDown += new MouseEventHandler(ReportCtrl_MouseDown);
            this.MouseUp += new MouseEventHandler(ReportCtrl_MouseUp);
        }

        private void ReportCtrl_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void ReportCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            //Point point = new Point(e.X, e.Y);
            //int n = HitTest(point);
        }

        private void ReportCtrl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                oldBounds = this.Bounds;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.GetType() == typeof(ReportCtrlPicture))
            {
                this.subCtrl.Visible = false;
                e.Graphics.ScaleTransform((float)ScaleFactor, (float)ScaleFactor);
                Draw(e.Graphics, ClientRectangle);
                e.Graphics.ResetTransform();
            }
            else
            {
                if (Scaling)
                {
                    e.Graphics.ScaleTransform((float)ScaleFactor, (float)ScaleFactor);
                    Draw(e.Graphics, ClientRectangle);
                    e.Graphics.ResetTransform();
                }
            }
            PaintBorder(e.Graphics);
        }

        protected void PaintBorder(Graphics g)
        {
            if (IsSelected)
            {
                Rectangle rect = new Rectangle(new Point(BORDER_SIZE / 2, BORDER_SIZE / 2), 
                    new Size(this.Width - BORDER_SIZE, this.Height - BORDER_SIZE));
                using (Pen pen = new Pen(borderColor))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                    pen.DashPattern = new float[] { 4, 4 };
                    g.DrawRectangle(pen, rect);
                }
            }
        }

        protected virtual void ReportCtrl_Load(object sender, EventArgs e)
        {
        }

        public virtual void Draw(Graphics g, Rectangle bounds)
        {
        }

        protected virtual void SetSubCtrl()
        {
            subCtrl.Enabled = false;
            subCtrl.Width = ReportItem.Bounds.Width - BORDER_SIZE * 2;
            subCtrl.Height = ReportItem.Bounds.Height - BORDER_SIZE * 2;
            this.Bounds = ReportItem.Bounds;
            subCtrl.Location = new Point(BORDER_SIZE, BORDER_SIZE);
            // do not anchor sub ctrl, size it manually
            //_subctrl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.Controls.Add(subCtrl);
        }

        public void ResetSubCtrlLocation()
        {
            if (subCtrl == null) return;
            subCtrl.Location = new Point(BORDER_SIZE, BORDER_SIZE);
        }
    }
}
