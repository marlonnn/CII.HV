using CII.LAR.DrawTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    /// <summary>
    /// ListViewItemEx: Set this to Tag of TransparentButton
    /// Used to delete item and delete drawObject in graphics when button click.
    /// Author:Zhong Wen 2017/08/03
    /// </summary>
    public class ListViewItemEx
    {

        /// <summary>
        /// ListViewItem
        /// </summary>
        private ListViewItem listViewItem;

        public ListViewItem ListViewItem
        {
            get
            {
                return listViewItem;
            }
            set
            {
                listViewItem = value;
            }
        }

        /// <summary>
        /// DrawObject
        /// </summary>
        private DrawObject drawObject;

        public DrawObject DrawObject
        {
            get
            {
                return this.drawObject;
            }
            set
            {
                drawObject = value;
            }
        }

        public ListViewItemEx(ListViewItem listViewItem, DrawObject drawObject)
        {
            this.listViewItem = listViewItem;
            this.drawObject = drawObject;
        }
    }
}
