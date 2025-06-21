using System.Collections.Generic;
using UnityEngine;

namespace FirebaseWrapper
{
    /// <summary>
    /// Static utility class for easy event tracking in Firebase Analytics
    /// and recording exceptions in Crashlytics.
    /// </summary>
    public static class FirebaseTracker
    {
        private static bool IsInitialized => FirebaseManager.Instance != null && FirebaseManager.Instance.IsInitialized;

        #region Analytics Methods

        /// <summary>
        /// Log a simple event with no parameters
        /// </summary>
        /// <param name="eventName">Name of the event to log</param>
        public static void LogEvent(string eventName)
        {
            if (!IsInitialized) return;
            FirebaseManager.Instance.LogEvent(eventName);
        }

        /// <summary>
        /// Log an event with a single string parameter
        /// </summary>
        /// <param name="eventName">Name of the event to log</param>
        /// <param name="paramName">Parameter name</param>
        /// <param name="paramValue">Parameter value</param>
        public static void LogEvent(string eventName, string paramName, string paramValue)
        {
            if (!IsInitialized) return;
            
            var parameters = new Dictionary<string, object>
            {
                { paramName, paramValue }
            };
            
            FirebaseManager.Instance.LogEvent(eventName, parameters);
        }

        /// <summary>
        /// Log an event with a single int parameter
        /// </summary>
        /// <param name="eventName">Name of the event to log</param>
        /// <param name="paramName">Parameter name</param>
        /// <param name="paramValue">Parameter value</param>
        public static void LogEvent(string eventName, string paramName, int paramValue)
        {
            if (!IsInitialized) return;
            
            var parameters = new Dictionary<string, object>
            {
                { paramName, paramValue }
            };
            
            FirebaseManager.Instance.LogEvent(eventName, parameters);
        }

        /// <summary>
        /// Log an event with a single float parameter
        /// </summary>
        /// <param name="eventName">Name of the event to log</param>
        /// <param name="paramName">Parameter name</param>
        /// <param name="paramValue">Parameter value</param>
        public static void LogEvent(string eventName, string paramName, float paramValue)
        {
            if (!IsInitialized) return;
            
            var parameters = new Dictionary<string, object>
            {
                { paramName, paramValue }
            };
            
            FirebaseManager.Instance.LogEvent(eventName, parameters);
        }

        /// <summary>
        /// Log an event with a single boolean parameter
        /// </summary>
        /// <param name="eventName">Name of the event to log</param>
        /// <param name="paramName">Parameter name</param>
        /// <param name="paramValue">Parameter value</param>
        public static void LogEvent(string eventName, string paramName, bool paramValue)
        {
            if (!IsInitialized) return;
            
            var parameters = new Dictionary<string, object>
            {
                { paramName, paramValue }
            };
            
            FirebaseManager.Instance.LogEvent(eventName, parameters);
        }

        /// <summary>
        /// Log an event with multiple parameters
        /// </summary>
        /// <param name="eventName">Name of the event to log</param>
        /// <param name="parameters">Dictionary of parameters</param>
        public static void LogEvent(string eventName, Dictionary<string, object> parameters)
        {
            if (!IsInitialized) return;
            FirebaseManager.Instance.LogEvent(eventName, parameters);
        }

        /// <summary>
        /// Set a user property
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="value">Property value</param>
        public static void SetUserProperty(string name, string value)
        {
            if (!IsInitialized) return;
            FirebaseManager.Instance.SetUserProperty(name, value);
        }

        #endregion

        #region Crashlytics Methods

        /// <summary>
        /// Log a message to Crashlytics
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void LogMessage(string message)
        {
            if (!IsInitialized) return;
            FirebaseManager.Instance.LogCrashlyticsMessage(message);
        }

        /// <summary>
        /// Set a custom key for Crashlytics
        /// </summary>
        public static void SetCustomKey(string key, string value)
        {
            if (!IsInitialized) return;
            FirebaseManager.Instance.SetCrashlyticsKey(key, value);
        }

        /// <summary>
        /// Record a non-fatal exception
        /// </summary>
        /// <param name="exception">Exception to record</param>
        public static void RecordException(System.Exception exception)
        {
            if (!IsInitialized) return;
            FirebaseManager.Instance.RecordException(exception);
        }

        #endregion
    }
}
