namespace CII.LAR.UI
{
    partial class ShortcutCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortcutCtrl));
            this.lblSnapshoot = new System.Windows.Forms.Label();
            this.txtSnap = new System.Windows.Forms.TextBox();
            this.btnCloseSnap = new CII.LAR.UI.TransparentButton();
            this.transparentButton1 = new CII.LAR.UI.TransparentButton();
            this.txtZoomIn = new System.Windows.Forms.TextBox();
            this.lblZoomIn = new System.Windows.Forms.Label();
            this.btnZoomOutClose = new CII.LAR.UI.TransparentButton();
            this.txtZoomOut = new System.Windows.Forms.TextBox();
            this.lblZoomOut = new System.Windows.Forms.Label();
            this.btnVideoClose = new CII.LAR.UI.TransparentButton();
            this.txtVideo = new System.Windows.Forms.TextBox();
            this.lblVideo = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // lblSnapshoot
            // 
            resources.ApplyResources(this.lblSnapshoot, "lblSnapshoot");
            this.lblSnapshoot.Name = "lblSnapshoot";
            // 
            // txtSnap
            // 
            resources.ApplyResources(this.txtSnap, "txtSnap");
            this.txtSnap.Name = "txtSnap";
            // 
            // btnCloseSnap
            // 
            this.btnCloseSnap.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnCloseSnap, "btnCloseSnap");
            this.btnCloseSnap.Name = "btnCloseSnap";
            // 
            // transparentButton1
            // 
            this.transparentButton1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.transparentButton1, "transparentButton1");
            this.transparentButton1.Name = "transparentButton1";
            // 
            // txtZoomIn
            // 
            resources.ApplyResources(this.txtZoomIn, "txtZoomIn");
            this.txtZoomIn.Name = "txtZoomIn";
            // 
            // lblZoomIn
            // 
            resources.ApplyResources(this.lblZoomIn, "lblZoomIn");
            this.lblZoomIn.Name = "lblZoomIn";
            // 
            // btnZoomOutClose
            // 
            this.btnZoomOutClose.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnZoomOutClose, "btnZoomOutClose");
            this.btnZoomOutClose.Name = "btnZoomOutClose";
            // 
            // txtZoomOut
            // 
            resources.ApplyResources(this.txtZoomOut, "txtZoomOut");
            this.txtZoomOut.Name = "txtZoomOut";
            // 
            // lblZoomOut
            // 
            resources.ApplyResources(this.lblZoomOut, "lblZoomOut");
            this.lblZoomOut.Name = "lblZoomOut";
            // 
            // btnVideoClose
            // 
            this.btnVideoClose.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnVideoClose, "btnVideoClose");
            this.btnVideoClose.Name = "btnVideoClose";
            // 
            // txtVideo
            // 
            resources.ApplyResources(this.txtVideo, "txtVideo");
            this.txtVideo.Name = "txtVideo";
            // 
            // lblVideo
            // 
            resources.ApplyResources(this.lblVideo, "lblVideo");
            this.lblVideo.Name = "lblVideo";
            // 
            // buttonSave
            // 
            this.buttonSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // ShortcutCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.btnVideoClose);
            this.Controls.Add(this.txtVideo);
            this.Controls.Add(this.lblVideo);
            this.Controls.Add(this.btnZoomOutClose);
            this.Controls.Add(this.txtZoomOut);
            this.Controls.Add(this.lblZoomOut);
            this.Controls.Add(this.transparentButton1);
            this.Controls.Add(this.txtZoomIn);
            this.Controls.Add(this.lblZoomIn);
            this.Controls.Add(this.btnCloseSnap);
            this.Controls.Add(this.txtSnap);
            this.Controls.Add(this.lblSnapshoot);
            this.Name = "ShortcutCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrShortcutTitle;
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.lblSnapshoot, 0);
            this.Controls.SetChildIndex(this.txtSnap, 0);
            this.Controls.SetChildIndex(this.btnCloseSnap, 0);
            this.Controls.SetChildIndex(this.lblZoomIn, 0);
            this.Controls.SetChildIndex(this.txtZoomIn, 0);
            this.Controls.SetChildIndex(this.transparentButton1, 0);
            this.Controls.SetChildIndex(this.lblZoomOut, 0);
            this.Controls.SetChildIndex(this.txtZoomOut, 0);
            this.Controls.SetChildIndex(this.btnZoomOutClose, 0);
            this.Controls.SetChildIndex(this.lblVideo, 0);
            this.Controls.SetChildIndex(this.txtVideo, 0);
            this.Controls.SetChildIndex(this.btnVideoClose, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSnapshoot;
        private System.Windows.Forms.TextBox txtSnap;
        private TransparentButton btnCloseSnap;
        private TransparentButton transparentButton1;
        private System.Windows.Forms.TextBox txtZoomIn;
        private System.Windows.Forms.Label lblZoomIn;
        private TransparentButton btnZoomOutClose;
        private System.Windows.Forms.TextBox txtZoomOut;
        private System.Windows.Forms.Label lblZoomOut;
        private TransparentButton btnVideoClose;
        private System.Windows.Forms.TextBox txtVideo;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.Button buttonSave;
    }
}
