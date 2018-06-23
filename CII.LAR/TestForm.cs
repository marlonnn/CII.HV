using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GraphicsPath gp2 = new GraphicsPath();
            Size s = new Size(materialSlider2.ThumbSize, materialSlider2.Height * 9 / 10);
            //gp2.AddPolygon(new PointF[] {
            //                   new PointF(0, 0),
            //                   new PointF(s.Width, 0),
            //                   new PointF(s.Width, -materialSlider1.Height * 2 / 5f),
            //                   new PointF(s.Width / 2, -materialSlider1.Height * 0.6f),
            //                   new PointF(0, -materialSlider1.Height * 2 / 5f)
            //               });
            gp2.AddEllipse(new RectangleF((0 + materialSlider2.Height) / 2f, (0 + materialSlider2.Height) / 2f, 16, 16));
            materialSlider2.ThumbCustomShape = gp2;
        }
    }
}
