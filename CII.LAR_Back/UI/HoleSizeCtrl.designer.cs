namespace CII.LAR.UI
{
    partial class HoleSizeCtrl
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
            this.btnUp = new System.Windows.Forms.Button();
            this.lblHoleSize = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.Image = global::CII.LAR.Properties.Resources.up;
            this.btnUp.Location = new System.Drawing.Point(66, 2);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(28, 17);
            this.btnUp.TabIndex = 0;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // lblHoleSize
            // 
            this.lblHoleSize.AutoSize = true;
            this.lblHoleSize.Location = new System.Drawing.Point(3, 11);
            this.lblHoleSize.Name = "lblHoleSize";
            this.lblHoleSize.Size = new System.Drawing.Size(48, 13);
            this.lblHoleSize.TabIndex = 1;
            this.lblHoleSize.Text = "0.001um";
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.Image = global::CII.LAR.Properties.Resources.down;
            this.btnDown.Location = new System.Drawing.Point(66, 18);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(28, 17);
            this.btnDown.TabIndex = 2;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // HoleSizeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.lblHoleSize);
            this.Controls.Add(this.btnUp);
            this.Name = "HoleSizeCtrl";
            this.Size = new System.Drawing.Size(100, 37);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Label lblHoleSize;
        private System.Windows.Forms.Button btnDown;
    }
}
