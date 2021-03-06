using CII.LAR.ExpClass;
using CII.LAR.MaterialSkin;
using Manina.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    /// <summary>
    /// image viewer form
    /// Author: Zhong Wen 2017/08/10
    /// </summary>
    public partial class ImageForm : MaterialForm
    {
        private ImageListViewItem imageListViewItem;
        
        public ImageListViewItem ImageListViewItem
        {
            get
            {
                return imageListViewItem;
            }
            set
            {
                imageListViewItem = value;
            }
        }

        public delegate void DeleteImageItem(ImageListViewItem imageListViewItem);
        public DeleteImageItem DeleteImageItemHandler;

        private AssignForm assignForm;
        private Bitmap currentImage;

        private bool isAssign;

        public bool IsAssign
        {
            get
            {
                return isAssign;
            }
            set
            {
                isAssign = value;
            }
        }
        public Bitmap CurrentImage
        {
            get
            {
                return currentImage;
            }
            set
            {
                currentImage = value;
            }
        }

        private string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                if (value != this.fileName)
                {
                    this.fileName = value;
                    currentImage = new Bitmap(value);
                }
            }
        }

        public ImageForm()
        {
            InitializeComponent();
            this.isAssign = false;
            this.WindowState = FormWindowState.Maximized;
            this.Load += ImageForm_Load;
        }

        public ImageForm(bool toolstripVisiable) : this()
        {
            this.toolStrip1.Visible = toolstripVisiable;
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
            //this.TitleText = this.Title;
            this.pictureBox.Width = (int)(this.ClientSize.Width * 0.8f);
            this.pictureBox.Height = this.ClientSize.Height - 75;
            this.pictureBox.Left = (int)(this.ClientSize.Width * 0.1f);
            this.pictureBox.Top = 75;

            this.pictureBox.Image = currentImage;
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            string info = string.Format("{0}\n {1}", ImageListViewItem.Text, ImageListViewItem.DateModified);
            var result = MessageBox.Show(info, global::CII.LAR.Properties.Resources.StrDeleteFile, MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                //delete file
                if (this.pictureBox.Image != null)
                {
                    this.pictureBox.Image.Dispose();
                    this.pictureBox.Image = null;
                }
                DeleteImageItemHandler?.Invoke(ImageListViewItem);
                this.Close();
            }
            
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            StringCollection paths = new StringCollection();
            paths.Add(fileName);
            Clipboard.SetFileDropList(paths);
        }

        private void toolStripButtonAssign_Click(object sender, EventArgs e)
        {
            var items = new List<ImageListViewItem>();
            items.Add(imageListViewItem);
            assignForm = new AssignForm(items);
            if (assignForm.ShowDialog() == DialogResult.OK)
            {
                this.isAssign = true;
            }
            else
            {
                this.isAssign = false;
            }
        }
        private ReportForm reportFrom;

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.ReportPages.Capacity = 1;
            ReportPage reportPage = new ReportPage();
            ReportPictureItem reportItem = new ReportPictureItem();
            reportItem.Picture = CurrentImage;
            reportItem.OldImageSize = reportItem.Picture.Size;
            reportItem.Bounds = new Rectangle(new Point(0, 0), reportItem.Picture.Size);
            reportPage.ReportItems.Add(reportItem);
            report.ReportPages.Add(reportPage);
            reportFrom = new ReportForm(report);
            reportFrom.Show();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.pictureBox.Image != null)
            {
                this.pictureBox.Image.Dispose();
                this.pictureBox.Image = null;
            }
            base.OnClosing(e);
            this.DialogResult = DialogResult.OK;
        }

    }
}