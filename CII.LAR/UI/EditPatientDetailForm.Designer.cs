using CII.LAR.MaterialSkin;

namespace CII.LAR.UI
{
    partial class EditPatientDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPatientDetailForm));
            this.btnCancel = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.btnConfirm = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.textBoxPatientName = new CII.LAR.MaterialSkin.MaterialTextBox();
            this.lblPatientName = new CII.LAR.MaterialSkin.MaterialLabel();
            this.textBoxPatientID = new CII.LAR.MaterialSkin.MaterialTextBox();
            this.lblPatientID = new CII.LAR.MaterialSkin.MaterialLabel();
            this.labelX1 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.textBoxComments = new CII.LAR.MaterialSkin.MaterialTextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Depth = 0;
            this.btnCancel.Icon = null;
            this.btnCancel.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primary = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnConfirm, "btnConfirm");
            this.btnConfirm.Depth = 0;
            this.btnConfirm.Icon = null;
            this.btnConfirm.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Primary = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // textBoxPatientName
            // 
            this.textBoxPatientName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxPatientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPatientName.CustomAutoSize = true;
            this.textBoxPatientName.Depth = 0;
            this.textBoxPatientName.EmptyTextTip = null;
            this.textBoxPatientName.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.textBoxPatientName, "textBoxPatientName");
            this.textBoxPatientName.ForeColor = System.Drawing.Color.Black;
            this.textBoxPatientName.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.textBoxPatientName.Name = "textBoxPatientName";
            this.textBoxPatientName.Radius = 3;
            this.textBoxPatientName.ReadOnly = true;
            // 
            // lblPatientName
            // 
            this.lblPatientName.Depth = 0;
            resources.ApplyResources(this.lblPatientName, "lblPatientName");
            this.lblPatientName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblPatientName.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblPatientName.Name = "lblPatientName";
            // 
            // textBoxPatientID
            // 
            this.textBoxPatientID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxPatientID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPatientID.CustomAutoSize = true;
            this.textBoxPatientID.Depth = 0;
            this.textBoxPatientID.EmptyTextTip = null;
            this.textBoxPatientID.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.textBoxPatientID, "textBoxPatientID");
            this.textBoxPatientID.ForeColor = System.Drawing.Color.Black;
            this.textBoxPatientID.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.textBoxPatientID.Name = "textBoxPatientID";
            this.textBoxPatientID.Radius = 3;
            this.textBoxPatientID.ReadOnly = true;
            // 
            // lblPatientID
            // 
            this.lblPatientID.Depth = 0;
            resources.ApplyResources(this.lblPatientID, "lblPatientID");
            this.lblPatientID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblPatientID.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblPatientID.Name = "lblPatientID";
            // 
            // labelX1
            // 
            this.labelX1.Depth = 0;
            resources.ApplyResources(this.labelX1, "labelX1");
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.labelX1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.labelX1.Name = "labelX1";
            // 
            // textBoxComments
            // 
            this.textBoxComments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBoxComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxComments.CustomAutoSize = true;
            this.textBoxComments.Depth = 0;
            this.textBoxComments.EmptyTextTip = null;
            this.textBoxComments.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.textBoxComments, "textBoxComments");
            this.textBoxComments.ForeColor = System.Drawing.Color.Black;
            this.textBoxComments.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.Radius = 3;
            // 
            // EditPatientDetailForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxComments);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.textBoxPatientName);
            this.Controls.Add(this.lblPatientName);
            this.Controls.Add(this.textBoxPatientID);
            this.Controls.Add(this.lblPatientID);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditPatientDetailForm";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialRoundButton btnCancel;
        private MaterialRoundButton btnConfirm;
        private MaterialTextBox textBoxPatientName;
        private MaterialLabel lblPatientName;
        private MaterialTextBox textBoxPatientID;
        private MaterialLabel lblPatientID;
        private MaterialLabel labelX1;
        private MaterialTextBox textBoxComments;
    }
}