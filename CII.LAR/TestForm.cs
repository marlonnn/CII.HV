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
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            this.materialSliderControl1.SliderValue = 5;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
