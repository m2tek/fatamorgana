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
        private readonly YoutubeManager _youtubeManager;
        private SongRoot _songList;
        public static MainWindow handler;
        private SettingsModel _settings;

        public MainWindow()
        {
            InitializeComponent();
            handler = this;
            _settings = SettingsModel.Load();
            _songList = SongRoot.Load();

            if (_settings.YoutubePlaylist == null)//todo: better check if the settings are valid
            {
                new Settings().ShowDialog();
            }
            else
            {
                new Task(SyncSongs).Start();
            }
        }

        private void SyncSongs()
        {
            var downloader = new MusicDownloader(_settings.FolderName, _songList);
            new Task(() => DownloadAllVideos(downloader)).Start();
            
            //sleep for 2 hours.
            Thread.Sleep(new TimeSpan(2,0,0));
        }

        private void syncButton_Click(object sender, RoutedEventArgs e)
        {
            waitingProgressRing.IsActive = true;
            var downloader = new MusicDownloader(_settings.FolderName, _songList);
            new Task(() => DownloadAllVideos(downloader)).Start();
            waitingProgressRing.IsActive = false;
        }

        private async void DownloadAllVideos(MusicDownloader downloader)
        {
            if (_settings.YoutubePlaylist == null)//todo: better check if the settings are valid
            {
                new Settings().ShowDialog();
            }
            Dispatcher.Invoke(new Func<bool>(() => waitingProgressRing.IsActive = true));
            var _youtubeManager = new YoutubeManager();
            foreach (var video in await _youtubeManager.GetAllVideos())
            {
                if(_songList.Songs != null && _songList.Songs.Exists(song => song.Title == video.Snippet.Title)) continue;
                Dispatcher.Invoke(
                    new Func<int>(() =>
                          downloadsStackPanel.Children.Add(new Controls.VideoDownload(video.Snippet.Title, downloader, video.Snippet.ResourceId.VideoId.ToYoutubeLink())))
                    );
            }
            Dispatcher.Invoke(new Func<bool>(() => waitingProgressRing.IsActive = false));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _songList.Save();
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