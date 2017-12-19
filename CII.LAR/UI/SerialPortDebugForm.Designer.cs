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
            this.components = new System.ComponentModel.Container();
            this.autoSendTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.sendIntervalTimetbx = new System.Windows.Forms.TextBox();
            this.autoSendcbx = new System.Windows.Forms.CheckBox();
            this.clearReceivebtn = new System.Windows.Forms.Button();
            this.clearSendbtn = new System.Windows.Forms.Button();
            this.receivetbx = new System.Windows.Forms.TextBox();
            this.sendtbx = new System.Windows.Forms.TextBox();
            this.sendbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxMotor = new System.Windows.Forms.GroupBox();
            this.motorStatus = new System.Windows.Forms.Label();
            this.motorHandshakingcbx = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.motorDataBitsCbx = new System.Windows.Forms.ComboBox();
            this.motorComListCbx = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.motorOpenCloseSpbtn = new System.Windows.Forms.Button();
            this.motorBaudRateCbx = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.motorParityCbx = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.motorStopBitsCbx = new System.Windows.Forms.ComboBox();
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
            this.btnC60 = new System.Windows.Forms.Button();
            this.btn6062 = new System.Windows.Forms.Button();
            this.btn6060 = new System.Windows.Forms.Button();
            this.btn6055 = new System.Windows.Forms.Button();
            this.btn6055A0 = new System.Windows.Forms.Button();
            this.btn605500 = new System.Windows.Forms.Button();
            this.btn605561 = new System.Windows.Forms.Button();
            this.btn605562 = new System.Windows.Forms.Button();
            this.autoReceiverTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBoxMotor.SuspendLayout();
            this.groupBoxLaser.SuspendLayout();
            this.SuspendLayout();
            // 
            // autoSendTimer
            // 
            this.autoSendTimer.Interval = 200;
            this.autoSendTimer.Tick += new System.EventHandler(this.autoSendTimer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.sendIntervalTimetbx);
            this.groupBox1.Controls.Add(this.autoSendcbx);
            this.groupBox1.Controls.Add(this.clearReceivebtn);
            this.groupBox1.Controls.Add(this.clearSendbtn);
            this.groupBox1.Controls.Add(this.receivetbx);
            this.groupBox1.Controls.Add(this.sendtbx);
            this.groupBox1.Controls.Add(this.sendbtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(368, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 355);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(143, 316);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 15);
            this.label8.TabIndex = 23;
            this.label8.Text = "ms";
            // 
            // sendIntervalTimetbx
            // 
            this.sendIntervalTimetbx.Location = new System.Drawing.Point(93, 311);
            this.sendIntervalTimetbx.MaxLength = 9;
            this.sendIntervalTimetbx.Name = "sendIntervalTimetbx";
            this.sendIntervalTimetbx.Size = new System.Drawing.Size(44, 21);
            this.sendIntervalTimetbx.TabIndex = 22;
            this.sendIntervalTimetbx.Text = "1000";
            // 
            // autoSendcbx
            // 
            this.autoSendcbx.AutoSize = true;
            this.autoSendcbx.Enabled = false;
            this.autoSendcbx.Location = new System.Drawing.Point(14, 313);
            this.autoSendcbx.Name = "autoSendcbx";
            this.autoSendcbx.Size = new System.Drawing.Size(79, 19);
            this.autoSendcbx.TabIndex = 21;
            this.autoSendcbx.Text = "AutoSend";
            this.autoSendcbx.UseVisualStyleBackColor = true;
            this.autoSendcbx.CheckedChanged += new System.EventHandler(this.autoSendcbx_CheckedChanged);
            // 
            // clearReceivebtn
            // 
            this.clearReceivebtn.AutoSize = true;
            this.clearReceivebtn.Location = new System.Drawing.Point(278, 13);
            this.clearReceivebtn.Name = "clearReceivebtn";
            this.clearReceivebtn.Size = new System.Drawing.Size(58, 25);
            this.clearReceivebtn.TabIndex = 11;
            this.clearReceivebtn.Text = "Clear";
            this.clearReceivebtn.UseVisualStyleBackColor = true;
            // 
            // clearSendbtn
            // 
            this.clearSendbtn.Location = new System.Drawing.Point(287, 181);
            this.clearSendbtn.Name = "clearSendbtn";
            this.clearSendbtn.Size = new System.Drawing.Size(58, 25);
            this.clearSendbtn.TabIndex = 10;
            this.clearSendbtn.Text = "Clear";
            this.clearSendbtn.UseVisualStyleBackColor = true;
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
            // sendbtn
            // 
            this.sendbtn.AutoSize = true;
            this.sendbtn.Enabled = false;
            this.sendbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendbtn.Location = new System.Drawing.Point(287, 311);
            this.sendbtn.Name = "sendbtn";
            this.sendbtn.Size = new System.Drawing.Size(58, 36);
            this.sendbtn.TabIndex = 7;
            this.sendbtn.Text = "Send";
            this.sendbtn.UseVisualStyleBackColor = true;
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
            // groupBoxMotor
            // 
            this.groupBoxMotor.Controls.Add(this.motorStatus);
            this.groupBoxMotor.Controls.Add(this.motorHandshakingcbx);
            this.groupBoxMotor.Controls.Add(this.label11);
            this.groupBoxMotor.Controls.Add(this.motorDataBitsCbx);
            this.groupBoxMotor.Controls.Add(this.motorComListCbx);
            this.groupBoxMotor.Controls.Add(this.label12);
            this.groupBoxMotor.Controls.Add(this.motorOpenCloseSpbtn);
            this.groupBoxMotor.Controls.Add(this.motorBaudRateCbx);
            this.groupBoxMotor.Controls.Add(this.label13);
            this.groupBoxMotor.Controls.Add(this.label14);
            this.groupBoxMotor.Controls.Add(this.motorParityCbx);
            this.groupBoxMotor.Controls.Add(this.label15);
            this.groupBoxMotor.Controls.Add(this.label16);
            this.groupBoxMotor.Controls.Add(this.motorStopBitsCbx);
            this.groupBoxMotor.Location = new System.Drawing.Point(190, 12);
            this.groupBoxMotor.Name = "groupBoxMotor";
            this.groupBoxMotor.Size = new System.Drawing.Size(172, 355);
            this.groupBoxMotor.TabIndex = 30;
            this.groupBoxMotor.TabStop = false;
            this.groupBoxMotor.Text = "Motor Serial Port";
            // 
            // motorStatus
            // 
            this.motorStatus.AutoSize = true;
            this.motorStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.motorStatus.Location = new System.Drawing.Point(37, 313);
            this.motorStatus.Name = "motorStatus";
            this.motorStatus.Size = new System.Drawing.Size(83, 12);
            this.motorStatus.TabIndex = 26;
            this.motorStatus.Text = "Not Connected";
            // 
            // motorHandshakingcbx
            // 
            this.motorHandshakingcbx.FormattingEnabled = true;
            this.motorHandshakingcbx.Location = new System.Drawing.Point(14, 235);
            this.motorHandshakingcbx.Name = "motorHandshakingcbx";
            this.motorHandshakingcbx.Size = new System.Drawing.Size(152, 20);
            this.motorHandshakingcbx.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(7, 220);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "HandShaking:";
            // 
            // motorDataBitsCbx
            // 
            this.motorDataBitsCbx.FormattingEnabled = true;
            this.motorDataBitsCbx.Location = new System.Drawing.Point(14, 115);
            this.motorDataBitsCbx.Name = "motorDataBitsCbx";
            this.motorDataBitsCbx.Size = new System.Drawing.Size(152, 20);
            this.motorDataBitsCbx.TabIndex = 11;
            // 
            // motorComListCbx
            // 
            this.motorComListCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.motorComListCbx.FormattingEnabled = true;
            this.motorComListCbx.Location = new System.Drawing.Point(14, 35);
            this.motorComListCbx.Name = "motorComListCbx";
            this.motorComListCbx.Size = new System.Drawing.Size(152, 20);
            this.motorComListCbx.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(7, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "Port Name:";
            // 
            // motorOpenCloseSpbtn
            // 
            this.motorOpenCloseSpbtn.Enabled = false;
            this.motorOpenCloseSpbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.motorOpenCloseSpbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.motorOpenCloseSpbtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.motorOpenCloseSpbtn.Location = new System.Drawing.Point(14, 261);
            this.motorOpenCloseSpbtn.Name = "motorOpenCloseSpbtn";
            this.motorOpenCloseSpbtn.Size = new System.Drawing.Size(152, 36);
            this.motorOpenCloseSpbtn.TabIndex = 17;
            this.motorOpenCloseSpbtn.Text = "Open";
            this.motorOpenCloseSpbtn.UseVisualStyleBackColor = true;
            this.motorOpenCloseSpbtn.Click += new System.EventHandler(this.motorOpenCloseSpbtn_Click);
            // 
            // motorBaudRateCbx
            // 
            this.motorBaudRateCbx.FormattingEnabled = true;
            this.motorBaudRateCbx.Location = new System.Drawing.Point(14, 75);
            this.motorBaudRateCbx.Name = "motorBaudRateCbx";
            this.motorBaudRateCbx.Size = new System.Drawing.Size(152, 20);
            this.motorBaudRateCbx.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(7, 180);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 16;
            this.label13.Text = "Parity:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(7, 60);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 10;
            this.label14.Text = "Baud Rate:";
            // 
            // motorParityCbx
            // 
            this.motorParityCbx.FormattingEnabled = true;
            this.motorParityCbx.Location = new System.Drawing.Point(14, 195);
            this.motorParityCbx.Name = "motorParityCbx";
            this.motorParityCbx.Size = new System.Drawing.Size(152, 20);
            this.motorParityCbx.TabIndex = 15;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(7, 100);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 12;
            this.label15.Text = "Data Bits:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(7, 140);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 14;
            this.label16.Text = "Stop Bits:";
            // 
            // motorStopBitsCbx
            // 
            this.motorStopBitsCbx.FormattingEnabled = true;
            this.motorStopBitsCbx.Location = new System.Drawing.Point(14, 155);
            this.motorStopBitsCbx.Name = "motorStopBitsCbx";
            this.motorStopBitsCbx.Size = new System.Drawing.Size(152, 20);
            this.motorStopBitsCbx.TabIndex = 13;
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
            this.btn00.Location = new System.Drawing.Point(752, 21);
            this.btn00.Name = "btn00";
            this.btn00.Size = new System.Drawing.Size(75, 23);
            this.btn00.TabIndex = 25;
            this.btn00.Text = "00";
            this.btn00.UseVisualStyleBackColor = true;
            this.btn00.Click += new System.EventHandler(this.btn00_Click);
            // 
            // btn01
            // 
            this.btn01.Location = new System.Drawing.Point(752, 44);
            this.btn01.Name = "btn01";
            this.btn01.Size = new System.Drawing.Size(75, 23);
            this.btn01.TabIndex = 26;
            this.btn01.Text = "01";
            this.btn01.UseVisualStyleBackColor = true;
            this.btn01.Click += new System.EventHandler(this.btn01_Click);
            // 
            // btn03
            // 
            this.btn03.Location = new System.Drawing.Point(752, 67);
            this.btn03.Name = "btn03";
            this.btn03.Size = new System.Drawing.Size(75, 23);
            this.btn03.TabIndex = 27;
            this.btn03.Text = "03";
            this.btn03.UseVisualStyleBackColor = true;
            this.btn03.Click += new System.EventHandler(this.btn03_Click);
            // 
            // btn04
            // 
            this.btn04.Location = new System.Drawing.Point(752, 91);
            this.btn04.Name = "btn04";
            this.btn04.Size = new System.Drawing.Size(75, 23);
            this.btn04.TabIndex = 28;
            this.btn04.Text = "04";
            this.btn04.UseVisualStyleBackColor = true;
            this.btn04.Click += new System.EventHandler(this.btn04_Click);
            // 
            // btn05
            // 
            this.btn05.Location = new System.Drawing.Point(752, 114);
            this.btn05.Name = "btn05";
            this.btn05.Size = new System.Drawing.Size(75, 23);
            this.btn05.TabIndex = 29;
            this.btn05.Text = "05";
            this.btn05.UseVisualStyleBackColor = true;
            this.btn05.Click += new System.EventHandler(this.btn05_Click);
            // 
            // btn06
            // 
            this.btn06.Location = new System.Drawing.Point(752, 137);
            this.btn06.Name = "btn06";
            this.btn06.Size = new System.Drawing.Size(75, 23);
            this.btn06.TabIndex = 30;
            this.btn06.Text = "06";
            this.btn06.UseVisualStyleBackColor = true;
            this.btn06.Click += new System.EventHandler(this.btn06_Click);
            // 
            // btn07
            // 
            this.btn07.Location = new System.Drawing.Point(752, 160);
            this.btn07.Name = "btn07";
            this.btn07.Size = new System.Drawing.Size(75, 23);
            this.btn07.TabIndex = 31;
            this.btn07.Text = "07";
            this.btn07.UseVisualStyleBackColor = true;
            this.btn07.Click += new System.EventHandler(this.btn07_Click);
            // 
            // btn08
            // 
            this.btn08.Location = new System.Drawing.Point(752, 182);
            this.btn08.Name = "btn08";
            this.btn08.Size = new System.Drawing.Size(75, 23);
            this.btn08.TabIndex = 32;
            this.btn08.Text = "08";
            this.btn08.UseVisualStyleBackColor = true;
            this.btn08.Click += new System.EventHandler(this.btn08_Click);
            // 
            // btn0B
            // 
            this.btn0B.Location = new System.Drawing.Point(752, 230);
            this.btn0B.Name = "btn0B";
            this.btn0B.Size = new System.Drawing.Size(75, 23);
            this.btn0B.TabIndex = 33;
            this.btn0B.Text = "0B";
            this.btn0B.UseVisualStyleBackColor = true;
            this.btn0B.Click += new System.EventHandler(this.btn0B_Click);
            // 
            // btn75
            // 
            this.btn75.Location = new System.Drawing.Point(528, 376);
            this.btn75.Name = "btn75";
            this.btn75.Size = new System.Drawing.Size(75, 23);
            this.btn75.TabIndex = 34;
            this.btn75.Text = "75";
            this.btn75.UseVisualStyleBackColor = true;
            this.btn75.Click += new System.EventHandler(this.btn75_Click);
            // 
            // btn09
            // 
            this.btn09.Location = new System.Drawing.Point(752, 206);
            this.btn09.Name = "btn09";
            this.btn09.Size = new System.Drawing.Size(75, 23);
            this.btn09.TabIndex = 35;
            this.btn09.Text = "09";
            this.btn09.UseVisualStyleBackColor = true;
            this.btn09.Click += new System.EventHandler(this.btn09_Click);
            // 
            // btn74
            // 
            this.btn74.Location = new System.Drawing.Point(752, 346);
            this.btn74.Name = "btn74";
            this.btn74.Size = new System.Drawing.Size(75, 23);
            this.btn74.TabIndex = 36;
            this.btn74.Text = "74";
            this.btn74.UseVisualStyleBackColor = true;
            this.btn74.Click += new System.EventHandler(this.btn74_Click);
            // 
            // btn0C
            // 
            this.btn0C.Location = new System.Drawing.Point(752, 253);
            this.btn0C.Name = "btn0C";
            this.btn0C.Size = new System.Drawing.Size(75, 23);
            this.btn0C.TabIndex = 37;
            this.btn0C.Text = "0C";
            this.btn0C.UseVisualStyleBackColor = true;
            this.btn0C.Click += new System.EventHandler(this.btn0C_Click);
            // 
            // btn71
            // 
            this.btn71.Location = new System.Drawing.Point(752, 276);
            this.btn71.Name = "btn71";
            this.btn71.Size = new System.Drawing.Size(75, 23);
            this.btn71.TabIndex = 38;
            this.btn71.Text = "71";
            this.btn71.UseVisualStyleBackColor = true;
            this.btn71.Click += new System.EventHandler(this.btn71_Click);
            // 
            // btn72
            // 
            this.btn72.Location = new System.Drawing.Point(752, 299);
            this.btn72.Name = "btn72";
            this.btn72.Size = new System.Drawing.Size(75, 23);
            this.btn72.TabIndex = 39;
            this.btn72.Text = "72";
            this.btn72.UseVisualStyleBackColor = true;
            this.btn72.Click += new System.EventHandler(this.btn72_Click);
            // 
            // btn73
            // 
            this.btn73.Location = new System.Drawing.Point(752, 322);
            this.btn73.Name = "btn73";
            this.btn73.Size = new System.Drawing.Size(75, 23);
            this.btn73.TabIndex = 40;
            this.btn73.Text = "73";
            this.btn73.UseVisualStyleBackColor = true;
            this.btn73.Click += new System.EventHandler(this.btn73_Click);
            // 
            // btnC60
            // 
            this.btnC60.Location = new System.Drawing.Point(851, 43);
            this.btnC60.Name = "btnC60";
            this.btnC60.Size = new System.Drawing.Size(75, 23);
            this.btnC60.TabIndex = 41;
            this.btnC60.Text = "60 61";
            this.btnC60.UseVisualStyleBackColor = true;
            this.btnC60.Click += new System.EventHandler(this.btnC60_Click);
            // 
            // btn6062
            // 
            this.btn6062.Location = new System.Drawing.Point(851, 66);
            this.btn6062.Name = "btn6062";
            this.btn6062.Size = new System.Drawing.Size(75, 23);
            this.btn6062.TabIndex = 42;
            this.btn6062.Text = "60 62";
            this.btn6062.UseVisualStyleBackColor = true;
            this.btn6062.Click += new System.EventHandler(this.btn6062_Click);
            // 
            // btn6060
            // 
            this.btn6060.Location = new System.Drawing.Point(851, 21);
            this.btn6060.Name = "btn6060";
            this.btn6060.Size = new System.Drawing.Size(75, 23);
            this.btn6060.TabIndex = 43;
            this.btn6060.Text = "60 60";
            this.btn6060.UseVisualStyleBackColor = true;
            this.btn6060.Click += new System.EventHandler(this.btn6060_Click);
            // 
            // btn6055
            // 
            this.btn6055.Location = new System.Drawing.Point(851, 91);
            this.btn6055.Name = "btn6055";
            this.btn6055.Size = new System.Drawing.Size(75, 23);
            this.btn6055.TabIndex = 44;
            this.btn6055.Text = "60 55 60";
            this.btn6055.UseVisualStyleBackColor = true;
            this.btn6055.Click += new System.EventHandler(this.btn6055_Click);
            // 
            // btn6055A0
            // 
            this.btn6055A0.Location = new System.Drawing.Point(851, 114);
            this.btn6055A0.Name = "btn6055A0";
            this.btn6055A0.Size = new System.Drawing.Size(75, 23);
            this.btn6055A0.TabIndex = 45;
            this.btn6055A0.Text = "60 55 A0";
            this.btn6055A0.UseVisualStyleBackColor = true;
            this.btn6055A0.Click += new System.EventHandler(this.btn6055A0_Click);
            // 
            // btn605500
            // 
            this.btn605500.Location = new System.Drawing.Point(851, 138);
            this.btn605500.Name = "btn605500";
            this.btn605500.Size = new System.Drawing.Size(75, 23);
            this.btn605500.TabIndex = 46;
            this.btn605500.Text = "60 55 00";
            this.btn605500.UseVisualStyleBackColor = true;
            this.btn605500.Click += new System.EventHandler(this.btn605500_Click);
            // 
            // btn605561
            // 
            this.btn605561.Location = new System.Drawing.Point(851, 161);
            this.btn605561.Name = "btn605561";
            this.btn605561.Size = new System.Drawing.Size(75, 23);
            this.btn605561.TabIndex = 47;
            this.btn605561.Text = "60 55 61";
            this.btn605561.UseVisualStyleBackColor = true;
            this.btn605561.Click += new System.EventHandler(this.btn605561_Click);
            // 
            // btn605562
            // 
            this.btn605562.Location = new System.Drawing.Point(851, 184);
            this.btn605562.Name = "btn605562";
            this.btn605562.Size = new System.Drawing.Size(75, 23);
            this.btn605562.TabIndex = 48;
            this.btn605562.Text = "60 55 62";
            this.btn605562.UseVisualStyleBackColor = true;
            this.btn605562.Click += new System.EventHandler(this.btn605562_Click);
            // 
            // autoReceiverTimer
            // 
            this.autoReceiverTimer.Tick += new System.EventHandler(this.autoReceiverTimer_Tick);
            // 
            // SerialPortDebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 373);
            this.Controls.Add(this.btn605562);
            this.Controls.Add(this.btn605561);
            this.Controls.Add(this.btn605500);
            this.Controls.Add(this.btn6055A0);
            this.Controls.Add(this.btn6055);
            this.Controls.Add(this.btn6060);
            this.Controls.Add(this.btn6062);
            this.Controls.Add(this.btnC60);
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
            this.Controls.Add(this.groupBoxMotor);
            this.Controls.Add(this.groupBox1);
            this.Name = "SerialPortDebugForm";
            this.Text = "SerialPortDebugForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxMotor.ResumeLayout(false);
            this.groupBoxMotor.PerformLayout();
            this.groupBoxLaser.ResumeLayout(false);
            this.groupBoxLaser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer autoSendTimer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox sendIntervalTimetbx;
        private System.Windows.Forms.CheckBox autoSendcbx;
        private System.Windows.Forms.Button clearReceivebtn;
        private System.Windows.Forms.Button clearSendbtn;
        private System.Windows.Forms.TextBox receivetbx;
        private System.Windows.Forms.TextBox sendtbx;
        private System.Windows.Forms.Button sendbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxMotor;
        private System.Windows.Forms.Label motorStatus;
        private System.Windows.Forms.ComboBox motorHandshakingcbx;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox motorDataBitsCbx;
        private System.Windows.Forms.ComboBox motorComListCbx;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button motorOpenCloseSpbtn;
        private System.Windows.Forms.ComboBox motorBaudRateCbx;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox motorParityCbx;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox motorStopBitsCbx;
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
        private System.Windows.Forms.Button btnC60;
        private System.Windows.Forms.Button btn6062;
        private System.Windows.Forms.Button btn6060;
        private System.Windows.Forms.Button btn6055;
        private System.Windows.Forms.Button btn6055A0;
        private System.Windows.Forms.Button btn605500;
        private System.Windows.Forms.Button btn605561;
        private System.Windows.Forms.Button btn605562;
        private System.Windows.Forms.Timer autoReceiverTimer;
    }
}