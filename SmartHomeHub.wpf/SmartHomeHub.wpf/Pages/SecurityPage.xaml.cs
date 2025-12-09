using SmartHome.Domain;
using SmartHomeHub.wpf.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SmartHome.Wpf.Pages
{
    public partial class SecurityPage : UserControl, IRefreshable
    {
        private readonly SmartHomeCoordinator _home;

        public SecurityPage(SmartHomeCoordinator home)
        {
            InitializeComponent();
            _home = home;
            Refresh();
        }

        public void Refresh()
        {
            var door = _home.Door;
            var motion = _home.Motion;
            var camera = _home.Camera;

            DoorStateText.Text = door.IsLocked ? "Locked" : "Unlocked";
            DoorLastActionText.Text = door.LastAction;

            MotionStateText.Text = motion.Detected ? "Motion Detected" : "No Motion";
            MotionLastText.Text = motion.LastDetectedUtc?.ToLocalTime().ToString("HH:mm:ss") ?? "--";

            CameraStateText.Text = camera.IsStreaming ? "Streaming" : "Idle";
            CameraEventText.Text = string.IsNullOrWhiteSpace(camera.LastEvent)
                ? "Last event: --"
                : $"Last event: {camera.LastEvent}";
        }

        private void ToggleDoor_Click(object sender, RoutedEventArgs e)
        {
            var door = _home.Door;
            if (door.IsLocked)
                door.Unlock();
            else
                door.Lock();

            Refresh();
        }

        private void SimulateMotion_Click(object sender, RoutedEventArgs e)
        {
            _home.Motion.Trigger();
            _home.Camera.ReactToMotion(true);
            _home.Camera.Snapshot();
            Refresh();
        }

        private void CameraStart_Click(object sender, RoutedEventArgs e)
        {
            _home.Camera.Start();
            Refresh();
        }

        private void CameraSnapshot_Click(object sender, RoutedEventArgs e)
        {
            _home.Camera.Snapshot();
            Refresh();
        }

        private void CameraStop_Click(object sender, RoutedEventArgs e)
        {
            _home.Camera.Stop();
            Refresh();
        }
    }
}
