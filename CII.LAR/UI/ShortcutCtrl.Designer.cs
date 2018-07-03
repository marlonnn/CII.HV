using CII.LAR.MaterialSkin;

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
            this.lblSnapshoot = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblZoomIn = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblZoomOut = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblVideo = new CII.LAR.MaterialSkin.MaterialLabel();
            this.buttonSave = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.txtTakePicture = new CII.LAR.UI.HotKeyControl();
            this.txtZoomIn = new CII.LAR.UI.HotKeyControl();
            this.txtZoomOut = new CII.LAR.UI.HotKeyControl();
            this.txtStart = new CII.LAR.UI.HotKeyControl();
            this.txtFire = new CII.LAR.UI.HotKeyControl();
            this.lblFire = new CII.LAR.MaterialSkin.MaterialLabel();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // lblSnapshoot
            // 
            resources.ApplyResources(this.lblSnapshoot, "lblSnapshoot");
            this.lblSnapshoot.Depth = 0;
            this.lblSnapshoot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblSnapshoot.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblSnapshoot.Name = "lblSnapshoot";
            // 
            // lblZoomIn
            // 
            resources.ApplyResources(this.lblZoomIn, "lblZoomIn");
            this.lblZoomIn.Depth = 0;
            this.lblZoomIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblZoomIn.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblZoomIn.Name = "lblZoomIn";
            // 
            // lblZoomOut
            // 
            resources.ApplyResources(this.lblZoomOut, "lblZoomOut");
            this.lblZoomOut.Depth = 0;
            this.lblZoomOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblZoomOut.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblZoomOut.Name = "lblZoomOut";
            // 
            // lblVideo
            // 
            resources.ApplyResources(this.lblVideo, "lblVideo");
            this.lblVideo.Depth = 0;
            this.lblVideo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblVideo.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblVideo.Name = "lblVideo";
            // 
            // buttonSave
            // 
            this.buttonSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Depth = 0;
            this.buttonSave.Icon = null;
            this.buttonSave.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Primary = false;
            this.buttonSave.Warning = false;
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
            // txtFire
            // 
            resources.ApplyResources(this.txtFire, "txtFire");
            this.txtFire.Name = "txtFire";
            this.txtFire.ToolTip = null;
            // 
            // lblFire
            // 
            resources.ApplyResources(this.lblFire, "lblFire");
            this.lblFire.Depth = 0;
            this.lblFire.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblFire.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblFire.Name = "lblFire";
            // 
            // ShortcutCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtFire);
            this.Controls.Add(this.lblFire);
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
            this.Controls.SetChildIndex(this.lblFire, 0);
            this.Controls.SetChildIndex(this.txtFire, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialLabel lblSnapshoot;
        private MaterialLabel lblZoomIn;
        private MaterialLabel lblZoomOut;
        private MaterialLabel lblVideo;
        private MaterialRoundButton buttonSave;
        private HotKeyControl txtTakePicture;
        private HotKeyControl txtZoomIn;
        private HotKeyControl txtZoomOut;
        private HotKeyControl txtStart;
        private HotKeyControl txtFire;
        private MaterialLabel lblFire;
    }
}
