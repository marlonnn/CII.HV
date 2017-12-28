using AForge.Video.DirectShow;
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
    public partial class VideoForm : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;

        private VideoCapabilities[] videoCapabilities;

        private int clickCount = 0;
        private Point startPoint;
        private Point endPoint;
        private List<DrawLine> lines;

        private float zoom = 1;
        public float Zoom
        {
            get { return this.zoom; }
            set { this.zoom = value; }
        }

        private Point centerPoint;
        public Point CenterPoint
        {
            get { return this.centerPoint; }
            set { this.centerPoint = value; }
        }

        public VideoForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            EnumerateVideoDevices();
            this.videoSourcePlayer.Paint += VideoSourcePlayer_Paint;
            this.videoSourcePlayer.NewFrame += VideoSourcePlayer_NewFrame;
            this.videoSourcePlayer.MouseDown += TopPanel_MouseDown;
            this.videoSourcePlayer.MouseMove += TopPanel_MouseMove;
            this.videoSourcePlayer.MouseUp += TopPanel_MouseUp;
            lines = new List<DrawLine>();
        }

        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickCount % 2 == 0)
            {
                endPoint = new Point(e.X, e.Y);
            }
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickCount % 2 == 1)
            {
                Point point = new Point(e.X, e.Y);
                lines[lines.Count - 1].MoveHandleTo(point, 2);
            }
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("picture box location X : {0},  Y : {1}", e.Location.X, e.Location.Y);
            //var pointToScreen = this.pictureBox.PointToScreen(e.Location);
            //Console.WriteLine("point to screen X : {0},  Y : {1}", pointToScreen.X, pointToScreen.Y);
            clickCount++;
            if (clickCount % 2 == 1)
            {
                startPoint = new Point(e.X, e.Y);
                DrawLine drawObject = new DrawLine(null, startPoint.X, startPoint.Y, startPoint.X + 1, startPoint.Y + 1);
                lines.Add(drawObject);
                //this.videoSourcePlayer.Invalidate();
            }
        }

        private void VideoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            //using (Graphics g = Graphics.FromImage(image))
            //{
            //    g.DrawString("Augmented reality?", new Font("Arial", 16), Brushes.Black, new Rectangle(10, 10, 200, 50));
            //    g.Dispose();
            //}
        }

        private void VideoSourcePlayer_Paint(object sender, PaintEventArgs e)
        {
            using (Pen p = new Pen(Color.Yellow, 1.5f))
            {
                e.Graphics.DrawLine(p, 0, this.videoSourcePlayer.Height / 2, videoSourcePlayer.Width, this.videoSourcePlayer.Height / 2);

                if (lines != null && lines.Count > 0)
                {
                    foreach (var line in lines)
                    {
                        line.Draw(e.Graphics, null);
                    }
                }
            }
        }

        private void EnumerateVideoDevices()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices != null && videoDevices.Count > 0)
            {
                foreach (var device in videoDevices)
                {
                    var filterInfo = device as FilterInfo;
                    if (filterInfo != null)
                        this.comboBoxSources.Items.Add(filterInfo.Name);
                }
            }
            else
            {
                this.comboBoxSources.Items.Add("No DirectShow devices found");
            }
            this.comboBoxSources.SelectedIndex = 0;
        }


        private void EnumerateVideoModes(VideoCaptureDevice device)
        {
            this.comboBoxModes.Items.Clear();

            videoCapabilities = device.VideoCapabilities;

            foreach (var capability in videoCapabilities)
            {
                if (!this.comboBoxModes.Items.Contains(capability.FrameSize))
                {
                    this.comboBoxModes.Items.Add(capability.FrameSize);
                }
            }
            if (videoCapabilities.Length == 0)
            {
                this.comboBoxModes.Items.Add("Not supported");
            }
            //this.comboBoxModes.SelectedIndex = 0;
        }

        private void comboBoxSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            videoDevice = new VideoCaptureDevice(videoDevices[this.comboBoxSources.SelectedIndex].MonikerString);
            EnumerateVideoModes(videoDevice);
        }

        private Size videoSize;
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (videoDevice != null)
            {
                if (videoCapabilities != null && videoCapabilities.Length != 0)
                {
                    var size = (Size)this.comboBoxModes.SelectedItem;
                    videoSize = new Size(size.Width * 2, size.Height * 2);
                    videoDevice.VideoResolution = videoCapabilities.FirstOrDefault(c => { return c.FrameSize == size; });
                    this.videoSourcePlayer.Bounds = new Rectangle( (this.Width - videoSize.Width) / 2, (this.Height - videoSize.Height) / 2, videoSize.Width, videoSize.Height);
                }
                this.videoSourcePlayer.VideoSource = videoDevice;
                this.videoSourcePlayer.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.videoSourcePlayer.VideoSource != null)
            {
                this.videoSourcePlayer.SignalToStop();
                this.videoSourcePlayer.WaitForStop();
                this.videoSourcePlayer.VideoSource = null;
            }
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            Zoom += 0.1f;
            Size size = Size.Ceiling(new SizeF(videoSize.Width * Zoom, videoSize.Height * Zoom));
            this.videoSourcePlayer.Bounds = new Rectangle((this.Width - size.Width) / 2, (this.Height - size.Height) / 2, size.Width, size.Height);
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            Zoom -= 0.1f;
            Size size =  Size.Ceiling(new SizeF(videoSize.Width * Zoom, videoSize.Height * Zoom));
            this.videoSourcePlayer.Bounds = new Rectangle((this.Width - size.Width) / 2, (this.Height - size.Height) / 2, size.Width, size.Height);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CenterPoint = new Point(this.Width / 2, this.Height / 2);
        }
    }
}
