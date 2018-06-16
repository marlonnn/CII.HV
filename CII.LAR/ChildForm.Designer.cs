using CII.LAR.MaterialSkin;

namespace CII.LAR
{
    partial class ChildForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChildForm));
            this.materialToolStrip1 = new CII.LAR.MaterialSkin.MaterialToolStrip();
            this.toolstripBtnScreenShort = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnVideo = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnFiles = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.materialToolStrip4 = new CII.LAR.MaterialSkin.MaterialToolStrip();
            this.toolstripBtnLaser = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnSetting = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnCamera = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnDebug = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnAbout = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.materialToolStrip3 = new CII.LAR.MaterialSkin.MaterialToolStrip();
            this.toolstripBtnZoomIn = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnZoomOut = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnFit = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.materialToolStrip2 = new CII.LAR.MaterialSkin.MaterialToolStrip();
            this.toolstripBtnMeasure = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnLine = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnRectangle = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnEllipse = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnHand = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.materialToolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.materialToolStrip4.SuspendLayout();
            this.materialToolStrip3.SuspendLayout();
            this.materialToolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialToolStrip1
            // 
            this.materialToolStrip1.AutoSize = false;
            this.materialToolStrip1.Depth = 0;
            this.materialToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.materialToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripBtnScreenShort,
            this.toolstripBtnVideo,
            this.toolstripBtnFiles});
            this.materialToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.materialToolStrip1.Location = new System.Drawing.Point(1, 15);
            this.materialToolStrip1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialToolStrip1.Name = "materialToolStrip1";
            this.materialToolStrip1.Size = new System.Drawing.Size(47, 143);
            this.materialToolStrip1.TabIndex = 1;
            this.materialToolStrip1.Text = "materialToolStrip1";
            // 
            // toolstripBtnScreenShort
            // 
            this.toolstripBtnScreenShort.AutoSize = false;
            this.toolstripBtnScreenShort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnScreenShort.Image = global::CII.LAR.Properties.Resources.camera;
            this.toolstripBtnScreenShort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnScreenShort.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnScreenShort.Name = "toolstripBtnScreenShort";
            this.toolstripBtnScreenShort.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnScreenShort.Text = "Screen Shot";
            this.toolstripBtnScreenShort.Click += new System.EventHandler(this.toolstripBtnScreenShort_Click);
            // 
            // toolstripBtnVideo
            // 
            this.toolstripBtnVideo.AutoSize = false;
            this.toolstripBtnVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnVideo.Image = global::CII.LAR.Properties.Resources.video;
            this.toolstripBtnVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnVideo.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnVideo.Name = "toolstripBtnVideo";
            this.toolstripBtnVideo.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnVideo.Text = "Video";
            this.toolstripBtnVideo.Click += new System.EventHandler(this.toolstripBtnVideo_Click);
            // 
            // toolstripBtnFiles
            // 
            this.toolstripBtnFiles.AutoSize = false;
            this.toolstripBtnFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnFiles.Image = global::CII.LAR.Properties.Resources.files;
            this.toolstripBtnFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnFiles.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnFiles.Name = "toolstripBtnFiles";
            this.toolstripBtnFiles.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnFiles.Text = "Files";
            this.toolstripBtnFiles.Click += new System.EventHandler(this.toolstripBtnFiles_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(31)))), ((int)(((byte)(38)))));
            this.panel1.Controls.Add(this.materialToolStrip4);
            this.panel1.Controls.Add(this.materialToolStrip3);
            this.panel1.Controls.Add(this.materialToolStrip2);
            this.panel1.Controls.Add(this.materialToolStrip1);
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(48, 711);
            this.panel1.TabIndex = 2;
            // 
            // materialToolStrip4
            // 
            this.materialToolStrip4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.materialToolStrip4.AutoSize = false;
            this.materialToolStrip4.Depth = 0;
            this.materialToolStrip4.Dock = System.Windows.Forms.DockStyle.None;
            this.materialToolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripBtnLaser,
            this.toolstripBtnSetting,
            this.toolstripBtnCamera,
            this.toolstripBtnDebug,
            this.toolstripBtnAbout});
            this.materialToolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.materialToolStrip4.Location = new System.Drawing.Point(1, 509);
            this.materialToolStrip4.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialToolStrip4.Name = "materialToolStrip4";
            this.materialToolStrip4.Size = new System.Drawing.Size(47, 219);
            this.materialToolStrip4.TabIndex = 4;
            this.materialToolStrip4.Text = "materialToolStrip4";
            // 
            // toolstripBtnLaser
            // 
            this.toolstripBtnLaser.AutoSize = false;
            this.toolstripBtnLaser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnLaser.Image = global::CII.LAR.Properties.Resources.laser;
            this.toolstripBtnLaser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnLaser.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnLaser.Name = "toolstripBtnLaser";
            this.toolstripBtnLaser.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnLaser.Text = "Laser";
            this.toolstripBtnLaser.Click += new System.EventHandler(this.toolstripBtnLaser_Click);
            // 
            // toolstripBtnSetting
            // 
            this.toolstripBtnSetting.AutoSize = false;
            this.toolstripBtnSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnSetting.Image = global::CII.LAR.Properties.Resources.setting;
            this.toolstripBtnSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnSetting.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnSetting.Name = "toolstripBtnSetting";
            this.toolstripBtnSetting.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnSetting.Text = "Setting";
            this.toolstripBtnSetting.Click += new System.EventHandler(this.toolstripBtnSetting_Click);
            // 
            // toolstripBtnCamera
            // 
            this.toolstripBtnCamera.AutoSize = false;
            this.toolstripBtnCamera.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnCamera.Image = global::CII.LAR.Properties.Resources.idscamera;
            this.toolstripBtnCamera.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnCamera.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnCamera.Name = "toolstripBtnCamera";
            this.toolstripBtnCamera.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnCamera.Text = "Camera";
            this.toolstripBtnCamera.Click += new System.EventHandler(this.toolstripBtnCamera_Click);
            // 
            // toolstripBtnDebug
            // 
            this.toolstripBtnDebug.AutoSize = false;
            this.toolstripBtnDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnDebug.Image = global::CII.LAR.Properties.Resources.backflush;
            this.toolstripBtnDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnDebug.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnDebug.Name = "toolstripBtnDebug";
            this.toolstripBtnDebug.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnDebug.Text = "Debug";
            this.toolstripBtnDebug.Click += new System.EventHandler(this.toolstripBtnDebug_Click);
            // 
            // toolstripBtnAbout
            // 
            this.toolstripBtnAbout.AutoSize = false;
            this.toolstripBtnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnAbout.Image = global::CII.LAR.Properties.Resources.about;
            this.toolstripBtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnAbout.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnAbout.Name = "toolstripBtnAbout";
            this.toolstripBtnAbout.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnAbout.Text = "About";
            this.toolstripBtnAbout.Click += new System.EventHandler(this.toolstripBtnAbout_Click);
            // 
            // materialToolStrip3
            // 
            this.materialToolStrip3.AutoSize = false;
            this.materialToolStrip3.Depth = 0;
            this.materialToolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.materialToolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripBtnZoomIn,
            this.toolstripBtnZoomOut,
            this.toolstripBtnFit});
            this.materialToolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.materialToolStrip3.Location = new System.Drawing.Point(1, 371);
            this.materialToolStrip3.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialToolStrip3.Name = "materialToolStrip3";
            this.materialToolStrip3.Size = new System.Drawing.Size(47, 140);
            this.materialToolStrip3.TabIndex = 4;
            this.materialToolStrip3.Text = "materialToolStrip3";
            // 
            // toolstripBtnZoomIn
            // 
            this.toolstripBtnZoomIn.AutoSize = false;
            this.toolstripBtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolstripBtnZoomIn.Image")));
            this.toolstripBtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnZoomIn.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnZoomIn.Name = "toolstripBtnZoomIn";
            this.toolstripBtnZoomIn.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnZoomIn.Text = "Zoom In";
            this.toolstripBtnZoomIn.Click += new System.EventHandler(this.toolstripBtnZoomIn_Click);
            // 
            // toolstripBtnZoomOut
            // 
            this.toolstripBtnZoomOut.AutoSize = false;
            this.toolstripBtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolstripBtnZoomOut.Image")));
            this.toolstripBtnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnZoomOut.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnZoomOut.Name = "toolstripBtnZoomOut";
            this.toolstripBtnZoomOut.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnZoomOut.Text = "Zoom Out";
            this.toolstripBtnZoomOut.Click += new System.EventHandler(this.toolstripBtnZoomOut_Click);
            // 
            // toolstripBtnFit
            // 
            this.toolstripBtnFit.AutoSize = false;
            this.toolstripBtnFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnFit.Image = global::CII.LAR.Properties.Resources.zoomFit;
            this.toolstripBtnFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnFit.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnFit.Name = "toolstripBtnFit";
            this.toolstripBtnFit.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnFit.Text = "Zoom Fit";
            this.toolstripBtnFit.Click += new System.EventHandler(this.toolstripBtnFit_Click);
            // 
            // materialToolStrip2
            // 
            this.materialToolStrip2.AutoSize = false;
            this.materialToolStrip2.Depth = 0;
            this.materialToolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.materialToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripBtnMeasure,
            this.toolstripBtnLine,
            this.toolstripBtnRectangle,
            this.toolstripBtnEllipse,
            this.toolstripBtnHand});
            this.materialToolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.materialToolStrip2.Location = new System.Drawing.Point(1, 155);
            this.materialToolStrip2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialToolStrip2.Name = "materialToolStrip2";
            this.materialToolStrip2.Size = new System.Drawing.Size(47, 219);
            this.materialToolStrip2.TabIndex = 3;
            this.materialToolStrip2.Text = "materialToolStrip2";
            // 
            // toolstripBtnMeasure
            // 
            this.toolstripBtnMeasure.AutoSize = false;
            this.toolstripBtnMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnMeasure.Image = global::CII.LAR.Properties.Resources.ruler;
            this.toolstripBtnMeasure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnMeasure.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnMeasure.Name = "toolstripBtnMeasure";
            this.toolstripBtnMeasure.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnMeasure.Text = "Measure";
            this.toolstripBtnMeasure.Click += new System.EventHandler(this.toolstripBtnMeasure_Click);
            // 
            // toolstripBtnLine
            // 
            this.toolstripBtnLine.AutoSize = false;
            this.toolstripBtnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnLine.Image = global::CII.LAR.Properties.Resources.line;
            this.toolstripBtnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnLine.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnLine.Name = "toolstripBtnLine";
            this.toolstripBtnLine.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnLine.Text = "Line";
            this.toolstripBtnLine.Click += new System.EventHandler(this.toolstripBtnLine_Click);
            // 
            // toolstripBtnRectangle
            // 
            this.toolstripBtnRectangle.AutoSize = false;
            this.toolstripBtnRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnRectangle.Image = global::CII.LAR.Properties.Resources.rectangular;
            this.toolstripBtnRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnRectangle.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnRectangle.Name = "toolstripBtnRectangle";
            this.toolstripBtnRectangle.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnRectangle.Text = "Rectangle";
            this.toolstripBtnRectangle.Click += new System.EventHandler(this.toolstripBtnRectangle_Click);
            // 
            // toolstripBtnEllipse
            // 
            this.toolstripBtnEllipse.AutoSize = false;
            this.toolstripBtnEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnEllipse.Image = global::CII.LAR.Properties.Resources.elliptical;
            this.toolstripBtnEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnEllipse.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnEllipse.Name = "toolstripBtnEllipse";
            this.toolstripBtnEllipse.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnEllipse.Text = "Ellipse";
            this.toolstripBtnEllipse.Click += new System.EventHandler(this.toolstripBtnEllipse_Click);
            // 
            // toolstripBtnHand
            // 
            this.toolstripBtnHand.AutoSize = false;
            this.toolstripBtnHand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnHand.Image = global::CII.LAR.Properties.Resources.hand;
            this.toolstripBtnHand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnHand.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnHand.Name = "toolstripBtnHand";
            this.toolstripBtnHand.Size = new System.Drawing.Size(35, 35);
            this.toolstripBtnHand.Text = "Hand";
            this.toolstripBtnHand.Click += new System.EventHandler(this.toolstripBtnHand_Click);
            // 
            // ChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(787, 758);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChildForm";
            this.Text = "LAR-100 控制系统";
            this.materialToolStrip1.ResumeLayout(false);
            this.materialToolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.materialToolStrip4.ResumeLayout(false);
            this.materialToolStrip4.PerformLayout();
            this.materialToolStrip3.ResumeLayout(false);
            this.materialToolStrip3.PerformLayout();
            this.materialToolStrip2.ResumeLayout(false);
            this.materialToolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.MaterialToolStrip materialToolStrip1;
        private MaterialToolStripButton toolstripBtnScreenShort;
        private MaterialToolStripButton toolstripBtnVideo;
        private MaterialToolStripButton toolstripBtnFiles;
        private System.Windows.Forms.Panel panel1;
        private MaterialToolStrip materialToolStrip2;
        private MaterialToolStripButton toolstripBtnMeasure;
        private MaterialToolStripButton toolstripBtnLine;
        private MaterialToolStripButton toolstripBtnRectangle;
        private CII.LAR.MaterialSkin.MaterialToolStripButton toolstripBtnEllipse;
        private CII.LAR.MaterialSkin.MaterialToolStripButton toolstripBtnHand;
        private MaterialToolStrip materialToolStrip3;
        private MaterialToolStripButton toolstripBtnZoomIn;
        private MaterialToolStripButton toolstripBtnZoomOut;
        private MaterialToolStripButton toolstripBtnFit;
        private MaterialToolStrip materialToolStrip4;
        private MaterialToolStripButton toolstripBtnLaser;
        private MaterialToolStripButton toolstripBtnSetting;
        private MaterialToolStripButton toolstripBtnCamera;
        private MaterialToolStripButton toolstripBtnDebug;
        private MaterialToolStripButton toolstripBtnAbout;
    }
}