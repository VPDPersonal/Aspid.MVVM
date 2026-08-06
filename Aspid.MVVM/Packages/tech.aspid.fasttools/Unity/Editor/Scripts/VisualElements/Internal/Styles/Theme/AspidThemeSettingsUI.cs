using System;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Builds the "Appearance" settings controls bound to <see cref="AspidThemeSettings"/> — the override-StyleSheet
    /// picker (layered on top of the built-in Default-Dark palette, applied live) and a template action that writes a
    /// commented starter sheet and assigns it. Shared by the window's Settings tab and the Preferences page, so both
    /// render the same controls from one definition. Every row is a per-user preference
    /// (<see cref="AspidSettingsUI.UserScopeClass"/>).
    /// </summary>
    internal static class AspidThemeSettingsUI
    {
        private const string TemplateFileName = "Aspid-FastTools-Theme-Override";

        private const string TemplateContent =
            "/*\n" +
            " * Aspid FastTools — editor theme override.\n" +
            " * Redefine any design token below to recolour the Aspid editor UI.\n" +
            " * This sheet is layered on top of the built-in Default-Dark palette,\n" +
            " * so you only need to declare the tokens you want to change.\n" +
            " * Full token list: Packages/tech.aspid.fasttools/Unity/Editor/Resources/UI/Aspid-FastTools-Default-Dark.uss\n" +
            " */\n" +
            ":root {\n" +
            "    /* Backgrounds (surface palette, dark → light) */\n" +
            "    /* --aspid-colors-bg-darkness:  rgb(26, 26, 26); */\n" +
            "    /* --aspid-colors-bg-dark:      rgb(36, 36, 36); */\n" +
            "    /* --aspid-colors-bg-light:     rgb(46, 46, 46); */\n" +
            "    /* --aspid-colors-bg-lightness: rgb(56, 56, 56); */\n" +
            "\n" +
            "    /* Text (high-contrast content) */\n" +
            "    /* --aspid-colors-text-lightness: rgb(220, 220, 220); */\n" +
            "    /* --aspid-colors-text-light:     rgb(190, 190, 190); */\n" +
            "\n" +
            "    /* Accent example — tweak the success status base */\n" +
            "    /* --aspid-colors-status-success-dark: rgb(12, 65, 30); */\n" +
            "}\n";

        /// <summary>
        /// Appends the theme-override ObjectField and the create-template action row to <paramref name="container"/>,
        /// wired straight to <see cref="AspidThemeSettings"/>.
        /// </summary>
        public static void BuildControls(VisualElement container)
        {
            var overrideField = new ObjectField("Theme override")
            {
                objectType = typeof(StyleSheet),
                allowSceneObjects = false,
                value = AspidThemeSettings.OverrideStyleSheet,
                tooltip = "A USS sheet layered on top of the built-in Default-Dark palette; redefine any "
                    + "--aspid-colors-* / --aspid-icons-* token inside a :root block. Applies live; clear to return "
                    + "to the default look.\n"
                    + "Per-user setting — stored locally, never committed.",
            };
            overrideField.AddClass(AspidSettingsUI.UserScopeClass);
            overrideField.RegisterValueChangedCallback(evt =>
                AspidThemeSettings.OverrideStyleSheet = evt.newValue as StyleSheet);
            SyncFromSettings(overrideField, () => (UnityEngine.Object)AspidThemeSettings.OverrideStyleSheet);
            container.Add(overrideField);

            container.Add(BuildTemplateRow());
        }

        // The template action, so theming starts from a documented token list instead of a blank file.
        private static VisualElement BuildTemplateRow()
        {
            var row = new VisualElement().AddClass(AspidSettingsUI.RowClass).AddClass(AspidSettingsUI.UserScopeClass);
            row.tooltip = "Create a starter override .uss with the palette's tokens listed in comments, and assign it "
                + "as the theme override.";

            var caption = new Label("Override template").AddClass(AspidSettingsUI.RowCaptionClass);

            var create = new Button(CreateTemplate) { text = "Create template…" }
                .AddClass(AspidSettingsUI.ActionClass)
                .AddClass(AspidSettingsUI.ActionInfoClass);

            return row
                .AddChild(caption)
                .AddChild(create);
        }

        private static void CreateTemplate()
        {
            var path = EditorUtility.SaveFilePanelInProject(
                "Create Aspid Theme Override",
                TemplateFileName,
                "uss",
                "Choose where to save the theme override style sheet.");

            if (string.IsNullOrEmpty(path)) return;

            File.WriteAllText(path, TemplateContent);
            AssetDatabase.ImportAsset(path);

            var sheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            if (sheet == null) return;

            // Assign through the store: every surface's field mirrors it via the live-sync.
            AspidThemeSettings.OverrideStyleSheet = sheet;
            EditorGUIUtility.PingObject(sheet);
        }

        // Shorthand over the shared live-sync helper, binding to this store's Changed signal.
        private static void SyncFromSettings<TControl, TValue>(TControl control, Func<TValue> read)
            where TControl : VisualElement, INotifyValueChanged<TValue>
        {
            AspidSettingsUI.SyncFromSettings(
                control,
                read,
                handler => AspidThemeSettings.Changed += handler,
                handler => AspidThemeSettings.Changed -= handler);
        }
    }
}
