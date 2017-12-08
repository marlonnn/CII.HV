using CII.LAR.Opertion;
using CII.LAR.SysClass;
using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR
{
    public partial class EntryForm : Form
    {
        private IDSCamera camera;
        private FullScreen fullScreen;
        private FormWindowState tempWindowState;

        private float zoom = 1;
        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
            }
        }

        public EntryForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            camera = new IDSCamera(this.zwPictureBox);
            camera.CameraSizeControl.AOIChanged += OnDisplayChanged;
            this.SizeChanged += EntryForm_SizeChanged;
            this.MouseWheel += EntryForm_MouseWheel;
        }

        private void EntryForm_MouseWheel(object sender, MouseEventArgs e)
        {
            var value = e.Delta;
            float oldzoom = zoom;
            if (e.Delta > 0)
            {
                zoom += 1F;
            }
            else if (e.Delta < 0)
            {
                zoom = Math.Max(zoom - 1F, 0.01F);
            }
            int width = (int)(1392 * zoom);
            int height = (int)(1080 * zoom);
            this.zwPictureBox.Bounds = new Rectangle((1920 - width) / 2, (1080 - height) / 2, width, height);
            this.zwPictureBox.Invalidate();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            fullScreen = new FullScreen(this);
            fullScreen.ShowFullScreen();
            this.zwPictureBox.EscapeFullScreenHandler += EscapeFullScreenHandler;
            this.zwPictureBox.OnLoad();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            camera.ExitCamera();
        }

        private void EscapeFullScreenHandler()
        {
            fullScreen.ShowFullScreen();
        }

        private void EntryForm_SizeChanged(object sender, EventArgs e)
        {
            if (tempWindowState != FormWindowState.Maximized && this.WindowState == FormWindowState.Maximized)//点击最大化
            {
                tempWindowState = FormWindowState.Maximized;
                if (fullScreen != null)
                {
                    fullScreen.ShowFullScreen();
                }
            }
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

        private void openCameraLiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CameraChooseForm chooseForm = new CameraChooseForm();
            if (chooseForm.ShowDialog() == DialogResult.OK)
            {
                if (camera.InitCamera(chooseForm.DeviceID | (Int32)uEye.Defines.DeviceEnumeration.UseDeviceID))
                {
                    SetCameraSize();
                    camera.DisplayLive();
                }
            }
        }

        private void SetCameraSize()
        {
            if (camera != null && camera.IsOpened())
            {
                camera.CameraSizeControl.SetAoiBounds(1392, 1080, (this.zwPictureBox.Width - 1392) / 2, 0);
            }
        }

        private void OnDisplayChanged(object sender, EventArgs e)
        {
            // get image size
            System.Drawing.Rectangle rect;
            camera.SetSize(out rect);
            this.zwPictureBox.Bounds = rect;
        }
    }
}
