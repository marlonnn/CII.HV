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
                zoom = value;
            }
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
            Tools = new Tool[(int)DrawToolType.NumberOfDrawTools];
            Tools[(int)DrawToolType.None] = new Tool();
            Tools[(int)DrawToolType.Pointer] = new ToolPointer();
            Tools[(int)DrawToolType.Line] = new ToolLine();
            Tools[(int)DrawToolType.Move] = new ToolMove();
        }

        private void ZWPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Tools[(int)ActiveTool].OnMouseUp(this, e);
            }
        }

        private void ZWPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
            {
                Tools[(int)ActiveTool].OnMouseMove(this, e);
            }
        }

        private void ZWPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Tools[(int)ActiveTool].OnMouseDown(this, e);
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

        public void OnLoad()
        {
            var height = this.Height;
            var width = this.Width;
        }

        public int StartOffsetX = 0;

        public void LoadImage(string imageFile)
        {
            this.Image = Image.FromFile(imageFile);
            StartOffsetX = (this.Width - this.Image.Width) / 2;
            this.OffsetX = StartOffsetX;
            this.OffsetY = (this.Height - this.Image.Height) / 2 + 25;
            //imageTracker.Picture = this.Image;
            this.zoom = 1;
            //this.imageTracker.ScalePercent = zoom * 100;
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
