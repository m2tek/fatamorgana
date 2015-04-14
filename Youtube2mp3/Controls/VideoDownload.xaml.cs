using System.Threading.Tasks;
using System.Windows.Controls;
using Youtube2mp3.Models;

namespace Youtube2mp3.Controls
{
    /// <summary>
    /// Interaction logic for videoDownload.xaml
    /// </summary>
    public partial class VideoDownload : UserControl
    {
        public VideoDownload(string title, MusicDownloader downloader, string link)
        {
            InitializeComponent();

            ProgressModel progressModel = new ProgressModel();
            downloadProgressBar.DataContext = progressModel;
            downloadProgressBar.SetBinding(ProgressBar.ValueProperty, "Progress");
            videotitleTextBlock.Text = title;

            new Task(()=>downloader.SaveVideo(link,progressModel,title)).Start();
        }
    }
}
