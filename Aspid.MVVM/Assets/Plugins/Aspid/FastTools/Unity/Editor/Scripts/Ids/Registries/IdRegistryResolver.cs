#nullable enable
using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Aspid.FastTools.Ids;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    /// <summary>
    /// Finds and creates Id registry assets bound to a given IdStruct type.
    /// Searches both <see cref="IdRegistry"/> and <see cref="StringIdRegistry"/>,
    /// enforcing one-registry-per-type at lookup time.
    /// </summary>
    internal static class IdRegistryResolver
    {
        private const string TargetStructTypeField = "_targetStructType";

        private static readonly Dictionary<string, ScriptableObject?> _cache = new();

        internal static void ClearCache() => _cache.Clear();

        public static ScriptableObject? Find(Type? declaringType)
        {
            if (declaringType == null) return null;

            var aqn = declaringType.AssemblyQualifiedName ?? string.Empty;
            if (_cache.TryGetValue(aqn, out var cached))
                return cached;

            ScriptableObject? first = null;
            List<string>? extraPaths = null;

            foreach (var path in EnumerateRegistryPaths())
            {
                var registry = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                if (registry == null) continue;

                var stored = ReadTargetStructType(registry);
                if (stored != aqn) continue;

                if (first == null)
                {
                    first = registry;
                    continue;
                }

                extraPaths ??= new List<string> { AssetDatabase.GetAssetPath(first) };
                extraPaths.Add(path);
            }

            if (extraPaths != null)
            {
                Debug.LogError(
                    $"Multiple registries found for type {declaringType.Name}: "
                    + string.Join(", ", extraPaths)
                    + ". Each IdStruct type must be bound to exactly one registry.");
            }

            _cache[aqn] = first;
            return first;
        }

        public static IdRegistry? FindIntOnly(Type? declaringType) =>
            Find(declaringType) as IdRegistry;

        public static StringIdRegistry? FindStringMapped(Type? declaringType) =>
            Find(declaringType) as StringIdRegistry;

        public static StringIdRegistry CreateStringMapped(Type declaringType)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var path = AssetDatabase.GenerateUniqueAssetPath($"Assets/StringIdRegistry_{declaringType.Name}.asset");
            var reg = ScriptableObject.CreateInstance<StringIdRegistry>();
            AssetDatabase.CreateAsset(reg, path);

            var so = new SerializedObject(reg);
            so.FindProperty(TargetStructTypeField).stringValue = declaringType.AssemblyQualifiedName ?? string.Empty;
            so.ApplyModifiedPropertiesWithoutUndo();

            AssetDatabase.SaveAssets();
            _cache[declaringType.AssemblyQualifiedName ?? string.Empty] = reg;
            return reg;
        }

        private static IEnumerable<string> EnumerateRegistryPaths()
        {
            var guids = AssetDatabase.FindAssets("t:IdRegistry t:StringIdRegistry");
            for (var i = 0; i < guids.Length; i++)
                yield return AssetDatabase.GUIDToAssetPath(guids[i]);
        }

        private static string ReadTargetStructType(ScriptableObject registry)
        {
            var so = new SerializedObject(registry);
            var prop = so.FindProperty(TargetStructTypeField);
            return prop != null ? prop.stringValue : string.Empty;
        }
    }
}
