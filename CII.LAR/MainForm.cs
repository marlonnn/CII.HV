using CII.LAR.MaterialSkin;
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
    public partial class MainForm : MaterialForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.panel1.Size = new Size(this.panel1.Size.Width, this.Height - 32);
        }

        #region ToolStrip button click
        private void toolstripBtnScreenShort_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnVideo_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnFiles_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnMeasure_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnLine_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnRectangle_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnEllipse_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnHand_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnZoomIn_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnZoomOut_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnFit_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnLaser_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnSetting_Click(object sender, EventArgs e)
        {
        }

        private void toolstripBtnCamera_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnDebug_Click(object sender, EventArgs e)
        {

        }

        private void toolstripBtnAbout_Click(object sender, EventArgs e)
        {

        } 
        #endregion
    }
}
