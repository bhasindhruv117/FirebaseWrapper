using System;
using System.Collections.Generic;
using UnityEngine;

namespace FirebaseWrapper
{
    /// <summary>
    /// Configuration for Firebase services.
    /// Create a new FirebaseConfig asset via Assets > Create > Firebase > Firebase Config
    /// </summary>
    [CreateAssetMenu(fileName = "FirebaseConfig", menuName = "Firebase/Firebase Config")]
    public class FirebaseConfig : ScriptableObject
    {
        [Header("Initialization")]
        [Tooltip("Initialize Firebase automatically on Awake")]
        public bool InitializeOnAwake = true;
        
        [Header("Services")]
        [Tooltip("Enable Firebase Analytics")]
        public bool EnableAnalytics = true;
        
        [Tooltip("Enable Firebase Crashlytics")]
        public bool EnableCrashlytics = true;
        
        [Tooltip("Enable user tracking (generates persistent user ID)")]
        public bool EnableUserTracking = true;
        
        [Header("Analytics Settings")]
        [Tooltip("Session timeout in seconds (default: 1800 = 30 minutes)")]
        public int SessionTimeoutSeconds = 1800;
        
        [Header("Debug")]
        [Tooltip("Enable verbose logging for debugging")]
        public bool VerboseLogging = false;
        
        [Header("Crashlytics")]
        public List<CustomKeyValue> CustomKeys = new List<CustomKeyValue>();
        
        /// <summary>
        /// Get the default config from Resources
        /// </summary>
        public static FirebaseConfig GetDefaultConfig()
        {
            var config = Resources.Load<FirebaseConfig>("FirebaseConfig");
            if (config == null)
            {
                // Return runtime-created default config if none exists
                config = CreateInstance<FirebaseConfig>();
                config.name = "FirebaseConfig (Default)";
            }
            return config;
        }
    }
    
    [Serializable]
    public class CustomKeyValue
    {
        public string Key;
        public FirebaseValueType ValueType;
        public string StringValue;
        public int IntValue;
        public float FloatValue;
        public bool BoolValue;
        
        public object Value
        {
            get
            {
                switch (ValueType)
                {
                    case FirebaseValueType.String:
                        return StringValue;
                    case FirebaseValueType.Int:
                        return IntValue;
                    case FirebaseValueType.Float:
                        return FloatValue;
                    case FirebaseValueType.Bool:
                        return BoolValue;
                    default:
                        return StringValue;
                }
            }
        }
    }
    
    public enum FirebaseValueType
    {
        String,
        Int,
        Float,
        Bool
    }
}
