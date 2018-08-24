using CII.LAR.ExpClass;
using CII.LAR.MaterialSkin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class ReportForm : MaterialForm
    {
        private Report report;
        public Report Report
        {
            get { return report; }
            private set
            {
                if (report == value)
                {
                    return;
                }
                report = value;
            }
        }

        private List<ReportPageUI> pages;

        public static int PAGE_WIDTH = 827;
        public static int PAGE_HEIGHT = 1169;
        private const int pagesSpace = 10;
        private const int pagesLeftSpace = 5;
        private int pagesInOnline = 1;
        private int pageWidth;
        private int pageHeight;
        private int pageLeft;
        private int pageTop;

        private PrintDocument printDocument;
        private int pagesIndex = 0;
        private int lastPrintPage = 0; // use can select a page rang to print
        private bool isPDFPrint = false;
        private bool isShowPrintDialog = false;
        private bool isPrinting = false;
        private PrintPreviewDialog MyPrintPreviewDg;
        private Timer timer;
        private string printDocName = "Cii Lar-100 Report";

        public ReportForm()
        {
            InitializeComponent();
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.BeginPrint += new PrintEventHandler(printDocument_BeginPrint);
            printDocument.EndPrint += new PrintEventHandler(printDocument_EndPrint);
            printDocument.PrintController = new StandardPrintController();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(Time_Tick);
        }

        public ReportForm(Report report) : this()
        {
            Report = report;
            pages = new List<ReportPageUI>();
            Report.Factor = 100f;
            LoadForm();
        }

        private void LoadForm()
        {
            //SetReportName();
            InitialPageSize();
            pages.Clear();
            RemoveReportControl();
            if (report.ReportPages.Count > 0)
            {
                for (int i = 0; i < report.ReportPages.Count; i++)
                {
                    ReportPageUI pageUI = new ReportPageUI(report.ReportPages[i]);
                    if (i == 0) //开始时设置第一页为当前页
                    {
                        pageUI.IsSelected = true;
                        this.tslPages.Text = "Page: 1/" + report.ReportPages.Count.ToString();
                    }
                    SetPageBounds(pageUI, i + 1);
                    //   pageUI.Top += this.toolStrip.Height;                   
                    pages.Add(pageUI);
                    this.reportLayout.Controls.Add(pageUI);
                }
            }
            else
            {
                NewReportPage();
            }
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            PrintButtonClick();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            PrintPage(e);
        }

        private void printDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            IsComplete = false;

            // set start page and end page
            if (printDocument.PrinterSettings.PrintRange == PrintRange.CurrentPage)
            {
                int selectedPage = 0;
                for (int i = 0; i < pages.Count; i++) if (pages[i].IsSelected) selectedPage = i;
                pagesIndex = selectedPage;
                lastPrintPage = selectedPage;
            }
            else if (printDocument.PrinterSettings.PrintRange == PrintRange.SomePages)
            {
                int from = printDocument.PrinterSettings.FromPage;
                int to = printDocument.PrinterSettings.ToPage;
                if (from <= 0) from = 1;
                if (from > pages.Count) from = pages.Count;
                if (to < from) to = from;
                if (to > pages.Count) to = pages.Count;

                pagesIndex = from - 1;
                lastPrintPage = to - 1;
            }
            else
            {
                pagesIndex = 0;
                lastPrintPage = pages.Count - 1;
            }

            if (isShowPrintDialog && !isPDFPrint)
            {
                PrintDialog MyPrintDg = new PrintDialog();
                MyPrintDg.UseEXDialog = true;
                MyPrintDg.Document = printDocument;
                printDocument.DefaultPageSettings.Landscape = report.Landscape;
                try { printDocument.DefaultPageSettings.PaperSize.RawKind = (int)PaperKind.A4; }
                catch { }   // will throw exception when no default printer is set
                if (MyPrintDg.ShowDialog() == DialogResult.OK)
                {

                    if (MyPrintPreviewDg != null)
                    {
                        MyPrintPreviewDg.Dispose();
                    }
                }
                else
                {
                    e.Cancel = true;
                }
                MyPrintDg.Dispose();
            }
        }

        public bool IsComplete
        {
            set;
            get;
        }

        private void printDocument_EndPrint(object sender, PrintEventArgs e)
        {
            if (!isPDFPrint)
                isShowPrintDialog = true;

            IsComplete = true;
        }

        private void PrintPage(PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            if (pagesIndex > lastPrintPage) return;

            if (pagesIndex % 27 == 0)
            { //if pages index is too large, refresh report
                reportLayout.AutoScrollPosition = new Point(0, -(pagesIndex * 1169));
                AdjustPagePosition();
                //ReportOperateManage.SetSeletectedPage();
            }

            PrintReport(g, pages[pagesIndex]);
            //   PrintFooter(g);

            if (pagesIndex == lastPrintPage)
            {
                e.HasMorePages = false;
                pagesIndex = 0;
            }
            else
            {
                e.HasMorePages = true;
                pagesIndex++;
            }
        }

        private void PrintReport(Graphics g, ReportPageUI reportPageUI)
        {
            isPrinting = true;
            this.Invoke(new Action(() =>
            {
                //reportPageUI.DrawPageHeadAndTail(g);

                for (int i = reportPageUI.Controls.Count - 1; i >= 0; i--)
                {
                    ReportCtrl rCtrl = reportPageUI.Controls[i] as ReportCtrl;
                    if (rCtrl != null)
                    {
                        rCtrl.IsPrint = true;
                        rCtrl.Draw(g, rCtrl.ReportItem.Bounds);
                        rCtrl.IsPrint = false;
                    }
                }
            }));
            isPrinting = false;
        }


        private void NewReportPage()
        {
            ReportPage reportPage = new ReportPage();
            ReportPageUI NewPageUI = new ReportPageUI(reportPage);
            NewPageUI.ReportCtrlChanged += new EventHandler<ArrayChangedEventArgs<ReportCtrl>>(pageUI_ReportCtrlChanged);
            SetPageBounds(NewPageUI, report.ReportPages.Count + 1);
            report.ReportPages.Add(reportPage);
            pages.Add(NewPageUI);
            this.reportLayout.Controls.Add(NewPageUI);
            reportLayout.AutoScrollPosition = new Point(0, -reportLayout.AutoScrollPosition.Y + 1169);
            //ReportOperateManage.SetSeletectedPage();
        }

        private void pageUI_ReportCtrlChanged(object sender, ArrayChangedEventArgs<ReportCtrl> e)
        {
            if (e.ChangeType == ArrayChangedType.ItemAdded)
            {
                ReportCtrl rc = e.Item;
                ZooomReportControl(rc);
            }
        }

        private void ZooomReportControl(ReportCtrl rc)
        {
            float coefficent = report.Factor / 100;
            ZoomControl(rc, coefficent);
        }

        private void ZoomControl(ReportCtrl rc, float coefficent)
        {
            rc.Width = (int)(rc.ReportItem.Bounds.Width * coefficent);
            rc.Height = (int)(rc.ReportItem.Bounds.Height * coefficent);
            rc.Location = new Point((int)(rc.ReportItem.Bounds.Location.X * coefficent), 
                (int)(rc.ReportItem.Bounds.Location.Y * coefficent));
            rc.Scale(report.Factor / 100);
        }

        private void SetPageBounds(ReportPageUI page, int pageNo)
        {
            int top = this.reportLayout.AutoScrollPosition.Y;
            int left = this.reportLayout.AutoScrollPosition.X;
            pagesInOnline = this.Width / pageWidth < 1 ? 1 : this.Width / pageWidth; // calculate how many pages display in one line

            if (pagesInOnline > 1)
                if (this.Width < (pageWidth * pagesInOnline) + pagesLeftSpace * (pagesInOnline - 1)) pagesInOnline -= 1;
            // calculate page belong to which column.
            int pageColumn = (pageNo % pagesInOnline) == 0 ? pagesInOnline : pageNo % pagesInOnline; // data from 1;
            int firstPageLeft = (this.Width - ((pageWidth * pagesInOnline) + pagesLeftSpace * (pagesInOnline - 1))) / 2 <= 0 ? 1
                : (this.Width - ((pageWidth * pagesInOnline) + pagesLeftSpace * (pagesInOnline - 1))) / 2;
            int pageLeft = firstPageLeft + ((pageColumn - 1) * (pageWidth + pagesLeftSpace)) + left;
            // calculate page top
            pageNo = (int)(Math.Ceiling((pageNo / (double)pagesInOnline)));         //pageNo data from 1;   
            int pageTop = (pageHeight * (pageNo - 1) + pagesSpace * (pageNo + 1) + top);

            page.Bounds = new Rectangle(new Point(pageLeft, pageTop),
                new Size(pageWidth, pageHeight));

            pnlSpace.Location = new Point(pageLeft, pageTop + pageHeight);
            pnlSpace.SendToBack();

        }

        private void InitialPageSize()
        {

            float coefficent = report.Factor / 100;

            if (report.Landscape)
            {
                pageHeight = (int)(PAGE_WIDTH * coefficent);
                pageWidth = (int)(PAGE_HEIGHT * coefficent);
            }
            else
            {
                pageHeight = (int)(PAGE_HEIGHT * coefficent);
                pageWidth = (int)(PAGE_WIDTH * coefficent);
            }

            pageLeft = (this.Width - pageWidth) / 2;
            pageTop = pagesSpace;
            //  g.Dispose();
        }

        private void RemoveReportControl()
        {
            List<Control> ctrs = new List<Control>();
            foreach (Control c in this.reportLayout.Controls)
            {
                if (c is ReportPageUI)
                {
                    ctrs.Add(c);
                }
            }
            foreach (Control c in ctrs)
            {
                this.reportLayout.Controls.Remove(c);
                c.Dispose();
            }
        }

        public double Coefficient
        {
            get
            {
                return report.Factor / 100;
            }
        }

        private void tssbZoom_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi != null)
            {
                UncheckZoomItem();
                tsmi.Checked = true;
                this.report.Factor = Convert.ToSingle(tsmi.Tag.ToString());

                Zoom();
            }
        }

        private void UncheckZoomItem()
        {
            tsmi500.Checked = false;
            tsmi200.Checked = false;
            tsmi150.Checked = false;
            tsmi100.Checked = false;
        }

        private void Zoom()
        {
            float coefficent = report.Factor / 100;

            if (report.Landscape)
            {
                this.pageHeight = (int)(PAGE_WIDTH * coefficent);
                this.pageWidth = (int)(PAGE_HEIGHT * coefficent);
            }
            else
            {
                this.pageHeight = (int)(PAGE_HEIGHT * coefficent);
                this.pageWidth = (int)(PAGE_WIDTH * coefficent);
            }

            AdjustPagePosition();

            foreach (ReportPageUI rpu in pages)
            {
                foreach (Control con in rpu.Controls)
                {
                    ReportCtrl rc = con as ReportCtrl;
                    if (rc != null)
                    {
                        ZoomControl(rc, coefficent);
                    }
                }
            }

            UncheckZoomItem();
            GetZoomTool((int)report.Factor).Checked = true;
        }

        private void AdjustPagePosition()
        {
            for (int i = 0; i < pages.Count; i++)
            {
                SetPageBounds(pages[i], i + 1);
                foreach (var item in pages[i].Controls)
                {
                    ReportCtrl rc = item as ReportCtrl;
                    if (rc != null)
                    {
                        rc.ResetSubCtrlLocation();
                    }
                }
            }
        }

        private ToolStripMenuItem GetZoomTool(int factor)
        {
            ToolStripMenuItem item = null;
            switch (factor)
            {
                case 500:
                    item = tsmi500;
                    break;
                case 200:
                    item = tsmi200;
                    break;
                case 150:
                    item = tsmi150;
                    break;
                case 100:
                    item = tsmi100;
                    break;
            }
            return item;
        }

        private void toolStripButtonPreview_Click(object sender, EventArgs e)
        {
            pagesIndex = 0;
            isPDFPrint = false;
            isShowPrintDialog = false;
            MyPrintPreviewDg = new PrintPreviewDialog();
            MyPrintPreviewDg.ShowIcon = false;
            MyPrintPreviewDg.UseAntiAlias = true;
            try
            {
                printDocument.DefaultPageSettings.PaperSize.RawKind = (int)PaperKind.A4;
            }
            catch
            {
            } // will throw exception when no default printer is set
            printDocument.DefaultPageSettings.Landscape = report.Landscape;
            MyPrintPreviewDg.Document = printDocument;
            try
            {
                MyPrintPreviewDg.ShowDialog();
                MyPrintPreviewDg.Dispose();
            }
            catch
            {
                printDocument.PrintController.OnEndPrint(printDocument, new System.Drawing.Printing.PrintEventArgs());
            }
        }

        /// <summary>
        /// print button click
        /// </summary>
        public void PrintButtonClick()
        {
            pagesIndex = 0;
            isPDFPrint = false;
            isShowPrintDialog = false;
            PrintDialog MyPrintDg = new PrintDialog();
            MyPrintDg.UseEXDialog = true;
            MyPrintDg.AllowSomePages = true;
            MyPrintDg.AllowCurrentPage = true;
            MyPrintDg.Document = printDocument;
            printDocument.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            printDocument.OriginAtMargins = true;  // ignore printer hardware margin

            MyPrintDg.PrinterSettings.FromPage = 1;
            MyPrintDg.PrinterSettings.ToPage = pages.Count;
            printDocument.DefaultPageSettings.Landscape = report.Landscape;
            try
            {
                printDocument.DefaultPageSettings.PaperSize.RawKind = (int)PaperKind.A4;
            }
            catch
            {
            }   // will throw exception when no default printer is set
            printDocument.DocumentName = printDocName;
            if (MyPrintDg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printDocument.Print();
                }
                catch
                {

                    printDocument.PrintController.OnEndPrint(printDocument, new System.Drawing.Printing.PrintEventArgs());
                }
            }
            MyPrintDg.Dispose();
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}