﻿namespace CII.LAR.UI
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
            this.lblZoomIn = new System.Windows.Forms.Label();
            this.lblZoomOut = new System.Windows.Forms.Label();
            this.lblVideo = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.txtTakePicture = new CII.LAR.UI.HotKeyControl();
            this.txtZoomIn = new CII.LAR.UI.HotKeyControl();
            this.txtZoomOut = new CII.LAR.UI.HotKeyControl();
            this.txtStart = new CII.LAR.UI.HotKeyControl();
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
            // lblZoomIn
            // 
            resources.ApplyResources(this.lblZoomIn, "lblZoomIn");
            this.lblZoomIn.Name = "lblZoomIn";
            // 
            // lblZoomOut
            // 
            resources.ApplyResources(this.lblZoomOut, "lblZoomOut");
            this.lblZoomOut.Name = "lblZoomOut";
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
            // txtTakePicture
            // 
            resources.ApplyResources(this.txtTakePicture, "txtTakePicture");
            this.txtTakePicture.Name = "txtTakePicture";
            this.txtTakePicture.ToolTip = null;
            // 
            // txtZoomIn
            // 
            resources.ApplyResources(this.txtZoomIn, "txtZoomIn");
            this.txtZoomIn.Name = "txtZoomIn";
            this.txtZoomIn.ToolTip = null;
            // 
            // txtZoomOut
            // 
            resources.ApplyResources(this.txtZoomOut, "txtZoomOut");
            this.txtZoomOut.Name = "txtZoomOut";
            this.txtZoomOut.ToolTip = null;
            // 
            // txtStart
            // 
            resources.ApplyResources(this.txtStart, "txtStart");
            this.txtStart.Name = "txtStart";
            this.txtStart.ToolTip = null;
            // 
            // ShortcutCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.txtZoomOut);
            this.Controls.Add(this.txtZoomIn);
            this.Controls.Add(this.txtTakePicture);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.lblVideo);
            this.Controls.Add(this.lblZoomOut);
            this.Controls.Add(this.lblZoomIn);
            this.Controls.Add(this.lblSnapshoot);
            this.Name = "ShortcutCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrShortcutTitle;
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.lblSnapshoot, 0);
            this.Controls.SetChildIndex(this.lblZoomIn, 0);
            this.Controls.SetChildIndex(this.lblZoomOut, 0);
            this.Controls.SetChildIndex(this.lblVideo, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.txtTakePicture, 0);
            this.Controls.SetChildIndex(this.txtZoomIn, 0);
            this.Controls.SetChildIndex(this.txtZoomOut, 0);
            this.Controls.SetChildIndex(this.txtStart, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSnapshoot;
        private System.Windows.Forms.Label lblZoomIn;
        private System.Windows.Forms.Label lblZoomOut;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.Button buttonSave;
        private HotKeyControl txtTakePicture;
        private HotKeyControl txtZoomIn;
        private HotKeyControl txtZoomOut;
        private HotKeyControl txtStart;
    }
}
