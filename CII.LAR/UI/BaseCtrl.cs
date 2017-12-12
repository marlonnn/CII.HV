using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CII.LAR.SysClass;

namespace CII.LAR.UI
{
    /// <summary>
    /// Custom base control
    /// Author: Zhong Wen 2017/08/05
    /// </summary>
    public partial class BaseCtrl : UserControl
    {
        public delegate void UpdateSliderValue(float value);
        public UpdateSliderValue UpdateSliderValueHandler;

        protected ComponentResourceManager resources;
        protected SysConfig sysConfig;

        /// <summary>
        /// title font
        /// </summary>
        protected Font font = new System.Drawing.Font("Times New Roman", 9.75F,
            ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))),
            System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        /// <summary>
        /// title of this contrl
        /// </summary>
        protected string title = "Title";
        [Description("Title of this control"), Category("Appearance"), DefaultValue("Title")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        private int showIndex;
        public int ShowIndex
        {
            get
            {
                return showIndex;
            }
            set
            {
                this.showIndex = value;
            }
        }


        /// <summary>
        /// button click delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="name"></param>
        public delegate void ClickDelegate(object sender, string name);
        public ClickDelegate ClickDelegateHandler;
        public BaseCtrl()
        {
            InitializeComponent();
            sysConfig = SysConfig.GetSysConfig();
            sysConfig.PropertyChanged += SysConfig_PropertyChanged;
            this.Load += BaseCtrl_Load;
        }

        private void BaseCtrl_Load(object sender, EventArgs e)
        {
        }

        private void SysConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == sysConfig.GetPropertyName(() => sysConfig.UICulture))
            {
                RefreshUI();
            }
        }

        protected virtual void RefreshUI()
        {
            foreach (var item in this.Controls)
            {
                Button btnX = item as Button;
                if (btnX != null)
                {
                    resources.ApplyResources(btnX, btnX.Name);
                }
                LabelX lblX = item as LabelX;
                if (lblX != null)
                {
                    resources.ApplyResources(lblX, lblX.Name);
                }
                Label lbl = item as Label;
                if (lbl != null)
                {
                    resources.ApplyResources(lbl, lbl.Name);
                }
            }
        }

        protected virtual void closeButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Enabled = false;
        }

        /// <summary>
        /// paint border and title
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle borderRect = this.ClientRectangle;
            borderRect.Width -= 1;
            borderRect.Height -= 1;
            e.Graphics.DrawRectangle(Pens.Navy, borderRect);

            e.Graphics.DrawString(Title, font, Brushes.Navy, 3, 3);
        }
    }
}
