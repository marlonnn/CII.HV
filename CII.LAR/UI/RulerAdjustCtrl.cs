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
    public partial class RulerAdjustCtrl : UpDownArrowCtrl
    {
        public delegate void UpdownClick(bool isUp);
        public UpdownClick UpdownClickHandler;

        public RulerAdjustCtrl()
        {
            InitializeComponent();
        }

        protected override void UpClick(object sender, EventArgs e)
        {
            UpdownClickHandler?.Invoke(true);
        }

        protected override void DownClick(object sender, EventArgs e)
        {
            UpdownClickHandler?.Invoke(false);
        }
    }
}
