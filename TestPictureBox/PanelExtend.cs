using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPictureBox
{
    public abstract class PanelExtend : Panel
    {
        protected Graphics graphics;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // 实现透明样式  

                return cp;
            }
        }
        public PanelExtend()
        {
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.Transparent;
        }
    }
}
