namespace CII.LAR.UI
{
    partial class VideoPropertyForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblCamera = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sliderContract = new DevComponents.DotNetBar.Controls.Slider();
            this.sliderBright = new DevComponents.DotNetBar.Controls.Slider();
            this.label3 = new System.Windows.Forms.Label();
            this.sliderHue = new DevComponents.DotNetBar.Controls.Slider();
            this.label4 = new System.Windows.Forms.Label();
            this.slideSaturation = new DevComponents.DotNetBar.Controls.Slider();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "相机：";
            // 
            // lblCamera
            // 
            this.lblCamera.AutoSize = true;
            this.lblCamera.Location = new System.Drawing.Point(61, 25);
            this.lblCamera.Name = "lblCamera";
            this.lblCamera.Size = new System.Drawing.Size(35, 12);
            this.lblCamera.TabIndex = 1;
            this.lblCamera.Text = "相机1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.slideSaturation);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.sliderHue);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.sliderBright);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.sliderContract);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(10, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 263);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "对比度：";
            // 
            // sliderContract
            // 
            // 
            // 
            // 
            this.sliderContract.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderContract.Location = new System.Drawing.Point(78, 31);
            this.sliderContract.Name = "sliderContract";
            this.sliderContract.Size = new System.Drawing.Size(308, 23);
            this.sliderContract.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderContract.TabIndex = 1;
            this.sliderContract.Text = "slider1";
            this.sliderContract.Value = 0;
            // 
            // sliderBright
            // 
            // 
            // 
            // 
            this.sliderBright.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderBright.Location = new System.Drawing.Point(78, 74);
            this.sliderBright.Name = "sliderBright";
            this.sliderBright.Size = new System.Drawing.Size(308, 23);
            this.sliderBright.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderBright.TabIndex = 3;
            this.sliderBright.Text = "slider1";
            this.sliderBright.Value = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "  亮度：";
            // 
            // sliderHue
            // 
            // 
            // 
            // 
            this.sliderHue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderHue.Location = new System.Drawing.Point(78, 121);
            this.sliderHue.Name = "sliderHue";
            this.sliderHue.Size = new System.Drawing.Size(308, 23);
            this.sliderHue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderHue.TabIndex = 5;
            this.sliderHue.Text = "slider";
            this.sliderHue.Value = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "  色调：";
            // 
            // slideSaturation
            // 
            // 
            // 
            // 
            this.slideSaturation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slideSaturation.Location = new System.Drawing.Point(78, 168);
            this.slideSaturation.Name = "slideSaturation";
            this.slideSaturation.Size = new System.Drawing.Size(308, 23);
            this.slideSaturation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slideSaturation.TabIndex = 7;
            this.slideSaturation.Text = "slider1";
            this.slideSaturation.Value = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "饱和度：";
            // 
            // VideoPropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 349);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCamera);
            this.Controls.Add(this.label1);
            this.Name = "VideoPropertyForm";
            this.Text = "VideoPropertyForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCamera;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.Slider slideSaturation;
        private System.Windows.Forms.Label label5;
        private DevComponents.DotNetBar.Controls.Slider sliderHue;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.Slider sliderBright;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.Slider sliderContract;
        private System.Windows.Forms.Label label2;
    }
}