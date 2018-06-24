using CII.LAR.ExpClass;
using CII.LAR.MaterialSkin;
using CII.LAR.SysClass;
using DevComponents.DotNetBar;
using Manina.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class AssignedForm : MaterialForm
    {
        private AllPatients allPatients;
        public AssignedForm()
        {
            InitializeComponent();
            videoFiles = new List<string>();
            allPatients = Program.SysConfig.AllPatients;
            InitializeListView();
            this.Load += AssignedForm_Load;
        }

        private void AssignedForm_Load(object sender, EventArgs e)
        {
            string[] suggestion = GetFilesSuggestion();
            var source = new AutoCompleteStringCollection();
            source.AddRange(suggestion);
            this.toolStripTextBoxSelect.AutoCompleteCustomSource = source;
            this.toolStripTextBoxSelect.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.toolStripTextBoxSelect.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.toolStripTextBoxSelect.Visible = true;
        }

        private string[] GetFilesSuggestion()
        {
            string[] suggestion = new string[allPatients.Patients.Count];
            for (int i = 0; i<allPatients.Patients.Count; i++)
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

            // Resume layout logic.
            imageListView.ResumeLayout(true);
        }

        private void InitializeListView()
        {
            foreach (var p in allPatients.Patients)
            {
                ListViewItem lvi = new ListViewItem(new string[] { p.ID.ToString(), p.Name });
                lvi.Tag = p;
                this.listView.Items.Add(lvi);
            }
            if (this.listView.Items != null && this.listView.Items.Count > 0)
            {
                //listView.Items[0].Selected = true;
                listView.Items[0].Focused = true;
                this.listView.Items[0].Selected = true;
            }
            this.listView.Invalidate();
        }

        private VideoForm videoForm;
        private ImageForm imageForm;
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
                        imageForm = new ImageForm(false);
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

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView != null)
            {
                if (listView.SelectedItems != null && listView.SelectedItems.Count > 0)
                {
                    for (int i = 0; i< listView.SelectedItems.Count; i++)
                    {
                        if (listView.SelectedItems[i].Selected)
                        {
                            Patient p = listView.SelectedItems[i].Tag as Patient;
                            if (p != null) InitializeImageListView(p);
                            break;
                        }
                    }
                }

            }
        }
        private List<string> videoFiles;
        private void InitializeImageListView(Patient p)
        {
            //imageListView.Items.Remove();
            string folderName = Program.SysConfig.StorePath;
            string folder = string.Format("{0}\\{1}", folderName,  p.Foldername);
            string[] extesnsions = new string[] { ".png", ".avi" };
            var files = GetFiles(folder, extesnsions, SearchOption.TopDirectoryOnly);
            //this.imageListView.View = Manina.Windows.Forms.View.Thumbnails;
            if (files != null)
            {
                imageListView.Items.Clear();
                videoFiles.Clear();
                imageListView.ClearThumbnailCache();
                imageListView.SuspendLayout();
                foreach (var file in files)
                {
                    imageListView.Items.Add(file.ToString());
                    if (Path.GetExtension(file.ToString()) == ".avi")
                    {
                        videoFiles.Add(file.ToString());
                    }

                }
                imageListView.ResumeLayout(true);
                listView.Focus();
            }
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

        private Patient FindPatient(string checkFolder)
        {
            Patient findPatient = null;
            foreach (ListViewItem item in this.listView.Items)
            {
                Patient p = item.Tag as Patient;
                if (p != null)
                {
                    string folder = string.Format("{0}\\{1}", Program.SysConfig.StorePath, p.Foldername);
                    if (checkFolder == folder)
                    {
                        findPatient = p;
                        break;
                    }
                }
            }
            return findPatient;

        }
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            // Suspend the layout logic while we are removing items.
            // Otherwise the control will be refreshed after each item
            // is removed.
            imageListView.SuspendLayout();
            string folder = "";
            // Remove selected items
            foreach (var item in imageListView.SelectedItems)
            {
                folder = item.FilePath;
                imageListView.Items.Remove(item);
                if (File.Exists(item.FileName))
                {
                    try
                    {
                        File.Delete(item.FileName);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
            if (imageListView.Items.Count == 0)
            {
                try
                {
                    if (Directory.Exists(folder))
                    {
                        Directory.Delete(folder);
                    }
                    Patient p = FindPatient(folder);
                    if (p != null)
                    {
                        allPatients.Rremove(p);
                        RemovePatientFormListView(p);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            // Resume layout logic.
            imageListView.ResumeLayout(true);
        }

        private void RemovePatientFormListView(Patient p)
        {
            var list = this.listView.Items;
            ListViewItem listViewItem = null;
            foreach (ListViewItem item in this.listView.Items)
            {
                var patient = item.Tag as Patient;
                if (patient.ID == p.ID && patient.Name == p.Name)
                {
                    listViewItem = item;
                }
            }
            if (listViewItem != null ) this.listView.Items.Remove(listViewItem);
            this.listView.Invalidate();
        }
        public delegate void RestoreFile(List<string> files);
        public RestoreFile RestoreFileHandler;
        private List<string> restoreFiles;
        private void toolStripButtonRestore_Click(object sender, EventArgs e)
        {
            if (imageListView.SelectedItems != null && imageListView.SelectedItems.Count > 0)
            {
                restoreFiles = new List<string>();
                string folder = "";
                imageListView.SuspendLayout();
                foreach (var item in imageListView.SelectedItems)
                {
                    string destFileName = string.Format("{0}\\{1}", Program.SysConfig.StorePath, item.Text);
                    File.Copy(item.FileName, destFileName);
                    restoreFiles.Add(destFileName);
                    folder = item.FilePath;
                    imageListView.Items.Remove(item);
                    if (File.Exists(item.FileName))
                    {
                        try
                        {
                            File.Delete(item.FileName);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                if (imageListView.Items.Count == 0)
                {
                    try
                    {
                        if (Directory.Exists(folder))
                        {
                            Directory.Delete(folder);
                        }
                        Patient p = FindPatient(folder);
                        if (p != null)
                        {
                            allPatients.Rremove(p);
                            RemovePatientFormListView(p);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                RestoreFileHandler?.Invoke(restoreFiles);
                // Resume layout logic.
                imageListView.ResumeLayout(true);
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

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            CreateReportFrom();
        }
        private ReportForm reportFrom;
        private void CreateReportFrom()
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

        private void toolStripButtonDetail_Click(object sender, EventArgs e)
        {
            if (imageListView.SelectedItems != null && imageListView.SelectedItems.Count > 0)
            {
                string folder = "";
                folder = imageListView.SelectedItems[0].FilePath;
                Patient patient = FindPatient(folder);
                EditPatientDetailForm epdf = new EditPatientDetailForm(patient);
                epdf.ShowDialog();
            }
        }

        private void SetSelectedItem()
        {
            if (!string.IsNullOrEmpty(this.toolStripTextBoxSelect.Text))
            {
                try
                {
                    Patient patient = null;
                    int ID = int.Parse(this.toolStripTextBoxSelect.Text);
                    for (int i=0; i< this.listView.Items.Count; i++)
                    {
                        Patient p = this.listView.Items[i].Tag as Patient;
                        if (p != null && p.ID == ID)
                        {
                            patient = p;
                            this.listView.Items[i].Selected = true;
                        }
                        else
                        {
                            this.listView.Items[i].Selected = false;
                        }
                    }
                    if (patient != null) InitializeImageListView(patient);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            SetSelectedItem();
        }
    }
}
