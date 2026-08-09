using System;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal static class TypeSelectorSettings
    {
        internal static event Action Changed;

        internal const int MaxRecentsCapacity = 20;
        internal const int DefaultRecentsCapacity = 5;

        private const string KeyPrefix = "Aspid.FastTools.TypeSelector.Settings.";

        private static Store _cache;

        private static Store Data => _cache ??= Load();

        private static string Key => KeyPrefix + PlayerSettings.productGUID;

        internal static bool ShowFavorites
        {
            get => Data.ShowFavorites;
            set
            {
                if (Data.ShowFavorites == value) return;

                Data.ShowFavorites = value;
                Save();
            }
        }

        internal static int RecentsCapacity
        {
            get => Mathf.Clamp(Data.RecentsCapacity, 0, MaxRecentsCapacity);
            set
            {
                var clamped = Mathf.Clamp(value, 0, MaxRecentsCapacity);
                if (Data.RecentsCapacity == clamped) return;

                Data.RecentsCapacity = clamped;
                Save();
            }
        }

        internal static void ResetToDefaults()
        {
            ShowFavorites = true;
            RecentsCapacity = DefaultRecentsCapacity;
        }

        private static Store Load()
        {
            var json = EditorPrefs.GetString(Key, string.Empty);
            if (string.IsNullOrWhiteSpace(json)) return new Store();

            try
            {
                return JsonUtility.FromJson<Store>(json) ?? new Store();
            }
            catch (Exception)
            {
                return new Store();
            }
        }

        private static void Save()
        {
            EditorPrefs.SetString(Key, JsonUtility.ToJson(Data));
            Changed?.Invoke();
        }

        [Serializable]
        private sealed class Store
        {
            public bool ShowFavorites = true;
            public int RecentsCapacity = DefaultRecentsCapacity;
        }
    }
}
