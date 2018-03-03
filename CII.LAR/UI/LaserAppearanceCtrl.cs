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
    /// <summary>
    /// Laser apparance control
    /// Author:Zhong Wen 2017/08/06
    /// </summary>
    public partial class LaserAppearanceCtrl : BaseCtrl
    {
        private GraphicsPropertiesManager graphicsPropertiesManager = Program.SysConfig.GraphicsPropertiesManager;

        private GraphicsProperties graphicsProperties;

        public LaserAppearanceCtrl() : base()
        {
            resources = new ComponentResourceManager(typeof(LaserAppearanceCtrl));
            this.ShowIndex = 3;
            graphicsProperties = graphicsPropertiesManager.GetPropertiesByName("Circle");
            InitializeComponent();
        }

        private void btnLaserCtrl_Click(object sender, EventArgs e)
        {
            ClickDelegateHandler?.Invoke(sender, "Laser Control");
        }

        private void sliderTransparency_ValueChanged(object sender, EventArgs e)
        {
            var value = this.sliderTransparency.Value;
            if (graphicsProperties != null)
            {
                graphicsProperties.Alpha = (0xFF / 100) * value;
            }
        }

        private void sliderThickness_ValueChanged(object sender, EventArgs e)
        {
            var value = this.sliderThickness.Value;
            if (graphicsProperties != null)
            {
                graphicsProperties.PenWidth = value;
            }
        }

        private void slideTargetSize_ValueChanged(object sender, EventArgs e)
        {
            var value = this.sliderTargetSize.Value;
            if (graphicsProperties != null)
            {
                graphicsProperties.TargetSize = value;
            }
        }

        private void slideZoneSize_ValueChanged(object sender, EventArgs e)
        {
            var value = this.sliderZoneSize.Value;
            if (graphicsProperties != null)
            {
                graphicsProperties.ExclusionSize = value;
            }
        }

        private void sliderZoneColour_ValueChanged(object sender, EventArgs e)
        {
            var value = this.sliderZoneColour.Value / 10;
            if (graphicsProperties != null)
            {
                graphicsProperties.ChangeColor(value);
            }
        }

        protected override void RefreshUI()
        {
            base.RefreshUI();
            this.Title = global::CII.LAR.Properties.Resources.StrLaserCtrlTitle;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            graphicsProperties.Alpha = 0xFF;
            graphicsProperties.PenWidth = 1;
            graphicsProperties.ExclusionSize = 20;
            graphicsProperties.TargetSize = 1;
            graphicsProperties.Color = Color.Yellow;
        }
    }
}
