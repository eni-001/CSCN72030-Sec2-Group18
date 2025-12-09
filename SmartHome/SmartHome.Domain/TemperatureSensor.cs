using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public class TemperatureSensor : IDevice
    {
        public string Name { get; }

        // Sprint 2: Added thresholds for auto-control logic
        public double CurrentCelsius { get; private set; }
        public double MinThreshold { get; set; } = 18.0;
        public double MaxThreshold { get; set; } = 26.0;

        public string Status => $"{CurrentCelsius:0.0} °C";

        public TemperatureSensor(string name = "Indoor Temperature", double start = 22.0)
        {
            Name = name;
            CurrentCelsius = start;
        }

        // Sprint 1 Method (keep)
        public void Set(double valueC) => CurrentCelsius = valueC;

        // Sprint 1 Method (keep)
        public void Nudge(double delta) => CurrentCelsius += delta;

        // Sprint 2: Added "cooling" and "heating" helpers for integration
        public void CoolDown(double amount = 0.5) => CurrentCelsius -= amount;
        public void HeatUp(double amount = 0.5) => CurrentCelsius += amount;

        // Sprint 2: Added checks for system automation
        public bool IsTooHot() => CurrentCelsius > MaxThreshold;
        public bool IsTooCold() => CurrentCelsius < MinThreshold;

        // Sprint 2: Simulate natural fluctuation for realism
        public void SimulateFluctuation()
        {
            Random rand = new Random();
            double change = rand.NextDouble() * 0.8 - 0.4; // -0.4 to +0.4
            CurrentCelsius += change;
        }
    }
}
