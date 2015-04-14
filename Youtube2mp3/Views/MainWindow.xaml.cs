using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace Youtube2mp3.Views
{
    public partial class MainWindow : MetroWindow
    {
        private readonly Youtube _youtube;
        private SongRoot _songList;
        public static MainWindow handler;

        public MainWindow()
        {
            InitializeComponent();
            handler = this;
            SettingsModel settings = SettingsModel.Load("settings");
            _youtube = new Youtube(settings.GoogleUsername, settings.GooglePassword, settings.YoututbeUsername, settings.YoutubePlaylist);
            _songList = SongRoot.Load("songs");

            new Task(SyncSongs).Start();
        }

        private void SyncSongs()
        {
            var downloader = new MusicDownloader("D:/youtube/", _songList);
            new Task(() => DownloadAllVideos(downloader)).Start();
            
            //sleep for 2 hours.
            Thread.Sleep(new TimeSpan(2,0,0));
        }

        private void syncButton_Click(object sender, RoutedEventArgs e)
        {
            waitingProgressRing.IsActive = true;
            var downloader = new MusicDownloader("D:/youtube/",_songList);
            new Task(() => DownloadAllVideos(downloader)).Start();
            waitingProgressRing.IsActive = false;
        }

        private void DownloadAllVideos(MusicDownloader downloader)
        {
            Dispatcher.Invoke(new Func<bool>(() => waitingProgressRing.IsActive = true));
            foreach (var video in _youtube.GetAllVideos())
            {
                if(_songList.Songs != null && _songList.Songs.Exists(song => song.Title == video.Title)) continue;
                Dispatcher.Invoke(
                    new Func<int>(() =>
                          downloadsStackPanel.Children.Add(new Controls.VideoDownload(video.Title, downloader, video.Link())))
                    );
            }
            Dispatcher.Invoke(new Func<bool>(() => waitingProgressRing.IsActive = false));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SongRoot.Save("songs",_songList);
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            new Settings().ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MetroWindow_StateChanged_1(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.ShowInTaskbar = true;
            }
        }

       
    }
}