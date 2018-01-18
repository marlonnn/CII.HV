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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m1Steps = new System.Windows.Forms.Label();
            this.m2Steps = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "M1 Steps:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "M2 Steps:";
            // 
            // m1Steps
            // 
            this.m1Steps.AutoSize = true;
            this.m1Steps.Location = new System.Drawing.Point(68, 38);
            this.m1Steps.Name = "m1Steps";
            this.m1Steps.Size = new System.Drawing.Size(11, 12);
            this.m1Steps.TabIndex = 3;
            this.m1Steps.Text = "0";
            // 
            // m2Steps
            // 
            this.m2Steps.AutoSize = true;
            this.m2Steps.Location = new System.Drawing.Point(68, 59);
            this.m2Steps.Name = "m2Steps";
            this.m2Steps.Size = new System.Drawing.Size(11, 12);
            this.m2Steps.TabIndex = 4;
            this.m2Steps.Text = "0";
            // 
            // DebugCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m2Steps);
            this.Controls.Add(this.m1Steps);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DebugCtrl";
            this.Size = new System.Drawing.Size(191, 92);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DebugCtrl_KeyDown);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.m1Steps, 0);
            this.Controls.SetChildIndex(this.m2Steps, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label m1Steps;
        private System.Windows.Forms.Label m2Steps;
    }
}
