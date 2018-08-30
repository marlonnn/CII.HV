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
            this.materialSliderControl1.SliderValue = 5;
            this.Paint += Form1_Paint;
            this.timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private int startAngle = 270;
        private int arcLength = 30;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var center = new Point(100, 100);
            var innerR = 10;
            var thickness = 30;
            var outerR = innerR + thickness;
            var outerRect = new Rectangle
                            (center.X - outerR, center.Y - outerR, 2 * outerR, 2 * outerR);
            var innerRect = new Rectangle
                            (center.X - innerR, center.Y - innerR, 2 * innerR, 2 * innerR);

            using (var p = new GraphicsPath())
            {
                p.AddArc(outerRect, startAngle, arcLength);
                p.AddArc(innerRect, startAngle + arcLength, -arcLength);
                p.CloseFigure();
                e.Graphics.FillPath(Brushes.YellowGreen, p);
                e.Graphics.DrawPath(Pens.Black, p);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startAngle += arcLength;
            this.Invalidate();
        }
    }
}
