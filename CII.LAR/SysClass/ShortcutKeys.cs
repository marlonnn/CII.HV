using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.SysClass
{
    [Serializable]
    public class ShortcutKeys
    {
        public const string Snap = "Snap";
        public const string ZoomOut = "ZoomOut";
        public const string ZoomIn = "ZoomIn";

        private Dictionary<string, List<Keys>> shortKeysDic;

        public Dictionary<string, List<Keys>> ShortKeyDic
        {
            get { return this.shortKeysDic; }
            set { this.shortKeysDic = value; }
        }

        public ShortcutKeys()
        {
            ShortKeyDic = new Dictionary<string, List<Keys>>();
            ShortKeyDic.Add(Snap, new List<Keys>());
            ShortKeyDic.Add(ZoomOut, new List<Keys>());
            ShortKeyDic.Add(ZoomIn, new List<Keys>());
        }

        public void AddSnapShortKey(Keys key)
        {
            var keys = ShortKeyDic[Snap];
            if (keys != null)
            {
                if (!CheckExist(keys, key)) ShortKeyDic[Snap].Add(key);
            }
        }

        private bool CheckExist(List<Keys> sourceKeys, Keys checkKey)
        {
            bool exist = false;
            foreach (var key in sourceKeys)
            {
                if (key == checkKey)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }
    }
}
