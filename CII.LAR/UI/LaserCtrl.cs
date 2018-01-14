using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CII.LAR.DrawTools;
using CII.LAR.Laser;
using CII.LAR.SysClass;
using CII.LAR.Algorithm;

namespace CII.LAR.UI
{
    /// <summary>
    /// Laser setting control
    /// Author: Zhong Wen 2017/08/05
    /// </summary>
    public partial class LaserCtrl : BaseCtrl
    {
        private List<HolePulsePoint> holePulsePoints;

        private GraphicsPropertiesManager graphicsPropertiesManager = GraphicsPropertiesManager.GraphicsManagerSingleInstance();

        private GraphicsProperties graphicsProperties;

        public LaserCtrl() : base()
        {
            resources = new ComponentResourceManager(typeof(LaserCtrl));
            holePulsePoints = SysConfig.GetSysConfig().LaserConfig.HolePulsePoints;
            this.ShowIndex = 5;
            graphicsProperties = graphicsPropertiesManager.GetPropertiesByName("Circle");
            InitializeComponent();
            InitializeSlider();
        }

        private void InitializeSlider()
        {
            this.sliderCtrl.SetMinMaxValue(5, 2500);
            this.sliderCtrl.SetValue(0.5f);
            this.btnFire.BackColor = Color.LightYellow;
            this.btnFire.Text = Res.LaserCtrl.StrFire;
        }

        public void HolesNumberSlider(bool isShow)
        {
            this.holesSlider.Visible = isShow;
        }

        public void UpdateHolesInfo(HolesInfo holesInfo)
        {
            this.holesSlider.Maximum = holesInfo.MaxHoleNum;
            this.holesSlider.Minimum = holesInfo.MinHoleNum;
            this.holesSlider.Value = holesInfo.HoleNum;
            this.holesSlider.Text = string.Format("{0}holes", holesInfo.HoleNum);
        }

        /// <summary>
        /// save preset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        bool flashing = false;
        /// <summary>
        /// fire laser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFire_Click(object sender, EventArgs e)
        {
            Program.EntryForm.Laser.Flashing = !flashing;
            Coordinate.GetCoordinate().SendAlignmentMotorPoint();
        }

        private void btnAlignLaser_Click(object sender, EventArgs e)
        {
            ClickDelegateHandler?.Invoke(sender, "Laser Alignment");
        }

        private void btnHoleSize_Click(object sender, EventArgs e)
        {
            ClickDelegateHandler?.Invoke(sender, "Laser Hole Size");
        }

        private void btnAppearance_Click(object sender, EventArgs e)
        {
            ClickDelegateHandler?.Invoke(sender, "Laser Appearance");
        }

        private void SliderValueChangedHandler(object sender, EventArgs e)
        {
            var value = this.sliderCtrl.Slider.Value;
            double y = CalSlopeFunction(value);
            this.sliderCtrl.PulseHole.Text = string.Format("{0:N}ms {1:N}um", value / 1000d, y);

            CheckPulse((int)y);

            if (graphicsProperties != null && SysConfig.GetSysConfig().LaserConfig != null)
            {
                SysConfig.GetSysConfig().LaserConfig.UpdatePulseWidth((float)y);
            }
            this.sliderCtrl.Update = false;
            UpdateSliderValueHandler?.Invoke(value / 1000f);
            this.sliderCtrl.Update = true;
        }

        public void UpdatePulseWidthSlider(float value)
        {
            this.sliderCtrl.UpdateValue(value);
        }

        private void CheckPulse(int value)
        {
            if (value > SysConfig.GetSysConfig().LaserConfig.MaxPulseWidth)
            {
                this.btnFire.BackColor = Color.LightSalmon;
                this.btnFire.Text = Res.LaserCtrl.StrBigPulse;
            }
            else if (value < SysConfig.GetSysConfig().LaserConfig.MinPulseWidth)
            {
                this.btnFire.BackColor = Color.LightSalmon;
                this.btnFire.Text = Res.LaserCtrl.StrSmallPulse;
            }
            else
            {
                this.btnFire.BackColor = Color.LightYellow;
                this.btnFire.Text = Res.LaserCtrl.StrFire;
            }
            this.btnFire.Invalidate();
        }

        private double CalSlopeFunction(int value)
        {
            double y = 0;
            int index = GetValueIndex(value);
            if (index != -1)
            {
                y = CalSlopeFunction(holePulsePoints[index], holePulsePoints[index + 1], value);
            }
            return y;
        }

        private double CalSlopeFunction(HolePulsePoint p1, HolePulsePoint p2, int value)
        {
            double k = 0;
            k = (p2.Y - p1.Y) / (p2.X - p1.X);
            int startX = (int)(p1.X * 1000);
            int endX = (int)(p2.X * 1000);
            var x = value / 1000d;
            return  k * (x - p2.X) + p2.Y;
        }

        private int GetValueIndex(int value)
        {
            int index = -1;
            var x = value / 1000d;
            if (holePulsePoints != null && holePulsePoints.Count > 0)
            {
                for (int i = 0; i < holePulsePoints.Count - 1; i++)
                {
                    var v1 = x - holePulsePoints[i].X;
                    var v2 = holePulsePoints[i + 1].X - x;
                    if (v1 > 0 && v2 >= 0)
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }

        protected override void RefreshUI()
        {
            base.RefreshUI();
            this.Title = global::CII.LAR.Properties.Resources.StrLaserCtrlTitle;
        }

        private void holesSlider_ValueChanged(object sender, EventArgs e)
        {
            Program.EntryForm.UpdateHoleNumber(this.holesSlider.Value);
        }
    }
}
