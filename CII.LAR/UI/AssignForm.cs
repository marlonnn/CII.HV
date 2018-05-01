using CII.LAR.SysClass;
using DevComponents.DotNetBar;
using Manina.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    /// <summary>
    /// Assign form
    /// Author: Zhong Wen 2017/08/17
    /// </summary>
    public partial class AssignForm : Office2007Form
    {
        private AllPatients allPatients;

        private List<ImageListViewItem> imageListViewItems;

        public List<ImageListViewItem> ImageListViewItems
        {
            get
            {
                return imageListViewItems;
            }
            set
            {
                imageListViewItems = value;
            }
        }
        public AssignForm()
        {
            InitializeComponent();
            allPatients = Program.SysConfig.AllPatients;
        }

        public AssignForm(List<ImageListViewItem> imageListViewItems) : this()
        {
            this.imageListViewItems = imageListViewItems;
            allPatients = Program.SysConfig.AllPatients;
        }

        private bool CheckTextBoxValided(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            return true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Patient patient = new Patient(Int32.Parse(this.textBoxPatientID.Text), this.textBoxPatientName.Text);
                allPatients.Add(patient);
                CheckMoveToFolder(patient);
                this.Close();
            }
            catch (Exception ex)
            {
                LogHelper.GetLogger<AssignForm>().Error(ex.Message);
                LogHelper.GetLogger<AssignForm>().Error(ex.StackTrace);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.DialogResult = DialogResult.OK;
        }

        private void CheckMoveToFolder(Patient patient)
        {
            if (imageListViewItems != null && imageListViewItems.Count > 0)
            {
                SuspendImageListViewHandler?.Invoke();
                foreach (var imageListViewItem in imageListViewItems)
                {
                    try
                    {
                        string desFileFolder = string.Format("{0}\\{1}", imageListViewItem.FilePath, patient.Foldername);
                        if (!Directory.Exists(desFileFolder))
                        {
                            Directory.CreateDirectory(desFileFolder);
                        }

                        string destFileName = string.Format("{0}\\{1}", desFileFolder, imageListViewItem.Text);

                        File.Copy(imageListViewItem.FileName, destFileName);
                        DeleteImageListViewiTemHandler?.Invoke(imageListViewItem);
                        File.Delete(imageListViewItem.FileName);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                ResumeImageListViewHandler?.Invoke();
            }
        }

        public delegate void SuspendImageListView();
        public SuspendImageListView SuspendImageListViewHandler;

        public delegate void DeleteImageListViewiTem(ImageListViewItem item);
        public DeleteImageListViewiTem DeleteImageListViewiTemHandler;

        public delegate void ResumeImageListView();
        public ResumeImageListView ResumeImageListViewHandler;
        //ResumeLayout
    }
}