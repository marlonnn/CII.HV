using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPictureBox
{
    public partial class RichPictureBox : UserControl
    {
        private FullScreen fullScreen;
        public FullScreen FullScreen
        {
            get { return this.fullScreen; }
            set { this.fullScreen = value; }
        }

        private Image pictureImage;
        public Image PictureImage
        {
            get { return this.pictureImage; }
            set { this.pictureImage = value; }
        }

        private float zoom = 1;
        public float Zoom
        {
            get { return this.zoom; }
            set { this.zoom = value; }
        }

        /// <summary>
        /// 图片或者视频初始化大小
        /// </summary>
        private Size mediaSize;
        public Size MediaSize
        {
            get { return this.mediaSize; }
            set { this.mediaSize = value; }
        }

        private Point centerPoint;
        public Point CenterPoint
        {
            get { return this.centerPoint; }
            set { this.centerPoint = value; }
        }

        private int clickCount = 0;
        private Point startPoint;
        private Point endPoint;
        private List<DrawLine> lines;

        public RichPictureBox()
        {
            InitializeComponent();
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            this.Load += RichPictureBox_Load;
            this.topPanel.MouseDown += TopPanel_MouseDown;
            this.topPanel.MouseMove += TopPanel_MouseMove;
            this.topPanel.MouseUp += TopPanel_MouseUp;
            this.topPanel.MouseWheel += TopPanel_MouseWheel;
            this.topPanel.Paint += TopPanel_Paint;
            lines = new List<DrawLine>();
        }

        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {
             Graphics graphics = e.Graphics;
             
             graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
             graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
             graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
             graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
             graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            if (lines != null && lines.Count > 0)
            {
                foreach (var line in lines)
                {
                    line.Draw(graphics, this.pictureBox);
                }
            }
        }

        private void TopPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            float oldzoom = Zoom;
            if (e.Delta > 0)
            {
                Zoom += 0.5F;
            }
            else if (e.Delta < 0)
            {
                if (Zoom > 1)
                {
                    Zoom = Math.Max(Zoom - 0.5F, 0.01F);
                }
            }
            var deltaX = e.Location.X - this.CenterPoint.X;
            var deltaY = e.Location.Y - this.CenterPoint.Y;
            SetPictureBoxBounds(Zoom, deltaX, deltaY);
        }

        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickCount % 2 == 0)
            {
                endPoint = new Point(e.X, e.Y);
                this.topPanel.Invalidate();
            }
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickCount % 2 == 1)
            {
                Point point = new Point(e.X, e.Y);
                lines[lines.Count - 1].MoveHandleTo(point, 2);
                this.topPanel.Invalidate();
            }
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("picture box location X : {0},  Y : {1}", e.Location.X, e.Location.Y);
            var pointToScreen = this.pictureBox.PointToScreen(e.Location);
            Console.WriteLine("point to screen X : {0},  Y : {1}", pointToScreen.X, pointToScreen.Y);
            clickCount++;
            if (clickCount % 2 == 1)
            {
                startPoint = new Point(e.X, e.Y);
                DrawLine drawObject = new DrawLine(pictureBox, startPoint.X, startPoint.Y, startPoint.X + 1, startPoint.Y + 1);
                lines.Add(drawObject);
                this.topPanel.Invalidate();
            }
        }

        private void RichPictureBox_Load(object sender, EventArgs e)
        {

        }

        public void LoadImage()
        {
            string fileName = string.Format("{0}\\Sperm.bmp", System.Environment.CurrentDirectory);
            PictureImage = Image.FromFile(fileName);
            MediaSize = new Size(PictureImage.Width, PictureImage.Height);
            this.pictureBox.Image = PictureImage;
            SetPictureBoxBounds(Zoom, 0, 0);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.topPanel.Bounds = this.Bounds;
            SetPictureBoxBounds(Zoom, 0, 0);
            CenterPoint = new Point(this.Width / 2, this.Height / 2);
        }

        private void SetPictureBoxBounds(float zoom, int deletaX, int deltaY)
        {
            if (this.MediaSize != Size.Empty)
            {
                int newWidth = (int)(this.MediaSize.Width * zoom);
                int newHeight = (int)(this.MediaSize.Height * zoom);
                if (newWidth < this.Width || newHeight < this.Height)
                {
                    deletaX = 0;
                    deltaY = 0;
                }
                this.pictureBox.Bounds = new Rectangle((this.Width - newWidth) / 2 + deletaX, (this.Height - newHeight) / 2 + deltaY, newWidth, newHeight);
                this.pictureBox.Invalidate();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                FullScreen.ResetFullScreen();
            }
            else if (e.KeyCode == Keys.F)
            {
                FullScreen.ShowFullScreen();
            }
        }
    }
}
