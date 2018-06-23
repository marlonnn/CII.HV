using CII.LAR.MaterialSkin;

namespace CII.LAR.UI
{
    partial class VideoChooseCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoChooseCtrl));
            this.listViewCamera = new System.Windows.Forms.ListView();
            this.columnHeaderAvailable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonCancel = new MaterialRoundButton();
            this.buttonOk = new MaterialRoundButton();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // listViewCamera
            // 
            this.listViewCamera.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderAvailable});
            this.listViewCamera.GridLines = true;
            resources.ApplyResources(this.listViewCamera, "listViewCamera");
            this.listViewCamera.MultiSelect = false;
            this.listViewCamera.Name = "listViewCamera";
            this.listViewCamera.UseCompatibleStateImageBehavior = false;
            this.listViewCamera.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderAvailable
            // 
            resources.ApplyResources(this.columnHeaderAvailable, "columnHeaderAvailable");
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.listViewCamera_DoubleClick);
            // 
            // VideoChooseCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.listViewCamera);
            this.Name = "VideoChooseCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrVideoChooseTitle;
            this.Controls.SetChildIndex(this.listViewCamera, 0);
            this.Controls.SetChildIndex(this.buttonOk, 0);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewCamera;
        private System.Windows.Forms.ColumnHeader columnHeaderAvailable;
        private MaterialRoundButton buttonCancel;
        private MaterialRoundButton buttonOk;
    }
}