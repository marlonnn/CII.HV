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
using System.Drawing.Drawing2D;
using CII.LAR.MaterialSkin;

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

        private RichPictureBox pictureBox;

        public LaserAppearanceCtrl(RichPictureBox pictureBox) : base()
        {
            resources = new ComponentResourceManager(typeof(LaserAppearanceCtrl));
            this.ShowIndex = 3;
            this.CtrlType = CtrlType.LaserAppreance;
            graphicsProperties = graphicsPropertiesManager.GetPropertiesByName("Circle");
            InitializeComponent();
            this.pictureBox = pictureBox;
            this.sliderTargetSize.Value = graphicsProperties.TargetSize;
            this.sliderThickness.Value = graphicsProperties.PenWidth;
            this.sliderTransparency.Value = (int)(graphicsProperties.Alpha * 100 / 255f);
            this.sliderZoneSize.Value = graphicsProperties.ExclusionSize;
            this.sliderZoneColour.Value = graphicsProperties.ColorIndex() * 10;
            //GraphicsPath gp2 = new GraphicsPath();
            //Size s = new Size(sliderTransparency.ThumbSize, sliderTransparency.Height * 9 / 10);
            //gp2.AddEllipse(new RectangleF((0 + sliderTransparency.Height) / 2f, (0 + sliderTransparency.Height) / 2f, 16, 16));
            //sliderTransparency.ThumbCustomShape = gp2;
            //sliderTargetSize.ThumbCustomShape = gp2;
            //sliderThickness.ThumbCustomShape = gp2;
            //sliderZoneColour.ThumbCustomShape = gp2;
            //sliderZoneSize.ThumbCustomShape = gp2;
            this.InitializeThumbCustomShape();
        }

        private void btnLaserCtrl_Click(object sender, EventArgs e)
        {
            DelegateClass.GetDelegate().ClickDelegateHandler?.Invoke(sender, CtrlType.LaserCtrl);
        }

        private void sliderTransparency_ValueChanged(object sender, EventArgs e)
        {
            var value = this.sliderTransparency.Value;
            if (graphicsProperties != null)
            {
                graphicsProperties.Alpha = (int)(0xFF * (value / 100f));
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

        public override void RefreshUI()
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
            this.pictureBox.Invalidate();
        }
    }
}
