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
        public double CurrentCelsius { get; private set; }
        public string Status => $"{CurrentCelsius:0.0} °C";

        public TemperatureSensor(string name = "Indoor Temperature", double start = 22.0)
        {
            Name = name;
            CurrentCelsius = start;
        }

        public void Set(double valueC) => CurrentCelsius = valueC;
        public void Nudge(double delta) => CurrentCelsius += delta;
    }
}
