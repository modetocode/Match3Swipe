using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Modetocode.Swiper.GameSettings {
    public static class GameSettingsManager {
        private static IDictionary<string, object> instances = new Dictionary<string, object>();

        private static T GetInstance<T>(string relativePath) where T : ScriptableObject {
            object instance;
            if (!instances.TryGetValue(relativePath, out instance) || instance == null) {
                instance = Resources.Load(Constants.GameSettings.GameSettingsRelativePath + relativePath) as T;
                if (instance == null) {
                    instance = CreateNewDefault<T>(relativePath);
                    instances.Add(relativePath, instance);
                }
                else {
                    instances.Add(relativePath, instance);
                }
            }
            return instance as T;
        }

        private static T CreateNewDefault<T>(string relativePath) where T : ScriptableObject {
            T instance = ScriptableObject.CreateInstance<T>();
#if UNITY_EDITOR
            string resourcesPath = Path.Combine(Constants.GameSettings.ResourcesPath, Constants.GameSettings.GameSettingsRelativePath);
            string assetPath = string.Concat(relativePath, Constants.GameSettings.AssetsExtension);
            string fullPath = Path.Combine(resourcesPath, assetPath);
            AssetDatabase.CreateAsset(instance as ScriptableObject, fullPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
            return instance as T;
        }

        public static GameConstants GetGameConstants() {
            return GetInstance<GameConstants>(Constants.GameSettings.GameConstantsAssetName);
        }

#if UNITY_EDITOR
        [MenuItem(Constants.GameSettings.GameSettingsMenuName + "/" + Constants.GameSettings.GameConstantsAssetName)]
        private static void DisplayGameSettingsData() {
            Selection.activeObject = GetGameConstants();
        }
#endif

    }
}
