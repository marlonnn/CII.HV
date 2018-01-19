using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.Commond;

namespace CII.LAR.UI
{
    public partial class DebugCtrl : UserControl
    {
        public delegate void VideoKeyDown(KeyEventArgs e);
        public VideoKeyDown VideoKeyDownHandler;

        public DebugCtrl()
        {
            InitializeComponent();
        }

        public void UpdateSteps(MotorC40Response m40r)
        {
            this.m1Steps.Text = m40r.Motor1Steps.ToString();
            this.m2Steps.Text = m40r.Motor2Steps.ToString();
        }

        public void UpdateResponseCode(MotorC60Response m60r)
        {
            this.responseCode.Text = m60r.GetResponseCode();
        }

        private void DebugCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (VideoKeyDownHandler != null)
            {
                VideoKeyDownHandler(e);
            }
        }
    }
}
