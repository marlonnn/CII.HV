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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR
{
    public partial class EntryForm : Form
    {
        private Bitmap videoFrame;
        private bool canCapture = false;
        private bool captureVideo = false;
        private AVIWriter AVIwriter = new AVIWriter("wmv3");

        private VideoCaptureDevice videoDevice;

        #region 串口相关
        private LaserProtocolFactory laserProtocolFactory;
        public LaserProtocolFactory LaserProtocolFactory
        {
            get { return this.laserProtocolFactory; }
            private set { this.laserProtocolFactory = value; }
        }

        private MotorProtocolFactory motorProtocolFactory;
        public MotorProtocolFactory MotorProtocolFactory
        {
            get { return this.motorProtocolFactory; }
            private set { this.motorProtocolFactory = value; }
        }

        private IController controller;
        public IController SerialController
        {
            get { return this.controller; }
        }
        #endregion

        private FullScreen fullScreen;
        private FormWindowState tempWindowState;

        public float Zoom
        {
            get
            {
                return this.videoControl.Zoom;
            }
            set
            {
                if (value != this.videoControl.Zoom)
                {
                    this.videoControl.Zoom = value;
                }
                //if zoom == 1,enable draw tools and show graphics
                //else disable draw tools and hide graphics
                EnableDrawTools(this.videoControl.Zoom == 1);
            }
        }

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
        private List<BaseCtrl> controls;
        public List<BaseCtrl> BaseCtrls
        {
            get { return this.controls; }
            private set { this.controls = value; }
        }

        private SettingCtrl settingCtrl;
        private SerialPortCtrl serialPortCtrl;
        private StatisticsCtrl statisticsCtrl;
        private LaserAppearanceCtrl laserAppearanceCtrl;
        private RulerAppearanceCtrl rulerAppearanceCtrl;
        private LaserCtrl laserCtrl;
        private LaserAlignment laserAlignment;
        private LaserHoleSize laserHoleSize;
        private VideoChooseCtrl videoChooseCtrl;
        private DebugCtrl df;

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
        }

        public EntryForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            listViewItemArray = new ListViewItemArray();

            this.SizeChanged += EntryForm_SizeChanged;
            this.MouseWheel += EntryForm_MouseWheel;
            this.Load += EntryForm_Load;
            this.FormClosing += EntryForm_FormClosing;
            this.FormClosed += EntryForm_FormClosed;
            this.videoControl.VideoKeyDownHandler += this.OnKeyDown;
            InitializeControls();
        }

        private void EntryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SysConfig.Save(Program.SysConfig, Program.SysConfigOrigin);
        }

        private void EntryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopVideoDevice();
            LaserProtocolFactory.DestroyDecodeThread();
            LaserProtocolFactory.DestroyEncodeThread();

            MotorProtocolFactory.DestroyDecodeThread();
            MotorProtocolFactory.DestroyEncodeThread();
            this.autoSendTimer.Enabled = false;
            this.systemMonitorTimer.Enabled = false;

            if (videoDevice == null) return;
            if (videoDevice.IsRunning)
            {
                this.AVIwriter.Close();
            }
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {
            InitializeBaseCtrls();

            MotorProtocolFactory = MotorProtocolFactory.GetInstance();
            LaserProtocolFactory = LaserProtocolFactory.GetInstance();

            LaserProtocolFactory.StartDecodeThread();
            LaserProtocolFactory.StartEncodeThread();

            MotorProtocolFactory.StartDecodeThread();
            MotorProtocolFactory.StartEncodeThread();

            this.autoSendTimer.Enabled = true;
            this.autoReceiverTimer.Enabled = true;
            this.systemMonitorTimer.Enabled = true;
            this.LaserFactory = LaserFactory.GetInstance(this.videoControl);
            LaserType = LaserType.SaturnFixed;

            Coordinate.GetCoordinate().MoveStepHandler += MoveStepHandler;
            this.videoControl.OffsetX = (this.Width - this.videoControl.VideoSize.Width) / 2;
            this.videoControl.OffsetY = (this.Height - this.videoControl.VideoSize.Height) / 2;
        }

        private void MoveStepHandler(int x, byte ox, int y, byte oy)
        {
            if (df != null)
            {
                df.UpdateMoveStep(x, ox, y, oy);
            }
        }

        private void InitializeControls()
        {
            CtrlFactory.InitializeCtrlFactory(this.videoControl);
            BaseCtrls = new List<BaseCtrl>();

            settingCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<SettingCtrl>(CtrlType.SettingCtrl);
            settingCtrl.UpdateSimulatorImageHandler += UpdateSimulatorImageHandler;
            BaseCtrls.Add(settingCtrl);

            serialPortCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<SerialPortCtrl>(CtrlType.SerialPort);
            controller = new IController(serialPortCtrl);
            BaseCtrls.Add(serialPortCtrl);

            statisticsCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<StatisticsCtrl>(CtrlType.StatisticsCtrl);
            BaseCtrls.Add(statisticsCtrl);

            laserAppearanceCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserAppearanceCtrl>(CtrlType.LaserAppreance);
            BaseCtrls.Add(laserAppearanceCtrl);

            rulerAppearanceCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<RulerAppearanceCtrl>(CtrlType.RulerAppearanceCtrl);
            BaseCtrls.Add(rulerAppearanceCtrl);

            laserCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserCtrl>(CtrlType.LaserCtrl);
            BaseCtrls.Add(laserCtrl);

            laserAlignment = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserAlignment>(CtrlType.LaserAlignment);
            laserAlignment.VideoControl = this.videoControl;
            laserAlignment.VideoKeyDownHandler += this.OnKeyDown;
            BaseCtrls.Add(laserAlignment);

            videoChooseCtrl = CtrlFactory.GetCtrlFactory().GetCtrlByType<VideoChooseCtrl>(CtrlType.VideoChooseCtrl);
            videoChooseCtrl.CaptureDeviceHandler += CaptureDeviceHandler;

            BaseCtrls.Add(videoChooseCtrl);

            laserHoleSize = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserHoleSize>(CtrlType.LaserHoleSize);
            BaseCtrls.Add(laserHoleSize);
        }

        private void CaptureDeviceHandler(string deviceMoniker)
        {
            videoDevice = new VideoCaptureDevice(deviceMoniker);
            if (videoDevice != null)
            {
                Size videoSize = this.videoControl.VideoSize;
                this.videoControl.Bounds = new Rectangle((this.Width - videoSize.Width) / 2, (this.Height - videoSize.Height) / 2, videoSize.Width, videoSize.Height);
                this.videoControl.VideoSource = videoDevice;
                this.videoControl.VideoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
                this.videoControl.Start();

            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (captureVideo && canCapture)
            {
                videoFrame = (Bitmap)eventArgs.Frame.Clone();
                AVIwriter.Quality = 0;
                AVIwriter.AddFrame(videoFrame);
            }
            else
            {
                videoFrame = (Bitmap)eventArgs.Frame.Clone();
            }
        }

        private void StopVideoDevice()
        {
            if (this.videoControl.VideoSource != null)
            {
                this.videoControl.SignalToStop();
                this.videoControl.WaitForStop();
                this.videoControl.VideoSource = null;
            }
        }

        private void InitializeBaseCtrls()
        {
            foreach (var ctrl in this.BaseCtrls)
            {
                ctrl.InitializeLocation(this.Size);
                ctrl.ClickDelegateHandler += new BaseCtrl.ClickDelegate(this.ClickDelegateHandler);
                ctrl.MouseDown += BaseCtrl_MouseDown;
                ctrl.MouseMove += BaseCtrl_MouseMove;
                ctrl.MouseUp += BaseCtrl_MouseUp;
                ctrl.Visible = false;
                ctrl.Enabled = false;
                this.Controls.Add(ctrl);
            }
        }

        /// <summary>
        /// switch to different base control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="name"></param>
        private void ClickDelegateHandler(object sender, string name)
        {
            switch (name)
            {
                case "Setting control":
                    ShowBaseCtrl(true, this.BaseCtrls[0]);
                    break;
                case "Serial Port Config":
                    ShowBaseCtrl(true, this.BaseCtrls[1]);
                    break;
                case "Statistics control":
                    ShowBaseCtrl(true, this.BaseCtrls[2]);
                    break;
                case "Laser Appearance":
                    ShowBaseCtrl(true, this.BaseCtrls[3]);
                    break;
                case "Ruler Appearance":
                    ShowBaseCtrl(true, this.BaseCtrls[4]);
                    break;
                case "Laser Control":
                    ShowBaseCtrl(true, this.BaseCtrls[5]);
                    break;

                case "Laser Alignment":
                    ShowBaseCtrl(true, this.BaseCtrls[6]);
                    break;

                //case "":
                //    ShowBaseCtrl(true, this.BaseCtrls[7]);
                    //break;
                case "Laser Hole Size":
                    ShowBaseCtrl(true, this.BaseCtrls[8]);
                    break;
            }
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
            this.videoControl.LoadImage(fileName);
        }

        /// <summary>
        /// show laser control
        /// </summary>
        /// <param name="show"></param>
        public void ShowBaseCtrl(bool show, int index)
        {
            for (int i = 0; i < this.controls.Count; i++)
            {
                if (this.controls[i].ShowIndex == index)
                {
                    this.baseCtrl = controls[index];
                    this.Controls.SetChildIndex(this.baseCtrl, 0);
                    this.baseCtrl.Visible = show;
                    this.baseCtrl.Enabled = show;
                    EnableAppearanceButton();
                }
                else
                {
                    this.controls[i].Visible = !show;
                    this.controls[i].Enabled = !show;
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
            this.baseCtrl.Visible = false;
            this.baseCtrl.Enabled = false;

            this.baseCtrl = baseCtrl;
            this.Controls.SetChildIndex(this.baseCtrl, 0);

            this.baseCtrl.Visible = show;
            this.baseCtrl.Enabled = show;
        }

        private void EntryForm_MouseWheel(object sender, MouseEventArgs e)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            fullScreen = new FullScreen(this);
            fullScreen.ShowFullScreen();

            LoadDebugCtrl();
        }

        private void LoadDebugCtrl()
        {
            df = new DebugCtrl();
            df.VideoKeyDownHandler += this.OnKeyDown;
            df.Location = new Point(10, 30);
            this.Controls.Add(df);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
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
                ChangeVideoCtrlSize();
            }
        }

        private void ChangeVideoCtrlSize()
        {
            Size videoSize = this.videoControl.VideoSize;
            this.videoControl.Bounds = new Rectangle((this.Width - videoSize.Width) / 2, (this.Height - videoSize.Height) / 2, videoSize.Width, videoSize.Height);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && this.videoControl.ActiveTool != UI.DrawToolType.Pointer)
            {
                this.videoControl.ActiveTool = DrawToolType.Pointer;
            }
            if (e.KeyCode == Keys.Escape)
            {
                fullScreen.ResetFullScreen();
            }
            else if (e.KeyCode == Keys.F)
            {
                fullScreen.ShowFullScreen();
            }
            else if (e.Control == true && e.KeyCode == Keys.F7)
            {
                viewLog(new string[] { "SerialPort.log"});
            }
            else if (e.Control == true && e.KeyCode == Keys.D)
            {
                SerialPortDebugForm debugForm = new SerialPortDebugForm();
                debugForm.Controller = this.controller;
                debugForm.ShowDialog();
            }
        }

        private void openCameraLiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowBaseCtrl(true, 7);
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
            if (this.videoControl.DrawObject == null || drawObject.Name != this.videoControl.DrawObject.Name)
            {
                try
                {
                    this.videoControl.DrawObject = drawObject;
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
                this.videoControl.GraphicsList.DeleteDrawObject(drawObject);
                this.videoControl.Invalidate();
                EnableAppearanceButton();
            }
        }

        /// <summary>
        /// enable or disable ruler appearance button
        /// </summary>
        private void EnableAppearanceButton()
        {
            var baseCtrl = this.controls[2] as StatisticsCtrl;
            if (baseCtrl != null)
            {
                if (this.videoControl.GraphicsList != null && this.videoControl.GraphicsList.Count > 0)
                {
                    baseCtrl.BtnAppearance.Enabled = true;
                }
                else
                {
                    baseCtrl.BtnAppearance.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 通过串口发送给激光器或者电机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoSendTimer_Tick(object sender, EventArgs e)
        {
            if (LaserProtocolFactory.GetInstance().TxQueue != null)
            {
                var laserOriginalBytes = LaserProtocolFactory.GetInstance().TxQueue.PopAll();
                if (laserOriginalBytes != null  && laserOriginalBytes.Count > 0)
                {
                    foreach (OriginalBytes ob in laserOriginalBytes)
                    {
                        if (this.SerialController != null)
                        {
                            this.SerialController.SendDataToLaserCom(ob.Data);
                        }
                    }
                }
            }
            if (MotorProtocolFactory.GetInstance().TxQueue != null)
            {
                var motorOriginalBytes = MotorProtocolFactory.GetInstance().TxQueue.PopAll();
                if (motorOriginalBytes != null && motorOriginalBytes.Count > 0)
                {
                    foreach (OriginalBytes ob in motorOriginalBytes)
                    {
                        if (this.SerialController != null)
                        {
                            this.SerialController.SendDataToMotorCom(ob.Data);
                        }
                    }
                }
            }
        }

        private void EnableDrawTools(bool Enabled)
        {
            this.toolStripButtonLine.Enabled = Enabled;
            this.toolStripButtonRectangle.Enabled = Enabled;
            this.toolStripButtonPolygon.Enabled = Enabled;
            this.toolStripButtonElliptical.Enabled = Enabled;
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
            LaserAlignment laserAlignment = controls[6] as LaserAlignment;
            if (laserAlignment != null)
            {
                laserAlignment.ButtonNext(isEnable);
            }
        }

        public void HolesInfoChangeHandler(HolesInfo holesInfo)
        {
            if (holesInfo != null)
            {
                LaserCtrl laserCtrl = controls[5] as LaserCtrl;
                if (laserCtrl != null)
                {
                    laserCtrl.UpdateHolesInfo(holesInfo);
                }
            }
        }

        public void HolesNumberSlider(bool isShow)
        {
            if (controls != null && controls.Count > 0)
            {
                LaserCtrl laserCtrl = controls[5] as LaserCtrl;
                if (laserCtrl != null)
                {
                    laserCtrl.HolesNumberSlider(isShow);
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
            ShowBaseCtrl(true, 0);
        }

        private void toolStripButtonPort_Click(object sender, EventArgs e)
        {
            ShowBaseCtrl(true, 1);
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
            this.videoControl.LaserFunction = false;
            this.videoControl.ActiveTool = toolType;
            ShowBaseCtrl(true, 2);
        }

        private void toolStripButtonLaser_Click(object sender, EventArgs e)
        {
            this.videoControl.LaserFunction = true;
            if (LaserType == LaserType.SaturnFixed)
            {
                this.videoControl.ActiveTool = DrawToolType.Circle;
            }
            else if (LaserType == LaserType.SaturnActive)
            {
                this.videoControl.ActiveTool = DrawToolType.MultipleCircle;
            }
            ShowBaseCtrl(true, 5);
            this.videoControl.GraphicsList.DeleteAll();
            this.videoControl.Invalidate();
            SetLaserByType(LaserType);
        }

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            Zoom += 0.1f;
            Size videoSize = this.videoControl.VideoSize;
            Size size = Size.Ceiling(new SizeF(videoSize.Width * Zoom, videoSize.Height * Zoom));
            this.videoControl.Bounds = new Rectangle((this.Width - size.Width) / 2 - 50, (this.Height - size.Height) / 2 -50, size.Width, size.Height);
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            Zoom -= 0.1f;
            Size videoSize = this.videoControl.VideoSize;
            Size size = Size.Ceiling(new SizeF(videoSize.Width * Zoom, videoSize.Height * Zoom));
            this.videoControl.Bounds = new Rectangle((this.Width - size.Width) / 2, (this.Height - size.Height) / 2, size.Width, size.Height);
        }

        private void toolStripButtonFit_Click(object sender, EventArgs e)
        {
            this.videoControl.ZoomFit();
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
                    Coordinate.GetCoordinate().LastPoint = new Point(monitorData.Motor1Steps, monitorData.Motor2Steps);
                    Coordinate.GetCoordinate().MotionComplete = monitorData.Motor1Status == 0x08 && monitorData.Motor2Status == 0x08;
                    if (df != null)
                    {
                        df.UpdateSteps(monitorData.Motor1Steps, monitorData.Motor2Steps);
                        LogHelper.GetLogger<EntryForm>().Error(string.Format("电机1当前步数： {0}， 电机2当前步数： {1}", monitorData.Motor1Steps, monitorData.Motor2Steps));
                        //Entry.Log(string.Format("电机1当前步数： {0}， 电机2当前步数： {1}", monitorData.Motor1Steps, monitorData.Motor2Steps));
                        df.UpdateResponseCode(Coordinate.GetCoordinate().ResponseCode);
                    }

                }
            }
        }

        private void autoReceiverTimer_Tick(object sender, EventArgs e)
        {
            if (LaserProtocolFactory.GetInstance().RxMsgQueue != null)
            {
                List<LaserBaseResponse> baseResponses = LaserProtocolFactory.GetInstance().RxMsgQueue.PopAll();
                if (baseResponses != null && baseResponses.Count > 0)
                {

                }
            }
        }

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

        private void toolStripButtonVideo_Click(object sender, EventArgs e)
        {
            try
            {
                captureVideo = !captureVideo;
                if (captureVideo)
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
                    this.toolStripButtonVideo.Image = global::CII.LAR.Properties.Resources.video;
                }
            }
            catch (Exception ex)
            {
                canCapture = false;
            }
        }

        private void toolStripFiles_Click(object sender, EventArgs e)
        {
            FilesForm filesForm = new FilesForm();
            filesForm.ShowDialog();
        }

        private void toolStripButtonScale_Click(object sender, EventArgs e)
        {
            this.videoControl.Rulers.ShowRulers = !this.videoControl.Rulers.ShowRulers;
            this.videoControl.Invalidate();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            if (df == null)
            {
                df = new DebugCtrl();
                df.VideoKeyDownHandler += this.OnKeyDown;
                df.Location = new Point(10, 30);
                this.Controls.Add(df);
            }
            else
            {
                df.Visible = true;
            }
        }
    }
}
