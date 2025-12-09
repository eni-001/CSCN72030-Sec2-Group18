
# Smart Home Monitoring & Control System – CSCN72030 (Project III)

**Group 18 – Fall 2025**

  

This repository contains the full implementation of our Smart Home Monitoring & Control Solution (SCADA/HMI). The system simulates real-time monitoring and control of home devices using C# and WPF, with automated tests validating the logic of all device modules.

  

## Team Members

-  **Emmanuel**

-  **Aya**

-  **Lanna**

-  **Abdulkarim**

  

---

  

# Project Overview

The Smart Home Monitoring & Control System provides a centralized dashboard that allows users to:

  

- Monitor home temperature

- Control lighting (on/off, brightness, auto mode)

- Lock/unlock the front door

- Detect motion in the home

- Detect smoke levels

- Control ventilation fans

- Operate and monitor a security camera

- View all device statuses in real time in a unified interface

  

The project follows Agile/Scrum methodology with 3 sprints:

**Sprint 1 – Core device logic**

**Sprint 2 – Device interaction & automation**

**Sprint 3 – Full GUI and system integration**

  

---

  

# Features Implemented

The final system includes fully functional modules for:

  

### TemperatureSensor

- Real-time temperature updates

- Threshold detection

- Heating/cooling interactions

  

### LightController

- ON/OFF

- Brightness control

- Automatic lighting based on temperature

  

### DoorLockController

- Manual lock/unlock

- Auto-lock after timeout

- Unlock triggered by motion

  

### MotionSensor

- Motion detection

- Auto-clear behavior

- Integration with door & camera

  

### SmokeDetector

- Smoke level detection

- Auto-clear

- Critical smoke alerting

  

### FanController

- 3-speed ventilation control

- Automatic reaction to smoke

  

### HeaterController

- Heating logic with setpoint

- Auto heating

- Power level adjustment

  

### CameraController

- Start/stop streaming

- Snapshot

- Auto-start on motion

  

---

  

# Dashboard (WPF GUI)

The GUI allows users to:

  

- View the status of all 8 devices

- Adjust temperature and lighting

- Observe motion, smoke, and device alerts

- See real-time device changes

- Interact with a clean, organized interface

  

Screenshot examples included in documentation and slides.

  

---

  

# Automated Testing

The project includes **32 automated unit tests**, covering:

  

- Temperature logic

- Lighting behavior

- Motion & door interaction

- Fan and smoke logic

- Camera behavior

- Coordinator automation rules

  

Additional system tests were performed through:

  

- GUI interaction

- UX Testing Plan (5 scenarios)

  

**All tests passed successfully.**

  

---

  

# Folder Structure

```plaintext

SmartHomeSolution/

│

├── SmartHome.Domain/ # All device classes + coordinator logic

├── SmartHome.GUI/ # WPF application (dashboard)

├── SmartHome.Tests/ # Unit & integration tests

└── README.md # Project documentation