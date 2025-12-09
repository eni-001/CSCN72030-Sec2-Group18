using SmartHome.Domain;
using SmartHomeHub.wpf.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace SmartHome.Wpf.Pages
{
    public partial class EnergyPage : UserControl, IRefreshable
    {
        private readonly SmartHomeCoordinator _home;

        public EnergyPage(SmartHomeCoordinator home)
        {
            InitializeComponent();
            _home = home;
            Refresh();
        }

        public void Refresh()
        {
            var fan = _home.Fan;
            var heater = _home.Heater;

            FanSpeedText.Text = fan.Speed == 0 ? "Off" : $"Speed {fan.Speed}";
            FanEnergyText.Text = fan.EnergySavingMode ? "Energy Mode: ON" : "Energy Mode: OFF";
            EnergyModeCheck.IsChecked = fan.EnergySavingMode;

            // set radio state based on speed
            // simple mapping
            // 0 = Off, 1 = Low, 2 = Medium, 3 = High
            // (You can improve this later if you want)
            // We won’t strictly force them here; it’s okay if mismatched occasionally.

            HeaterStateText.Text = heater.IsHeating ? "Heating" : "Idle";
            HeaterDetailsText.Text = $"Setpoint: {heater.SetpointC:0.0}°C · Power {heater.PowerLevel}";
        }

        private void FanSpeedRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb)
            {
                var fan = _home.Fan;

                switch (rb.Content.ToString())
                {
                    case "Off":
                        fan.Off();
                        break;
                    case "Low":
                        fan.SetSpeed(1);
                        break;
                    case "Medium":
                        fan.SetSpeed(2);
                        break;
                    case "High":
                        fan.SetSpeed(3);
                        break;
                }

                Refresh();
            }
        }

        private void EnergyModeCheck_Changed(object sender, RoutedEventArgs e)
        {
            _home.Fan.EnergySavingMode = EnergyModeCheck.IsChecked == true;
            Refresh();
        }
    }
}
