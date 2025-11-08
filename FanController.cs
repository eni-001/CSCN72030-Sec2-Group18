using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public class FanController : IDevice
    {
        public string Name { get; }
        public int Speed { get; private set; } // 0=Off, 1..3=Speeds
        public string Status => Speed == 0 ? "OFF" : $"Speed {Speed}";

        public FanController(string name = "Exhaust Fan")
        {
            Name = name;
        }

        public void Off() => Speed = 0;
        public void SetSpeed(int speed)
        {
            if (speed < 0) speed = 0;
            if (speed > 3) speed = 3;
            Speed = speed;
        }
    }
}

