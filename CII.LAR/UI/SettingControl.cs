using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.UI
{
    public class SettingControl : BaseCtrl
    {
        private MaterialSkin.MaterialLabel lblCamera;
        private MaterialSkin.MaterialLabel lblCameraStatus;
        private MaterialSkin.MaterialRoundButton btnSimulator;
        private MaterialSkin.MaterialGroupBox groupBoxLaser;
        private MaterialSkin.MaterialComboBox cmbLaser;
        private MaterialSkin.MaterialLabel materialLabel3;
        private MaterialSkin.MaterialComboBox cmbImage;
        private MaterialSkin.MaterialGroupBox groupBoxLanguage;
        private MaterialSkin.MaterialComboBox comboBoxItemLanguage;
        private MaterialSkin.MaterialGroupBox materialGroupBox1;
        private MaterialSkin.MaterialTextBox materialTextBox1;
        private MaterialSkin.MaterialGroupBox materialGroupBox2;
        private MaterialSkin.MaterialGroupBox gropBoxSystemInfo;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingControl));
            this.gropBoxSystemInfo = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.cmbImage = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.materialLabel3 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.btnSimulator = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.lblCameraStatus = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblCamera = new CII.LAR.MaterialSkin.MaterialLabel();
            this.groupBoxLaser = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.cmbLaser = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.groupBoxLanguage = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.comboBoxItemLanguage = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.materialGroupBox1 = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.materialTextBox1 = new CII.LAR.MaterialSkin.MaterialTextBox();
            this.materialGroupBox2 = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.gropBoxSystemInfo.SuspendLayout();
            this.groupBoxLaser.SuspendLayout();
            this.groupBoxLanguage.SuspendLayout();
            this.materialGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // gropBoxSystemInfo
            // 
            this.gropBoxSystemInfo.Controls.Add(this.cmbImage);
            this.gropBoxSystemInfo.Controls.Add(this.materialLabel3);
            this.gropBoxSystemInfo.Controls.Add(this.btnSimulator);
            this.gropBoxSystemInfo.Controls.Add(this.lblCameraStatus);
            this.gropBoxSystemInfo.Controls.Add(this.lblCamera);
            this.gropBoxSystemInfo.Depth = 0;
            resources.ApplyResources(this.gropBoxSystemInfo, "gropBoxSystemInfo");
            this.gropBoxSystemInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.gropBoxSystemInfo.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.gropBoxSystemInfo.Name = "gropBoxSystemInfo";
            this.gropBoxSystemInfo.TabStop = false;
            // 
            // cmbImage
            // 
            this.cmbImage.Depth = 0;
            this.cmbImage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImage.FormattingEnabled = true;
            this.cmbImage.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.cmbImage, "cmbImage");
            this.cmbImage.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.cmbImage.Name = "cmbImage";
            // 
            // materialLabel3
            // 
            resources.ApplyResources(this.materialLabel3, "materialLabel3");
            this.materialLabel3.Depth = 0;
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialLabel3.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            // 
            // btnSimulator
            // 
            resources.ApplyResources(this.btnSimulator, "btnSimulator");
            this.btnSimulator.Depth = 0;
            this.btnSimulator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.btnSimulator.Icon = null;
            this.btnSimulator.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnSimulator.Name = "btnSimulator";
            this.btnSimulator.Primary = false;
            this.btnSimulator.UseVisualStyleBackColor = true;
            // 
            // lblCameraStatus
            // 
            resources.ApplyResources(this.lblCameraStatus, "lblCameraStatus");
            this.lblCameraStatus.Depth = 0;
            this.lblCameraStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblCameraStatus.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblCameraStatus.Name = "lblCameraStatus";
            // 
            // lblCamera
            // 
            resources.ApplyResources(this.lblCamera, "lblCamera");
            this.lblCamera.Depth = 0;
            this.lblCamera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblCamera.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblCamera.Name = "lblCamera";
            // 
            // groupBoxLaser
            // 
            this.groupBoxLaser.Controls.Add(this.cmbLaser);
            this.groupBoxLaser.Depth = 0;
            resources.ApplyResources(this.groupBoxLaser, "groupBoxLaser");
            this.groupBoxLaser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.groupBoxLaser.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.groupBoxLaser.Name = "groupBoxLaser";
            this.groupBoxLaser.TabStop = false;
            // 
            // cmbLaser
            // 
            this.cmbLaser.Depth = 0;
            this.cmbLaser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLaser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLaser.FormattingEnabled = true;
            this.cmbLaser.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.cmbLaser, "cmbLaser");
            this.cmbLaser.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.cmbLaser.Name = "cmbLaser";
            // 
            // groupBoxLanguage
            // 
            this.groupBoxLanguage.Controls.Add(this.comboBoxItemLanguage);
            this.groupBoxLanguage.Depth = 0;
            resources.ApplyResources(this.groupBoxLanguage, "groupBoxLanguage");
            this.groupBoxLanguage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.groupBoxLanguage.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.groupBoxLanguage.Name = "groupBoxLanguage";
            this.groupBoxLanguage.TabStop = false;
            // 
            // comboBoxItemLanguage
            // 
            this.comboBoxItemLanguage.Depth = 0;
            this.comboBoxItemLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxItemLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemLanguage.FormattingEnabled = true;
            this.comboBoxItemLanguage.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.comboBoxItemLanguage, "comboBoxItemLanguage");
            this.comboBoxItemLanguage.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.comboBoxItemLanguage.Name = "comboBoxItemLanguage";
            // 
            // materialGroupBox1
            // 
            this.materialGroupBox1.Controls.Add(this.materialTextBox1);
            this.materialGroupBox1.Depth = 0;
            resources.ApplyResources(this.materialGroupBox1, "materialGroupBox1");
            this.materialGroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialGroupBox1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialGroupBox1.Name = "materialGroupBox1";
            this.materialGroupBox1.TabStop = false;
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.materialTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.materialTextBox1.CustomAutoSize = true;
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.EmptyTextTip = null;
            this.materialTextBox1.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.materialTextBox1, "materialTextBox1");
            this.materialTextBox1.ForeColor = System.Drawing.Color.White;
            this.materialTextBox1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialTextBox1.Name = "materialTextBox1";
            this.materialTextBox1.Radius = 3;
            // 
            // materialGroupBox2
            // 
            this.materialGroupBox2.Depth = 0;
            resources.ApplyResources(this.materialGroupBox2, "materialGroupBox2");
            this.materialGroupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialGroupBox2.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialGroupBox2.Name = "materialGroupBox2";
            this.materialGroupBox2.TabStop = false;
            // 
            // SettingControl
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.materialGroupBox2);
            this.Controls.Add(this.materialGroupBox1);
            this.Controls.Add(this.groupBoxLanguage);
            this.Controls.Add(this.groupBoxLaser);
            this.Controls.Add(this.gropBoxSystemInfo);
            this.Name = "SettingControl";
            this.Controls.SetChildIndex(this.gropBoxSystemInfo, 0);
            this.Controls.SetChildIndex(this.groupBoxLaser, 0);
            this.Controls.SetChildIndex(this.groupBoxLanguage, 0);
            this.Controls.SetChildIndex(this.materialGroupBox1, 0);
            this.Controls.SetChildIndex(this.materialGroupBox2, 0);
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.gropBoxSystemInfo.ResumeLayout(false);
            this.gropBoxSystemInfo.PerformLayout();
            this.groupBoxLaser.ResumeLayout(false);
            this.groupBoxLanguage.ResumeLayout(false);
            this.materialGroupBox1.ResumeLayout(false);
            this.materialGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
