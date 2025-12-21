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
        
        private AspidToggle? _profilerToggle;
        private AspidToggle? _binderLogToggle;
        private AspidToggle? _editorChecksToggle;
        
        private Button? _applyButton;
        private Button? _revertButton;
        
        // Temporary values for changes
        private bool _tempProfilerValue;
        private bool _tempBinderLogValue;
        private bool _tempEditorChecksValue;
        
        private bool _hasChanges;
        
        // Original values for comparison
        private bool _originalProfilerValue;
        private bool _originalBinderLogValue;
        private bool _originalEditorChecksValue;

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
            
            // Main content area (header + settings)
            var mainContent = new VisualElement()
                .SetFlexGrow(1)
                .SetFlexDirection(FlexDirection.Column);
            mainContent.AddToClassList("main-content");
            
            // Header
            var header = CreateHeader();
            mainContent.Add(header);
            
            // Settings section
            var settingsSection = CreateSettingsSection();
            mainContent.Add(settingsSection);
            
            // Action buttons (Apply/Revert)
            var actionButtons = CreateActionButtons();
            settingsSection.Add(actionButtons);
            
            container.Add(mainContent);
            
            // Footer - always at bottom
            var footer = CreateFooter();
            container.Add(footer);
            
            // Initialize values
            LoadCurrentValues();
            
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
                .SetName("settings-section");
            section.AddToClassList("settings-section");
            
            var sectionTitle = new Label("Build Settings");
            sectionTitle.AddToClassList("section-title");
            section.Add(sectionTitle);
            
            // Profiler Toggle
            _profilerToggle = CreateSettingToggle(
                "Enable Profiler",
                "Enable profiler markers for performance monitoring in the Unity Profiler.",
                AspidMvvmSettings.IsEnabledProfiler,
                value => { _tempProfilerValue = value; CheckForChanges(); }
            );
            section.Add(_profilerToggle);
            
            var sectionTitle2 = new Label("Editor Settings")
                .SetMargin(top: 10);
            sectionTitle2.AddToClassList("section-title");
            section.Add(sectionTitle2);
            
            // Binder Log Toggle
            _binderLogToggle = CreateSettingToggle(
                "Enable Binder Log",
                "Enable detailed logging for binder operations. Useful for debugging bindings.",
                AspidMvvmSettings.IsEnabledBinderLog,
                value => { _tempBinderLogValue = value; CheckForChanges(); }
            );
            section.Add(_binderLogToggle);
            
            // Editor Checks Toggle
            _editorChecksToggle = CreateSettingToggle(
                "Checks for Editor",
                "Enable additional validation checks in the Editor. Helps catch errors early.",
                AspidMvvmSettings.IsEnabledCheckForEditor,
                value => { _tempEditorChecksValue = value; CheckForChanges(); }
            );
            section.Add(_editorChecksToggle);
            
            return section;
        }

        private AspidToggle CreateSettingToggle(string label, string tooltip, bool initialValue, System.Action<bool> onValueChanged)
        {
            var toggle = new AspidToggle(label, initialValue)
            {
                tooltip = tooltip
            };
            toggle.AddToClassList("setting-row");
            toggle.OnValueChanged += onValueChanged;
            
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

        private VisualElement CreateActionButtons()
        {
            var container = new VisualElement()
                .SetName("action-buttons")
                .SetFlexDirection(FlexDirection.Row)
                .SetJustifyContent(Justify.FlexEnd);
            container.AddToClassList("action-buttons");
            
            _revertButton = new Button(OnRevert)
            {
                text = "Revert"
            };
            _revertButton.AddToClassList("action-button");
            _revertButton.AddToClassList("revert-button");
            _revertButton.SetEnabled(false);
            
            _applyButton = new Button(OnApply)
            {
                text = "Apply"
            };
            _applyButton.AddToClassList("action-button");
            _applyButton.AddToClassList("apply-button");
            _applyButton.SetEnabled(false);
            
            container.Add(_revertButton);
            container.Add(_applyButton);
            
            return container;
        }

        private void LoadCurrentValues()
        {
            _originalProfilerValue = AspidMvvmSettings.IsEnabledProfiler;
            _originalBinderLogValue = AspidMvvmSettings.IsEnabledBinderLog;
            _originalEditorChecksValue = AspidMvvmSettings.IsEnabledCheckForEditor;
            
            _tempProfilerValue = _originalProfilerValue;
            _tempBinderLogValue = _originalBinderLogValue;
            _tempEditorChecksValue = _originalEditorChecksValue;
            
            _hasChanges = false;
            UpdateButtonStates();
        }

        private void CheckForChanges()
        {
            _hasChanges = _tempProfilerValue != _originalProfilerValue ||
                         _tempBinderLogValue != _originalBinderLogValue ||
                         _tempEditorChecksValue != _originalEditorChecksValue;
            
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            _applyButton?.SetEnabled(_hasChanges);
            _revertButton?.SetEnabled(_hasChanges);
        }

        private void OnApply()
        {
            AspidMvvmSettings.IsEnabledProfiler = _tempProfilerValue;
            AspidMvvmSettings.IsEnabledBinderLog = _tempBinderLogValue;
            AspidMvvmSettings.IsEnabledCheckForEditor = _tempEditorChecksValue;
            
            _originalProfilerValue = _tempProfilerValue;
            _originalBinderLogValue = _tempBinderLogValue;
            _originalEditorChecksValue = _tempEditorChecksValue;
            
            _hasChanges = false;
            UpdateButtonStates();
            
            Debug.Log("Aspid MVVM settings applied successfully.");
        }

        private void OnRevert()
        {
            _tempProfilerValue = _originalProfilerValue;
            _tempBinderLogValue = _originalBinderLogValue;
            _tempEditorChecksValue = _originalEditorChecksValue;
            
            _profilerToggle?.SetValueWithoutNotify(_originalProfilerValue);
            _binderLogToggle?.SetValueWithoutNotify(_originalBinderLogValue);
            _editorChecksToggle?.SetValueWithoutNotify(_originalEditorChecksValue);
            
            _hasChanges = false;
            UpdateButtonStates();
        }

        private void OnFocus()
        {
            // Refresh values when window gains focus
            LoadCurrentValues();
            
            if (_profilerToggle != null)
                _profilerToggle.SetValueWithoutNotify(_tempProfilerValue);
            
            if (_binderLogToggle != null)
                _binderLogToggle.SetValueWithoutNotify(_tempBinderLogValue);
            
            if (_editorChecksToggle != null)
                _editorChecksToggle.SetValueWithoutNotify(_tempEditorChecksValue);
        }
    }
}

