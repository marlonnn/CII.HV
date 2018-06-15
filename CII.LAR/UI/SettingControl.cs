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
        private MaterialSkin.MaterialComboBox materialComboBox1;
        private MaterialSkin.MaterialRoundButton materialRoundButton1;
        private MaterialSkin.MaterialGroupBox gropBoxSystemInfo;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingControl));
            this.gropBoxSystemInfo = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.materialComboBox1 = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.lblCameraStatus = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblCamera = new CII.LAR.MaterialSkin.MaterialLabel();
            this.materialRoundButton1 = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.gropBoxSystemInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // gropBoxSystemInfo
            // 
            this.gropBoxSystemInfo.Controls.Add(this.materialComboBox1);
            this.gropBoxSystemInfo.Controls.Add(this.lblCameraStatus);
            this.gropBoxSystemInfo.Controls.Add(this.lblCamera);
            this.gropBoxSystemInfo.Depth = 0;
            resources.ApplyResources(this.gropBoxSystemInfo, "gropBoxSystemInfo");
            this.gropBoxSystemInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.gropBoxSystemInfo.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.gropBoxSystemInfo.Name = "gropBoxSystemInfo";
            this.gropBoxSystemInfo.TabStop = false;
            // 
            // materialComboBox1
            // 
            this.materialComboBox1.Depth = 0;
            this.materialComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.materialComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialComboBox1.FormattingEnabled = true;
            this.materialComboBox1.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(209)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.materialComboBox1, "materialComboBox1");
            this.materialComboBox1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialComboBox1.Name = "materialComboBox1";
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
            // materialRoundButton1
            // 
            resources.ApplyResources(this.materialRoundButton1, "materialRoundButton1");
            this.materialRoundButton1.Depth = 0;
            this.materialRoundButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.materialRoundButton1.Icon = null;
            this.materialRoundButton1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.materialRoundButton1.Name = "materialRoundButton1";
            this.materialRoundButton1.Primary = false;
            this.materialRoundButton1.UseVisualStyleBackColor = true;
            // 
            // SettingControl
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.materialRoundButton1);
            this.Controls.Add(this.gropBoxSystemInfo);
            this.Name = "SettingControl";
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.gropBoxSystemInfo, 0);
            this.Controls.SetChildIndex(this.materialRoundButton1, 0);
            this.gropBoxSystemInfo.ResumeLayout(false);
            this.gropBoxSystemInfo.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
