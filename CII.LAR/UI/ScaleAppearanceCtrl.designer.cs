namespace CII.LAR.UI
{
    partial class ScaleAppearanceCtrl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScaleAppearanceCtrl));
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.btnLaserCtrl = new System.Windows.Forms.Button();
            this.sliderColour = new DevComponents.DotNetBar.Controls.Slider();
            this.lblZColour = new DevComponents.DotNetBar.LabelX();
            this.sliderTargetSize = new DevComponents.DotNetBar.Controls.Slider();
            this.lblTargerSize = new DevComponents.DotNetBar.LabelX();
            this.sliderThickness = new DevComponents.DotNetBar.Controls.Slider();
            this.lblThickness = new DevComponents.DotNetBar.LabelX();
            this.sliderTransparency = new DevComponents.DotNetBar.Controls.Slider();
            this.lblTransparency = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // line1
            // 
            resources.ApplyResources(this.line1, "line1");
            this.line1.Name = "line1";
            // 
            // btnLaserCtrl
            // 
            this.btnLaserCtrl.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnLaserCtrl, "btnLaserCtrl");
            this.btnLaserCtrl.Name = "btnLaserCtrl";
            this.btnLaserCtrl.Click += new System.EventHandler(this.btnLaserCtrl_Click);
            // 
            // sliderColour
            // 
            // 
            // 
            // 
            this.sliderColour.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderColour.LabelVisible = false;
            resources.ApplyResources(this.sliderColour, "sliderColour");
            this.sliderColour.Minimum = 1;
            this.sliderColour.Name = "sliderColour";
            this.sliderColour.Step = 10;
            this.sliderColour.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderColour.Value = 0;
            this.sliderColour.ValueChanged += new System.EventHandler(this.sliderColour_ValueChanged);
            // 
            // lblZColour
            // 
            // 
            // 
            // 
            this.lblZColour.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblZColour, "lblZColour");
            this.lblZColour.Name = "lblZColour";
            // 
            // sliderTargetSize
            // 
            // 
            // 
            // 
            this.sliderTargetSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderTargetSize.LabelVisible = false;
            resources.ApplyResources(this.sliderTargetSize, "sliderTargetSize");
            this.sliderTargetSize.Maximum = 20;
            this.sliderTargetSize.Minimum = 8;
            this.sliderTargetSize.Name = "sliderTargetSize";
            this.sliderTargetSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderTargetSize.Value = 9;
            this.sliderTargetSize.ValueChanged += new System.EventHandler(this.sliderTargetSize_ValueChanged);
            // 
            // lblTargerSize
            // 
            // 
            // 
            // 
            this.lblTargerSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblTargerSize, "lblTargerSize");
            this.lblTargerSize.Name = "lblTargerSize";
            // 
            // sliderThickness
            // 
            // 
            // 
            // 
            this.sliderThickness.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderThickness.LabelVisible = false;
            resources.ApplyResources(this.sliderThickness, "sliderThickness");
            this.sliderThickness.Maximum = 10;
            this.sliderThickness.Minimum = 1;
            this.sliderThickness.Name = "sliderThickness";
            this.sliderThickness.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderThickness.Value = 1;
            this.sliderThickness.ValueChanged += new System.EventHandler(this.sliderThickness_ValueChanged);
            // 
            // lblThickness
            // 
            // 
            // 
            // 
            this.lblThickness.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblThickness, "lblThickness");
            this.lblThickness.Name = "lblThickness";
            // 
            // sliderTransparency
            // 
            // 
            // 
            // 
            this.sliderTransparency.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderTransparency.LabelVisible = false;
            resources.ApplyResources(this.sliderTransparency, "sliderTransparency");
            this.sliderTransparency.Name = "sliderTransparency";
            this.sliderTransparency.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderTransparency.Value = 0;
            this.sliderTransparency.ValueChanged += new System.EventHandler(this.sliderTransparency_ValueChanged);
            // 
            // lblTransparency
            // 
            // 
            // 
            // 
            this.lblTransparency.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblTransparency, "lblTransparency");
            this.lblTransparency.Name = "lblTransparency";
            // 
            // ScaleAppearanceCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLaserCtrl);
            this.Controls.Add(this.sliderColour);
            this.Controls.Add(this.lblZColour);
            this.Controls.Add(this.sliderTargetSize);
            this.Controls.Add(this.lblTargerSize);
            this.Controls.Add(this.sliderThickness);
            this.Controls.Add(this.lblThickness);
            this.Controls.Add(this.sliderTransparency);
            this.Controls.Add(this.lblTransparency);
            this.Controls.Add(this.line1);
            this.DoubleBuffered = true;
            this.Name = "ScaleAppearanceCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrScaleAppearanceTitle;
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.line1, 0);
            this.Controls.SetChildIndex(this.lblTransparency, 0);
            this.Controls.SetChildIndex(this.sliderTransparency, 0);
            this.Controls.SetChildIndex(this.lblThickness, 0);
            this.Controls.SetChildIndex(this.sliderThickness, 0);
            this.Controls.SetChildIndex(this.lblTargerSize, 0);
            this.Controls.SetChildIndex(this.sliderTargetSize, 0);
            this.Controls.SetChildIndex(this.lblZColour, 0);
            this.Controls.SetChildIndex(this.sliderColour, 0);
            this.Controls.SetChildIndex(this.btnLaserCtrl, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.Line line1;
        private System.Windows.Forms.Button btnLaserCtrl;
        private DevComponents.DotNetBar.Controls.Slider sliderColour;
        private DevComponents.DotNetBar.LabelX lblZColour;
        private DevComponents.DotNetBar.Controls.Slider sliderTargetSize;
        private DevComponents.DotNetBar.LabelX lblTargerSize;
        private DevComponents.DotNetBar.Controls.Slider sliderThickness;
        private DevComponents.DotNetBar.LabelX lblThickness;
        private DevComponents.DotNetBar.Controls.Slider sliderTransparency;
        private DevComponents.DotNetBar.LabelX lblTransparency;
    }
}
