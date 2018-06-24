using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    public class MaterialToolStripStatusLabel : ToolStripStatusLabel, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public MaterialToolStripStatusLabel()
        {
            this.ForeColor = SkinManager.GetLabelTextColor();
            this.Font = SkinManager.PINGFANG_MEDIUM_9;
        }
    }
}
