using System;
using UnityEngine;
using UnityEditor.ShortcutManagement;

namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The single owner of the tab keyboard layout: shortcut ids, their default bindings, and the order Ctrl+Tab
    /// cycles through.
    /// </summary>
    /// <remarks>
    /// The window renders its badges from <see cref="HintFor"/> rather than rebuilding the defaults itself, so a key
    /// changed here can never disagree with what the toolbar shows.
    /// </remarks>
    internal static class TabWindowShortcuts
    {
        private const string Category = "Aspid FastTools/Window/";

        private const string NextTabId = Category + "Next Tab";
        private const string PreviousTabId = Category + "Previous Tab";

        private const string HomeId = Category + "Home";
        private const string AssetReferencesId = Category + "Asset References";
        private const string ProjectReferencesId = Category + "Project References";
        private const string SettingsId = Category + "Settings";

        private const KeyCode HomeKey = KeyCode.Alpha1;
        private const KeyCode SettingsKey = KeyCode.Alpha0;
        private const KeyCode AssetReferencesKey = KeyCode.Alpha2;
        private const KeyCode ProjectReferencesKey = KeyCode.Alpha3;
        private const ShortcutModifiers TabModifiers = ShortcutModifiers.Alt;

        // Tab order for cycling and the lookup behind HintFor, ordered as the toolbar renders them. Cycling walks this
        // array instead of the TabType values, so reordering the enum can't silently reshuffle Ctrl+Tab.
        private static readonly TabData[] _tabData =
        {
            new(HomeId, TabType.Welcome, HomeKey),
            new(AssetReferencesId, TabType.AssetReference, AssetReferencesKey),
            new(ProjectReferencesId, TabType.ProjectReferences, ProjectReferencesKey),
            new(SettingsId, TabType.Settings, SettingsKey),
        };

        [Shortcut(HomeId, typeof(TabWindow), HomeKey, TabModifiers)]
        private static void OnHomeShortcut(ShortcutArguments args) =>
            SwitchFrom(args, TabType.Welcome);

        [Shortcut(AssetReferencesId, typeof(TabWindow), AssetReferencesKey, TabModifiers)]
        private static void OnInspectShortcut(ShortcutArguments args) =>
            SwitchFrom(args, TabType.AssetReference);

        [Shortcut(ProjectReferencesId, typeof(TabWindow), ProjectReferencesKey, TabModifiers)]
        private static void OnProjectShortcut(ShortcutArguments args) =>
            SwitchFrom(args, TabType.ProjectReferences);

        [Shortcut(SettingsId, typeof(TabWindow), SettingsKey, TabModifiers)]
        private static void OnSettingsShortcut(ShortcutArguments args) =>
            SwitchFrom(args, TabType.Settings);

        [Shortcut(NextTabId, typeof(TabWindow), KeyCode.Tab, ShortcutModifiers.Control)]
        private static void OnNextTabShortcut(ShortcutArguments args) =>
            CycleFrom(args, +1);

        [Shortcut(PreviousTabId, typeof(TabWindow), KeyCode.Tab, ShortcutModifiers.Control | ShortcutModifiers.Shift)]
        private static void OnPreviousTabShortcut(ShortcutArguments args) =>
            CycleFrom(args, -1);

        /// <summary>
        /// The badge and tooltip text for a tab: the live binding read from the ShortcutManager, so it tracks user
        /// rebinds and renders the real per-platform glyph.
        /// </summary>
        /// <remarks>
        /// Falls back to the shortcut's declared default when the id isn't registered yet or its binding was cleared.
        /// </remarks>
        internal static string HintFor(TabType tab)
        {
            foreach (var tabData in _tabData)
            {
                if (tabData.Tab != tab) continue;
                return LiveBinding(tabData.Id) ?? DefaultHint(tabData.Key);
            }

            return string.Empty;
        }

        private static void SwitchFrom(ShortcutArguments args, TabType tab)
        {
            if (args.context is TabWindow window)
                window.SwitchMode(tab);
        }

        private static void CycleFrom(ShortcutArguments args, int step)
        {
            if (args.context is not TabWindow window) return;

            var currentTabIndex = IndexOf(window.CurrentTabType);
            var nextTabIndex = (currentTabIndex + step + _tabData.Length) % _tabData.Length;

            window.SwitchMode(_tabData[nextTabIndex].Tab);
        }

        private static int IndexOf(TabType tab)
        {
            for (var i = 0; i < _tabData.Length; i++)
            {
                if (_tabData[i].Tab == tab) return i;
            }

            return 0;
        }

        private static string LiveBinding(string shortcutId)
        {
            try
            {
                var binding = ShortcutManager.instance.GetShortcutBinding(shortcutId).ToString();
                return string.IsNullOrEmpty(binding) ? null : binding;
            }
            catch (Exception)
            {
                // ShortcutManager not ready / unknown id — the caller falls back to the declared default.
                return null;
            }
        }

        // LiveBinding's fallback, spelled from the [Shortcut] defaults above: glyphs on macOS, spelled-out names
        // elsewhere, mirroring how Unity itself renders a binding.
        private static string DefaultHint(KeyCode key)
        {
            var label = key is >= KeyCode.Alpha0 and <= KeyCode.Alpha9
                ? (key - KeyCode.Alpha0).ToString()
                : key.ToString();

            return ModifierPrefix(TabModifiers) + label;
        }

        private static string ModifierPrefix(ShortcutModifiers modifiers)
        {
            var isMac = Application.platform == RuntimePlatform.OSXEditor;
            var prefix = string.Empty;

            if ((modifiers & ShortcutModifiers.Control) != 0) prefix += isMac ? "⌃" : "Ctrl+";
            if ((modifiers & ShortcutModifiers.Action) != 0) prefix += isMac ? "⌘" : "Ctrl+";
            if ((modifiers & ShortcutModifiers.Alt) != 0) prefix += isMac ? "⌥" : "Alt+";
            if ((modifiers & ShortcutModifiers.Shift) != 0) prefix += isMac ? "⇧" : "Shift+";

            return prefix;
        }

        private readonly struct TabData
        {
            internal readonly string Id;
            internal readonly TabType Tab;
            internal readonly KeyCode Key;

            internal TabData(string id, TabType tab, KeyCode key)
            {
                Id = id;
                Tab = tab;
                Key = key;
            }
        }
    }
}
