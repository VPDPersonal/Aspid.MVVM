using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal static class UniqueIdIndex
    {
        public static event Action IndexChanged;

        private static Dictionary<Type, FieldInfo[]> _fieldsByType;
        private static Dictionary<Type, Dictionary<string, HashSet<string>>> _index;

        public static bool IsUnique(Type assetType, string stringId, string currentAssetGuid)
        {
            if (string.IsNullOrEmpty(stringId)) return true;

            EnsureBuilt();

            if (!_index.TryGetValue(assetType, out var byId)) return true;
            if (!byId.TryGetValue(stringId, out var guids)) return true;
            if (guids.Count is 0) return true;

            return guids.Count is 1 && guids.Contains(currentAssetGuid);
        }

        public static void RefreshAsset(UnityEngine.Object target)
        {
            if (_index is null) return;
            if (target == null) return;

            var assetType = target.GetType();
            if (!TryGetFieldsFor(assetType, out var fields)) return;

            var path = AssetDatabase.GetAssetPath(target);
            if (string.IsNullOrEmpty(path)) return;

            var guid = AssetDatabase.AssetPathToGUID(path);
            if (string.IsNullOrEmpty(guid)) return;

            var so = new SerializedObject(target);
            RebuildAssetBucket(so, fields, assetType, guid);
            IndexChanged?.Invoke();
        }

        public static void OnAssetChanged(string path)
        {
            if (_index is null) return;

            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            if (asset == null) return;

            var assetType = asset.GetType();
            if (!TryGetFieldsFor(assetType, out var fields)) return;

            var guid = AssetDatabase.AssetPathToGUID(path);
            if (string.IsNullOrEmpty(guid)) return;

            var so = new SerializedObject(asset);
            RebuildAssetBucket(so, fields, assetType, guid);
            IndexChanged?.Invoke();
        }

        public static void Reset()
        {
            _index = null;
            _fieldsByType = null;
            IndexChanged?.Invoke();
        }

        private static void EnsureBuilt()
        {
            if (_index is not null) return;

            _index = new Dictionary<Type, Dictionary<string, HashSet<string>>>();
            _fieldsByType = BuildFieldsByType();

            foreach (var (assetType, fields) in _fieldsByType)
            {
                var byId = new Dictionary<string, HashSet<string>>();
                _index[assetType] = byId;

                var guids = AssetDatabase.FindAssets($"t:{assetType.FullName}");
                foreach (var guid in guids)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath(path, assetType);
                    if (asset == null) continue;

                    var so = new SerializedObject(asset);
                    foreach (var field in fields)
                    {
                        var prop = so.FindProperty(BuildStringIdPath(field));
                        if (prop is null) continue;
                        AddToIndex(byId, prop.stringValue, guid);
                    }
                }
            }
        }

        private static Dictionary<Type, FieldInfo[]> BuildFieldsByType()
        {
            var grouped = new Dictionary<Type, List<FieldInfo>>();
            foreach (var field in TypeCache.GetFieldsWithAttribute<UniqueIdAttribute>())
            {
                if (field.DeclaringType is null) continue;
                if (!grouped.TryGetValue(field.DeclaringType, out var list))
                {
                    list = new List<FieldInfo>();
                    grouped[field.DeclaringType] = list;
                }
                list.Add(field);
            }

            var result = new Dictionary<Type, FieldInfo[]>(grouped.Count);
            foreach (var (type, list) in grouped)
                result[type] = list.ToArray();
            return result;
        }

        private static bool TryGetFieldsFor(Type assetType, out FieldInfo[] fields)
        {
            _fieldsByType ??= BuildFieldsByType();
            return _fieldsByType.TryGetValue(assetType, out fields);
        }

        private static void AddToIndex(Dictionary<string, HashSet<string>> byId, string stringId, string guid)
        {
            if (string.IsNullOrEmpty(stringId)) return;
            if (!byId.TryGetValue(stringId, out var set))
            {
                set = new HashSet<string>();
                byId[stringId] = set;
            }
            set.Add(guid);
        }

        private static void RebuildAssetBucket(SerializedObject so, FieldInfo[] fields, Type assetType, string guid)
        {
            if (!_index.TryGetValue(assetType, out var byId))
            {
                byId = new Dictionary<string, HashSet<string>>();
                _index[assetType] = byId;
            }
            else
            {
                foreach (var bucket in byId.Values)
                    bucket.Remove(guid);
            }

            foreach (var field in fields)
            {
                var prop = so.FindProperty(BuildStringIdPath(field));
                if (prop is null) continue;
                AddToIndex(byId, prop.stringValue, guid);
            }
        }

        private static string BuildStringIdPath(FieldInfo field) =>
            $"{field.Name}.{Constants.StringIdFieldName}";

        [InitializeOnLoadMethod]
        private static void HookInspectorRepaint() =>
            IndexChanged += RepaintInspectors;

        private static void RepaintInspectors()
        {
            foreach (var window in Resources.FindObjectsOfTypeAll<EditorWindow>())
            {
                if (window != null && window.GetType().Name == "InspectorWindow")
                    window.Repaint();
            }
        }
    }
}
