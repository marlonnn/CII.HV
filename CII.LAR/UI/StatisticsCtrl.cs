using CII.LAR.SysClass;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class StatisticsCtrl : BaseCtrl
    {
        /// <summary>
        /// delegate of StatisticsCtrl control closed event handler
        /// </summary>
        public delegate void StatisticsClosedHandler();

        /// <summary>
        /// StatisticsCtrl control closed event
        /// </summary>
        public event StatisticsClosedHandler StatisticsClosed;

        public ListViewEx StatisticsListView
        {
            get
            {
                return this.listViewEx;
            }
            set
            {
                this.listViewEx = value;
            }
        }

        public MaterialSkin.MaterialRoundButton BtnAppearance
        {
            get
            {
                return this.btnAppearance;
            }
            private set
            {
                this.btnAppearance = value;
            }
        }

        public StatisticsCtrl() : base()
        {
            this.ShowIndex = 2;
            this.CtrlType = CtrlType.StatisticsCtrl;
            InitializeComponent();
            resources = new ComponentResourceManager(typeof(StatisticsCtrl));
        }


        /// <summary>
        /// close the StatisticsCtrl control when close button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void closeButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Enabled = false;
            StatisticsClosed?.Invoke();
        }

        private void btnAppearance_Click(object sender, EventArgs e)
        {
            DelegateClass.GetDelegate().ClickDelegateHandler?.Invoke(sender, CtrlType.RulerAppearanceCtrl);
        }

        public override void RefreshUI()
        {
            this.Title = global::CII.LAR.Properties.Resources.StrStatisticsTitle;
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            resources.ApplyResources(btnAppearance, btnAppearance.Name);
            this.Invalidate();
        }
    }
}