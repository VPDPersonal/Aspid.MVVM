#nullable enable
using UnityEditor;
using UnityEngine;
using Aspid.Internal;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Settings window for Aspid MVVM with animated background.
    /// Provides toggles for Profiler, Binder Log, and Editor Checks.
    /// </summary>
    public sealed class AspidMvvmSettingsWindow : EditorWindow
    {
        private const string WindowTitle = "Aspid MVVM Settings";
        private const float MinWidth = 350f;
        private const float MinHeight = 300f;
        
        private static StyleSheet? _styleSheet;
        private static StyleSheet SettingsStyleSheet => _styleSheet ??= Resources.Load<StyleSheet>("Styles/aspid-mvvm-settings-window");
        
        private Toggle? _profilerToggle;
        private Toggle? _binderLogToggle;
        private Toggle? _editorChecksToggle;

        [MenuItem("Tools/Aspid/Mvvm/Settings Window", priority = 100)]
        public static void ShowWindow()
        {
            var window = GetWindow<AspidMvvmSettingsWindow>();
            window.titleContent = new GUIContent(WindowTitle, Resources.Load<Texture2D>(EditorConstants.AspidIconGreen));
            window.minSize = new Vector2(MinWidth, MinHeight);
            window.Show();
        }

        private void CreateGUI()
        {
            rootVisualElement.styleSheets.Add(SettingsStyleSheet);
            rootVisualElement.AddToClassList("settings-window-root");
            
            // Background layer with floating animation
            var background = new FloatingBackgroundElement();
            background.AddToClassList("settings-background");
            rootVisualElement.Add(background);
            
            // Content layer
            var content = CreateContent();
            rootVisualElement.Add(content);
        }

        private VisualElement CreateContent()
        {
            var container = new VisualElement()
                .SetName("content-container")
                .SetFlexGrow(1)
                .SetFlexDirection(FlexDirection.Column);
            container.AddToClassList("content-container");
            
            // Header
            var header = CreateHeader();
            container.Add(header);
            
            // Settings section
            var settingsSection = CreateSettingsSection();
            container.Add(settingsSection);
            
            // Footer
            var footer = CreateFooter();
            container.Add(footer);
            
            return container;
        }

        private VisualElement CreateHeader()
        {
            var header = new VisualElement()
                .SetName("header")
                .SetFlexDirection(FlexDirection.Row)
                .SetAlignItems(Align.Center);
            header.AddToClassList("header");
            
            var icon = new Image()
                .SetImageFromResource(EditorConstants.AspidIconGreen)
                .SetSize(48);
            icon.AddToClassList("header-icon");
            
            var titleContainer = new VisualElement()
                .SetFlexDirection(FlexDirection.Column)
                .SetFlexGrow(1);
            
            var titleLabel = new Label("Aspid MVVM")
                .SetUnityFontStyleAndWeight(FontStyle.Bold);
            titleLabel.AddToClassList("header-title");
            
            var subtitle = new Label("Settings & Configuration");
            subtitle.AddToClassList("header-subtitle");
            
            titleContainer.Add(titleLabel);
            titleContainer.Add(subtitle);
            
            header.Add(icon);
            header.Add(titleContainer);
            
            return header;
        }

        private VisualElement CreateSettingsSection()
        {
            var section = new VisualElement()
                .SetName("settings-section")
                .SetFlexGrow(1);
            section.AddToClassList("settings-section");
            
            var sectionTitle = new Label("Build Settings");
            sectionTitle.AddToClassList("section-title");
            section.Add(sectionTitle);
            
            // Profiler Toggle
            _profilerToggle = CreateSettingToggle(
                "Enable Profiler",
                "Enable profiler markers for performance monitoring in the Unity Profiler.",
                AspidMvvmSettings.IsEnabledProfiler,
                value => AspidMvvmSettings.IsEnabledProfiler = value
            );
            section.Add(_profilerToggle);
            
            // Binder Log Toggle
            _binderLogToggle = CreateSettingToggle(
                "Enable Binder Log",
                "Enable detailed logging for binder operations. Useful for debugging bindings.",
                AspidMvvmSettings.IsEnabledBinderLog,
                value => AspidMvvmSettings.IsEnabledBinderLog = value
            );
            section.Add(_binderLogToggle);
            
            // Editor Checks Toggle
            _editorChecksToggle = CreateSettingToggle(
                "Checks for Editor",
                "Enable additional validation checks in the Editor. Helps catch errors early.",
                AspidMvvmSettings.IsEnabledCheckForEditor,
                value => AspidMvvmSettings.IsEnabledCheckForEditor = value
            );
            section.Add(_editorChecksToggle);
            
            return section;
        }

        private Toggle CreateSettingToggle(string label, string tooltip, bool initialValue, System.Action<bool> onValueChanged)
        {
            var container = new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .SetAlignItems(Align.Center);
            container.AddToClassList("setting-row");
            
            var toggle = new Toggle(label)
            {
                value = initialValue,
                tooltip = tooltip
            };
            toggle.AddToClassList("setting-toggle");
            
            toggle.RegisterValueChangedCallback(evt => onValueChanged(evt.newValue));
            
            container.Add(toggle);
            
            return toggle;
        }

        private VisualElement CreateFooter()
        {
            var footer = new VisualElement()
                .SetName("footer")
                .SetFlexDirection(FlexDirection.Row)
                .SetAlignItems(Align.Center);
            footer.AddToClassList("footer");
            
            var versionLabel = new Label($"Version {AspidMvvmSettings.Version}");
            versionLabel.AddToClassList("version-label");
            
            var spacer = new VisualElement().SetFlexGrow(1);
            
            var docsButton = new Button(() => Application.OpenURL("https://github.com/aspid-mvvm"))
            {
                text = "Documentation"
            };
            docsButton.AddToClassList("footer-button");
            
            footer.Add(versionLabel);
            footer.Add(spacer);
            footer.Add(docsButton);
            
            return footer;
        }

        private void OnFocus()
        {
            // Refresh toggle states when window gains focus
            if (_profilerToggle != null)
                _profilerToggle.SetValueWithoutNotify(AspidMvvmSettings.IsEnabledProfiler);
            
            if (_binderLogToggle != null)
                _binderLogToggle.SetValueWithoutNotify(AspidMvvmSettings.IsEnabledBinderLog);
            
            if (_editorChecksToggle != null)
                _editorChecksToggle.SetValueWithoutNotify(AspidMvvmSettings.IsEnabledCheckForEditor);
        }
    }
}

