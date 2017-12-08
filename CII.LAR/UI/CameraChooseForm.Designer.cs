namespace CII.LAR.UI
{
    partial class CameraChooseForm
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
            this.listViewCamera = new System.Windows.Forms.ListView();
            this.columnHeaderAvailable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCamID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDevID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderModell = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSerNr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewCamera
            // 
            this.listViewCamera.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderAvailable,
            this.columnHeaderCamID,
            this.columnHeaderDevID,
            this.columnHeaderModell,
            this.columnHeaderSerNr});
            this.listViewCamera.GridLines = true;
            this.listViewCamera.Location = new System.Drawing.Point(-1, 1);
            this.listViewCamera.MultiSelect = false;
            this.listViewCamera.Name = "listViewCamera";
            this.listViewCamera.Size = new System.Drawing.Size(604, 228);
            this.listViewCamera.TabIndex = 1;
            this.listViewCamera.UseCompatibleStateImageBehavior = false;
            this.listViewCamera.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderAvailable
            // 
            this.columnHeaderAvailable.Text = "Available";
            this.columnHeaderAvailable.Width = 100;
            // 
            // columnHeaderCamID
            // 
            this.columnHeaderCamID.Text = "Camera ID";
            this.columnHeaderCamID.Width = 100;
            // 
            // columnHeaderDevID
            // 
            this.columnHeaderDevID.Text = "Device ID";
            this.columnHeaderDevID.Width = 100;
            // 
            // columnHeaderModell
            // 
            this.columnHeaderModell.Text = "Model";
            // 
            // columnHeaderSerNr
            // 
            this.columnHeaderSerNr.Text = "SerNr.";
            this.columnHeaderSerNr.Width = 228;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(528, 235);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 21);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(446, 235);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 21);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // CameraChooseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 262);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.listViewCamera);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CameraChooseForm";
            this.Text = "CameraChooseForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewCamera;
        private System.Windows.Forms.ColumnHeader columnHeaderAvailable;
        private System.Windows.Forms.ColumnHeader columnHeaderCamID;
        private System.Windows.Forms.ColumnHeader columnHeaderDevID;
        private System.Windows.Forms.ColumnHeader columnHeaderModell;
        private System.Windows.Forms.ColumnHeader columnHeaderSerNr;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
    }
}