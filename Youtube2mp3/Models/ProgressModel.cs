using System.ComponentModel;

namespace Youtube2mp3.Models
{
    public class ProgressModel : INotifyPropertyChanged
    {
        private double _progress;
        public double Progress
        {
            get { return _progress; }
            set 
            {
                _progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}