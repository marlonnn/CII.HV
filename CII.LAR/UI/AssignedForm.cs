using CII.LAR.SysClass;
using DevComponents.DotNetBar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class AssignedForm : Office2007Form
    {
        private AllPatients allPatients;
        public AssignedForm()
        {
            InitializeComponent();
            allPatients = Program.SysConfig.AllPatients;
            InitializeListView();
        }

        private void InitializeListView()
        {
            foreach (var p in allPatients.Patients)
            {
                ListViewItem lvi = new ListViewItem(new string[] { p.ID.ToString(), p.Name });
                lvi.Tag = p;
                this.listView.Items.Add(lvi);
            }
            if (this.listView.Items != null && this.listView.Items.Count > 0)
            {
                //listView.Items[0].Selected = true;
                listView.Items[0].Focused = true;
                this.listView.Items[0].Selected = true;
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView != null)
            {
                if (listView.SelectedItems != null && listView.SelectedItems.Count > 0)
                {
                    for (int i = 0; i< listView.SelectedItems.Count; i++)
                    {
                        if (listView.SelectedItems[i].Selected)
                        {
                            Patient p = listView.SelectedItems[i].Tag as Patient;
                            if (p != null) InitializeImageListView(p);
                            break;
                        }
                    }
                }

            }
        }

        private void InitializeImageListView(Patient p)
        {
            imageListView.Items.Clear();
            string folderName = Program.SysConfig.StorePath;
            string folder = string.Format("{0}\\{1}", folderName,  p.Foldername);
            string[] extesnsions = new string[] { ".png", ".avi" };
            var files = GetFiles(folder, extesnsions, SearchOption.TopDirectoryOnly);
            //this.imageListView.View = Manina.Windows.Forms.View.Thumbnails;
            foreach (var file in files)
            {
                imageListView.Items.Add(file.ToString());
            }
            listView.Focus();
        }


        /// <summary>
        /// Get files form directory
        /// </summary>
        /// <param name="sourceDirectory">source directory</param>
        /// <param name="exts">extensions</param>
        /// <param name="searchOpt">search option</param>
        /// <returns></returns>
        private IEnumerable GetFiles(string sourceDirectory, string[] exts, SearchOption searchOpt)
        {
            return Directory.GetFiles(sourceDirectory, "*.*", searchOpt)
                    .Where(
                inS => exts.Contains(System.IO.Path.GetExtension(inS),
                StringComparer.OrdinalIgnoreCase)
                           );
        }
    }
}
