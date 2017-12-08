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
        private Camera camera;
        private FullScreen fullScreen;
        private FormWindowState tempWindowState;

        public EntryForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            camera = new IDSCamera(this.zwPictureBox.Handle);
            this.SizeChanged += EntryForm_SizeChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            fullScreen = new FullScreen(this);
            this.zwPictureBox.EscapeFullScreenHandler += EscapeFullScreenHandler;
            fullScreen.ShowFullScreen();
        }

        private void CheckCameraAndSetSize()
        {
            //if (camera.InitCamera())
            {
                //seccuss
                //Set camera capture size

            }
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

                }
            }
        }
    }
}
