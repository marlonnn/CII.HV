using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPictureBox
{
    public partial class Form1 : Form
    {
        private FullScreen fullScreen;
        public float Zoom = 1;
        private int clickCount = 0;

        private Point startPoint;
        private Point endPoint;
        private List<DrawLine> lines;
        //private DrawLine drawObject;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.WindowState = FormWindowState.Maximized;
            this.MouseWheel += Form_MouseWheel;
            lines = new List<DrawLine>();
            this.pictureBox.Paint += PictureBox_Paint;
            this.MouseDown += Form1_MouseDown;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("form location X : {0},  Y : {1}",  e.Location.X, e.Location.Y);
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (lines != null && lines.Count > 0)
            {
                foreach (var line in lines)
                {
                    line.Draw(e.Graphics, this.pictureBox);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fullScreen = new FullScreen(this);
            fullScreen.ShowFullScreen();
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            int width = (int)(1392 * Zoom);
            int height = (int)(1080 * Zoom);
            this.pictureBox.Bounds = new Rectangle((1920 - width) / 2, (1080 - height) / 2, width, height);
            LoadImage();
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
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
            int width = (int)(1392 * Zoom);
            int height = (int)(1080 * Zoom);
            this.pictureBox.Bounds = new Rectangle((1920 - width) / 2, (1080 - height) / 2, width, height);
            this.pictureBox.Invalidate();
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
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
                pictureBox.Invalidate();
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickCount % 2 == 1)
            {
                Point point = new Point(e.X, e.Y);
                lines[lines.Count - 1].MoveHandleTo(point, 2);
                pictureBox.Invalidate();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickCount % 2 == 0)
            {
                endPoint = new Point(e.X, e.Y);
                pictureBox.Invalidate();
            }
        }

        private void LoadImage()
        {
            string fileName = string.Format("{0}\\Sperm.bmp", System.Environment.CurrentDirectory);
            this.pictureBox.Image = Image.FromFile(fileName);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                fullScreen.ResetFullScreen();
            }
            else if (e.KeyCode == Keys.F)
            {
                fullScreen.ShowFullScreen();
            }
        }
    }
}
