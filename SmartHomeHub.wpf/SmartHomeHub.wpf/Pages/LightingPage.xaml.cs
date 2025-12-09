using SmartHome.Domain;
using SmartHomeHub.wpf.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace SmartHome.Wpf.Pages
{
    public partial class LightingPage : UserControl, IRefreshable
    {
        private readonly SmartHomeCoordinator _home;

        public LightingPage(SmartHomeCoordinator home)
        {
            InitializeComponent();
            _home = home;
            Refresh();
        }

        public void Refresh()
        {
            var light = _home.Light;

            LightStateText.Text = light.IsOn ? "ON" : "OFF";
            LightBrightnessText.Text = $"Brightness: {light.BrightnessLevel}%";
            LightToggle.IsChecked = light.IsOn;
            LightAutoCheck.IsChecked = light.AutoMode;

            if (BrightnessSlider.Value != light.BrightnessLevel)
                BrightnessSlider.Value = light.BrightnessLevel;
        }

        private void LightToggle_Click(object sender, RoutedEventArgs e)
        {
            var light = _home.Light;

            if (light.IsOn)
                light.TurnOff();
            else
                light.TurnOn();

            Refresh();
        }

        private void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!IsLoaded) return;

            var light = _home.Light;
            light.SetBrightness((int)e.NewValue);
            Refresh();
        }

        private void LightAutoCheck_Changed(object sender, RoutedEventArgs e)
        {
            _home.Light.AutoMode = LightAutoCheck.IsChecked == true;
            Refresh();
        }
    }
}
