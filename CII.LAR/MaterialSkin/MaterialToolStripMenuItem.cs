using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    public class MaterialToolStripMenuItem : ToolStripMenuItem , IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public MaterialToolStripMenuItem ()
        {
            this.Font = SkinManager.PINGFANG_MEDIUM_9;
            this.BackColor = System.Drawing.Color.FromArgb(0x28, 0x2C, 0x35);
            this.ForeColor = SkinManager.FontColor;
        }
    }
}
