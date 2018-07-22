using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.SysClass;
using CII.LAR.Properties;

namespace CII.LAR.UI
{
    /// <summary>
    /// 物镜管理窗口
    /// 1.新建和删除物镜
    /// 2.测量刻度尺调整
    /// Zhong Wen 2018/4/3
    /// </summary>
    public partial class ObjectLenseCtrl : BaseCtrl
    {
        public EventHandler LenseChangeHandler;
        private RichPictureBox richPictureBox;
        public ObjectLenseCtrl(RichPictureBox richPictureBox)
        {
            this.CtrlType = CtrlType.LenseCtrl;
            this.richPictureBox = richPictureBox;
            InitializeComponent();
            InitializeLenses();
            resources = new ComponentResourceManager(typeof(ObjectLenseCtrl));
            this.rulerAdjustCtrl1.UpdownClickHandler += UpdownClickHandler;
            this.rulerAdjustCtrl1.LabelValueKeyDownHandler += LabelValueKeyDownHandler;
            this.cmbCameraType.SelectedIndex = (int)Program.SysConfig.CCD.CType;
            this.cmbCameraType.SelectedIndexChanged += new System.EventHandler(this.cmbCameraType_SelectedIndexChanged);
        }

        private void LabelValueKeyDownHandler()
        {
            try
            {
                float value = -1;
                var labelString = this.rulerAdjustCtrl1.LabelText;
                if (labelString.EndsWith("%"))
                {
                    int index = labelString.IndexOf("%");
                    string valueString = labelString.Substring(0, index);
                    value = float.Parse(valueString);
                }
                else
                {
                    value = float.Parse(labelString);
                }
                if (value == 0) return;
                var selectLense = cmbLenses.SelectedItem as Lense;
                if (selectLense != null)
                {
                    selectLense.FineAdjustment = value;
                    selectLense.FineAdjustment = float.Parse(selectLense.FineAdjustment.ToString("000.0"));
                    UpdateComBoxItemLense(selectLense);
                    //this.txtAdd.Text = selectLense.FineAdjustment.ToString();
                    //this.rulerAdjustCtrl1.LabelValue = selectLense.FineAdjustment.ToString();
                    this.richPictureBox.Invalidate();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdownClickHandler(bool isUp)
        {
            var selectLense = cmbLenses.SelectedItem as Lense;
            if (selectLense != null)
            {
                ShowRuler();
                if (isUp)
                {
                    selectLense.FineAdjustment += 0.1f;
                }
                else
                {
                    if (selectLense.FineAdjustment - 0.1f != 0)
                    {
                        selectLense.FineAdjustment -= 0.1f;
                    }
                }
                try
                {
                    selectLense.FineAdjustment = float.Parse(selectLense.FineAdjustment.ToString("000.0"));
                    UpdateComBoxItemLense(selectLense);
                    //this.txtAdd.Text = selectLense.FineAdjustment.ToString();
                    this.rulerAdjustCtrl1.LabelValue =  selectLense.FineAdjustment.ToString();
                    this.richPictureBox.Invalidate();
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void ShowRuler()
        {
            if (!this.richPictureBox.Rulers.ShowRulers)
                this.richPictureBox.Rulers.ShowRulers = true;
        }

        private void txtAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                NewObjectLense();
            }
        }

        private void InitializeLenses()
        {
            if (Program.SysConfig.Lenses != null && Program.SysConfig.Lenses.Count > 0)
            {
                cmbLenses.Items.Clear();
                for (int i=0; i< Program.SysConfig.Lenses.Count; i++)
                {
                    cmbLenses.Items.Add(Program.SysConfig.Lenses[i]);
                    if (Program.SysConfig.Lenses[i].Name == Program.SysConfig.Lense.Name)
                    {
                        cmbLenses.SelectedItem = Program.SysConfig.Lenses[i];
                        this.txtAdd.Text = Program.SysConfig.Lenses[i].Factor.ToString();
                        this.rulerAdjustCtrl1.LabelValue = Program.SysConfig.Lenses[i].FineAdjustment.ToString();
                    }
                }
            }
        }

        private void UpdateComBoxItemLense(Lense lense)
        {
            cmbLenses.Items.Clear();
            cmbLenses.Items.AddRange(Program.SysConfig.Lenses.ToArray());
            int index = Program.SysConfig.Lenses.FindIndex(l => (l.Name == lense.Name));
            cmbLenses.SelectedIndex = index;
            Program.SysConfig.Lense = lense;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var lense = cmbLenses.SelectedItem as Lense;
            if (lense != null && lense.Factor != 1)
            {
                int index = Program.SysConfig.Lenses.FindIndex(l => (l.Name == lense.Name));
                if (Program.SysConfig.DeleteLense(lense.Name)) LenseChangeHandler?.Invoke(null, null);
                cmbLenses.Items.Clear();
                cmbLenses.Items.AddRange(Program.SysConfig.Lenses.ToArray());
                int currentIndex = FindSelectedIndex(index);
                if (currentIndex != -1)
                {
                    this.cmbLenses.SelectedIndex = currentIndex;
                    this.txtAdd.Text = Program.SysConfig.Lenses[currentIndex].Factor.ToString();
                }
                else
                {
                    this.cmbLenses.Text = "";
                    this.txtAdd.Text = "";
                    this.rulerAdjustCtrl1.LabelValue = "";
                    Program.SysConfig.Lense.Factor = 1;
                    this.richPictureBox.Invalidate();
                }

            }
        }

        private int FindSelectedIndex(int delectIndex)
        {
            int index = delectIndex - 1;
            if (Program.SysConfig.Lenses !=null && Program.SysConfig.Lenses.Count > 0)
            {
                if (index >= 0 && index < Program.SysConfig.Lenses.Count) return index;
            }
            return -1;
        }

        private void cmbLenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.SysConfig.Lense = cmbLenses.SelectedItem as Lense;
            this.rulerAdjustCtrl1.LabelValue = Program.SysConfig.Lense.FineAdjustment.ToString();
            this.richPictureBox.Invalidate();
        }

        public override void RefreshUI()
        {
            this.Title = global::CII.LAR.Properties.Resources.StrObjectLense;
            resources.ApplyResources(this.labelX1, labelX1.Name);
            resources.ApplyResources(this.lblAdjustment, lblAdjustment.Name);
            resources.ApplyResources(this.lblCurrentLense, lblCurrentLense.Name);
            resources.ApplyResources(this.btnDelete, btnDelete.Name);
            resources.ApplyResources(this.btnNew, btnNew.Name);
            this.Invalidate();
        }

        public delegate void UpdateObjectLenses(Lense lense);
        public UpdateObjectLenses UpdateObjectLensesHandler;
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewObjectLense();
        }

        private void NewObjectLense()
        {
            int factor = 0;
            Int32.TryParse(txtAdd.Text, out factor);
            if (factor != 0)
            {
                Lense lense = new Lense(factor);
                if (Program.SysConfig.AddLense(lense))
                {
                    LenseChangeHandler?.Invoke(null, null);
                    //this.richPictureBox.ZoomNewLense(1, factor);
                    this.rulerAdjustCtrl1.LabelValue = lense.FineAdjustment.ToString();
                    UpdateComBoxItemLense(lense);
                    //更新界面的刻度尺
                    DelegateClass.GetDelegate().UpdateLenseHandler?.Invoke(lense);
                    ShowRuler();
                    this.richPictureBox.Invalidate();
                    UpdateObjectLensesHandler?.Invoke(lense);
                }

            }
            else
            {
                //should input correct lense factor
                MessageBox.Show(Resources.StrLenseFactorInputError, Application.ProductName, MessageBoxButtons.OK);
                return;
            }
        }

        private void cmbCameraType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.SysConfig.CCD.CType = GetCCDType(cmbCameraType.SelectedIndex);
        }

        private CCDType GetCCDType(int index)
        {
            CCDType type = CCDType.OneThird;
            switch (index)
            {
                case 0:
                    type = CCDType.OneForth;
                    break;
                case 1:
                    type = CCDType.OneThird;
                    break;
                case 2:
                    type = CCDType.OneSecond;
                    break;
                case 3:
                    type = CCDType.TwoThird;
                    break;
                default:
                    type = CCDType.OneThird;
                    break;
            }
            return type;
        }
    }
}
