using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    /// <summary>
    /// ListViewItem array
    /// Author:Zhong Wen 2017/08/02
    /// </summary>
    public class ListViewItemArray
    {
        private List<ListViewItem> items;

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public ListViewItemArray()
        {
            items = new List<ListViewItem>();
        }

        public void AddItem(ListViewItem item)
        {
            items.Add(item);
        }

        public void DeleteItem(ListViewItem item)
        {
            items.Remove(item);
        }
    }
}
