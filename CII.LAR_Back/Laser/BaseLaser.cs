﻿using CII.LAR.DrawTools;
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
        public int FlickCount
        {
            get { return this._flickCount; }
        }

        private bool _flashing;
        public bool Flashing
        {
            get { return this._flashing; }
            set
            {
                _flickCount = 0;
                this._flashing = value;
                if (value)
                {
                    this.FlashTimer.Enabled = value;
                    this.FlashTimer.Start();
                    _flickCount = -1;
                    this.FlashTimer_Tick(null, null);
                }
                else
                {
                    this.FlashTimer.Enabled = value;
                    this.FlashTimer.Stop();
                }
                this.videoControl.Invalidate();
            }
        }
        protected VideoControl videoControl;

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
            _flickCount++;
            this.videoControl.Invalidate();
            if (_flickCount == 6)
            {
                Flashing = false;
            }
        }

        /// <summary>
        /// Left nous button is pressed
        /// </summary>
        /// <param name="videoControl"></param>
        /// <param name="e"></param>
        public virtual void OnMouseDown(VideoControl videoControl, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Mouse is moved, left mouse button is pressed or none button is pressed
        /// </summary>
        /// <param name="videoControl"></param>
        /// <param name="e"></param>
        public virtual void OnMouseMove(VideoControl videoControl, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Left mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public virtual void OnMouseUp(VideoControl videoControl, MouseEventArgs e)
        {
        }

        public virtual void OnPaint(PaintEventArgs e)
        {

        }
    }
}