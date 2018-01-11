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
        public AssignForm()
        {
            InitializeComponent();
            allPatients = AllPatients.GetAllPatients();
        }

        public AssignForm(ImageListViewItem imageListViewItem) : this()
        {
            this.imageListViewItem = imageListViewItem;
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
            string desFileFolder = string.Format("{0}\\{1}_{2}", imageListViewItem.FilePath,
                DateTime.Now.ToString("yyyyMMddHHmmsss"), patient.ID);
            if (!Directory.Exists(desFileFolder))
            {
                Directory.CreateDirectory(desFileFolder);
            }

            string destFileName = string.Format("{0}\\{1}", desFileFolder, imageListViewItem.Text);

            File.Copy(imageListViewItem.FileName, destFileName);
        }
    }
}