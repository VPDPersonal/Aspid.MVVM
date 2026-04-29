using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    [InitializeOnLoad]
    internal static class WelcomeWindowStartup
    {
        private const string SessionKey = "Aspid.FastTools.WelcomeWindow.StartupHandled";

        static WelcomeWindowStartup()
        {
            EditorApplication.delayCall += TryShowOnStartup;
        }

        private static void TryShowOnStartup()
        {
            if (SessionState.GetBool(SessionKey, false)) return;
            SessionState.SetBool(SessionKey, true);

            if (Application.isBatchMode) return;
            if (WelcomeWindow.HasBeenSeen) return;
            if (HasOpenWelcomeWindow()) return;

            WelcomeWindow.ShowWindow();
        }

        private static bool HasOpenWelcomeWindow()
        {
            var windows = Resources.FindObjectsOfTypeAll<WelcomeWindow>();
            return windows is { Length: > 0 };
        }
    }
}
