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

        private RichPictureBox richPictureBox;
        public LaserFactory(RichPictureBox richPictureBox)
        {
            this.richPictureBox = richPictureBox;
            this.fixedLaser = new FixedLaser(richPictureBox);
            this.activeLaser = new ActiveLaser(richPictureBox);
            this.alignLaser = new AlignLaser(richPictureBox);
            this.alignLaser.ZoomHandler += richPictureBox.ZoomHandler;
            alignLaser.ButtonStateHandler += Program.EntryForm.ButtonStateHandler;
        }

        private static LaserFactory factory;

        public static LaserFactory GetInstance(RichPictureBox richPictureBox)
        {
            if (factory == null)
            {
                factory = new LaserFactory(richPictureBox);
            }
            return factory;
        }
    }
}
