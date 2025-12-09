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

        // Sprint 1: base speed 
        public int Speed { get; private set; } // 0=Off, 1..3=Speeds
        public string Status => Speed == 0 ? "OFF" : $"Speed {Speed}";

        // Sprint 2: Energy saving + max speed
        public bool EnergySavingMode { get; set; } = false;

        private int MaxSpeed => EnergySavingMode ? 2 : 3;

        public FanController(string name = "Exhaust Fan")
        {
            Name = name;
        }

        public void Off() => Speed = 0;

        public void SetSpeed(int speed)
        {
            if (speed < 0) speed = 0;
            if (speed > MaxSpeed) speed = MaxSpeed;
            Speed = speed;
        }

        //Sprint 2: React based on smoke level (0–100)
        public void ReactToSmokeLevel(int smokeLevel)
        {
            if (smokeLevel <= 0)
            {
                Off();
                return;
            }

            if (smokeLevel >= 80)
            {
                SetSpeed(3);
            }
            else if (smokeLevel >= 50)
            {
                SetSpeed(2);
            }
            else
            {
                SetSpeed(1);
            }
        }

        // Sprint 2: Simple helper for "smoke yes/no"
        public void ReactToSmokeDetected(bool smokeDetected)
        {
            if (smokeDetected)
            {
                // Default to medium speed when just boolean info
                SetSpeed(2);
            }
            else
            {
                Off();
            }
        }
    }
}

