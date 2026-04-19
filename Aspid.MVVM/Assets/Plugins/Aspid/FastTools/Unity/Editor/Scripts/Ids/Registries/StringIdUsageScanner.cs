#nullable enable
using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal static class StringIdUsageScanner
    {
        public static int CountUsages(Type? structType, string name)
        {
            var idFieldName = GetStringIdFieldName(structType);
            if (idFieldName == null) return 0;

            try
            {
                return FindUsages(idFieldName, name, showProgress: true).Count;
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        public static List<(UnityEngine.Object asset, string path)> FindUsages(
            string idFieldName, string name, bool showProgress = false)
        {
            var results = new List<(UnityEngine.Object, string)>();
            ScanAssets("t:ScriptableObject", idFieldName, name, results, showProgress, "Scanning ScriptableObjects");
            ScanPrefabs(idFieldName, name, results, showProgress);
            ScanScenes(idFieldName, name, results, showProgress);
            return results;
        }

        private static string? GetStringIdFieldName(Type? structType) =>
            structType != null && typeof(IId).IsAssignableFrom(structType)
                ? Constants.StringIdFieldName
                : null;

        private static void ScanAssets(string filter, string idFieldName, string name,
            List<(UnityEngine.Object, string)> results, bool showProgress, string progressTitle)
        {
            var guids = AssetDatabase.FindAssets(filter, new[] { "Assets" });
            for (int i = 0; i < guids.Length; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);

                if (showProgress)
                    EditorUtility.DisplayProgressBar(progressTitle, assetPath, (float)i / guids.Length);

                var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
                if (asset == null) continue;

                ScanObject(asset, idFieldName, name, results);
            }
        }

        private static void ScanPrefabs(string idFieldName, string name,
            List<(UnityEngine.Object, string)> results, bool showProgress)
        {
            var guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets" });
            for (int i = 0; i < guids.Length; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                if (showProgress)
                    EditorUtility.DisplayProgressBar("Scanning Prefabs", assetPath, (float)i / guids.Length);

                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if (prefab == null) continue;

                foreach (var component in prefab.GetComponentsInChildren<Component>(includeInactive: true))
                {
                    if (component != null)
                        ScanObject(component, idFieldName, name, results);
                }
            }
        }

        private static void ScanScenes(string idFieldName, string name,
            List<(UnityEngine.Object asset, string path)> results, bool showProgress)
        {
            var guids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets" });
            for (int i = 0; i < guids.Length; i++)
            {
                var scenePath = AssetDatabase.GUIDToAssetPath(guids[i]);
                if (showProgress)
                    EditorUtility.DisplayProgressBar("Scanning Scenes", scenePath, (float)i / guids.Length);

                var alreadyOpen = false;
                for (int s = 0; s < SceneManager.sceneCount; s++)
                {
                    if (SceneManager.GetSceneAt(s).path == scenePath)
                    {
                        alreadyOpen = true;
                        break;
                    }
                }

                var scene = alreadyOpen
                    ? SceneManager.GetSceneByPath(scenePath)
                    : EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

                if (!scene.IsValid())
                {
                    if (!alreadyOpen) EditorSceneManager.CloseScene(scene, true);
                    continue;
                }

                foreach (var root in scene.GetRootGameObjects())
                {
                    foreach (var component in root.GetComponentsInChildren<Component>(includeInactive: true))
                    {
                        if (component != null)
                            ScanObject(component, idFieldName, name, results);
                    }
                }

                if (!alreadyOpen)
                    EditorSceneManager.CloseScene(scene, true);
            }
        }

        private static void ScanObject(UnityEngine.Object obj, string idFieldName, string name,
            List<(UnityEngine.Object, string)> results)
        {
            var so       = new SerializedObject(obj);
            var iterator = so.GetIterator();

            while (iterator.Next(enterChildren: true))
            {
                if (iterator.propertyType == SerializedPropertyType.String
                    && iterator.name == idFieldName
                    && iterator.stringValue == name)
                {
                    results.Add((obj, iterator.propertyPath));
                }
            }
        }
    }
}
