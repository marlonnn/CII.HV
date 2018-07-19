﻿using System;
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
    public partial class AboutControl : BaseCtrl
    {
        public AboutControl()
        {
            resources = new ComponentResourceManager(typeof(AboutControl));
            InitializeComponent();
        }

        private void materialRoundButton1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        public override void RefreshUI()
        {
            base.RefreshUI();
            this.Title = global::CII.LAR.Properties.Resources.StrAboutCtrlTitle;
        }
    }
}