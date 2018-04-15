using CII.LAR.DrawTools;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.SysClass
{
    [Serializable]
    public class SysConfig
    {
        private int recordTime;
        public int RecordTime
        {
            get { return this.recordTime; }
            set { this.recordTime = value; }
        }
        [NonSerialized]
        private bool laserPortConected;
        public bool LaserPortConected
        {
            get { return this.laserPortConected; }
            set { this.laserPortConected = value; }
        }
        private string laserPort;
        public string LaserPort
        {
            get { return this.laserPort; }
            set { this.laserPort = value; }
        }

        private string motorPort;
        public string MotorPort
        {
            get { return this.motorPort; }
            set { this.motorPort = value; }
        }
        private string deviceMoniker;
        public string DeviceMoniker
        {
            get { return deviceMoniker; }
            set { this.deviceMoniker = value; }
        }

        [NonSerialized]
        private static string filePath = Application.StartupPath + "\\LConfig";

        [NonSerialized]
        public bool LiveMode = false;

        [NonSerialized]
        public SystemFunction Function;

        [NonSerialized]
        public System.Drawing.Point Point = System.Drawing.Point.Empty;

        public int DefaultScaleCoefficient;

        public static SysConfig Load()
        {
            SysConfig config = null;

            try
            {
                config = config.SerializeFromFile(filePath);
            }
            catch (Exception e)
            {
                config = new SysConfig();
            }

            return config;
        }

        /// <summary>
        /// do not manually call this function during software, the config should only be saved before software quit
        /// </summary>
        /// <param name="config"></param>
        public static void Save(SysConfig config)
        {
            if (config != null)
            {
                try
                {
                    config.SerializeToFile(filePath);
                }
                catch (System.Exception ex)
                {
                    Ins.Business.Entry.LAR.Entry.LogException(ex);
                }
            }
        }

        public static void Save(SysConfig config, SysConfig configOrigin)
        {
            SysConfig configNewer = SysConfig.Load();

            //merge the sysConfig
            FieldInfo[] fieldsSysConfig = config.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo fieldSysConfig in fieldsSysConfig)
            {
                if (fieldSysConfig == null)
                    continue;
                if ((fieldSysConfig.Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
                {
                    object origin = fieldSysConfig.GetValue(configOrigin);
                    object modified = fieldSysConfig.GetValue(config);
                    object newer = fieldSysConfig.GetValue(configNewer);
                    CustomAttrs attr = (CustomAttrs)Attribute.GetCustomAttribute(fieldSysConfig, typeof(CustomAttrs));
                    if (attr != null && !attr.IfEntirelyModify)
                    {
                        object mergedProperty = MergeField(origin, modified, newer);
                        fieldSysConfig.SetValue(configNewer, mergedProperty);
                    }
                    else
                    {
                        if (!FieldEqual(origin, modified))
                        {
                            fieldSysConfig.SetValue(configNewer, modified);
                        }
                    }
                }
            }
            Save(configNewer);
        }

        /// <summary>
        /// compare two serializable objects
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="modified"></param>
        /// <returns></returns>
        public static bool FieldEqual(object origin, object modified)
        {
            return origin == modified || (origin != null && modified != null && modified.SerializeEqual(origin));
        }

        public static Object MergeField(Object objOrigin, Object objModified, Object objNewer)
        {
            if (objModified == objOrigin) return objNewer;      // both null

            if (objOrigin == null || objModified == null || objNewer == null) return objModified;

            FieldInfo[] fields = objModified.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                if (field == null) continue;

                // ignore notserialized field
                if ((field.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.NotSerialized) continue;

                object origin = field.GetValue(objOrigin);
                object modified = field.GetValue(objModified);

                if (!FieldEqual(origin, modified))
                {
                    field.SetValue(objNewer, modified);
                }
            }
            return objNewer;
        }

        private LaserConfig laserConfig;
        public LaserConfig LaserConfig
        {
            get { return this.laserConfig; }
            set { this.laserConfig = value; }
        }

        public static SysConfig systemConfig;

        private string storagePath;

        public string StorePath
        {
            get
            {
                return this.storagePath;
            }
            set
            {
                this.storagePath = value;
            }
        }

        private string archivePath;
        public string ArchivePath
        {
            get
            {
                return this.archivePath;
            }
            set
            {
                this.archivePath = value;
            }
        }

        private string uiCulture;

        public string UICulture
        {
            get
            {
                if (string.IsNullOrEmpty(uiCulture))
                {
                    UICulture = GetSysDefaultCulture().Name;
                }

                return uiCulture;
            }
            set
            {
                if (uiCulture == value) // compare after previous check
                {
                    return;
                }
                uiCulture = value;

                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(uiCulture);
                MessageBoxManager.Reset();  // after UI culture changes

                OnPropertyChanged(() => UICulture);
            }
        }

        private Lense lense;
        public Lense Lense
        {
            get { return lense; }
            set { this.lense = value; }
        }

        private List<Lense> lenses;

        public List<Lense> Lenses
        {
            get { return lenses; }
            set { lenses = value; }
        }

        private GraphicsPropertiesManager graphicsPropertiesManager;

        public GraphicsPropertiesManager GraphicsPropertiesManager
        {
            get { return this.graphicsPropertiesManager; }
            set { this.graphicsPropertiesManager = value; }
        }

        public Lense GetLense(string lense)
        {
            return Lenses.Find(l => (l.ToString() == lense));
        }

        public bool AddLense(Lense newLense)
        {
            var lense = Lenses.Find(l => (l.Factor == newLense.Factor));
            if (lense == null)
            {
                //create a new lense
                Lenses.Add(newLense);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteLense(string lense)
        {
            var item = Lenses.Find(l => (l.Name == lense));
            if (item != null)
            {
                Lenses.Remove(item);
            }
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged<T>(Expression<Func<T>> propertyId)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)propertyId.Body).Member.Name));
            }
        }

        /// <summary>
        /// if this is Chinese edition which need register process
        /// </summary>
        public static bool ChineseEdition
        {
            get { return s_chineseEdition ?? (bool)(s_chineseEdition = File.Exists(Application.StartupPath + "\\zh-CN\\Cii.Lar.resources.dll")); }
        }

        private static bool? s_chineseEdition;

        public SysConfig()
        {
            SetDefault();

            SysConfig.Save(this);
        }

        private void SetDefault()
        {
            this.graphicsPropertiesManager = new GraphicsPropertiesManager();
            this.Lense = new Lense(1);
            this.lenses = new List<Lense>();
            this.laserConfig = new LaserConfig();
            this.storagePath = string.Format("{0}\\Archive", System.Environment.CurrentDirectory);
            this.archivePath = string.Format("{0}\\Archive", System.Environment.CurrentDirectory);
            this.Function = SystemFunction.Empty;
            this.DefaultScaleCoefficient = 4;
            this.recordTime = 0;
        }

        // set default value
        [OnDeserializing]
        private void OnDeserializing(StreamingContext sc)
        {
            SetDefault();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc)
        {

        }

        public CultureInfo GetSysDefaultCulture()
        {
            CultureInfo sysDefault = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (sysDefault.Name == "zh-CN" && !ChineseEdition)
            {
                sysDefault = new CultureInfo("en-US");
            }
            return sysDefault;
        }

        public string GetPropertyName<TValue>(Expression<Func<TValue>> propertyId)
        {
            return ((MemberExpression)propertyId.Body).Member.Name;
        }

        public void RefreshUICulture(ComponentResourceManager resources, Control ctr)
        {
            foreach (Control c in ctr.Controls)
            {
                //resources.ApplyResources(c, c.Name);
                //ControlCtrl control = c as ControlCtrl;
                //if (control != null)
                //{
                //    control.RefreshUI();
                //}
                //RefreshUICulture(resources, c);

                //if (c is ToolStripButton)
                //{
                //    RefreshBaseItemsUICulture(resources, (c as ToolStripButton).Items);
                //}
                //else  if (c is RibbonBar)
                //{
                //    RefreshBaseItemsUICulture(resources, (c as RibbonBar).Items);
                //}
                //else if (c is RibbonControl)
                //{
                //    RefreshBaseItemsUICulture(resources, (c as RibbonControl).Items);
                //}
                //else if (c is Bar)
                //{
                //    RefreshBaseItemsUICulture(resources, (c as Bar).Items);
                //}
                //else if (c is ButtonX)
                //{
                //    RefreshBaseItemsUICulture(resources, (c as ButtonX).SubItems);
                //}
                //else if (c is ToolStrip)
                //{
                //    RefreshToolStripUICulture(resources, (c as ToolStrip).Items);
                //}
            }
        }

        private void RefreshBaseItemsUICulture(ComponentResourceManager resources, SubItemsCollection items)
        {
            foreach (BaseItem item in items)
            {
                resources.ApplyResources(item, item.Name);
                RefreshBaseItemsUICulture(resources, item.SubItems);
            }
        }

        private void RefreshToolStripUICulture(ComponentResourceManager resources, ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                resources.ApplyResources(item, item.Name);
                if (item is ToolStripDropDownItem)
                    RefreshToolStripUICulture(resources, (item as ToolStripDropDownItem).DropDownItems);
            }
        }

    }
}
