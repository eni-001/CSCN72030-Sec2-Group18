using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using SmartHomeHub.wpf.Interfaces;
using SmartHome.Domain;
using SmartHome.Wpf.Pages;

namespace SmartHome.Wpf
{
    public partial class MainWindow : Window
    {
        private TemperatureSensor _temp;
        private LightController _light;
        private MotionSensor _motion;
        private DoorLockController _door;
        private SmokeDetector _smoke;
        private FanController _fan;
        private HeaterController _heater;
        private CameraController _camera;
        private SmartHomeCoordinator _home;

        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDomainObjects();
            StartSimulationTimer();

            // Load dashboard by default
            LoadPage(new DashboardPage(_home));
        }

        // ============================================================
        // PAGE LOADER (required for navigation)
        // ============================================================
        private void LoadPage(UserControl page)
        {
            MainContent.Content = page;
        }

        // ============================================================
        // INITIALIZE DOMAIN OBJECTS
        // ============================================================
        private void InitializeDomainObjects()
        {
            _temp = new TemperatureSensor();
            _light = new LightController();
            _motion = new MotionSensor();
            _door = new DoorLockController();
            _smoke = new SmokeDetector();
            _fan = new FanController();
            _heater = new HeaterController(_temp);
            _camera = new CameraController();

            _home = new SmartHomeCoordinator(
                _temp, _light, _motion, _door,
                _smoke, _fan, _heater, _camera);
        }

        // ============================================================
        // SIMULATION TIMER
        // ============================================================
        private void StartSimulationTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += (s, e) =>
            {
                _home.RunSprint2Simulation();
                UpdateClockAndSummary();

                // Let the current page update itself
                if (MainContent.Content is IRefreshable refreshable)
                    refreshable.Refresh();
            };


            _timer.Start();
        }

        // ============================================================
        // HEADER SUMMARY + CLOCK
        // ============================================================
        private void UpdateClockAndSummary()
        {
            var now = DateTime.Now;
            string partOfDay =
                now.Hour < 12 ? "Morning" :
                now.Hour < 18 ? "Afternoon" : "Evening";

            ClockText.Text = $"{partOfDay} · {now:hh:mm tt}";
            SummaryText.Text = "8 devices online · 0 alerts";
        }

        // ============================================================
        // LEFT NAVIGATION BUTTON HANDLERS
        // ============================================================
        private void DashboardNav_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(new DashboardPage(_home));
        }

        private void ClimateNav_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(new ClimatePage(_home));
        }

        private void LightingNav_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(new LightingPage(_home));
        }

        private void SecurityNav_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(new SecurityPage(_home));
        }

        private void EnergyNav_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(new EnergyPage(_home));
        }

        private void SettingsNav_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(new SettingsPage(_home));
        }
    }
}
