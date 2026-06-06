using System;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    /// <summary>
    /// Finds and creates <see cref="IdRegistry"/> assets bound to a given IdStruct type,
    /// enforcing one-registry-per-type at lookup time.
    /// </summary>
    internal static class IdRegistryResolver
    {
        private const string TargetStructTypeField = "_targetStructType";

        private static Dictionary<string, IdRegistry> _byAqn;
        public static event Action RegistryChanged;

        public static void ClearCache()
        {
            _byAqn = null;
            RegistryChanged?.Invoke();
        }

        public static IdRegistry Find(Type declaringType)
        {
            if (declaringType is null) return null;

            EnsureWarmedUp();
            var aqn = declaringType.AssemblyQualifiedName ?? string.Empty;

            return _byAqn.GetValueOrDefault(aqn);
        }

        public static IdRegistry Create(Type declaringType)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var aqn = declaringType.AssemblyQualifiedName ?? string.Empty;
            var path = AssetDatabase.GenerateUniqueAssetPath($"Assets/IdRegistry_{declaringType.Name}.asset");
            var registry = ScriptableObject.CreateInstance<IdRegistry>();

            AssetDatabase.CreateAsset(registry, path);
            {
                var so = new SerializedObject(registry);
                so.FindProperty(TargetStructTypeField).stringValue = aqn;
                so.ApplyModifiedPropertiesWithoutUndo();
            }
            AssetDatabase.SaveAssets();

            EnsureWarmedUp();
            _byAqn[aqn] = registry;
            RegistryChanged?.Invoke();

            return registry;
        }

        public static IdRegistry GetOrCreate(Type declaringType) =>
            Find(declaringType) ?? Create(declaringType);

        public static void OnAssetImported(string path)
        {
            if (_byAqn is null) return;

            var registry = AssetDatabase.LoadAssetAtPath<IdRegistry>(path);
            if (registry is null) return;

            var aqn = ReadTargetStructType(registry);
            var changed = RemoveEntriesPointingTo(registry, exceptKey: aqn);

            if (string.IsNullOrEmpty(aqn))
            {
                if (changed) RegistryChanged?.Invoke();
                return;
            }

            if (_byAqn.TryGetValue(aqn, out var existing)
                && existing is not null
                && existing != registry)
            {
                Debug.LogError(
                    $"Multiple registries found for type AQN={aqn}: "
                    + $"{AssetDatabase.GetAssetPath(existing)}, {path}. "
                    + "Each IdStruct type must be bound to exactly one registry.");

                if (changed) RegistryChanged?.Invoke();
                return;
            }

            _byAqn[aqn] = registry;
            RegistryChanged?.Invoke();
        }

        private static void EnsureWarmedUp()
        {
            if (_byAqn is not null) return;

            _byAqn = new Dictionary<string, IdRegistry>();
            var duplicates = new Dictionary<string, List<string>>();

            foreach (var guid in AssetDatabase.FindAssets("t:IdRegistry"))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                TryRegisterAt(path, duplicates);
            }

            ReportDuplicates(duplicates);
        }

        private static string ReadTargetStructType(IdRegistry registry)
        {
            var serializedObject = new SerializedObject(registry);
            var property = serializedObject.FindProperty(TargetStructTypeField);

            return property is not null ? property.stringValue : string.Empty;
        }

        private static void ReportDuplicates(Dictionary<string, List<string>> duplicates)
        {
            foreach (var pair in duplicates)
            {
                Debug.LogError(
                    $"Multiple registries found for type AQN={pair.Key}: "
                    + string.Join(", ", pair.Value)
                    + ". Each IdStruct type must be bound to exactly one registry.");
            }
        }

        private static bool RemoveEntriesPointingTo(IdRegistry registry, string exceptKey)
        {
            if (_byAqn is null) return false;
            List<string> toRemove = null;

            foreach (var pair in _byAqn
                         .Where(pair => pair.Value == registry)
                         .Where(pair => pair.Key != exceptKey))
            {
                toRemove ??= new List<string>();
                toRemove.Add(pair.Key);
            }

            if (toRemove is null) return false;

            foreach (var key in toRemove)
                _byAqn.Remove(key);

            return true;
        }

        private static void TryRegisterAt(string path, Dictionary<string, List<string>> duplicates)
        {
            var registry = AssetDatabase.LoadAssetAtPath<IdRegistry>(path);
            if (registry is null) return;

            var aqn = ReadTargetStructType(registry);
            if (string.IsNullOrEmpty(aqn)) return;

            if (_byAqn.TryGetValue(aqn, out var existing))
            {
                if (!duplicates.TryGetValue(aqn, out var list))
                {
                    list = new List<string> { AssetDatabase.GetAssetPath(existing) };
                    duplicates[aqn] = list;
                }

                list.Add(path);
                return;
            }

            _byAqn[aqn] = registry;
        }
    }
}
