﻿using CII.LAR.MaterialSkin;
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
    public partial class ChildForm : MaterialForm
    {
        public ChildForm()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.panel1.Size = new Size(this.panel1.Size.Width, this.Height - 32);
        }
    }
}