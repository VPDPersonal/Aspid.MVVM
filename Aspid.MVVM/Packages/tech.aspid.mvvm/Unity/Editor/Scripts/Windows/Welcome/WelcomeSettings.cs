using System;
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // Whether the Welcome tab may open on its own. An individual preference: it changes what one developer's editor
    // does on launch, never the project, so it lives in project-scoped EditorPrefs and is never committed.
    internal static class WelcomeSettings
    {
        public static event Action Changed;

        private const string AutoShowKeyPrefix = "Aspid.MVVM.Welcome.AutoShow.";

        private static string AutoShowKey => AutoShowKeyPrefix + PlayerSettings.productGUID;

        // Whether the tab may auto-open after an install or update; the once-per-version gate lives in the startup
        // hook. Off suppresses every future auto-show, and the menu entry keeps working either way.
        public static bool AutoShowEnabled
        {
            get => EditorPrefs.GetBool(AutoShowKey, true);
            set
            {
                if (AutoShowEnabled == value) return;
                EditorPrefs.SetBool(AutoShowKey, value);
                Changed?.Invoke();
            }
        }

        // The per-version "seen" flag is startup state rather than a setting, so a reset leaves it alone and a
        // project that already showed this version's Welcome does not show it again.
        public static void ResetToDefaults() =>
            AutoShowEnabled = true;
    }
}
