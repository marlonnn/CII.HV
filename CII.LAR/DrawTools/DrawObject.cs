using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Base class for all draw objects
    /// Author:Zhong Wen 2017/07/25
    /// </summary>
    public abstract class DrawObject
    {
        public ObjectType ObjectType;

        protected RichPictureBox richPictureBox;

        private const double D254 = 2.54d;
        protected double UnitOfMeasureFactor
        {
            get
            {
                return MeasureSystem.CustomUnitToMicron(1, richPictureBox.UnitOfMeasure);
            }
        }

        public double PixelToCentimeter(double pixel)
        {
            return pixel * D254 / Program.DpiX;
        }

        public double PixelToMillimeter(double pixel)
        {
            return PixelToCentimeter(pixel) * 10;
        }

        /// <summary>
        /// GraphicsProperties of this draw object 
        /// </summary>
        private GraphicsProperties graphicsProperties;
        public GraphicsProperties GraphicsProperties
        {
            get
            {
                return graphicsProperties;
            }
            set
            {
                graphicsProperties = value;
            }
        }

        /// <summary>
        /// Update statistic information delegate ang handler
        /// </summary>
        /// <param name="drawObject"></param>
        /// <param name="statistics"></param>
        public delegate void UpdateStatisticInfo(DrawObject drawObject, Statistics statistics);
        public UpdateStatisticInfo UpdateStatisticInfoHandler;
        public enum ElementType
        {
            Nothing,
            Label,
            Handle,
            Gate
        }

        protected void RegisterUpdateStatisticsHandler()
        {
            this.UpdateStatisticInfoHandler += Program.EntryForm.UpdateStatisticInfoHandler;
        }

        public void UpdateStatisticsInformation()
        {
            this.UpdateStatisticInfoHandler?.Invoke(this, Statistics);
        }

        public class HitTestResult
        {
            public ElementType ElementType
            {
                get;
                private set;
            }


            /// <summary>
            /// handle, gate or label index 
            /// </summary>
            public int Index
            {
                get;
                private set;
            }


            public HitTestResult(ElementType type, int index)
            {
                ElementType = type;
                Index = index;
            }
        }

        private bool selected;

        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                // can not set creating object not selected
                if (Creating && !value)
                {
                    return;
                }

                this.selected = value;
            }
        }

        public virtual string Prefix
        {
            get
            {
                return "";
            }
        }

        //graphic object name
        private string name;
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private bool creating; // this value does not need to serialize, it always false when deserializing
        public bool Creating
        {
            get { return creating; }
            set { creating = value; }
        }

        protected RectangleF[] _rectProportion; // gate label rectangle proportion        

        protected RectangleF[] rectF;   // gate label rectangle, keep for old data, and in memory use

        //graphic object ID
        // Allows to write Undo - Redo functions and don't care about
        // objects order in the list.
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private bool _autoSnap;
        public bool AutoSnap
        {
            get { return _autoSnap; }
            set { _autoSnap = value; }
        }

        protected static Size DefaultDrawAreaSize = new Size(256, 256); //const draw area size use for hit test

        private Font font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular);

        public Font Font
        {
            get { return font; }
            set { font = value; }
        }

        private Point movingOffset;
        public Point MovingOffset
        {
            get { return movingOffset; }
            set { movingOffset = value; }
        }

        public bool IsMoving
        {
            get
            {
                if (!MovingOffset.IsEmpty)
                {
                    if (Control.ModifierKeys == Keys.Control)
                    {
                        if (MovingOffset.X == 10 || MovingOffset.X == -10 || MovingOffset.Y == 10 || MovingOffset.Y == -10)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if (MovingOffset.X == 1 || MovingOffset.X == -1 || MovingOffset.Y == 1 || MovingOffset.Y == -1)
                    {
                        return false;
                    }
                    else
                    {
                        return !MovingOffset.IsEmpty;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        protected int SelectionHitTestWidth = 4;

        /// <summary>
        /// Number of handles
        /// </summary>
        public virtual int HandleCount
        {
            get
            {
                return 0;
            }
        }
        public virtual bool IsHandleVisible(int handleNumber)
        {
            return true;
        }

        private Statistics statistics = new Statistics();

        public Statistics Statistics
        {
            get
            {
                return statistics;
            }
            set
            {
                statistics = value;
            }
        }

        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.Default;
        }

        public virtual void UpdateHitTestRegions()
        {
            // update gate for data hit test
        }

        public abstract bool HitTest(int nIndex, PointF dataPoint);

        public HitTestResult HitTest(RichPictureBox richPictureBox, Point point, bool forSelection, bool hitTestHandle = true)
        {
            if (Selected && hitTestHandle)
            {
                for (int i = 1; i <= HandleCount; i++)
                {
                    if (CheckHandleRegion(richPictureBox, i, point))
                    {
                        return new HitTestResult(ElementType.Handle, i);
                    }
                }
            }
            UpdateHitTestRegions();
            if (forSelection)
            {
                return HitTestForSelection(richPictureBox, point);
            }
            else
            {
                for (int i = 0; i < richPictureBox.GraphicsList.Count; i++)
                {
                    PointF dataPoint = point;
                    if (HitTest(i, dataPoint))
                    {
                        return new HitTestResult(ElementType.Gate, i);
                    }
                }
                return new HitTestResult(ElementType.Nothing, -1);
            }
        }

        protected virtual bool CheckHandleRegion(RichPictureBox richPictureBox, int handleNumber, Point point)
        {
            return (GetHandleRectangle(richPictureBox, handleNumber).Contains(point));
        }

        /// <summary>
        /// hit test function for user selection by mouse left button
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="point"></param>
        public abstract HitTestResult HitTestForSelection(RichPictureBox richPictureBox, Point point);

        /// <summary>
        /// Get handle point by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Point GetHandle(RichPictureBox richPictureBox, int handleNumber)
        {
            return new Point(0, 0);
        }

        /// <summary>
        /// Get handle rectangle by 1-based number
        /// </summary>
        /// <param name="handleNumber"></param>
        /// <returns></returns>
        public virtual Rectangle GetHandleRectangle(RichPictureBox richPictureBox, int handleNumber)
        {
            Point point = GetHandle(richPictureBox, handleNumber);

            return new Rectangle(point.X - 3, point.Y - 3, 6, 6);
        }

        /// <summary>
        /// Move object
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public virtual void Move(RichPictureBox richPictureBox, int deltaX, int deltaY)
        {
        }

        /// <summary>
        /// Move handle to the point
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public virtual void MoveHandleTo(RichPictureBox richPictureBox, Point point, int handleNumber)
        {
        }

        /// <summary>
        /// Normalize object.
        /// Call this function in the end of object resizing.
        /// </summary>
        public virtual void Normalize()
        {
        }

        public void RaiseChanged(bool userManually = false)
        {
            UpdateHitTestRegions();
        }
        /// <summary>
        /// Draw object
        /// </summary>
        /// <param name="g"></param>
        public virtual void Draw(Graphics g, RichPictureBox richPictureBox)
        {
        }

        public virtual void DrawText(Graphics g, RichPictureBox richPictureBox)
        {
            SolidBrush brush = new SolidBrush(Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Text").Color);
            RectangleF r = GetTextF(this.Name, g, this.ID);
            r.Offset(MovingOffset);
            
            g.DrawString(this.Name, this.Font, brush, r);
            brush.Dispose();
        }

        public virtual RectangleF GetTextF(string name, Graphics g, int index)
        {
            return new RectangleF();
        }

        public virtual void DrawTracker(Graphics g, RichPictureBox richPictureBox)
        {
            if (Selected)
            {
                SolidBrush brush = new SolidBrush(Color.White);
                Pen pen = new Pen(Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Text").Color,
                    Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Text").PenWidth);

                for (int i = 1; i <= HandleCount; i++)
                {
                    if (!IsHandleVisible(i))
                        continue;

                    Rectangle r = GetHandleRectangle(richPictureBox, i);
                    if (this is DrawEllipse)
                    {
                        r.Offset(-richPictureBox.OffsetX, -richPictureBox.OffsetY);
                        r.Offset(MovingOffset);
                    }
                    else
                    {
                        r.Offset(MovingOffset);
                    }
                    try
                    {
                        g.DrawRectangle(pen, r);
                        g.FillRectangle(brush, r);
                    }
                    catch (System.Exception ex)
                    {
                        Selected = false;
                    }
                }
                pen.Dispose();
                brush.Dispose();
            }
        }
    }
}
