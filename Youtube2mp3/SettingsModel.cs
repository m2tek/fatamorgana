using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Youtube2mp3
{
    //todo: load only once and keep instance
    public class SettingsModel : INotifyPropertyChanged
    {
        public string GoogleUsername { get; set; }
        public string GooglePassword { get; set; }
        public string YoututbeUsername { get; set; }
        public string YoutubePlaylist { get; set; }

        private string _folderName;
        public string FolderName
        {
            get { return _folderName; }
            set
            {
                _folderName = value;
                OnPropertyChanged("FolderName");
            }
        }

        public static void Save(SettingsModel settings)
        {
            using (Stream stream = File.Open(App.SettingsFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                var serializer = new XmlSerializer(typeof(SettingsModel));
                serializer.Serialize(stream, settings);
            }
        }

        public static SettingsModel Load()
        {
            try
            {
                using (Stream stream = File.Open(App.SettingsFile, FileMode.Open, FileAccess.Read, FileShare.Read))
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

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }

        #endregion
    }
}
