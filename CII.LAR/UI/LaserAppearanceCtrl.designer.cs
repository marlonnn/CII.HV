namespace CII.LAR.UI
{
    partial class LaserAppearanceCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaserAppearanceCtrl));
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.lblTransparency = new DevComponents.DotNetBar.LabelX();
            this.sliderTransparency = new DevComponents.DotNetBar.Controls.Slider();
            this.lblThickness = new DevComponents.DotNetBar.LabelX();
            this.sliderThickness = new DevComponents.DotNetBar.Controls.Slider();
            this.lblTargerSize = new DevComponents.DotNetBar.LabelX();
            this.sliderTargetSize = new DevComponents.DotNetBar.Controls.Slider();
            this.lblZoneSize = new DevComponents.DotNetBar.LabelX();
            this.sliderZoneSize = new DevComponents.DotNetBar.Controls.Slider();
            this.lblZoneColour = new DevComponents.DotNetBar.LabelX();
            this.sliderZoneColour = new DevComponents.DotNetBar.Controls.Slider();
            this.btnLaserCtrl = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
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
            // lblTransparency
            // 
            // 
            // 
            // 
            this.lblTransparency.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblTransparency, "lblTransparency");
            this.lblTransparency.Name = "lblTransparency";
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
            // lblThickness
            // 
            // 
            // 
            // 
            this.lblThickness.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblThickness, "lblThickness");
            this.lblThickness.Name = "lblThickness";
            // 
            // sliderThickness
            // 
            // 
            // 
            // 
            this.sliderThickness.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderThickness.LabelVisible = false;
            resources.ApplyResources(this.sliderThickness, "sliderThickness");
            this.sliderThickness.Maximum = 5;
            this.sliderThickness.Name = "sliderThickness";
            this.sliderThickness.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderThickness.Value = 0;
            this.sliderThickness.ValueChanged += new System.EventHandler(this.sliderThickness_ValueChanged);
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
            // sliderTargetSize
            // 
            // 
            // 
            // 
            this.sliderTargetSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderTargetSize.LabelVisible = false;
            resources.ApplyResources(this.sliderTargetSize, "sliderTargetSize");
            this.sliderTargetSize.Maximum = 10;
            this.sliderTargetSize.Minimum = 1;
            this.sliderTargetSize.Name = "sliderTargetSize";
            this.sliderTargetSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderTargetSize.Value = 1;
            this.sliderTargetSize.ValueChanged += new System.EventHandler(this.slideTargetSize_ValueChanged);
            // 
            // lblZoneSize
            // 
            // 
            // 
            // 
            this.lblZoneSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblZoneSize, "lblZoneSize");
            this.lblZoneSize.Name = "lblZoneSize";
            // 
            // sliderZoneSize
            // 
            // 
            // 
            // 
            this.sliderZoneSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderZoneSize.LabelVisible = false;
            resources.ApplyResources(this.sliderZoneSize, "sliderZoneSize");
            this.sliderZoneSize.Minimum = 2;
            this.sliderZoneSize.Name = "sliderZoneSize";
            this.sliderZoneSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderZoneSize.Value = 2;
            this.sliderZoneSize.ValueChanged += new System.EventHandler(this.slideZoneSize_ValueChanged);
            // 
            // lblZoneColour
            // 
            // 
            // 
            // 
            this.lblZoneColour.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblZoneColour, "lblZoneColour");
            this.lblZoneColour.Name = "lblZoneColour";
            // 
            // sliderZoneColour
            // 
            // 
            // 
            // 
            this.sliderZoneColour.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderZoneColour.LabelVisible = false;
            resources.ApplyResources(this.sliderZoneColour, "sliderZoneColour");
            this.sliderZoneColour.Name = "sliderZoneColour";
            this.sliderZoneColour.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderZoneColour.Value = 0;
            this.sliderZoneColour.ValueChanged += new System.EventHandler(this.sliderZoneColour_ValueChanged);
            // 
            // btnLaserCtrl
            // 
            this.btnLaserCtrl.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnLaserCtrl, "btnLaserCtrl");
            this.btnLaserCtrl.Name = "btnLaserCtrl";
            this.btnLaserCtrl.Click += new System.EventHandler(this.btnLaserCtrl_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnDefault, "btnDefault");
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // LaserAppearanceCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnLaserCtrl);
            this.Controls.Add(this.sliderZoneColour);
            this.Controls.Add(this.lblZoneColour);
            this.Controls.Add(this.sliderZoneSize);
            this.Controls.Add(this.lblZoneSize);
            this.Controls.Add(this.sliderTargetSize);
            this.Controls.Add(this.lblTargerSize);
            this.Controls.Add(this.sliderThickness);
            this.Controls.Add(this.lblThickness);
            this.Controls.Add(this.sliderTransparency);
            this.Controls.Add(this.lblTransparency);
            this.Controls.Add(this.line1);
            this.Name = "LaserAppearanceCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrLaserCtrlTitle;
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.line1, 0);
            this.Controls.SetChildIndex(this.lblTransparency, 0);
            this.Controls.SetChildIndex(this.sliderTransparency, 0);
            this.Controls.SetChildIndex(this.lblThickness, 0);
            this.Controls.SetChildIndex(this.sliderThickness, 0);
            this.Controls.SetChildIndex(this.lblTargerSize, 0);
            this.Controls.SetChildIndex(this.sliderTargetSize, 0);
            this.Controls.SetChildIndex(this.lblZoneSize, 0);
            this.Controls.SetChildIndex(this.sliderZoneSize, 0);
            this.Controls.SetChildIndex(this.lblZoneColour, 0);
            this.Controls.SetChildIndex(this.sliderZoneColour, 0);
            this.Controls.SetChildIndex(this.btnLaserCtrl, 0);
            this.Controls.SetChildIndex(this.btnDefault, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.Line line1;
        private DevComponents.DotNetBar.LabelX lblTransparency;
        private DevComponents.DotNetBar.Controls.Slider sliderTransparency;
        private DevComponents.DotNetBar.LabelX lblThickness;
        private DevComponents.DotNetBar.LabelX lblZoneColour;
        private DevComponents.DotNetBar.Controls.Slider sliderThickness;
        private DevComponents.DotNetBar.LabelX lblTargerSize;
        private DevComponents.DotNetBar.Controls.Slider sliderTargetSize;
        private DevComponents.DotNetBar.LabelX lblZoneSize;
        private DevComponents.DotNetBar.Controls.Slider sliderZoneSize;
        private DevComponents.DotNetBar.Controls.Slider sliderZoneColour;
        private System.Windows.Forms.Button btnLaserCtrl;
        private System.Windows.Forms.Button btnDefault;
    }
}
