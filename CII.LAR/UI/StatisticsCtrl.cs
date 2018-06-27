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

        private RichPictureBox richPictureBox;
        public StatisticsCtrl(RichPictureBox richPictureBox) : base()
        {
            this.ShowIndex = 2;
            this.CtrlType = CtrlType.StatisticsCtrl;
            InitializeComponent();
            resources = new ComponentResourceManager(typeof(StatisticsCtrl));
            this.richPictureBox = richPictureBox;
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
            base.RefreshUI();
            this.Title = global::CII.LAR.Properties.Resources.StrStatisticsTitle;
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            resources.ApplyResources(btnAppearance, btnAppearance.Name);
            this.Invalidate();
        }

        public void CalculateStatiscsInformation(double minCir, double maxCir, double aveCir, double minArea, double maxArea, double aveArea)
        {
            this.lblMinCir.Text = string.Format("{0:F2}  {1}", minCir, richPictureBox.UnitOfMeasure.ToString());
            this.lblMaxCir.Text = string.Format("{0:F2}  {1}", aveCir, richPictureBox.UnitOfMeasure.ToString());
            this.lblAveCir.Text = string.Format("{0:F2}  {1}", aveCir, richPictureBox.UnitOfMeasure.ToString());
            this.lblMinArea.Text = string.Format("{0:F2} {1}²", minArea, richPictureBox.UnitOfMeasure.ToString());
            this.lblMaxArea.Text = string.Format("{0:F2} {1}²", maxArea, richPictureBox.UnitOfMeasure.ToString());
            this.lblAveArea.Text = string.Format("{0:F2} {1}²", aveArea, richPictureBox.UnitOfMeasure.ToString());
        }
    }
}