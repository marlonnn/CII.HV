using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class VideoChooseCtrl : BaseCtrl
    {
        private FilterInfoCollection videoDevices;

        public VideoChooseCtrl()
        {
            this.ShowIndex = 7;
            this.CtrlType = CtrlType.VideoChooseCtrl;
            InitializeComponent();
            resources = new ComponentResourceManager(typeof(VideoChooseCtrl));
            listViewCamera.FullRowSelect = true;
            filterInfoDic = new Dictionary<string, FilterInfo>();
        }

        private void SelectDevice()
        {
            string selectedDevice = Program.SysConfig.DeviceName;
            if (!string.IsNullOrEmpty(selectedDevice))
            {
                if (listViewCamera.Items != null || listViewCamera.Items.Count > 0)
                {
                    for(int i=0; i< listViewCamera.Items.Count; i++)
                    {
                        if (selectedDevice == listViewCamera.Items[i].Text.ToString())
                        {
                            this.listViewCamera.Focus();
                            this.listViewCamera.Items[i].Selected = true;
                        }
                    }
                }
            }
        }

        private Dictionary<string, FilterInfo> filterInfoDic;
        private void AddFilterInfo(FilterInfo filterInfo)
        {
            if (!filterInfoDic.ContainsKey(filterInfo.Name))
                filterInfoDic.Add(filterInfo.Name, filterInfo);
        }

        public void EnumerateVideoDevices()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices != null && videoDevices.Count > 0)
            {
                foreach (var device in videoDevices)
                {
                    FilterInfo filterInfo = device as FilterInfo;
                    if (filterInfo != null)
                    {
                        if (!filterInfoDic.ContainsKey(filterInfo.Name))
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = filterInfo.Name;
                            listViewCamera.Items.Add(item);
                            AddFilterInfo(filterInfo);
                        }
                    }
                }
            }
            else
            {
                FilterInfo fileInfo = new FilterInfo("No DirectShow devices found");
                if (!filterInfoDic.ContainsKey(fileInfo.Name))
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = "No DirectShow devices found";
                    //item.SubItems.Add("No DirectShow devices found");
                    listViewCamera.Items.Add(item);
                    AddFilterInfo(fileInfo);
                }
            }
            if (listViewCamera.SelectedItems != null)
            {
                this.listViewCamera.Focus();
                this.listViewCamera.Items[0].Selected = true;
                SelectDevice();
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                SelectDevice();
            }
        }

        private void listViewCamera_DoubleClick(object sender, EventArgs e)
        {
            if (listViewCamera.SelectedItems == null || listViewCamera.SelectedItems.Count == 0) return;

            string selectedDevice = listViewCamera.SelectedItems[0].Text;
            string deviceMoniker = GetMonikerString(selectedDevice);
            FilterInfo filterInfo = GetFilterInfo(selectedDevice);
            if (deviceMoniker != "" && DelegateClass.GetDelegate().CaptureDeviceHandler != null)
            {
                var videoDevice = Program.EntryForm.VideoDevice;
                if (videoDevice != null && videoDevice.IsRunning)
                {
                    this.Visible = false;
                    return;
                }
                DelegateClass.GetDelegate().CaptureDeviceHandler(deviceMoniker);
                Program.SysConfig.DeviceName = filterInfo.Name;
            }
            this.Visible = false;
        }

        private string GetMonikerString(string filterInfoName)
        {
            string deviceMoniker = "";
            foreach (var device in this.videoDevices)
            {
                var filterInfo = device as FilterInfo;
                if (filterInfo != null && filterInfo.Name == filterInfoName)
                {
                    deviceMoniker = filterInfo.MonikerString;
                    break;
                }
            }
            return deviceMoniker;
        }

        private FilterInfo GetFilterInfo(string filterInfoName)
        {
            FilterInfo fInfo = null;
            foreach (var device in this.videoDevices)
            {
                var filterInfo = device as FilterInfo;
                if (filterInfo != null && filterInfo.Name == filterInfoName)
                {
                    fInfo = filterInfo;
                    break;
                }
            }
            return fInfo;
        }

        public override void RefreshUI()
        {
            this.Title = global::CII.LAR.Properties.Resources.StrVideoChooseTitle;
            resources.ApplyResources(columnHeaderAvailable, columnHeaderAvailable.Text);
            columnHeaderAvailable.Text = global::CII.LAR.Properties.Resources.StrVideoAvailable;
            resources.ApplyResources(this.buttonCancel, this.buttonCancel.Name);
            resources.ApplyResources(this.buttonOk, this.buttonOk.Name);
        }
        //private string GetDeviceName(string deviceMoniker)
        //{
        //    if (string.IsNullOrEmpty(deviceMoniker)) return null;
        //    string deviceName = "";
        //    if (this.videoDevices != null && this.videoDevices.Count > 0)
        //    {
        //        foreach (var device in this.videoDevices)
        //        {
        //            var filterInfo = device as FilterInfo;
        //            if (filterInfo != null && filterInfo.MonikerString == deviceMoniker)
        //            {
        //                deviceName = filterInfo.Name;
        //                break;
        //            }
        //        }
        //    }
        //    return deviceName;
        //}

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        public override void InitializeLocation(Size size)
        {
            this.Location = new Point(30, 30);
        }
    }
}
