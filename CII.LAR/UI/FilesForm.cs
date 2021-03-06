using CII.LAR.UI;
using CII.LAR.ExpClass;
using Manina.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CII.LAR.SysClass;
using System.Collections.Specialized;
using CII.LAR.MaterialSkin;

namespace CII.LAR
{
    public partial class FilesForm : MaterialForm
    {
        private AssignForm assignForm;
        private ReportForm reportFrom;
        //private VideoForm videoForm;
        private List<string> videoFiles;

        private AllPatients allPatients;
        public FilesForm()
        {
            //this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            videoFiles = new List<string>();
            string folderName = Program.SysConfig.StorePath;
            string[] extesnsions = new string[] { ".png", ".avi" };
            var files = GetFiles(folderName, extesnsions, SearchOption.TopDirectoryOnly);
            //this.imageListView.View = Manina.Windows.Forms.View.Thumbnails;
            foreach (var file in files)
            {
                imageListView.Items.Add(file.ToString());
                if (Path.GetExtension(file.ToString()) == ".avi")
                {
                    videoFiles.Add(file.ToString());
                }
            }
            allPatients = Program.SysConfig.AllPatients;
            this.Load += FilesForm_Load;
        }

        private void SetSelectedItem()
        {
            if (!string.IsNullOrEmpty(this.toolStripTextBox1.Text))
            {
                imageListView.SuspendLayout();
                for (int i = 0; i < imageListView.Items.Count; i++)
                {
                    if (imageListView.Items[i].Text == this.toolStripTextBox1.Text)
                    {
                        imageListView.Items[i].Selected = true;
                    }
                    else
                    {
                        imageListView.Items[i].Selected = false;
                    }
                }
                imageListView.ResumeLayout(true);
            }
        }

        private void FilesForm_Load(object sender, EventArgs e)
        {
            var suggestion = GetFilesSuggestion();
            var source = new AutoCompleteStringCollection();
            source.AddRange(suggestion);
            this.toolStripTextBox1.AutoCompleteCustomSource = source;
            this.toolStripTextBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.toolStripTextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.toolStripTextBox1.Visible = true;
        }


        private string[] GetFilesSuggestion()
        {
            //string folderName = Program.SysConfig.StorePath;
            //string[] extesnsions = new string[] { ".png", ".avi" };
            //var files = GetFiles(folderName, extesnsions, SearchOption.TopDirectoryOnly);
            //string[] suggestion = new string[imageListView.Items.Count];

            //for (int i=0; i< imageListView.Items.Count; i++)
            //{
            //    suggestion[i] = imageListView.Items[i].Text;
            //}
            string[] suggestion = new string[allPatients.Count];
            for (int i=0; i<allPatients.Count; i++)
            {
                suggestion[i] = allPatients.Patients[i].ID.ToString();
            }
            return suggestion;
        }

        private void DeleteImageItemHandler(ImageListViewItem imageListViewItem)
        {
            imageListView.SuspendLayout();

            // Remove selected items
            imageListView.Items.Remove(imageListViewItem);
            if (File.Exists(imageListViewItem.FileName))
            {
                try
                {
                    File.Delete(imageListViewItem.FileName);
                }
                catch (Exception ex)
                {
                }
            }
            // Resume layout logic.
            imageListView.ResumeLayout(true);
        }

        /// <summary>
        /// Get files form directory
        /// </summary>
        /// <param name="sourceDirectory">source directory</param>
        /// <param name="exts">extensions</param>
        /// <param name="searchOpt">search option</param>
        /// <returns></returns>
        private IEnumerable GetFiles(string sourceDirectory, string[] exts, SearchOption searchOpt)
        {
            return Directory.GetFiles(sourceDirectory, "*.*", searchOpt)
                    .Where(
                inS => exts.Contains(System.IO.Path.GetExtension(inS),
                StringComparer.OrdinalIgnoreCase)
                           );
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (imageListView.SelectedItems.Count == 0)
            {
                MsgBox.Show(Properties.Resources.StrConfirmToSelect, Properties.Resources.StrWaring,
                    MsgBox.Buttons.OK, MsgBox.Icon.Warning);
            }
            else
            {
                if (MsgBox.Show(Properties.Resources.StrConfirmToDelete, Properties.Resources.StrWaring,
                    MsgBox.Buttons.YesNo, MsgBox.Icon.Warning) == DialogResult.Yes)
                {
                    // Suspend the layout logic while we are removing items.
                    // Otherwise the control will be refreshed after each item
                    // is removed.
                    imageListView.SuspendLayout();

                    // Remove selected items
                    foreach (var item in imageListView.SelectedItems)
                    {
                        imageListView.Items.Remove(item);
                        if (File.Exists(item.FileName))
                        {
                            try
                            {
                                new FileInfo(item.FileName).Attributes = FileAttributes.Normal;
                                File.Delete(item.FileName);
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                    }

                    // Resume layout logic.
                    imageListView.ResumeLayout(true);
                }
            }
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            CreateReportFrom();
        }

        private void CreateReportFrom()
        {
            if (imageListView.SelectedItems != null && imageListView.SelectedItems.Count == 0)
            {
                DialogResult result = MsgBox.Show(Properties.Resources.StrCannotPrintEmpty, Properties.Resources.StrWarning, MsgBox.Buttons.OK, MsgBox.Icon.Warning);
            }
            else
            {
                if (CanPrint())
                {
                    if (imageListView.SelectedItems != null && imageListView.SelectedItems.Count > 0)
                    {
                        Report report = new Report();
                        report.ReportPages.Capacity = imageListView.SelectedItems.Count;
                        for (int i = 0; i < imageListView.SelectedItems.Count; i++)
                        {
                            var fileExtension = Path.GetExtension(imageListView.SelectedItems[i].FileName);
                            if (fileExtension == ".png")
                            {
                                ReportPage reportPage = new ReportPage();
                                ReportPictureItem reportItem = new ReportPictureItem();
                                string fileName = imageListView.SelectedItems[i].FileName;
                                reportItem.Picture = new Bitmap(fileName);
                                reportItem.OldImageSize = reportItem.Picture.Size;
                                reportItem.Bounds = new Rectangle(new Point(0, 0), reportItem.Picture.Size);
                                reportPage.ReportItems.Add(reportItem);
                                report.ReportPages.Add(reportPage);
                            }
                        }
                        reportFrom = new ReportForm(report);
                        reportFrom.ShowDialog();
                    }
                }
                else
                {
                    DialogResult result = MsgBox.Show(Properties.Resources.StrCannotPrint, Properties.Resources.StrWarning, MsgBox.Buttons.OK, MsgBox.Icon.Warning);
                }
            }
        }

        private bool CanPrint()
        {
            bool canPrint = false;
            List<string> fileExtensions = new List<string>();
            if (imageListView.SelectedItems != null && imageListView.SelectedItems.Count > 0)
            {
                for (int i = 0; i < imageListView.SelectedItems.Count; i++)
                {
                    var fileExtension = Path.GetExtension(imageListView.SelectedItems[i].FileName);
                    fileExtensions.Add(fileExtension);
                }
                if (fileExtensions != null && fileExtensions.Count > 0)
                {
                    var aviFiles = fileExtensions.Where(f => f == ".avi");
                    if (aviFiles.Count() != imageListView.SelectedItems.Count)
                    {
                        canPrint = true;
                    }
                }
            }
            return canPrint;
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
            timerStatus.Enabled = false;
        }

        private void imageListView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (imageListView.Items.Count == 0)
                UpdateStatus("Ready");
            else if (imageListView.SelectedItems.Count == 0)
                UpdateStatus(string.Format("{0} images", imageListView.Items.Count));
            else
                UpdateStatus(string.Format("{0} images ({1} selected)", imageListView.Items.Count, 
                    imageListView.SelectedItems.Count));
        }

        private void UpdateStatus(string text)
        {
            //toolStripStatusLabel.Text = text;
        }

        private ImageForm imageForm;
        private VideoForm videoForm;

        private void imageListView_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ImageListViewItem item = this.imageListView.Items.FocusedItem;
                if (item != null)
                {
                    var fileExtension = Path.GetExtension(item.FileName);
                    if (fileExtension == ".avi")
                    {
                        string fileName = item.FileName;
                        int v = videoFiles.FindIndex(file => { return file == fileName; });
                        videoForm = new VideoForm(videoFiles, fileName);
                        videoForm.ShowDialog();
                    }
                    else if (fileExtension == ".png")
                    {
                        string fileName = item.FileName;
                        imageForm = new ImageForm();
                        imageForm.DeleteImageItemHandler += DeleteImageItemHandler;
                        imageForm.Text = item.Text;
                        imageForm.ImageListViewItem = item;
                        imageForm.FileName = fileName;
                        DialogResult dr = imageForm.ShowDialog();
                        if (dr == DialogResult.OK && imageForm.IsAssign)
                        {
                            DeleteImageItemHandler(item);
                            File.Delete(item.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.GetLogger<FilesForm>().Error(ex.Message);
                LogHelper.GetLogger<FilesForm>().Error(ex.StackTrace);
            }
        }

        private void RemoveAndDelete(ImageListViewItem item)
        {

        }

        private void imageListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ImageListViewItem item = this.imageListView.Items.FocusedItem;
            //if (item != null)
            //{
            //    string fileName = item.FileName;
            //    imageForm.FileName = fileName;
            //    imageForm.ShowDialog();
            //}
        }

        private void toolStripButtonAssign_Click(object sender, EventArgs e)
        {
            if (this.imageListView.SelectedItems != null && this.imageListView.SelectedItems.Count > 0)
            {
                assignForm = new AssignForm(GetSelectedImageListViewItems());
                assignForm.SuspendImageListViewHandler += SuspendImageListViewHandler;
                assignForm.DeleteImageListViewiTemHandler += DeleteImageListViewiTemHandler;
                assignForm.ResumeImageListViewHandler += ResumeImageListViewHandler;
                assignForm.Show();
            }
        }

        private void DeleteImageListViewiTemHandler(ImageListViewItem item)
        {
            imageListView.Items.Remove(item);
        }

        private void SuspendImageListViewHandler()
        {
            this.imageListView.SuspendLayout();
        }

        private void ResumeImageListViewHandler()
        {
            this.imageListView.ResumeLayout(true);
        }

        private List<ImageListViewItem> GetSelectedImageListViewItems()
        {
            var items = new List<ImageListViewItem>();
            if (this.imageListView.SelectedItems != null && this.imageListView.SelectedItems.Count > 0)
            {
                foreach (var item in this.imageListView.SelectedItems)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            var items = this.imageListView.Items;
            StringCollection paths = new StringCollection();
            foreach (var item in items)
            {
                if (item != null && item.Selected)
                {
                    string fileName = item.FileName;
                    paths.Add(fileName);
                }
            }
            if (paths.Count > 0)
            {
                Clipboard.SetFileDropList(paths);
            }
        }
        private AssignedForm assignedForm;
        private void tsbAssigned_Click(object sender, EventArgs e)
        {
            assignedForm = new AssignedForm();
            assignedForm.RestoreFileHandler += RestoreFileHandler;
            assignedForm.ShowDialog();
        }

        private void RestoreFileHandler(List<string> files)
        {
            if (files != null &&files.Count > 0)
            {
                imageListView.SuspendLayout();
                foreach (string file in files)
                {
                    imageListView.Items.Add(file);
                }
                imageListView.ResumeLayout(true);
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            //SetSelectedItem();
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            //SetSelectedItem();
            Patient patient = FindPatient();
            if (patient != null)
            {
                SearchForm sf = new SearchForm(patient);
                sf.ShowDialog();
            }
        }

        private Patient FindPatient()
        {
            Patient patient = null;
            if (!string.IsNullOrEmpty(toolStripTextBox1.Text))
            {
                try
                {
                    int id = int.Parse(toolStripTextBox1.Text);
                    for (int i =0; i<Program.SysConfig.AllPatients.Count; i++)
                    {
                        if (allPatients.Patients[i].ID == id)
                        {
                            patient = allPatients.Patients[i];
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return patient;
        }
    }
}