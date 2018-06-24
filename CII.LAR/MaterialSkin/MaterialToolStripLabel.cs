using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.MaterialSkin
{
    public class MaterialToolStripLabel : ToolStripLabel, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public MaterialToolStripLabel()
        {
            this.ForeColor = SkinManager.GetLabelTextColor();
            this.Font = SkinManager.PINGFANG_MEDIUM_9;
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //base.OnPaint(e);
        //    SizeF textSize = e.Graphics.MeasureString(this.Text, this.Font);
        //    //e.Graphics.DrawString(this.Text, this.Font, Brushes.Red, new PointF((this.Size.Width - textSize.Width) / 2f, -2));
        //    e.Graphics.DrawString(this.Text, this.Font, Brushes.Red, new PointF(0, 0));
        //}
    }
}
