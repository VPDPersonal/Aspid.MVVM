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
    /// Settings window for Aspid MVVM with an animated background.
    /// Provides toggles for Profiler, Binder Log, and Editor Checks.
    /// </summary>
    public sealed class AspidMvvmSettingsWindow : EditorWindow
    {
        private const string WindowTitle = "Aspid MVVM Settings";
        
        private const float MinWidth = 350f;
        private const float MinHeight = 300f;
        
        private static StyleSheet? _styleSheet;
        
        private static StyleSheet SettingsStyleSheet =>
            _styleSheet ??= Resources.Load<StyleSheet>("Styles/aspid-mvvm-settings-window");
        
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

        [MenuItem("Tools/🐍 Aspid/Aspid.Mvvm Settings", priority = 100)]
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
            rootVisualElement.Add(new FloatingBackgroundElement());
            rootVisualElement.Add(CreateContent());
        }
        
        private void OnFocus()
        {
            LoadCurrentValues();

            _profilerToggle?.SetValueWithoutNotify(_tempProfilerValue);
            _binderLogToggle?.SetValueWithoutNotify(_tempBinderLogValue);
            _editorChecksToggle?.SetValueWithoutNotify(_tempEditorChecksValue);
        }

        private VisualElement CreateContent()
        {
            var container = new VisualElement();
            container.AddToClassList("content-container");
            
            var scrollView = new ScrollView(ScrollViewMode.Vertical);
            scrollView.AddToClassList("main-scroll-view");
            scrollView.verticalScrollerVisibility = ScrollerVisibility.Auto;
            scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
            
            var mainContent = new VisualElement();
            mainContent.AddToClassList("main-content");
            
            mainContent.Add(CreateHeader());
            mainContent.Add(CreateSettingsSection());
            
            scrollView.Add(mainContent);
            container.Add(scrollView);
            container.Add(CreateFooter());
            
            LoadCurrentValues();
            return container;
        }

        private static VisualElement CreateHeader()
        {
            var header = new VisualElement();
            header.AddToClassList("header");

            var icon = new Image().SetImageFromResource(EditorConstants.AspidIconGreen);
            
            var titleContainer = new VisualElement()
                .SetFlexDirection(FlexDirection.Column)
                .SetFlexGrow(1);

            var titleLabel = new Label("Aspid MVVM");
            titleLabel.AddToClassList("header-title");
            
            var subtitle = new Label("Settings & Configuration");
            subtitle.AddToClassList("header-subtitle");
            
            return header
                .AddChild(icon)
                .AddChild(titleContainer
                    .AddChild(titleLabel)
                    .AddChild(subtitle));
        }

        private VisualElement CreateSettingsSection()
        {
            var section = new VisualElement();
            section.AddToClassList("settings-section");
            
            var buildSettingsTitle = new Label(text: "Build Settings");
            buildSettingsTitle.AddToClassList("section-title");
            
            var editorSettingsTitle = new Label(text: "Editor Settings");
            editorSettingsTitle.AddToClassList("section-title");
            
            _profilerToggle = CreateSettingToggle(
                label: "Enable Profiler",
                tooltip: "Enable profiler markers for performance monitoring in the Unity Profiler.",
                AspidMvvmSettings.IsEnabledProfiler,
                onValueChanged: value =>
                {
                    _tempProfilerValue = value;
                    CheckForChanges();
                }
            );
            
            _binderLogToggle = CreateSettingToggle(
                label: "Enable Binder Log",
                tooltip: "Enable detailed logging for binder operations. Useful for debugging bindings.",
                AspidMvvmSettings.IsEnabledBinderLog,
                onValueChanged: value =>
                {
                    _tempBinderLogValue = value;
                    CheckForChanges();
                }
            );
            
            _editorChecksToggle = CreateSettingToggle(
                label: "Checks for Editor",
                tooltip: "Enable additional validation checks in the Editor. Helps catch errors early.",
                AspidMvvmSettings.IsEnabledCheckForEditor,
                onValueChanged: value =>
                {
                    _tempEditorChecksValue = value;
                    CheckForChanges();
                }
            );

            return section
                .AddChild(buildSettingsTitle)
                .AddChild(_profilerToggle)
                .AddChild(editorSettingsTitle
                    .SetMargin(top: 10))
                .AddChild(_binderLogToggle)
                .AddChild(_editorChecksToggle)
                .AddChild(CreateActionButtons());
            
            void CheckForChanges()
            {
                _hasChanges =
                    _tempProfilerValue != _originalProfilerValue 
                    || _tempBinderLogValue != _originalBinderLogValue 
                    || _tempEditorChecksValue != _originalEditorChecksValue;
            
                UpdateButtonStates();
            }
            
            VisualElement CreateActionButtons()
            {
                var container = new VisualElement();
                container.AddToClassList("action-buttons");

                _revertButton = new Button(OnRevert).SetText("Revert");
                _revertButton.AddToClassList("action-button");
                _revertButton.AddToClassList("revert-button");
                _revertButton.SetEnabled(false);
            
                _applyButton = new Button(OnApply).SetText("Apply");
                _applyButton.AddToClassList("action-button");
                _applyButton.AddToClassList("apply-button");
                _applyButton.SetEnabled(false);
            
                return container
                    .AddChild(_revertButton)
                    .AddChild(_applyButton);
            }
        }

        private static VisualElement CreateFooter()
        {
            var footer = new VisualElement();
            footer.AddToClassList("footer");
            
            var versionLabel = new Label(text: $"Version {AspidMvvmSettings.Version}");
            
            var docsButton = new Button(clickEvent: () => Application.OpenURL("https://vpd-inc.gitbook.io/aspid.mvvm"))
                .SetText("Documentation");
            
            return footer
                .AddChild(versionLabel)
                .AddChild(docsButton);
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
            
            Debug.Log("<color=#0d8c5e>Aspid.MVVM settings applied successfully.</color>");
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
        
        private static AspidToggle CreateSettingToggle(string label, string tooltip, bool initialValue, System.Action<bool> onValueChanged)
        {
            var toggle = new AspidToggle(label, initialValue)
            {
                tooltip = tooltip
            };
            
            toggle.AddToClassList("setting-row");
            toggle.OnValueChanged += onValueChanged;
            
            return toggle;
        }
    }
}
