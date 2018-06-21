﻿using CII.LAR.MaterialSkin;

namespace CII.LAR
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.materialToolStrip1 = new CII.LAR.MaterialSkin.MaterialToolStrip();
            this.toolstripBtnScreenShort = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnVideo = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnFiles = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.materialToolStrip4 = new CII.LAR.MaterialSkin.MaterialToolStrip();
            this.toolstripBtnLaser = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnSetting = new CII.LAR.MaterialSkin.MaterialToolStripButton();
            this.toolstripBtnCamera = new System.Windows.Forms.ToolStripDropDownButton();
            this.openCameraLiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalFlipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalFlipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.richPictureBox = new CII.LAR.UI.RichPictureBox();
            this.videoControl = new CII.LAR.UI.VideoControl();
            this.systemMonitorTimer = new System.Windows.Forms.Timer(this.components);
            this.LaserCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.materialToolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.materialToolStrip4.SuspendLayout();
            this.materialToolStrip3.SuspendLayout();
            this.materialToolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.richPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // materialToolStrip1
            // 
            resources.ApplyResources(this.materialToolStrip1, "materialToolStrip1");
            this.materialToolStrip1.Depth = 0;
            this.materialToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripBtnScreenShort,
            this.toolstripBtnVideo,
            this.toolstripBtnFiles});
            this.materialToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.materialToolStrip1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialToolStrip1.Name = "materialToolStrip1";
            // 
            // toolstripBtnScreenShort
            // 
            resources.ApplyResources(this.toolstripBtnScreenShort, "toolstripBtnScreenShort");
            this.toolstripBtnScreenShort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnScreenShort.Image = global::CII.LAR.Properties.Resources.camera;
            this.toolstripBtnScreenShort.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnScreenShort.Name = "toolstripBtnScreenShort";
            this.toolstripBtnScreenShort.Click += new System.EventHandler(this.toolstripBtnScreenShort_Click);
            // 
            // toolstripBtnVideo
            // 
            resources.ApplyResources(this.toolstripBtnVideo, "toolstripBtnVideo");
            this.toolstripBtnVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnVideo.Image = global::CII.LAR.Properties.Resources.video;
            this.toolstripBtnVideo.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnVideo.Name = "toolstripBtnVideo";
            this.toolstripBtnVideo.Click += new System.EventHandler(this.toolstripBtnVideo_Click);
            // 
            // toolstripBtnFiles
            // 
            resources.ApplyResources(this.toolstripBtnFiles, "toolstripBtnFiles");
            this.toolstripBtnFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnFiles.Image = global::CII.LAR.Properties.Resources.files;
            this.toolstripBtnFiles.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnFiles.Name = "toolstripBtnFiles";
            this.toolstripBtnFiles.Click += new System.EventHandler(this.toolstripBtnFiles_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(31)))), ((int)(((byte)(38)))));
            this.panel1.Controls.Add(this.materialToolStrip4);
            this.panel1.Controls.Add(this.materialToolStrip3);
            this.panel1.Controls.Add(this.materialToolStrip2);
            this.panel1.Controls.Add(this.materialToolStrip1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // materialToolStrip4
            // 
            resources.ApplyResources(this.materialToolStrip4, "materialToolStrip4");
            this.materialToolStrip4.Depth = 0;
            this.materialToolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripBtnLaser,
            this.toolstripBtnSetting,
            this.toolstripBtnCamera,
            this.toolstripBtnDebug,
            this.toolstripBtnAbout});
            this.materialToolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.materialToolStrip4.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialToolStrip4.Name = "materialToolStrip4";
            // 
            // toolstripBtnLaser
            // 
            resources.ApplyResources(this.toolstripBtnLaser, "toolstripBtnLaser");
            this.toolstripBtnLaser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnLaser.Image = global::CII.LAR.Properties.Resources.laser;
            this.toolstripBtnLaser.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnLaser.Name = "toolstripBtnLaser";
            this.toolstripBtnLaser.Click += new System.EventHandler(this.toolstripBtnLaser_Click);
            // 
            // toolstripBtnSetting
            // 
            resources.ApplyResources(this.toolstripBtnSetting, "toolstripBtnSetting");
            this.toolstripBtnSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnSetting.Image = global::CII.LAR.Properties.Resources.setting;
            this.toolstripBtnSetting.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnSetting.Name = "toolstripBtnSetting";
            this.toolstripBtnSetting.Click += new System.EventHandler(this.toolstripBtnSetting_Click);
            // 
            // toolstripBtnCamera
            // 
            resources.ApplyResources(this.toolstripBtnCamera, "toolstripBtnCamera");
            this.toolstripBtnCamera.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnCamera.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCameraLiveToolStripMenuItem,
            this.closeCameraToolStripMenuItem,
            this.horizontalFlipToolStripMenuItem,
            this.verticalFlipToolStripMenuItem});
            this.toolstripBtnCamera.Image = global::CII.LAR.Properties.Resources.idscamera;
            //this.toolstripBtnCamera.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnCamera.Name = "toolstripBtnCamera";
            this.toolstripBtnCamera.Click += new System.EventHandler(this.toolstripBtnCamera_Click);
            // 
            // openCameraLiveToolStripMenuItem
            // 
            this.openCameraLiveToolStripMenuItem.Name = "openCameraLiveToolStripMenuItem";
            resources.ApplyResources(this.openCameraLiveToolStripMenuItem, "openCameraLiveToolStripMenuItem");
            this.openCameraLiveToolStripMenuItem.Click += new System.EventHandler(this.openCameraLiveToolStripMenuItem_Click);
            // 
            // closeCameraToolStripMenuItem
            // 
            this.closeCameraToolStripMenuItem.Name = "closeCameraToolStripMenuItem";
            resources.ApplyResources(this.closeCameraToolStripMenuItem, "closeCameraToolStripMenuItem");
            this.closeCameraToolStripMenuItem.Click += new System.EventHandler(this.closeCameraToolStripMenuItem_Click);
            // 
            // horizontalFlipToolStripMenuItem
            // 
            this.horizontalFlipToolStripMenuItem.Name = "horizontalFlipToolStripMenuItem";
            resources.ApplyResources(this.horizontalFlipToolStripMenuItem, "horizontalFlipToolStripMenuItem");
            this.horizontalFlipToolStripMenuItem.Click += new System.EventHandler(this.horizontalFlipToolStripMenuItem_Click);
            // 
            // verticalFlipToolStripMenuItem
            // 
            this.verticalFlipToolStripMenuItem.Name = "verticalFlipToolStripMenuItem";
            resources.ApplyResources(this.verticalFlipToolStripMenuItem, "verticalFlipToolStripMenuItem");
            this.verticalFlipToolStripMenuItem.Click += new System.EventHandler(this.verticalFlipToolStripMenuItem_Click);
            // 
            // toolstripBtnDebug
            // 
            resources.ApplyResources(this.toolstripBtnDebug, "toolstripBtnDebug");
            this.toolstripBtnDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnDebug.Image = global::CII.LAR.Properties.Resources.backflush;
            this.toolstripBtnDebug.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnDebug.Name = "toolstripBtnDebug";
            this.toolstripBtnDebug.Click += new System.EventHandler(this.toolstripBtnDebug_Click);
            // 
            // toolstripBtnAbout
            // 
            resources.ApplyResources(this.toolstripBtnAbout, "toolstripBtnAbout");
            this.toolstripBtnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnAbout.Image = global::CII.LAR.Properties.Resources.about;
            this.toolstripBtnAbout.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnAbout.Name = "toolstripBtnAbout";
            this.toolstripBtnAbout.Click += new System.EventHandler(this.toolstripBtnAbout_Click);
            // 
            // materialToolStrip3
            // 
            resources.ApplyResources(this.materialToolStrip3, "materialToolStrip3");
            this.materialToolStrip3.Depth = 0;
            this.materialToolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripBtnZoomIn,
            this.toolstripBtnZoomOut,
            this.toolstripBtnFit});
            this.materialToolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.materialToolStrip3.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialToolStrip3.Name = "materialToolStrip3";
            // 
            // toolstripBtnZoomIn
            // 
            resources.ApplyResources(this.toolstripBtnZoomIn, "toolstripBtnZoomIn");
            this.toolstripBtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnZoomIn.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnZoomIn.Name = "toolstripBtnZoomIn";
            this.toolstripBtnZoomIn.Click += new System.EventHandler(this.toolstripBtnZoomIn_Click);
            // 
            // toolstripBtnZoomOut
            // 
            resources.ApplyResources(this.toolstripBtnZoomOut, "toolstripBtnZoomOut");
            this.toolstripBtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnZoomOut.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnZoomOut.Name = "toolstripBtnZoomOut";
            this.toolstripBtnZoomOut.Click += new System.EventHandler(this.toolstripBtnZoomOut_Click);
            // 
            // toolstripBtnFit
            // 
            resources.ApplyResources(this.toolstripBtnFit, "toolstripBtnFit");
            this.toolstripBtnFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnFit.Image = global::CII.LAR.Properties.Resources.zoomFit;
            this.toolstripBtnFit.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnFit.Name = "toolstripBtnFit";
            this.toolstripBtnFit.Click += new System.EventHandler(this.toolstripBtnFit_Click);
            // 
            // materialToolStrip2
            // 
            resources.ApplyResources(this.materialToolStrip2, "materialToolStrip2");
            this.materialToolStrip2.Depth = 0;
            this.materialToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripBtnMeasure,
            this.toolstripBtnLine,
            this.toolstripBtnRectangle,
            this.toolstripBtnEllipse,
            this.toolstripBtnHand});
            this.materialToolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.materialToolStrip2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialToolStrip2.Name = "materialToolStrip2";
            // 
            // toolstripBtnMeasure
            // 
            resources.ApplyResources(this.toolstripBtnMeasure, "toolstripBtnMeasure");
            this.toolstripBtnMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnMeasure.Image = global::CII.LAR.Properties.Resources.ruler;
            this.toolstripBtnMeasure.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnMeasure.Name = "toolstripBtnMeasure";
            this.toolstripBtnMeasure.Click += new System.EventHandler(this.toolstripBtnMeasure_Click);
            // 
            // toolstripBtnLine
            // 
            resources.ApplyResources(this.toolstripBtnLine, "toolstripBtnLine");
            this.toolstripBtnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnLine.Image = global::CII.LAR.Properties.Resources.line;
            this.toolstripBtnLine.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnLine.Name = "toolstripBtnLine";
            this.toolstripBtnLine.Click += new System.EventHandler(this.toolstripBtnLine_Click);
            // 
            // toolstripBtnRectangle
            // 
            resources.ApplyResources(this.toolstripBtnRectangle, "toolstripBtnRectangle");
            this.toolstripBtnRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnRectangle.Image = global::CII.LAR.Properties.Resources.rectangular;
            this.toolstripBtnRectangle.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnRectangle.Name = "toolstripBtnRectangle";
            this.toolstripBtnRectangle.Click += new System.EventHandler(this.toolstripBtnRectangle_Click);
            // 
            // toolstripBtnEllipse
            // 
            resources.ApplyResources(this.toolstripBtnEllipse, "toolstripBtnEllipse");
            this.toolstripBtnEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnEllipse.Image = global::CII.LAR.Properties.Resources.elliptical;
            this.toolstripBtnEllipse.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnEllipse.Name = "toolstripBtnEllipse";
            this.toolstripBtnEllipse.Click += new System.EventHandler(this.toolstripBtnEllipse_Click);
            // 
            // toolstripBtnHand
            // 
            resources.ApplyResources(this.toolstripBtnHand, "toolstripBtnHand");
            this.toolstripBtnHand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnHand.Image = global::CII.LAR.Properties.Resources.hand;
            this.toolstripBtnHand.MouseState = CII.LAR.MaterialSkin.MouseState.OUT;
            this.toolstripBtnHand.Name = "toolstripBtnHand";
            this.toolstripBtnHand.Click += new System.EventHandler(this.toolstripBtnHand_Click);
            // 
            // richPictureBox
            // 
            resources.ApplyResources(this.richPictureBox, "richPictureBox");
            this.richPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
            this.richPictureBox.CaptureVideo = false;
            this.richPictureBox.DrawObject = null;
            this.richPictureBox.LaserFunction = false;
            this.richPictureBox.Name = "richPictureBox";
            this.richPictureBox.OffsetX = 0;
            this.richPictureBox.OffsetY = 0;
            this.richPictureBox.Picture = null;
            this.richPictureBox.RecordCount = 0;
            this.richPictureBox.TabStop = false;
            this.richPictureBox.ToolStripRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.richPictureBox.UnitOfMeasure = CII.LAR.enUniMis.mm;
            this.richPictureBox.VideoSize = new System.Drawing.Size(1280, 960);
            this.richPictureBox.Zoom = 1F;
            // 
            // systemMonitorTimer
            // 
            this.systemMonitorTimer.Interval = 1000;
            this.systemMonitorTimer.Tick += new System.EventHandler(this.systemMonitorTimer_Tick);
            // LaserCheckTimer
            // 
            this.LaserCheckTimer.Interval = 2000;
            this.LaserCheckTimer.Tick += new System.EventHandler(this.LaserCheckTimer_Tick);
            // 
            // videoControl
            // 
            resources.ApplyResources(this.videoControl, "videoControl");
            this.videoControl.Name = "videoControl";
            this.videoControl.VideoSource = null;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richPictureBox);
            this.DrawIcon = true;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.materialToolStrip1.ResumeLayout(false);
            this.materialToolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.materialToolStrip4.ResumeLayout(false);
            this.materialToolStrip4.PerformLayout();
            this.materialToolStrip3.ResumeLayout(false);
            this.materialToolStrip3.PerformLayout();
            this.materialToolStrip2.ResumeLayout(false);
            this.materialToolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.richPictureBox)).EndInit();
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
        private System.Windows.Forms.ToolStripDropDownButton toolstripBtnCamera;
        private System.Windows.Forms.ToolStripMenuItem openCameraLiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalFlipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalFlipToolStripMenuItem;
        private MaterialToolStripButton toolstripBtnDebug;
        private MaterialToolStripButton toolstripBtnAbout;
        private UI.RichPictureBox richPictureBox;
        private System.Windows.Forms.Timer systemMonitorTimer;
        private System.Windows.Forms.Timer LaserCheckTimer;
        private UI.VideoControl videoControl;
    }
}