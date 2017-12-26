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
        public VideoForm()
        {
            InitializeComponent();
            EnumerateVideoDevices();
            this.videoSourcePlayer.Paint += VideoSourcePlayer_Paint;
            this.videoSourcePlayer.NewFrame += VideoSourcePlayer_NewFrame;
        }

        private void VideoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawString("Augmented reality?", new Font("Arial", 16), Brushes.Black, new Rectangle(10, 10, 200, 50));
                g.Dispose();
            }
        }

        private void VideoSourcePlayer_Paint(object sender, PaintEventArgs e)
        {
            using (Pen p = new Pen(Color.Yellow, 1.5f))
            {
                e.Graphics.DrawLine(p, 0, this.videoSourcePlayer.Height / 2, videoSourcePlayer.Width, this.videoSourcePlayer.Height / 2);
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (videoDevice != null)
            {
                if (videoCapabilities != null && videoCapabilities.Length != 0)
                {
                    var size = (Size)this.comboBoxModes.SelectedItem;
                    videoDevice.VideoResolution = videoCapabilities.FirstOrDefault(c => { return c.FrameSize == size; });
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
    }
}
