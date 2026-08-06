using System.IO;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.SerializeReferences.Editors;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// Per-version auto-show for the Welcome panel. The Welcome content lives as the "home" tab of the
    /// Managed References window (<see cref="TabWindow"/>), so on the first launch after an install
    /// or a package update we open that window on its home tab instead of a standalone window. Owns the
    /// per-project, per-package-version "seen" flag that gates the auto-show.
    /// </summary>
    [InitializeOnLoad]
    internal static class WelcomeWindowStartup
    {
        private const string SessionKey = "Aspid.FastTools.WelcomeWindow.StartupHandled";

        // The package version is part of the key, so every update resets the flag and re-triggers the auto-show.
        private static string SeenKey => $"Aspid.FastTools.WelcomeWindow.Seen::{PackageVersion}::{ProjectPath}";

        private static string PackageVersion =>
            PackageInfo.FindForAssembly(typeof(WelcomeWindowStartup).Assembly)?.version ?? "unknown";

        public static bool HasBeenSeen
        {
            get => EditorPrefs.GetBool(SeenKey, false);
            private set => EditorPrefs.SetBool(SeenKey, value);
        }

        private static string ProjectPath
        {
            get
            {
                var projectDirectory = Directory.GetParent(Application.dataPath);
                return projectDirectory?.FullName ?? Application.dataPath;
            }
        }

        static WelcomeWindowStartup()
        {
            EditorApplication.delayCall += TryShowOnStartup;
        }

        /// <summary>
        /// Records that the Welcome panel has been opened, so the auto-show won't fire again until the next
        /// package update.
        /// </summary>
        public static void MarkSeen() => HasBeenSeen = true;

        private static void TryShowOnStartup()
        {
            if (SessionState.GetBool(SessionKey, false)) return;
            SessionState.SetBool(SessionKey, true);

            if (Application.isBatchMode) return;

            // The user's opt-out (Settings tab / Preferences → Welcome) gates every auto-show; the manual menu entry
            // is untouched by it.
            if (!WelcomeSettings.AutoShowEnabled) return;
            if (HasBeenSeen) return;
            if (HasOpenWindow()) return;

            TabWindow.OpenWelcome();
        }

        private static bool HasOpenWindow()
        {
            var windows = Resources.FindObjectsOfTypeAll<TabWindow>();
            return windows is { Length: > 0 };
        }
    }
}
