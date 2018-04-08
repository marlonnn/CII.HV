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
    public partial class UpDownArrowCtrl : UserControl
    {
        private string labelValue;
        [Description("Label text"), Category("UpDownArrowCtrl"), DefaultValue(typeof(string), "")]
        public string LabelValue
        {
            get { return this.labelValue; }
            set
            {
                if (value != null && value != labelValue)
                {
                    InvokeInvalidate(this.lblValue, value);
                    this.Invalidate();
                }
            }
        }

        private void InvokeInvalidate(Label lable, string value)
        {
            if (!IsHandleCreated)
                return;
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.labelValue = value;
                    lable.Text = value;
                });
            }
            catch { }
        }

        public UpDownArrowCtrl()
        {
            InitializeComponent();
            this.labelValue = "";
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            UpClick(sender, e);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            DownClick(sender, e);
        }

        protected virtual void UpClick(object sender, EventArgs e) { }

        protected virtual void DownClick(object sender, EventArgs e) { }
    }
}
