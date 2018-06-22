using CII.LAR.MaterialSkin;

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
            this.cmbLenses = new MaterialComboBox();
            this.lblCurrentLense = new MaterialLabel();
            this.btnDelete = new MaterialRoundButton();
            this.txtAdd = new MaterialTextBox();
            this.labelX1 = new MaterialLabel();
            this.lblAdjustment = new MaterialLabel();
            this.rulerAdjustCtrl1 = new CII.LAR.UI.RulerAdjustCtrl();
            this.btnNew = new MaterialRoundButton();
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
            this.cmbLenses.SelectedIndexChanged += new System.EventHandler(this.cmbLenses_SelectedIndexChanged);
            // 
            // lblCurrentLense
            // 
            // 
            // 
            // 
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
            // labelX1
            // 
            // 
            // 
            // 
            resources.ApplyResources(this.labelX1, "labelX1");
            this.labelX1.Name = "labelX1";
            // 
            // lblAdjustment
            // 
            // 
            // 
            // 
            resources.ApplyResources(this.lblAdjustment, "lblAdjustment");
            this.lblAdjustment.Name = "lblAdjustment";
            // 
            // rulerAdjustCtrl1
            // 
            resources.ApplyResources(this.rulerAdjustCtrl1, "rulerAdjustCtrl1");
            this.rulerAdjustCtrl1.Name = "rulerAdjustCtrl1";
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Name = "btnNew";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // ObjectLenseCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.rulerAdjustCtrl1);
            this.Controls.Add(this.lblAdjustment);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblCurrentLense);
            this.Controls.Add(this.cmbLenses);
            this.Name = "ObjectLenseCtrl";
            this.Title = global::CII.LAR.Properties.Resources.StrObjectLense;
            this.Controls.SetChildIndex(this.closeButton, 0);
            this.Controls.SetChildIndex(this.cmbLenses, 0);
            this.Controls.SetChildIndex(this.lblCurrentLense, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.txtAdd, 0);
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.Controls.SetChildIndex(this.lblAdjustment, 0);
            this.Controls.SetChildIndex(this.rulerAdjustCtrl1, 0);
            this.Controls.SetChildIndex(this.btnNew, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialComboBox cmbLenses;
        private MaterialLabel lblCurrentLense;
        private MaterialRoundButton btnDelete;
        private MaterialTextBox txtAdd;
        private MaterialLabel labelX1;
        private MaterialLabel lblAdjustment;
        private RulerAdjustCtrl rulerAdjustCtrl1;
        private MaterialRoundButton btnNew;
    }
}
