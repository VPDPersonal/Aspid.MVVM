using System;
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// The Welcome tab's configurable behavior: whether <see cref="WelcomeWindowStartup"/> may open the tab on its own
    /// at all. An individual preference — it changes what one developer's editor does on launch, never the project —
    /// so it is persisted in project-scoped <see cref="EditorPrefs"/> (keyed by
    /// <see cref="PlayerSettings.productGUID"/>, the established package pattern) and never committed. Edited from the
    /// window's Settings tab and the Preferences page. Fires <see cref="Changed"/> so the settings controls mirror
    /// each other live.
    /// </summary>
    internal static class WelcomeSettings
    {
        /// <summary>
        /// Raised whenever a setting changes.
        /// </summary>
        public static event Action Changed;

        private const string AutoShowKeyPrefix = "Aspid.FastTools.Welcome.AutoShow.";

        private static string AutoShowKey => AutoShowKeyPrefix + PlayerSettings.productGUID;

        /// <summary>
        /// Whether the Welcome tab may auto-open on the first launch after an installation or a package update (the
        /// once-per-version gate itself lives in <see cref="WelcomeWindowStartup"/>). Off suppresses every future
        /// auto-show; the manual menu entry keeps working either way.
        /// </summary>
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

        /// <summary>Restores the default: auto-show on. The per-version "seen" flag is startup state, not a setting —
        /// resetting leaves it alone, so a project that already showed the current version's Welcome doesn't show it
        /// again.</summary>
        public static void ResetToDefaults() => AutoShowEnabled = true;
    }
}
