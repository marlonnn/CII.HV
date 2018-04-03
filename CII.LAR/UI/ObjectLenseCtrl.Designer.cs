namespace CII.LAR.UI
{
    partial class ObjectLenseCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectLenseCtrl));
            this.cmbLenses = new System.Windows.Forms.ComboBox();
            this.lblCurrentLense = new DevComponents.DotNetBar.LabelX();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtAdd = new System.Windows.Forms.TextBox();
            this.slider = new CII.LAR.UI.SliderCtrl();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lblAdjustment = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // cmbLenses
            // 
            this.cmbLenses.FormattingEnabled = true;
            resources.ApplyResources(this.cmbLenses, "cmbLenses");
            this.cmbLenses.Name = "cmbLenses";
            // 
            // lblCurrentLense
            // 
            // 
            // 
            // 
            this.lblCurrentLense.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblCurrentLense, "lblCurrentLense");
            this.lblCurrentLense.Name = "lblCurrentLense";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtAdd
            // 
            resources.ApplyResources(this.txtAdd, "txtAdd");
            this.txtAdd.Name = "txtAdd";
            this.txtAdd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdd_KeyPress);
            // 
            // slider
            // 
            resources.ApplyResources(this.slider, "slider");
            this.slider.Name = "slider";
            this.slider.Update = true;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.labelX1, "labelX1");
            this.labelX1.Name = "labelX1";
            // 
            // lblAdjustment
            // 
            // 
            // 
            // 
            this.lblAdjustment.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblAdjustment, "lblAdjustment");
            this.lblAdjustment.Name = "lblAdjustment";
            // 
            // ObjectLenseCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblAdjustment);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.slider);
            this.Controls.Add(this.txtAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblCurrentLense);
            this.Controls.Add(this.cmbLenses);
            this.Name = "ObjectLenseCtrl";
            this.Title = "Object Lense Manager";
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.cmbLenses, 0);
            this.Controls.SetChildIndex(this.lblCurrentLense, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.txtAdd, 0);
            this.Controls.SetChildIndex(this.slider, 0);
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.Controls.SetChildIndex(this.lblAdjustment, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbLenses;
        private DevComponents.DotNetBar.LabelX lblCurrentLense;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtAdd;
        private SliderCtrl slider;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX lblAdjustment;
    }
}
