using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Domain
{
    public class CameraController : IDevice
    {
        public string Name { get; }

        // Sprint 1 fields (keep)
        public bool IsStreaming { get; private set; }
        public DateTime? LastSnapshotUtc { get; private set; }

        // Sprint 2: New
        public bool AutoMode { get; set; } = true;
        public bool NightMode { get; set; } = false;
        public TimeSpan AutoStopDelay { get; set; } = TimeSpan.FromSeconds(15);
        public string LastEvent { get; private set; } = "";

        private DateTime? _lastStartedUtc;

        public string Status =>
            IsStreaming
                ? $"Streaming (snapshot {LastSnapshotUtc:HH:mm:ss} UTC) [{LastEvent}]"
                : "Idle";

        public CameraController(string name = "Front Door Camera")
        {
            Name = name;
        }

        // Sprint 1 (keep)
        public void Start()
        {
            IsStreaming = true;
            _lastStartedUtc = DateTime.UtcNow;
            LastEvent = "Manual start";
        }

        public void Stop()
        {
            IsStreaming = false;
            LastEvent = "Stopped";
        }

        public void Snapshot()
        {
            LastSnapshotUtc = DateTime.UtcNow;
            LastEvent = "Snapshot taken";
        }

        // Sprint 2: Motion-based activation
        public void ReactToMotion(bool motionDetected)
        {
            if (!AutoMode) return;

            if (motionDetected)
            {
                Start();
                LastEvent = "Started due to motion";
            }
        }

        // Sprint 2: Auto turn off after a timeout
        public void Update()
        {
            if (IsStreaming && _lastStartedUtc.HasValue)
            {
                if (DateTime.UtcNow - _lastStartedUtc > AutoStopDelay)
                {
                    Stop();
                    LastEvent = "Auto-stop (timeout)";
                }
            }
        }

        // Sprint 2: Snapshots every X seconds (optional enhancement)
        public void AutoSnapshotIfNeeded(TimeSpan interval)
        {
            if (!IsStreaming) return;

            if (!LastSnapshotUtc.HasValue ||
                (DateTime.UtcNow - LastSnapshotUtc) > interval)
            {
                Snapshot();
            }
        }
    }
}
