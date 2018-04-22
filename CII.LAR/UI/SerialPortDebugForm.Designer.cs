namespace CII.LAR.UI
{
    partial class SerialPortDebugForm
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
            this.clearReceivebtn = new System.Windows.Forms.Button();
            this.clearSendbtn = new System.Windows.Forms.Button();
            this.receivetbx = new System.Windows.Forms.TextBox();
            this.sendtbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.btn00 = new System.Windows.Forms.Button();
            this.btn01 = new System.Windows.Forms.Button();
            this.btn03 = new System.Windows.Forms.Button();
            this.btn04 = new System.Windows.Forms.Button();
            this.btn05 = new System.Windows.Forms.Button();
            this.btn06 = new System.Windows.Forms.Button();
            this.btn07 = new System.Windows.Forms.Button();
            this.btn08 = new System.Windows.Forms.Button();
            this.btn0B = new System.Windows.Forms.Button();
            this.btn75 = new System.Windows.Forms.Button();
            this.btn09 = new System.Windows.Forms.Button();
            this.btn74 = new System.Windows.Forms.Button();
            this.btn0C = new System.Windows.Forms.Button();
            this.btn71 = new System.Windows.Forms.Button();
            this.btn72 = new System.Windows.Forms.Button();
            this.btn73 = new System.Windows.Forms.Button();
            this.btn70 = new System.Windows.Forms.Button();
            this.slider = new DevComponents.DotNetBar.Controls.Slider();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBoxLaser.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clearReceivebtn);
            this.groupBox1.Controls.Add(this.clearSendbtn);
            this.groupBox1.Controls.Add(this.receivetbx);
            this.groupBox1.Controls.Add(this.sendtbx);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(190, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 355);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // clearReceivebtn
            // 
            this.clearReceivebtn.AutoSize = true;
            this.clearReceivebtn.Location = new System.Drawing.Point(287, 12);
            this.clearReceivebtn.Name = "clearReceivebtn";
            this.clearReceivebtn.Size = new System.Drawing.Size(58, 25);
            this.clearReceivebtn.TabIndex = 11;
            this.clearReceivebtn.Text = "Clear";
            this.clearReceivebtn.UseVisualStyleBackColor = true;
            this.clearReceivebtn.Click += new System.EventHandler(this.clearReceivebtn_Click);
            // 
            // clearSendbtn
            // 
            this.clearSendbtn.Location = new System.Drawing.Point(287, 181);
            this.clearSendbtn.Name = "clearSendbtn";
            this.clearSendbtn.Size = new System.Drawing.Size(58, 25);
            this.clearSendbtn.TabIndex = 10;
            this.clearSendbtn.Text = "Clear";
            this.clearSendbtn.UseVisualStyleBackColor = true;
            this.clearSendbtn.Click += new System.EventHandler(this.clearSendbtn_Click);
            // 
            // receivetbx
            // 
            this.receivetbx.BackColor = System.Drawing.SystemColors.InfoText;
            this.receivetbx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.receivetbx.ForeColor = System.Drawing.SystemColors.Info;
            this.receivetbx.Location = new System.Drawing.Point(7, 38);
            this.receivetbx.Multiline = true;
            this.receivetbx.Name = "receivetbx";
            this.receivetbx.ReadOnly = true;
            this.receivetbx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.receivetbx.Size = new System.Drawing.Size(354, 140);
            this.receivetbx.TabIndex = 9;
            this.receivetbx.TabStop = false;
            // 
            // sendtbx
            // 
            this.sendtbx.BackColor = System.Drawing.SystemColors.InfoText;
            this.sendtbx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sendtbx.ForeColor = System.Drawing.SystemColors.Info;
            this.sendtbx.Location = new System.Drawing.Point(7, 207);
            this.sendtbx.Multiline = true;
            this.sendtbx.Name = "sendtbx";
            this.sendtbx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sendtbx.Size = new System.Drawing.Size(354, 98);
            this.sendtbx.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Received:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Send:";
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
            this.groupBoxLaser.Location = new System.Drawing.Point(12, 12);
            this.groupBoxLaser.Name = "groupBoxLaser";
            this.groupBoxLaser.Size = new System.Drawing.Size(172, 355);
            this.groupBoxLaser.TabIndex = 31;
            this.groupBoxLaser.TabStop = false;
            this.groupBoxLaser.Text = "Laser Serial Port";
            // 
            // laserStatus
            // 
            this.laserStatus.AutoSize = true;
            this.laserStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.laserStatus.Location = new System.Drawing.Point(39, 313);
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
            this.laserHandshakingcbx.Size = new System.Drawing.Size(152, 20);
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
            this.laserDataBitsCbx.Size = new System.Drawing.Size(152, 20);
            this.laserDataBitsCbx.TabIndex = 11;
            // 
            // laserComListCbx
            // 
            this.laserComListCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.laserComListCbx.FormattingEnabled = true;
            this.laserComListCbx.Location = new System.Drawing.Point(14, 35);
            this.laserComListCbx.Name = "laserComListCbx";
            this.laserComListCbx.Size = new System.Drawing.Size(152, 20);
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
            this.laserOpenCloseSpbtn.Size = new System.Drawing.Size(152, 36);
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
            this.laserBaudRateCbx.Size = new System.Drawing.Size(152, 20);
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
            this.laserParityCbx.Size = new System.Drawing.Size(152, 20);
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
            this.laserStopBitsCbx.Size = new System.Drawing.Size(152, 20);
            this.laserStopBitsCbx.TabIndex = 13;
            // 
            // btn00
            // 
            this.btn00.Location = new System.Drawing.Point(570, 21);
            this.btn00.Name = "btn00";
            this.btn00.Size = new System.Drawing.Size(75, 23);
            this.btn00.TabIndex = 25;
            this.btn00.Text = "00";
            this.btn00.UseVisualStyleBackColor = true;
            this.btn00.Click += new System.EventHandler(this.btn00_Click);
            // 
            // btn01
            // 
            this.btn01.Location = new System.Drawing.Point(570, 44);
            this.btn01.Name = "btn01";
            this.btn01.Size = new System.Drawing.Size(75, 23);
            this.btn01.TabIndex = 26;
            this.btn01.Text = "01";
            this.btn01.UseVisualStyleBackColor = true;
            this.btn01.Click += new System.EventHandler(this.btn01_Click);
            // 
            // btn03
            // 
            this.btn03.Location = new System.Drawing.Point(570, 67);
            this.btn03.Name = "btn03";
            this.btn03.Size = new System.Drawing.Size(75, 23);
            this.btn03.TabIndex = 27;
            this.btn03.Text = "03";
            this.btn03.UseVisualStyleBackColor = true;
            this.btn03.Click += new System.EventHandler(this.btn03_Click);
            // 
            // btn04
            // 
            this.btn04.Location = new System.Drawing.Point(570, 91);
            this.btn04.Name = "btn04";
            this.btn04.Size = new System.Drawing.Size(75, 23);
            this.btn04.TabIndex = 28;
            this.btn04.Text = "04";
            this.btn04.UseVisualStyleBackColor = true;
            this.btn04.Click += new System.EventHandler(this.btn04_Click);
            // 
            // btn05
            // 
            this.btn05.Location = new System.Drawing.Point(570, 114);
            this.btn05.Name = "btn05";
            this.btn05.Size = new System.Drawing.Size(75, 23);
            this.btn05.TabIndex = 29;
            this.btn05.Text = "05";
            this.btn05.UseVisualStyleBackColor = true;
            this.btn05.Click += new System.EventHandler(this.btn05_Click);
            // 
            // btn06
            // 
            this.btn06.Location = new System.Drawing.Point(570, 137);
            this.btn06.Name = "btn06";
            this.btn06.Size = new System.Drawing.Size(75, 23);
            this.btn06.TabIndex = 30;
            this.btn06.Text = "06";
            this.btn06.UseVisualStyleBackColor = true;
            this.btn06.Click += new System.EventHandler(this.btn06_Click);
            // 
            // btn07
            // 
            this.btn07.Location = new System.Drawing.Point(570, 160);
            this.btn07.Name = "btn07";
            this.btn07.Size = new System.Drawing.Size(75, 23);
            this.btn07.TabIndex = 31;
            this.btn07.Text = "07";
            this.btn07.UseVisualStyleBackColor = true;
            this.btn07.Click += new System.EventHandler(this.btn07_Click);
            // 
            // btn08
            // 
            this.btn08.Location = new System.Drawing.Point(651, 21);
            this.btn08.Name = "btn08";
            this.btn08.Size = new System.Drawing.Size(75, 23);
            this.btn08.TabIndex = 32;
            this.btn08.Text = "08";
            this.btn08.UseVisualStyleBackColor = true;
            this.btn08.Click += new System.EventHandler(this.btn08_Click);
            // 
            // btn0B
            // 
            this.btn0B.Location = new System.Drawing.Point(651, 69);
            this.btn0B.Name = "btn0B";
            this.btn0B.Size = new System.Drawing.Size(75, 23);
            this.btn0B.TabIndex = 33;
            this.btn0B.Text = "0B";
            this.btn0B.UseVisualStyleBackColor = true;
            this.btn0B.Click += new System.EventHandler(this.btn0B_Click);
            // 
            // btn75
            // 
            this.btn75.Location = new System.Drawing.Point(741, 138);
            this.btn75.Name = "btn75";
            this.btn75.Size = new System.Drawing.Size(75, 23);
            this.btn75.TabIndex = 34;
            this.btn75.Text = "75";
            this.btn75.UseVisualStyleBackColor = true;
            this.btn75.Click += new System.EventHandler(this.btn75_Click);
            // 
            // btn09
            // 
            this.btn09.Location = new System.Drawing.Point(651, 45);
            this.btn09.Name = "btn09";
            this.btn09.Size = new System.Drawing.Size(75, 23);
            this.btn09.TabIndex = 35;
            this.btn09.Text = "09";
            this.btn09.UseVisualStyleBackColor = true;
            this.btn09.Click += new System.EventHandler(this.btn09_Click);
            // 
            // btn74
            // 
            this.btn74.Location = new System.Drawing.Point(741, 114);
            this.btn74.Name = "btn74";
            this.btn74.Size = new System.Drawing.Size(75, 23);
            this.btn74.TabIndex = 36;
            this.btn74.Text = "74";
            this.btn74.UseVisualStyleBackColor = true;
            this.btn74.Click += new System.EventHandler(this.btn74_Click);
            // 
            // btn0C
            // 
            this.btn0C.Location = new System.Drawing.Point(651, 92);
            this.btn0C.Name = "btn0C";
            this.btn0C.Size = new System.Drawing.Size(75, 23);
            this.btn0C.TabIndex = 37;
            this.btn0C.Text = "0C";
            this.btn0C.UseVisualStyleBackColor = true;
            this.btn0C.Click += new System.EventHandler(this.btn0C_Click);
            // 
            // btn71
            // 
            this.btn71.Location = new System.Drawing.Point(741, 44);
            this.btn71.Name = "btn71";
            this.btn71.Size = new System.Drawing.Size(75, 23);
            this.btn71.TabIndex = 38;
            this.btn71.Text = "71";
            this.btn71.UseVisualStyleBackColor = true;
            this.btn71.Click += new System.EventHandler(this.btn71_Click);
            // 
            // btn72
            // 
            this.btn72.Location = new System.Drawing.Point(741, 67);
            this.btn72.Name = "btn72";
            this.btn72.Size = new System.Drawing.Size(75, 23);
            this.btn72.TabIndex = 39;
            this.btn72.Text = "72";
            this.btn72.UseVisualStyleBackColor = true;
            this.btn72.Click += new System.EventHandler(this.btn72_Click);
            // 
            // btn73
            // 
            this.btn73.Location = new System.Drawing.Point(741, 90);
            this.btn73.Name = "btn73";
            this.btn73.Size = new System.Drawing.Size(75, 23);
            this.btn73.TabIndex = 40;
            this.btn73.Text = "73";
            this.btn73.UseVisualStyleBackColor = true;
            this.btn73.Click += new System.EventHandler(this.btn73_Click);
            // 
            // btn70
            // 
            this.btn70.Location = new System.Drawing.Point(741, 21);
            this.btn70.Name = "btn70";
            this.btn70.Size = new System.Drawing.Size(75, 23);
            this.btn70.TabIndex = 49;
            this.btn70.Text = "70";
            this.btn70.UseVisualStyleBackColor = true;
            this.btn70.Click += new System.EventHandler(this.btn70_Click);
            // 
            // slider
            // 
            // 
            // 
            // 
            this.slider.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider.Location = new System.Drawing.Point(629, 344);
            this.slider.Maximum = 82;
            this.slider.Name = "slider";
            this.slider.Size = new System.Drawing.Size(251, 23);
            this.slider.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider.TabIndex = 50;
            this.slider.Text = "1";
            this.slider.Value = 1;
            this.slider.ValueChanged += new System.EventHandler(this.slider_ValueChanged);
            this.slider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.slider_MouseUp);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(567, 350);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 51;
            this.label10.Text = "Red Laser";
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox.Location = new System.Drawing.Point(563, 219);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(354, 98);
            this.textBox.TabIndex = 52;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(840, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 25);
            this.button1.TabIndex = 53;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SerialPortDebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 405);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.slider);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btn70);
            this.Controls.Add(this.btn73);
            this.Controls.Add(this.btn72);
            this.Controls.Add(this.btn71);
            this.Controls.Add(this.btn0C);
            this.Controls.Add(this.btn74);
            this.Controls.Add(this.btn09);
            this.Controls.Add(this.btn75);
            this.Controls.Add(this.btn0B);
            this.Controls.Add(this.btn08);
            this.Controls.Add(this.btn07);
            this.Controls.Add(this.btn06);
            this.Controls.Add(this.btn05);
            this.Controls.Add(this.btn04);
            this.Controls.Add(this.btn03);
            this.Controls.Add(this.btn01);
            this.Controls.Add(this.btn00);
            this.Controls.Add(this.groupBoxLaser);
            this.Controls.Add(this.groupBox1);
            this.Name = "SerialPortDebugForm";
            this.Text = "SerialPortDebugForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxLaser.ResumeLayout(false);
            this.groupBoxLaser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button clearReceivebtn;
        private System.Windows.Forms.Button clearSendbtn;
        private System.Windows.Forms.TextBox receivetbx;
        private System.Windows.Forms.TextBox sendtbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
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

        private System.Windows.Forms.Button btn00;
        private System.Windows.Forms.Button btn01;
        private System.Windows.Forms.Button btn03;
        private System.Windows.Forms.Button btn04;
        private System.Windows.Forms.Button btn05;
        private System.Windows.Forms.Button btn06;
        private System.Windows.Forms.Button btn07;
        private System.Windows.Forms.Button btn08;
        private System.Windows.Forms.Button btn0B;
        private System.Windows.Forms.Button btn75;
        private System.Windows.Forms.Button btn09;
        private System.Windows.Forms.Button btn74;
        private System.Windows.Forms.Button btn0C;
        private System.Windows.Forms.Button btn71;
        private System.Windows.Forms.Button btn72;
        private System.Windows.Forms.Button btn73;
        private System.Windows.Forms.Button btn70;
        private DevComponents.DotNetBar.Controls.Slider slider;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button button1;
    }
}