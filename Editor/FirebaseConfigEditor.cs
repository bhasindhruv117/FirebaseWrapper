using UnityEngine;
using UnityEditor;
using System.IO;
using FirebaseWrapper;

namespace FirebaseWrapper.Editor
{
    [CustomEditor(typeof(FirebaseConfig))]
    public class FirebaseConfigEditor : UnityEditor.Editor
    {
        private SerializedProperty _initializeOnAwake;
        private SerializedProperty _enableAnalytics;
        private SerializedProperty _enableCrashlytics;
        private SerializedProperty _enableUserTracking;
        private SerializedProperty _sessionTimeoutSeconds;
        private SerializedProperty _verboseLogging;
        private SerializedProperty _customKeys;
        
        private bool _showFirebaseDependencies = false;
        
        private void OnEnable()
        {
            _initializeOnAwake = serializedObject.FindProperty("InitializeOnAwake");
            _enableAnalytics = serializedObject.FindProperty("EnableAnalytics");
            _enableCrashlytics = serializedObject.FindProperty("EnableCrashlytics");
            _enableUserTracking = serializedObject.FindProperty("EnableUserTracking");
            _sessionTimeoutSeconds = serializedObject.FindProperty("SessionTimeoutSeconds");
            _verboseLogging = serializedObject.FindProperty("VerboseLogging");
            _customKeys = serializedObject.FindProperty("CustomKeys");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Firebase Configuration", EditorStyles.boldLabel);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Initialization", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_initializeOnAwake);
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Services", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_enableAnalytics);
            EditorGUILayout.PropertyField(_enableCrashlytics);
            EditorGUILayout.PropertyField(_enableUserTracking);
            
            if (_enableAnalytics.boolValue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Analytics Settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_sessionTimeoutSeconds);
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_verboseLogging);
            
            if (_enableCrashlytics.boolValue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Crashlytics", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_customKeys, true);
            }
            
            // Firebase dependency check section
            EditorGUILayout.Space(10);
            _showFirebaseDependencies = EditorGUILayout.Foldout(_showFirebaseDependencies, "Firebase SDK Status", true, EditorStyles.foldoutHeader);
            
            if (_showFirebaseDependencies)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                
                bool hasAnalytics = CheckFirebaseDependency("Firebase.Analytics");
                bool hasCrashlytics = CheckFirebaseDependency("Firebase.Crashlytics");
                bool hasCore = CheckFirebaseDependency("Firebase.App");
                
                EditorGUILayout.LabelField("Firebase Core:", hasCore ? "✓ Installed" : "✗ Not Found", hasCore ? EditorStyles.label : GetErrorStyle());
                EditorGUILayout.LabelField("Firebase Analytics:", hasAnalytics ? "✓ Installed" : "✗ Not Found", hasAnalytics ? EditorStyles.label : GetErrorStyle());
                EditorGUILayout.LabelField("Firebase Crashlytics:", hasCrashlytics ? "✓ Installed" : "✗ Not Found", hasCrashlytics ? EditorStyles.label : GetErrorStyle());
                
                if (!hasAnalytics || !hasCrashlytics || !hasCore)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.HelpBox("Some Firebase dependencies are missing. Please install the required Firebase packages through the Firebase SDK.", MessageType.Warning);
                    
                    if (GUILayout.Button("Open Firebase Documentation"))
                    {
                        Application.OpenURL("https://firebase.google.com/docs/unity/setup");
                    }
                }
                
                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Create Default Resources Folder"))
            {
                CreateResourcesFolder();
            }
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private bool CheckFirebaseDependency(string assemblyName)
        {
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.GetName().Name == assemblyName)
                {
                    return true;
                }
            }
            return false;
        }
        
        private GUIStyle GetErrorStyle()
        {
            GUIStyle style = new GUIStyle(EditorStyles.label);
            style.normal.textColor = Color.red;
            return style;
        }
        
        private void CreateResourcesFolder()
        {
            // Create Resources folder if it doesn't exist
            string resourcesPath = "Assets/Resources";
            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
                AssetDatabase.Refresh();
            }
            
            // Check if the config already exists in Resources
            string configAssetPath = "Assets/Resources/FirebaseConfig.asset";
            FirebaseConfig existingConfig = AssetDatabase.LoadAssetAtPath<FirebaseConfig>(configAssetPath);
            
            if (existingConfig == null)
            {
                // Create a new config and save it in Resources
                FirebaseConfig config = ScriptableObject.CreateInstance<FirebaseConfig>();
                AssetDatabase.CreateAsset(config, configAssetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                EditorUtility.DisplayDialog("Firebase Config", "FirebaseConfig asset created in Resources folder.", "OK");
                Selection.activeObject = config;
            }
            else
            {
                EditorUtility.DisplayDialog("Firebase Config", "FirebaseConfig already exists in Resources folder.", "OK");
                Selection.activeObject = existingConfig;
            }
        }
    }
    
    [CustomEditor(typeof(ColorBlast.ColorBlastFirebaseConfig))]
    public class ColorBlastFirebaseConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Color Blast Firebase Configuration", EditorStyles.boldLabel);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space();
            
            // Draw default inspector properties
            DrawDefaultInspector();
            
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Create Default Resources Folder"))
            {
                CreateResourcesFolder();
            }
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private void CreateResourcesFolder()
        {
            // Create Resources folder if it doesn't exist
            string resourcesPath = "Assets/Resources";
            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
                AssetDatabase.Refresh();
            }
            
            // Check if the config already exists in Resources
            string configAssetPath = "Assets/Resources/ColorBlastFirebaseConfig.asset";
            ColorBlast.ColorBlastFirebaseConfig existingConfig = AssetDatabase.LoadAssetAtPath<ColorBlast.ColorBlastFirebaseConfig>(configAssetPath);
            
            if (existingConfig == null)
            {
                // Create a new config and save it in Resources
                ColorBlast.ColorBlastFirebaseConfig config = ScriptableObject.CreateInstance<ColorBlast.ColorBlastFirebaseConfig>();
                AssetDatabase.CreateAsset(config, configAssetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                EditorUtility.DisplayDialog("Color Blast Firebase Config", "ColorBlastFirebaseConfig asset created in Resources folder.", "OK");
                Selection.activeObject = config;
            }
            else
            {
                EditorUtility.DisplayDialog("Color Blast Firebase Config", "ColorBlastFirebaseConfig already exists in Resources folder.", "OK");
                Selection.activeObject = existingConfig;
            }
        }
    }
}
