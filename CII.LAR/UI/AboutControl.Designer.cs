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
            this.materialLabel1 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel2 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel3 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel4 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialLabel5 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(415, 4);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel1.Location = new System.Drawing.Point(26, 36);
            this.materialLabel1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(130, 15);
            this.materialLabel1.TabIndex = 1;
            this.materialLabel1.Text = "LAR-100 激光破膜软件";
            // 
            // materialLabel2
            // 
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel2.Location = new System.Drawing.Point(26, 103);
            this.materialLabel2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(204, 19);
            this.materialLabel2.TabIndex = 3;
            this.materialLabel2.Text = "版权所有：华仪宁创，保留所有权利";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel3.Location = new System.Drawing.Point(26, 72);
            this.materialLabel3.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(105, 15);
            this.materialLabel3.TabIndex = 4;
            this.materialLabel3.Text = "Software Version: ";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel4.Location = new System.Drawing.Point(129, 72);
            this.materialLabel4.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(142, 15);
            this.materialLabel4.TabIndex = 5;
            this.materialLabel4.Text = "LAR-100.P003.T01A.032";
            // 
            // materialLabel5
            // 
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel5.Location = new System.Drawing.Point(26, 133);
            this.materialLabel5.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(250, 79);
            this.materialLabel5.TabIndex = 6;
            this.materialLabel5.Text = "警告：本计算机程序受版权和国际条约保护，如未经授权而擅自复制或传播程序（或其中任何部分），将受到严厉的民事和刑事制裁，并将在法律许可的最大限度内受到起诉。";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CII.LAR.Properties.Resources.company;
            this.pictureBox1.Location = new System.Drawing.Point(282, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 162);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // AboutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Name = "AboutControl";
            this.Size = new System.Drawing.Size(427, 280);
            this.Title = "About";
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.materialLabel1, 0);
            this.Controls.SetChildIndex(this.materialLabel2, 0);
            this.Controls.SetChildIndex(this.materialLabel3, 0);
            this.Controls.SetChildIndex(this.materialLabel4, 0);
            this.Controls.SetChildIndex(this.materialLabel5, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
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
    }
}
