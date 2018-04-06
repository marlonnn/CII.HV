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
        private RichPictureBox richPictureBox;
        public ObjectLenseCtrl(RichPictureBox richPictureBox)
        {
            this.CtrlType = CtrlType.LenseCtrl;
            this.richPictureBox = richPictureBox;
            InitializeComponent();
            InitializeLenses();
            this.rulerAdjustCtrl1.UpdownClickHandler += UpdownClickHandler;
        }

        private void UpdownClickHandler(bool isUp)
        {
            var selectLense = cmbLenses.SelectedItem as Lense;
            if (selectLense != null)
            {
                if (isUp)
                {
                    selectLense.Factor += 0.1;
                }
                else
                {
                    if (selectLense.Factor - 0.1 != 0)
                        selectLense.Factor -= 0.1;
                }
                UpdateComBoxItemLense(selectLense);
                this.txtAdd.Text = selectLense.Factor.ToString();
                this.richPictureBox.Zoom = (float)selectLense.Factor;
                this.richPictureBox.Invalidate();
            }
        }

        private void txtAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int factor = 0;
                Int32.TryParse(txtAdd.Text, out factor);
                if (factor != 0)
                {
                    Lense lense = new Lense(factor);
                    if (Program.SysConfig.AddLense(lense))
                    {
                        UpdateComBoxItemLense(lense);
                        //更新界面的刻度尺
                        DelegateClass.GetDelegate().UpdateLenseHandler?.Invoke(lense);
                    }

                }
                else
                {
                    //should input correct lense factor
                    MessageBox.Show("Please input a number lense factor.", Application.ProductName, MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void InitializeLenses()
        {
            if (Program.SysConfig.Lenses != null && Program.SysConfig.Lenses.Count > 0)
            {
                cmbLenses.Items.Clear();
                cmbLenses.Items.AddRange(Program.SysConfig.Lenses.ToArray());
                cmbLenses.SelectedIndex = 0;
                this.txtAdd.Text = Program.SysConfig.Lenses[0].Factor.ToString();
            }
        }

        private void UpdateComBoxItemLense(Lense lense)
        {
            cmbLenses.Items.Clear();
            cmbLenses.Items.AddRange(Program.SysConfig.Lenses.ToArray());
            int index = Program.SysConfig.Lenses.FindIndex(l => (l.Name == lense.Name));
            cmbLenses.SelectedIndex = index;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var lense = cmbLenses.SelectedItem as Lense;
            if (lense != null && lense.Factor != 1)
            {
                int index = Program.SysConfig.Lenses.FindIndex(l => (l.Name == lense.Name));
                Program.SysConfig.DeleteLense(lense.Name);
                cmbLenses.Items.Clear();
                cmbLenses.Items.AddRange(Program.SysConfig.Lenses.ToArray());
                int currentIndex = FindSelectedIndex(index);
                if (currentIndex != -1)
                {
                    this.cmbLenses.SelectedIndex = currentIndex;
                    this.txtAdd.Text = Program.SysConfig.Lenses[currentIndex].Factor.ToString();
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
        }
    }
}
