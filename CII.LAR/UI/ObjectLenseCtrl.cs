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
        public ObjectLenseCtrl()
        {
            this.CtrlType = CtrlType.LenseCtrl;
            InitializeComponent();
            InitializeLenses();
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
            }
        }

        private void UpdateComBoxItemLense(Lense lense)
        {
            cmbLenses.Items.Clear();
            cmbLenses.Items.AddRange(Program.SysConfig.Lenses.ToArray());
            int index = Program.SysConfig.Lenses.FindIndex(l => (l.ToString() == lense.ToString()));
            cmbLenses.SelectedIndex = index;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var lense = cmbLenses.SelectedItem;
            if (lense != null)
            {
                int index = Program.SysConfig.Lenses.FindIndex(l => (l.ToString() == lense.ToString()));
                Program.SysConfig.DeleteLense(lense.ToString());
                cmbLenses.Items.Clear();
                cmbLenses.Items.AddRange(Program.SysConfig.Lenses.ToArray());
                cmbLenses.SelectedIndex = index - 1;

            }
        }
    }
}
