using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.ExpClass
{
    public class Report
    {
        private List<ReportPage> reportPages;
        public List<ReportPage> ReportPages
        {
            get
            {
                return reportPages;
            }
            set
            {
                reportPages = value;
            }
        }

        private float factor = 100;
        public float Factor
        {
            get
            {
                return factor;
            }
            set
            {
                factor = value;
            }
        }

        private bool landscape;
        public bool Landscape
        {
            get
            {
                return landscape;
            }
            set
            {
                landscape = value;
            }
        }

        public bool IsEmpty
        {
            get { return reportPages.All(page => page.ReportItems.Count <= 0); }
        }

        private int pageWidth;
        private int pageHeight;

        public Report()
        {
            reportPages = new List<ReportPage>();

            pageWidth = Landscape ? ReportForm.PAGE_HEIGHT : ReportForm.PAGE_WIDTH;
            pageHeight = Landscape ? ReportForm.PAGE_WIDTH : ReportForm.PAGE_HEIGHT;
        }
    }
}
