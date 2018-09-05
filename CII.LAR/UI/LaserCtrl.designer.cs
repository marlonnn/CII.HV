using CII.LAR.MaterialSkin;
using System;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    partial class LaserCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaserCtrl));
            this.sliderCtrl = new CII.LAR.UI.SliderCtrl();
            this.btnFire = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.btnAlignLaser = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.btnHoleSize = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.btnAppearance = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.holesSlider = new DevComponents.DotNetBar.Controls.Slider();
            this.btnDelete = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.comboBoxEx1 = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.btnSave = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.lblHoleNumber = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialGroupBox1 = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.materialGroupBox2 = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.btnStop = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.btnRedLaser = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.textBox = new TextBox();
            this.materialGroupBox1.SuspendLayout();
            this.materialGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // sliderCtrl
            // 
            resources.ApplyResources(this.sliderCtrl, "sliderCtrl");
            this.sliderCtrl.Name = "sliderCtrl";
            this.sliderCtrl.Update = true;
            this.sliderCtrl.SliderValueChangedHandler += SliderValueChangedHandler;
            // 
            // btnFire
            // 
            this.btnFire.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnFire, "btnFire");
            this.btnFire.BackColor = System.Drawing.Color.LightYellow;
            this.btnFire.Depth = 0;
            this.btnFire.Icon = null;
            this.btnFire.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnFire.Name = "btnFire";
            this.btnFire.Primary = false;
            this.btnFire.UseVisualStyleBackColor = false;
            this.btnFire.Warning = false;
            this.btnFire.Click += new System.EventHandler(this.btnFire_Click);
            // 
            // btnAlignLaser
            // 
            this.btnAlignLaser.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnAlignLaser, "btnAlignLaser");
            this.btnAlignLaser.Depth = 0;
            this.btnAlignLaser.Icon = null;
            this.btnAlignLaser.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnAlignLaser.Name = "btnAlignLaser";
            this.btnAlignLaser.Primary = false;
            this.btnAlignLaser.Warning = false;
            this.btnAlignLaser.Click += new System.EventHandler(this.btnAlignLaser_Click);
            // 
            // btnHoleSize
            // 
            this.btnHoleSize.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnHoleSize, "btnHoleSize");
            this.btnHoleSize.Depth = 0;
            this.btnHoleSize.Icon = null;
            this.btnHoleSize.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnHoleSize.Name = "btnHoleSize";
            this.btnHoleSize.Primary = false;
            this.btnHoleSize.Warning = false;
            this.btnHoleSize.Click += new System.EventHandler(this.btnHoleSize_Click);
            // 
            // btnAppearance
            // 
            this.btnAppearance.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnAppearance, "btnAppearance");
            this.btnAppearance.Depth = 0;
            this.btnAppearance.Icon = null;
            this.btnAppearance.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnAppearance.Name = "btnAppearance";
            this.btnAppearance.Primary = false;
            this.btnAppearance.Warning = false;
            this.btnAppearance.Click += new System.EventHandler(this.btnAppearance_Click);
            // 
            // holesSlider
            // 
            // 
            // 
            // 
            this.holesSlider.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.holesSlider.LabelPosition = DevComponents.DotNetBar.eSliderLabelPosition.Right;
            this.holesSlider.LabelVisible = false;
            resources.ApplyResources(this.holesSlider, "holesSlider");
            this.holesSlider.Name = "holesSlider";
            this.holesSlider.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.holesSlider.Value = 0;
            this.holesSlider.ValueChanged += new System.EventHandler(this.holesSlider_ValueChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Depth = 0;
            this.btnDelete.Icon = null;
            this.btnDelete.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Primary = false;
            this.btnDelete.Warning = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.Depth = 0;
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxEx1, "comboBoxEx1");
            this.comboBoxEx1.ForeColor = System.Drawing.Color.White;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.comboBoxEx1.Name = "comboBoxEx1";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Depth = 0;
            this.btnSave.Icon = null;
            this.btnSave.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnSave.Name = "btnSave";
            this.btnSave.Primary = false;
            this.btnSave.Warning = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblHoleNumber
            // 
            this.lblHoleNumber.Depth = 0;
            resources.ApplyResources(this.lblHoleNumber, "lblHoleNumber");
            this.lblHoleNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblHoleNumber.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblHoleNumber.Name = "lblHoleNumber";
            // 
            // materialGroupBox1
            // 
            this.materialGroupBox1.Controls.Add(this.lblHoleNumber);
            this.materialGroupBox1.Controls.Add(this.sliderCtrl);
            this.materialGroupBox1.Controls.Add(this.holesSlider);
            this.materialGroupBox1.Depth = 0;
            resources.ApplyResources(this.materialGroupBox1, "materialGroupBox1");
            this.materialGroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialGroupBox1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialGroupBox1.Name = "materialGroupBox1";
            this.materialGroupBox1.TabStop = false;
            // 
            // materialGroupBox2
            // 
            this.materialGroupBox2.Controls.Add(this.btnDelete);
            this.materialGroupBox2.Controls.Add(this.btnSave);
            this.materialGroupBox2.Controls.Add(this.comboBoxEx1);
            this.materialGroupBox2.Depth = 0;
            resources.ApplyResources(this.materialGroupBox2, "materialGroupBox2");
            this.materialGroupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialGroupBox2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialGroupBox2.Name = "materialGroupBox2";
            this.materialGroupBox2.TabStop = false;
            // 
            // btnStop
            // 
            this.btnStop.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnStop, "btnStop");
            this.btnStop.BackColor = System.Drawing.Color.LightYellow;
            this.btnStop.Depth = 0;
            this.btnStop.Icon = null;
            this.btnStop.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnStop.Name = "btnStop";
            this.btnStop.Primary = false;
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Warning = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRedLaser
            // 
            this.btnRedLaser.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnRedLaser, "btnRedLaser");
            this.btnRedLaser.BackColor = System.Drawing.Color.LightYellow;
            this.btnRedLaser.Depth = 0;
            this.btnRedLaser.Icon = null;
            this.btnRedLaser.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnRedLaser.Name = "btnRedLaser";
            this.btnRedLaser.Primary = false;
            this.btnRedLaser.UseVisualStyleBackColor = false;
            this.btnRedLaser.Warning = false;
            this.btnRedLaser.Click += new System.EventHandler(this.btnRedLaser_Click);
            // 
            // LaserCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.textBox.Visible = false;
            this.Controls.Add(textBox);
            this.Controls.Add(this.btnRedLaser);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.materialGroupBox2);
            this.Controls.Add(this.btnAppearance);
            this.Controls.Add(this.btnHoleSize);
            this.Controls.Add(this.btnAlignLaser);
            this.Controls.Add(this.btnFire);
            this.Controls.Add(this.materialGroupBox1);
            this.Name = "LaserCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrLaserCtrlTitle;
            this.Controls.SetChildIndex(this.materialGroupBox1, 0);
            this.Controls.SetChildIndex(this.btnFire, 0);
            this.Controls.SetChildIndex(this.btnAlignLaser, 0);
            this.Controls.SetChildIndex(this.btnHoleSize, 0);
            this.Controls.SetChildIndex(this.btnAppearance, 0);
            this.Controls.SetChildIndex(this.materialGroupBox2, 0);
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.btnStop, 0);
            this.Controls.SetChildIndex(this.btnRedLaser, 0);
            this.materialGroupBox1.ResumeLayout(false);
            this.materialGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private SliderCtrl sliderCtrl;
        private MaterialRoundButton btnFire;
        private MaterialRoundButton btnAlignLaser;
        private MaterialRoundButton btnHoleSize;
        private MaterialRoundButton btnAppearance;
        private DevComponents.DotNetBar.Controls.Slider holesSlider;
        private MaterialRoundButton btnDelete;
        private MaterialComboBox comboBoxEx1;
        private MaterialRoundButton btnSave;
        private MaterialLabel lblHoleNumber;
        private MaterialGroupBox materialGroupBox1;
        private MaterialGroupBox materialGroupBox2;
        private MaterialRoundButton btnStop;
        private MaterialRoundButton btnRedLaser;
        private TextBox textBox;
    }
}
