using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Youtube2mp3.Views;

namespace Youtube2mp3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string AppName = "YoutubeSync";
        public static string SettingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName,"settings");
        public static string SongsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName,"songs");
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string specificFolder = Path.Combine(folder, AppName);

            // Check if folder exists and if not, create it
            if (!Directory.Exists(specificFolder))
                Directory.CreateDirectory(specificFolder);

            // Create main application window, starting minimized
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowState = WindowState.Minimized;
            mainWindow.Show();
        }
    }
}
