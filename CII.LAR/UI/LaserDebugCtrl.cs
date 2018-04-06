using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class LaserDebugCtrl : BaseCtrl
    {
        public LaserDebugCtrl()
        {
            this.CtrlType = CtrlType.LaserDebugCtrl;
            InitializeComponent();
        }

        private void slider_ValueChanged(object sender, EventArgs e)
        {

        }

        private void slider_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
