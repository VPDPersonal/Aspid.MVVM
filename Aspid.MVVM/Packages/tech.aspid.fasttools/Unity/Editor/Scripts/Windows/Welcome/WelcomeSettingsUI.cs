using System;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// Builds the "Welcome" settings controls bound to <see cref="WelcomeSettings"/> — the auto-show switch. Shared by
    /// the window's Settings tab and the Preferences page, so both render the same control from one definition. A
    /// per-user preference (<see cref="AspidSettingsUI.UserScopeClass"/>).
    /// </summary>
    internal static class WelcomeSettingsUI
    {
        /// <summary>Appends the auto-show switch to <paramref name="container"/>, wired straight to
        /// <see cref="WelcomeSettings"/>.</summary>
        public static void BuildControls(VisualElement container)
        {
            var autoShow = new AspidSwitch("Auto-show Welcome")
            {
                value = WelcomeSettings.AutoShowEnabled,
                tooltip = "Open the Welcome tab automatically after the package is installed or updated.\n"
                    + "Turning it off suppresses every future auto-show; Tools → Aspid 🐍 → FastTools → Welcome keeps working.\n"
                    + "Per-user setting — stored locally, never committed.",
            };
            autoShow.WithScopeStripe(AspidSettingsUI.UserScopeClass);
            autoShow.RegisterValueChangedCallback(evt => WelcomeSettings.AutoShowEnabled = evt.newValue);
            SyncFromSettings(autoShow, () => WelcomeSettings.AutoShowEnabled);
            container.Add(autoShow);
        }

        // Shorthand over the shared live-sync helper, binding to this store's Changed signal.
        private static void SyncFromSettings<TControl, TValue>(TControl control, Func<TValue> read)
            where TControl : VisualElement, INotifyValueChanged<TValue>
        {
            AspidSettingsUI.SyncFromSettings(
                control,
                read,
                handler => WelcomeSettings.Changed += handler,
                handler => WelcomeSettings.Changed -= handler);
        }
    }
}
