using CII.LAR.MaterialSkin;

namespace CII.LAR.UI
{
    partial class AssignForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssignForm));
            this.lblPatientID = new MaterialLabel();
            this.textBoxPatientID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblPatientName = new MaterialLabel();
            this.textBoxPatientName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnConfirm = new MaterialRoundButton();
            this.btnCancel = new MaterialRoundButton();
            this.requiredFieldValidator2 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("Patient name should not empty.");
            this.regularExpressionValidator1 = new DevComponents.DotNetBar.Validator.RegularExpressionValidator();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.highlighter = new DevComponents.DotNetBar.Validator.Highlighter();
            this.superValidator = new DevComponents.DotNetBar.Validator.SuperValidator();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPatientID
            // 
            // 
            // 
            // 
            resources.ApplyResources(this.lblPatientID, "lblPatientID");
            this.lblPatientID.Name = "lblPatientID";
            // 
            // textBoxPatientID
            // 
            this.textBoxPatientID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.textBoxPatientID.Border.Class = "TextBoxBorder";
            this.textBoxPatientID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxPatientID.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.textBoxPatientID, "textBoxPatientID");
            this.textBoxPatientID.Name = "textBoxPatientID";
            this.superValidator.SetValidator1(this.textBoxPatientID, this.regularExpressionValidator1);
            this.textBoxPatientID.TextChanged += new System.EventHandler(this.textBoxPatientID_TextChanged);
            // 
            // lblPatientName
            // 
            // 
            // 
            // 
            resources.ApplyResources(this.lblPatientName, "lblPatientName");
            this.lblPatientName.Name = "lblPatientName";
            // 
            // textBoxPatientName
            // 
            this.textBoxPatientName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.textBoxPatientName.Border.Class = "TextBoxBorder";
            this.textBoxPatientName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxPatientName.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.textBoxPatientName, "textBoxPatientName");
            this.textBoxPatientName.Name = "textBoxPatientName";
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnConfirm, "btnConfirm");
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // requiredFieldValidator2
            // 
            resources.ApplyResources(this.requiredFieldValidator2, "requiredFieldValidator2");
            this.requiredFieldValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            // 
            // regularExpressionValidator1
            // 
            resources.ApplyResources(this.regularExpressionValidator1, "regularExpressionValidator1");
            this.regularExpressionValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.regularExpressionValidator1.ValidationExpression = "^[0-9]*$";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // highlighter
            // 
            this.highlighter.ContainerControl = this;
            this.highlighter.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // superValidator
            // 
            this.superValidator.ContainerControl = this;
            this.superValidator.ErrorProvider = this.errorProvider;
            this.superValidator.Highlighter = this.highlighter;
            this.superValidator.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.superValidator.ValidationType = DevComponents.DotNetBar.Validator.eValidationType.ValidatingEventPerControl;
            // 
            // AssignForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.textBoxPatientName);
            this.Controls.Add(this.lblPatientName);
            this.Controls.Add(this.textBoxPatientID);
            this.Controls.Add(this.lblPatientID);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssignForm";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Validator.RequiredFieldValidator requiredFieldValidator2;
        private DevComponents.DotNetBar.Validator.RegularExpressionValidator regularExpressionValidator1;
        private DevComponents.DotNetBar.Validator.Highlighter highlighter;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private DevComponents.DotNetBar.Validator.SuperValidator superValidator;
        private MaterialLabel lblPatientID;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxPatientID;
        private MaterialLabel lblPatientName;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxPatientName;
        private MaterialRoundButton btnConfirm;
        private MaterialRoundButton btnCancel;
    }
}

