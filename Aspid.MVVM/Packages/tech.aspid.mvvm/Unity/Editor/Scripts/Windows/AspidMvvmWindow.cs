using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // The package window in the Aspid.FastTools workbench idiom: one dotted canvas behind a tab strip, the active tab's
    // view in the middle and the version footer pinned to the bottom. Welcome is the home tab, Settings the edge tab.
    internal sealed class AspidMvvmWindow : EditorWindow
    {
        private const string RootClass = "aspid-mvvm-window";
        private const string BackgroundClass = RootClass + "__background";
        private const string ToolbarClass = RootClass + "__toolbar";
        private const string ToolbarTitleClass = RootClass + "__toolbar-title";
        private const string ToolbarButtonClass = RootClass + "__toolbar-button";
        private const string ToolbarButtonActiveClass = ToolbarButtonClass + "--active";
        private const string ToolbarButtonSquareClass = ToolbarButtonClass + "--square";
        private const string TabUnderlineClass = RootClass + "__tab-underline";
        private const string TabIconClass = RootClass + "__tab-icon";
        private const string TabIconHomeClass = TabIconClass + "--home";
        private const string TabIconSettingsClass = TabIconClass + "--settings";
        private const string ContainerClass = RootClass + "__container";

        private const string WindowStyleSheetPath = "Styles/Windows/Aspid-MVVM-Window";
        private const string WindowIconPath = "Icons/aspid_icon_medium_green_1020x1008";

        // Below this the hero and the cards degrade into slivers. Applied in CreateGUI, so a pane restored from a saved
        // layout, which never passes through Open, gets it too.
        private static readonly Vector2 _minWindowSize = new(480f, 360f);

        private AspidAnimatedDotsBackground _background;
        private VisualElement _container;
        private Button _homeButton;
        private Button _settingsButton;

        internal TabType CurrentTabType { get; private set; }

        [MenuItem("Tools/Aspid 🐍/MVVM/Welcome", priority = 0)]
        public static void OpenWelcome()
        {
            Open().SwitchMode(TabType.Welcome);
            WelcomeWindowStartup.MarkSeen();
        }

        [MenuItem("Tools/Aspid 🐍/MVVM/Settings", priority = 40)]
        public static void OpenSettings() =>
            Open().SwitchMode(TabType.Settings);

        private static AspidMvvmWindow Open()
        {
            var window = GetWindow<AspidMvvmWindow>();
            window.Show();

            return window;
        }

        private void CreateGUI()
        {
            minSize = _minWindowSize;
            titleContent = new GUIContent("Aspid MVVM", Resources.Load<Texture2D>(WindowIconPath));

            var root = rootVisualElement;
            root.AddAspidThemeStyleSheets()
                .AddStyleSheetsFromResource(WindowStyleSheetPath)
                .AddClass(RootClass);

            _background = new AspidAnimatedDotsBackground()
                .AddClass(BackgroundClass)
                .SetPickingMode(PickingMode.Ignore);

            _homeButton = SquareTabButton(TabType.Welcome, TabIconHomeClass);
            _settingsButton = SquareTabButton(TabType.Settings, TabIconSettingsClass);

            // With only two edge tabs the strip's middle would be empty, so the window name fills it and keeps the two
            // squares pinned to the edges, where the FastTools workbench puts them too.
            var title = new Label("Aspid.MVVM")
                .AddClass(ToolbarTitleClass)
                .SetPickingMode(PickingMode.Ignore);

            var toolbar = new VisualElement().AddClass(ToolbarClass);
            toolbar.AddChild(_homeButton)
                .AddChild(title)
                .AddChild(_settingsButton);

            _container = new VisualElement().AddClass(ContainerClass);
            _container.style.flexGrow = 1;

            // The footer is owned by the window, not any single tab, so it stays pinned to the bottom across modes.
            root.AddChild(_background)
                .AddChild(toolbar)
                .AddChild(_container)
                .AddChild(new AspidMvvmWindowFooter());

            SwitchMode(CurrentTabType);
        }

        // The edge tabs are square and icon-only: the USS --square modifier overrides the flex sizing, the inner
        // __tab-icon modifier supplies the glyph. The active underline is a child bar, not a border-bottom, because
        // flipping a child's background-color via the parent's --active class repaints reliably.
        private Button SquareTabButton(TabType tabType, string iconModifierClass)
        {
            var button = new Button(() => SwitchMode(tabType)) { tooltip = AspidMvvmWindowShortcuts.HintFor(tabType) };
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
            if (_container is null) return; // Open() ran before CreateGUI; CreateGUI re-invokes SwitchMode.

            _container.Clear();

            if (tabType == TabType.Welcome)
            {
                // Welcome carries no single status; dropping the status class restores the default signal gradient.
                _background?.SetStatus(StatusStyle.Type.None);
                _container.AddChild(new WelcomeView());
            }
            else
            {
                // Settings carries no status either; the calm idle wash keeps the canvas neutral here.
                _background?.SetStatus(StatusStyle.Type.Info);
                _container.AddChild(new SettingsView());
            }

            UpdateToolbar();
        }

        private void UpdateToolbar()
        {
            _homeButton?.EnableInClassList(ToolbarButtonActiveClass, CurrentTabType == TabType.Welcome);
            _settingsButton?.EnableInClassList(ToolbarButtonActiveClass, CurrentTabType == TabType.Settings);
        }
    }
}
