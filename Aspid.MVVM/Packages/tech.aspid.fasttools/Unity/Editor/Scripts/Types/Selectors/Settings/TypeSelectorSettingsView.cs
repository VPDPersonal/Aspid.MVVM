using System;
using UnityEditor;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal static class TypeSelectorSettingsView
    {
        internal static void BuildControls(VisualElement container)
        {
            var showFavorites = new AspidSwitch("Favorites section")
            {
                value = TypeSelectorSettings.ShowFavorites,
                tooltip = "Show the ★ Favorites section on the picker's root page.\n"
                    + "Hiding it keeps your saved favorites (and the per-row ★ toggle) — turning it back on restores the same list.\n"
                    + "Per-user setting — stored locally, never committed.",
            };

            showFavorites.WithScopeStripe(AspidSettingsUI.UserScopeClass);
            showFavorites.RegisterValueChangedCallback(evt => TypeSelectorSettings.ShowFavorites = evt.newValue);
            SyncFromSettings(showFavorites, () => TypeSelectorSettings.ShowFavorites);
            container.Add(showFavorites);

            var capacity = new SliderInt("Recent items", 0, TypeSelectorSettings.MaxRecentsCapacity)
            {
                value = TypeSelectorSettings.RecentsCapacity,
                showInputField = true,
                tooltip = "How many picks the picker's Recent section keeps (most recent first).\n"
                    + "0 hides the section and pauses recording without wiping the already-collected history.\n"
                    + "Per-user setting — stored locally, never committed.",
            };

            capacity.WithScopeStripe(AspidSettingsUI.UserScopeClass);
            capacity.RegisterValueChangedCallback(evt => TypeSelectorSettings.RecentsCapacity = evt.newValue);
            SyncFromSettings(capacity, () => TypeSelectorSettings.RecentsCapacity);
            container.Add(capacity);

            container.Add(BuildClearRow());
        }

        private static VisualElement BuildClearRow()
        {
            var row = new VisualElement().AddClass(AspidSettingsUI.RowClass).WithScopeStripe(AspidSettingsUI.UserScopeClass);
            row.tooltip = "The Favorites / Recent lists are saved per user and per project.\n"
                + "Clearing removes every stored entry, including ones kept for types that don't currently resolve.";

            var caption = new Label("Saved lists").AddClass(AspidSettingsUI.RowCaptionClass);

            var clearFavorites = new Button(ClearFavorites) { text = "Clear favorites" }
                .AddClass(AspidSettingsUI.ActionClass)
                .AddClass(AspidSettingsUI.ActionDangerClass);

            var clearRecents = new Button(ClearRecents) { text = "Clear recents" }
                .AddClass(AspidSettingsUI.ActionClass)
                .AddClass(AspidSettingsUI.ActionDangerClass);

            return row
                .AddChild(caption)
                .AddChild(clearFavorites)
                .AddChild(clearRecents);
        }

        private static void ClearFavorites()
        {
            if (!ConfirmClear("favorites", TypeSelectorPreferences.FavoritesCount)) return;
            TypeSelectorPreferences.ClearFavorites();
        }

        private static void ClearRecents()
        {
            if (!ConfirmClear("recents", TypeSelectorPreferences.RecentsCount)) return;
            TypeSelectorPreferences.ClearRecents();
        }

        private static bool ConfirmClear(string list, int count)
        {
            var title = $"Clear {list}";

            if (count is 0)
            {
                EditorUtility.DisplayDialog(title, $"There are no saved {list} to clear.", "OK");
                return false;
            }

            return EditorUtility.DisplayDialog(
                title,
                $"Remove all saved {list} ({count} {(count == 1 ? "entry" : "entries")}) for this project? This cannot be undone.",
                "Clear",
                "Cancel");
        }

        private static void SyncFromSettings<TControl, TValue>(TControl control, Func<TValue> read)
            where TControl : VisualElement, INotifyValueChanged<TValue>
        {
            AspidSettingsUI.SyncFromSettings(
                control,
                read,
                handler => TypeSelectorSettings.Changed += handler,
                handler => TypeSelectorSettings.Changed -= handler);
        }
    }
}
