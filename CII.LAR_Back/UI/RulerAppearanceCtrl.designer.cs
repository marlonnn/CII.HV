namespace CII.LAR.UI
{
    partial class RulerAppearanceCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RulerAppearanceCtrl));
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.btnLaserCtrl = new System.Windows.Forms.Button();
            this.sliderColour = new DevComponents.DotNetBar.Controls.Slider();
            this.lblZColour = new DevComponents.DotNetBar.LabelX();
            this.sliderTickLength = new DevComponents.DotNetBar.Controls.Slider();
            this.lblTickLength = new DevComponents.DotNetBar.LabelX();
            this.sliderTargetSize = new DevComponents.DotNetBar.Controls.Slider();
            this.lblTargerSize = new DevComponents.DotNetBar.LabelX();
            this.sliderThickness = new DevComponents.DotNetBar.Controls.Slider();
            this.lblThickness = new DevComponents.DotNetBar.LabelX();
            this.sliderTransparency = new DevComponents.DotNetBar.Controls.Slider();
            this.lblTransparency = new DevComponents.DotNetBar.LabelX();
            this.lblRuler = new DevComponents.DotNetBar.LabelX();
            this.cmboxRuler = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
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
            // sliderTickLength
            // 
            // 
            // 
            // 
            this.sliderTickLength.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderTickLength.LabelVisible = false;
            resources.ApplyResources(this.sliderTickLength, "sliderTickLength");
            this.sliderTickLength.Name = "sliderTickLength";
            this.sliderTickLength.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderTickLength.Value = 0;
            this.sliderTickLength.ValueChanged += new System.EventHandler(this.sliderTickLength_ValueChanged);
            // 
            // lblTickLength
            // 
            // 
            // 
            // 
            this.lblTickLength.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblTickLength, "lblTickLength");
            this.lblTickLength.Name = "lblTickLength";
            // 
            // sliderTargetSize
            // 
            // 
            // 
            // 
            this.sliderTargetSize.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderTargetSize.LabelVisible = false;
            resources.ApplyResources(this.sliderTargetSize, "sliderTargetSize");
            this.sliderTargetSize.Name = "sliderTargetSize";
            this.sliderTargetSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderTargetSize.Value = 0;
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
            // lblRuler
            // 
            // 
            // 
            // 
            this.lblRuler.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblRuler, "lblRuler");
            this.lblRuler.Name = "lblRuler";
            // 
            // cmboxRuler
            // 
            this.cmboxRuler.DisplayMember = "Text";
            this.cmboxRuler.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboxRuler.FormattingEnabled = true;
            resources.ApplyResources(this.cmboxRuler, "cmboxRuler");
            this.cmboxRuler.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4});
            this.cmboxRuler.Name = "cmboxRuler";
            this.cmboxRuler.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmboxRuler.DropDown += new System.EventHandler(this.cmboxRuler_DropDown);
            this.cmboxRuler.SelectedIndexChanged += new System.EventHandler(this.cmboxRuler_SelectedIndexChanged);
            this.cmboxRuler.DropDownClosed += new System.EventHandler(this.cmboxRuler_DropDownClosed);
            // 
            // comboItem1
            // 
            resources.ApplyResources(this.comboItem1, "comboItem1");
            // 
            // comboItem2
            // 
            resources.ApplyResources(this.comboItem2, "comboItem2");
            // 
            // comboItem3
            // 
            resources.ApplyResources(this.comboItem3, "comboItem3");
            // 
            // comboItem4
            // 
            resources.ApplyResources(this.comboItem4, "comboItem4");
            // 
            // RulerAppearanceCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmboxRuler);
            this.Controls.Add(this.lblRuler);
            this.Controls.Add(this.btnLaserCtrl);
            this.Controls.Add(this.sliderColour);
            this.Controls.Add(this.lblZColour);
            this.Controls.Add(this.sliderTickLength);
            this.Controls.Add(this.lblTickLength);
            this.Controls.Add(this.sliderTargetSize);
            this.Controls.Add(this.lblTargerSize);
            this.Controls.Add(this.sliderThickness);
            this.Controls.Add(this.lblThickness);
            this.Controls.Add(this.sliderTransparency);
            this.Controls.Add(this.lblTransparency);
            this.Controls.Add(this.line1);
            this.DoubleBuffered = true;
            this.Name = "RulerAppearanceCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrRulerAppearanceTitle;
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.line1, 0);
            this.Controls.SetChildIndex(this.lblTransparency, 0);
            this.Controls.SetChildIndex(this.sliderTransparency, 0);
            this.Controls.SetChildIndex(this.lblThickness, 0);
            this.Controls.SetChildIndex(this.sliderThickness, 0);
            this.Controls.SetChildIndex(this.lblTargerSize, 0);
            this.Controls.SetChildIndex(this.sliderTargetSize, 0);
            this.Controls.SetChildIndex(this.lblTickLength, 0);
            this.Controls.SetChildIndex(this.sliderTickLength, 0);
            this.Controls.SetChildIndex(this.lblZColour, 0);
            this.Controls.SetChildIndex(this.sliderColour, 0);
            this.Controls.SetChildIndex(this.btnLaserCtrl, 0);
            this.Controls.SetChildIndex(this.lblRuler, 0);
            this.Controls.SetChildIndex(this.cmboxRuler, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.Line line1;
        private System.Windows.Forms.Button btnLaserCtrl;
        private DevComponents.DotNetBar.Controls.Slider sliderColour;
        private DevComponents.DotNetBar.LabelX lblZColour;
        private DevComponents.DotNetBar.Controls.Slider sliderTickLength;
        private DevComponents.DotNetBar.LabelX lblTickLength;
        private DevComponents.DotNetBar.Controls.Slider sliderTargetSize;
        private DevComponents.DotNetBar.LabelX lblTargerSize;
        private DevComponents.DotNetBar.Controls.Slider sliderThickness;
        private DevComponents.DotNetBar.LabelX lblThickness;
        private DevComponents.DotNetBar.Controls.Slider sliderTransparency;
        private DevComponents.DotNetBar.LabelX lblTransparency;
        private DevComponents.DotNetBar.LabelX lblRuler;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmboxRuler;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
    }
}
