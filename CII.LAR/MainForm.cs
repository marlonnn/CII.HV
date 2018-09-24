using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.VFW;
using CII.Ins.Business.Command.LAR;
using CII.LAR.Algorithm;
using CII.LAR.Commond;
using CII.LAR.DrawTools;
using CII.LAR.Laser;
using CII.LAR.MaterialSkin;
using CII.LAR.Protocol;
using CII.LAR.SysClass;
using CII.LAR.SysClass.Shortcuts;
using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR
{
    public partial class MainForm : MaterialForm
    {
        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO Dummy);
        private LASTINPUTINFO lastInputInfo;

        internal HotKeyManager hotKeyManager;
        private SerialPortManager serialPortCom;

        protected ComponentResourceManager resources;

        private bool captureVideo = false;
        public bool CaptureVideo
        {
            get { return this.captureVideo; }
            set
            {
                //if (value != this.captureVideo)
                {
                    this.captureVideo = value;
                    this.richPictureBox.CaptureVideo = value;
                    this.richPictureBox.Invalidate();
                }
            }
        }

        private VideoCaptureDevice videoDevice;
        public VideoCaptureDevice VideoDevice
        {
            get { return this.videoDevice; }
        }

        private FullScreen fullScreen;
        private FormWindowState tempWindowState;

        #region BaseCtrl
        private BaseCtrl baseCtrl;
        [BrowsableAttribute(false)]
        [System.ComponentModel.Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BaseCtrl BaseCtrl
        {
            get
            {
                return this.baseCtrl;
            }
        }
        private List<BaseCtrl> baseControls;
        public List<BaseCtrl> BaseCtrls
        {
            get { return this.baseControls; }
            private set { this.baseControls = value; }
        }

        private SettingControl settingControl;
        private StatisticsCtrl statisticsCtrl;
        private LaserAppearanceCtrl laserAppearanceCtrl;
        private RulerAppearanceCtrl rulerAppearanceCtrl;
        private ScaleAppearanceCtrl scaleAppearanceCtrl;
        private LaserCtrl laserCtrl;
        private LaserAlignment laserAlignment;
        private LaserHoleSize laserHoleSize;
        private VideoChooseCtrl videoChooseCtrl;
        private ObjectLenseCtrl lenseCtrl;
        private ShortcutCtrl shortcutCtrl;
        private AboutControl aboutCtrl;
        private SystemInfoCtrl systemInfoCtrl;

        #endregion

        #region dragging base control
        /// <summary>
        /// indicating mouse dragging mode of Statistics control
        /// </summary>
        private bool isDraggingBaseCtrl = false;

        /// <summary>
        /// last Statistics mouse position of mouse dragging
        /// </summary>
        private Point lastBaseCtrlMousePos;

        /// <summary>
        /// the new area where the Statistics control to be dragged
        /// </summary>
        private Rectangle draggingBaseCtrlRectangle;
        #endregion

        private ListViewItemArray listViewItemArray;

        private BaseLaser laser;
        public BaseLaser Laser
        {
            get { return this.laser; }
            set { this.laser = value; }
        }

        private LaserFactory laserFactory;
        public LaserFactory LaserFactory
        {
            get { return this.laserFactory; }
            set { this.laserFactory = value; }
        }

        private LaserType laserType;

        public LaserType LaserType
        {
            get { return this.laserType; }
            set
            {
                if (value != this.laserType)
                {
                    laserType = value;
                    SetLaserByType(value);
                }
            }
        }

        public void SetLaserByType(LaserType type)
        {
            switch (type)
            {
                case LaserType.SaturnFixed:
                    this.Laser = LaserFactory.FixedLaser;
                    break;
                case LaserType.SaturnActive:
                    this.Laser = LaserFactory.ActiveLaser;
                    break;
                case LaserType.Alignment:
                    this.Laser = LaserFactory.AlignLaser;
                    break;
                default:
                    this.Laser = LaserFactory.FixedLaser;
                    break;
            }
            this.laser.ClearLaser();
        }

        private bool invokeLocalHotKeyPressed;
        public bool InvokeLocalHotKeyPressed
        {
            get { return this.invokeLocalHotKeyPressed; }
            set { this.invokeLocalHotKeyPressed = value; }
        }

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            hotKeyManager = new HotKeyManager(this);
            hotKeyManager.LocalHotKeyPressed += HotKeyManager_LocalHotKeyPressed;
            InvokeLocalHotKeyPressed = true;
            resources = new ComponentResourceManager(typeof(MainForm));
            listViewItemArray = new ListViewItemArray();

            //this.SizeChanged += EntryForm_SizeChanged;
            this.MouseWheel += EntryForm_MouseWheel;
            this.FormClosing += EntryForm_FormClosing;
            this.FormClosed += EntryForm_FormClosed;
            this.richPictureBox.FlipType = FlipType.Empty;
            DelegateClass.GetDelegate().VideoKeyUpHandler += this.OnKeyUp;
            DelegateClass.GetDelegate().VideoKeyDownHandler += this.OnKeyDown;
            DelegateClass.GetDelegate().ChangeSysFunctionHandler += this.ChangeSysFunctionHandler;
            DelegateClass.GetDelegate().CheckCloseVideoHandler += this.CheckCloseVideoHandler;
            DelegateClass.GetDelegate().CaptureDeviceHandler += this.CaptureDeviceHandler;

            serialPortCom = SerialPortManager.GetInstance();

            this.materialTitleBar1.CloseHandler += CloseHandler;
            this.materialTitleBar1.MinHandler += MinHandler;
            this.materialTitleBar1.Icon = this.Icon;
            this.materialTitleBar1.Text = Properties.Resources.StrMainTitle;
            this.materialTitleBar1.Invalidate();
        }

        private void MinHandler(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CloseHandler(object sender, EventArgs e)
        {
            //if (laserAlignment.Visible)
            //{
            //    DialogResult result = MsgBox.Show(Properties.Resources.StrExitExceptionMsg, Properties.Resources.StrExit, MsgBox.Buttons.OK, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn);
            //}
            //else
            //{
            //    DialogResult result = MsgBox.Show(Properties.Resources.StrExitMsg, Properties.Resources.StrExit, MsgBox.Buttons.YesNo, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn);
            //    if (result == DialogResult.Yes)
            //    {
            //        //CheckLaserStatus();
            //        this.Close();
            //    }
            //}
            DialogResult result = MsgBox.Show(Properties.Resources.StrExitMsg, Properties.Resources.StrExit, MsgBox.Buttons.YesNo, MsgBox.Icon.Info);
            if (result == DialogResult.Yes)
            {
                CheckLaserStatus();
                this.Close();
            }
        }

        /// <summary>
        /// 检查红光状态并关闭
        /// </summary>
        private void CheckLaserStatus()
        {
            LaserC01Request c01 = new LaserC01Request();
            var bytes = serialPortCom.Encode(c01);
            byte[] recData = serialPortCom.SendData(bytes);
            if (recData != null)
            {
                if (recData.Length == 6)
                {
                    var flag = recData[1] * 128 + recData[2];
                    if (flag == 1152)
                    {
                        EnableRedLaser(false);
                    }
                }
            }
        }

        private void EnableRedLaser(bool enable)
        {
            var c70 = new LaserC70Request();
            var bytes = serialPortCom.Encode(c70);
            serialPortCom.SendData(bytes);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeControls();
            InitializeBaseCtrls();
            Application.Idle += OnIdle;

            //fullScreen = new FullScreen(this);
            //fullScreen.ShowFullScreen();

            //LoadDebugCtrl();

            this.systemMonitorTimer.Enabled = true;
            this.LaserFactory = LaserFactory.GetInstance(this.richPictureBox);
            LaserType = LaserType.SaturnFixed;

            Coordinate.GetCoordinate().MoveStepHandler += MoveStepHandler;
            if (!string.IsNullOrEmpty(Program.SysConfig.DeviceName))
                InitializeDefaultDevice();

            if (Program.SysConfig != null)
            {
                Program.SysConfig.PropertyChanged += SysConfig_PropertyChanged;
            }

            //InitializeIdleTimer();
            this.richPictureBox.RestrictArea.TransformMotorOriginalPoints();
            settingControl.SettingControl_Load(null, null);
            this.LaserCheckTimer.Enabled = true;
            CreateMeasureItems();
        }

        public int GetLastInputTime()
        {
            int idletime = 0;
            idletime = 0;
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            if (GetLastInputInfo(ref lastInputInfo))
            {
                idletime = (int)(Environment.TickCount - lastInputInfo.dwTime);
            }
            return idletime;
        }

        private void HotKeyManager_LocalHotKeyPressed(object sender, LocalHotKeyEventArgs e)
        {
            if (e.HotKey.Name != null && InvokeLocalHotKeyPressed)
            {
                switch (e.HotKey.Name)
                {
                    case "takePicture":
                        toolstripBtnScreenShort_Click(null, null);
                        break;
                    case "zoomIn":
                        toolstripBtnZoomIn_Click(null, null);
                        break;
                    case "zoomOut":
                        toolstripBtnZoomOut_Click(null, null);
                        break;
                    case "startRecord":
                    case "stopRecord":
                        toolstripBtnVideo_Click(null, null);
                        break;
                    case "fireLaser":
                        laserCtrl.btnFire_Click(null, null);
                        break;
                }

            }
        }

        private void CheckCloseVideoHandler()
        {
            StopVideoDevice();
        }

        private void ChangeSysFunctionHandler()
        {
            switch (Program.SysConfig.Function)
            {
                case SystemFunction.Laser:
                    if (!BaseCtrlVisiable(CtrlType.LaserCtrl))
                    {
                        ShowBaseCtrl(true, CtrlType.LaserCtrl);
                    }
                    break;
                case SystemFunction.Measure:
                    if (!BaseCtrlVisiable(CtrlType.StatisticsCtrl))
                    {
                        ShowBaseCtrl(true, CtrlType.StatisticsCtrl);
                    }
                    break;
                case SystemFunction.Empty:
                    break;
            }
        }

        private bool BaseCtrlVisiable(CtrlType type)
        {
            foreach (var baseCtrl in this.baseControls)
            {
                if (baseCtrl.CtrlType == type)
                {
                    return baseCtrl.Visible;
                }
            }
            return false;
        }

        private void EntryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SysConfig.Save(Program.SysConfig, Program.SysConfigOrigin);
        }

        private void EntryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopVideo();
            StopVideoDevice();
            this.systemMonitorTimer.Enabled = false;
            this.LaserCheckTimer.Enabled = false;

            if (serialPortCom != null)
            {
                if (serialPortCom.SerialPort.IsOpen) serialPortCom.Close();
            }
            UnregisterEvent();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.panel1.Size = new Size(this.panel1.Size.Width, this.Height - 32);
        }

        #region ToolStrip button click
        private void toolstripBtnScreenShort_Click(object sender, EventArgs e)
        {
            if (CheckStorePath(Program.SysConfig.StorePath))
            {
                string fileName = string.Format(@"{0}\{1}.png", Program.SysConfig.StorePath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff"));

                if (Program.SysConfig.ScreenshotWithLocation)
                {
                    FixedLaser laser = Program.EntryForm.Laser as FixedLaser;
                    if (laser != null)
                    {
                        string folder = Program.SysConfig.StorePath + "\\Accuracy";
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                        PointF centerPoint = laser.CenterPoint;
                        fileName = string.Format(@"{0}\{1}_{2}_{3}.png", folder, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff"), centerPoint.X, centerPoint.Y);
                    }
                }
                if (Program.SysConfig.LiveMode)
                {
                    if (this.richPictureBox.VideoFrame != null)
                    {
                        Bitmap b = this.ResizeImage(this.richPictureBox.VideoFrame, this.videoControl.Bounds.Width, this.videoControl.Bounds.Height);
                        b.Save(fileName, ImageFormat.Png);
                        MaterialSkin.ToastNotification.Instance().ShowToast(Properties.Resources.StrScreenshotSuccess, global::CII.LAR.Properties.Resources.capture);
                    }
                }
                else
                {
                    if (this.richPictureBox.Image != null)
                    {
                        this.richPictureBox.Image.Save(fileName);
                        MaterialSkin.ToastNotification.Instance().ShowToast(Properties.Resources.StrScreenshotSuccess, global::CII.LAR.Properties.Resources.capture);

                    }
                }
            }
        }

        private void toolstripBtnVideo_Click(object sender, EventArgs e)
        {
            try
            {
                CaptureVideo = !CaptureVideo;
                if (CaptureVideo)
                {
                    var filePath = Program.SysConfig.StorePath;
                    if (!Directory.Exists(filePath))
                    {
                        File.Create(filePath);
                    }
                    if (videoDevice != null)
                    {
                        int h = videoDevice.VideoCapabilities[0].FrameSize.Height;
                        int w = videoDevice.VideoCapabilities[0].FrameSize.Width;
                        string fileName = string.Format(@"{0}\{1}.avi", filePath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff"));
                        this.richPictureBox.AVIwriter.Open(fileName, w, h);
                        this.toolstripBtnVideo.Image = global::CII.LAR.Properties.Resources.Stop;
                        this.richPictureBox.CanCapture = true;
                        if (Program.SysConfig.RecordTime != 0)
                        {
                            recordTimer = new System.Timers.Timer(1000);
                            recordTimer.Elapsed += RecordTimer_Elapsed;
                            this.richPictureBox.RecordCount = 0;
                            recordTimer.Start();
                        }
                    }
                }
                else
                {
                    StopVideo();
                    this.toolstripBtnVideo.Image = global::CII.LAR.Properties.Resources.video;
                }
            }
            catch (Exception ex)
            {
                this.richPictureBox.CanCapture = false;
            }
        }

        private void StopVideo()
        {
            if (videoDevice == null) return;
            if (videoDevice.IsRunning)
            {
                this.richPictureBox.CanCapture = false;
                this.richPictureBox.AVIwriter.Close();
            }
            if (recordTimer != null && recordTimer.Enabled)
            {
                recordTimer.Stop();
                recordTimer.Dispose();
            }
        }

        private void toolstripBtnFiles_Click(object sender, EventArgs e)
        {
            try
            {
                FilesForm filesForm = new FilesForm();
                filesForm.ShowDialog();
            }
            catch (Exception ex)
            {
            }
        }

        private void toolstripBtnMeasure_Click(object sender, EventArgs e)
        {
            //CreateMeasureItems();
        }

        private void CreateMeasureItems()
        {
            var lenses = Program.SysConfig.Lenses;
            if (lenses != null && lenses.Count > 0)
            {
                //ToolStripItem[] toolstripItem = new ToolStripItem[lenses.Count + 1];
                this.toolstripBtnMeasure.DropDownItems.Clear();
                MaterialToolStripMenuItem itemOne = new MaterialToolStripMenuItem();
                itemOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
                itemOne.Depth = 0;
                itemOne.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(226)))), ((int)(((byte)(241)))));
                itemOne.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
                itemOne.Name = "lenseItemOne";
                Lense lense = new Lense(1);
                itemOne.Text = lense.Name;
                itemOne.Tag = lense;
                itemOne.Click += new System.EventHandler(this.MeasureItem_Click);
                this.toolstripBtnMeasure.DropDownItems.Add(itemOne);
                for (int i=0; i< lenses.Count; i++)
                {
                    if (lenses[i].Factor == 1f) continue;
                    MaterialToolStripMenuItem item = new MaterialToolStripMenuItem();
                    item.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
                    item.Depth = 0;
                    item.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(226)))), ((int)(((byte)(241)))));
                    item.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
                    item.Name = "lenseItem" + i;
                    item.Text = lenses[i].Name;
                    item.Tag = lenses[i];
                    item.Click += new System.EventHandler(this.MeasureItem_Click);
                    this.toolstripBtnMeasure.DropDownItems.Add(item);
                }
                MaterialToolStripMenuItem itemHide = new MaterialToolStripMenuItem();
                itemHide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
                itemHide.Depth = 0;
                itemHide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(226)))), ((int)(((byte)(241)))));
                itemHide.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
                itemHide.Name = "lenseItemHide";
                itemHide.Text = Properties.Resources.StrHideRuler;
                itemHide.Click += new System.EventHandler(this.MeasureItem_Click);
                this.toolstripBtnMeasure.DropDownItems.Add(itemHide);
            }
            else
            {
                this.toolstripBtnMeasure.DropDownItems.Clear();
                MaterialToolStripMenuItem itemOne = new MaterialToolStripMenuItem();
                itemOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
                itemOne.Depth = 0;
                itemOne.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(226)))), ((int)(((byte)(241)))));
                itemOne.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
                itemOne.Name = "lenseItemOne";
                Lense lense = new Lense(1);
                itemOne.Text = lense.Name;
                itemOne.Tag = lense;
                itemOne.Click += new System.EventHandler(this.MeasureItem_Click);
                this.toolstripBtnMeasure.DropDownItems.Add(itemOne);

                MaterialToolStripMenuItem itemHide = new MaterialToolStripMenuItem();
                itemHide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
                itemHide.Depth = 0;
                itemHide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(226)))), ((int)(((byte)(241)))));
                itemHide.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
                itemHide.Name = "lenseItemHide";
                itemHide.Text = Properties.Resources.StrHideRuler;
                itemHide.Click += new System.EventHandler(this.MeasureItem_Click);
                this.toolstripBtnMeasure.DropDownItems.Add(itemHide);
            }
        }

        private void MeasureItem_Click(object sender, EventArgs e)
        {
            var lense = (sender as MaterialToolStripMenuItem).Tag as Lense;
            if (lense != null)
            {
                Program.SysConfig.Lense = lense;
                this.richPictureBox.Rulers.ShowRulers = true;
            }
            else
            {
                this.richPictureBox.Rulers.ShowRulers = false;
            }
            this.richPictureBox.Invalidate();
        }

        private void toolstripBtnLine_Click(object sender, EventArgs e)
        {
            SetActiveTool(DrawToolType.Line);
        }

        private void toolstripBtnRectangle_Click(object sender, EventArgs e)
        {
            SetActiveTool(DrawToolType.Rectangle);
        }

        private void toolstripBtnEllipse_Click(object sender, EventArgs e)
        {
            SetActiveTool(DrawToolType.Ellipse);
        }

        private void toolstripBtnHand_Click(object sender, EventArgs e)
        {
            SetActiveTool(DrawToolType.Move);
        }

        private void toolstripBtnZoomIn_Click(object sender, EventArgs e)
        {
            if (this.richPictureBox.CanZoom())
            {
                this.richPictureBox.ZoomIn();
                EnableDrawTools(this.richPictureBox.Zoom == 1.0f);
            }
        }

        private void toolstripBtnZoomOut_Click(object sender, EventArgs e)
        {
            if (this.richPictureBox.CanZoom())
            {
                this.richPictureBox.ZoomOut();
                EnableDrawTools(this.richPictureBox.Zoom == 1.0f);
            }
        }

        private void toolstripBtnFit_Click(object sender, EventArgs e)
        {
            this.richPictureBox.ZoomFit();
            EnableDrawTools(this.richPictureBox.Zoom == 1.0f);
        }

        private void toolstripBtnLaser_Click(object sender, EventArgs e)
        {
            this.richPictureBox.LaserFunction = true;
            if (LaserType == LaserType.SaturnFixed)
            {
                this.richPictureBox.ActiveTool = DrawToolType.Circle;
            }
            else if (LaserType == LaserType.SaturnActive)
            {
                this.richPictureBox.ActiveTool = DrawToolType.MultipleCircle;
            }
            ShowBaseCtrl(true, CtrlType.LaserCtrl);
            //this.richPictureBox.GraphicsList.DeleteAll();
            this.richPictureBox.Invalidate();
            SetLaserByType(LaserType);
        }

        private void toolstripBtnSetting_Click(object sender, EventArgs e)
        {
            ShowBaseCtrl(true, CtrlType.SettingCtrl);
        }

        private void toolstripBtnCamera_Click(object sender, EventArgs e)
        {

        }

        private void openCameraLiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowBaseCtrl(true, CtrlType.VideoChooseCtrl);
            this.videoChooseCtrl.EnumerateVideoDevices();
        }

        private void closeCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopVideoDevice();
        }

        private bool flipHorizontal;
        private bool flipVertical;
        private void horizontalFlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flipHorizontal = !flipHorizontal;
            this.horizontalFlipToolStripMenuItem.Checked = flipHorizontal;
            if (flipHorizontal)
            {
                this.richPictureBox.FlipType = FlipType.Horizontal;
                this.verticalFlipToolStripMenuItem.Checked = false;
            }
            else
            {
                this.richPictureBox.FlipType = FlipType.Empty;
            }
        }

        private void verticalFlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flipVertical = !flipVertical;
            this.verticalFlipToolStripMenuItem.Checked = flipVertical;
            if (flipVertical)
            {
                this.richPictureBox.FlipType = FlipType.Vertical;
                this.horizontalFlipToolStripMenuItem.Checked = false;
            }
            else
            {
                this.richPictureBox.FlipType = FlipType.Empty;
            }

        }

        private void toolstripBtnDebug_Click(object sender, EventArgs e)
        {
            //ShowBaseCtrl(true, CtrlType.LaserDebugCtrl);
            laserDebugForm = new LaserDebugCtrl();
            ////controller = new IController(laserDebugCtrl);
            //laserDebugForm.SetController(this.controller);
            this.LaserCheckTimer.Enabled = false;
            laserDebugForm.ShowDialog();
        }

        private void toolstripBtnAbout_Click(object sender, EventArgs e)
        {
            ShowBaseCtrl(true, CtrlType.AboutCtrl);
        }
        #endregion

        private LaserDebugCtrl laserDebugForm;
        private void RecordTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.richPictureBox.RecordCount == Program.SysConfig.RecordTime * 60)
            {
                recordTimer.Stop();
                CaptureVideo = false;
                if (videoDevice == null) return;
                if (videoDevice.IsRunning)
                {
                    this.richPictureBox.CanCapture = false;
                    this.richPictureBox.AVIwriter.Close();
                }
                this.toolstripBtnVideo.Image = global::CII.LAR.Properties.Resources.video;
            }
            else
            {
                CaptureVideo = true;
            }
            this.richPictureBox.RecordCount++;
        }

        private bool CheckStorePath(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            return true;
        }

        private System.Timers.Timer recordTimer;
        private void SetActiveTool(DrawToolType toolType)
        {
            this.richPictureBox.LaserFunction = false;
            this.richPictureBox.ActiveTool = toolType;
            ShowBaseCtrl(true, CtrlType.StatisticsCtrl);
        }

        private void OnIdle(object sender, EventArgs e)
        {
            if (this.richPictureBox.Picture != null && Program.SysConfig.LiveMode == true)
            {
                EnableDrawTools(true);
            }
            else
            {
                bool enable = (this.richPictureBox.Zoom == 1 && this.richPictureBox.Picture != null);
                EnableDrawTools(enable);
                if (!enable) this.richPictureBox.ActiveTool = DrawToolType.Pointer;
            }
            //if (this.richPictureBox.df != null)
            //{
            //    this.richPictureBox.df.UpdateLaserStatus();
            //}
            if (this.systemInfoCtrl != null)
            {
                this.systemInfoCtrl.UpdateStatus();
            }
        }

        private void MoveStepHandler(int x, byte ox, int y, byte oy)
        {
            if (this.richPictureBox.df != null)
            {
                this.richPictureBox.df.UpdateMoveStep(x, ox, y, oy);
            }
        }
        private void UnregisterEvent()
        {
            if (settingControl != null)
            {
                settingControl.ShowObjectLenseManagerHandler -= ShowObjectLenseManagerHandler;
                settingControl.ShowShortcutManagerHandler -= ShowShortcutManagerHandler;
                settingControl.ShowScaleAppearanceCtrlHandler -= ShowScaleAppearanceCtrlHandler;
            }
            if (laserAlignment != null)
            {
                laserAlignment.VideoKeyDownHandler -= this.OnKeyDown;
            }
            if (lenseCtrl != null)
            {
                lenseCtrl.LenseChangeHandler -= LenseChangeHandler;
            }
        }
        private void InitializeControls()
        {
            CtrlFactory.InitializeCtrlFactory(this.richPictureBox);
            BaseCtrls = new List<BaseCtrl>();

            settingControl = CtrlFactory.GetCtrlFactory().GetCtrlByType<SettingControl>(CtrlType.SettingCtrl);
            settingControl.ShowObjectLenseManagerHandler += ShowObjectLenseManagerHandler;
            settingControl.ShowShortcutManagerHandler += ShowShortcutManagerHandler;
            settingControl.ShowScaleAppearanceCtrlHandler += ShowScaleAppearanceCtrlHandler;
            BaseCtrls.Add(settingControl);

            statisticsCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<StatisticsCtrl>(CtrlType.StatisticsCtrl);
            BaseCtrls.Add(statisticsCtrl);

            laserAppearanceCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserAppearanceCtrl>(CtrlType.LaserAppreance);
            BaseCtrls.Add(laserAppearanceCtrl);

            rulerAppearanceCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<RulerAppearanceCtrl>(CtrlType.RulerAppearanceCtrl);
            BaseCtrls.Add(rulerAppearanceCtrl);

            scaleAppearanceCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<ScaleAppearanceCtrl>(CtrlType.ScaleAppearanceCtrl);
            BaseCtrls.Add(scaleAppearanceCtrl);

            laserCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserCtrl>(CtrlType.LaserCtrl);
            BaseCtrls.Add(laserCtrl);

            laserAlignment = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserAlignment>(CtrlType.LaserAlignment);
            //laserAlignment.SetController(this.controller);
            laserAlignment.RichPictureBox = this.richPictureBox;
            laserAlignment.VideoKeyDownHandler += this.OnKeyDown;
            BaseCtrls.Add(laserAlignment);

            videoChooseCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<VideoChooseCtrl>(CtrlType.VideoChooseCtrl);

            BaseCtrls.Add(videoChooseCtrl);

            laserHoleSize = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserHoleSize>(CtrlType.LaserHoleSize);
            BaseCtrls.Add(laserHoleSize);

            lenseCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<ObjectLenseCtrl>(CtrlType.LenseCtrl);
            lenseCtrl.LenseChangeHandler += LenseChangeHandler;
            BaseCtrls.Add(lenseCtrl);

            shortcutCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<ShortcutCtrl>(CtrlType.ShortCut);
            BaseCtrls.Add(shortcutCtrl);

            aboutCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<AboutControl>(CtrlType.AboutCtrl);
            BaseCtrls.Add(aboutCtrl);

            systemInfoCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<SystemInfoCtrl>(CtrlType.SystemInoCtrl);
            BaseCtrls.Add(systemInfoCtrl);
        }

        private void LenseChangeHandler(object sender, EventArgs e)
        {
            CreateMeasureItems();
        }

        private void ShowObjectLenseManagerHandler()
        {
            ShowBaseCtrl(true, CtrlType.LenseCtrl);
        }

        private void ShowShortcutManagerHandler()
        {
            ShowBaseCtrl(true, CtrlType.ShortCut);
        }

        private void ShowScaleAppearanceCtrlHandler()
        {
            ShowBaseCtrl(true, CtrlType.ScaleAppearanceCtrl);
        }

        private FilterInfo EnumerateVideoDevices()
        {
            FilterInfo fileInfo = null;
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices != null && videoDevices.Count > 0)
            {
                foreach (var device in videoDevices)
                {
                    FilterInfo filterInfo = device as FilterInfo;
                    if (filterInfo != null)
                    {
                        if (filterInfo.Name == Program.SysConfig.DeviceName)
                        {
                            fileInfo = filterInfo;
                            break;
                        }
                    }
                }
            }
            return fileInfo;
        }

        private void InitializeDefaultDevice()
        {
            FilterInfo fileInfo = EnumerateVideoDevices();
            if (fileInfo != null)
            {
                CaptureDeviceHandler(fileInfo.MonikerString);
            }
        }

        public void CaptureDeviceHandler(string deviceMoniker)
        {
            videoDevice = new VideoCaptureDevice(deviceMoniker);
            if (videoDevice != null)
            {
                Program.SysConfig.LiveMode = true;
                Size videoSize = this.richPictureBox.RealSize;
                int offsetX = (this.richPictureBox.Width - videoSize.Width) / 2;
                int offsetY = (this.richPictureBox.Height - videoSize.Height) / 2;
                this.richPictureBox.SetOffset(offsetX, offsetY);
                this.videoControl.Bounds = new Rectangle(0, 0, videoSize.Width, videoSize.Height);
                this.videoControl.VideoSource = videoDevice;
                this.videoControl.VideoSource.NewFrame += new NewFrameEventHandler(this.richPictureBox.VideoSource_NewFrame);
                this.videoControl.Start();
                this.richPictureBox.Zoom = 1;
                if (settingControl != null)
                {
                    settingControl.CloseSimulator();
                }
            }
        }

        /// <summary>
        /// 关闭视频设备
        /// </summary>
        public void StopVideoDevice()
        {
            if (this.videoControl.VideoSource != null)
            {
                this.videoControl.SignalToStop();
                this.videoControl.WaitForStop();
                this.videoControl.VideoSource = null;
            }
            if (this.richPictureBox != null)
            {
                this.richPictureBox.Picture = null;
                this.richPictureBox.GraphicsList.DeleteAll();
                this.statisticsCtrl.StatisticsListView.Items.Clear();
                this.richPictureBox.Invalidate();
            }
        }

        private void InitializeBaseCtrls()
        {
            foreach (var ctrl in this.BaseCtrls)
            {
                ctrl.InitializeLocation(this.Size);
                ctrl.MouseDown += BaseCtrl_MouseDown;
                ctrl.MouseMove += BaseCtrl_MouseMove;
                ctrl.MouseUp += BaseCtrl_MouseUp;
                ctrl.Visible = false;
                ctrl.Enabled = false;
                this.Controls.Add(ctrl);
            }
            DelegateClass.GetDelegate().ClickDelegateHandler += this.ClickDelegateHandler;
        }

        private BaseCtrl GetBascCtrl(CtrlType type)
        {
            foreach (var bc in this.BaseCtrls)
            {
                if (bc.CtrlType == type)
                {
                    return bc;
                }
            }
            return null;
        }

        /// <summary>
        /// switch to different base control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="name"></param>
        private void ClickDelegateHandler(object sender, CtrlType type)
        {
            ShowBaseCtrl(true, GetBascCtrl(type));
        }

        /// <summary>
        /// show laser control
        /// </summary>
        /// <param name="show"></param>
        public void ShowBaseCtrl(bool show, CtrlType type)
        {
            for (int i = 0; i < this.baseControls.Count; i++)
            {
                if (this.baseControls[i].CtrlType == type)
                {
                    this.baseCtrl = GetBascCtrl(type);
                    this.Controls.SetChildIndex(this.baseCtrl, 0);
                    this.baseCtrl.Visible = show;
                    this.baseCtrl.Enabled = show;
                    EnableAppearanceButton();
                }
                else
                {
                    this.baseControls[i].Visible = !show;
                    this.baseControls[i].Enabled = !show;
                }
            }
        }

        /// <summary>
        /// switch to the base control
        /// </summary>
        /// <param name="show"></param>
        /// <param name="baseCtrl"></param>
        public void ShowBaseCtrl(bool show, BaseCtrl baseCtrl)
        {
            if (baseCtrl != null)
            {
                this.baseCtrl.Visible = false;
                this.baseCtrl.Enabled = false;

                this.baseCtrl = baseCtrl;
                this.Controls.SetChildIndex(this.baseCtrl, 0);

                this.baseCtrl.Visible = show;
                this.baseCtrl.Enabled = show;
            }
        }

        private void EntryForm_MouseWheel(object sender, MouseEventArgs e)
        {
        }

        private void LoadDebugCtrl()
        {
            //df = new DebugCtrl();
            //df.VideoKeyDownHandler += this.OnKeyDown;
            //df.Location = new Point(10, 30);
            //this.Controls.Add(df);
            this.richPictureBox.LoadDebugCtrl();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (fullScreen != null)
            {
                fullScreen.ResetFullScreen();
            }
            base.OnFormClosing(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //base.OnMouseWheel(e);
            if (Program.EntryForm.LaserType != LaserType.Alignment)
            {
                this.richPictureBox.RichPictureBoxMouseWheel(e);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && this.richPictureBox.ActiveTool != DrawToolType.Pointer)
            {
                this.richPictureBox.LaserFunction = false;
                this.richPictureBox.ResetClickCount();
                this.richPictureBox.ActiveTool = DrawToolType.Pointer;
                if (this.richPictureBox.GraphicsList != null && this.richPictureBox.GraphicsList.Count > 0)
                {
                    foreach (var toolObject in this.richPictureBox.GraphicsList)
                    {
                        toolObject.Creating = false;
                    }
                }
            }

        }

        /// <summary>
        /// Update statistics information in listviewEx control
        /// </summary>
        /// <param name="statistics"></param>
        public void UpdateStatisticInfoHandler(DrawObject drawObject, Statistics statistics)
        {
            AppendItems(drawObject, statistics);
        }

        /// <summary>
        /// Append new list view item to StatisticsListView items
        /// </summary>
        /// <param name="drawObject"></param>
        /// <param name="statistics"></param>
        private void AppendItems(DrawObject drawObject, Statistics statistics)
        {
            if (this.richPictureBox.ActiveTool == DrawToolType.Pointer)
            {
                try
                {
                    foreach (ListViewItem item in this.statisticsCtrl.StatisticsListView.Items)
                    {
                        if (item != null && item.Text == drawObject.Name)
                        {
                            if (statistics.Circumference != null) item.SubItems[1].Text = statistics.Circumference.ToString();
                            if (statistics.Area != null) item.SubItems[2].Text = statistics.Area.ToString();
                        }
                    }
                    CalculateStatisticsInformation();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                if (this.richPictureBox.DrawObject == null || (drawObject.Name != this.richPictureBox.DrawObject.Name && !Exist(drawObject.Name)))
                {
                    try
                    {
                        this.richPictureBox.DrawObject = drawObject;
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = drawObject.Name;
                        lvi.SubItems.Add(statistics.Circumference.ToString());
                        lvi.SubItems.Add(statistics.Area.ToString());
                        Console.WriteLine(statistics.Circumference.ToString());
                        this.statisticsCtrl.StatisticsListView.Items.Add(lvi);
                        ListViewItemEx listViewItemEx = new ListViewItemEx(lvi, drawObject);
                        AddEmbeddedControlToListView(listViewItemEx);
                        EnableAppearanceButton();
                    }
                    catch (Exception ee)
                    {
                        LogHelper.GetLogger<MainForm>().Error(ee.Message);
                        LogHelper.GetLogger<MainForm>().Error(ee.StackTrace);
                    }
                }
                else if (this.richPictureBox.DrawObject != null && drawObject.Name == this.richPictureBox.DrawObject.Name)
                {
                    foreach (ListViewItem item in this.statisticsCtrl.StatisticsListView.Items)
                    {
                        if (item != null && item.Text == this.richPictureBox.DrawObject.Name)
                        {
                            if (statistics.Circumference != null) item.SubItems[1].Text = statistics.Circumference.ToString();
                            if (statistics.Area != null) item.SubItems[2].Text = statistics.Area.ToString();
                        }
                    }
                }
                CalculateStatisticsInformation();
            }
        }

        private void CalculateStatisticsInformation()
        {
            if (this.statisticsCtrl.StatisticsListView.Items !=null)
            {
                if (this.statisticsCtrl.StatisticsListView.Items.Count == 0)
                {
                    statisticsCtrl.CalculateStatiscsInformation(0, 0, 0, 0, 0, 0);
                }
                else if (this.statisticsCtrl.StatisticsListView.Items.Count > 0)
                {
                    double minCir = 0, maxCir = 0, aveCir = 0;
                    double minArea = 0, maxArea = 0, aveArea = 0;
                    List<double> circumferences = new List<double>();
                    List<double> areas = new List<double>();
                    foreach (ListViewItem item in this.statisticsCtrl.StatisticsListView.Items)
                    {
                        if (item != null)
                        {
                            var cir = item.SubItems[1].Text;
                            if (!string.IsNullOrEmpty(cir) && cir != "null")
                            {
                                int indexOf = cir.IndexOf(" um");
                                var c = cir.Substring(0, indexOf);
                                circumferences.Add(Double.Parse(c));
                            }
                            var area = item.SubItems[2].Text;
                            if (!string.IsNullOrEmpty(area) && area != "null")
                            {
                                int indexOf = area.IndexOf(" um²");
                                var a = area.Substring(0, indexOf);
                                areas.Add(Double.Parse(a));
                            }
                        }
                    }
                    if (circumferences.Count > 0)
                    {
                        minCir = circumferences.Min();
                        maxCir = circumferences.Max();
                        aveCir = circumferences.Average();
                    }
                    if (areas.Count > 0)
                    {
                        minArea = areas.Min();
                        maxArea = areas.Max();
                        aveArea = areas.Average();
                    }
                    statisticsCtrl.CalculateStatiscsInformation(minCir, maxCir, aveCir, minArea, maxArea, aveArea);
                }

            }

        }

        private bool Exist(string name)
        {
            bool exist = false;
            if (this.statisticsCtrl.StatisticsListView.Items != null && this.statisticsCtrl.StatisticsListView.Items.Count > 0)
            {
                foreach (ListViewItem item in this.statisticsCtrl.StatisticsListView.Items)
                {
                    if (item != null && item.Text == name)
                    {
                        exist = true;
                        break;
                    }
                }
            }
            return exist;
        }

        /// <summary>
        /// Add embedded delete button control to list view
        /// </summary>
        /// <param name="listViewItemEx"></param>
        private void AddEmbeddedControlToListView(ListViewItemEx listViewItemEx)
        {
            TransparentButton deleteButton = new TransparentButton();
            deleteButton.BackColor = System.Drawing.Color.Transparent;
            deleteButton.BackgroundImage = global::CII.LAR.Properties.Resources.delete;
            deleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            deleteButton.Name = "removeButton";
            deleteButton.Size = new System.Drawing.Size(16, 16);
            deleteButton.Tag = listViewItemEx;
            listViewItemArray.AddItem(listViewItemEx.ListViewItem);
            this.statisticsCtrl.StatisticsListView.AddEmbeddedControl(deleteButton, 3, listViewItemArray.Count - 1);
            deleteButton.Click += DeleteButton_Click;
        }

        public void ClearStatisticsListViewItems()
        {
            listViewItemArray.ClearItem();
            this.statisticsCtrl.StatisticsListView.Items.Clear();
        }

        /// <summary>
        /// Delete button click event
        /// delete listviewitem and draw object graphic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var index = (int)this.richPictureBox.ActiveTool;
            var tool = this.richPictureBox.Tools[index];
            if (tool.ClickCount % 2 != 0) return;
            TransparentButton deleteButton = sender as TransparentButton;
            if (deleteButton != null)
            {
                deleteButton.Click -= DeleteButton_Click;
                ListViewItemEx listViewItemEx = (ListViewItemEx)deleteButton.Tag;
                listViewItemArray.DeleteItem(listViewItemEx.ListViewItem);
                this.statisticsCtrl.StatisticsListView.Items.Remove(listViewItemEx.ListViewItem);
                this.statisticsCtrl.StatisticsListView.Invalidate();
                DeleteDrawObject(listViewItemEx.DrawObject);
            }
        }

        /// <summary>
        /// delete draw objcect graphic
        /// </summary>
        /// <param name="drawObject"></param>
        private void DeleteDrawObject(DrawObject drawObject)
        {
            if (drawObject != null)
            {
                this.richPictureBox.GraphicsList.DeleteDrawObject(drawObject);
                this.richPictureBox.Invalidate();
                EnableAppearanceButton();
                CalculateStatisticsInformation();
            }
        }

        /// <summary>
        /// enable or disable ruler appearance button
        /// </summary>
        private void EnableAppearanceButton()
        {
            var baseCtrl = this.baseControls[2] as StatisticsCtrl;
            if (baseCtrl != null)
            {
                if (this.richPictureBox.GraphicsList != null && this.richPictureBox.GraphicsList.Count > 0)
                {
                    baseCtrl.BtnAppearance.Enabled = true;
                }
                else
                {
                    baseCtrl.BtnAppearance.Enabled = false;
                }
            }
        }

        private void EnableDrawTools(bool Enabled)
        {
            this.toolstripBtnLine.Enabled = Enabled;
            this.toolstripBtnRectangle.Enabled = Enabled;
            this.toolstripBtnEllipse.Enabled = Enabled;
            //this.toolstripBtnLaser.Enabled = Enabled;
        }

        #region 鼠标点击拖动BaseCtrl
        /// <summary>
        /// Draw a reversible rectangle
        /// </summary>
        /// <param name="rect">rectangle to be drawn</param>
        private void DrawReversibleRect(Rectangle rect)
        {
            // Convert the location of rectangle to screen coordinates.
            rect.Location = PointToScreen(rect.Location);

            // Draw the reversible frame.
            ControlPaint.DrawReversibleFrame(rect, Color.Navy, FrameStyle.Thick);
        }

        private void BaseCtrl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isDraggingBaseCtrl)
            {
                isDraggingBaseCtrl = false;

                // erase dragging rectangle
                DrawReversibleRect(draggingBaseCtrlRectangle);

                // move the Statistics control to the new position
                this.baseCtrl.Location = draggingBaseCtrlRectangle.Location;
            }
        }

        private void BaseCtrl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isDraggingBaseCtrl)
            {
                // caculating next candidate dragging rectangle
                Point newPos = new Point(draggingBaseCtrlRectangle.Location.X + e.X - lastBaseCtrlMousePos.X,
                                         draggingBaseCtrlRectangle.Location.Y + e.Y - lastBaseCtrlMousePos.Y);
                Rectangle newPictureTrackerArea = draggingBaseCtrlRectangle;
                newPictureTrackerArea.Location = newPos;

                // saving current mouse position to be used for next dragging
                this.lastBaseCtrlMousePos = new Point(e.X, e.Y);

                // dragging Statistics ctrl only when the candidate dragging rectangle
                // is within this ScalablePictureBox control
                if (this.ClientRectangle.Contains(newPictureTrackerArea))
                {
                    // removing previous rubber-band frame
                    DrawReversibleRect(draggingBaseCtrlRectangle);

                    // updating dragging rectangle
                    draggingBaseCtrlRectangle = newPictureTrackerArea;

                    // drawing new rubber-band frame
                    DrawReversibleRect(draggingBaseCtrlRectangle);
                }
            }
        }

        private void BaseCtrl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            isDraggingBaseCtrl = true;    // Make a note that we are dragging Statistics control

            // Store the last mouse poit for this rubber-band rectangle.
            lastBaseCtrlMousePos.X = e.X;
            lastBaseCtrlMousePos.Y = e.Y;

            // draw initial dragging rectangle
            draggingBaseCtrlRectangle = this.baseCtrl.Bounds;
            DrawReversibleRect(draggingBaseCtrlRectangle);
        }
        #endregion

        public void ButtonStateHandler(bool isEnable)
        {
            LaserAlignment laserAlignment = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserAlignment>(CtrlType.LaserAlignment);
            if (laserAlignment != null)
            {
                laserAlignment.ButtonNext(isEnable);
            }
        }

        public void HolesInfoChangeHandler(HolesInfo holesInfo)
        {
            if (holesInfo != null)
            if (holesInfo != null)
                {
                LaserCtrl laserCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserCtrl>(CtrlType.LaserCtrl);
                if (laserCtrl != null)
                {
                    laserCtrl.UpdateHolesInfo(holesInfo);
                }
            }
        }

        public void HolesNumberSlider(bool isShow)
        {
            if (baseControls != null && baseControls.Count > 0)
            {
                LaserCtrl laserCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserCtrl>(CtrlType.LaserCtrl);
                if (laserCtrl != null)
                {
                    laserCtrl.HolesSliderVisiable(isShow);
                }
            }
        }

        public void UpdateHoleNumber(int value)
        {
            ActiveLaser activeLaser = Laser as ActiveLaser;
            if (activeLaser != null)
            {
                activeLaser.UpdateHoleNumber(value);
            }
        }

        private void SysConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Program.SysConfig.GetPropertyName(() => Program.SysConfig.UICulture))
            {
                RefreshUI();
                if (BaseCtrls != null && BaseCtrls.Count > 0)
                {
                    foreach (var bc in BaseCtrls)
                    {
                        bc.RefreshUI();
                    }
                }
                this.CreateMeasureItems();
            }
        }

        private void RefreshUI()
        {
            foreach (var ctrls in this.panel1.Controls)
            {
                MaterialToolStrip mtsp = ctrls as MaterialToolStrip;
                if (mtsp != null)
                {
                    foreach (var subItem in mtsp.Items)
                    {
                        ToolStripItem tsi = subItem as ToolStripItem;
                        if (tsi != null) resources.ApplyResources(tsi, tsi.Name);
                        ToolStripDropDownButton tsdd = subItem as ToolStripDropDownButton;
                        if (tsdd != null)
                        {
                            foreach (var item in tsdd.DropDownItems)
                            {
                                ToolStripMenuItem tsmi = item as ToolStripMenuItem;
                                if (tsmi != null) resources.ApplyResources(tsmi, tsmi.Name);
                            }
                        }
                    }
                }
            }
            this.materialTitleBar1.Text = Properties.Resources.StrMainTitle;
            this.materialTitleBar1.Invalidate();
        }

        private void viewLog(string[] logname)
        {
            string logView = string.Format("{0}\\Resources\\LogView.exe", System.Environment.CurrentDirectory);
            string logFile = "";
            foreach (var log in logname)
            {
                logFile += string.Format("\"{0}\\log\\{1}\" ", System.Environment.CurrentDirectory, log);
            }
            try
            {
                System.Diagnostics.Process.Start("\"" + logView + "\"", logFile);
            }
            catch (Exception e)
            {
                LogHelper.GetLogger<MainForm>().Error(
                    string.Format("打开日志异常，异常消息Message： {0}, StackTrace: {1}", e.Message, e.StackTrace));
            }
        }

        /// <summary>
        /// 系统监控命令0x40
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void systemMonitorTimer_Tick(object sender, EventArgs e)
        {
            if (LARCommandHelper.GetInstance() != null)
            {
                var monitorData = LARCommandHelper.GetInstance().GetMonitorData();
                if (monitorData != null)
                {
                    Program.SysConfig.MotorPortConected = true;
                    if (this.systemMonitorTimer.Interval != 300)
                    {
                        this.systemMonitorTimer.Interval = 300;
                    }
                    //if (!Program.SysConfig.LaserPortConected && this.LaserCheckTimer.Enabled == false)
                    //{
                    //    this.LaserCheckTimer.Enabled = true;
                    //}
                    Coordinate.GetCoordinate().LastPoint = new Point(monitorData.Motor1Steps, monitorData.Motor2Steps);
                    Coordinate.GetCoordinate().MotionComplete = monitorData.Motor1Status == 0x08 && monitorData.Motor2Status == 0x08;
                    if (this.richPictureBox.df != null)
                    {
                        this.richPictureBox.df.UpdateSteps(monitorData.Motor1Steps, monitorData.Motor2Steps);
                        //LogHelper.GetLogger<MainForm>().Error(string.Format("电机1当前步数： {0}， 电机2当前步数： {1}", monitorData.Motor1Steps, monitorData.Motor2Steps));
                        //Entry.Log(string.Format("电机1当前步数： {0}， 电机2当前步数： {1}", monitorData.Motor1Steps, monitorData.Motor2Steps));
                        this.richPictureBox.df.UpdateResponseCode(Coordinate.GetCoordinate().ResponseCode);
                        //this.richPictureBox.df.UpdateLaserStatus();
                    }

                }
                else
                {
                    Program.SysConfig.MotorPortConected = false;
                    //1.检查串口是否存在，不存在则继续检查，调整定时器间隔时间为3s
                    //2.串口存在，则遍历串口，尝试连接
                    //3.连接成功则保存串口到本地，同时修改定时器间隔为300ms
                    //4.所有串口连接失败，则继续循环
                    if (SerialPortHelper.GetHelper().HasPorts)
                    {
                        SerialPortHelper.GetHelper().CheckPort();
                    }
                    else
                    {
                        if (this.systemMonitorTimer.Interval != 1000)
                        {
                            this.systemMonitorTimer.Interval = 1000;
                        }
                    }
                }
            }
        }
        private string portName;
        private SerialPortManager spManager = SerialPortManager.GetInstance();
        private void LaserCheckTimer_Tick(object sender, EventArgs e)
        {
            if (spManager != null)
            {
                if (Program.SysConfig.LaserPortConected)
                {
                    //若激光器连接，则每隔2s发送消息
                    LaserC01Request c01R = new LaserC01Request();
                    byte[] c01Bytes = spManager.Encode(c01R);
                    if (spManager.SerialPort.IsOpen)
                    {
                        byte[] data = spManager.SendData(c01Bytes, false);
                        if (data != null)
                        {
                            //连接状态
                            Program.SysConfig.LaserPortConected = true;
                        }
                        else
                        {
                            //断开连接，需要尝试重连
                            Program.SysConfig.LaserPortConected = false;
                        }
                    }
                    else
                    {
                        Program.SysConfig.LaserPortConected = false;
                    }
                }
                else
                {
                    var ports = SerialPortHelper.GetHelper().GetPorts();
                    if (ports != null && ports.Count() > 0)
                    {
                        foreach (var p in ports)
                        {
                            if (Program.SysConfig.MotorPort != null && Program.SysConfig.MotorPort == p) continue;
                            spManager.SerialPortOpen(p, "9600", "8", "One", "None", "None");
                            LaserC01Request c01R = new LaserC01Request();
                            byte[] c01Bytes = spManager.Encode(c01R);
                            byte[] data = spManager.SendData(c01Bytes);
                            if (data != null)
                            {
                                Program.SysConfig.LaserPort = p;
                                Program.SysConfig.LaserPortConected = true;
                                break;
                            }
                            else
                            {
                                Program.SysConfig.LaserPortConected = false;
                                LogHelper.GetLogger<MainForm>().Error("received data is null");
                            }
                        }
                    }
                }
            }
        }
    }
}
