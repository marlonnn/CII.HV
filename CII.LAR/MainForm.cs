using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR
{
    public partial class MainForm : BaseXtraForm
    {
        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.toolStrip1.Paint += ToolStrip1_Paint;
        }

        private void ToolStrip1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, this.toolStrip1.Width - 2, this.toolStrip1.Height - 2);
            e.Graphics.SetClip(rect);

            using (Pen pen = new Pen(Color.Black, 1.0f))
            {
                Rectangle rectangle = new Rectangle(0, 0, this.toolStrip1.Width - 3, this.toolStrip1.Height- 3);
                e.Graphics.DrawRectangle(pen, rectangle);
            }
        }
    }
}
