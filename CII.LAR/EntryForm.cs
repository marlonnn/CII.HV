using CII.LAR.DrawTools;
using CII.LAR.Laser;
using CII.LAR.Opertion;
using CII.LAR.Protocol;
using CII.LAR.SysClass;
using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR
{
    public partial class EntryForm : Form
    {
        public ZWPictureBox PictureBox
        {
            get { return this.zwPictureBox; }
        }
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

        private IDSCamera camera;
        private FullScreen fullScreen;
        private FormWindowState tempWindowState;

        private float zoom = 1;
        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                if (value != this.zoom)
                {
                    this.zoom = value;
                }
                //if zoom == 1,enable draw tools and show graphics
                //else disable draw tools and hide graphics
                EnableDrawTools(this.zoom == 1);
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


        public EntryForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            listViewItemArray = new ListViewItemArray();
            camera = new IDSCamera(this.zwPictureBox);
            camera.CameraSizeControl.AOIChanged += OnDisplayChanged;
            this.SizeChanged += EntryForm_SizeChanged;
            this.MouseWheel += EntryForm_MouseWheel;
            this.Load += EntryForm_Load;
            this.FormClosing += EntryForm_FormClosing;
            InitializeControls();
        }

        private void EntryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LaserProtocolFactory.DestroyDecodeThread();
            LaserProtocolFactory.DestroyEncodeThread();

            MotorProtocolFactory.DestroyDecodeThread();
            MotorProtocolFactory.DestroyEncodeThread();
            this.autoSendTimer.Enabled = false;
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
        }

        private void InitializeControls()
        {
            CtrlFactory.InitializeCtrlFactory(this.zwPictureBox);
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
            BaseCtrls.Add(laserAlignment);
        }

        private void InitializeBaseCtrls()
        {
            foreach (var ctrl in this.BaseCtrls)
            {
                if (ctrl.Name == "LaserAlignment")
                {
                    ctrl.Location = new Point(this.Width - ctrl.Width - 5, this.Height - ctrl.Height - 20);
                }
                else
                {
                    ctrl.Location = new Point(this.Width - ctrl.Width - 5, 30);
                }
                //ctrl.Location = new Point(this.Width - ctrl.Width - 5, 30);
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
                //case "Laser Control":
                //    ShowBaseCtrl(true, this.BaseCtrls[0]);
                //    break;

                    //case "Laser Alignment":
                    //    ShowBaseCtrl(true, this.BaseCtrls[5]);
                    //    break;
                    //case "Laser Hole Size":
                    //    ShowBaseCtrl(true, this.BaseCtrls[6]);
                    //    break;
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
            this.zwPictureBox.LoadImage(fileName);
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
            if (Program.ExpManager.MachineStatus == MachineStatus.Unknown)
            {
                return;
            }
            else if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
            {
                float oldzoom = this.zwPictureBox.Zoom;
                if (e.Delta > 0)
                {
                    this.zwPictureBox.Zoom += 0.5F;
                    this.zwPictureBox.ZoomOnMouseCenter(e, oldzoom);
                }
                else if (e.Delta < 0)
                {
                    if (this.zwPictureBox.Zoom > 1)
                    {
                        this.zwPictureBox.Zoom = Math.Max(this.zwPictureBox.Zoom - 0.5F, 0.01F);
                        this.zwPictureBox.ZoomOnMouseCenter(e, oldzoom);
                    }
                }
                this.zwPictureBox.Invalidate();
            }
            else if (Program.ExpManager.MachineStatus == MachineStatus.LiveVideo)
            {
                float oldzoom = Zoom;
                if (e.Delta > 0)
                {
                    Zoom += 0.5F;
                }
                else if (e.Delta < 0)
                {
                    if (Zoom > 1)
                    {
                        Zoom = Math.Max(Zoom - 0.5F, 0.01F);
                    }
                }
                int width = (int)(1392 * Zoom);
                int height = (int)(1080 * Zoom);
                this.zwPictureBox.Bounds = new Rectangle((1920 - width) / 2, (1080 - height) / 2, width, height);
                //this.zwPictureBox.Invalidate();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            fullScreen = new FullScreen(this);
            fullScreen.ShowFullScreen();
            this.zwPictureBox.EscapeFullScreenHandler += EscapeFullScreenHandler;
            this.zwPictureBox.OnLoad();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            camera.ExitCamera();
        }

        private void EscapeFullScreenHandler()
        {
            fullScreen.ShowFullScreen();
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
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && this.zwPictureBox.ActiveTool != UI.DrawToolType.Pointer)
            {
                this.zwPictureBox.ActiveTool = DrawToolType.Pointer;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                fullScreen.ResetFullScreen();
            }
            else if (e.KeyCode == Keys.F)
            {
                fullScreen.ShowFullScreen();
            }
        }

        private void openCameraLiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CameraChooseForm chooseForm = new CameraChooseForm();
            if (chooseForm.ShowDialog() == DialogResult.OK)
            {
                if (camera.InitCamera(chooseForm.DeviceID | (Int32)uEye.Defines.DeviceEnumeration.UseDeviceID))
                {
                    SetCameraSize();
                    camera.DisplayLive();
                    Program.ExpManager.MachineStatus = MachineStatus.LiveVideo;
                }
            }
        }

        private void SetCameraSize()
        {
            if (camera != null && camera.IsOpened())
            {
                camera.CameraSizeControl.SetAoiBounds(1392, 1080, (this.zwPictureBox.Width - 1392) / 2, 0);
            }
        }

        private void OnDisplayChanged(object sender, EventArgs e)
        {
            // get image size
            System.Drawing.Rectangle rect;
            camera.SetSize(out rect);
            this.zwPictureBox.Bounds = rect;
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
            if (this.zwPictureBox.DrawObject == null || drawObject.Name != this.zwPictureBox.DrawObject.Name)
            {
                try
                {
                    this.zwPictureBox.DrawObject = drawObject;
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
                    LogHelper.GetLogger<ZWPictureBox>().Error(ee.Message);
                    LogHelper.GetLogger<ZWPictureBox>().Error(ee.StackTrace);
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
                this.zwPictureBox.GraphicsList.DeleteDrawObject(drawObject);
                this.zwPictureBox.Invalidate();
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
                if (this.zwPictureBox.GraphicsList != null && this.zwPictureBox.GraphicsList.Count > 0)
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
                        this.SerialController.SendDataToLaserCom(ob.Data);
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
                        this.SerialController.SendDataToMotorCom(ob.Data);
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
            LaserAlignment laserAlignment = controls[5] as LaserAlignment;
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
            this.zwPictureBox.ActiveTool = toolType;
            ShowBaseCtrl(true, 2);
        }

        private void toolStripButtonLaser_Click(object sender, EventArgs e)
        {
            this.PictureBox.LaserFunction = true;
            if (Program.ExpManager.LaserType == LaserType.SaturnFixed)
            {
                this.PictureBox.ActiveTool = DrawToolType.Circle;
            }
            else if (Program.ExpManager.LaserType == LaserType.SaturnActive)
            {
                this.PictureBox.ActiveTool = DrawToolType.MultipleCircle;
            }
            ShowBaseCtrl(true, 5);
            this.PictureBox.GraphicsList.DeleteAll();
            this.PictureBox.Invalidate();
        }
    }
}
