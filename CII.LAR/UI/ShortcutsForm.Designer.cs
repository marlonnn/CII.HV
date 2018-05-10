namespace CII.LAR.UI
{
    partial class ShortcutsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortcutsForm));
            this.txtStartStop = new CII.LAR.UI.HotKeyControl();
            this.txtZoomOut = new CII.LAR.UI.HotKeyControl();
            this.txtZoomIn = new CII.LAR.UI.HotKeyControl();
            this.txtTakePicture = new CII.LAR.UI.HotKeyControl();
            this.buttonSave = new System.Windows.Forms.Button();
            this.lblVideo = new System.Windows.Forms.Label();
            this.lblZoomOut = new System.Windows.Forms.Label();
            this.lblZoomIn = new System.Windows.Forms.Label();
            this.lblSnapshoot = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtStartStop
            // 
            resources.ApplyResources(this.txtStartStop, "txtStartStop");
            this.txtStartStop.Name = "txtStartStop";
            this.txtStartStop.ToolTip = null;
            // 
            // txtZoomOut
            // 
            resources.ApplyResources(this.txtZoomOut, "txtZoomOut");
            this.txtZoomOut.Name = "txtZoomOut";
            this.txtZoomOut.ToolTip = null;
            // 
            // txtZoomIn
            // 
            resources.ApplyResources(this.txtZoomIn, "txtZoomIn");
            this.txtZoomIn.Name = "txtZoomIn";
            this.txtZoomIn.ToolTip = null;
            // 
            // txtTakePicture
            // 
            resources.ApplyResources(this.txtTakePicture, "txtTakePicture");
            this.txtTakePicture.Name = "txtTakePicture";
            this.txtTakePicture.ToolTip = null;
            // 
            // buttonSave
            // 
            this.buttonSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            // 
            // lblVideo
            // 
            resources.ApplyResources(this.lblVideo, "lblVideo");
            this.lblVideo.Name = "lblVideo";
            // 
            // lblZoomOut
            // 
            resources.ApplyResources(this.lblZoomOut, "lblZoomOut");
            this.lblZoomOut.Name = "lblZoomOut";
            // 
            // lblZoomIn
            // 
            resources.ApplyResources(this.lblZoomIn, "lblZoomIn");
            this.lblZoomIn.Name = "lblZoomIn";
            // 
            // lblSnapshoot
            // 
            resources.ApplyResources(this.lblSnapshoot, "lblSnapshoot");
            this.lblSnapshoot.Name = "lblSnapshoot";
            // 
            // ShortcutsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtStartStop);
            this.Controls.Add(this.txtZoomOut);
            this.Controls.Add(this.txtZoomIn);
            this.Controls.Add(this.txtTakePicture);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.lblVideo);
            this.Controls.Add(this.lblZoomOut);
            this.Controls.Add(this.lblZoomIn);
            this.Controls.Add(this.lblSnapshoot);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShortcutsForm";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HotKeyControl txtStartStop;
        private HotKeyControl txtZoomOut;
        private HotKeyControl txtZoomIn;
        private HotKeyControl txtTakePicture;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.Label lblZoomOut;
        private System.Windows.Forms.Label lblZoomIn;
        private System.Windows.Forms.Label lblSnapshoot;
    }
}