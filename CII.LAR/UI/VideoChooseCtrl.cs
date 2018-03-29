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

        public delegate void CaptureDevice(string deviceMoniker);
        public CaptureDevice CaptureDeviceHandler;

        public VideoChooseCtrl()
        {
            this.ShowIndex = 7;
            this.CtrlType = CtrlType.VideoChooseCtrl;
            InitializeComponent();
            listViewCamera.FullRowSelect = true;
            filterInfoDic = new Dictionary<string, FilterInfo>();
        }

        private Dictionary<string, FilterInfo> filterInfoDic;
        private void AddFilterInfo(FilterInfo filterInfo)
        {
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
                if (!filterInfoDic.ContainsKey("No DirectShow devices found"))
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = "No DirectShow devices found";
                    //item.SubItems.Add("No DirectShow devices found");
                    listViewCamera.Items.Add(item);
                    AddFilterInfo(new FilterInfo("No DirectShow devices found"));
                }
            }
            if (listViewCamera.SelectedItems != null)
            {
                this.listViewCamera.Focus();
                this.listViewCamera.Items[0].Selected = true;
            }
        }

        private void listViewCamera_DoubleClick(object sender, EventArgs e)
        {
            if (listViewCamera.SelectedItems == null || listViewCamera.SelectedItems.Count == 0) return;

            var deviceInfoName = listViewCamera.SelectedItems[0].Text;
            string deviceMoniker = GetMonikerString(deviceInfoName);
            if (deviceMoniker != "" && CaptureDeviceHandler != null)
            {
                CaptureDeviceHandler(deviceMoniker);
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
