using CII.LAR.MaterialSkin;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    partial class LaserAlignment
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaserAlignment));
            this.btnBack = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.btnNext = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.lblInfo = new CII.LAR.MaterialSkin.MaterialLabel();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // btnBack
            // 
            resources.ApplyResources(this.btnBack, "btnBack");
            this.btnBack.Depth = 0;
            this.btnBack.Icon = null;
            this.btnBack.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnBack.Name = "btnBack";
            this.btnBack.Primary = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Depth = 0;
            this.btnNext.Icon = null;
            this.btnNext.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnNext.Name = "btnNext";
            this.btnNext.Primary = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Depth = 0;
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblInfo.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblInfo.Name = "lblInfo";
            // 
            // LaserAlignment
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblInfo);
            this.Name = "LaserAlignment";
            this.Title = global::CII.LAR.Properties.Resources.StrLaserCtrlTitle;
            this.VisibleChanged += new System.EventHandler(this.LaserAlignment_VisibleChanged);
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.lblInfo, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialRoundButton btnBack;
        private MaterialRoundButton btnNext;
        private MaterialLabel lblInfo;
    }
}
