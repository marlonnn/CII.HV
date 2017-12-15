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

        private ZWPictureBox picturebox;
        public LaserFactory(ZWPictureBox picturebox)
        {
            this.picturebox = picturebox;
            this.fixedLaser = new FixedLaser(picturebox);
            this.activeLaser = new ActiveLaser(picturebox);
            this.alignLaser = new AlignLaser(picturebox);
            alignLaser.ZoomHandler += picturebox.ZoomHandler;
            alignLaser.ButtonStateHandler += Program.EntryForm.ButtonStateHandler;
        }

        private static LaserFactory factory;

        public static LaserFactory GetInstance(ZWPictureBox picturebox)
        {
            if (factory == null)
            {
                factory = new LaserFactory(picturebox);
            }
            return factory;
        }
    }
}
