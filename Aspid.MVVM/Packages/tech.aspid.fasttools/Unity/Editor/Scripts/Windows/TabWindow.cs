using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The single managed-reference workbench. Two modes share one window: <b>Asset References</b> maps a saved asset's
    /// whole reference graph and repairs entries inline, and <b>Project References</b> sweeps the project for missing
    /// references and bulk-fixes them grouped by broken type. The per-asset repair list of the old Repair window is
    /// subsumed by the richer Inspect graph; the project sweep keeps its grouped bulk-fix flow.
    /// </summary>
    internal sealed class TabWindow : EditorWindow
    {
        private const string RootClass = "aspid-fasttools-serialize-reference-window";
        private const string BackgroundClass = RootClass + "__background";
        private const string ToolbarClass = RootClass + "__toolbar";
        private const string ToolbarButtonClass = RootClass + "__toolbar-button";
        private const string ToolbarButtonActiveClass = ToolbarButtonClass + "--active";
        private const string ToolbarButtonSquareClass = ToolbarButtonClass + "--square";
        private const string TabUnderlineClass = RootClass + "__tab-underline";
        private const string TabHintClass = RootClass + "__tab-hint";
        private const string TabIconClass = RootClass + "__tab-icon";
        private const string TabIconHomeClass = TabIconClass + "--home";
        private const string TabIconSettingsClass = TabIconClass + "--settings";
        private const string ContainerClass = RootClass + "__container";

        private const string WindowStyleSheetPath = "UI/SerializeReferences/Aspid-FastTools-SerializeReference-Window";

        // The Aspid brand mark shown beside the window title; padded variant so it doesn't dominate the tab.
        private const string WindowIconPath = "Icons/aspid_icon_window_tab_green_1022x1011";

        // Below this the toolbar tabs and cards degrade into slivers; applied in CreateGUI so every instance
        // gets it — including panes restored from a saved layout, which never pass through Reveal.
        private static readonly Vector2 _minWindowSize = new(480f, 360f);

        private AspidAnimatedDotsBackground _background;
        private VisualElement _container;
        private Button _homeButton;
        private Button _inspectButton;
        private Button _projectButton;
        private Button _settingsButton;

        [SerializeField] private Object _pendingTarget;

        // One-shot flag: the breakage-notification deep-link wants the project scanned immediately even from a cold
        // index, whereas a plain Project References tab click is warmth-gated inside the view. Consumed in SwitchMode.
        private bool _forceProjectScan;

        internal TabType CurrentTabType { get; private set; }

        #region Open Methods
        [MenuItem("Tools/Aspid 🐍/FastTools/Welcome", priority = 0)]
        public static void OpenWelcome()
        {
            var window = Open();
            window.SwitchMode(TabType.Welcome);

            WelcomeWindowStartup.MarkSeen();
        }

        [MenuItem("Tools/Aspid 🐍/FastTools/Asset References", priority = 20)]
        public static void OpenAssetReferences() =>
            OpenAssetReferences(Selection.activeObject);

        public static void OpenAssetReferences(Object target)
        {
            var window = Open();

            window._pendingTarget = target;
            window.SwitchMode(TabType.AssetReference);
        }

        [MenuItem("Tools/Aspid 🐍/FastTools/Project References", priority = 21)]
        public static void OpenProjectReferences() =>
            Open().SwitchMode(TabType.ProjectReferences);

        [MenuItem("Tools/Aspid 🐍/FastTools/Settings", priority = 40)]
        public static void OpenSettings() =>
            Open().SwitchMode(TabType.Settings);

        private static TabWindow Open()
        {
            var window = GetWindow<TabWindow>();
            window.Show();

            return window;
        }
        #endregion

        private void CreateGUI()
        {
            minSize = _minWindowSize;
            titleContent = new GUIContent("Aspid FastTools", Resources.Load<Texture2D>(WindowIconPath));

            var root = rootVisualElement;
            root.AddAspidThemeStyleSheets()
                .AddStyleSheetsFromResource(WindowStyleSheetPath)
                .AddClass(RootClass);

            // One dotted canvas, owned by the window, fills it behind everything; its tint follows the active view's
            // state via the SetCanvasStatus callback handed to each view.
            _background = new AspidAnimatedDotsBackground()
                .AddClass(BackgroundClass)
                .SetPickingMode(PickingMode.Ignore);

            _homeButton = SquareTabButton(TabType.Welcome, TabIconHomeClass);
            _inspectButton = ModeButton("Asset References", TabType.AssetReference);
            _projectButton = ModeButton("Project References", TabType.ProjectReferences);
            _settingsButton = SquareTabButton(TabType.Settings, TabIconSettingsClass);

            var toolbar = new VisualElement().AddClass(ToolbarClass);
            toolbar.AddChild(_homeButton)
                .AddChild(_inspectButton)
                .AddChild(_projectButton)
                .AddChild(_settingsButton);

            _container = new VisualElement().AddClass(ContainerClass);
            _container.style.flexGrow = 1;

            // The footer is owned by the window, not any single tab, so it stays pinned to the bottom across every
            // mode; _container (flex-grow:1) pushes it down.
            root.AddChild(_background)
                .AddChild(toolbar)
                .AddChild(_container)
                .AddChild(new AspidWindowFooter());

            SwitchMode(CurrentTabType);
        }

        private Button ModeButton(string label, TabType tabType)
        {
            var hint = TabWindowShortcuts.HintFor(tabType);

            var button = new Button(() => SwitchMode(tabType)) { text = label, tooltip = hint };
            button.AddClass(ToolbarButtonClass);

            // Shortcut badge, absolutely positioned so it floats over the button without disturbing the centred label.
            button.AddChild(new Label(hint)
                .AddClass(TabHintClass)
                .SetPickingMode(PickingMode.Ignore));

            // The active underline is a child bar, not a border-bottom — flipping a child's background-color via the
            // parent's --active class repaints reliably (a border-color flip only showed up after a window resize).
            button.AddChild(new VisualElement()
                .AddClass(TabUnderlineClass)
                .SetPickingMode(PickingMode.Ignore));

            return button;
        }

        // The edge tabs (home / settings) are square and icon-only: the USS --square modifier overrides the flex
        // sizing, the inner __tab-icon modifier supplies the glyph. Same underline bar as the mode tabs.
        private Button SquareTabButton(TabType tabType, string iconModifierClass)
        {
            var button = new Button(() => SwitchMode(tabType)) { tooltip = TabWindowShortcuts.HintFor(tabType) };
            button.AddClass(ToolbarButtonClass).AddClass(ToolbarButtonSquareClass);

            button.AddChild(new VisualElement()
                .AddClass(TabIconClass)
                .AddClass(iconModifierClass)
                .SetPickingMode(PickingMode.Ignore));

            button.AddChild(new VisualElement()
                .AddClass(TabUnderlineClass)
                .SetPickingMode(PickingMode.Ignore));

            return button;
        }

        internal void SwitchMode(TabType tabType)
        {
            CurrentTabType = tabType;
            if (_container is null) return; // Open() ran before CreateGUI; CreateGUI re-invokes SwitchMode(_mode).

            _container.Clear();

            if (tabType == TabType.Welcome)
            {
                // Welcome carries no single status; dropping the status class restores the default signal gradient a
                // prior view's wash flattened.
                SetCanvasStatus(StatusStyle.Type.None);
                _container.AddChild(new WelcomeView());
            }
            else if (tabType == TabType.AssetReference)
            {
                // Track the in-view pick back onto _pendingTarget so a tab switch rebuilds the view on the asset the user
                // actually has open, not the one Inspect first opened on.
                _container.AddChild(new SerializeReferenceGraphView(_pendingTarget, SetCanvasStatus, target => _pendingTarget = target));
            }
            else if (tabType == TabType.Settings)
            {
                // Settings carries no status either; the calm idle wash keeps the canvas neutral here.
                SetCanvasStatus(StatusStyle.Type.Info);
                _container.AddChild(new SettingsView());
            }
            else
            {
                var project = new SerializeReferenceProjectView
                {
                    OnInspectAsset = InspectAsset,
                    OnCanvasStatus = SetCanvasStatus,
                };
                _container.AddChild(project);

                // A plain tab switch never auto-scans (no scan freeze on large projects); only the
                // breakage-notification deep-link forces the scan.
                if (_forceProjectScan)
                {
                    _forceProjectScan = false;
                    project.ScanProject();
                }
                else
                {
                    project.Initialize();
                }
            }

            UpdateToolbar();
        }

        // The active view reports its state here; the window owns the shared dotted canvas and applies it as a status
        // class, so the wash itself stays in the canvas stylesheet rather than in any view.
        private void SetCanvasStatus(StatusStyle.Type status) =>
            _background?.SetStatus(status);

        // Cross-link: jumping from a project-audit result to that asset's full graph.
        private void InspectAsset(Object target)
        {
            _pendingTarget = target;
            SwitchMode(TabType.AssetReference);
        }

        private void UpdateToolbar()
        {
            _homeButton?.EnableInClassList(ToolbarButtonActiveClass, CurrentTabType == TabType.Welcome);
            _inspectButton?.EnableInClassList(ToolbarButtonActiveClass, CurrentTabType == TabType.AssetReference);
            _projectButton?.EnableInClassList(ToolbarButtonActiveClass, CurrentTabType == TabType.ProjectReferences);
            _settingsButton?.EnableInClassList(ToolbarButtonActiveClass, CurrentTabType == TabType.Settings);
        }
    }
}
