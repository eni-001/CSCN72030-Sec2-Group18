using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using SmartHome.Domain;

namespace SmartHome.Wpf.ViewModels
{
    public class SmartHomeViewModel : INotifyPropertyChanged
    {
        // Devices
        private readonly TemperatureSensor _temp;
        private readonly LightController _light;
        private readonly MotionSensor _motion;
        private readonly DoorLockController _door;
        private readonly SmokeDetector _smoke;
        private readonly FanController _fan;
        private readonly HeaterController _heater;
        private readonly CameraController _camera;
        private readonly SmartHomeCoordinator _home;

        private readonly DispatcherTimer _timer;

        public SmartHomeViewModel()
        {
            // Create device objects (same as Program.cs)
            _temp = new TemperatureSensor();
            _light = new LightController();
            _motion = new MotionSensor();
            _door = new DoorLockController();
            _smoke = new SmokeDetector();
            _fan = new FanController();
            _heater = new HeaterController(_temp);
            _camera = new CameraController();

            _home = new SmartHomeCoordinator(
                _temp, _light, _motion, _door,
                _smoke, _fan, _heater, _camera);

            // Timer to drive simulation (every 1 second)
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (s, e) => Tick();
            _timer.Start();
        }

        private int _hour = 10;
        public string SimulatedTime => $"{_hour:00}:00";

        private void Tick()
        {
            _home.RunSprint2Simulation();
            _hour++;
            if (_hour > 23) _hour = 0;

            // Notify UI that all bound properties changed
            OnPropertyChanged(nameof(SimulatedTime));
            OnPropertyChanged(nameof(TemperatureValue));
            OnPropertyChanged(nameof(TemperatureStatus));
            OnPropertyChanged(nameof(HeaterStatus));
            OnPropertyChanged(nameof(HeaterPower));
            OnPropertyChanged(nameof(LightState));
            OnPropertyChanged(nameof(LightBrightness));
            OnPropertyChanged(nameof(MotionState));
            OnPropertyChanged(nameof(MotionLastTrigger));
            OnPropertyChanged(nameof(DoorState));
            OnPropertyChanged(nameof(DoorLastAction));
            OnPropertyChanged(nameof(SmokeState));
            OnPropertyChanged(nameof(SmokeLevel));
            OnPropertyChanged(nameof(FanSpeed));
            OnPropertyChanged(nameof(FanEnergyMode));
            OnPropertyChanged(nameof(CameraState));
            OnPropertyChanged(nameof(CameraLastEvent));
        }

        // ------------- Properties for binding -------------

        public string TemperatureValue => $"{_temp.CurrentCelsius:0.0} °C";
        public string TemperatureStatus => _temp.Status;

        public string HeaterStatus => _heater.Status;
        public string HeaterPower => $"Power {_heater.PowerLevel}";

        public string LightState => _light.IsOn ? "ON" : "OFF";
        public string LightBrightness => $"Brightness {_light.BrightnessLevel}%";

        public string MotionState => _motion.Detected ? "Motion detected" : "No motion";
        public string MotionLastTrigger =>
            _motion.LastDetectedUtc?.ToLocalTime().ToString("HH:mm:ss") ?? "--";

        public string DoorState => _door.IsLocked ? "Locked" : "Unlocked";
        public string DoorLastAction => _door.LastAction;

        public string SmokeState => _smoke.SmokeDetected ? "SMOKE ALERT" : "Clear";
        public string SmokeLevel => $"Level {_smoke.SmokeLevel}";

        public string FanSpeed => _fan.Speed == 0 ? "OFF" : $"Speed {_fan.Speed}";
        public string FanEnergyMode => _fan.EnergySavingMode ? "Energy Saving: ON" : "Energy Saving: OFF";

        public string CameraState => _camera.IsStreaming ? "Streaming" : "Idle";
        public string CameraLastEvent => string.IsNullOrWhiteSpace(_camera.LastEvent) ? "--" : _camera.LastEvent;

        // ------------- Simple Actions (buttons) -------------

        public void ToggleLight()
        {
            if (_light.IsOn) _light.TurnOff();
            else _light.TurnOn();

            OnPropertyChanged(nameof(LightState));
            OnPropertyChanged(nameof(LightBrightness));
        }

        public void UnlockDoor()
        {
            _door.Unlock();
            OnPropertyChanged(nameof(DoorState));
            OnPropertyChanged(nameof(DoorLastAction));
        }

        public void ToggleFanEnergyMode()
        {
            _fan.EnergySavingMode = !_fan.EnergySavingMode;
            OnPropertyChanged(nameof(FanEnergyMode));
        }

        public void StartCamera()
        {
            _camera.Start();
            OnPropertyChanged(nameof(CameraState));
            OnPropertyChanged(nameof(CameraLastEvent));
        }

        public void StopCamera()
        {
            _camera.Stop();
            OnPropertyChanged(nameof(CameraState));
            OnPropertyChanged(nameof(CameraLastEvent));
        }

        // ------------- INotifyPropertyChanged -------------

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
