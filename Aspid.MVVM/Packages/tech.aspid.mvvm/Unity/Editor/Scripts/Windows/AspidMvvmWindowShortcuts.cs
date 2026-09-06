using System;
using UnityEngine;
using UnityEditor.ShortcutManagement;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // The single owner of the tab keyboard layout: shortcut ids, their defaults and the Ctrl+Tab cycle order. The
    // window renders its tooltips from HintFor rather than rebuilding the defaults, so the two cannot disagree.
    internal static class AspidMvvmWindowShortcuts
    {
        private const string Category = "Aspid MVVM/Window/";

        private const string NextTabId = Category + "Next Tab";
        private const string PreviousTabId = Category + "Previous Tab";

        private const string HomeId = Category + "Home";
        private const string SettingsId = Category + "Settings";

        private const KeyCode HomeKey = KeyCode.Alpha1;
        private const KeyCode SettingsKey = KeyCode.Alpha0;
        private const ShortcutModifiers TabModifiers = ShortcutModifiers.Alt;

        // Ordered as the toolbar renders the tabs. Cycling walks this array instead of the TabType values, so
        // reordering the enum can't silently reshuffle Ctrl+Tab.
        private static readonly TabData[] _tabData =
        {
            new(HomeId, TabType.Welcome, HomeKey),
            new(SettingsId, TabType.Settings, SettingsKey),
        };

        [Shortcut(HomeId, typeof(AspidMvvmWindow), HomeKey, TabModifiers)]
        private static void OnHomeShortcut(ShortcutArguments args) =>
            SwitchFrom(args, TabType.Welcome);

        [Shortcut(SettingsId, typeof(AspidMvvmWindow), SettingsKey, TabModifiers)]
        private static void OnSettingsShortcut(ShortcutArguments args) =>
            SwitchFrom(args, TabType.Settings);

        [Shortcut(NextTabId, typeof(AspidMvvmWindow), KeyCode.Tab, ShortcutModifiers.Control)]
        private static void OnNextTabShortcut(ShortcutArguments args) =>
            CycleFrom(args, +1);

        [Shortcut(PreviousTabId, typeof(AspidMvvmWindow), KeyCode.Tab, ShortcutModifiers.Control | ShortcutModifiers.Shift)]
        private static void OnPreviousTabShortcut(ShortcutArguments args) =>
            CycleFrom(args, -1);

        // The live binding read from the ShortcutManager, so a hint tracks user rebinds and renders the real
        // per-platform glyph. Falls back to the declared default when the id is unregistered or its binding cleared.
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
            if (args.context is AspidMvvmWindow window)
                window.SwitchMode(tab);
        }

        private static void CycleFrom(ShortcutArguments args, int step)
        {
            if (args.context is not AspidMvvmWindow window) return;

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
                // ShortcutManager not ready or unknown id: the caller falls back to the declared default.
                return null;
            }
        }

        // Glyphs on macOS, spelled-out names elsewhere, mirroring how Unity itself renders a binding.
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
