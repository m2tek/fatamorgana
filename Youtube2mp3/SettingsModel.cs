using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Youtube2mp3
{
    public class SettingsModel
    {
        public string GoogleUsername { get; set; }
        public string GooglePassword { get; set; }
        public string YoututbeUsername { get; set; }
        public string YoutubePlaylist { get; set; }

        public static void Save(string filename, SettingsModel settings)
        {
            using (Stream stream = File.Open(@"D:\youtube\" + filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                var serializer = new XmlSerializer(typeof(SettingsModel));
                serializer.Serialize(stream, settings);
            }
        }

        public static SettingsModel Load(string filename)
        {
            try//todo: nie hardcoded D:
            {
                using (Stream stream = File.Open(@"D:\youtube\" + filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var serializer = new XmlSerializer(typeof(SettingsModel));
                    var settings = ((SettingsModel)serializer.Deserialize(stream));

                    return settings;
                }
            }
            catch (FileNotFoundException)
            {
                return new SettingsModel();
            }
        } 
    }
}
