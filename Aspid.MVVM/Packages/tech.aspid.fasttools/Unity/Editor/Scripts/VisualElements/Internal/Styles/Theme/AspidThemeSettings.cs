using System;
using UnityEditor;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Per-user theme settings for Aspid editor UI. Stores the GUID of an optional user
    /// override <see cref="StyleSheet"/> in <see cref="EditorPrefs"/>; the override is layered on
    /// top of <see cref="AspidStyles.DefaultStyleSheet"/> and may redefine any
    /// <c>--aspid-colors-*</c> / <c>--aspid-icons-*</c> token inside a <c>:root</c> block.
    /// </summary>
    internal static class AspidThemeSettings
    {
        // Project-scoped: the stored GUID only resolves inside the project it was picked in, and the per-user reset
        // must not wipe another project's override — one machine-global slot did both (see the legacy key below).
        private static string OverrideStyleSheetGuidKey =>
            "Aspid.FastTools.Theme.OverrideStyleSheetGuid." + PlayerSettings.productGUID;

        // The pre-scoping key. Read once as a fallback and migrated forward, so an override saved before the change
        // keeps working in the project it was set for (elsewhere its GUID never resolved to an asset anyway).
        private const string LegacyOverrideStyleSheetGuidKey = "Aspid.FastTools.Theme.OverrideStyleSheetGuid";

        /// <summary>
        /// Raised whenever the override style sheet changes so that live elements can re-apply it.
        /// </summary>
        public static event Action Changed;

        /// <summary>
        /// The resolved user override style sheet, or <c>null</c> when none is set or the asset is missing.
        /// </summary>
        public static StyleSheet OverrideStyleSheet
        {
            get
            {
                var guid = OverrideStyleSheetGuid;
                if (string.IsNullOrEmpty(guid)) return null;

                var path = AssetDatabase.GUIDToAssetPath(guid);
                return string.IsNullOrEmpty(path) ? null : AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            }
            set => OverrideStyleSheetGuid = value == null
                ? string.Empty
                : AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(value));
        }

        // Backing storage for OverrideStyleSheet, persisted in EditorPrefs as the asset GUID
        // (empty when none). Setting it raises Changed; callers go through the typed property.
        private static string OverrideStyleSheetGuid
        {
            get
            {
                var value = EditorPrefs.GetString(OverrideStyleSheetGuidKey, string.Empty);
                if (value.Length > 0) return value;

                var legacy = EditorPrefs.GetString(LegacyOverrideStyleSheetGuidKey, string.Empty);
                if (legacy.Length == 0) return string.Empty;

                EditorPrefs.SetString(OverrideStyleSheetGuidKey, legacy);
                EditorPrefs.DeleteKey(LegacyOverrideStyleSheetGuidKey);
                return legacy;
            }
            set
            {
                value ??= string.Empty;
                if (OverrideStyleSheetGuid == value) return;

                if (string.IsNullOrEmpty(value)) EditorPrefs.DeleteKey(OverrideStyleSheetGuidKey);
                else EditorPrefs.SetString(OverrideStyleSheetGuidKey, value);

                Changed?.Invoke();
            }
        }
    }
}
