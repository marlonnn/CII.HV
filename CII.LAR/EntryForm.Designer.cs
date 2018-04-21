using AForge.Controls;
using CII.LAR.UI;

namespace CII.LAR
{
    partial class EntryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntryForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCapture = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonVideo = new System.Windows.Forms.ToolStripButton();
            this.toolStripFiles = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.comboBoxLense = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonScale = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonMove = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRectangle = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonElliptical = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButtonUnit = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnUm = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDmm = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMm = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCm = new System.Windows.Forms.ToolStripMenuItem();
            this.btnInches = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMeters = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonLaser = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownCamera = new System.Windows.Forms.ToolStripDropDownButton();
            this.openCameraLiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freeRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snapshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            //this.toolStripButtonPort = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLaserDebug = new System.Windows.Forms.ToolStripButton();
            this.openCameraLive = new System.Windows.Forms.ToolStripMenuItem();
            this.openCameraAndStop = new System.Windows.Forms.ToolStripMenuItem();
            this.closeCameraTool = new System.Windows.Forms.ToolStripMenuItem();
            this.freeRun = new System.Windows.Forms.ToolStripMenuItem();
            this.snapshot = new System.Windows.Forms.ToolStripMenuItem();
            this.systemMonitorTimer = new System.Windows.Forms.Timer(this.components);
            this.videoControl = new CII.LAR.UI.VideoControl();
            this.richPictureBox = new CII.LAR.UI.RichPictureBox();
            this.horizontalFlipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalFlipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.LaserCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.richPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCapture,
            this.toolStripButtonVideo,
            this.toolStripFiles,
            this.toolStripSeparator1,
            this.comboBoxLense,
            this.toolStripSeparator2,
            this.toolStripButtonZoomOut,
            this.toolStripButtonZoomIn,
            this.toolStripButtonFit,
            this.toolStripSeparator3,
            this.toolStripButtonScale,
            this.toolStripSeparator4,
            this.toolStripButtonMove,
            this.toolStripButtonLine,
            this.toolStripButtonRectangle,
            this.toolStripButtonElliptical,
            this.toolStripDropDownButtonUnit,
            this.toolStripSeparator5,
            this.toolStripButtonLaser,
            this.toolStripButtonSetting,
            this.toolStripButtonOpen,
            this.toolStripDropDownCamera,
            //this.toolStripButtonPort,
            this.toolStripButtonLaserDebug});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButtonCapture
            // 
            this.toolStripButtonCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCapture.Image = global::CII.LAR.Properties.Resources.camera;
            this.toolStripButtonCapture.Name = "toolStripButtonCapture";
            resources.ApplyResources(this.toolStripButtonCapture, "toolStripButtonCapture");
            this.toolStripButtonCapture.Click += new System.EventHandler(this.toolStripButtonCapture_Click);
            // 
            // toolStripButtonVideo
            // 
            this.toolStripButtonVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonVideo.Image = global::CII.LAR.Properties.Resources.video;
            this.toolStripButtonVideo.Name = "toolStripButtonVideo";
            resources.ApplyResources(this.toolStripButtonVideo, "toolStripButtonVideo");
            this.toolStripButtonVideo.Click += new System.EventHandler(this.toolStripButtonVideo_Click);
            // 
            // toolStripFiles
            // 
            this.toolStripFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripFiles.Image = global::CII.LAR.Properties.Resources.files;
            this.toolStripFiles.Name = "toolStripFiles";
            resources.ApplyResources(this.toolStripFiles, "toolStripFiles");
            this.toolStripFiles.Click += new System.EventHandler(this.toolStripFiles_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // comboBoxLense
            // 
            this.comboBoxLense.Name = "comboBoxLense";
            resources.ApplyResources(this.comboBoxLense, "comboBoxLense");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripButtonZoomOut
            // 
            this.toolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomOut.Image = global::CII.LAR.Properties.Resources.Zoom_out;
            this.toolStripButtonZoomOut.Name = "toolStripButtonZoomOut";
            resources.ApplyResources(this.toolStripButtonZoomOut, "toolStripButtonZoomOut");
            this.toolStripButtonZoomOut.Click += new System.EventHandler(this.toolStripButtonZoomOut_Click);
            // 
            // toolStripButtonZoomIn
            // 
            this.toolStripButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomIn.Image = global::CII.LAR.Properties.Resources.Zoom_in;
            this.toolStripButtonZoomIn.Name = "toolStripButtonZoomIn";
            resources.ApplyResources(this.toolStripButtonZoomIn, "toolStripButtonZoomIn");
            this.toolStripButtonZoomIn.Click += new System.EventHandler(this.toolStripButtonZoomIn_Click);
            // 
            // toolStripButtonFit
            // 
            this.toolStripButtonFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFit.Image = global::CII.LAR.Properties.Resources.zoomFit;
            this.toolStripButtonFit.Name = "toolStripButtonFit";
            resources.ApplyResources(this.toolStripButtonFit, "toolStripButtonFit");
            this.toolStripButtonFit.Click += new System.EventHandler(this.toolStripButtonFit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripButtonScale
            // 
            this.toolStripButtonScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonScale.Image = global::CII.LAR.Properties.Resources.ruler;
            this.toolStripButtonScale.Name = "toolStripButtonScale";
            resources.ApplyResources(this.toolStripButtonScale, "toolStripButtonScale");
            this.toolStripButtonScale.Click += new System.EventHandler(this.toolStripButtonScale_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripButtonMove
            // 
            this.toolStripButtonMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMove.Image = global::CII.LAR.Properties.Resources.hand;
            this.toolStripButtonMove.Name = "toolStripButtonMove";
            resources.ApplyResources(this.toolStripButtonMove, "toolStripButtonMove");
            this.toolStripButtonMove.Click += new System.EventHandler(this.toolStripButtonMove_Click);
            // 
            // toolStripButtonLine
            // 
            this.toolStripButtonLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLine.Image = global::CII.LAR.Properties.Resources.line;
            this.toolStripButtonLine.Name = "toolStripButtonLine";
            resources.ApplyResources(this.toolStripButtonLine, "toolStripButtonLine");
            this.toolStripButtonLine.Click += new System.EventHandler(this.toolStripButtonLine_Click);
            // 
            // toolStripButtonRectangle
            // 
            this.toolStripButtonRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRectangle.Image = global::CII.LAR.Properties.Resources.rectangular;
            this.toolStripButtonRectangle.Name = "toolStripButtonRectangle";
            resources.ApplyResources(this.toolStripButtonRectangle, "toolStripButtonRectangle");
            this.toolStripButtonRectangle.Click += new System.EventHandler(this.toolStripButtonRectangle_Click);
            // 
            // toolStripButtonElliptical
            // 
            this.toolStripButtonElliptical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonElliptical.Image = global::CII.LAR.Properties.Resources.elliptical;
            this.toolStripButtonElliptical.Name = "toolStripButtonElliptical";
            resources.ApplyResources(this.toolStripButtonElliptical, "toolStripButtonElliptical");
            this.toolStripButtonElliptical.Click += new System.EventHandler(this.toolStripButtonElliptical_Click);
            // 
            // toolStripDropDownButtonUnit
            // 
            this.toolStripDropDownButtonUnit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonUnit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUm,
            this.btnDmm,
            this.btnMm,
            this.btnCm,
            this.btnInches,
            this.btnMeters});
            this.toolStripDropDownButtonUnit.Visible = false;
            this.toolStripDropDownButtonUnit.Image = global::CII.LAR.Properties.Resources.unit;
            this.toolStripDropDownButtonUnit.Name = "toolStripDropDownButtonUnit";
            resources.ApplyResources(this.toolStripDropDownButtonUnit, "toolStripDropDownButtonUnit");
            // 
            // btnUm
            // 
            this.btnUm.Name = "btnUm";
            resources.ApplyResources(this.btnUm, "btnUm");
            // 
            // btnDmm
            // 
            this.btnDmm.Name = "btnDmm";
            resources.ApplyResources(this.btnDmm, "btnDmm");
            // 
            // btnMm
            // 
            this.btnMm.Checked = true;
            this.btnMm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnMm.Name = "btnMm";
            resources.ApplyResources(this.btnMm, "btnMm");
            // 
            // btnCm
            // 
            this.btnCm.Name = "btnCm";
            resources.ApplyResources(this.btnCm, "btnCm");
            // 
            // btnInches
            // 
            this.btnInches.Name = "btnInches";
            resources.ApplyResources(this.btnInches, "btnInches");
            // 
            // btnMeters
            // 
            this.btnMeters.Name = "btnMeters";
            resources.ApplyResources(this.btnMeters, "btnMeters");
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // toolStripButtonLaser
            // 
            this.toolStripButtonLaser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLaser.Image = global::CII.LAR.Properties.Resources.laser;
            this.toolStripButtonLaser.Name = "toolStripButtonLaser";
            resources.ApplyResources(this.toolStripButtonLaser, "toolStripButtonLaser");
            this.toolStripButtonLaser.Click += new System.EventHandler(this.toolStripButtonLaser_Click);
            // 
            // toolStripButtonSetting
            // 
            this.toolStripButtonSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSetting.Image = global::CII.LAR.Properties.Resources.setting;
            this.toolStripButtonSetting.Name = "toolStripButtonSetting";
            resources.ApplyResources(this.toolStripButtonSetting, "toolStripButtonSetting");
            this.toolStripButtonSetting.Click += new System.EventHandler(this.toolStripButtonSetting_Click);
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = global::CII.LAR.Properties.Resources.open;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            resources.ApplyResources(this.toolStripButtonOpen, "toolStripButtonOpen");
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripDropDownCamera
            // 
            this.toolStripDropDownCamera.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownCamera.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCameraLiveToolStripMenuItem,
            this.closeCameraToolStripMenuItem,
            this.freeRunToolStripMenuItem,
            this.snapshotToolStripMenuItem,
            this.horizontalFlipToolStripMenuItem,
            this.verticalFlipToolStripMenuItem});
            this.toolStripDropDownCamera.Image = global::CII.LAR.Properties.Resources.idscamera;
            resources.ApplyResources(this.toolStripDropDownCamera, "toolStripDropDownCamera");
            this.toolStripDropDownCamera.Name = "toolStripDropDownCamera";
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
            // freeRunToolStripMenuItem
            // 
            this.freeRunToolStripMenuItem.Name = "freeRunToolStripMenuItem";
            resources.ApplyResources(this.freeRunToolStripMenuItem, "freeRunToolStripMenuItem");
            // 
            // snapshotToolStripMenuItem
            // 
            this.snapshotToolStripMenuItem.Name = "snapshotToolStripMenuItem";
            resources.ApplyResources(this.snapshotToolStripMenuItem, "snapshotToolStripMenuItem");
            //// 
            //// toolStripButtonPort
            //// 
            //this.toolStripButtonPort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.toolStripButtonPort.Image = global::CII.LAR.Properties.Resources.port;
            //resources.ApplyResources(this.toolStripButtonPort, "toolStripButtonPort");
            //this.toolStripButtonPort.Name = "toolStripButtonPort";
            //this.toolStripButtonPort.Click += new System.EventHandler(this.toolStripButtonPort_Click);
            // 
            // toolStripButtonLaserDebug
            // 
            this.toolStripButtonLaserDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLaserDebug.Image = global::CII.LAR.Properties.Resources.backflush;
            this.toolStripButtonLaserDebug.Name = "toolStripButtonLaserDebug";
            resources.ApplyResources(this.toolStripButtonLaserDebug, "toolStripButtonLaserDebug");
            this.toolStripButtonLaserDebug.Click += new System.EventHandler(this.toolStripButtonLaserDebug_Click);
            // 
            // openCameraLive
            // 
            this.openCameraLive.Name = "openCameraLive";
            resources.ApplyResources(this.openCameraLive, "openCameraLive");
            // 
            // openCameraAndStop
            // 
            this.openCameraAndStop.Name = "openCameraAndStop";
            resources.ApplyResources(this.openCameraAndStop, "openCameraAndStop");
            // 
            // closeCameraTool
            // 
            this.closeCameraTool.Name = "closeCameraTool";
            resources.ApplyResources(this.closeCameraTool, "closeCameraTool");
            // 
            // freeRun
            // 
            this.freeRun.Name = "freeRun";
            resources.ApplyResources(this.freeRun, "freeRun");
            // 
            // snapshot
            // 
            this.snapshot.Name = "snapshot";
            resources.ApplyResources(this.snapshot, "snapshot");
            // 
            // systemMonitorTimer
            // 
            this.systemMonitorTimer.Interval = 1000;
            this.systemMonitorTimer.Tick += new System.EventHandler(this.systemMonitorTimer_Tick);
            // 
            // videoControl
            // 
            //this.videoControl.DrawObject = null;
            //this.videoControl.LaserFunction = false;
            resources.ApplyResources(this.videoControl, "videoControl");
            this.videoControl.Name = "videoControl";
            //this.videoControl.UnitOfMeasure = CII.LAR.DrawTools.enUniMis.mm;
            //this.videoControl.VideoSize = new System.Drawing.Size(1280, 960);
            this.videoControl.VideoSource = null;
            //this.videoControl.Zoom = 1F;
            // 
            // richPictureBox
            // 
            resources.ApplyResources(this.richPictureBox, "richPictureBox");
            this.richPictureBox.Name = "richPictureBox";
            this.richPictureBox.BackColor = System.Drawing.Color.Gray;
            this.richPictureBox.OffsetX = 0;
            this.richPictureBox.OffsetY = 0;
            this.richPictureBox.TabStop = false;
            this.richPictureBox.UnitOfMeasure = enUniMis.mm;
            this.richPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
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

            // LaserCheckTimer
            // 
            this.LaserCheckTimer.Interval = 2000;
            this.LaserCheckTimer.Tick += new System.EventHandler(this.LaserCheckTimer_Tick);
            // 
            // EntryForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.richPictureBox);
            this.Name = "EntryForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.richPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCapture;
        private System.Windows.Forms.ToolStripButton toolStripButtonVideo;
        private System.Windows.Forms.ToolStripButton toolStripFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox comboBoxLense;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonScale;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonLine;
        private System.Windows.Forms.ToolStripButton toolStripButtonRectangle;
        private System.Windows.Forms.ToolStripButton toolStripButtonElliptical;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonLaser;
        private System.Windows.Forms.ToolStripButton toolStripButtonSetting;
        private System.Windows.Forms.ToolStripButton toolStripButtonFit;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonUnit;
        private System.Windows.Forms.ToolStripMenuItem btnUm;
        private System.Windows.Forms.ToolStripMenuItem btnDmm;
        private System.Windows.Forms.ToolStripMenuItem btnInches;
        private System.Windows.Forms.ToolStripMenuItem btnMeters;
        private System.Windows.Forms.ToolStripMenuItem btnMm;
        private System.Windows.Forms.ToolStripMenuItem btnCm;
        private System.Windows.Forms.ToolStripButton toolStripButtonMove;
        private System.Windows.Forms.ToolStripMenuItem openCameraLive;
        private System.Windows.Forms.ToolStripMenuItem openCameraAndStop;
        private System.Windows.Forms.ToolStripMenuItem closeCameraTool;
        private System.Windows.Forms.ToolStripMenuItem freeRun;
        private System.Windows.Forms.ToolStripMenuItem snapshot;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownCamera;
        private System.Windows.Forms.ToolStripMenuItem openCameraLiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem freeRunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snapshotToolStripMenuItem;
        //private System.Windows.Forms.ToolStripButton toolStripButtonPort;
        private System.Windows.Forms.ToolStripButton toolStripButtonLaserDebug;
        private System.Windows.Forms.Timer systemMonitorTimer;
        private RichPictureBox richPictureBox;
        private VideoControl videoControl;
        private System.Windows.Forms.ToolStripMenuItem horizontalFlipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalFlipToolStripMenuItem;
        private System.Windows.Forms.Timer LaserCheckTimer;
    }
}