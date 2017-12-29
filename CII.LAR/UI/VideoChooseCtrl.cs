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
            InitializeComponent();
            listViewCamera.FullRowSelect = true;
        }


        public void EnumerateVideoDevices()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices != null && videoDevices.Count > 0)
            {
                foreach (var device in videoDevices)
                {
                    var filterInfo = device as FilterInfo;
                    if (filterInfo != null)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = filterInfo.Name;
                        listViewCamera.Items.Add(item);
                    }
                }
            }
            else
            {
                ListViewItem item = new ListViewItem();
                item.Text = "No DirectShow devices found";
                //item.SubItems.Add("No DirectShow devices found");
                listViewCamera.Items.Add(item);
            }
        }

        private void listViewCamera_DoubleClick(object sender, EventArgs e)
        {
            var deviceInfoName = listViewCamera.SelectedItems[0].Text;
            string deviceMoniker = GetMonikerString(deviceInfoName);
            if (deviceMoniker != "" && CaptureDeviceHandler != null)
            {
                CaptureDeviceHandler(deviceMoniker);
            }
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
    }
}
