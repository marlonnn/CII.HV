using CII.LAR.MaterialSkin;
using CII.LAR.SysClass;
using DevComponents.DotNetBar;
using Manina.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public partial class SearchForm : MaterialForm
    {
        private Patient patient;
        public SearchForm()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            videoFiles = new List<string>();
        }

        public SearchForm(Patient patient)
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            videoFiles = new List<string>();
            this.patient = patient;
            this.Text = patient.ID.ToString();
            InitializeImageListView(patient);
        }

        private List<string> videoFiles;
        private void InitializeImageListView(Patient p)
        {
            //imageListView.Items.Remove();
            string folderName = Program.SysConfig.StorePath;
            string folder = string.Format("{0}\\{1}", folderName, p.Foldername);
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

        private void DeleteImageItemHandler(ImageListViewItem imageListViewItem)
        {
            imageListView.SuspendLayout();

            // Remove selected items
            imageListView.Items.Remove(imageListViewItem);

            // Resume layout logic.
            imageListView.ResumeLayout(true);
        }
    }
}
