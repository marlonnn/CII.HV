using CII.LAR.MaterialSkin;
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
    public partial class AssignForm : MaterialForm
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

        protected override void OnLoad(EventArgs e)
        {
            var suggestion = GetPatientSuggestion();
            var source = new AutoCompleteStringCollection();
            source.AddRange(suggestion);
            this.textBoxPatientID.AutoCompleteCustomSource = source;
            this.textBoxPatientID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.textBoxPatientID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.textBoxPatientID.Visible = true;
        }

        private string[] GetPatientSuggestion()
        {
            string[] suggestion = new string[allPatients.Count];
            for (int i = 0; i< allPatients.Patients.Count; i++)
            {
                suggestion[i] = allPatients.Patients[i].ID.ToString();
            }
            return suggestion;
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
                this.superValidator.SetValidator1(this.textBoxPatientName, this.requiredFieldValidator2);
                this.textBoxPatientName.CausesValidation = true;
                if (this.superValidator.Validate(this.textBoxPatientName, true))
                {
                    Patient patient = new Patient(Int32.Parse(this.textBoxPatientID.Text), this.textBoxPatientName.Text);
                    allPatients.Add(patient);
                    CheckMoveToFolder(patient);
                    this.Close();
                }
                //this.superValidator.Validate(this.textBoxPatientName,true);
                this.superValidator.SetValidator1(this.textBoxPatientName, this.requiredFieldValidator2);
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

        private void textBoxPatientID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var id = int.Parse(this.textBoxPatientID.Text);
                string name = TryFindPatientName(id);
                if (name != null) this.textBoxPatientName.Text = name;

                if (allPatients.Patients.Count == 0)
                {
                    this.textBoxPatientName.Enabled = true;
                }
                else
                {
                    var patientId = Int32.Parse(this.textBoxPatientID.Text);
                    var findPatient = allPatients.Patients.Find(p => p.ID == patientId);
                    this.textBoxPatientName.Enabled = findPatient == null;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private string TryFindPatientName(int id)
        {
            string name = "";
            for (int i = 0; i < allPatients.Patients.Count; i++)
            {
                if (id == allPatients.Patients[i].ID)
                {
                    name = allPatients.Patients[i].Name;
                    break;
                } 
            }
            return name;
        }
        //ResumeLayout
    }
}