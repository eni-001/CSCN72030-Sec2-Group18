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
        public bool IsOn { get; private set; }
        public string Status => IsOn ? "ON" : "OFF";

        public LightController(string name = "Living Room Light")
        {
            Name = name;
        }

        public void TurnOn() => IsOn = true;
        public void TurnOff() => IsOn = false;
        public void Toggle() => IsOn = !IsOn;
    }
}

