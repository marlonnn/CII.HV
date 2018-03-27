using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.UI
{
    public enum ArrayChangedType
    {
        ItemAdded,
        ItemRemoved,
        ItemMoveAuto,
    }

    public class ArrayChangedEventArgs<ItemType> : EventArgs
    {
        public ItemType Item
        {
            get;
            private set;
        }

        public ArrayChangedType ChangeType
        {
            get;
            private set;
        }

        //We need to set this flag as true to repaint dot plot when a gate is added via redo or undo.
        public bool RefreshWhenAdded
        {
            get;
            private set;
        }

        public ArrayChangedEventArgs(ItemType item, ArrayChangedType type, bool refreshWhenAdded = false)
        {
            Item = item;
            ChangeType = type;
            RefreshWhenAdded = type == ArrayChangedType.ItemAdded && refreshWhenAdded;
        }
    }
}
