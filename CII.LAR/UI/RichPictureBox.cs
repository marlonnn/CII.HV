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

namespace CII.LAR.UI
{
    public partial class RichPictureBox : PictureBox
    {
        private static Cursor s_cursor = new Cursor(new MemoryStream((byte[])new ResourceManager(typeof(EntryForm)).GetObject("Cross")));
        private bool mousePressed;
        private Point lastMousePos;//记录鼠标指针的坐标
        private Point mousePos = Point.Empty;

        /// <summary>
        /// 当前帧图和缩略图帧图
        /// </summary>
        private Image picture;
        public Image Picture
        {
            get { return this.picture; }
            set
            {
                if (this.picture != null) this.picture.Dispose();
                if (value != this.picture)
                {
                    this.picture = value;
                    this.Image = value;
                    this.imageTracker.Picture = value;
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

        private Rulers rulers;
        public Rulers Rulers
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

        public delegate void VideoKeyDown(KeyEventArgs e);
        public VideoKeyDown VideoKeyDownHandler;

        #region Measure tool
        public event OnMeasureUnitChangedEventHandler OnMeasureUnitChanged;
        public delegate void OnMeasureUnitChangedEventHandler(enUniMis unit);
        public const enUniMis DefaultUnitOfMeasure = enUniMis.mm;
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
            this.rulers = new Rulers(this);
            InitializeTools();
            InitializeImageTracker();
            this.PictureBoxPaintedEvent += imageTracker.OnPicturePainted;
        }

        public DebugCtrl df;
        public void LoadDebugCtrl()
        {
            df = new DebugCtrl();
            df.VideoKeyDownHandler += this.OnKeyDown;
            df.Location = new Point(10, this.Height - df.Height);
            this.Controls.Add(df);
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
            this.imageTracker.Location = new System.Drawing.Point(5, 30);
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
            if (VideoKeyDownHandler != null)
            {
                VideoKeyDownHandler(e);
            }
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
                    zoom += 1;
                    ZoomOnMouseCenter(e, oldzoom);
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
                    if (zoom > 1)
                    {
                        zoom -= 1;
                        ZoomOnMouseCenter(e, oldzoom);
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
                for (int i = 0; i < 4; i++)
                {
                    float oldzoom = zoom;
                    zoom += 1F;
                    ZoomOnMouseCenter(e, oldzoom);
                    this.imageTracker.ScalePercent = oldzoom * 100;
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
            zoom += 1;
            ZoomOnMouseCenter(args, oldzoom);
            this.imageTracker.ScalePercent = zoom * 100;
            this.Invalidate();
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
                this.Invalidate();
            }
        }

        public void LoadImage(string imageFile)
        {
            this.Picture = Image.FromFile(imageFile);
            this.OffsetX = (this.Width - this.Image.Width) / 2;
            this.OffsetY = (this.Height - this.Image.Height) / 2 + 25;
            this.zoom = 1;
            this.imageTracker.ScalePercent = zoom * 100;
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
                    e.Graphics.DrawImage(this.Picture, 0, 0, RealSize.Width, RealSize.Height);
                    //this.imageTracker.Picture = this.Image;
                    e.Graphics.ResetTransform();
                }

                if (LaserFunction)
                {
                    if (Program.EntryForm.Laser != null)
                    {
                        Program.EntryForm.Laser.OnPaint(e);
                    }
                }
                if (GraphicsList != null)
                {
                    GraphicsList.Draw(e.Graphics, this);
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
