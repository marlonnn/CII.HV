using CII.LAR.MaterialSkin;

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
            this.lblTransparency = new CII.LAR.MaterialSkin.MaterialLabel();
            this.sliderTransparency = new CII.LAR.MaterialSkin.MaterialSlider();
            this.lblThickness = new CII.LAR.MaterialSkin.MaterialLabel();
            this.sliderThickness = new CII.LAR.MaterialSkin.MaterialSlider();
            this.lblTargerSize = new CII.LAR.MaterialSkin.MaterialLabel();
            this.sliderTargetSize = new CII.LAR.MaterialSkin.MaterialSlider();
            this.lblZoneSize = new CII.LAR.MaterialSkin.MaterialLabel();
            this.sliderZoneSize = new CII.LAR.MaterialSkin.MaterialSlider();
            this.lblZoneColour = new CII.LAR.MaterialSkin.MaterialLabel();
            this.sliderZoneColour = new CII.LAR.MaterialSkin.MaterialSlider();
            this.btnLaserCtrl = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.btnDefault = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // lblTransparency
            // 
            this.lblTransparency.Depth = 0;
            resources.ApplyResources(this.lblTransparency, "lblTransparency");
            this.lblTransparency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblTransparency.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblTransparency.Name = "lblTransparency";
            // 
            // sliderTransparency
            // 
            this.sliderTransparency.BackColor = System.Drawing.Color.Transparent;
            this.sliderTransparency.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderTransparency.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderTransparency.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderTransparency.Depth = 0;
            this.sliderTransparency.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderTransparency.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderTransparency.LargeChange = ((uint)(5u));
            resources.ApplyResources(this.sliderTransparency, "sliderTransparency");
            this.sliderTransparency.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.sliderTransparency.Name = "sliderTransparency";
            this.sliderTransparency.SmallChange = ((uint)(1u));
            this.sliderTransparency.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderTransparency.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderTransparency.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderTransparency.ThumbSize = 6;
            this.sliderTransparency.Value = 0;
            this.sliderTransparency.ValueChanged += new System.EventHandler(this.sliderTransparency_ValueChanged);
            // 
            // lblThickness
            // 
            this.lblThickness.Depth = 0;
            resources.ApplyResources(this.lblThickness, "lblThickness");
            this.lblThickness.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblThickness.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblThickness.Name = "lblThickness";
            // 
            // sliderThickness
            // 
            this.sliderThickness.BackColor = System.Drawing.Color.Transparent;
            this.sliderThickness.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderThickness.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderThickness.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderThickness.Depth = 0;
            this.sliderThickness.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderThickness.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderThickness.LargeChange = ((uint)(5u));
            resources.ApplyResources(this.sliderThickness, "sliderThickness");
            this.sliderThickness.Maximum = 5;
            this.sliderThickness.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.sliderThickness.Name = "sliderThickness";
            this.sliderThickness.SmallChange = ((uint)(1u));
            this.sliderThickness.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderThickness.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderThickness.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderThickness.ThumbSize = 6;
            this.sliderThickness.Value = 0;
            this.sliderThickness.ValueChanged += new System.EventHandler(this.sliderThickness_ValueChanged);
            // 
            // lblTargerSize
            // 
            this.lblTargerSize.Depth = 0;
            resources.ApplyResources(this.lblTargerSize, "lblTargerSize");
            this.lblTargerSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblTargerSize.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblTargerSize.Name = "lblTargerSize";
            // 
            // sliderTargetSize
            // 
            this.sliderTargetSize.BackColor = System.Drawing.Color.Transparent;
            this.sliderTargetSize.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderTargetSize.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderTargetSize.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderTargetSize.Depth = 0;
            this.sliderTargetSize.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderTargetSize.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderTargetSize.LargeChange = ((uint)(5u));
            resources.ApplyResources(this.sliderTargetSize, "sliderTargetSize");
            this.sliderTargetSize.Maximum = 10;
            this.sliderTargetSize.Minimum = 1;
            this.sliderTargetSize.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.sliderTargetSize.Name = "sliderTargetSize";
            this.sliderTargetSize.SmallChange = ((uint)(1u));
            this.sliderTargetSize.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderTargetSize.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderTargetSize.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderTargetSize.ThumbSize = 6;
            this.sliderTargetSize.Value = 1;
            this.sliderTargetSize.ValueChanged += new System.EventHandler(this.slideTargetSize_ValueChanged);
            // 
            // lblZoneSize
            // 
            this.lblZoneSize.Depth = 0;
            resources.ApplyResources(this.lblZoneSize, "lblZoneSize");
            this.lblZoneSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblZoneSize.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblZoneSize.Name = "lblZoneSize";
            // 
            // sliderZoneSize
            // 
            this.sliderZoneSize.BackColor = System.Drawing.Color.Transparent;
            this.sliderZoneSize.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderZoneSize.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderZoneSize.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderZoneSize.Depth = 0;
            this.sliderZoneSize.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderZoneSize.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderZoneSize.LargeChange = ((uint)(5u));
            resources.ApplyResources(this.sliderZoneSize, "sliderZoneSize");
            this.sliderZoneSize.Minimum = 2;
            this.sliderZoneSize.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.sliderZoneSize.Name = "sliderZoneSize";
            this.sliderZoneSize.SmallChange = ((uint)(1u));
            this.sliderZoneSize.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderZoneSize.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderZoneSize.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderZoneSize.ThumbSize = 6;
            this.sliderZoneSize.Value = 2;
            this.sliderZoneSize.ValueChanged += new System.EventHandler(this.slideZoneSize_ValueChanged);
            // 
            // lblZoneColour
            // 
            this.lblZoneColour.Depth = 0;
            resources.ApplyResources(this.lblZoneColour, "lblZoneColour");
            this.lblZoneColour.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblZoneColour.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblZoneColour.Name = "lblZoneColour";
            // 
            // sliderZoneColour
            // 
            this.sliderZoneColour.BackColor = System.Drawing.Color.Transparent;
            this.sliderZoneColour.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderZoneColour.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderZoneColour.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderZoneColour.Depth = 0;
            this.sliderZoneColour.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderZoneColour.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderZoneColour.LargeChange = ((uint)(5u));
            resources.ApplyResources(this.sliderZoneColour, "sliderZoneColour");
            this.sliderZoneColour.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.sliderZoneColour.Name = "sliderZoneColour";
            this.sliderZoneColour.SmallChange = ((uint)(1u));
            this.sliderZoneColour.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderZoneColour.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderZoneColour.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderZoneColour.ThumbSize = 6;
            this.sliderZoneColour.Value = 0;
            this.sliderZoneColour.ValueChanged += new System.EventHandler(this.sliderZoneColour_ValueChanged);
            // 
            // btnLaserCtrl
            // 
            this.btnLaserCtrl.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnLaserCtrl, "btnLaserCtrl");
            this.btnLaserCtrl.Depth = 0;
            this.btnLaserCtrl.Icon = null;
            this.btnLaserCtrl.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnLaserCtrl.Name = "btnLaserCtrl";
            this.btnLaserCtrl.Primary = false;
            this.btnLaserCtrl.Click += new System.EventHandler(this.btnLaserCtrl_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnDefault, "btnDefault");
            this.btnDefault.Depth = 0;
            this.btnDefault.Icon = null;
            this.btnDefault.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Primary = false;
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
            this.Name = "LaserAppearanceCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrLaserCtrlTitle;
            this.Controls.SetChildIndex(this.closeButton, 0);
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

        private MaterialLabel lblTransparency;
        private MaterialSlider sliderTransparency;
        private MaterialLabel lblThickness;
        private MaterialLabel lblZoneColour;
        private MaterialSlider sliderThickness;
        private MaterialLabel lblTargerSize;
        private MaterialSlider sliderTargetSize;
        private MaterialLabel lblZoneSize;
        private MaterialSlider sliderZoneSize;
        private MaterialSlider sliderZoneColour;
        private MaterialRoundButton btnLaserCtrl;
        private MaterialRoundButton btnDefault;
    }
}
