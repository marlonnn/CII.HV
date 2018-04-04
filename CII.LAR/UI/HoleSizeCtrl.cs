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
    public partial class HoleSizeCtrl : UpDownArrowCtrl
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
                    this.LabelValue = string.Format("{0}um", v);
                }
            }
        }
        public HoleSizeCtrl()
        {
            InitializeComponent();
            this.LabelValue = "0.001um";
        }

        //public void UpdateHoleSize(double value)
        //{
        //    holeSize = value;
        //    string v = holeSize.ToString("0.00");
        //    this.lblHoleSize.Text = string.Format("{0}um", v);
        //}

        protected override void UpClick(object sender, EventArgs e)
        {
            UpdownClickHandler?.Invoke(true);
        }

        protected override void DownClick(object sender, EventArgs e)
        {
            UpdownClickHandler?.Invoke(false);
        }
    }
}
