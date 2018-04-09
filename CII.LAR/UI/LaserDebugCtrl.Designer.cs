namespace CII.LAR.UI
{
    partial class LaserDebugCtrl
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
            this.slider = new DevComponents.DotNetBar.Controls.Slider();
            this.groupBoxLaser = new System.Windows.Forms.GroupBox();
            this.laserStatus = new System.Windows.Forms.Label();
            this.laserHandshakingcbx = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.laserDataBitsCbx = new System.Windows.Forms.ComboBox();
            this.laserComListCbx = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.laserOpenCloseSpbtn = new System.Windows.Forms.Button();
            this.laserBaudRateCbx = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.laserParityCbx = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.laserStopBitsCbx = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn70 = new System.Windows.Forms.Button();
            this.groupBoxLaser.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // slider
            // 
            // 
            // 
            // 
            this.slider.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider.Location = new System.Drawing.Point(62, 71);
            this.slider.Maximum = 82;
            this.slider.Name = "slider";
            this.slider.Size = new System.Drawing.Size(251, 23);
            this.slider.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider.TabIndex = 1;
            this.slider.Text = "1";
            this.slider.Value = 1;
            this.slider.ValueChanged += new System.EventHandler(this.slider_ValueChanged);
            this.slider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.slider_MouseUp);
            // 
            // groupBoxLaser
            // 
            this.groupBoxLaser.Controls.Add(this.laserStatus);
            this.groupBoxLaser.Controls.Add(this.laserHandshakingcbx);
            this.groupBoxLaser.Controls.Add(this.label9);
            this.groupBoxLaser.Controls.Add(this.laserDataBitsCbx);
            this.groupBoxLaser.Controls.Add(this.laserComListCbx);
            this.groupBoxLaser.Controls.Add(this.label3);
            this.groupBoxLaser.Controls.Add(this.laserOpenCloseSpbtn);
            this.groupBoxLaser.Controls.Add(this.laserBaudRateCbx);
            this.groupBoxLaser.Controls.Add(this.label7);
            this.groupBoxLaser.Controls.Add(this.label4);
            this.groupBoxLaser.Controls.Add(this.laserParityCbx);
            this.groupBoxLaser.Controls.Add(this.label5);
            this.groupBoxLaser.Controls.Add(this.label6);
            this.groupBoxLaser.Controls.Add(this.laserStopBitsCbx);
            this.groupBoxLaser.Location = new System.Drawing.Point(3, 19);
            this.groupBoxLaser.Name = "groupBoxLaser";
            this.groupBoxLaser.Size = new System.Drawing.Size(319, 325);
            this.groupBoxLaser.TabIndex = 32;
            this.groupBoxLaser.TabStop = false;
            this.groupBoxLaser.Text = "Laser Serial Port";
            // 
            // laserStatus
            // 
            this.laserStatus.AutoSize = true;
            this.laserStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.laserStatus.Location = new System.Drawing.Point(120, 300);
            this.laserStatus.Name = "laserStatus";
            this.laserStatus.Size = new System.Drawing.Size(83, 12);
            this.laserStatus.TabIndex = 25;
            this.laserStatus.Text = "Not Connected";
            // 
            // laserHandshakingcbx
            // 
            this.laserHandshakingcbx.FormattingEnabled = true;
            this.laserHandshakingcbx.Location = new System.Drawing.Point(14, 235);
            this.laserHandshakingcbx.Name = "laserHandshakingcbx";
            this.laserHandshakingcbx.Size = new System.Drawing.Size(299, 20);
            this.laserHandshakingcbx.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(7, 220);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "HandShaking:";
            // 
            // laserDataBitsCbx
            // 
            this.laserDataBitsCbx.FormattingEnabled = true;
            this.laserDataBitsCbx.Location = new System.Drawing.Point(14, 115);
            this.laserDataBitsCbx.Name = "laserDataBitsCbx";
            this.laserDataBitsCbx.Size = new System.Drawing.Size(299, 20);
            this.laserDataBitsCbx.TabIndex = 11;
            // 
            // laserComListCbx
            // 
            this.laserComListCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.laserComListCbx.FormattingEnabled = true;
            this.laserComListCbx.Location = new System.Drawing.Point(14, 35);
            this.laserComListCbx.Name = "laserComListCbx";
            this.laserComListCbx.Size = new System.Drawing.Size(299, 20);
            this.laserComListCbx.TabIndex = 7;
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
            // laserOpenCloseSpbtn
            // 
            this.laserOpenCloseSpbtn.Enabled = false;
            this.laserOpenCloseSpbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.laserOpenCloseSpbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.laserOpenCloseSpbtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.laserOpenCloseSpbtn.Location = new System.Drawing.Point(14, 261);
            this.laserOpenCloseSpbtn.Name = "laserOpenCloseSpbtn";
            this.laserOpenCloseSpbtn.Size = new System.Drawing.Size(299, 36);
            this.laserOpenCloseSpbtn.TabIndex = 17;
            this.laserOpenCloseSpbtn.Text = "Open";
            this.laserOpenCloseSpbtn.UseVisualStyleBackColor = true;
            this.laserOpenCloseSpbtn.Click += new System.EventHandler(this.laserOpenCloseSpbtn_Click);
            // 
            // laserBaudRateCbx
            // 
            this.laserBaudRateCbx.FormattingEnabled = true;
            this.laserBaudRateCbx.Location = new System.Drawing.Point(14, 75);
            this.laserBaudRateCbx.Name = "laserBaudRateCbx";
            this.laserBaudRateCbx.Size = new System.Drawing.Size(299, 20);
            this.laserBaudRateCbx.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(7, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "Parity:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(7, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Baud Rate:";
            // 
            // laserParityCbx
            // 
            this.laserParityCbx.FormattingEnabled = true;
            this.laserParityCbx.Location = new System.Drawing.Point(14, 195);
            this.laserParityCbx.Name = "laserParityCbx";
            this.laserParityCbx.Size = new System.Drawing.Size(299, 20);
            this.laserParityCbx.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(7, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "Data Bits:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(7, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Stop Bits:";
            // 
            // laserStopBitsCbx
            // 
            this.laserStopBitsCbx.FormattingEnabled = true;
            this.laserStopBitsCbx.Location = new System.Drawing.Point(14, 155);
            this.laserStopBitsCbx.Name = "laserStopBitsCbx";
            this.laserStopBitsCbx.Size = new System.Drawing.Size(299, 20);
            this.laserStopBitsCbx.TabIndex = 13;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn70);
            this.groupBox1.Controls.Add(this.slider);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 350);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 97);
            this.groupBox1.TabIndex = 34;
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
            this.btn70.Text = "70";
            this.btn70.UseVisualStyleBackColor = true;
            this.btn70.Click += new System.EventHandler(this.btn70_Click);
            // 
            // LaserDebugCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxLaser);
            this.Name = "LaserDebugCtrl";
            this.Text = "Laser Debug Form";
            this.groupBoxLaser.ResumeLayout(false);
            this.groupBoxLaser.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.Slider slider;
        private System.Windows.Forms.GroupBox groupBoxLaser;
        private System.Windows.Forms.Label laserStatus;
        private System.Windows.Forms.ComboBox laserHandshakingcbx;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox laserDataBitsCbx;
        private System.Windows.Forms.ComboBox laserComListCbx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button laserOpenCloseSpbtn;
        private System.Windows.Forms.ComboBox laserBaudRateCbx;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox laserParityCbx;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox laserStopBitsCbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn70;
    }
}
