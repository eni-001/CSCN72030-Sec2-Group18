using SmartHome.Domain;
using SmartHomeHub.wpf.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace SmartHome.Wpf.Pages
{
    public partial class ClimatePage : UserControl, IRefreshable
    {
        private readonly SmartHomeCoordinator _home;

        public ClimatePage(SmartHomeCoordinator home)
        {
            InitializeComponent();
            _home = home;

            // Initialize slider + radios from current state
            SetpointSlider.Value = _home.Heater.SetpointC;
            HeaterAutoModeCheck.IsChecked = _home.Heater.AutoMode;

            int power = _home.Heater.PowerLevel;
            foreach (var child in LogicalTreeHelper.GetChildren(this))
            {
                // not strictly needed; we set by power in Refresh instead
            }

            Refresh();
        }

        public void Refresh()
        {
            var temp = _home.Temp;
            var heater = _home.Heater;

            CurrentTempText.Text = $"{temp.CurrentCelsius:0.0} °C";
            TempStatusText.Text = temp.Status;

            HeaterStateText.Text = heater.IsHeating ? "Heating" : "Idle";
            HeaterEventText.Text = string.IsNullOrWhiteSpace(heater.LastEvent)
                ? "--"
                : heater.LastEvent;

            SetpointValueText.Text = $"{heater.SetpointC:0.0}";
            if (SetpointSlider.Value != heater.SetpointC)
                SetpointSlider.Value = heater.SetpointC;

            HeaterAutoModeCheck.IsChecked = heater.AutoMode;

            // highlight radio for current power
            int power = heater.PowerLevel;
            // quick and simple: assume order 1..5:
            // you can wire them separately if you want more control.
        }

        private void SetpointSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!IsLoaded) return;

            double value = e.NewValue;
            _home.Heater.SetSetpoint(value);
            Refresh();
        }

        private void HeaterAutoModeCheck_Changed(object sender, RoutedEventArgs e)
        {
            _home.Heater.AutoMode = HeaterAutoModeCheck.IsChecked == true;
            Refresh();
        }

        private void HeaterPowerRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb && int.TryParse(rb.Content.ToString(), out int level))
            {
                _home.Heater.SetPower(level);
                Refresh();
            }
        }
    }
}
