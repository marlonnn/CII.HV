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
            this.cmbLenses = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.lblCurrentLense = new CII.LAR.MaterialSkin.MaterialLabel();
            this.btnDelete = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.txtAdd = new CII.LAR.MaterialSkin.MaterialTextBox();
            this.labelX1 = new CII.LAR.MaterialSkin.MaterialLabel();
            this.lblAdjustment = new CII.LAR.MaterialSkin.MaterialLabel();
            this.rulerAdjustCtrl1 = new CII.LAR.UI.RulerAdjustCtrl();
            this.btnNew = new CII.LAR.MaterialSkin.MaterialRoundButton();
            this.lblCameraType = new CII.LAR.MaterialSkin.MaterialLabel();
            this.cmbCameraType = new CII.LAR.MaterialSkin.MaterialComboBox();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            // 
            // cmbLenses
            // 
            this.cmbLenses.Depth = 0;
            this.cmbLenses.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLenses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbLenses, "cmbLenses");
            this.cmbLenses.ForeColor = System.Drawing.Color.White;
            this.cmbLenses.FormattingEnabled = true;
            this.cmbLenses.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.cmbLenses.Name = "cmbLenses";
            this.cmbLenses.SelectedIndexChanged += new System.EventHandler(this.cmbLenses_SelectedIndexChanged);
            // 
            // lblCurrentLense
            // 
            this.lblCurrentLense.Depth = 0;
            resources.ApplyResources(this.lblCurrentLense, "lblCurrentLense");
            this.lblCurrentLense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblCurrentLense.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblCurrentLense.Name = "lblCurrentLense";
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackColor = System.Drawing.Color.Gray;
            this.btnDelete.Depth = 0;
            this.btnDelete.Icon = null;
            this.btnDelete.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Primary = false;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Warning = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtAdd
            // 
            this.txtAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.txtAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdd.CustomAutoSize = true;
            this.txtAdd.Depth = 0;
            this.txtAdd.EmptyTextTip = null;
            this.txtAdd.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.txtAdd, "txtAdd");
            this.txtAdd.ForeColor = System.Drawing.Color.White;
            this.txtAdd.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.txtAdd.Name = "txtAdd";
            this.txtAdd.Radius = 3;
            this.txtAdd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdd_KeyPress);
            // 
            // labelX1
            // 
            this.labelX1.Depth = 0;
            resources.ApplyResources(this.labelX1, "labelX1");
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.labelX1.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.labelX1.Name = "labelX1";
            // 
            // lblAdjustment
            // 
            this.lblAdjustment.Depth = 0;
            resources.ApplyResources(this.lblAdjustment, "lblAdjustment");
            this.lblAdjustment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblAdjustment.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblAdjustment.Name = "lblAdjustment";
            // 
            // rulerAdjustCtrl1
            // 
            this.rulerAdjustCtrl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(53)))));
            resources.ApplyResources(this.rulerAdjustCtrl1, "rulerAdjustCtrl1");
            this.rulerAdjustCtrl1.Name = "rulerAdjustCtrl1";
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.BackColor = System.Drawing.Color.Gray;
            this.btnNew.Depth = 0;
            this.btnNew.Icon = null;
            this.btnNew.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.btnNew.Name = "btnNew";
            this.btnNew.Primary = false;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Warning = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lblCameraType
            // 
            resources.ApplyResources(this.lblCameraType, "lblCameraType");
            this.lblCameraType.Depth = 0;
            this.lblCameraType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(184)))), ((int)(((byte)(208)))));
            this.lblCameraType.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.lblCameraType.Name = "lblCameraType";
            // 
            // cmbCameraType
            // 
            this.cmbCameraType.Depth = 0;
            this.cmbCameraType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCameraType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbCameraType, "cmbCameraType");
            this.cmbCameraType.ForeColor = System.Drawing.Color.White;
            this.cmbCameraType.FormattingEnabled = true;
            this.cmbCameraType.Items.AddRange(new object[] {
            resources.GetString("cmbCameraType.Items"),
            resources.GetString("cmbCameraType.Items1"),
            resources.GetString("cmbCameraType.Items2"),
            resources.GetString("cmbCameraType.Items3")});
            this.cmbCameraType.MouseState = CII.LAR.MaterialSkin.MouseState.HOVER;
            this.cmbCameraType.Name = "cmbCameraType";
            // 
            // ObjectLenseCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCameraType);
            this.Controls.Add(this.lblCameraType);
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
            this.Controls.SetChildIndex(this.lblCameraType, 0);
            this.Controls.SetChildIndex(this.cmbCameraType, 0);
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
        private MaterialLabel lblCameraType;
        private MaterialComboBox cmbCameraType;
    }
}
