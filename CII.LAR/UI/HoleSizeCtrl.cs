using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class HoleSizeCtrl : UserControl
    {
        public delegate void UpdownClick(bool isUp);
        public UpdownClick UpdownClickHandler;

        private double holeSize;
        public double HoleSize
        {
            get { return this.holeSize; }
            set
            {
                if (value != this.holeSize)
                {
                    this.holeSize = value;
                    string v = holeSize.ToString("0.00");
                    this.lblHoleSize.Text = string.Format("{0}um", v);
                }
            }
        }
        public HoleSizeCtrl()
        {
            InitializeComponent();
        }

        //public void UpdateHoleSize(double value)
        //{
        //    holeSize = value;
        //    string v = holeSize.ToString("0.00");
        //    this.lblHoleSize.Text = string.Format("{0}um", v);
        //}

        private void btnUp_Click(object sender, EventArgs e)
        {
            UpdownClickHandler?.Invoke(true);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            UpdownClickHandler?.Invoke(false);
        }
    }
}
