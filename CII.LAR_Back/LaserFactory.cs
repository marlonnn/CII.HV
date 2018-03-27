using CII.LAR.Laser;
using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR
{
    public class LaserFactory
    {
        private FixedLaser fixedLaser;
        public FixedLaser FixedLaser
        {
            get { return this.fixedLaser; }
        }

        private ActiveLaser activeLaser;
        public ActiveLaser ActiveLaser
        {
            get { return this.activeLaser; }
        }

        private AlignLaser alignLaser;
        public AlignLaser AlignLaser
        {
            get { return this.alignLaser; }
        }

        private VideoControl videoControl;
        public LaserFactory(VideoControl videoControl)
        {
            this.videoControl = videoControl;
            this.fixedLaser = new FixedLaser(videoControl);
            this.activeLaser = new ActiveLaser(videoControl);
            this.alignLaser = new AlignLaser(videoControl);
            this.alignLaser.ZoomHandler += videoControl.ZoomHandler;
            alignLaser.ButtonStateHandler += Program.EntryForm.ButtonStateHandler;
        }

        private static LaserFactory factory;

        public static LaserFactory GetInstance(VideoControl videoControl)
        {
            if (factory == null)
            {
                factory = new LaserFactory(videoControl);
            }
            return factory;
        }
    }
}
