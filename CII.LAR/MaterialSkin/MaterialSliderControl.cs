using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    /// <summary>
    /// Material slider control
    /// Zhong Wen 2018/08/27
    /// </summary>
    public partial class MaterialSliderControl : UserControl
    {
        private Timer timer;
        private bool isUpLongPress;
        private int sliderValue;
        public int SliderValue
        {
            get { return this.sliderValue; }
            set
            {
                if (value != this.sliderValue)
                {
                    this.sliderValue = value;
                    this.slider.Value = value;
                    this.lblValue.Text = value.ToString();
                    this.lblValue.Invalidate();
                }
            }
        }

        public MaterialSliderControl()
        {
            InitializeComponent();
            isUpLongPress = true;
            timer = new Timer();
            timer.Enabled = false;
            timer.Tick += Timer_Tick;
            SliderValue = slider.Value;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (isUpLongPress)
            {
                btnAdd_Click(null, null);
            }
            else
            {
                btnSub_Click(null, null);
            }
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            if (this.slider.Value - 1 < this.slider.Minimum) return;
            SliderValue--;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.slider.Value + 1 > this.slider.Maximum) return;
            SliderValue++;
        }

        private void btnSub_MouseDown(object sender, MouseEventArgs e)
        {
            if (!timer.Enabled)
            {
                isUpLongPress = false;
                timer.Interval = 100;
                timer.Enabled = true;
            }
        }

        private void btnSub_MouseUp(object sender, MouseEventArgs e)
        {
            BtnMouseUp();
        }

        private void btnAdd_MouseDown(object sender, MouseEventArgs e)
        {
            if (!timer.Enabled)
            {
                isUpLongPress = true;
                timer.Interval = 100;
                timer.Enabled = true;
            }
        }

        private void btnAdd_MouseUp(object sender, MouseEventArgs e)
        {
            BtnMouseUp();
        }

        private void BtnMouseUp()
        {
            if (timer.Enabled)
            {
                timer.Enabled = false;
            }
        }

        private void slider_ValueChanged(object sender, EventArgs e)
        {
            this.SliderValue = this.slider.Value;
            SliderValueChanged?.Invoke(sender, e);
        }

        public event EventHandler SliderValueChanged;
    }
}
