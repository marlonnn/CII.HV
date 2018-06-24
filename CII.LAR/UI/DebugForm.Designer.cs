using CII.LAR.MaterialSkin;

namespace CII.LAR.UI
{
    partial class DebugCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.label2 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.m1Steps = new CII.LAR.MaterialSkin.MaterialLabel();
            this.m2Steps = new CII.LAR.MaterialSkin.MaterialLabel();
            this.label3 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.responseCode = new CII.LAR.MaterialSkin.MaterialLabel();
            this.label4 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.label5 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.label6 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.label7 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.motor1Steps = new CII.LAR.MaterialSkin.MaterialLabel();
            this.motor1Origination = new CII.LAR.MaterialSkin.MaterialLabel();
            this.motor2Steps = new CII.LAR.MaterialSkin.MaterialLabel();
            this.motor2Origination = new CII.LAR.MaterialSkin.MaterialLabel();
            this.label8 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblMousePosition = new CII.LAR.MaterialSkin.MaterialLabel();
            this.label9 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblMatrix = new CII.LAR.MaterialSkin.MaterialLabel();
            this.label10 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblLaserStatus = new CII.LAR.MaterialSkin.MaterialLabel();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(208, 4);
            this.closeButton.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Depth = 0;
            this.label1.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.label1.Location = new System.Drawing.Point(3, 41);
            this.label1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "电机1步数:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Depth = 0;
            this.label2.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.label2.Location = new System.Drawing.Point(3, 62);
            this.label2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "电机2步数:";
            // 
            // m1Steps
            // 
            this.m1Steps.AutoSize = true;
            this.m1Steps.Depth = 0;
            this.m1Steps.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.m1Steps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.m1Steps.Location = new System.Drawing.Point(68, 41);
            this.m1Steps.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.m1Steps.Name = "m1Steps";
            this.m1Steps.Size = new System.Drawing.Size(15, 17);
            this.m1Steps.TabIndex = 3;
            this.m1Steps.Text = "0";
            // 
            // m2Steps
            // 
            this.m2Steps.AutoSize = true;
            this.m2Steps.Depth = 0;
            this.m2Steps.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.m2Steps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.m2Steps.Location = new System.Drawing.Point(68, 62);
            this.m2Steps.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.m2Steps.Name = "m2Steps";
            this.m2Steps.Size = new System.Drawing.Size(15, 17);
            this.m2Steps.TabIndex = 4;
            this.m2Steps.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Depth = 0;
            this.label3.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.label3.Location = new System.Drawing.Point(3, 181);
            this.label3.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "状态:";
            // 
            // responseCode
            // 
            this.responseCode.AutoSize = true;
            this.responseCode.Depth = 0;
            this.responseCode.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.responseCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.responseCode.Location = new System.Drawing.Point(44, 181);
            this.responseCode.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.responseCode.Name = "responseCode";
            this.responseCode.Size = new System.Drawing.Size(0, 17);
            this.responseCode.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Depth = 0;
            this.label4.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.label4.Location = new System.Drawing.Point(3, 87);
            this.label4.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "电机1下发步数:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Depth = 0;
            this.label5.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.label5.Location = new System.Drawing.Point(3, 113);
            this.label5.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "电机1方向:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Depth = 0;
            this.label6.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.label6.Location = new System.Drawing.Point(3, 160);
            this.label6.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "电机2方向:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Depth = 0;
            this.label7.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.label7.Location = new System.Drawing.Point(3, 135);
            this.label7.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "电机2下发步数:";
            // 
            // motor1Steps
            // 
            this.motor1Steps.AutoSize = true;
            this.motor1Steps.Depth = 0;
            this.motor1Steps.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.motor1Steps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.motor1Steps.Location = new System.Drawing.Point(98, 87);
            this.motor1Steps.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.motor1Steps.Name = "motor1Steps";
            this.motor1Steps.Size = new System.Drawing.Size(0, 17);
            this.motor1Steps.TabIndex = 11;
            // 
            // motor1Origination
            // 
            this.motor1Origination.AutoSize = true;
            this.motor1Origination.Depth = 0;
            this.motor1Origination.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.motor1Origination.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.motor1Origination.Location = new System.Drawing.Point(74, 113);
            this.motor1Origination.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.motor1Origination.Name = "motor1Origination";
            this.motor1Origination.Size = new System.Drawing.Size(0, 17);
            this.motor1Origination.TabIndex = 12;
            // 
            // motor2Steps
            // 
            this.motor2Steps.AutoSize = true;
            this.motor2Steps.Depth = 0;
            this.motor2Steps.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.motor2Steps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.motor2Steps.Location = new System.Drawing.Point(98, 135);
            this.motor2Steps.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.motor2Steps.Name = "motor2Steps";
            this.motor2Steps.Size = new System.Drawing.Size(0, 17);
            this.motor2Steps.TabIndex = 13;
            // 
            // motor2Origination
            // 
            this.motor2Origination.AutoSize = true;
            this.motor2Origination.Depth = 0;
            this.motor2Origination.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.motor2Origination.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.motor2Origination.Location = new System.Drawing.Point(74, 161);
            this.motor2Origination.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.motor2Origination.Name = "motor2Origination";
            this.motor2Origination.Size = new System.Drawing.Size(0, 17);
            this.motor2Origination.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Depth = 0;
            this.label8.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.label8.Location = new System.Drawing.Point(3, 206);
            this.label8.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "鼠标坐标：";
            // 
            // lblMousePosition
            // 
            this.lblMousePosition.AutoSize = true;
            this.lblMousePosition.Depth = 0;
            this.lblMousePosition.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.lblMousePosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblMousePosition.Location = new System.Drawing.Point(68, 206);
            this.lblMousePosition.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblMousePosition.Name = "lblMousePosition";
            this.lblMousePosition.Size = new System.Drawing.Size(33, 17);
            this.lblMousePosition.TabIndex = 16;
            this.lblMousePosition.Text = "[0,0]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Depth = 0;
            this.label9.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.label9.Location = new System.Drawing.Point(3, 229);
            this.label9.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 17);
            this.label9.TabIndex = 17;
            this.label9.Text = "转换矩阵:";
            // 
            // lblMatrix
            // 
            this.lblMatrix.Depth = 0;
            this.lblMatrix.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.lblMatrix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblMatrix.Location = new System.Drawing.Point(3, 253);
            this.lblMatrix.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblMatrix.Name = "lblMatrix";
            this.lblMatrix.Size = new System.Drawing.Size(214, 112);
            this.lblMatrix.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Depth = 0;
            this.label10.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.label10.Location = new System.Drawing.Point(3, 22);
            this.label10.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 17);
            this.label10.TabIndex = 19;
            this.label10.Text = "激光器连接状态:";
            // 
            // lblLaserStatus
            // 
            this.lblLaserStatus.AutoSize = true;
            this.lblLaserStatus.Depth = 0;
            this.lblLaserStatus.Font = new System.Drawing.Font("PingFang SC Medium", 9F);
            this.lblLaserStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblLaserStatus.Location = new System.Drawing.Point(98, 22);
            this.lblLaserStatus.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblLaserStatus.Name = "lblLaserStatus";
            this.lblLaserStatus.Size = new System.Drawing.Size(32, 17);
            this.lblLaserStatus.TabIndex = 20;
            this.lblLaserStatus.Text = "断开";
            // 
            // DebugCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLaserStatus);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblMatrix);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblMousePosition);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.motor2Origination);
            this.Controls.Add(this.motor2Steps);
            this.Controls.Add(this.motor1Origination);
            this.Controls.Add(this.motor1Steps);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.responseCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m2Steps);
            this.Controls.Add(this.m1Steps);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DebugCtrl";
            this.Size = new System.Drawing.Size(220, 362);
            this.Title = "调试信息";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.m1Steps, 0);
            this.Controls.SetChildIndex(this.m2Steps, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.responseCode, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.motor1Steps, 0);
            this.Controls.SetChildIndex(this.motor1Origination, 0);
            this.Controls.SetChildIndex(this.motor2Steps, 0);
            this.Controls.SetChildIndex(this.motor2Origination, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.lblMousePosition, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.lblMatrix, 0);
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.lblLaserStatus, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialLabel label1;
        private MaterialLabel label2;
        private MaterialLabel m1Steps;
        private MaterialLabel m2Steps;
        private MaterialLabel label3;
        private MaterialLabel responseCode;
        private MaterialLabel label4;
        private MaterialLabel label5;
        private MaterialLabel label6;
        private MaterialLabel label7;
        private MaterialLabel motor1Steps;
        private MaterialLabel motor1Origination;
        private MaterialLabel motor2Steps;
        private MaterialLabel motor2Origination;
        private MaterialLabel label8;
        private MaterialLabel lblMousePosition;
        private MaterialLabel label9;
        private MaterialLabel lblMatrix;
        private MaterialLabel label10;
        private MaterialLabel lblLaserStatus;
    }
}
