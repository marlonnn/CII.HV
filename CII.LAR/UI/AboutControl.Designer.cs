namespace CII.LAR.UI
{
    partial class AboutControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutControl));
            this.materialLabel1 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel2 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel3 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel4 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel5 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.materialRoundButton1 = new CII.LAR.MaterialSkin.MaterialRoundButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // materialLabel1
            // 
            resources.ApplyResources(this.materialLabel1, "materialLabel1");
            this.materialLabel1.Depth = 0;
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            // 
            // materialLabel2
            // 
            this.materialLabel2.Depth = 0;
            resources.ApplyResources(this.materialLabel2, "materialLabel2");
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            // 
            // materialLabel3
            // 
            resources.ApplyResources(this.materialLabel3, "materialLabel3");
            this.materialLabel3.Depth = 0;
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel3.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            // 
            // materialLabel4
            // 
            resources.ApplyResources(this.materialLabel4, "materialLabel4");
            this.materialLabel4.Depth = 0;
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel4.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            // 
            // materialLabel5
            // 
            this.materialLabel5.Depth = 0;
            resources.ApplyResources(this.materialLabel5, "materialLabel5");
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel5.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CII.LAR.Properties.Resources.company;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // materialRoundButton1
            // 
            resources.ApplyResources(this.materialRoundButton1, "materialRoundButton1");
            this.materialRoundButton1.Depth = 0;
            this.materialRoundButton1.Icon = null;
            this.materialRoundButton1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialRoundButton1.Name = "materialRoundButton1";
            this.materialRoundButton1.Primary = false;
            this.materialRoundButton1.UseVisualStyleBackColor = true;
            this.materialRoundButton1.Warning = false;
            this.materialRoundButton1.Click += new System.EventHandler(this.materialRoundButton1_Click);
            // 
            // AboutControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.materialRoundButton1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Name = "AboutControl";
            this.Title = global::CII.LAR.Properties.Resources.StrAboutCtrlTitle;
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.materialLabel1, 0);
            this.Controls.SetChildIndex(this.materialLabel2, 0);
            this.Controls.SetChildIndex(this.materialLabel3, 0);
            this.Controls.SetChildIndex(this.materialLabel4, 0);
            this.Controls.SetChildIndex(this.materialLabel5, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.materialRoundButton1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.MaterialLabel materialLabel1;
        private MaterialSkin.MaterialLabel materialLabel2;
        private MaterialSkin.MaterialLabel materialLabel3;
        private MaterialSkin.MaterialLabel materialLabel4;
        private MaterialSkin.MaterialLabel materialLabel5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.MaterialRoundButton materialRoundButton1;
    }
}
