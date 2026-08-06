using System;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Builds the SerializeReference settings controls bound to <see cref="SerializeReferenceSettings"/>. Rendered —
    /// via <see cref="Aspid.FastTools.Editors.AspidSettingsUI.BuildSurfaceContent"/> — by the window's Settings tab
    /// (both scopes), the Preferences page (per-user controls) and the Project Settings page (shared controls), so
    /// every surface renders the same controls from one definition and mirrors the others live.
    /// </summary>
    internal static class SerializeReferenceSettingsUI
    {
        /// <summary>
        /// Appends the References controls that belong to <paramref name="scope"/> — breakage detection for the
        /// per-user scope; auto de-alias, the build gate and the excluded folders for the shared scope — to
        /// <paramref name="container"/>, each wired straight to <see cref="SerializeReferenceSettings"/>. Rid colours
        /// are not configurable — they always identify a shared reference, so there is no control for them here. Each
        /// row is tagged with its storage scope (<see cref="AspidSettingsUI.SharedScopeClass"/> /
        /// <see cref="AspidSettingsUI.UserScopeClass"/>) matching where <see cref="SerializeReferenceSettings"/>
        /// persists it; the classes paint a scope stripe on the branded surfaces.
        /// </summary>
        public static void BuildControls(VisualElement container, AspidSettingsScope scope = AspidSettingsScope.All)
        {
            if ((scope & AspidSettingsScope.User) != 0)
            {
                container.Add(CreateBreakageDetectionSwitch());
                container.Add(AspidSettingsUI.CreateRowNote(
                    "Watches for references broken by script renames / deletes and points at Repair."));
            }

            if ((scope & AspidSettingsScope.Shared) == 0) return;

            var autoDeAlias = new AspidSwitch("Auto de-alias duplicated list elements")
            {
                value = SerializeReferenceSettings.AutoDeAliasEnabled,
                tooltip = "Give a duplicated list element its own independent instance instead of sharing the original's rid.\n"
                    + "Stored in a committed ProjectSettings asset, so every teammate (and CI) sees the same behaviour.",
            };
            autoDeAlias.WithScopeStripe(AspidSettingsUI.SharedScopeClass);
            autoDeAlias.RegisterValueChangedCallback(evt => SerializeReferenceSettings.AutoDeAliasEnabled = evt.newValue);
            SyncFromSettings(autoDeAlias, () => SerializeReferenceSettings.AutoDeAliasEnabled);
            container.Add(autoDeAlias);
            container.Add(AspidSettingsUI.CreateRowNote(
                "A duplicated list element gets its own instance instead of sharing the original's reference."));

            var severity = new EnumField("Build / CI gate", SerializeReferenceSettings.BuildSeverity)
            {
                tooltip = "Off: never check. Warn: log missing / unset-required references. Fail: abort the build / fail the CI job.\n"
                    + "Stored in a committed ProjectSettings asset, so it travels to a clean CI runner. "
                    + "CLI flags -srGateWarnOnly / -srGateFail override it per run.",
            };
            severity.WithScopeStripe(AspidSettingsUI.SharedScopeClass);
            severity.RegisterValueChangedCallback(evt => SerializeReferenceSettings.BuildSeverity = (GateSeverity)evt.newValue);
            SyncFromSettings<EnumField, Enum>(severity, () => SerializeReferenceSettings.BuildSeverity);
            container.Add(severity);
            container.Add(AspidSettingsUI.CreateRowNote(
                "Off — never check · Warn — log missing / unset-required references · Fail — abort the build / CI job."));

            // The excluded-folders control carries its own "Excluded scan folders" header row.
            container.Add(new SerializeReferenceExcludedFoldersField().WithScopeStripe(AspidSettingsUI.SharedScopeClass));
        }

        // Built by one definition so the window tab and the Preferences page both render (and live-sync) the same
        // switch.
        private static AspidSwitch CreateBreakageDetectionSwitch()
        {
            var breakageDetection = new AspidSwitch("Breakage detection")
            {
                value = SerializeReferenceSettings.BreakageDetectionEnabled,
                tooltip = "Watch for managed references that just became missing (renamed/deleted scripts) and surface a "
                    + "toast pointing at Repair. Turn off to silence the domain-reload / import-time detection entirely.\n"
                    + "Per-user setting — stored locally, never committed.",
            };
            breakageDetection.WithScopeStripe(AspidSettingsUI.UserScopeClass);
            breakageDetection.RegisterValueChangedCallback(evt => SerializeReferenceSettings.BreakageDetectionEnabled = evt.newValue);
            SyncFromSettings(breakageDetection, () => SerializeReferenceSettings.BreakageDetectionEnabled);
            return breakageDetection;
        }

        // Shorthand over the shared live-sync helper, binding to this store's Changed signal.
        private static void SyncFromSettings<TControl, TValue>(TControl control, Func<TValue> read)
            where TControl : VisualElement, INotifyValueChanged<TValue>
        {
            AspidSettingsUI.SyncFromSettings(
                control,
                read,
                handler => SerializeReferenceSettings.Changed += handler,
                handler => SerializeReferenceSettings.Changed -= handler);
        }
    }
}
