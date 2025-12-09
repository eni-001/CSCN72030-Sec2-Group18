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

        // Sprint 1 
        public double SetpointC { get; private set; }
        public bool IsHeating { get; private set; }

        // Sprint 2: Added details for GUI and automation
        public int PowerLevel { get; private set; } = 1;  // 1–5
        public bool AutoMode { get; set; } = true;
        public string LastEvent { get; private set; } = "";

        private readonly TemperatureSensor _sensor;

        public string Status =>
            IsHeating
                ? $"Heating to {SetpointC:0.0}°C (Power {PowerLevel})"
                : $"Idle (set {SetpointC:0.0}°C)";

        public HeaterController(TemperatureSensor sensor, string name = "Heater")
        {
            Name = name;
            _sensor = sensor;
            SetpointC = 22.0;
        }

        // Sprint 1 
        public void SetSetpoint(double celsius) => SetpointC = celsius;

        // Sprint 2: Manual power adjustment
        public void SetPower(int level)
        {
            PowerLevel = Math.Clamp(level, 1, 5);
        }

        // Sprint 2: For automatic behaviour (called each cycle)
        public void ReactToTemperature()
        {
            if (!AutoMode) return;

            if (_sensor.CurrentCelsius < SetpointC - 0.3)
            {
                IsHeating = true;
                _sensor.HeatUp(0.4 + (PowerLevel * 0.1));
                LastEvent = "Auto-heating";
            }
            else if (_sensor.CurrentCelsius > SetpointC + 0.2)
            {
                IsHeating = false;
                LastEvent = "Heater idle";
            }
        }

        // Sprint 1 Tick, but improved for Sprint 2
        public void Tick()
        {
            if (_sensor.CurrentCelsius < SetpointC - 0.2)
            {
                IsHeating = true;
                _sensor.HeatUp(0.3);
                LastEvent = "Heating (Tick)";
            }
            else
            {
                IsHeating = false;
            }
        }
    }
}

