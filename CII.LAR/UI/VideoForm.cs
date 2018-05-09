using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class VideoForm : Office2007Form
    {
        private int startIndex = 0;
        private List<string> videoFiles;
        public List<string> VideoFiles
        {
            get { return videoFiles; }
            set { videoFiles = value; }
        }

        private string fileName;

        public VideoForm()
        {
            InitializeComponent();
            this.Load += VideoForm_Load;
        }

        public VideoForm(List<string> videoFiles, string fileName) : this()
        {
            this.videoFiles = videoFiles;
            this.fileName = fileName;
        }

        private void VideoForm_Load(object sender, System.EventArgs e)
        {
            startIndex = VideoFiles.FindIndex(file => { return file == fileName; });
            PlayVideo(startIndex);
        }

        private void WindowsMediaPlayer_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {

        }

        private void WindowsMediaPlayer_StatusChange(object sender, System.EventArgs e)
        {

        }

        private void VideoForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            StopPlayer();
        }

        private void PlayVideo(int playListIndex)
        {
            if (VideoFiles.Count > 0 && (playListIndex >= 0 && playListIndex < VideoFiles .Count))
            {
                windowsMediaPlayer.settings.autoStart = true;
                windowsMediaPlayer.URL = VideoFiles[playListIndex];
                windowsMediaPlayer.Ctlcontrols.next();
                windowsMediaPlayer.Ctlcontrols.play();
            }
        }

        private void StopPlayer()
        {
            windowsMediaPlayer.Ctlcontrols.stop();
            windowsMediaPlayer.Dispose();
        }
    }
}