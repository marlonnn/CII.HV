using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.VFW;
//using AForge.Video.FFMPEG;
//using AForge.Video.VFW;
using CII.Ins.Business.Command.LAR;
using CII.Ins.Business.Entry.LAR;
using CII.LAR.Algorithm;
using CII.LAR.Commond;
using CII.LAR.DrawTools;
using CII.LAR.Laser;
using CII.LAR.Opertion;
using CII.LAR.Protocol;
using CII.LAR.SysClass;
using CII.LAR.SysClass.Shortcuts;
using CII.LAR.UI;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class EntryForm : Form
    {
        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO Dummy);
        private System.Windows.Forms.Timer idleTimer;
        private LASTINPUTINFO lastInputInfo;
        private int totalTime = 0;

        internal HotKeyManager hotKeyManager;
        private SerialPortCommunication serialPortCom;
        //视频翻转类型
        private FlipType flipType;
        public FlipType FlipType
        {
            get { return this.flipType; }
            set
            {
                if (value != this.flipType)
                {
                    this.flipType = value;
                }
            }
        }

        private Bitmap videoFrame;
        private bool canCapture = false;
        private bool captureVideo = false;
        public bool CaptureVideo
        {
            get { return this.captureVideo; }
            set
            {
                if (value != this.captureVideo)
                {
                    this.captureVideo = value;
                    this.richPictureBox.CaptureVideo = value;
                    this.richPictureBox.Invalidate();
                }
            }
        }
        private AVIWriter AVIwriter = new AVIWriter("wmv3");

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

        private SettingCtrl settingCtrl;
        private SettingControl settingControl;
        private SerialPortCtrl serialPortCtrl;
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

        public EntryForm()
        {
            InitializeComponent();
            hotKeyManager = new HotKeyManager(this);
            hotKeyManager.LocalHotKeyPressed += HotKeyManager_LocalHotKeyPressed;
            resources = new ComponentResourceManager(typeof(EntryForm));
            this.WindowState = FormWindowState.Maximized;
            listViewItemArray = new ListViewItemArray();

            this.SizeChanged += EntryForm_SizeChanged;
            this.MouseWheel += EntryForm_MouseWheel;
            this.Load += EntryForm_Load;
            this.FormClosing += EntryForm_FormClosing;
            this.FormClosed += EntryForm_FormClosed;
            this.FlipType = FlipType.Empty;
            DelegateClass.GetDelegate().VideoKeyUpHandler += this.OnKeyUp;
            DelegateClass.GetDelegate().VideoKeyDownHandler += this.OnKeyDown;
            DelegateClass.GetDelegate().ChangeSysFunctionHandler += this.ChangeSysFunctionHandler;
            DelegateClass.GetDelegate().CheckCloseVideoHandler += this.CheckCloseVideoHandler;
            DelegateClass.GetDelegate().CaptureDeviceHandler += this.CaptureDeviceHandler;

            //this.controller = new IController(this);

            serialPortCom = SerialPortCommunication.GetInstance();
            serialPortCom.SerialDataReceivedHandler += SerialDataReceivedHandler;
            InitializeComboBoxLense();
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

        private void InitializeIdleTimer()
        {
            lastInputInfo = new LASTINPUTINFO();
            idleTimer = new System.Windows.Forms.Timer();
            idleTimer.Interval = 1000;
            idleTimer.Enabled = true;
            idleTimer.Tick += IdleTimer_Tick;
        }


        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            totalTime = GetLastInputTime();
            this.toolStrip1.Visible = totalTime < 2000;
        }

        private void HotKeyManager_LocalHotKeyPressed(object sender, LocalHotKeyEventArgs e)
        {
            if (e.HotKey.Name != null)
            {
                switch (e.HotKey.Name)
                {
                    case "takePicture":
                        toolStripButtonCapture_Click(null, null);
                        break;
                    case "zoomIn":
                        toolStripButtonZoomIn_Click(null, null);
                        break;
                    case "zoomOut":
                        toolStripButtonZoomOut_Click(null, null);
                        break;
                    case "startRecord":
                    case "stopRecord":
                        toolStripButtonVideo_Click(null, null);
                        break;
                }

            }
        }

        private void SerialDataReceivedHandler(LaserBaseResponse baseResponse)
        {

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
            StopVideoDevice();
            this.systemMonitorTimer.Enabled = false;

            if (videoDevice == null) return;
            if (videoDevice.IsRunning)
            {
                this.AVIwriter.Close();
            }

            if (serialPortCom != null)
            {
                serialPortCom.SerialDataReceivedHandler -= SerialDataReceivedHandler;
                if (serialPortCom.SerialPort.IsOpen) serialPortCom.Close();
            }
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {
            this.richPictureBox.ToolStripRectangle = this.toolStrip1.Bounds;
            InitializeControls();
            InitializeBaseCtrls();
            Application.Idle += OnIdle;

            fullScreen = new FullScreen(this);
            fullScreen.ShowFullScreen();

            LoadDebugCtrl();

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
            //settingCtrl.SettingCtrl_Load(null, null);
        }

        private void SysConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Program.SysConfig.GetPropertyName(() => Program.SysConfig.UICulture))
            {
                RefrshUI();
                if (BaseCtrls !=null && BaseCtrls.Count > 0)
                {
                    foreach (var bc in BaseCtrls)
                    {
                        bc.RefreshUI();
                    }
                }
            }
        }
        protected ComponentResourceManager resources;
        private void RefrshUI()
        {
            resources.ApplyResources(toolStripButtonCapture, toolStripButtonCapture.Name);
            resources.ApplyResources(toolStripButtonVideo, toolStripButtonVideo.Name);
            resources.ApplyResources(toolStripFiles, toolStripFiles.Name);
            resources.ApplyResources(toolStripButtonZoomOut, toolStripButtonZoomOut.Name);
            resources.ApplyResources(toolStripButtonZoomIn, toolStripButtonZoomIn.Name);

            resources.ApplyResources(toolStripButtonLine, toolStripButtonLine.Name);
            resources.ApplyResources(toolStripButtonRectangle, toolStripButtonRectangle.Name);
            resources.ApplyResources(toolStripButtonElliptical, toolStripButtonElliptical.Name);
            resources.ApplyResources(toolStripButtonLaser, toolStripButtonLaser.Name);
            resources.ApplyResources(toolStripButtonSetting, toolStripButtonSetting.Name);

            resources.ApplyResources(toolStripButtonFit, toolStripButtonFit.Name);
            resources.ApplyResources(toolStripButtonScale, toolStripButtonScale.Name);
            resources.ApplyResources(toolStripButtonMove, toolStripButtonMove.Name);
            resources.ApplyResources(toolStripDropDownCamera, toolStripDropDownCamera.Name);
            resources.ApplyResources(openCameraLiveToolStripMenuItem, openCameraLiveToolStripMenuItem.Name);
            resources.ApplyResources(closeCameraToolStripMenuItem, closeCameraToolStripMenuItem.Name);
            resources.ApplyResources(horizontalFlipToolStripMenuItem, horizontalFlipToolStripMenuItem.Name);
            resources.ApplyResources(verticalFlipToolStripMenuItem, verticalFlipToolStripMenuItem.Name);
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
        }

        private void MoveStepHandler(int x, byte ox, int y, byte oy)
        {
            if (this.richPictureBox.df != null)
            {
                this.richPictureBox.df.UpdateMoveStep(x, ox, y, oy);
            }
        }

        private void InitializeControls()
        {
            CtrlFactory.InitializeCtrlFactory(this.richPictureBox);
            BaseCtrls = new List<BaseCtrl>();

            settingControl = CtrlFactory.GetCtrlFactory().GetCtrlByType<SettingControl>(CtrlType.SettingCtrl);
            BaseCtrls.Add(settingControl);

            //settingCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<SettingCtrl>(CtrlType.SettingCtrl);
            //settingCtrl.UpdateSimulatorImageHandler += UpdateSimulatorImageHandler;
            //settingCtrl.ShowObjectLenseManagerHandler += ShowObjectLenseManagerHandler;
            //settingCtrl.ShowShortcutManagerHandler += ShowShortcutManagerHandler;
            //settingCtrl.ShowScaleAppearanceCtrlHandler += ShowScaleAppearanceCtrlHandler;
            //BaseCtrls.Add(settingCtrl);

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
            lenseCtrl.UpdateObjectLensesHandler += UpdateObjectLensesHandler;
            BaseCtrls.Add(lenseCtrl);

            shortcutCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<ShortcutCtrl>(CtrlType.ShortCut);
            BaseCtrls.Add(shortcutCtrl);
            //laserDebugCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserDebugCtrl>(CtrlType.LaserDebugCtrl);
            //BaseCtrls.Add(laserDebugCtrl);
        }

        private void UpdateObjectLensesHandler(Lense lense)
        {
            //comboBoxLense
            if (Program.SysConfig.Lenses != null && Program.SysConfig.Lenses.Count > 0)
            {
                comboBoxLense.Items.Clear();
                comboBoxLense.Items.AddRange(Program.SysConfig.Lenses.ToArray());
                comboBoxLense.SelectedItem = lense;
            }
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

        private void OpenBtnClickHandler(string btnName)
        {
            //switch (btnName)
            //{
            //    case "motor":
            //        SerialPortHelper.GetHelper().ResetIndex();
            //        autoCheckMotorPort = true;
            //        this.systemMonitorTimer.Enabled = true;
            //        break;
            //    case "laser":
            //        break;
            //    case "close":
            //        SerialPortHelper.GetHelper().ResetIndex();
            //        autoCheckMotorPort = true;
            //        this.systemMonitorTimer.Enabled = true;
            //        break;
            //}
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
                int offsetX = (this.Width - videoSize.Width) / 2;
                int offsetY = (this.Height - videoSize.Height) / 2;
                this.richPictureBox.SetOffset(offsetX, offsetY);
                this.videoControl.Bounds = new Rectangle(0, 0, videoSize.Width, videoSize.Height);
                this.videoControl.VideoSource = videoDevice;
                this.videoControl.VideoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
                this.videoControl.Start();

            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                if (CaptureVideo && canCapture)
                {
                    videoFrame = (Bitmap)eventArgs.Frame.Clone();
                    AVIwriter.Quality = 0;
                    AVIwriter.AddFrame(videoFrame);
                }
                else
                {
                    videoFrame = (Bitmap)eventArgs.Frame.Clone();
                }


                //videoFrame.RotateFlip(RotateFlipType.Rotate180FlipY);
                Image filpImage = FilpImage(videoFrame);
                this.richPictureBox.ImageTracker.Picture = filpImage;
                this.richPictureBox.Picture = filpImage;

            }
            catch (Exception ex)
            {
            }
        }

        private Image FilpImage(Image image)
        {
            switch (FlipType)
            {
                case FlipType.Horizontal:
                    image.RotateFlip(RotateFlipType.Rotate180FlipY);
                    break;
                case FlipType.Vertical:
                    image.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case FlipType.Empty:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;
                default:
                    break;
            }
            return image;
        }

        public Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
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

        private void UpdateSimulatorImageHandler(int selectIndex)
        {
            string fileName = "";
            switch (selectIndex)
            {
                case 0:
                    fileName = string.Format("{0}\\Resources\\Simulator\\Embryo.bmp", System.Environment.CurrentDirectory);
                    break;
                case 1:
                    fileName = string.Format("{0}\\Resources\\Simulator\\Sperm.bmp", System.Environment.CurrentDirectory);
                    break;
                case 2:
                    fileName = string.Format("{0}\\Resources\\Simulator\\Embryo 8 Cell.bmp", System.Environment.CurrentDirectory);
                    break;
                case 3:
                    fileName = string.Format("{0}\\Resources\\Simulator\\egg.bmp", System.Environment.CurrentDirectory);
                    break;
                default:
                    fileName = string.Format("{0}\\Resources\\Simulator\\egg.bmp", System.Environment.CurrentDirectory);
                    break;
            }
            this.richPictureBox.LoadImage(fileName);
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

        //protected override void OnLoad(EventArgs e)
        //{

        //}

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

        private void EntryForm_SizeChanged(object sender, EventArgs e)
        {
            if (tempWindowState != FormWindowState.Maximized && this.WindowState == FormWindowState.Maximized)//点击最大化
            {
                tempWindowState = FormWindowState.Maximized;
                if (fullScreen != null)
                {
                    fullScreen.ShowFullScreen();
                }
                //ChangeVideoCtrlSize();
            }
        }

        private void ChangeVideoCtrlSize()
        {
            //Size videoSize = this.richPictureBox.VideoSize;
            //this.videoControl.Bounds = new Rectangle((this.Width - videoSize.Width) / 2, (this.Height - videoSize.Height) / 2, videoSize.Width, videoSize.Height);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //base.OnMouseWheel(e);
            this.richPictureBox.RichPictureBoxMouseWheel(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && this.richPictureBox.ActiveTool != DrawToolType.Pointer)
            {
                this.richPictureBox.ActiveTool = DrawToolType.Pointer;
            }
            if (e.KeyCode == Keys.Escape)
            {
                fullScreen.ResetFullScreen();
            }
            else if (e.KeyCode == Keys.F)
            {
                fullScreen.ShowFullScreen();
                if (this.richPictureBox != null && this.richPictureBox.Picture != null)
                {
                    this.richPictureBox.ZoomFit();
                }
            }
            else if (e.Control == true && e.KeyCode == Keys.F7)
            {
                viewLog(new string[] { "SerialPort.log"});
            }
            //else if (e.Control == true && e.KeyCode == Keys.D)
            //{
            //    SerialPortDebugForm debugForm = new SerialPortDebugForm();
            //    debugForm.Controller = this.controller;
            //    debugForm.ShowDialog();
            //}
            else if (e.Control == true && e.KeyCode == Keys.A)
            {
                this.richPictureBox.DebugCtrlVisiable();
            }
            else if (e.Control == true && e.KeyCode == Keys.O)
            {
                ShowBaseCtrl(true, CtrlType.LenseCtrl);
            }
            else if (e.Control == true && e.KeyCode == Keys.V)
            {
                VideoPropertyForm form = new VideoPropertyForm();
                form.ShowDialog();
            }
            else if (e.Control == true && e.KeyCode == Keys.P)
            {

                SerialPortDebugForm spd = new SerialPortDebugForm();
                spd.ShowDialog();
            }
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
                    LogHelper.GetLogger<EntryForm>().Error(ee.Message);
                    LogHelper.GetLogger<EntryForm>().Error(ee.StackTrace);
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

        /// <summary>
        /// Delete button click event
        /// delete listviewitem and draw object graphic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
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
            this.toolStripButtonLine.Enabled = Enabled;
            this.toolStripButtonRectangle.Enabled = Enabled;
            this.toolStripButtonElliptical.Enabled = Enabled;
            this.toolStripButtonLaser.Enabled = Enabled;
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

        private void toolStripButtonMove_Click(object sender, EventArgs e)
        {
            SetActiveTool(DrawToolType.Move);
        }

        private void toolStripButtonSetting_Click(object sender, EventArgs e)
        {
            ShowBaseCtrl(true, CtrlType.SettingCtrl);
        }

        private void toolStripButtonPort_Click(object sender, EventArgs e)
        {
            ShowBaseCtrl(true, CtrlType.SerialPort);
        }

        private LaserDebugCtrl laserDebugForm;
        private SerialPortDebugForm spd;
        private void toolStripButtonLaserDebug_Click(object sender, EventArgs e)
        {
            //ShowBaseCtrl(true, CtrlType.LaserDebugCtrl);
            laserDebugForm = new LaserDebugCtrl();
            ////controller = new IController(laserDebugCtrl);
            //laserDebugForm.SetController(this.controller);
            this.LaserCheckTimer.Enabled = false;
            laserDebugForm.ShowDialog();

            //LaserDebugControl ldc = new LaserDebugControl();
            //ldc.ShowDialog();
        }

        private void toolStripButtonLine_Click(object sender, EventArgs e)
        {
            SetActiveTool(DrawToolType.Line);
        }

        private void toolStripButtonRectangle_Click(object sender, EventArgs e)
        {
            SetActiveTool(DrawToolType.Rectangle);
        }

        private void toolStripButtonElliptical_Click(object sender, EventArgs e)
        {
            SetActiveTool(DrawToolType.Ellipse);
        }

        private void toolStripButtonPolygon_Click(object sender, EventArgs e)
        {
            SetActiveTool(DrawToolType.PolyLine);
        }

        private void SetActiveTool(DrawToolType toolType)
        {
            this.richPictureBox.LaserFunction = false;
            this.richPictureBox.ActiveTool = toolType;
            ShowBaseCtrl(true, CtrlType.StatisticsCtrl);
        }

        private void toolStripButtonLaser_Click(object sender, EventArgs e)
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

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            if (this.richPictureBox.CanZoom())
            {
                this.richPictureBox.ZoomIn();
                EnableDrawTools(this.richPictureBox.Zoom == 1.0f);
            }
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            if (this.richPictureBox.CanZoom())
            {
                this.richPictureBox.ZoomOut();
                EnableDrawTools(this.richPictureBox.Zoom == 1.0f);
            }
        }

        private void toolStripButtonFit_Click(object sender, EventArgs e)
        {
            this.richPictureBox.ZoomFit();
            EnableDrawTools(this.richPictureBox.Zoom == 1.0f);
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
                LogHelper.GetLogger<EntryForm>().Error(
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
                    if (this.systemMonitorTimer.Interval != 300)
                    {
                        this.systemMonitorTimer.Interval = 300;
                    }
                    if (!Program.SysConfig.LaserPortConected && this.LaserCheckTimer.Enabled == false)
                    {
                        this.LaserCheckTimer.Enabled = true;
                    }
                    Coordinate.GetCoordinate().LastPoint = new Point(monitorData.Motor1Steps, monitorData.Motor2Steps);
                    Coordinate.GetCoordinate().MotionComplete = monitorData.Motor1Status == 0x08 && monitorData.Motor2Status == 0x08;
                    if (this.richPictureBox.df != null)
                    {
                        this.richPictureBox.df.UpdateSteps(monitorData.Motor1Steps, monitorData.Motor2Steps);
                        LogHelper.GetLogger<EntryForm>().Error(string.Format("电机1当前步数： {0}， 电机2当前步数： {1}", monitorData.Motor1Steps, monitorData.Motor2Steps));
                        //Entry.Log(string.Format("电机1当前步数： {0}， 电机2当前步数： {1}", monitorData.Motor1Steps, monitorData.Motor2Steps));
                        this.richPictureBox.df.UpdateResponseCode(Coordinate.GetCoordinate().ResponseCode);
                        this.richPictureBox.df.UpdateLaserStatus();
                    }

                }
                else
                {
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
                    //1.查看串口是否存在，不存在则弹出一个对话框
                    //2.遍历串口，尝试连接
                    //3.连接成功则保存串口到本地，失败则继续尝试
                    //if (SerialPortHelper.GetHelper().WaringCheckSystem())
                    //{
                    //    //弹对话框，跳转到手动配置界面
                    //    this.systemMonitorTimer.Enabled = false;
                    //    autoCheckMotorPort = false;
                    //    msgShowTime++;
                    //    if (msgShowTime == 1)
                    //    {
                    //        if (MessageBox.Show("自动配置串口失败，请检查本地串口是否正确，点击确定将跳转到手动配置界面", "激光破膜仪", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    //        {
                    //            //手动配置界面
                    //            msgShowTime = 0;
                    //            ShowBaseCtrl(true, CtrlType.SerialPort);
                    //        }
                    //        else
                    //        {
                    //            //退出系统
                    //            fullScreen.ResetFullScreen();
                    //            this.Close();
                    //        }
                    //    }
                    //}
                    //else if (autoCheckMotorPort)
                    //{
                    //    SerialPortHelper.GetHelper().CheckPort();
                    //}
                }
            }
        }

        //private bool autoCheckMotorPort = true;
        //private int msgShowTime = 0;

        private void toolStripButtonCapture_Click(object sender, EventArgs e)
        {
            if (videoFrame != null)
            {
                string fileName = string.Format(@"{0}\{1}.png", Program.SysConfig.StorePath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff"));
                if (CheckStorePath(Program.SysConfig.StorePath))
                {
                    Bitmap bitmap = new Bitmap(this.videoControl.Bounds.Width, this.videoControl.Bounds.Height);
                    this.videoControl.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                    bitmap.Save(fileName, ImageFormat.Png);
                    ShowToastNotification();
                }
            }
        }

        private void ShowToastNotification()
        {
            ToastNotification.Show(this, "Screenshot success",
                global::CII.LAR.Properties.Resources.capture, 1000, eToastGlowColor.Blue,
                eToastPosition.MiddleCenter);
        }

        private bool CheckStorePath(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                File.Create(filePath);
            }
            return true;
        }

        private System.Timers.Timer recordTimer;

        private void toolStripButtonVideo_Click(object sender, EventArgs e)
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
                        AVIwriter.Open(fileName, w, h);
                        this.toolStripButtonVideo.Image = global::CII.LAR.Properties.Resources.Stop;
                        canCapture = true;
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
                    if (videoDevice == null) return;
                    if (videoDevice.IsRunning)
                    {
                        canCapture = false;
                        this.AVIwriter.Close();
                    }
                    recordTimer.Stop();
                    recordTimer.Dispose();
                    this.toolStripButtonVideo.Image = global::CII.LAR.Properties.Resources.video;
                }
            }
            catch (Exception ex)
            {
                canCapture = false;
            }
        }
        
        private void RecordTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.richPictureBox.RecordCount == Program.SysConfig.RecordTime * 60)
            {
                recordTimer.Stop();
                CaptureVideo = false;
                if (videoDevice == null) return;
                if (videoDevice.IsRunning)
                {
                    canCapture = false;
                    this.AVIwriter.Close();
                }
                this.toolStripButtonVideo.Image = global::CII.LAR.Properties.Resources.video;
            }
            this.richPictureBox.RecordCount++;
        }

        private void toolStripFiles_Click(object sender, EventArgs e)
        {
            FilesForm filesForm = new FilesForm();
            filesForm.ShowDialog();
        }

        private void toolStripButtonScale_Click(object sender, EventArgs e)
        {
            this.richPictureBox.Rulers.ShowRulers = !this.richPictureBox.Rulers.ShowRulers;
            this.richPictureBox.Invalidate();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            if (this.richPictureBox.df == null)
            {
                this.richPictureBox.df = new DebugCtrl();
                this.richPictureBox.df.VideoKeyDownHandler += this.OnKeyDown;
                this.richPictureBox.df.Location = new Point(10, 30);
                this.Controls.Add(this.richPictureBox.df);
            }
            else
            {
                this.richPictureBox.df.Visible = true;
            }
        }

        private bool flipHorizontal;
        private bool flipVertical;
        private void horizontalFlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flipHorizontal = !flipHorizontal;
            this.horizontalFlipToolStripMenuItem.Checked = flipHorizontal;
            if (flipHorizontal)
            {
                this.FlipType = FlipType.Horizontal;
                this.verticalFlipToolStripMenuItem.Checked = false;
            }
            else
            {
                this.FlipType = FlipType.Empty;
            }
        }

        private void verticalFlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flipVertical = !flipVertical;
            this.verticalFlipToolStripMenuItem.Checked = flipVertical;
            if (flipVertical)
            {
                this.FlipType = FlipType.Vertical ;
                this.horizontalFlipToolStripMenuItem.Checked = false;
            }
            else
            {
                this.FlipType = FlipType.Empty;
            }

        }

        private void LaserCheckTimer_Tick(object sender, EventArgs e)
        {
            if (serialPortCom != null)
            {
                if (Program.SysConfig.LaserPortConected)
                {
                    Program.SysConfig.LaserPortConected = serialPortCom.SerialPort.IsOpen;
                    ////若激光器连接，则每隔2s发送消息
                    //LaserC01Request c01R = new LaserC01Request();
                    //byte[] c01Bytes = serialPortCom.Encode(c01R);
                    //if (serialPortCom.SerialPort.IsOpen)
                    //{
                    //    serialPortCom.SendData(c01Bytes);
                    //    Thread.Sleep(200);
                    //    if (serialPortCom.FinalData != null)
                    //    {
                    //        //连接状态
                    //        Program.SysConfig.LaserPortConected = true;
                    //    }
                    //    else
                    //    {
                    //        //断开连接，需要尝试重连
                    //        Program.SysConfig.LaserPortConected = false;
                    //    }
                    //}
                    //else
                    //{
                    //    Program.SysConfig.LaserPortConected = false;
                    //}
                }
                else
                {
                    var ports = SerialPortHelper.GetHelper().GetPorts();
                    if (ports != null && ports.Count() > 0)
                    {
                        foreach (var p in ports)
                        {
                            if (Program.SysConfig.MotorPort != null && Program.SysConfig.MotorPort == p) continue;
                            if (serialPortCom.SerialPort.IsOpen) serialPortCom.Close();
                            serialPortCom.SerialPortOpen(p, "9600", "8", "One", "None", "None");
                            LaserC01Request c01R = new LaserC01Request();
                            byte[] c01Bytes = serialPortCom.Encode(c01R);
                            serialPortCom.SendData(c01Bytes);
                            Thread.Sleep(100);
                            if (serialPortCom.FinalData != null)
                            {
                                Program.SysConfig.LaserPort = p;
                                Program.SysConfig.LaserPortConected = true;
                                //this.LaserCheckTimer.Enabled = false;
                                toolStripButtonLaserDebug.Enabled = true;
                                break;
                            }
                            else
                            {
                                Program.SysConfig.LaserPortConected = false;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 初始化物镜
        /// </summary>
        private bool needUpdateComboBoxLense = true;
        private void InitializeComboBoxLense()
        {
            if (Program.SysConfig.Lenses != null && Program.SysConfig.Lenses.Count > 0)
            {
                needUpdateComboBoxLense = false;
                foreach (var lense in Program.SysConfig.Lenses)
                {
                    comboBoxLense.Items.Add(lense);
                    if (Program.SysConfig.Lense != null && lense.Factor == Program.SysConfig.Lense.Factor)
                    {
                        comboBoxLense.SelectedItem = lense;
                    }
                }
                needUpdateComboBoxLense = true;
            }
        }

        /// <summary>
        /// 切换物镜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxLense_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lense lense = comboBoxLense.SelectedItem as Lense;
            if (lense != null)
            {
                Program.SysConfig.Lense = lense;
                this.richPictureBox.Invalidate();
            }
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
