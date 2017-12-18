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
using System.Drawing.Drawing2D;
using System.IO;
using System.Resources;

namespace CII.LAR.UI
{
    public enum PanDirection
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    public enum DrawToolType
    {
        None,
        Pointer,
        Line,
        Rectangle,
        Ellipse,
        Polygon,
        PolyLine,
        Circle,
        MultipleCircle,
        Move,
        NumberOfDrawTools
    }

    public partial class ZWPictureBox : PictureBox
    {
        private static Cursor s_cursor = new Cursor(new MemoryStream((byte[])new ResourceManager(typeof(ZWPictureBox)).GetObject("Cross")));
        private bool mousePressed;
        //current offset of image
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
                if (value != zoom)
                {
                    this.zoom = value;
                }
            }
        }
        private DrawObject drawObject;
        public DrawObject DrawObject
        {
            get { return this.drawObject; }
            set { this.drawObject = value; }
        }

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

        private Point mousePos = Point.Empty;

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

        public ZWPictureBox()
        {
            this.SetStyle(ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.OptimizedDoubleBuffer, true);
            this.MouseDown += ZWPictureBox_MouseDown;
            this.MouseMove += ZWPictureBox_MouseMove;
            this.MouseUp += ZWPictureBox_MouseUp;
            this.GraphicsList = new GraphicsList();
            InitializeTools();
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

        #region Mouse down move and up
        private void ZWPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            mousePressed = false;
            if (e.Button == MouseButtons.Left)
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
        }

        private void ZWPictureBox_MouseMove(object sender, MouseEventArgs e)
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
                    //Point mousePosNow = e.Location;

                    //int deltaX = mousePosNow.X - mouseDownPoint.X;
                    //int deltaY = mousePosNow.Y - mouseDownPoint.Y;

                    //OffsetX = (int)(startX + deltaX / zoom);
                    //OffsetY = (int)(startY + deltaY / zoom);

                    //this.Invalidate();
                }
            }
        }

        private void ZWPictureBox_MouseDown(object sender, MouseEventArgs e)
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
        #endregion

        public void ZoomIn()
        {
            float oldzoom = zoom;
            if (mousePos == Point.Empty)
            {
                mousePos = new Point(this.Width / 2, this.Height / 2);
            }
            MouseEventArgs args = new MouseEventArgs(new MouseButtons(), 1, mousePos.X, mousePos.Y, 0);
            zoom += 0.2F;
            ZoomOnMouseCenter(args, oldzoom);
            if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                this.Invalidate();
        }

        public void ZoonOut()
        {
            if (zoom > 1)
            {
                float oldzoom = zoom;
                if (mousePos == Point.Empty)
                {
                    mousePos = new Point(this.Width / 2, this.Height / 2);
                }
                MouseEventArgs args = new MouseEventArgs(new MouseButtons(), 1, mousePos.X, mousePos.Y, 0);
                zoom = Math.Max(zoom - 0.2F, 0.01F);
                ZoomOnMouseCenter(args, oldzoom);
                if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                    this.Invalidate();
            }
        }

        public void ZoomFit()
        {
            if (this.Image != null)
            {
                this.OffsetX = (this.Width - this.Image.Width) / 2;
                //this.OffsetY = 0;
                this.OffsetY = (this.Height - this.Image.Height) / 2;
            }
            this.zoom = 1;
            if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                this.Invalidate();
        }

        public void ZoomOnMouseCenter(MouseEventArgs e, float oldzoom)
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

        public void ZoomHandler(MouseEventArgs e, bool zoomIn)
        {
            if (zoomIn)
            {
                for (int i = 0; i < 4; i++)
                {
                    float oldzoom = zoom;
                    zoom += 1F;
                    ZoomOnMouseCenter(e, oldzoom);
                    //this.imageTracker.ScalePercent = oldzoom * 100;
                    if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                        this.Invalidate();
                }
            }
            else
            {
                ZoomFit();
            }
        }

        public void OnLoad()
        {
            var height = this.Height;
            var width = this.Width;
        }

        public int StartOffsetX = 0;

        public void LoadImage(string imageFile)
        {
            Program.ExpManager.MachineStatus = MachineStatus.Simulate;
            this.Image = Image.FromFile(imageFile);
            StartOffsetX = (this.Width - this.Image.Width) / 2;
            this.OffsetX = StartOffsetX;
            this.OffsetY = (this.Height - this.Image.Height) / 2;
            ////imageTracker.Picture = this.Image;
            //this.zoom = 1;
            ////this.imageTracker.ScalePercent = zoom * 100;
            //int width = (int)(1392 * zoom);
            //int height = (int)(1080 * zoom);
            //this.Bounds = new Rectangle((1920 - width) / 2, (1080 - height) / 2, width, height);
            if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Image != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.ScaleTransform(zoom, zoom);
                e.Graphics.TranslateTransform(OffsetX, OffsetY);
                e.Graphics.DrawImage(this.Image, 0, 0);
            }
            if (GraphicsList != null)
            {
                GraphicsList.Draw(e.Graphics, this);
            }
            if (LaserFunction)
            {
                if (Program.EntryForm.Laser != null)
                {
                    Program.EntryForm.Laser.OnPaint(e);
                }
            }
        }

        public void GraphicsPropertiesChangedHandler(DrawObject drawObject, GraphicsProperties graphicsProperties)
        {
            if (Program.ExpManager.MachineStatus == MachineStatus.Simulate)
                this.Invalidate();
        }

        public delegate void EscapeFullScreen();
        public EscapeFullScreen EscapeFullScreenHandler;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && this.ActiveTool != UI.DrawToolType.Pointer)
            {
                this.ActiveTool = DrawToolType.Pointer;
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                this.ActiveTool = DrawToolType.Move;
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                EscapeFullScreenHandler?.Invoke();
            }
        }
    }
}
