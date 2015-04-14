using MahApps.Metro.Controls;

namespace Youtube2mp3.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        private SettingsModel _settingsModel;
        public Settings()
        {
            InitializeComponent();
            _settingsModel = SettingsModel.Load("settings") ?? new SettingsModel();
            this.DataContext = _settingsModel;
        }

        private void MetroWindow_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsModel.Save("settings",_settingsModel);
        }
    }
}
