using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Crashlytics;

namespace FirebaseWrapper
{
    /// <summary>
    /// Core Firebase manager that handles initialization and provides access to Firebase services.
    /// </summary>
    public class FirebaseManager : MonoBehaviour
    {
        private static FirebaseManager _instance;
        
        [SerializeField] private FirebaseConfig _config;
        
        private bool _isInitialized = false;
        private FirebaseApp _app;
        private DependencyStatus _dependencyStatus = DependencyStatus.UnavailableOther;
        
        public static FirebaseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("FirebaseManager");
                    _instance = go.AddComponent<FirebaseManager>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }
        
        public bool IsInitialized => _isInitialized;
        public FirebaseConfig Config => _config;
        public DependencyStatus Status => _dependencyStatus;
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (_config == null)
            {
                _config = FirebaseConfig.GetDefaultConfig();
                Debug.LogWarning("[FirebaseManager] No config assigned, using default config");
            }
            
            if (_config.InitializeOnAwake)
            {
                Initialize();
            }
        }
        
        /// <summary>
        /// Initialize Firebase services
        /// </summary>
        public async void Initialize()
        {
            if (_isInitialized)
            {
                Debug.Log("[FirebaseManager] Firebase is already initialized");
                return;
            }
            
            Debug.Log("[FirebaseManager] Initializing Firebase");
            
            try
            {
                // Check dependencies
                _dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
                if (_dependencyStatus != DependencyStatus.Available)
                {
                    Debug.LogError($"[FirebaseManager] Could not resolve all Firebase dependencies: {_dependencyStatus}");
                    FirebaseEvents.OnInitializationFailed?.Invoke(_dependencyStatus);
                    return;
                }
                
                // Initialize Firebase
                _app = FirebaseApp.DefaultInstance;
                
                // Initialize Analytics if enabled
                if (_config.EnableAnalytics)
                {
                    InitializeAnalytics();
                }
                
                // Initialize Crashlytics if enabled
                if (_config.EnableCrashlytics)
                {
                    InitializeCrashlytics();
                }
                
                _isInitialized = true;
                Debug.Log("[FirebaseManager] Firebase initialized successfully");
                FirebaseEvents.OnInitializationComplete?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[FirebaseManager] Firebase initialization failed: {ex}");
                FirebaseEvents.OnInitializationError?.Invoke(ex);
            }
        }
        
        private void InitializeAnalytics()
        {
            try
            {
                Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(_config.EnableAnalytics);
                Debug.Log("[FirebaseManager] Analytics initialized");
                FirebaseEvents.OnAnalyticsInitialized?.Invoke();
                
                if (_config.EnableUserTracking)
                {
                    string userId = GetOrCreateUserId();
                    SetUserId(userId);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[FirebaseManager] Analytics initialization failed: {e}");
                FirebaseEvents.OnServiceInitError?.Invoke(FirebaseService.Analytics, e);
            }
        }
        
        private void InitializeCrashlytics()
        {
            try
            {
                Crashlytics.IsCrashlyticsCollectionEnabled = _config.EnableCrashlytics;
                Debug.Log("[FirebaseManager] Crashlytics initialized");
                FirebaseEvents.OnCrashlyticsInitialized?.Invoke();
                
                if (_config.EnableUserTracking)
                {
                    string userId = GetOrCreateUserId();
                    Crashlytics.SetUserId(userId);
                }
                
                if (_config.CustomKeys != null && _config.CustomKeys.Count > 0)
                {
                    foreach (var key in _config.CustomKeys)
                    {
                        Crashlytics.SetCustomKey(key.Key, key.Value.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[FirebaseManager] Crashlytics initialization failed: {e}");
                FirebaseEvents.OnServiceInitError?.Invoke(FirebaseService.Crashlytics, e);
            }
        }
        
        private string GetOrCreateUserId()
        {
            string userId = PlayerPrefs.GetString("FirebaseUserId", "");
            if (string.IsNullOrEmpty(userId))
            {
                userId = Guid.NewGuid().ToString();
                PlayerPrefs.SetString("FirebaseUserId", userId);
                PlayerPrefs.Save();
            }
            return userId;
        }
        
        #region Analytics Methods
        
        /// <summary>
        /// Set user ID for Analytics and Crashlytics
        /// </summary>
        public void SetUserId(string userId)
        {
            if (!_isInitialized || string.IsNullOrEmpty(userId)) 
                return;
            
            if (_config.EnableAnalytics)
            {
                Firebase.Analytics.FirebaseAnalytics.SetUserId(userId);
            }
            
            if (_config.EnableCrashlytics)
            {
                Crashlytics.SetUserId(userId);
            }
        }
        
        /// <summary>
        /// Set user property for Analytics
        /// </summary>
        public void SetUserProperty(string name, string value)
        {
            if (!_isInitialized || !_config.EnableAnalytics) 
                return;
            
            Firebase.Analytics.FirebaseAnalytics.SetUserProperty(name, value);
        }
        
        /// <summary>
        /// Log event with parameters
        /// </summary>
        public void LogEvent(string name, IDictionary<string, object> parameters = null)
        {
            if (!_isInitialized || !_config.EnableAnalytics) 
                return;
            
            if (parameters == null)
            {
                Firebase.Analytics.FirebaseAnalytics.LogEvent(name);
                return;
            }
            
            var firebaseParams = new List<Parameter>();
            foreach (var param in parameters)
            {
                if (param.Value is string stringValue)
                    firebaseParams.Add(new Parameter(param.Key, stringValue));
                else if (param.Value is double doubleValue)
                    firebaseParams.Add(new Parameter(param.Key, doubleValue));
                else if (param.Value is long longValue)
                    firebaseParams.Add(new Parameter(param.Key, longValue));
                else if (param.Value is int intValue)
                    firebaseParams.Add(new Parameter(param.Key, (long)intValue));
                else if (param.Value is float floatValue)
                    firebaseParams.Add(new Parameter(param.Key, (double)floatValue));
                else if (param.Value is bool boolValue)
                    firebaseParams.Add(new Parameter(param.Key, boolValue ? "true" : "false"));
            }
            
            Firebase.Analytics.FirebaseAnalytics.LogEvent(name, firebaseParams.ToArray());
        }
        
        #endregion
        
        #region Crashlytics Methods
        
        /// <summary>
        /// Log a message to Crashlytics (shown in crash reports)
        /// </summary>
        public void LogCrashlyticsMessage(string message)
        {
            if (!_isInitialized || !_config.EnableCrashlytics) 
                return;
            
            Crashlytics.Log(message);
        }
        
        /// <summary>
        /// Set a custom key for Crashlytics
        /// </summary>
        public void SetCrashlyticsKey(string key, string value)
        {
            if (!_isInitialized || !_config.EnableCrashlytics) 
                return;
            
            Crashlytics.SetCustomKey(key, value);
        }
        
        /// <summary>
        /// Record a non-fatal exception
        /// </summary>
        public void RecordException(Exception exception)
        {
            if (!_isInitialized || !_config.EnableCrashlytics) 
                return;
            
            Crashlytics.LogException(exception);
        }
        
        #endregion
        
        private void OnDestroy()
        {
            FirebaseEvents.OnInitializationComplete = null;
            FirebaseEvents.OnInitializationFailed = null;
            FirebaseEvents.OnInitializationError = null;
            FirebaseEvents.OnAnalyticsInitialized = null;
            FirebaseEvents.OnCrashlyticsInitialized = null;
            FirebaseEvents.OnServiceInitError = null;
        }
    }
}
