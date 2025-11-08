using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public class HeaterController : IDevice
    {
        public string Name { get; }
        public double SetpointC { get; private set; }
        public bool IsHeating { get; private set; }
        public string Status => IsHeating ? $"Heating to {SetpointC:0.0}°C" : $"Idle (set {SetpointC:0.0}°C)";

        private readonly TemperatureSensor _sensor;

        public HeaterController(TemperatureSensor sensor, string name = "Heater")
        {
            Name = name;
            _sensor = sensor;
            SetpointC = 22.0;
        }

        public void SetSetpoint(double celsius) => SetpointC = celsius;

        public void Tick()
        {
            IsHeating = _sensor.CurrentCelsius < SetpointC - 0.2;
            if (IsHeating) _sensor.Nudge(0.3);
        }
    }
}

