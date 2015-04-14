using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.GData.Client;
using Google.YouTube;

namespace Youtube2mp3
{
    class Youtube
    {
        private const string c_developerKey =
    "AI39si5jQXPE9uH1_QtN6tZxU84bWr3tYsOALpLe_lgr7Fh-G3vTIK9Eg5ZBw-DIwdrqQ1zyB4gLF1KTgkfkcH6XLuPkFc-mgQ";

        private readonly string _username;
        private readonly string _password;
        private readonly string _youtubeUsername;
        private readonly string _playlistName;

        public Youtube(string username,string password, string youtubeUser, string playlistName)
        {
            _username = username;
            _password = password;
            _youtubeUsername = youtubeUser;
            _playlistName = playlistName;
        }

        public IEnumerable<Video> GetAllVideos()
        {
            var settings = new YouTubeRequestSettings("youtube2mp3", c_developerKey, _username,
                                                                       _password);
            settings.AutoPaging = true;
            var request = new YouTubeRequest(settings);

            Feed<Playlist> userPlaylists = request.GetPlaylistsFeed(_youtubeUsername);
            Playlist playlist = userPlaylists.Entries.First(p => p.Title == _playlistName);
            Feed<PlayListMember> list = request.GetPlaylist(playlist);


            foreach (Video v in list.Entries)
            {
                yield return v;
            }
        }

    }

    static class VideoExt
    {
        public static string Link(this Video v)
        {
            return "http://www.youtube.com/watch?feature=player_profilepage&v=" + v.VideoId;
        }
    }

}
