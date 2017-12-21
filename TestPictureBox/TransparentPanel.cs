using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPictureBox
{
    public partial class TransparentPanel : Panel
    {
        public TransparentPanel()
        {
            //this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint |
            //ControlStyles.UserPaint |
            //ControlStyles.OptimizedDoubleBuffer, true);
            //this.UpdateStyles();
            //this.SetStyle(
            //    ControlStyles.AllPaintingInWmPaint |
            //    ControlStyles.UserPaint |
            //    ControlStyles.DoubleBuffer,
            //    true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint |
            //    ControlStyles.SupportsTransparentBackColor, true);
            //base.BackColor = Color.FromArgb(0, 0, 0, 0);//Added this because image wasnt redrawn when resizing form
            //this.BackColor = Color.Transparent;
            //this.ForeColor = Color.Transparent;
            //this.DoubleBuffered = true;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }
    }
}
