namespace CII.LAR.UI
{
    partial class SystemInfoCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemInfoCtrl));
            this.materialLabel2 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblMotorStatus = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel5 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblLaserStatus = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblLaserVersion = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel8 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblWorkingHour = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel10 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialGroupBox1 = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.materialGroupBox2 = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.btnBack = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.materialGroupBox1.SuspendLayout();
            this.materialGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // materialLabel2
            // 
            resources.ApplyResources(this.materialLabel2, "materialLabel2");
            this.materialLabel2.Depth = 0;
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            // 
            // lblMotorStatus
            // 
            resources.ApplyResources(this.lblMotorStatus, "lblMotorStatus");
            this.lblMotorStatus.Depth = 0;
            this.lblMotorStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblMotorStatus.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblMotorStatus.Name = "lblMotorStatus";
            // 
            // materialLabel5
            // 
            resources.ApplyResources(this.materialLabel5, "materialLabel5");
            this.materialLabel5.Depth = 0;
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel5.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            // 
            // lblLaserStatus
            // 
            resources.ApplyResources(this.lblLaserStatus, "lblLaserStatus");
            this.lblLaserStatus.Depth = 0;
            this.lblLaserStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblLaserStatus.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblLaserStatus.Name = "lblLaserStatus";
            // 
            // lblLaserVersion
            // 
            resources.ApplyResources(this.lblLaserVersion, "lblLaserVersion");
            this.lblLaserVersion.Depth = 0;
            this.lblLaserVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblLaserVersion.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblLaserVersion.Name = "lblLaserVersion";
            // 
            // materialLabel8
            // 
            resources.ApplyResources(this.materialLabel8, "materialLabel8");
            this.materialLabel8.Depth = 0;
            this.materialLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel8.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel8.Name = "materialLabel8";
            // 
            // lblWorkingHour
            // 
            resources.ApplyResources(this.lblWorkingHour, "lblWorkingHour");
            this.lblWorkingHour.Depth = 0;
            this.lblWorkingHour.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblWorkingHour.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblWorkingHour.Name = "lblWorkingHour";
            // 
            // materialLabel10
            // 
            resources.ApplyResources(this.materialLabel10, "materialLabel10");
            this.materialLabel10.Depth = 0;
            this.materialLabel10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel10.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel10.Name = "materialLabel10";
            // 
            // materialGroupBox1
            // 
            this.materialGroupBox1.Controls.Add(this.lblLaserVersion);
            this.materialGroupBox1.Controls.Add(this.lblWorkingHour);
            this.materialGroupBox1.Controls.Add(this.materialLabel2);
            this.materialGroupBox1.Controls.Add(this.materialLabel10);
            this.materialGroupBox1.Controls.Add(this.lblLaserStatus);
            this.materialGroupBox1.Controls.Add(this.materialLabel8);
            this.materialGroupBox1.Depth = 0;
            resources.ApplyResources(this.materialGroupBox1, "materialGroupBox1");
            this.materialGroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialGroupBox1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialGroupBox1.Name = "materialGroupBox1";
            this.materialGroupBox1.TabStop = false;
            // 
            // materialGroupBox2
            // 
            this.materialGroupBox2.Controls.Add(this.lblMotorStatus);
            this.materialGroupBox2.Controls.Add(this.materialLabel5);
            this.materialGroupBox2.Depth = 0;
            resources.ApplyResources(this.materialGroupBox2, "materialGroupBox2");
            this.materialGroupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialGroupBox2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialGroupBox2.Name = "materialGroupBox2";
            this.materialGroupBox2.TabStop = false;
            // 
            // btnBack
            // 
            resources.ApplyResources(this.btnBack, "btnBack");
            this.btnBack.Depth = 0;
            this.btnBack.Icon = null;
            this.btnBack.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnBack.Name = "btnBack";
            this.btnBack.Primary = false;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Warning = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // SystemInfoCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.materialGroupBox2);
            this.Controls.Add(this.materialGroupBox1);
            this.Name = "SystemInfoCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrSystemInfoTitle;
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.materialGroupBox1, 0);
            this.Controls.SetChildIndex(this.materialGroupBox2, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.materialGroupBox1.ResumeLayout(false);
            this.materialGroupBox1.PerformLayout();
            this.materialGroupBox2.ResumeLayout(false);
            this.materialGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.MaterialLabel materialLabel2;
        private MaterialSkin.MaterialLabel lblMotorStatus;
        private MaterialSkin.MaterialLabel materialLabel5;
        private MaterialSkin.MaterialLabel lblLaserStatus;
        private MaterialSkin.MaterialLabel lblLaserVersion;
        private MaterialSkin.MaterialLabel materialLabel8;
        private MaterialSkin.MaterialLabel lblWorkingHour;
        private MaterialSkin.MaterialLabel materialLabel10;
        private MaterialSkin.MaterialGroupBox materialGroupBox1;
        private MaterialSkin.MaterialGroupBox materialGroupBox2;
        private MaterialSkin.MaterialRoundButton btnBack;
    }
}
