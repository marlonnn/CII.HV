namespace CII.LAR.UI
{
    partial class LaserDebugControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn70 = new System.Windows.Forms.Button();
            this.slider = new DevComponents.DotNetBar.Controls.Slider();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxLaser = new System.Windows.Forms.GroupBox();
            this.lblComName = new System.Windows.Forms.Label();
            this.laserStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBoxLaser.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn70);
            this.groupBox1.Controls.Add(this.slider);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 97);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Laser Control";
            // 
            // btn70
            // 
            this.btn70.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn70.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btn70.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn70.Location = new System.Drawing.Point(10, 22);
            this.btn70.Name = "btn70";
            this.btn70.Size = new System.Drawing.Size(299, 36);
            this.btn70.TabIndex = 34;
            this.btn70.Text = "Open";
            this.btn70.UseVisualStyleBackColor = true;
            this.btn70.Click += new System.EventHandler(this.btn70_Click);
            // 
            // slider
            // 
            // 
            // 
            // 
            this.slider.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider.Location = new System.Drawing.Point(62, 71);
            this.slider.Maximum = 81;
            this.slider.Minimum = 1;
            this.slider.Name = "slider";
            this.slider.Size = new System.Drawing.Size(251, 23);
            this.slider.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider.TabIndex = 1;
            this.slider.Text = "1";
            this.slider.Value = 1;
            this.slider.ValueChanged += new System.EventHandler(this.slider_ValueChanged);
            this.slider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.slider_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "Red Laser";
            // 
            // groupBoxLaser
            // 
            this.groupBoxLaser.Controls.Add(this.lblComName);
            this.groupBoxLaser.Controls.Add(this.laserStatus);
            this.groupBoxLaser.Controls.Add(this.label3);
            this.groupBoxLaser.Location = new System.Drawing.Point(12, 12);
            this.groupBoxLaser.Name = "groupBoxLaser";
            this.groupBoxLaser.Size = new System.Drawing.Size(319, 58);
            this.groupBoxLaser.TabIndex = 36;
            this.groupBoxLaser.TabStop = false;
            this.groupBoxLaser.Text = "Laser Serial Port";
            // 
            // lblComName
            // 
            this.lblComName.AutoSize = true;
            this.lblComName.Location = new System.Drawing.Point(78, 20);
            this.lblComName.Name = "lblComName";
            this.lblComName.Size = new System.Drawing.Size(41, 12);
            this.lblComName.TabIndex = 26;
            this.lblComName.Text = "label2";
            // 
            // laserStatus
            // 
            this.laserStatus.AutoSize = true;
            this.laserStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.laserStatus.Location = new System.Drawing.Point(219, 20);
            this.laserStatus.Name = "laserStatus";
            this.laserStatus.Size = new System.Drawing.Size(83, 12);
            this.laserStatus.TabIndex = 25;
            this.laserStatus.Text = "Not Connected";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Port Name:";
            // 
            // LaserDebugControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 184);
            this.Controls.Add(this.groupBoxLaser);
            this.Controls.Add(this.groupBox1);
            this.Name = "LaserDebugControl";
            this.Text = "LaserDebugControl";
            this.Load += new System.EventHandler(this.LaserDebugControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxLaser.ResumeLayout(false);
            this.groupBoxLaser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn70;
        private DevComponents.DotNetBar.Controls.Slider slider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxLaser;
        private System.Windows.Forms.Label laserStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblComName;
    }
}