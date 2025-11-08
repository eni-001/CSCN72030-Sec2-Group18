using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public class SmokeDetector : IDevice
    {
        public string Name { get; }
        public bool SmokeDetected { get; private set; }
        public string Status => SmokeDetected ? "SMOKE ALERT" : "Clear";

        public SmokeDetector(string name = "Kitchen Smoke")
        {
            Name = name;
        }

        public void Trigger() => SmokeDetected = true;
        public void Clear() => SmokeDetected = false;
    }
}
