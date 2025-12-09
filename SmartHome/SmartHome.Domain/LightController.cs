using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public class LightController : IDevice
    {
        public string Name { get; }

        // Sprint 2: Added brightness & auto mode
        public bool IsOn { get; private set; }
        public int BrightnessLevel { get; private set; } = 100;  // 0–100
        public bool AutoMode { get; set; } = false;

        public string Status => IsOn ? $"ON (Brightness {BrightnessLevel}%)" : "OFF";

        public LightController(string name = "Living Room Light")
        {
            Name = name;
        }

        // Sprint 1 
        public void TurnOn() => IsOn = true;
        public void TurnOff()
        {
            IsOn = false;
            BrightnessLevel = 0;
        }

        public void Toggle() => IsOn = !IsOn;

        // Sprint 2: Adjust brightness manually
        public void SetBrightness(int level)
        {
            BrightnessLevel = Math.Clamp(level, 0, 100);
            if (BrightnessLevel > 0) IsOn = true;
        }

        // Sprint 2: Adjust based on temperature sensor
        public void AdjustBasedOnTemperature(double temperature)
        {
            if (!AutoMode) return;

            if (temperature > 26)
            {
                TurnOff();
            }
            else if (temperature < 20)
            {
                TurnOn();
                SetBrightness(80);
            }
            else
            {
                TurnOn();
                SetBrightness(50);
            }
        }
    }
}

