# Firebase Wrapper for Unity

A modular wrapper for Firebase services (Analytics, Crashlytics, etc.) that can be used across multiple Unity games.

## Features

- Easy integration of Firebase Analytics and Crashlytics
- Modular architecture for reuse across multiple games
- Separation of core functionality from game-specific implementation
- Static utility classes for simple event tracking
- ScriptableObject-based configuration
- Support for custom events and parameters
- Editor tools for configuration

## Installation

### Option 1: Unity Package Manager (UPM) via Git URL

1. Open the Unity Package Manager (Window > Package Manager)
2. Click the "+" button in the top-left corner
3. Select "Add package from git URL..."
4. Enter `https://github.com/yourusername/firebase-wrapper.git`
5. Click "Add"

### Option 2: Manual Installation

1. Clone or download this repository
2. Copy the `FirebaseWrapper` folder to your Unity project's Assets folder

## Setup

### Prerequisites

1. Firebase SDK for Unity installed in your project
   - Firebase Analytics
   - Firebase Crashlytics

### Basic Setup

1. Right-click in your Project window
2. Select Create > Firebase > Firebase Config
3. Configure the settings in the inspector
4. Create a GameObject in your scene and add the FirebaseManager component
5. Assign your Firebase Config to the manager

For game-specific implementations:
1. Create a game-specific Firebase Manager extending the base FirebaseManager
2. Create a game-specific config asset

## Usage

### Basic Usage

```csharp
// Initialize Firebase (if not set to auto-initialize)
FirebaseManager.Instance.Initialize();

// Log a simple event
FirebaseTracker.LogEvent("my_event");

// Log an event with parameters
Dictionary<string, object> parameters = new Dictionary<string, object>
{
    { "parameter_name", "parameter_value" },
    { "score", 100 }
};
FirebaseTracker.LogEvent("event_with_params", parameters);

// Record an exception in Crashlytics
try {
    // Your code that might throw an exception
}
catch (Exception e) {
    FirebaseTracker.RecordException(e);
}
```

### Game-specific Usage (Color Blast example)

```csharp
// Log game-specific events
ColorBlastFirebaseTracker.LogLevelStart(1, "Level 1", "classic");
ColorBlastFirebaseTracker.LogGameEnd("adventure", 500, 120.5f);
ColorBlastFirebaseTracker.LogAdView("rewarded", "level_complete");
```

## Structure

```
FirebaseWrapper/
├── Runtime/
│   ├── FirebaseManager.cs        # Core Firebase manager
│   ├── FirebaseConfig.cs         # Configuration ScriptableObject
│   ├── FirebaseEvents.cs         # Events for Firebase services
│   ├── FirebaseTracker.cs        # Static utility for tracking
│   └── FirebaseConstants.cs      # Common event and parameter names
├── Editor/
│   └── FirebaseConfigEditor.cs   # Custom editor for configuration
├── ProjectSpecific/
│   ├── ColorBlastFirebaseManager.cs   # Game-specific manager
│   ├── ColorBlastFirebaseConfig.cs    # Game-specific config
│   └── ColorBlastFirebaseTracker.cs   # Game-specific tracker
└── Samples~/
    ├── BasicSetup/
    └── ColorBlastIntegration/
```

## License

[MIT License](LICENSE.md)

## Support

For issues and feature requests, please [open an issue](https://github.com/yourusername/firebase-wrapper/issues) on the GitHub repository.
