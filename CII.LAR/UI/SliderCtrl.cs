using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;

namespace CII.LAR.UI
{
    /// <summary>
    /// Pulse width and hole size control
    /// Author: Zhong Wen 2018/08/05
    /// </summary>
    public partial class SliderCtrl : UserControl
    {
        public delegate void SliderValueChanged(object sender, EventArgs e);
        public SliderValueChanged SliderValueChangedHandler;
        public Slider Slider
        {
            get
            {
                return slider;
            }
            set
            {
                slider = value;
            }
        }

        public MaterialSkin.MaterialLabel PulseHole
        {
            get
            {
                return this.PulseHoleWS;
            }
            set
            {
                PulseHoleWS = value;
            }
        }
        public SliderCtrl()
        {
            InitializeComponent();
        }

        private void Slide_ValueChanged(object sender, EventArgs e)
        {
            if (Update)
                SliderValueChangedHandler?.Invoke(sender, e);
        }

        public void SetMinMaxValue(int min, int max)
        {
            this.slider.Minimum = min;
            this.slider.Maximum = max;
        }

        public void SetValue(float value)
        {
            this.PulseHoleWS.Text = string.Format("{0} us", value);
            this.slider.Value = (int)(value * 10);
        }

        private bool update = true;
        public new  bool Update
        {
            get { return this.update; }
            set
            {
                if (value != this.update)
                {
                    this.update = value;
                }
            }
        }
        public void UpdateValue(float value)
        {
            this.PulseHoleWS.Text = string.Format("{0} us", value);
            this.slider.Value = (int)(value * 10);
        }
    }
}
