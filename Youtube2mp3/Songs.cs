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

        public static void Save(string filename, SongRoot songRoot)
        {
            using (Stream stream = File.Open(@"D:\youtube\" + filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                var serializer = new XmlSerializer(typeof(SongRoot));
                serializer.Serialize(stream, songRoot);
            }
        }

        public static SongRoot Load(string filename)
        {
            try
            {
                using (Stream stream = File.Open(@"D:\youtube\" + filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var serializer = new XmlSerializer(typeof(SongRoot));
                    var songRoot = ((SongRoot)serializer.Deserialize(stream));

                    return songRoot;
                }
            }
            catch (FileNotFoundException)
            {
                return new SongRoot(){Songs = new List<Song>()};
            }
        } 
    }

    public class Song
    {
        public string Title { get; set; }
    }
}
