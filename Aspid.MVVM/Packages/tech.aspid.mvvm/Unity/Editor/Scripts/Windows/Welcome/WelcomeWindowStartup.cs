using System.IO;
using UnityEditor;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // Per-version auto-show for the Welcome tab: the first launch after an install or update opens the window on it.
    // Owns the per-project, per-version "seen" flag gating the auto-show.
    [InitializeOnLoad]
    internal static class WelcomeWindowStartup
    {
        private const string SessionKey = "Aspid.MVVM.WelcomeWindow.StartupHandled";

        // The package version is part of the key, so every update resets the flag and re-triggers the auto-show.
        private static string SeenKey => $"Aspid.MVVM.WelcomeWindow.Seen::{PackageVersion}::{ProjectPath}";

        private static string PackageVersion =>
            PackageInfo.FindForAssembly(typeof(WelcomeWindowStartup).Assembly)?.version ?? AspidMvvmSettings.Version;

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

        // Records that the tab was opened, so the auto-show waits for the next package update.
        public static void MarkSeen() =>
            HasBeenSeen = true;

        private static void TryShowOnStartup()
        {
            if (SessionState.GetBool(SessionKey, false)) return;
            SessionState.SetBool(SessionKey, true);

            if (Application.isBatchMode) return;

            // The user's opt-out (Settings tab) gates every auto-show; the manual menu entry is untouched by it.
            if (!WelcomeSettings.AutoShowEnabled) return;
            if (HasBeenSeen) return;
            if (HasOpenWindow()) return;

            AspidMvvmWindow.OpenWelcome();
        }

        private static bool HasOpenWindow()
        {
            var windows = Resources.FindObjectsOfTypeAll<AspidMvvmWindow>();
            return windows is { Length: > 0 };
        }
    }
}
