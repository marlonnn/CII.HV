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
    public partial class RulerAppearanceCtrl : BaseCtrl
    {
        private GraphicsPropertiesManager graphicsPropertiesManager = Program.SysConfig.GraphicsPropertiesManager;

        private GraphicsProperties graphicsProperties;
        public RulerAppearanceCtrl() : base()
        {
            resources = new ComponentResourceManager(typeof(RulerAppearanceCtrl));
            this.ShowIndex = 4;
            this.CtrlType = CtrlType.RulerAppearanceCtrl;
            InitializeComponent();
        }

        private void SetSliderValue()
        {
            //this.sliderTargetSize.Value = graphicsProperties.TargetSize;
            this.sliderThickness.Value = graphicsProperties.PenWidth;
            this.sliderTransparency.Value = graphicsProperties.Alpha * (100 / 0xFF);
            //this.sliderTickLength.Value = graphicsProperties.TargetSize;
            this.sliderColour.Value = graphicsProperties.ColorIndex() * 10;
        }

        private void btnLaserCtrl_Click(object sender, EventArgs e)
        {
            DelegateClass.GetDelegate().ClickDelegateHandler?.Invoke(sender, CtrlType.StatisticsCtrl);
        }

        private void cmboxRuler_DropDown(object sender, EventArgs e)
        {
            UpdateTimerStatesHandler?.Invoke(false);
        }

        private void cmboxRuler_DropDownClosed(object sender, EventArgs e)
        {
            UpdateTimerStatesHandler?.Invoke(true);
        }

        public delegate void UpdateTimerState(bool enable);
        public UpdateTimerState UpdateTimerStatesHandler;

        private void cmboxRuler_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = this.cmboxRuler.SelectedItem.ToString();
            graphicsProperties = graphicsPropertiesManager.GetPropertiesByName(name);
            SetSliderValue();

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

        private void sliderTargetSize_ValueChanged(object sender, EventArgs e)
        {

        }

        private void sliderTickLength_ValueChanged(object sender, EventArgs e)
        {

        }

        private void sliderColour_ValueChanged(object sender, EventArgs e)
        {
            var value = this.sliderColour.Value / 10;
            if (graphicsProperties != null)
            {
                graphicsProperties.ChangeColor(value);
            }
        }

        public override void RefreshUI()
        {
            base.RefreshUI();
            this.Title = global::CII.LAR.Properties.Resources.StrRulerAppearanceTitle;
            resources.ApplyResources(this.btnLaserCtrl, btnLaserCtrl.Name);
            this.Invalidate();
        }
    }
}
