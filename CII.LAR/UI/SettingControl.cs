using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.UI
{
    public class SettingControl : BaseCtrl
    {
        private MaterialSkin.MaterialGroupBox gropBoxSystemInfo;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingControl));
            this.gropBoxSystemInfo = new CII.LAR.MaterialSkin.MaterialGroupBox();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // gropBoxSystemInfo
            // 
            this.gropBoxSystemInfo.Depth = 0;
            resources.ApplyResources(this.gropBoxSystemInfo, "gropBoxSystemInfo");
            this.gropBoxSystemInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.gropBoxSystemInfo.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.gropBoxSystemInfo.Name = "gropBoxSystemInfo";
            this.gropBoxSystemInfo.TabStop = false;
            // 
            // SettingControl
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.gropBoxSystemInfo);
            this.Name = "SettingControl";
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.gropBoxSystemInfo, 0);
            this.ResumeLayout(false);

        }
    }
}
