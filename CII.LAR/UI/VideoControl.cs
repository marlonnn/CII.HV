using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Controls;
using CII.LAR.DrawTools;
using System.IO;
using System.Resources;

namespace CII.LAR.UI
{
    public partial class VideoControl : VideoSourcePlayer
    {
        private static Cursor s_cursor = new Cursor(new MemoryStream((byte[])new ResourceManager(typeof(EntryForm)).GetObject("Cross")));
        private bool mousePressed;
        private Point lastBaseCtrlMousePos;//记录鼠标指针的坐标

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

        public VideoControl()
        {
            this.DoubleBuffered = true;
            this.GraphicsList = new GraphicsList();
            this.rulers = new Rulers(this);
            InitializeTools();
            this.KeyDown += VideoControl_KeyDown;
            this.MouseDown += VideoControl_MouseDown;
            this.MouseMove += VideoControl_MouseMove;
            this.MouseUp += VideoControl_MouseUp;
            this.Paint += VideoControl_Paint;
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

        private void VideoControl_MouseUp(object sender, MouseEventArgs e)
        {
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

        private void VideoControl_MouseMove(object sender, MouseEventArgs e)
        {
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
                    if (mousePressed)
                    {
                        Point newPos = new Point(
                            draggingBaseCtrlRectangle.Location.X + e.X - lastBaseCtrlMousePos.X,
                            draggingBaseCtrlRectangle.Location.Y + e.Y - lastBaseCtrlMousePos.Y);
                        Rectangle newPictureTrackerArea = draggingBaseCtrlRectangle;
                        newPictureTrackerArea.Location = newPos;

                        this.lastBaseCtrlMousePos = e.Location;
                        // dragging Statistics ctrl only when the candidate dragging rectangle
                        // is within this ScalablePictureBox control
                        if (this.ClientRectangle.Contains(newPictureTrackerArea))
                        {
                            // updating dragging rectangle
                            draggingBaseCtrlRectangle = newPictureTrackerArea;
                        }
                    }
                }
            }
        }

        private void VideoControl_MouseDown(object sender, MouseEventArgs e)
        {
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

                            lastBaseCtrlMousePos.X = e.X;
                            lastBaseCtrlMousePos.Y = e.Y;
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

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            tools[(int)ActiveTool].OnDoubleClick(this, e);
        }

        private void VideoControl_Paint(object sender, PaintEventArgs e)
        {
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

        private void VideoControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (VideoKeyDownHandler != null)
            {
                VideoKeyDownHandler(e);
            }
        }

        public void LoadImage(string imageFile)
        {
            this.BackgroundImage = Image.FromFile(imageFile);
            this.Invalidate();
        }

        public void GraphicsPropertiesChangedHandler(DrawObject drawObject, GraphicsProperties graphicsProperties)
        {
            this.Invalidate();
        }
    }
}
