using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // The Welcome auto-show switch, defined once so every settings surface renders the same control.
    internal static class WelcomeSettingsUI
    {
        public static void BuildControls(VisualElement container)
        {
            var autoShow = new AspidSwitch("Auto-show Welcome")
            {
                value = WelcomeSettings.AutoShowEnabled,
                tooltip = "Open the Welcome tab automatically after the package is installed or updated.\n" +
                    "Turning it off suppresses every future auto-show; Tools → Aspid 🐍 → MVVM → Welcome keeps working.\n" +
                    "Per-user setting: stored locally, never committed.",
            };

            autoShow.WithScopeStripe(AspidSettingsUI.UserScopeClass);
            autoShow.RegisterValueChangedCallback(evt => WelcomeSettings.AutoShowEnabled = evt.newValue);

            AspidSettingsUI.SyncFromSettings(
                autoShow,
                () => WelcomeSettings.AutoShowEnabled,
                handler => WelcomeSettings.Changed += handler,
                handler => WelcomeSettings.Changed -= handler);

            container.Add(autoShow);
        }
    }
}
