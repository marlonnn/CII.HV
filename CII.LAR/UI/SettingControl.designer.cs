namespace CII.LAR.UI
{
    partial class SettingControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingControl));
            this.gropBoxSystemInfo = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.cmbImage = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.lblSimulatorImage = new CII.LAR.MaterialSkin.MaterialLabel();
            this.btnSimulator = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.lblCameraStatus = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblCamera = new CII.LAR.MaterialSkin.MaterialLabel();
            this.groupBoxLaser = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.cmbLaser = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.groupBoxLanguage = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.comboBoxItemLanguage = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.groupBoxStoragePath = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.textBoxItemStoragePath = new CII.LAR.MaterialSkin.MaterialTextBox();
            this.groupBoxObjectLense = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.materialRoundButton1 = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.groupBoxCoefficient = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.cbxScale = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.groupBoxTime = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.cmbTime = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.groupBoxShortcuts = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.btnShortcuts = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.groupBoxScale = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.btnScaleAppearance = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.gropBoxSystemInfo.SuspendLayout();
            this.groupBoxLaser.SuspendLayout();
            this.groupBoxLanguage.SuspendLayout();
            this.groupBoxStoragePath.SuspendLayout();
            this.groupBoxObjectLense.SuspendLayout();
            this.groupBoxCoefficient.SuspendLayout();
            this.groupBoxTime.SuspendLayout();
            this.groupBoxShortcuts.SuspendLayout();
            this.groupBoxScale.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // gropBoxSystemInfo
            // 
            this.gropBoxSystemInfo.Controls.Add(this.cmbImage);
            this.gropBoxSystemInfo.Controls.Add(this.lblSimulatorImage);
            this.gropBoxSystemInfo.Controls.Add(this.btnSimulator);
            this.gropBoxSystemInfo.Controls.Add(this.lblCameraStatus);
            this.gropBoxSystemInfo.Controls.Add(this.lblCamera);
            this.gropBoxSystemInfo.Depth = 0;
            resources.ApplyResources(this.gropBoxSystemInfo, "gropBoxSystemInfo");
            this.gropBoxSystemInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.gropBoxSystemInfo.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.gropBoxSystemInfo.Name = "gropBoxSystemInfo";
            this.gropBoxSystemInfo.TabStop = false;
            // 
            // cmbImage
            // 
            this.cmbImage.Depth = 0;
            this.cmbImage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbImage, "cmbImage");
            this.cmbImage.ForeColor = System.Drawing.Color.White;
            this.cmbImage.FormattingEnabled = true;
            this.cmbImage.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.cmbImage.Name = "cmbImage";
            this.cmbImage.SelectedIndexChanged += new System.EventHandler(this.cmbImage_SelectedIndexChanged);
            // 
            // lblSimulatorImage
            // 
            resources.ApplyResources(this.lblSimulatorImage, "lblSimulatorImage");
            this.lblSimulatorImage.Depth = 0;
            this.lblSimulatorImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblSimulatorImage.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblSimulatorImage.Name = "lblSimulatorImage";
            // 
            // btnSimulator
            // 
            resources.ApplyResources(this.btnSimulator, "btnSimulator");
            this.btnSimulator.Depth = 0;
            this.btnSimulator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.btnSimulator.Icon = null;
            this.btnSimulator.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnSimulator.Name = "btnSimulator";
            this.btnSimulator.Primary = false;
            this.btnSimulator.UseVisualStyleBackColor = true;
            this.btnSimulator.Click += new System.EventHandler(this.btnSimulator_Click);
            // 
            // lblCameraStatus
            // 
            resources.ApplyResources(this.lblCameraStatus, "lblCameraStatus");
            this.lblCameraStatus.Depth = 0;
            this.lblCameraStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblCameraStatus.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblCameraStatus.Name = "lblCameraStatus";
            // 
            // lblCamera
            // 
            resources.ApplyResources(this.lblCamera, "lblCamera");
            this.lblCamera.Depth = 0;
            this.lblCamera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblCamera.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblCamera.Name = "lblCamera";
            // 
            // groupBoxLaser
            // 
            this.groupBoxLaser.Controls.Add(this.cmbLaser);
            this.groupBoxLaser.Depth = 0;
            resources.ApplyResources(this.groupBoxLaser, "groupBoxLaser");
            this.groupBoxLaser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.groupBoxLaser.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.groupBoxLaser.Name = "groupBoxLaser";
            this.groupBoxLaser.TabStop = false;
            // 
            // cmbLaser
            // 
            this.cmbLaser.Depth = 0;
            this.cmbLaser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLaser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbLaser, "cmbLaser");
            this.cmbLaser.ForeColor = System.Drawing.Color.White;
            this.cmbLaser.FormattingEnabled = true;
            this.cmbLaser.Items.AddRange(new object[] {
            resources.GetString("cmbLaser.Items"),
            resources.GetString("cmbLaser.Items1")});
            this.cmbLaser.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.cmbLaser.Name = "cmbLaser";
            // 
            // groupBoxLanguage
            // 
            this.groupBoxLanguage.Controls.Add(this.comboBoxItemLanguage);
            this.groupBoxLanguage.Depth = 0;
            resources.ApplyResources(this.groupBoxLanguage, "groupBoxLanguage");
            this.groupBoxLanguage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.groupBoxLanguage.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.groupBoxLanguage.Name = "groupBoxLanguage";
            this.groupBoxLanguage.TabStop = false;
            // 
            // comboBoxItemLanguage
            // 
            this.comboBoxItemLanguage.Depth = 0;
            this.comboBoxItemLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxItemLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxItemLanguage, "comboBoxItemLanguage");
            this.comboBoxItemLanguage.ForeColor = System.Drawing.Color.White;
            this.comboBoxItemLanguage.FormattingEnabled = true;
            this.comboBoxItemLanguage.Items.AddRange(new object[] {
            resources.GetString("comboBoxItemLanguage.Items"),
            resources.GetString("comboBoxItemLanguage.Items1")});
            this.comboBoxItemLanguage.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.comboBoxItemLanguage.Name = "comboBoxItemLanguage";
            this.comboBoxItemLanguage.SelectedIndexChanged += new System.EventHandler(this.ComboBoxItemLanguage_SelectedIndexChanged);
            // 
            // groupBoxStoragePath
            // 
            this.groupBoxStoragePath.Controls.Add(this.textBoxItemStoragePath);
            this.groupBoxStoragePath.Depth = 0;
            resources.ApplyResources(this.groupBoxStoragePath, "groupBoxStoragePath");
            this.groupBoxStoragePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.groupBoxStoragePath.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.groupBoxStoragePath.Name = "groupBoxStoragePath";
            this.groupBoxStoragePath.TabStop = false;
            // 
            // textBoxItemStoragePath
            // 
            this.textBoxItemStoragePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.textBoxItemStoragePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxItemStoragePath.CustomAutoSize = true;
            this.textBoxItemStoragePath.Depth = 0;
            this.textBoxItemStoragePath.EmptyTextTip = null;
            this.textBoxItemStoragePath.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.textBoxItemStoragePath, "textBoxItemStoragePath");
            this.textBoxItemStoragePath.ForeColor = System.Drawing.Color.White;
            this.textBoxItemStoragePath.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.textBoxItemStoragePath.Name = "textBoxItemStoragePath";
            this.textBoxItemStoragePath.Radius = 3;
            // 
            // groupBoxObjectLense
            // 
            this.groupBoxObjectLense.Controls.Add(this.materialRoundButton1);
            this.groupBoxObjectLense.Depth = 0;
            resources.ApplyResources(this.groupBoxObjectLense, "groupBoxObjectLense");
            this.groupBoxObjectLense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.groupBoxObjectLense.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.groupBoxObjectLense.Name = "groupBoxObjectLense";
            this.groupBoxObjectLense.TabStop = false;
            // 
            // materialRoundButton1
            // 
            resources.ApplyResources(this.materialRoundButton1, "materialRoundButton1");
            this.materialRoundButton1.Depth = 0;
            this.materialRoundButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialRoundButton1.Icon = null;
            this.materialRoundButton1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialRoundButton1.Name = "materialRoundButton1";
            this.materialRoundButton1.Primary = false;
            this.materialRoundButton1.UseVisualStyleBackColor = true;
            this.materialRoundButton1.Click += new System.EventHandler(this.materialRoundButton1_Click);
            // 
            // groupBoxCoefficient
            // 
            this.groupBoxCoefficient.Controls.Add(this.cbxScale);
            this.groupBoxCoefficient.Depth = 0;
            resources.ApplyResources(this.groupBoxCoefficient, "groupBoxCoefficient");
            this.groupBoxCoefficient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.groupBoxCoefficient.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.groupBoxCoefficient.Name = "groupBoxCoefficient";
            this.groupBoxCoefficient.TabStop = false;
            // 
            // cbxScale
            // 
            this.cbxScale.Depth = 0;
            this.cbxScale.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbxScale, "cbxScale");
            this.cbxScale.ForeColor = System.Drawing.Color.White;
            this.cbxScale.FormattingEnabled = true;
            this.cbxScale.Items.AddRange(new object[] {
            resources.GetString("cbxScale.Items"),
            resources.GetString("cbxScale.Items1"),
            resources.GetString("cbxScale.Items2"),
            resources.GetString("cbxScale.Items3"),
            resources.GetString("cbxScale.Items4"),
            resources.GetString("cbxScale.Items5")});
            this.cbxScale.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.cbxScale.Name = "cbxScale";
            // 
            // groupBoxTime
            // 
            this.groupBoxTime.Controls.Add(this.cmbTime);
            this.groupBoxTime.Depth = 0;
            resources.ApplyResources(this.groupBoxTime, "groupBoxTime");
            this.groupBoxTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.groupBoxTime.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.groupBoxTime.Name = "groupBoxTime";
            this.groupBoxTime.TabStop = false;
            // 
            // cmbTime
            // 
            this.cmbTime.Depth = 0;
            this.cmbTime.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbTime, "cmbTime");
            this.cmbTime.ForeColor = System.Drawing.Color.White;
            this.cmbTime.FormattingEnabled = true;
            this.cmbTime.Items.AddRange(new object[] {
            resources.GetString("cmbTime.Items"),
            resources.GetString("cmbTime.Items1"),
            resources.GetString("cmbTime.Items2"),
            resources.GetString("cmbTime.Items3"),
            resources.GetString("cmbTime.Items4"),
            resources.GetString("cmbTime.Items5"),
            resources.GetString("cmbTime.Items6"),
            resources.GetString("cmbTime.Items7"),
            resources.GetString("cmbTime.Items8"),
            resources.GetString("cmbTime.Items9")});
            this.cmbTime.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.cmbTime.Name = "cmbTime";
            this.cmbTime.SelectedIndexChanged += new System.EventHandler(this.cmbTime_SelectedIndexChanged);
            // 
            // groupBoxShortcuts
            // 
            this.groupBoxShortcuts.Controls.Add(this.btnShortcuts);
            this.groupBoxShortcuts.Depth = 0;
            resources.ApplyResources(this.groupBoxShortcuts, "groupBoxShortcuts");
            this.groupBoxShortcuts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.groupBoxShortcuts.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.groupBoxShortcuts.Name = "groupBoxShortcuts";
            this.groupBoxShortcuts.TabStop = false;
            // 
            // btnShortcuts
            // 
            resources.ApplyResources(this.btnShortcuts, "btnShortcuts");
            this.btnShortcuts.Depth = 0;
            this.btnShortcuts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.btnShortcuts.Icon = null;
            this.btnShortcuts.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnShortcuts.Name = "btnShortcuts";
            this.btnShortcuts.Primary = false;
            this.btnShortcuts.UseVisualStyleBackColor = true;
            this.btnShortcuts.Click += new System.EventHandler(this.btnShortcuts_Click);
            // 
            // groupBoxScale
            // 
            this.groupBoxScale.Controls.Add(this.btnScaleAppearance);
            this.groupBoxScale.Depth = 0;
            resources.ApplyResources(this.groupBoxScale, "groupBoxScale");
            this.groupBoxScale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.groupBoxScale.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.groupBoxScale.Name = "groupBoxScale";
            this.groupBoxScale.TabStop = false;
            // 
            // btnScaleAppearance
            // 
            resources.ApplyResources(this.btnScaleAppearance, "btnScaleAppearance");
            this.btnScaleAppearance.Depth = 0;
            this.btnScaleAppearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.btnScaleAppearance.Icon = null;
            this.btnScaleAppearance.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnScaleAppearance.Name = "btnScaleAppearance";
            this.btnScaleAppearance.Primary = false;
            this.btnScaleAppearance.UseVisualStyleBackColor = true;
            this.btnScaleAppearance.Click += new System.EventHandler(this.btnScaleAppearance_Click);
            // 
            // SettingControl
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBoxScale);
            this.Controls.Add(this.groupBoxShortcuts);
            this.Controls.Add(this.groupBoxTime);
            this.Controls.Add(this.groupBoxCoefficient);
            this.Controls.Add(this.groupBoxObjectLense);
            this.Controls.Add(this.groupBoxStoragePath);
            this.Controls.Add(this.groupBoxLanguage);
            this.Controls.Add(this.groupBoxLaser);
            this.Controls.Add(this.gropBoxSystemInfo);
            this.Name = "SettingControl";
            this.Title = "Setting";
            this.Controls.SetChildIndex(this.gropBoxSystemInfo, 0);
            this.Controls.SetChildIndex(this.groupBoxLaser, 0);
            this.Controls.SetChildIndex(this.groupBoxLanguage, 0);
            this.Controls.SetChildIndex(this.groupBoxStoragePath, 0);
            this.Controls.SetChildIndex(this.groupBoxObjectLense, 0);
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.groupBoxCoefficient, 0);
            this.Controls.SetChildIndex(this.groupBoxTime, 0);
            this.Controls.SetChildIndex(this.groupBoxShortcuts, 0);
            this.Controls.SetChildIndex(this.groupBoxScale, 0);
            this.gropBoxSystemInfo.ResumeLayout(false);
            this.gropBoxSystemInfo.PerformLayout();
            this.groupBoxLaser.ResumeLayout(false);
            this.groupBoxLanguage.ResumeLayout(false);
            this.groupBoxStoragePath.ResumeLayout(false);
            this.groupBoxStoragePath.PerformLayout();
            this.groupBoxObjectLense.ResumeLayout(false);
            this.groupBoxCoefficient.ResumeLayout(false);
            this.groupBoxTime.ResumeLayout(false);
            this.groupBoxShortcuts.ResumeLayout(false);
            this.groupBoxScale.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private MaterialSkin.MaterialLabel lblCamera;
        private MaterialSkin.MaterialLabel lblCameraStatus;
        private MaterialSkin.MaterialRoundButton btnSimulator;
        private MaterialSkin.MaterialGroupBox groupBoxLaser;
        private MaterialSkin.MaterialComboBox cmbLaser;
        private MaterialSkin.MaterialLabel lblSimulatorImage;
        private MaterialSkin.MaterialComboBox cmbImage;
        private MaterialSkin.MaterialGroupBox groupBoxLanguage;
        private MaterialSkin.MaterialComboBox comboBoxItemLanguage;
        private MaterialSkin.MaterialGroupBox groupBoxStoragePath;
        private MaterialSkin.MaterialTextBox textBoxItemStoragePath;
        private MaterialSkin.MaterialGroupBox groupBoxObjectLense;
        private MaterialSkin.MaterialRoundButton materialRoundButton1;
        private MaterialSkin.MaterialGroupBox groupBoxCoefficient;
        private MaterialSkin.MaterialComboBox cbxScale;
        private MaterialSkin.MaterialGroupBox groupBoxTime;
        private MaterialSkin.MaterialComboBox cmbTime;
        private MaterialSkin.MaterialGroupBox groupBoxShortcuts;
        private MaterialSkin.MaterialRoundButton btnShortcuts;
        private MaterialSkin.MaterialGroupBox groupBoxScale;
        private MaterialSkin.MaterialRoundButton btnScaleAppearance;
        private MaterialSkin.MaterialGroupBox gropBoxSystemInfo;
    }
}
