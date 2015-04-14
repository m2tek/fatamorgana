using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using Youtube2mp3.Models;
using YoutubeExtractor;

namespace Youtube2mp3
{
    public class MusicDownloader
    {
        private readonly string _path;
        private readonly SongRoot _songRoot;
        public MusicDownloader(string path,SongRoot songRoot)
        {
            _path = path;
            _songRoot = songRoot;
        }

        public void SaveVideo(string link, ProgressModel progressModel, string title)
        {
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);

            /*
             * We want the first extractable video with the highest audio quality.
             */
            VideoInfo video = videoInfos
                .Where(info => info.CanExtractAudio)
                .OrderByDescending(info => info.AudioBitrate)
                .First();

            var audioDownloader = new AudioDownloader(video, _path + video.Title + video.AudioExtension);

            audioDownloader.DownloadProgressChanged += (o, args) => progressModel.Progress = args.ProgressPercentage;

            new Task(()=>Download(audioDownloader,title)).Start();
        }
        private void Download(AudioDownloader audioDownloader, string title)
        {
            audioDownloader.Execute();
            _songRoot.Songs.Add(new Song(){Title = title});
        }
    }
}