using SmartHome.Domain;
using SmartHomeHub.wpf.Interfaces;
using System.Windows.Controls;

namespace SmartHome.Wpf.Pages
{
    public partial class DashboardPage : UserControl, IRefreshable
    {
        private readonly SmartHomeCoordinator _home;

        public DashboardPage(SmartHomeCoordinator home)
        {
            InitializeComponent();
            _home = home;
            Refresh();
        }

        public void Refresh()
        {
            var temp = _home.Temp;
            var heater = _home.Heater;
            var light = _home.Light;
            var door = _home.Door;
            var motion = _home.Motion;
            var smoke = _home.Smoke;
            var fan = _home.Fan;
            var camera = _home.Camera;

            // Temperature
            TempValueText.Text = $"{temp.CurrentCelsius:0.0} °C";
            TempStatusText.Text = temp.Status;

            // Heater
            HeaterStateText.Text = heater.IsHeating ? "Heating" : "Idle";
            HeaterPowerText.Text = $"Setpoint: {heater.SetpointC:0.0}°C · Power {heater.PowerLevel}";

            // Light
            LightStateText.Text = light.IsOn ? "ON" : "OFF";
            LightBrightnessText.Text = $"Brightness: {light.BrightnessLevel}%";

            // Door
            DoorStateText.Text = door.IsLocked ? "Locked" : "Unlocked";
            DoorLastActionText.Text = door.LastAction;

            // Motion
            MotionStateText.Text = motion.Detected ? "Motion Detected" : "No Motion";
            MotionLastText.Text = motion.LastDetectedUtc?.ToLocalTime().ToString("HH:mm:ss") ?? "--";

            // Smoke
            SmokeStateText.Text = smoke.SmokeDetected ? "Smoke Detected" : "All Clear";
            SmokeLevelText.Text = $"Level: {smoke.SmokeLevel}%";

            // Fan
            FanSpeedText.Text = fan.Speed == 0 ? "Off" : $"Speed {fan.Speed}";
            FanEnergyText.Text = fan.EnergySavingMode ? "Energy Mode: ON" : "Energy Mode: OFF";

            // Camera
            CameraStateText.Text = camera.IsStreaming ? "Streaming" : "Idle";
            CameraEventText.Text = string.IsNullOrWhiteSpace(camera.LastEvent)
                ? "Last event: --"
                : $"Last event: {camera.LastEvent}";
        }
    }
}
