using CII.LAR.DrawTools;
using CII.LAR.SysClass;
using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.Laser
{
    public class BaseLaser
    {
        protected Timer FlashTimer;

        protected int _flickCount;
        private object sync = new object();
        public int FlickCount
        {
            get
            {
                lock(sync)
                    return this._flickCount;
            }
            set
            {
                lock(sync)
                {
                    if (value != this._flickCount)
                    {
                        this._flickCount = value;
                    }
                }
            }
        }

        private bool _flashing;
        public bool Flashing
        {
            get { return this._flashing; }
            set
            {
                FlickCount = 0;
                this._flashing = value;
                if (value)
                {
				    FlickCount = -1;
                    this.FlashTimer.Enabled = value;
                    this.FlashTimer.Start();
                    //this.FlashTimer_Tick(null, null);
                }
                else
                {
                    this.FlashTimer.Enabled = value;
                    this.FlashTimer.Stop();
                }
                this.richPictureBox.Invalidate();
            }
        }
        protected RichPictureBox richPictureBox;

        protected SolidBrush brush;
        public SolidBrush Brush
        {
            get { return this.brush; }
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

        public BaseLaser()
        {
            this.FlashTimer = new Timer();
            this.FlashTimer.Interval = 1000;
            this.FlashTimer.Tick += new System.EventHandler(this.FlashTimer_Tick);
            GraphicsProperties = Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Circle");
        }

        protected virtual void FlashTimer_Tick(object sender, EventArgs e)
        {
            FlickCount++;
            this.richPictureBox.Invalidate();
            if (FlickCount == 2)
            {
                Flashing = false;
            }
        }

        protected PointF ToZoomPointF(PointF point)
        {
            return new PointF(point.X / richPictureBox.Zoom - richPictureBox.OffsetX, point.Y / richPictureBox.Zoom - richPictureBox.OffsetY);
        }

        protected Point ToZoomPoint(Point point)
        {
            return Point.Ceiling(new PointF(point.X / richPictureBox.Zoom - richPictureBox.OffsetX, point.Y / richPictureBox.Zoom - richPictureBox.OffsetY));
        }

        /// <summary>
        /// Left nous button is pressed
        /// </summary>
        /// <param name="richPictureBox"></param>
        /// <param name="e"></param>
        public virtual void OnMouseDown(RichPictureBox richPictureBox, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Mouse is moved, left mouse button is pressed or none button is pressed
        /// </summary>
        /// <param name="richPictureBox"></param>
        /// <param name="e"></param>
        public virtual void OnMouseMove(RichPictureBox richPictureBox, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Left mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseUp(RichPictureBox richPictureBox, MouseEventArgs e)
        {
            //保存鼠标当前点
            Program.SysConfig.Point = e.Location;
        }

        public virtual void OnPaint(PaintEventArgs e)
        {

        }

        public virtual void ClearLaser()
        {

        }
    }
}
