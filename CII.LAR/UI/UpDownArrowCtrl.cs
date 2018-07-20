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

        public string LabelText
        {
            get { return this.lblValue.Text; }
        }

        private void InvokeInvalidate(TextBox lable, string value)
        {
            //if (!IsHandleCreated)
            //    return;
            //try
            //{
            //    this.Invoke((MethodInvoker)delegate
            //    {
            //        this.labelValue = value;
            //        lable.Text = string.Format("{0}%", value);
            //    });
            //}
            //catch { }
            this.labelValue = value;
            lable.Text = string.Format("{0}", value);
        }

        private Timer timer;
        private bool isUpLongPress;
        public UpDownArrowCtrl()
        {
            InitializeComponent();
            this.labelValue = "";
            this.lblValue.KeyDown += LblValue_KeyDown;
            this.btnDown.MouseDown += BtnDown_MouseDown;
            this.btnUp.MouseUp += BtnUp_MouseUp;
            this.btnDown.MouseUp += BtnDown_MouseUp;
            this.btnUp.MouseDown += BtnUp_MouseDown;
            isUpLongPress = true;
            timer = new Timer();
            timer.Enabled = false;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (isUpLongPress)
            {
                UpClick(null, null);
            }
            else
            {
                DownClick(null, null);
            }
        }

        private void BtnUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (!timer.Enabled)
            {
                isUpLongPress = true;
                timer.Interval = 100;
                timer.Enabled = true;
            }
        }

        private void BtnDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Enabled = false;
            }
        }

        private void BtnUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Enabled = false;
            }
        }

        private void BtnDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (!timer.Enabled)
            {
                isUpLongPress = false;
                timer.Interval = 100;
                timer.Enabled = true;
            }
        }

        public delegate void LabelValueKeyDown();
        public LabelValueKeyDown LabelValueKeyDownHandler;
        private void LblValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LabelValueKeyDownHandler?.Invoke();
            }
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
