using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Youtube2mp3
{
    public class SongRoot
    {
        public List<Song> Songs { get; set; }

        private SongRoot()
        {
            Songs=new List<Song>();
        }

        public void Save()
        {
            using (Stream stream = File.Open(App.SongsFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                var serializer = new XmlSerializer(typeof(SongRoot));
                serializer.Serialize(stream, this);
            }
        }

        public static SongRoot Load()
        {
            try
            {
                using (Stream stream = File.Open(App.SongsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var serializer = new XmlSerializer(typeof(SongRoot));
                    var songRoot = ((SongRoot)serializer.Deserialize(stream));

                    return songRoot;
                }
            }
            catch (FileNotFoundException)
            {
                return new SongRoot();
            }
        } 
    }

    public class Song
    {
        public string Title { get; set; }
    }
}
