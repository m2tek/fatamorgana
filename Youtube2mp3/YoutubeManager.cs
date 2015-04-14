using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms.VisualStyles;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace Youtube2mp3
{
    class YoutubeManager
    {
        private const string c_developerKey = "AIzaSyC-hQDSMypihadkfnlbjcOuFBpQDTU9uP4";

        private readonly string _username;
        private readonly string _password;
        private readonly string _youtubeUsername;
        private readonly string _playlistId;

        public YoutubeManager()
        {
            var settings = SettingsModel.Load();
            _username = settings.GoogleUsername;
            _password = settings.GooglePassword;
            _youtubeUsername = settings.YoututbeUsername;
            if (settings.YoutubePlaylist != null)
            {
                var uri = new Uri(settings.YoutubePlaylist);
                _playlistId = HttpUtility.ParseQueryString(uri.Query).Get("list");    
            }
            
        }

        public async Task<IEnumerable<PlaylistItem>> GetAllVideos()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = c_developerKey,
                ApplicationName = "YoutubeSync"
            });

            var playlist = youtubeService.PlaylistItems.List("snippet");//make next page
            playlist.PlaylistId = _playlistId;
            playlist.MaxResults = 2;
            var videos = playlist.Execute();
            var list = videos.Items;
            return list;
        }

    }

    static class VideoExt
    {
        public static string ToYoutubeLink(this string v)
        {
            return "http://www.youtube.com/watch?feature=player_profilepage&v=" + v;
        }
    }

}
