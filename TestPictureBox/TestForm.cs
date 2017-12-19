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
    public partial class TestForm : Form
    {
        private FullScreen fullScreen;

        public TestForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Load += TestForm_Load;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            fullScreen = new FullScreen(this);
            fullScreen.ShowFullScreen();
            this.richPictureBox.FullScreen = fullScreen;
            this.richPictureBox.LoadImage();
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
