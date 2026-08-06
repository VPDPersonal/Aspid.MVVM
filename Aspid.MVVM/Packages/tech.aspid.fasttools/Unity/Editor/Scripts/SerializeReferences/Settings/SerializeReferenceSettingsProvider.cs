using UnityEditor;
using UnityEngine;
using Aspid.FastTools.Editors;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The package's Project Settings page (<c>Project Settings → Aspid.FastTools → SerializeReference</c>) — the
    /// team-wide half of the settings, matching the page's <see cref="SettingsScope.Project"/>: auto de-alias, the
    /// build/CI gate severity and the excluded scan folders, all persisted in the committed ProjectSettings asset.
    /// The per-user controls live on the <c>Preferences → Aspid.FastTools</c> pages instead, and the window's Settings
    /// tab shows both scopes as the one full overview. <see cref="AspidSettingsUI.BuildProviderPage"/> composes the
    /// same branded page (dotted canvas, legend, sections, pinned reset footer) both Unity-native pages share.
    /// Backed by <see cref="SerializeReferenceSettings"/>.
    /// </summary>
    internal static class SerializeReferenceSettingsProvider
    {
        // Repaint open editor windows when a setting changes, so toggles like rid colours apply without reselection.
        [InitializeOnLoadMethod]
        private static void HookRepaint() => SerializeReferenceSettings.Changed += RepaintAll;

        private static void RepaintAll()
        {
            foreach (var window in Resources.FindObjectsOfTypeAll<EditorWindow>())
                if (window != null) window.Repaint();
        }

        [SettingsProvider]
        public static SettingsProvider Create() =>
            new("Project/Aspid.FastTools/SerializeReference", SettingsScope.Project)
            {
                label = "SerializeReference",
                keywords = new HashSet<string>(new[]
                {
                    "serialize", "reference", "managed", "aspid", "rid", "gate", "missing", "required",
                    "alias", "build", "ci", "excluded", "folders",
                }),
                activateHandler = (_, root) => AspidSettingsUI.BuildProviderPage(root, AspidSettingsScope.Shared),
            };
    }
}
