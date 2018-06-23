using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.DrawTools;
using DevComponents.DotNetBar;

namespace CII.LAR.UI
{
    public partial class ScaleAppearanceCtrl : BaseCtrl
    {
        private GraphicsPropertiesManager graphicsPropertiesManager = Program.SysConfig.GraphicsPropertiesManager;

        private GraphicsProperties graphicsProperties;
        private RichPictureBox pictureBox;
        public ScaleAppearanceCtrl(RichPictureBox pictureBox) : base()
        {
            this.pictureBox = pictureBox;
            resources = new ComponentResourceManager(typeof(ScaleAppearanceCtrl));
            this.ShowIndex = 10;
            this.CtrlType = CtrlType.ScaleAppearanceCtrl;
            InitializeComponent();
            this.graphicsProperties = graphicsPropertiesManager.GetPropertiesByName("Ruler");
            this.InitializeThumbCustomShape();
            SetSliderValue();
        }

        private void SetSliderValue()
        {
            invokeColorChange = false;
            this.sliderTargetSize.Value = (int)graphicsProperties.TextSize;
            this.sliderThickness.Value = graphicsProperties.PenWidth;
            this.sliderTransparency.Value = (int)((graphicsProperties.Alpha * 100) / 255f);
            //this.sliderTickLength.Value = graphicsProperties.TargetSize;
            this.sliderColour.Value = graphicsProperties.ColorIndex() * 10;
            invokeColorChange = true;
        }

        private void btnLaserCtrl_Click(object sender, EventArgs e)
        {
            DelegateClass.GetDelegate().ClickDelegateHandler?.Invoke(sender, CtrlType.SettingCtrl);
        }


        public delegate void UpdateTimerState(bool enable);
        public UpdateTimerState UpdateTimerStatesHandler;

        private void sliderTransparency_ValueChanged(object sender, EventArgs e)
        {
            if (invokeColorChange)
            {
                var value = this.sliderTransparency.Value;
                if (graphicsProperties != null)
                {
                    graphicsProperties.Alpha = (int)((0xFF * value ) / 100f);
                    this.pictureBox.Invalidate();
                }
            }
        }

        private void sliderThickness_ValueChanged(object sender, EventArgs e)
        {
            if (invokeColorChange)
            {
                var value = this.sliderThickness.Value;
                if (graphicsProperties != null)
                {
                    graphicsProperties.PenWidth = value;
                    this.pictureBox.Invalidate();
                }
            }
        }

        private void sliderTargetSize_ValueChanged(object sender, EventArgs e)
        {
            if (invokeColorChange)
            {
                var value = this.sliderTargetSize.Value;
                if (graphicsProperties != null)
                {
                    graphicsProperties.TextSize = value;
                    this.pictureBox.Invalidate();
                }
            }
        }

        private bool invokeColorChange = true;
        private void sliderColour_ValueChanged(object sender, EventArgs e)
        {
            if (invokeColorChange)
            {
                var value = this.sliderColour.Value / 10;
                if (graphicsProperties != null)
                {
                    graphicsProperties.ChangeColor(value);
                    this.pictureBox.Invalidate();
                }
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                SetSliderValue();
            }
        }

        public override void RefreshUI()
        {
            base.RefreshUI();
            this.Title = global::CII.LAR.Properties.Resources.StrScaleAppearanceTitle;
            resources.ApplyResources(this.btnLaserCtrl, btnLaserCtrl.Name);
            this.Invalidate();
        }
    }
}
