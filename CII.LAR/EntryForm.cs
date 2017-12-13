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
                zoom = value;
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

        public EntryForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
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
                //ctrl.ClickDelegateHandler += new BaseCtrl.ClickDelegate(this.ClickDelegateHandler);
                ctrl.MouseDown += BaseCtrl_MouseDown;
                ctrl.MouseMove += BaseCtrl_MouseMove;
                ctrl.MouseUp += BaseCtrl_MouseUp;
                ctrl.Visible = false;
                ctrl.Enabled = false;
                this.Controls.Add(ctrl);
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
                    //EnableAppearanceButton();
                }
                else
                {
                    this.controls[i].Visible = !show;
                    this.controls[i].Enabled = !show;
                }
            }
        }

        private void EntryForm_MouseWheel(object sender, MouseEventArgs e)
        {
            var value = e.Delta;
            float oldzoom = zoom;
            if (e.Delta > 0)
            {
                zoom += 0.5F;
            }
            else if (e.Delta < 0)
            {
                if (Zoom > 1)
                {
                    zoom = Math.Max(zoom - 0.5F, 0.01F);
                }
            }
            int width = (int)(1392 * zoom);
            int height = (int)(1080 * zoom);
            this.zwPictureBox.Bounds = new Rectangle((1920 - width) / 2, (1080 - height) / 2, width, height);
            this.zwPictureBox.Invalidate();
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
            if (e.KeyCode == Keys.Escape)
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

        private void toolStripButtonSetting_Click(object sender, EventArgs e)
        {
            ShowBaseCtrl(true, 0);
        }

        private void toolStripButtonPort_Click(object sender, EventArgs e)
        {
            ShowBaseCtrl(true, 1);
        }
    }
}
