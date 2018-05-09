using CII.LAR.SysClass;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class EditPatientDetailForm : Office2007Form
    {
        private Patient patient;
        public EditPatientDetailForm()
        {
            InitializeComponent();
        }

        public EditPatientDetailForm(Patient patient) : this()
        {
            InitializePatientInfo(patient);
        }

        private void InitializePatientInfo(Patient patient)
        {
            if (patient != null)
            {
                this.patient = patient;
                this.textBoxPatientID.Text = patient.ID.ToString();
                this.textBoxPatientName.Text = patient.Name.ToString();
                if (!string.IsNullOrEmpty(patient.Detail)) this.textBoxComments.Text = patient.Detail;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (patient != null)
            {
                patient.Detail = this.textBoxComments.Text;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
