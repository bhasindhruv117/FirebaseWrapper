using System;
using Firebase;

namespace FirebaseWrapper
{
    /// <summary>
    /// Events for Firebase initialization and service events
    /// </summary>
    public static class FirebaseEvents
    {
        // Initialization events
        public static Action OnInitializationComplete;
        public static Action<DependencyStatus> OnInitializationFailed;
        public static Action<Exception> OnInitializationError;
        
        // Service-specific initialization events
        public static Action OnAnalyticsInitialized;
        public static Action OnCrashlyticsInitialized;
        public static Action<FirebaseService, Exception> OnServiceInitError;
    }
    
    /// <summary>
    /// Available Firebase services
    /// </summary>
    public enum FirebaseService
    {
        Core,
        Analytics,
        Crashlytics,
        RemoteConfig,
        Database,
        Auth,
        Storage,
        Functions,
        Messaging
    }
}
