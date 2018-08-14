using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.DrawTools;
using System.IO;
using System.Resources;
using CII.LAR.SysClass;
using CII.LAR.Algorithm;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace CII.LAR.UI
{
    public partial class RichPictureBox : PictureBox
    {
        private static Cursor s_cursor = new Cursor(new MemoryStream((byte[])new ResourceManager(typeof(MainForm)).GetObject("Cross")));

        /// <summary>
        /// 数码放大倍数=物镜倍数*{25.4*屏幕尺寸（英寸）/CCD对角线的长度}*适配器的放大倍数，
        /// 如果系统放大倍数，还需要乘以系统放大倍数
        /// 　1）物镜倍数指的是您现在使用的显微镜的物镜镜头的倍数，如20倍；
        /// 　2）适配器的放大倍数：指的显微镜与成像设备连接部分的放大倍数，通常为1倍，也有0.35、0.5、0.63倍的； 
        /// 　3）25.4*屏幕尺寸（英寸）：这里是把屏幕尺寸换算成毫米计算，1英寸=25.4mm； 
        /// 　4）CCD对角线的长度：指的是CCD的芯片尺寸，常有的是1/4英寸、1/3英寸、1/2英寸、2/3英寸的，相对应的长度分别为4mm；6mm；8mm；11mm，这个是行业内统一规范的
        /// </summary>
        public double DigitalMagnification
        {
            //1600 -> image size is 1280*960
            get { return (Program.SysConfig.Lense.Factor * this.PixelToMillimeter(1600) / Program.SysConfig.CCD.Length) * this.Zoom; }
        }

        private RestrictArea restrictArea;
        public RestrictArea RestrictArea
        {
            get { return this.restrictArea; }
        }
        private int recordCount;
        public int RecordCount
        {
            get { return this.recordCount; }
            set { this.recordCount = value; }
        }
        private bool captureVideo = false;
        public bool CaptureVideo
        {
            get { return this.captureVideo; }
            set
            {
                if (value != this.captureVideo)
                {
                    this.captureVideo = value;
                }
            }
        }
        private bool mousePressed;
        private Point lastMousePos;//记录鼠标指针的坐标
        private Point mousePos = Point.Empty;
        private object lockObject = new object();
        /// <summary>
        /// 当前帧图和缩略图帧图
        /// </summary>
        private Image picture;
        public Image Picture
        {
            get
            {
                lock (lockObject)
                    return this.picture;
            }
            set
            {
                lock (lockObject)
                {
                    if (this.picture != null) this.picture.Dispose();
                    if (value != this.picture)
                    {
                        this.picture = value;
                        this.Image = value;
                        //this.imageTracker.Picture = value;
                    }
                }
            }
        }

        //小窗口
        private ImageTracker imageTracker;
        public ImageTracker ImageTracker { get { return this.imageTracker; } }
        private bool isDraggingPictureTracker = false;
        private Rectangle draggingRectangle;
        /// <summary>
        /// delegate of PictureBox painted event handler
        /// </summary>
        /// <param name="visibleAreaRect">currently visible area of picture</param>
        /// <param name="pictureBoxRect">picture box area</param>
        public delegate void PictureBoxPaintedEventHandler(Rectangle visibleAreaRect, Rectangle pictureBoxRect);

        /// <summary>
        /// PictureBox painted event
        /// </summary>
        public event PictureBoxPaintedEventHandler PictureBoxPaintedEvent;


        #region 偏移量和缩放系数
        private Point mouseDownPoint;
        private int startX;
        private int startY;

        private int offsetX;
        public int OffsetX
        {
            get
            {
                return offsetX;
            }
            set
            {
                offsetX = value;
            }
        }
        private int offsetY;
        public int OffsetY
        {
            get
            {
                return offsetY;
            }
            set
            {
                offsetY = value;
            }
        }
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
        #endregion

        private SmartRuler rulers;
        public SmartRuler Rulers
        {
            get
            {
                return rulers;
            }
        }

        /// <summary>
        /// the new area where the Statistics control to be dragged
        /// </summary>
        private Rectangle draggingBaseCtrlRectangle;

        #region Measure tool
        public event OnMeasureUnitChangedEventHandler OnMeasureUnitChanged;
        public delegate void OnMeasureUnitChangedEventHandler(enUniMis unit);
        public const enUniMis DefaultUnitOfMeasure = enUniMis.um;
        private enUniMis myUnitOfMeasure = DefaultUnitOfMeasure;

        public enUniMis UnitOfMeasure
        {
            get
            {
                return myUnitOfMeasure;
            }
            set
            {
                if (value != myUnitOfMeasure)
                {
                    myUnitOfMeasure = value;
                    OnMeasureUnitChanged?.Invoke(value);
                }
            }
        }
        #endregion

        #region Draw tool
        private GraphicsList drawObjects;
        [BrowsableAttribute(false)]
        [System.ComponentModel.Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphicsList GraphicsList
        {
            get
            {
                return drawObjects;
            }

            set
            {
                drawObjects = value;
            }
        }

        public bool CreatingDrawObject
        {
            get
            {
                return ActiveTool != DrawToolType.None && ActiveTool != DrawToolType.Pointer &&
                      GraphicsList.Count > 0 && GraphicsList[0].Creating;
            }
        }

        private Tool[] tools;                 // array of tools
        [BrowsableAttribute(false)]
        [System.ComponentModel.Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Tool[] Tools
        {
            get
            {
                return tools;
            }
            set
            {
                this.tools = value;
            }
        }

        private DrawToolType activeTool;      // active drawing tool
        [BrowsableAttribute(false)]
        [System.ComponentModel.Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DrawToolType ActiveTool
        {
            get
            {
                return activeTool;
            }
            set
            {
                if (activeTool == DrawToolType.PolyLine && activeTool != value)
                {
                    if (GraphicsList.Count > 0 && GraphicsList[0] is DrawPolyLine)
                    {
                        DrawPolyLine polygon = (DrawPolyLine)GraphicsList[0];
                        if (polygon != null && polygon.Creating)
                        {
                            tools[(int)DrawToolType.PolyLine].OnCancel(this, true);
                        }
                    }
                }
                activeTool = value;
                if (tools != null && activeTool != DrawToolType.None)
                {
                    Cursor = tools[(int)activeTool] is ToolPointer ? Cursors.Default : (tools[(int)activeTool] as ToolObject).Cursor;
                }
                Enabled = activeTool != DrawToolType.None;
                SetSystemFunction(value);
            }
        }

        private void SetSystemFunction(DrawToolType tool)
        {
            switch (tool)
            {
                case DrawToolType.Line:
                case DrawToolType.Ellipse:
                case DrawToolType.Rectangle:
                case DrawToolType.Polygon:
                case DrawToolType.PolyLine:
                    Program.SysConfig.Function = SystemFunction.Measure;
                    this.Invalidate();
                    break;
                case DrawToolType.Circle:
                case DrawToolType.MultipleCircle:
                    Program.SysConfig.Function = SystemFunction.Laser;
                    break;
                //case DrawToolType.Pointer:
                //    Program.SysConfig.Function = SystemFunction.Empty;
                //    break;
                //default:
                //    Program.SysConfig.Function = SystemFunction.Empty;
                //    break;
            }
        }
        #endregion

        private DrawObject drawObject;
        public DrawObject DrawObject
        {
            get { return this.drawObject; }
            set { this.drawObject = value; }
        }

        private bool laserFunction;
        public bool LaserFunction
        {
            get { return laserFunction; }
            set
            {
                if (value != laserFunction)
                {
                    laserFunction = value;
                    if (value)
                    {
                        this.Cursor = s_cursor;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private Size videoSize;
        public Size VideoSize
        {
            get { return this.videoSize; }
            set { this.videoSize = value; }
        }

        public Size RealSize
        {
            get
            {
                if (this.Image != null)
                {
                    return Program.SysConfig.LiveMode ? this.VideoSize : this.Image.Size;
                }
                else
                {
                    return this.VideoSize;
                }
            } 
        }

        public RichPictureBox()
        {
            VideoSize = new Size(1280, 960);
            this.DoubleBuffered = true;
            this.GraphicsList = new GraphicsList();
            this.rulers = new SmartRuler(this);
            InitializeTools();
            InitializeImageTracker();
            this.PictureBoxPaintedEvent += imageTracker.OnPicturePainted;
            this.restrictArea = new RestrictArea(this);
            this.BackColor = Color.FromArgb(0x28, 0x2C, 0x35);
        }

        public DebugCtrl df;
        public void LoadDebugCtrl()
        {
            df = CtrlFactory.GetCtrlFactory().GetCtrlByType<DebugCtrl>(CtrlType.DebugCtrl);
            df.Location = new Point(48, this.Height - df.Height);
            this.Controls.Add(df);
        }

        public void DebugCtrlVisiable()
        {
            this.df.Visible = !this.df.Visible;
        }

        public void SetOffset(int offsetX, int offsetY)
        {
            this.OffsetX = offsetX;
            this.OffsetY = offsetY;
        }

        private void InitializeTools()
        {
            ActiveTool = DrawToolType.Pointer;
            Tools = new Tool[(int)DrawToolType.NumberOfDrawTools];
            Tools[(int)DrawToolType.None] = new Tool();
            Tools[(int)DrawToolType.Pointer] = new ToolPointer();
            Tools[(int)DrawToolType.Line] = new ToolLine();
            Tools[(int)DrawToolType.Move] = new ToolMove();
            Tools[(int)DrawToolType.Ellipse] = new ToolEllipse();
            Tools[(int)DrawToolType.Rectangle] = new ToolRectangle();
            Tools[(int)DrawToolType.PolyLine] = new ToolPolyLine();
            Tools[(int)DrawToolType.Circle] = new ToolCircle();
            Tools[(int)DrawToolType.MultipleCircle] = new ToolMultipleCircle();
            Tools[(int)DrawToolType.Move] = new ToolMove();
        }

        private void InitializeImageTracker()
        {
            this.imageTracker = new ImageTracker(this);
            this.imageTracker.Location = new System.Drawing.Point(48, 30);
            this.imageTracker.Size = new System.Drawing.Size(137, 152);
            this.imageTracker.TabIndex = 1;
            //this.imageTracker.ScalePercent = 0;
            this.imageTracker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageTracker_MouseDown);
            this.imageTracker.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageTracker_MouseMove);
            this.imageTracker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageTracker_MouseUp);
            this.imageTracker.Visible = false;
            this.Controls.Add(this.imageTracker);
        }


        #region 小窗口鼠标事件相关
        private void imageTracker_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingPictureTracker)
            {
                // caculating next candidate dragging rectangle
                Point newPos = new Point(draggingRectangle.Location.X + e.X - lastMousePos.X,
                                         draggingRectangle.Location.Y + e.Y - lastMousePos.Y);
                Rectangle newPictureTrackerArea = draggingRectangle;
                newPictureTrackerArea.Location = newPos;

                // saving current mouse position to be used for next dragging
                lastMousePos = new Point(e.X, e.Y);

                // dragging picture tracker only when the candidate dragging rectangle
                // is within this ScalablePictureBox control
                if (ClientRectangle.Contains(newPictureTrackerArea))
                {
                    // removing previous rubber-band frame
                    DrawReversibleRect(draggingRectangle);

                    // updating dragging rectangle
                    draggingRectangle = newPictureTrackerArea;

                    // drawing new rubber-band frame
                    DrawReversibleRect(draggingRectangle);
                }
            }
        }

        private void imageTracker_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDraggingPictureTracker)
            {
                isDraggingPictureTracker = false;

                // erase dragging rectangle
                DrawReversibleRect(draggingRectangle);

                // move the picture tracker control to the new position
                imageTracker.Location = draggingRectangle.Location;
            }
        }

        private void imageTracker_MouseDown(object sender, MouseEventArgs e)
        {
            isDraggingPictureTracker = true;    // Make a note that we are dragging picture tracker control

            // Store the last mouse poit for this rubber-band rectangle.
            lastMousePos.X = e.X;
            lastMousePos.Y = e.Y;

            // draw initial dragging rectangle
            draggingRectangle = imageTracker.Bounds;
            DrawReversibleRect(draggingRectangle);
        } 
        #endregion

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
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (DelegateClass.GetDelegate().VideoKeyDownHandler != null)
            {
                DelegateClass.GetDelegate().VideoKeyDownHandler(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (DelegateClass.GetDelegate().VideoKeyUpHandler != null)
            {
                DelegateClass.GetDelegate().VideoKeyUpHandler(e);
            }
        }

        private Rectangle toolStripRectangle;
        public Rectangle ToolStripRectangle
        {
            get { return this.toolStripRectangle; }
            set { this.toolStripRectangle = value; }
        }

        #region 鼠标事件
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!IsInLegalRegion(e.Location)) return;
            if (e.Button == MouseButtons.Left)
            {
                if (!mousePressed)
                {
                    if (LaserFunction)
                    {
                        if (Program.EntryForm.Laser != null)
                        {
                            Program.EntryForm.Laser.OnMouseDown(this, e);
                        }
                    }
                    else
                    {
                        if (activeTool == DrawToolType.Move)
                        {
                            mousePressed = true;
                            mouseDownPoint = e.Location;
                            //lastBaseCtrlMousePos.X = e.X;
                            //lastBaseCtrlMousePos.Y = e.Y;
                            startX = OffsetX;
                            startY = OffsetY;
                            // draw initial dragging rectangle
                            draggingBaseCtrlRectangle = this.Bounds;
                        }
                        else
                        {
                            Tools[(int)ActiveTool].OnMouseDown(this, e);
                        }
                    }
                }
            }
        }

        private bool IsInLegalRegion(Point point)
        {
            if (this.Zoom == 1)
            {
                return (new Rectangle(this.OffsetX, this.OffsetY, this.RealSize.Width, this.RealSize.Height)).Contains(point);
            }
            else
            {
                return true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!IsInLegalRegion(e.Location)) return;
            if (LaserFunction)
            {
                if (Program.EntryForm.Laser != null)
                {
                    Program.EntryForm.Laser.OnMouseMove(this, e);
                }
            }
            else
            {
                if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.None) && ActiveTool != DrawToolType.Move)
                {

                    tools[(int)activeTool].OnMouseMove(this, e);
                }
                else if (e.Button == MouseButtons.Left && ActiveTool == DrawToolType.Move)
                {
                    Point mousePosNow = e.Location;

                    int deltaX = mousePosNow.X - mouseDownPoint.X;
                    int deltaY = mousePosNow.Y - mouseDownPoint.Y;

                    OffsetX = (int)(startX + deltaX / zoom);
                    OffsetY = (int)(startY + deltaY / zoom);

                    //if (mousePressed)
                    //{
                    //    Point newPos = new Point(
                    //        draggingBaseCtrlRectangle.Location.X + e.X - lastMousePos.X,
                    //        draggingBaseCtrlRectangle.Location.Y + e.Y - lastMousePos.Y);
                    //    Rectangle newPictureTrackerArea = draggingBaseCtrlRectangle;
                    //    newPictureTrackerArea.Location = newPos;

                    //    this.lastMousePos = e.Location;
                    //    // dragging Statistics ctrl only when the candidate dragging rectangle
                    //    // is within this ScalablePictureBox control
                    //    if (this.ClientRectangle.Contains(newPictureTrackerArea))
                    //    {
                    //        // updating dragging rectangle
                    //        draggingBaseCtrlRectangle = newPictureTrackerArea;
                    //    }
                    //}
                    this.Invalidate();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!IsInLegalRegion(e.Location)) return;
            if (activeTool != DrawToolType.Move)
            {
                if (LaserFunction)
                {
                    if (Program.EntryForm.Laser != null)
                    {
                        Program.EntryForm.Laser.OnMouseUp(this, e);
                    }
                }
                else
                {
                    if (activeTool != DrawToolType.Move)
                    {
                        Tools[(int)activeTool].OnMouseUp(this, e);
                    }
                }
            }
            else
            {
                if (mousePressed)
                {
                    mousePressed = false;
                    // move the Statistics control to the new position
                    this.Location = draggingBaseCtrlRectangle.Location;
                }
            }
        }

        public void RichPictureBoxMouseWheel(MouseEventArgs e)
        {
            OnMouseWheel(e);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //if (this.Picture == null) return;
            float oldzoom = zoom;

            if (e.Delta > 0)
            {
                if (IsCtrlKeyPressed)
                {
                    PanAllDrirection(oldzoom, PanDirection.Left);
                }
                else if (IsShiftKeyPressed)
                {
                    PanAllDrirection(oldzoom, PanDirection.Up);
                }
                else
                {
                    //最大放大16倍
                    if (zoom + 1 == 17) return;
                    zoom += 1;
                    if (zoom < 3)
                    {
                        Point mousePos = new Point(this.Width / 2, this.Height / 2);
                        MouseEventArgs args = new MouseEventArgs(new MouseButtons(), 1, mousePos.X, mousePos.Y, 0);
                        ZoomOnMouseCenter(args, oldzoom);
                    }
                    else
                    {
                        ZoomOnMouseCenter(e, oldzoom);
                    }
                }
            }
            else if (e.Delta < 0)
            {
                if (IsCtrlKeyPressed)
                {
                    PanAllDrirection(oldzoom, PanDirection.Right);
                }
                else if (IsShiftKeyPressed)
                {
                    PanAllDrirection(oldzoom, PanDirection.Down);
                }
                else
                {
                    if (zoom - 1 < 1) return;
                    if (zoom == 2)
                    {
                        zoom -= 1;
                        ZoomFit();
                    }
                    else if (zoom > 1)
                    {
                        zoom -= 1;
                        if (zoom < 3)
                        {
                            Point mousePos = new Point(this.Width / 2, this.Height / 2);
                            MouseEventArgs args = new MouseEventArgs(new MouseButtons(), 1, mousePos.X, mousePos.Y, 0);
                            ZoomOnMouseCenter(args, oldzoom);
                        }
                        else
                        {
                            ZoomOnMouseCenter(e, oldzoom);
                        }
                    }
                }
            }
            this.imageTracker.ScalePercent = zoom * 100;
            this.Refresh();

        }


        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            tools[(int)ActiveTool].OnDoubleClick(this, e);
        }
        #endregion

        private void PanAllDrirection(float zoom, PanDirection panDirection)
        {
            switch (panDirection)
            {
                case PanDirection.Left:
                    this.OffsetX += (int)(this.Width * 0.1f / zoom);
                    break;
                case PanDirection.Up:
                    this.OffsetY += (int)(this.Height * 0.1f / zoom);
                    break;
                case PanDirection.Right:
                    this.OffsetX -= (int)(this.Width * 0.1f / zoom);
                    break;
                case PanDirection.Down:
                    this.OffsetY -= (int)(this.Height * 0.1f / zoom);
                    break;
                default:
                    break;
            }
        }

        private void ZoomOnMouseCenter(MouseEventArgs e, float oldzoom)
        {
            mousePos = e.Location;
            Point mousePosNow = e.Location;

            // Where location of the mouse in the pictureframe
            int x = mousePosNow.X - this.Location.X;
            int y = mousePosNow.Y - this.Location.Y;

            // Where in the IMAGE is it now
            int oldImageX = (int)(x / oldzoom);
            int oldImageY = (int)(y / oldzoom);

            // Where in the IMAGE will it be when the new zoom i made
            int newImageX = (int)(x / zoom);
            int newImageY = (int)(y / zoom);

            // Where to move image to keep focus on one point
            offsetX = newImageX - oldImageX + offsetX;
            offsetY = newImageY - oldImageY + offsetY;
        }
        public void GraphicsPropertiesChangedHandler(DrawObject drawObject, GraphicsProperties graphicsProperties)
        {
            this.Invalidate();
        }

        public void ZoomHandler(MouseEventArgs e, bool zoomIn)
        {
            if (zoomIn)
            {
                for (int i = 0; i < Program.SysConfig.DefaultScaleCoefficient - 1; i++)
                {
                    float oldzoom = zoom;
                    zoom += 1F;
                    ZoomOnMouseCenter(e, oldzoom);
                    this.imageTracker.ScalePercent = zoom * 100;
                    this.Invalidate();
                }
            }
            else
            {
                ZoomFit();
            }
        }

        public void ZoomFit()
        {
            if (this.Image != null)
            {
                this.OffsetX = (this.Width - RealSize.Width) / 2;
                //this.OffsetY = 0;
                this.OffsetY = (this.Height - RealSize.Height) / 2;
            }
            this.zoom = 1;
            this.imageTracker.ScalePercent = zoom * 100;
            this.imageTracker.Invalidate();
            this.Invalidate();
        }

        public void ZoomIn()
        {
            float oldzoom = zoom;
            if (mousePos == Point.Empty)
            {
                mousePos = new Point(this.Width / 2, this.Height / 2);
            }
            MouseEventArgs args = new MouseEventArgs(new MouseButtons(), 1, mousePos.X, mousePos.Y, 0);
            //最大放大16倍
            if (zoom + 1 == 17) return;
            zoom += 1;
            ZoomOnMouseCenter(args, oldzoom);
            this.imageTracker.ScalePercent = zoom * 100;
            this.imageTracker.Invalidate();
            this.Refresh();
            //this.Invalidate();
        }

        public void ZoomOut()
        {
            if (zoom > 1)
            {
                float oldzoom = zoom;
                if (mousePos == Point.Empty)
                {
                    mousePos = new Point(this.Width / 2, this.Height / 2);
                }
                MouseEventArgs args = new MouseEventArgs(new MouseButtons(), 1, mousePos.X, mousePos.Y, 0);
                //zoom = Math.Max(zoom - 0.2F, 0.01F);
                zoom -= 1;
                ZoomOnMouseCenter(args, oldzoom);
                this.imageTracker.ScalePercent = zoom * 100;
                this.imageTracker.Invalidate();
                this.Refresh();
                //this.Invalidate();
            }
        }

        /// <summary>
        /// 新建物镜，需要放大适当的倍数
        /// </summary>
        public void ZoomNewLense(float zoom, float newZoom)
        {
            float oldzoom = zoom;
            Point centerPoint = new Point(this.Width / 2, this.Height / 2);
            MouseEventArgs args = new MouseEventArgs(new MouseButtons(), 1, centerPoint.X, centerPoint.Y, 0);
            this.Zoom = newZoom;
            ZoomOnMouseCenter(args, oldzoom);
            this.imageTracker.ScalePercent = zoom * 100;
            this.Invalidate();
        }

        public bool CanZoom()
        {
            return this.Picture != null;
        }

        public void LoadImage(string imageFile)
        {
            //判断视频是否开启，若开启则先关闭视频
            DelegateClass.GetDelegate().CheckCloseVideoHandler?.Invoke();
            this.Picture = Image.FromFile(imageFile);
            this.OffsetX = (this.Width - this.Image.Width) / 2;
            this.OffsetY = (this.Height - this.Image.Height) / 2 + 25;
            this.zoom = 1;
            this.imageTracker.ScalePercent = zoom * 100;
            this.ImageTracker.Picture = this.Picture;
            this.Invalidate();
        }

        #region 检查按键
        public static bool IsShiftKeyPressed
        {
            get { return (Control.ModifierKeys & Keys.Shift) != 0; }
        }
        public static bool IsAltKeyPressed
        {
            get { return (Control.ModifierKeys & Keys.Alt) != 0; }
        }
        public static bool IsCtrlKeyPressed
        {
            get { return (Control.ModifierKeys & Keys.Control) != 0; }
        } 
        #endregion
        private string GetRecordTime()
        {
            int minutes = this.recordCount / 60;
            int seconds = this.recordCount % 60;
            string sMinutes = string.Format("{0}", minutes).PadLeft(2, '0');
            string sSeconds = string.Format("{0}", seconds).PadLeft(2, '0');
            return string.Format("00:{0}:{1}", sMinutes, sSeconds);
        }
        public  void ToHighQuality(Graphics graphics)
        {
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        public  void ToLowQuality(Graphics graphics)
        {
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (PictureBoxPaintedEvent != null)
                {
                    Rectangle controlClientRect = ClientRectangle;
                    controlClientRect.X -= OffsetX;
                    controlClientRect.Y -= OffsetY;
                    PictureBoxPaintedEvent(controlClientRect, this.ClientRectangle);
                }

                if (this.Picture != null)
                {
                    e.Graphics.ScaleTransform(Zoom, Zoom);
                    e.Graphics.TranslateTransform(OffsetX, OffsetY);
                    //ToLowQuality(e.Graphics);
                    e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    e.Graphics.DrawImage(this.Picture, 0, 0, RealSize.Width, RealSize.Height);
                    e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                    //this.imageTracker.Picture = this.Image;
                    e.Graphics.ResetTransform();
                }

                if (LaserFunction)
                {
                    if (Program.EntryForm.Laser != null && Program.SysConfig.Function == SystemFunction.Laser)
                    {
                        Program.EntryForm.Laser.OnPaint(e);
                    }
                }
                if (GraphicsList != null && Program.SysConfig.Function == SystemFunction.Measure)
                {
                    GraphicsList.Draw(e.Graphics, this);
                }
                if (this.restrictArea != null)
                {
                    Color drawColor = Color.FromArgb(200, Color.Blue);

                    using (Pen pen = new Pen(drawColor, 2f))
                    {
                        //模拟模式不需要绘制限制区域
                        if (Program.SysConfig.LiveMode)
                        {
                            //激光校准不需要绘制限制区域
                            var laserAlignment = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserAlignment>(CtrlType.LaserAlignment);
                            if (laserAlignment != null && !laserAlignment.Visible)
                                this.restrictArea.TestDrawMotorRectangle(e.Graphics, pen);
                        }
                    }
                }
                if (this.CaptureVideo)
                {
                    Color drawColor = Color.FromArgb(200, Color.Red);

                    using (Pen pen = new Pen(drawColor, 2f))
                    using (Font font = new Font("宋体", 25f, FontStyle.Bold))
                    using (SolidBrush sb = new SolidBrush(drawColor))
                    {
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        Circle circle = new Circle(new PointF(98, 60), new SizeF(40, 40));
                        //e.Graphics.DrawEllipse(pen, circle.Rectangle);
                        var size = e.Graphics.MeasureString("REC", font);
                        e.Graphics.DrawString("REC", font, sb, circle.CenterPoint.X + 30 + 48, 60 - size.Height / 2);
                        e.Graphics.FillEllipse(sb, circle.Rectangle);
                        string time = GetRecordTime();
                        var tSize = e.Graphics.MeasureString(time, font);
                        e.Graphics.DrawString(time, font, sb, new PointF(circle.CenterPoint.X + 30 + size.Width + 48, 60 - tSize.Height / 2));
                    }
                }
                if (rulers != null)
                {
                    rulers.Draw(e.Graphics);
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
