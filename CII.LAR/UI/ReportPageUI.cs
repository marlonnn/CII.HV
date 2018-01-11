using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.ExpClass;

namespace CII.LAR.UI
{
    public partial class ReportPageUI : UserControl
    {
        private Color borderColor = Color.Tomato;

        private bool isSelected;
        public bool IsSelected
        {
            set
            {
                if (!Enabled && value) return;//if is in showing head and foot model,return.
                isSelected = value;
                if (isSelected)
                {
                    borderColor = Color.Tomato;
                }
                else
                {
                    borderColor = Color.Black;
                }
                this.Invalidate();
            }
            get
            {
                return isSelected;
            }
        }

        private ReportPage reportPage;

        public ReportPage ReportPage
        {
            private set
            {
                this.reportPage = value;
            }
            get
            {
                return this.reportPage;
            }
        }

        public event EventHandler<ArrayChangedEventArgs<ReportCtrl>> ReportCtrlChanged;

        private void OnReportCtrlChanged(ReportCtrl temp, ArrayChangedType type)
        {
            ReportCtrlChanged?.Invoke(this, new ArrayChangedEventArgs<ReportCtrl>(temp, type));
        }

        public ReportPageUI()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        public ReportPageUI(ReportPage reportPage) : this()
        {
            this.reportPage = reportPage;
            foreach (ReportItemBase reportItem in reportPage.ReportItems)
            {
                AddReportControl(reportItem, -1);
            }
        }

        private void AddReportControl(ReportItemBase reportItem, double factor)
        {
            ReportCtrl reportCtrl = null;
            if (reportItem is ReportPictureItem)
            {
                reportCtrl = new ReportCtrlPicture((ReportPictureItem)reportItem);
            }
            reportCtrl.Bounds = reportItem.Bounds;
            if (factor > 0)
            {
                reportPage.ReportItems.Add(reportItem);
                // _reportPage.ReportItems.Insert(0,reportItemBase);
            }
            this.Controls.Add(reportCtrl);

            try
            {
                this.Controls.SetChildIndex(reportCtrl, reportItem.Level);
            }
            catch
            {

            }
            reportCtrl.Scale(factor);

            if (factor > 0)
            {
                OnReportCtrlChanged(reportCtrl, ArrayChangedType.ItemAdded);
            }
        }

        private void ReportPageUI_Load(object sender, EventArgs e)
        {

        }
    }
}
