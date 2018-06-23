using CII.LAR.MaterialSkin;

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
            this.btnLaserCtrl = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.sliderColour = new CII.LAR.MaterialSkin.MaterialSlider();
            this.lblZColour = new CII.LAR.MaterialSkin.MaterialLabel();
            this.sliderTargetSize = new CII.LAR.MaterialSkin.MaterialSlider();
            this.lblTargerSize = new CII.LAR.MaterialSkin.MaterialLabel();
            this.sliderThickness = new CII.LAR.MaterialSkin.MaterialSlider();
            this.lblThickness = new CII.LAR.MaterialSkin.MaterialLabel();
            this.sliderTransparency = new CII.LAR.MaterialSkin.MaterialSlider();
            this.lblTransparency = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblRuler = new CII.LAR.MaterialSkin.MaterialLabel();
            this.cmboxRuler = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
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
            // sliderColour
            // 
            this.sliderColour.BackColor = System.Drawing.Color.Transparent;
            this.sliderColour.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderColour.BarOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderColour.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderColour.Depth = 0;
            this.sliderColour.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderColour.ElapsedOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(95)))));
            this.sliderColour.LargeChange = ((uint)(5u));
            resources.ApplyResources(this.sliderColour, "sliderColour");
            this.sliderColour.Minimum = 1;
            this.sliderColour.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.sliderColour.Name = "sliderColour";
            this.sliderColour.SmallChange = ((uint)(1u));
            this.sliderColour.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderColour.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderColour.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderColour.ThumbSize = 6;
            this.sliderColour.ValueChanged += new System.EventHandler(this.sliderColour_ValueChanged);
            // 
            // lblZColour
            // 
            this.lblZColour.Depth = 0;
            resources.ApplyResources(this.lblZColour, "lblZColour");
            this.lblZColour.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblZColour.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblZColour.Name = "lblZColour";
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
            this.sliderTargetSize.Maximum = 20;
            this.sliderTargetSize.Minimum = 8;
            this.sliderTargetSize.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.sliderTargetSize.Name = "sliderTargetSize";
            this.sliderTargetSize.SmallChange = ((uint)(1u));
            this.sliderTargetSize.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderTargetSize.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderTargetSize.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderTargetSize.ThumbSize = 6;
            this.sliderTargetSize.Value = 9;
            this.sliderTargetSize.ValueChanged += new System.EventHandler(this.sliderTargetSize_ValueChanged);
            // 
            // lblTargerSize
            // 
            this.lblTargerSize.Depth = 0;
            resources.ApplyResources(this.lblTargerSize, "lblTargerSize");
            this.lblTargerSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblTargerSize.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblTargerSize.Name = "lblTargerSize";
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
            this.sliderThickness.Maximum = 10;
            this.sliderThickness.Minimum = 1;
            this.sliderThickness.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.sliderThickness.Name = "sliderThickness";
            this.sliderThickness.SmallChange = ((uint)(1u));
            this.sliderThickness.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderThickness.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderThickness.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderThickness.ThumbSize = 6;
            this.sliderThickness.Value = 1;
            this.sliderThickness.ValueChanged += new System.EventHandler(this.sliderThickness_ValueChanged);
            // 
            // lblThickness
            // 
            this.lblThickness.Depth = 0;
            resources.ApplyResources(this.lblThickness, "lblThickness");
            this.lblThickness.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblThickness.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblThickness.Name = "lblThickness";
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
            this.sliderTransparency.Minimum = 1;
            this.sliderTransparency.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.sliderTransparency.Name = "sliderTransparency";
            this.sliderTransparency.SmallChange = ((uint)(1u));
            this.sliderTransparency.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderTransparency.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            this.sliderTransparency.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.sliderTransparency.ThumbSize = 6;
            this.sliderTransparency.Value = 1;
            this.sliderTransparency.ValueChanged += new System.EventHandler(this.sliderTransparency_ValueChanged);
            // 
            // lblTransparency
            // 
            this.lblTransparency.Depth = 0;
            resources.ApplyResources(this.lblTransparency, "lblTransparency");
            this.lblTransparency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblTransparency.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblTransparency.Name = "lblTransparency";
            // 
            // lblRuler
            // 
            this.lblRuler.Depth = 0;
            resources.ApplyResources(this.lblRuler, "lblRuler");
            this.lblRuler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblRuler.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblRuler.Name = "lblRuler";
            // 
            // cmboxRuler
            // 
            this.cmboxRuler.Depth = 0;
            this.cmboxRuler.DisplayMember = "Text";
            this.cmboxRuler.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmboxRuler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmboxRuler, "cmboxRuler");
            this.cmboxRuler.ForeColor = System.Drawing.Color.White;
            this.cmboxRuler.FormattingEnabled = true;
            this.cmboxRuler.Items.AddRange(new object[] {
            resources.GetString("cmboxRuler.Items"),
            resources.GetString("cmboxRuler.Items1"),
            resources.GetString("cmboxRuler.Items2")});
            this.cmboxRuler.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.cmboxRuler.Name = "cmboxRuler";
            this.cmboxRuler.DropDown += new System.EventHandler(this.cmboxRuler_DropDown);
            this.cmboxRuler.SelectedIndexChanged += new System.EventHandler(this.cmboxRuler_SelectedIndexChanged);
            this.cmboxRuler.DropDownClosed += new System.EventHandler(this.cmboxRuler_DropDownClosed);
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
            this.Controls.Add(this.sliderTargetSize);
            this.Controls.Add(this.lblTargerSize);
            this.Controls.Add(this.sliderThickness);
            this.Controls.Add(this.lblThickness);
            this.Controls.Add(this.sliderTransparency);
            this.Controls.Add(this.lblTransparency);
            this.DoubleBuffered = true;
            this.Name = "RulerAppearanceCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrRulerAppearanceTitle;
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.lblTransparency, 0);
            this.Controls.SetChildIndex(this.sliderTransparency, 0);
            this.Controls.SetChildIndex(this.lblThickness, 0);
            this.Controls.SetChildIndex(this.sliderThickness, 0);
            this.Controls.SetChildIndex(this.lblTargerSize, 0);
            this.Controls.SetChildIndex(this.sliderTargetSize, 0);
            this.Controls.SetChildIndex(this.lblZColour, 0);
            this.Controls.SetChildIndex(this.sliderColour, 0);
            this.Controls.SetChildIndex(this.btnLaserCtrl, 0);
            this.Controls.SetChildIndex(this.lblRuler, 0);
            this.Controls.SetChildIndex(this.cmboxRuler, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialRoundButton btnLaserCtrl;
        private MaterialSlider sliderColour;
        private MaterialLabel lblZColour;
        private MaterialSlider sliderTargetSize;
        private MaterialLabel lblTargerSize;
        private MaterialSlider sliderThickness;
        private MaterialLabel lblThickness;
        private MaterialSlider sliderTransparency;
        private MaterialLabel lblTransparency;
        private MaterialLabel lblRuler;
        private MaterialComboBox cmboxRuler;
    }
}
