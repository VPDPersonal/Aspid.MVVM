using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Durable, project-scoped named templates for managed-reference instances: a stored type plus its JSON payload,
    /// rehydrated into an independent instance on use. A persistent upgrade over the session-only
    /// <see cref="SerializeReferenceClipboard"/>, modeled on <c>TypeSelectorPreferences</c> (EditorPrefs JSON, scoped by
    /// <see cref="PlayerSettings.productGUID"/>). Entries whose type no longer resolves are pruned on load.
    /// </summary>
    /// <remarks>
    /// Like the clipboard, the JSON round-trip is <see cref="JsonUtility"/>-based, so nested <c>[SerializeReference]</c>
    /// children are not preserved — templates capture single-level instances.
    /// </remarks>
    internal static class SerializeReferenceTemplates
    {
        private const string KeyPrefix = "Aspid.FastTools.SerializeReference.Templates.";

        [Serializable]
        private sealed class Entry
        {
            public string name;
            public string aqn;
            public string json;
        }

        [Serializable]
        private sealed class Store
        {
            public List<Entry> entries = new();
        }

        /// <summary>
        /// A resolved template: its display name and the concrete type it instantiates.
        /// </summary>
        public readonly struct Template
        {
            public readonly string Name;
            public readonly Type Type;

            public Template(string name, Type type)
            {
                Name = name;
                Type = type;
            }
        }

        private static string Key => KeyPrefix + PlayerSettings.productGUID;

        /// <summary>
        /// Whether a template with <paramref name="name"/> already exists.
        /// </summary>
        public static bool Contains(string name) => Load().entries.Exists(entry => entry.name == name);

        /// <summary>
        /// Saves <paramref name="value"/> under <paramref name="name"/>, asking for confirmation first when a template
        /// with that name already exists (the existing one would be overwritten).
        /// </summary>
        public static void SaveConfirmed(string name, object value)
        {
            if (Contains(name) && !EditorUtility.DisplayDialog("Overwrite Template?",
                    $"A template named \"{name}\" already exists. Overwrite it?", "Overwrite", "Cancel")) return;

            Save(name, value);
        }

        /// <summary>
        /// Saves <paramref name="value"/> under <paramref name="name"/> (replacing an existing one).
        /// </summary>
        public static void Save(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name) || value is null) return;

            var type = value.GetType();
            var store = Load();
            store.entries.RemoveAll(entry => entry.name == name);
            store.entries.Add(new Entry { name = name.Trim(), aqn = type.AssemblyQualifiedName, json = JsonUtility.ToJson(value) });
            Persist(store);
        }

        /// <summary>
        /// Every template whose type still resolves, pruning the rest. In stored order.
        /// </summary>
        public static List<Template> LoadResolved()
        {
            var store = Load();
            var result = new List<Template>(store.entries.Count);
            var changed = false;

            foreach (var entry in store.entries)
            {
                var type = string.IsNullOrEmpty(entry.aqn) ? null : Type.GetType(entry.aqn, throwOnError: false);
                if (type is not null) result.Add(new Template(entry.name, type));
                else changed = true;
            }

            if (changed)
            {
                store.entries.RemoveAll(entry => string.IsNullOrEmpty(entry.aqn) || Type.GetType(entry.aqn, throwOnError: false) is null);
                Persist(store);
            }

            return result;
        }

        /// <summary>
        /// Rehydrates an independent instance from the named template, or null if it is gone/unresolved.
        /// </summary>
        public static object CreateInstance(string name)
        {
            var entry = Load().entries.Find(e => e.name == name);
            if (entry is null) return null;

            var type = string.IsNullOrEmpty(entry.aqn) ? null : Type.GetType(entry.aqn, throwOnError: false);
            if (type is null) return null;

            var instance = SerializeReferenceHelpers.CreateInstance(type);
            if (instance != null && !string.IsNullOrEmpty(entry.json)) JsonUtility.FromJsonOverwrite(entry.json, instance);
            return instance;
        }

        /// <summary>
        /// A unique default name for a new template of <paramref name="type"/> (deduplicated with a suffix).
        /// </summary>
        public static string SuggestName(Type type)
        {
            var baseName = type?.Name ?? "Template";
            var existing = new HashSet<string>(StringComparer.Ordinal);
            foreach (var entry in Load().entries) existing.Add(entry.name);

            if (!existing.Contains(baseName)) return baseName;
            for (var i = 2; ; i++)
            {
                var candidate = $"{baseName} {i}";
                if (!existing.Contains(candidate)) return candidate;
            }
        }

        private static Store Load()
        {
            var json = EditorPrefs.GetString(Key, string.Empty);
            if (string.IsNullOrEmpty(json)) return new Store();

            try
            {
                return JsonUtility.FromJson<Store>(json) ?? new Store();
            }
            catch (Exception)
            {
                return new Store();
            }
        }

        private static void Persist(Store store) => EditorPrefs.SetString(Key, JsonUtility.ToJson(store));
    }
}
