using SmartHome.Domain;
using SmartHomeHub.wpf.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace SmartHome.Wpf.Pages
{
    public partial class SettingsPage : UserControl, IRefreshable
    {
        private readonly SmartHomeCoordinator _home;

        public SettingsPage(SmartHomeCoordinator home)
        {
            InitializeComponent();
            _home = home;
            Refresh();
        }

        public void Refresh()
        {
            HeaterAutoCheck.IsChecked = _home.Heater.AutoMode;
            LightAutoCheck.IsChecked = _home.Light.AutoMode;
            CameraAutoCheck.IsChecked = _home.Camera.AutoMode;
        }

        private void HeaterAutoCheck_Changed(object sender, RoutedEventArgs e)
        {
            _home.Heater.AutoMode = HeaterAutoCheck.IsChecked == true;
        }

        private void LightAutoCheck_Changed(object sender, RoutedEventArgs e)
        {
            _home.Light.AutoMode = LightAutoCheck.IsChecked == true;
        }

        private void CameraAutoCheck_Changed(object sender, RoutedEventArgs e)
        {
            _home.Camera.AutoMode = CameraAutoCheck.IsChecked == true;
        }
    }
}
