namespace CII.LAR.UI
{
    partial class SerialPortCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerialPortCtrl));
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
            this.groupBoxMotor = new System.Windows.Forms.GroupBox();
            this.motorStatus = new System.Windows.Forms.Label();
            this.motorHandshakingcbx = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.motorDataBitsCbx = new System.Windows.Forms.ComboBox();
            this.motorComListCbx = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.motorOpenCloseSpbtn = new System.Windows.Forms.Button();
            this.motorBaudRateCbx = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.motorParityCbx = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.motorStopBitsCbx = new System.Windows.Forms.ComboBox();
            this.groupBoxLaser.SuspendLayout();
            this.groupBoxMotor.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
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
            resources.ApplyResources(this.groupBoxLaser, "groupBoxLaser");
            this.groupBoxLaser.Name = "groupBoxLaser";
            this.groupBoxLaser.TabStop = false;
            // 
            // laserStatus
            // 
            resources.ApplyResources(this.laserStatus, "laserStatus");
            this.laserStatus.Name = "laserStatus";
            // 
            // laserHandshakingcbx
            // 
            this.laserHandshakingcbx.FormattingEnabled = true;
            resources.ApplyResources(this.laserHandshakingcbx, "laserHandshakingcbx");
            this.laserHandshakingcbx.Name = "laserHandshakingcbx";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // laserDataBitsCbx
            // 
            this.laserDataBitsCbx.FormattingEnabled = true;
            resources.ApplyResources(this.laserDataBitsCbx, "laserDataBitsCbx");
            this.laserDataBitsCbx.Name = "laserDataBitsCbx";
            // 
            // laserComListCbx
            // 
            this.laserComListCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.laserComListCbx.FormattingEnabled = true;
            resources.ApplyResources(this.laserComListCbx, "laserComListCbx");
            this.laserComListCbx.Name = "laserComListCbx";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // laserOpenCloseSpbtn
            // 
            resources.ApplyResources(this.laserOpenCloseSpbtn, "laserOpenCloseSpbtn");
            this.laserOpenCloseSpbtn.Name = "laserOpenCloseSpbtn";
            this.laserOpenCloseSpbtn.UseVisualStyleBackColor = true;
            this.laserOpenCloseSpbtn.Click += new System.EventHandler(this.laserOpenCloseSpbtn_Click);
            // 
            // laserBaudRateCbx
            // 
            this.laserBaudRateCbx.FormattingEnabled = true;
            resources.ApplyResources(this.laserBaudRateCbx, "laserBaudRateCbx");
            this.laserBaudRateCbx.Name = "laserBaudRateCbx";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // laserParityCbx
            // 
            this.laserParityCbx.FormattingEnabled = true;
            resources.ApplyResources(this.laserParityCbx, "laserParityCbx");
            this.laserParityCbx.Name = "laserParityCbx";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // laserStopBitsCbx
            // 
            this.laserStopBitsCbx.FormattingEnabled = true;
            resources.ApplyResources(this.laserStopBitsCbx, "laserStopBitsCbx");
            this.laserStopBitsCbx.Name = "laserStopBitsCbx";
            // 
            // groupBoxMotor
            // 
            this.groupBoxMotor.Controls.Add(this.motorStatus);
            this.groupBoxMotor.Controls.Add(this.motorHandshakingcbx);
            this.groupBoxMotor.Controls.Add(this.label1);
            this.groupBoxMotor.Controls.Add(this.motorDataBitsCbx);
            this.groupBoxMotor.Controls.Add(this.motorComListCbx);
            this.groupBoxMotor.Controls.Add(this.label2);
            this.groupBoxMotor.Controls.Add(this.motorOpenCloseSpbtn);
            this.groupBoxMotor.Controls.Add(this.motorBaudRateCbx);
            this.groupBoxMotor.Controls.Add(this.label8);
            this.groupBoxMotor.Controls.Add(this.label10);
            this.groupBoxMotor.Controls.Add(this.motorParityCbx);
            this.groupBoxMotor.Controls.Add(this.label11);
            this.groupBoxMotor.Controls.Add(this.label12);
            this.groupBoxMotor.Controls.Add(this.motorStopBitsCbx);
            resources.ApplyResources(this.groupBoxMotor, "groupBoxMotor");
            this.groupBoxMotor.Name = "groupBoxMotor";
            this.groupBoxMotor.TabStop = false;
            // 
            // motorStatus
            // 
            resources.ApplyResources(this.motorStatus, "motorStatus");
            this.motorStatus.Name = "motorStatus";
            // 
            // motorHandshakingcbx
            // 
            this.motorHandshakingcbx.FormattingEnabled = true;
            resources.ApplyResources(this.motorHandshakingcbx, "motorHandshakingcbx");
            this.motorHandshakingcbx.Name = "motorHandshakingcbx";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // motorDataBitsCbx
            // 
            this.motorDataBitsCbx.FormattingEnabled = true;
            resources.ApplyResources(this.motorDataBitsCbx, "motorDataBitsCbx");
            this.motorDataBitsCbx.Name = "motorDataBitsCbx";
            // 
            // motorComListCbx
            // 
            this.motorComListCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.motorComListCbx.FormattingEnabled = true;
            resources.ApplyResources(this.motorComListCbx, "motorComListCbx");
            this.motorComListCbx.Name = "motorComListCbx";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // motorOpenCloseSpbtn
            // 
            resources.ApplyResources(this.motorOpenCloseSpbtn, "motorOpenCloseSpbtn");
            this.motorOpenCloseSpbtn.Name = "motorOpenCloseSpbtn";
            this.motorOpenCloseSpbtn.UseVisualStyleBackColor = true;
            this.motorOpenCloseSpbtn.Click += new System.EventHandler(this.motorOpenCloseSpbtn_Click);
            // 
            // motorBaudRateCbx
            // 
            this.motorBaudRateCbx.FormattingEnabled = true;
            resources.ApplyResources(this.motorBaudRateCbx, "motorBaudRateCbx");
            this.motorBaudRateCbx.Name = "motorBaudRateCbx";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // motorParityCbx
            // 
            this.motorParityCbx.FormattingEnabled = true;
            resources.ApplyResources(this.motorParityCbx, "motorParityCbx");
            this.motorParityCbx.Name = "motorParityCbx";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // motorStopBitsCbx
            // 
            this.motorStopBitsCbx.FormattingEnabled = true;
            resources.ApplyResources(this.motorStopBitsCbx, "motorStopBitsCbx");
            this.motorStopBitsCbx.Name = "motorStopBitsCbx";
            // 
            // SerialPortCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMotor);
            this.Controls.Add(this.groupBoxLaser);
            this.Name = "SerialPortCtrl";
            this.Title = "Serial port config";
            this.Controls.SetChildIndex(this.groupBoxLaser, 0);
            this.Controls.SetChildIndex(this.groupBoxMotor, 0);
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.groupBoxLaser.ResumeLayout(false);
            this.groupBoxLaser.PerformLayout();
            this.groupBoxMotor.ResumeLayout(false);
            this.groupBoxMotor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLaser;
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
        private System.Windows.Forms.GroupBox groupBoxMotor;
        private System.Windows.Forms.ComboBox motorHandshakingcbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox motorDataBitsCbx;
        private System.Windows.Forms.ComboBox motorComListCbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button motorOpenCloseSpbtn;
        private System.Windows.Forms.ComboBox motorBaudRateCbx;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox motorParityCbx;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox motorStopBitsCbx;
        private System.Windows.Forms.Label laserStatus;
        private System.Windows.Forms.Label motorStatus;
    }
}