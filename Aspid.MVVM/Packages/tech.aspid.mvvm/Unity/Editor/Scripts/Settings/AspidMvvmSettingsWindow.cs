#nullable enable
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using System.Text.RegularExpressions;
using Aspid.FastTools.UIElements.Editors.Internal;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Settings window for Aspid MVVM, styled to match the Aspid.FastTools Welcome window.
    /// Provides toggles for Profiler, Binder Log, and Editor Checks.
    /// </summary>
    public sealed class AspidMvvmSettingsWindow : EditorWindow
    {
        private const string MenuPath = "Tools/Aspid 🐍/Aspid.Mvvm Settings";
        private const string WindowTitle = "Aspid MVVM Settings";

        private const float MinWidth = 560f;
        private const float MinHeight = 420f;

        private const string ThemeStyleSheetPath = "UI/Aspid-FastTools-Default-Dark";
        private const string WindowStyleSheetPath = "Styles/Aspid-MVVM-AspidMvvmSettingsWindow";

        private const string PackageName = "tech.aspid.mvvm";
        private const string PackageManifestPath = "Assets/Aspid/MVVM/package.json";

        private const string GitHubUrl = "https://github.com/VPDPersonal/Aspid.MVVM";
        private const string GitHubReleasesUrl = GitHubUrl + "/releases";
        private const string GitHubReleaseTagUrlFormat = GitHubReleasesUrl + "/tag/v{0}";
        private const string DocumentationUrl = "https://vpd-inc.gitbook.io/aspid.mvvm";
        private const string AssetStoreUrl = "https://assetstore.unity.com/packages/slug/298463";

        private const string Icon1ResourcePath = "Icons/aspid_icon_medium_green_1020x1008";
        private const string Icon2ResourcePath = "Icons/aspid_icon_medium_yellow_1020x1008";
        private const string Icon3ResourcePath = "Icons/aspid_icon_medium_red_1020x1008";

        // Accent (hover) colors for the action buttons — mirror the bright status-text colors of the theme.
        private static readonly Color ApplyAccent = new(120f / 255f, 235f / 255f, 145f / 255f);
        private static readonly Color RevertAccent = new(235f / 255f, 95f / 255f, 95f / 255f);

        // Idle and hover gradient backgrounds — mirror the Default-Dark theme colors.
        private static readonly Color IdleGradient = new(26f / 255f, 26f / 255f, 26f / 255f);    // --aspid-colors-bg-darkness
        private static readonly Color ApplyGradient = new(12f / 255f, 65f / 255f, 30f / 255f);   // --aspid-colors-status-success-dark
        private static readonly Color RevertGradient = new(85f / 255f, 20f / 255f, 20f / 255f);  // --aspid-colors-status-error-dark

        private AspidToggle? _profilerToggle;
        private AspidToggle? _binderLogToggle;
        private AspidToggle? _editorChecksToggle;

        private AspidGradientButton? _applyButton;
        private AspidGradientButton? _revertButton;

        // Temporary values for changes
        private bool _tempProfilerValue;
        private bool _tempBinderLogValue;
        private bool _tempEditorChecksValue;

        private bool _hasChanges;

        // Original values for comparison
        private bool _originalProfilerValue;
        private bool _originalBinderLogValue;
        private bool _originalEditorChecksValue;

        [MenuItem(MenuPath, priority = 1)]
        public static void ShowWindow()
        {
            var window = GetWindow<AspidMvvmSettingsWindow>(
                utility: false,
                title: WindowTitle,
                focus: true);

            window.titleContent = new GUIContent(WindowTitle, Resources.Load<Texture2D>(Icon1ResourcePath));
            window.minSize = new Vector2(MinWidth, MinHeight);
            window.Show();
        }

        private void CreateGUI()
        {
            var root = rootVisualElement;
            root.Clear();

            AddStyleSheet(root, ThemeStyleSheetPath);
            AddStyleSheet(root, WindowStyleSheetPath);
            root.AddToClassList("settings-window-root");

            root.Add(new AspidAnimatedDotsBackground());
            root.Add(CreateContent());

            LoadCurrentValues();
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
            container.AddToClassList("content");

            var scroll = new ScrollView(ScrollViewMode.Vertical);
            scroll.AddToClassList("scroll");
            scroll.verticalScrollerVisibility = ScrollerVisibility.Auto;
            scroll.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

            scroll.Add(CreateHero());
            scroll.Add(CreateSettingsCard());

            container.Add(scroll);
            container.Add(CreateFooter());
            return container;
        }

        private static VisualElement CreateHero()
        {
            var hero = new VisualElement();
            hero.AddToClassList("hero");

            var logo = new AspidAnimatedLogo()
                .SetImage1(Resources.Load<Texture2D>(Icon1ResourcePath))
                .SetImage2(Resources.Load<Texture2D>(Icon2ResourcePath))
                .SetImage3(Resources.Load<Texture2D>(Icon3ResourcePath));
            logo.AddToClassList("logo");
            logo.AddManipulator(new Clickable(() => Application.OpenURL(AssetStoreUrl)));

            var heroText = new VisualElement();
            heroText.AddToClassList("hero-text");

            var title = new AspidAnimatedTitle("Welcome to Aspid.MVVM");

            var description = new AspidLabel(
                "A high-performance, Source Generator-based MVVM framework for Unity. " +
                "Configure the build-time defines for profiler markers, binder logging, and editor checks below.",
                AspidLabelPreset.Default
                    .SetLabelTheme(ThemeStyle.Type.Light)
                    .SetLabelSize(AspidLabelSizeStyle.Type.H4)
                    .SetLineSize(AspidDividingLineSizeStyle.Type.None)
                    .SetFontStyle(FontStyle.Normal)
                    .SetSelectable());
            description.AddToClassList("description");

            return hero
                .AddChild(logo)
                .AddChild(heroText
                    .AddChild(title)
                    .AddChild(description));
        }

        private VisualElement CreateSettingsCard()
        {
            var card = new VisualElement();
            card.AddToClassList("card");

            var buildHeader = CreateCardHeader("Build Settings");

            var editorHeader = CreateCardHeader("Editor Settings");
            editorHeader.AddToClassList("card-header--secondary");

            _profilerToggle = CreateSettingToggle(
                label: "Enable Profiler",
                tooltip: "Enable profiler markers for performance monitoring in the Unity Profiler.",
                AspidMvvmSettings.IsEnabledProfiler,
                onValueChanged: value =>
                {
                    _tempProfilerValue = value;
                    CheckForChanges();
                });

            _binderLogToggle = CreateSettingToggle(
                label: "Enable Binder Log",
                tooltip: "Enable detailed logging for binder operations. Useful for debugging bindings.",
                AspidMvvmSettings.IsEnabledBinderLog,
                onValueChanged: value =>
                {
                    _tempBinderLogValue = value;
                    CheckForChanges();
                });

            _editorChecksToggle = CreateSettingToggle(
                label: "Checks for Editor",
                tooltip: "Enable additional validation checks in the Editor. Helps catch errors early.",
                AspidMvvmSettings.IsEnabledCheckForEditor,
                onValueChanged: value =>
                {
                    _tempEditorChecksValue = value;
                    CheckForChanges();
                });

            return card
                .AddChild(buildHeader)
                .AddChild(_profilerToggle)
                .AddChild(editorHeader)
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
                container.AddToClassList("actions");

                _revertButton = CreateActionButton("Revert", _ => OnRevert(), RevertAccent, RevertGradient);
                _applyButton = CreateActionButton("Apply", _ => OnApply(), ApplyAccent, ApplyGradient);

                return container
                    .AddChild(_revertButton)
                    .AddChild(_applyButton);
            }
        }

        private static AspidLabel CreateCardHeader(string text)
        {
            var header = new AspidLabel(text, AspidLabelPreset.Default
                .SetLabelTheme(ThemeStyle.Type.Lightness)
                .SetLabelSize(AspidLabelSizeStyle.Type.H3)
                .SetLineTheme(ThemeStyle.Type.Dark)
                .SetLineStatus(StatusStyle.Type.Success));

            header.AddToClassList("card-header");
            return header;
        }

        private static AspidGradientButton CreateActionButton(string text, Action<EventBase> onClick, Color accent, Color hoverGradient)
        {
            var button = new AspidGradientButton(text, onClick)
                .SetAccent(accent)
                .SetGradient(IdleGradient);

            button.AddToClassList("action");

            // Deepen the pill toward the theme's status background on hover (Default-Dark style),
            // restoring the neutral idle background on leave.
            button.RegisterCallback<MouseEnterEvent>(_ => button.Gradient = hoverGradient);
            button.RegisterCallback<MouseLeaveEvent>(_ => button.Gradient = IdleGradient);

            return button;
        }

        private static VisualElement CreateFooter()
        {
            var footer = new VisualElement();
            footer.AddToClassList("footer");

            var version = ReadPackageVersion();
            var releaseUrl = version is "?"
                ? GitHubReleasesUrl
                : string.Format(GitHubReleaseTagUrlFormat, version);

            var versionLabel = new Label($"v{version}");
            versionLabel.AddToClassList("footer-version");
            versionLabel.AddManipulator(new Clickable(() => Application.OpenURL(releaseUrl)));

            var docsLabel = new Label("Documentation");
            docsLabel.AddToClassList("footer-link");
            docsLabel.AddManipulator(new Clickable(() => Application.OpenURL(DocumentationUrl)));

            var githubLabel = new Label("GitHub");
            githubLabel.AddToClassList("footer-link");
            githubLabel.AddManipulator(new Clickable(() => Application.OpenURL(GitHubUrl)));

            var links = new VisualElement();
            links.AddToClassList("footer-links");

            var row = new VisualElement();
            row.AddToClassList("footer-row");

            return footer
                .AddChild(new AspidDividingLine(AspidDividingLinePreset.Default
                    .SetTheme(ThemeStyle.Type.Darkness)))
                .AddChild(row
                    .AddChild(versionLabel)
                    .AddChild(links
                        .AddChild(docsLabel)
                        .AddChild(githubLabel)));
        }

        private static AspidToggle CreateSettingToggle(string label, string tooltip, bool initialValue, Action<bool> onValueChanged)
        {
            var toggle = new AspidToggle(label, initialValue)
            {
                tooltip = tooltip
            };

            toggle.AddToClassList("setting-row");
            toggle.OnValueChanged += onValueChanged;

            return toggle;
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
            // Hide the action buttons entirely while there is nothing to apply or revert.
            var display = _hasChanges ? DisplayStyle.Flex : DisplayStyle.None;

            if (_applyButton is not null) _applyButton.style.display = display;
            if (_revertButton is not null) _revertButton.style.display = display;
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

        private static string ReadPackageVersion()
        {
            var package = PackageInfo.FindForPackageName(PackageName);
            if (package is not null && !string.IsNullOrEmpty(package.version))
                return package.version;

            var manifest = AssetDatabase.LoadAssetAtPath<TextAsset>(PackageManifestPath);
            if (manifest is null) return "?";

            var match = Regex.Match(
                input: manifest.text,
                pattern: "\"version\"\\s*:\\s*\"([^\"]+)\"");

            return match.Success ? match.Groups[1].Value : "?";
        }

        private static void AddStyleSheet(VisualElement element, string resourcePath)
        {
            var styleSheet = Resources.Load<StyleSheet>(resourcePath);
            if (styleSheet != null) element.styleSheets.Add(styleSheet);
        }
    }
}
