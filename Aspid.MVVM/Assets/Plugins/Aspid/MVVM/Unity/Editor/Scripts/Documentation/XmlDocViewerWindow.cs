#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Xml.Linq;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.XmlDoc
{
    /// <summary>
    /// Editor window that displays parsed XML documentation for a type selected via <see cref="TypeSelectorWindow"/>.
    /// Supports multiple tabs in the left panel and opening types in new windows.
    /// Inline <c>&lt;see cref="..."/&gt;</c> references are rendered as clickable chips.
    /// </summary>
    public sealed class XmlDocViewerWindow : EditorWindow
    {
        private const string WindowTitle = "XML Doc Viewer";

        private const float MinWidth = 560f;
        private const float MinHeight = 360f;

        private static StyleSheet? _styleSheet;

        private static StyleSheet? DocStyleSheet =>
            _styleSheet ??= Resources.Load<StyleSheet>("Styles/aspid-xmldoc-viewer");

        private static readonly XmlDocParser _parser = new();

        private static readonly System.Text.RegularExpressions.Regex _whitespaceRun =
            new(@"\s+", System.Text.RegularExpressions.RegexOptions.Compiled);

        // Groups: 1=line comment, 2=block comment, 3=verbatim string, 4=string, 5=char,
        //         6=keyword, 7=camelCase method, 8=PascalCase ctor, 9=PascalCase method,
        //         10=PascalCase type, 11=number
        private static readonly System.Text.RegularExpressions.Regex _csharpTokenRegex = new(
                @"(//[^\n]*)" +
                @"|(\/\*[\s\S]*?\*\/)" +
                @"|(@""(?:[^""]|"""")*"")" +
                @"|(""(?:\\.|[^""\\])*"")" +
                @"|('(?:\\.|[^'\\])')" +
                @"|\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|partial|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|var|virtual|void|volatile|while|async|await|yield|get|set|value|add|remove|when|where)\b" +
                @"|\b([a-z_]\w*)(?=\s*\()" +
                @"|(?<=new[ \t]+)([A-Z]\w*)(?=\s*\()" +
                @"|\b([A-Z]\w*)(?=\s*\()" +
                @"|\b([A-Z]\w*)\b" +
                @"|\b(\d+(?:\.\d+)?(?:[fFdDmMlLuU]*)?)\b",
                System.Text.RegularExpressions.RegexOptions.Compiled);

        // ── Tab system ────────────────────────────────────────────────────────

        private sealed class TabEntry
        {
            public string Aqn          = string.Empty;
            public string TypeName     = string.Empty;
            public string Namespace    = string.Empty;
            public TypeDocumentation? Doc;
            public VisualElement?     TabButton;

            public bool IsEmpty => string.IsNullOrEmpty(Aqn);
        }

        private readonly List<TabEntry> _tabs = new();
        private int _activeTabIndex = -1;

        /// <summary>AQN of the type shown during the next CreateGUI (used when opening a new window).</summary>
        private string? _pendingAqn;

        private TabEntry? ActiveTab =>
            _activeTabIndex >= 0 && _activeTabIndex < _tabs.Count
                ? _tabs[_activeTabIndex] : null;

        // ── Panel resize state ────────────────────────────────────────────────

        private const float TabPanelCollapseThreshold = 60f;
        private const float TabPanelDefaultWidth      = 148f;

        private VisualElement? _tabPanelWrapper;
        private VisualElement? _tabPanelElement;
        private VisualElement? _dragHandle;
        private bool           _tabPanelVisible = true;
        private float          _tabPanelWidth   = TabPanelDefaultWidth;
        private bool           _isDragging;
        private float          _dragStartX;
        private float          _dragStartWidth;

        // ── UI references ─────────────────────────────────────────────────────

        private Label?         _namespaceLabel;
        private Label?         _typeLabel;
        private Button?        _selectButton;
        private Button?        _openInIdeButton;
        private VisualElement? _docContainer;
        private VisualElement? _tabList;
        private Button?        _showPanelButton;
        private Button?        _addTabButton;

        // ── Window lifecycle ──────────────────────────────────────────────────

        [MenuItem("Tools/\U0001f40d Aspid/XML Doc Viewer", priority = 200)]
        public static void ShowWindow()
        {
            var window = GetWindow<XmlDocViewerWindow>();
            window.titleContent = new GUIContent(WindowTitle);
            window.minSize = new Vector2(MinWidth, MinHeight);
            window.Show();
        }

        private void CreateGUI()
        {
            rootVisualElement.AddToClassList("doc-root");
            if (DocStyleSheet != null)
                rootVisualElement.styleSheets.Add(DocStyleSheet);

            rootVisualElement.Add(CreateGridBackground());
            rootVisualElement.Add(CreateLayout());

            // Always start with at least one tab
            AddNewTab();

            if (_pendingAqn != null)
            {
                var aqn = _pendingAqn;
                _pendingAqn = null;
                rootVisualElement.schedule.Execute(() => OpenInTab(aqn)).StartingIn(0);
            }
        }

        private VisualElement CreateLayout()
        {
            var root = new VisualElement()
                .SetFlexGrow(1)
                .SetFlexDirection(FlexDirection.Row)
                .SetPadding(top: 20, bottom: 20, left: 20, right: 20);

            // Left wrapper: tab panel (expanded) or collapsed toggle (when hidden)
            var wrapper = new VisualElement()
                .SetFlexDirection(FlexDirection.Column)
                .SetFlexShrink(0);
            wrapper.style.width = _tabPanelWidth;
            _tabPanelWrapper = wrapper;

            wrapper.Add(CreateTabPanel());
            root.Add(wrapper);

            // Drag handle
            var handle = new VisualElement();
            handle.AddToClassList("doc-drag-handle");
            // handle.style.cursor = new StyleCursor(MouseCursor.ResizeHorizontal);
            _dragHandle = handle;
            SetupDragHandle(handle);
            root.Add(handle);

            var content = new VisualElement()
                .SetFlexGrow(1)
                .SetFlexDirection(FlexDirection.Column);

            content.Add(CreateHeader());
            content.Add(CreateDocScrollView());
            root.Add(content);

            return root;
        }

        private void ToggleTabPanel()
        {
            _tabPanelVisible = !_tabPanelVisible;

            if (_tabPanelWrapper != null)
            {
                if (_tabPanelVisible)
                {
                    _tabPanelWrapper.SetDisplay(DisplayStyle.Flex);
                    _tabPanelWrapper.style.width = _tabPanelWidth;
                }
                else
                {
                    _tabPanelWrapper.SetDisplay(DisplayStyle.None);
                }
            }

            if (_tabPanelElement != null)
                _tabPanelElement.SetDisplay(_tabPanelVisible ? DisplayStyle.Flex : DisplayStyle.None);

            if (_dragHandle != null)
                _dragHandle.SetDisplay(_tabPanelVisible ? DisplayStyle.Flex : DisplayStyle.None);

            if (_showPanelButton != null)
                _showPanelButton.text = _tabPanelVisible ? "\u2039" : "\u203a"; // ‹ or ›
        }

        private void SetupDragHandle(VisualElement handle)
        {
            handle.RegisterCallback<PointerDownEvent>(evt =>
            {
                if (evt.button != 0) return;
                _isDragging    = true;
                _dragStartX    = evt.position.x;
                _dragStartWidth = _tabPanelWidth;
                handle.CapturePointer(evt.pointerId);
                evt.StopPropagation();
            });

            handle.RegisterCallback<PointerMoveEvent>(evt =>
            {
                if (!_isDragging) return;
                var newWidth = Mathf.Max(0f, _dragStartWidth + (evt.position.x - _dragStartX));

                if (_tabPanelWrapper != null)
                    _tabPanelWrapper.style.width = Mathf.Max(0f, newWidth);

                // Live-preview: hide/show the panel contents while dragging
                if (_tabPanelElement != null)
                    _tabPanelElement.SetDisplay(newWidth >= TabPanelCollapseThreshold ? DisplayStyle.Flex : DisplayStyle.None);

                evt.StopPropagation();
            });

            handle.RegisterCallback<PointerUpEvent>(evt =>
            {
                if (!_isDragging) return;
                _isDragging = false;
                handle.ReleasePointer(evt.pointerId);

                var finalWidth = Mathf.Max(0f, _dragStartWidth + (evt.position.x - _dragStartX));

                if (finalWidth < TabPanelCollapseThreshold)
                {
                    // Collapse
                    _tabPanelVisible = false;
                    _tabPanelWidth   = TabPanelDefaultWidth; // restore default for next expand
                    if (_tabPanelWrapper != null)  _tabPanelWrapper.SetDisplay(DisplayStyle.None);
                    if (_tabPanelElement != null)  _tabPanelElement.SetDisplay(DisplayStyle.None);
                    if (_dragHandle != null)       _dragHandle.SetDisplay(DisplayStyle.None);
                    if (_showPanelButton != null)  _showPanelButton.text = "\u203a"; // ›
                }
                else
                {
                    _tabPanelVisible = true;
                    _tabPanelWidth   = finalWidth;
                    if (_tabPanelWrapper != null)  { _tabPanelWrapper.SetDisplay(DisplayStyle.Flex); _tabPanelWrapper.style.width = finalWidth; }
                    if (_tabPanelElement != null)  _tabPanelElement.SetDisplay(DisplayStyle.Flex);
                    if (_dragHandle != null)       _dragHandle.SetDisplay(DisplayStyle.Flex);
                    if (_showPanelButton != null)  _showPanelButton.text = "\u2039"; // ‹
                }

                evt.StopPropagation();
            });
        }

        // ── Grid background ───────────────────────────────────────────────────

        private static VisualElement CreateGridBackground()
        {
            const int gridSize = 40;

            var grid = new VisualElement();
            grid.pickingMode = PickingMode.Ignore;
            grid.SetDistance(0)
                .SetOverflow(Overflow.Hidden)
                .SetPosition(Position.Absolute);

            grid.generateVisualContent += ctx =>
            {
                var bounds = grid.contentRect;
                if (bounds.width <= 0 || bounds.height <= 0) return;

                var painter = ctx.painter2D;
                painter.strokeColor = new Color(1f, 1f, 1f, 0.04f);
                painter.lineWidth = 1f;

                for (var x = gridSize; x < bounds.width; x += gridSize)
                {
                    painter.BeginPath();
                    painter.MoveTo(new Vector2(x, 0));
                    painter.LineTo(new Vector2(x, bounds.height));
                    painter.Stroke();
                }

                for (var y = gridSize; y < bounds.height; y += gridSize)
                {
                    painter.BeginPath();
                    painter.MoveTo(new Vector2(0, y));
                    painter.LineTo(new Vector2(bounds.width, y));
                    painter.Stroke();
                }
            };

            grid.RegisterCallback<GeometryChangedEvent>(_ => grid.MarkDirtyRepaint());
            return grid;
        }

        // ── Tab panel ─────────────────────────────────────────────────────────

        private VisualElement CreateTabPanel()
        {
            var panel = new VisualElement();
            panel.AddToClassList("doc-tab-panel");

            var scroll = new ScrollView(ScrollViewMode.Vertical);
            scroll.SetFlexGrow(1);
            scroll.verticalScrollerVisibility = ScrollerVisibility.AlwaysVisible;
            scroll.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

            _tabList = new VisualElement();
            _tabList.AddToClassList("doc-tab-list");
            scroll.Add(_tabList);

            // Add tab button inside scroll, right after the tab list
            var addTabBtn = new Button(AddNewTab);
            addTabBtn.AddToClassList("doc-add-tab-btn");
            addTabBtn.text = "+ New Tab";
            _addTabButton = addTabBtn;
            scroll.Add(addTabBtn);

            panel.Add(scroll);

            _tabPanelElement = panel;
            return panel;
        }

        private VisualElement BuildTabItem(TabEntry tab)
        {
            var item = new VisualElement();
            item.AddToClassList("doc-tab");

            var displayName = tab.IsEmpty ? "New Tab" : tab.TypeName;
            var name = new Label(displayName);
            name.AddToClassList("doc-tab-name");
            name.tooltip = tab.IsEmpty
                ? "New Tab"
                : string.IsNullOrEmpty(tab.Namespace)
                    ? tab.TypeName
                    : $"{tab.Namespace}.{tab.TypeName}";

            var close = new Button();
            close.AddToClassList("doc-tab-close");
            close.text = "\u00d7"; // ×
            close.clicked += () =>
            {
                var idx = _tabs.IndexOf(tab);
                if (idx >= 0) CloseTab(idx);
            };

            item.Add(name);
            item.Add(close);

            item.RegisterCallback<ClickEvent>(evt =>
            {
                if (evt.target is Button) return; // let close button handle itself
                var idx = _tabs.IndexOf(tab);
                if (idx >= 0) SetActiveTab(idx);
            });

            tab.TabButton = item;
            return item;
        }

        private void SetActiveTab(int index)
        {
            if (index < 0 || index >= _tabs.Count) return;

            if (_activeTabIndex >= 0 && _activeTabIndex < _tabs.Count)
                _tabs[_activeTabIndex].TabButton?.RemoveFromClassList("doc-tab--active");

            _activeTabIndex = index;
            _tabs[index].TabButton?.AddToClassList("doc-tab--active");

            UpdateHeaderForTab(_tabs[index]);
            RefreshDocDisplay();
        }

        private void CloseTab(int index)
        {
            if (index < 0 || index >= _tabs.Count) return;

            // If closing the last tab, reset it to empty instead of removing
            if (_tabs.Count == 1)
            {
                var tab = _tabs[0];
                tab.Aqn       = string.Empty;
                tab.TypeName  = string.Empty;
                tab.Namespace = string.Empty;
                tab.Doc       = null;

                var nameLabel = tab.TabButton?.Q<Label>(className: "doc-tab-name");
                if (nameLabel != null)
                {
                    nameLabel.text    = "New Tab";
                    nameLabel.tooltip = "New Tab";
                }

                _activeTabIndex = -1;
                SetActiveTab(0);
                UpdateAddTabButtonVisibility();
                return;
            }

            _tabs[index].TabButton?.RemoveFromHierarchy();
            _tabs.RemoveAt(index);

            var newIndex = Mathf.Clamp(index, 0, _tabs.Count - 1);
            _activeTabIndex = -1; // force SetActiveTab to apply styles
            SetActiveTab(newIndex);
            UpdateAddTabButtonVisibility();
        }

        private void AddNewTab()
        {
            var tab = new TabEntry();
            _tabs.Add(tab);
            _tabList?.Add(BuildTabItem(tab));
            SetActiveTab(_tabs.Count - 1);
            UpdateAddTabButtonVisibility();
        }

        private void UpdateAddTabButtonVisibility()
        {
            // Always visible
        }

        // ── Opening types ─────────────────────────────────────────────────────

        private void OpenInTab(string aqn)
        {
            // Reuse existing tab if already open
            var existing = _tabs.FindIndex(t => t.Aqn == aqn);
            if (existing >= 0)
            {
                SetActiveTab(existing);
                return;
            }

            var type = Type.GetType(aqn);
            if (type == null) return;

            // If the active tab is empty (New Tab), update it in-place
            if (ActiveTab is { IsEmpty: true })
            {
                var tab = ActiveTab;
                tab.Aqn       = aqn;
                tab.TypeName  = type.Name;
                tab.Namespace = GetTypeNamespace(type);
                tab.Doc       = LoadDocForType(type);

                var nameLabel = tab.TabButton?.Q<Label>(className: "doc-tab-name");
                if (nameLabel != null)
                {
                    nameLabel.text    = tab.TypeName;
                    nameLabel.tooltip = string.IsNullOrEmpty(tab.Namespace)
                        ? tab.TypeName
                        : $"{tab.Namespace}.{tab.TypeName}";
                }

                UpdateHeaderForTab(tab);
                RefreshDocDisplay();
                UpdateAddTabButtonVisibility();
                return;
            }

            var newTab = new TabEntry
            {
                Aqn       = aqn,
                TypeName  = type.Name,
                Namespace = GetTypeNamespace(type),
                Doc       = LoadDocForType(type),
            };
            _tabs.Add(newTab);
            _tabList?.Add(BuildTabItem(newTab));

            SetActiveTab(_tabs.Count - 1);
            UpdateAddTabButtonVisibility();
        }

        private static void OpenInNewWindow(string aqn)
        {
            var window = CreateInstance<XmlDocViewerWindow>();
            window._pendingAqn = aqn;
            window.titleContent = new GUIContent(WindowTitle);
            window.minSize = new Vector2(MinWidth, MinHeight);
            window.Show();
        }

        // ── Header ───────────────────────────────────────────────────────────

        private VisualElement CreateHeader()
        {
            var header = new VisualElement();
            header.AddToClassList("doc-header");

            // Toggle button — always in top-left of header
            _showPanelButton = new Button(ToggleTabPanel);
            _showPanelButton.AddToClassList("doc-show-panel-btn");
            _showPanelButton.text = "\u2039"; // ‹ (changes to › when collapsed)

            _namespaceLabel = new Label();
            _namespaceLabel.AddToClassList("doc-header-namespace");
            _namespaceLabel.SetDisplay(DisplayStyle.None);

            // Top row: toggle button + namespace label
            var topRow = new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .SetAlignItems(Align.Center);
            topRow.style.marginBottom = 4;
            topRow.Add(_showPanelButton);
            topRow.Add(_namespaceLabel);

            // Bottom row: type name + action buttons
            _typeLabel = new Label("No type selected");
            _typeLabel.AddToClassList("doc-header-typename");
            _typeLabel.AddToClassList("doc-header-typename--empty");

            _openInIdeButton = new Button(OpenCurrentTypeInIde);
            _openInIdeButton.AddToClassList("doc-open-btn");
            _openInIdeButton.text = "Open";
            _openInIdeButton.SetDisplay(DisplayStyle.None);

            _selectButton = new Button(OpenTypeSelector);
            _selectButton.AddToClassList("doc-select-btn");
            _selectButton.text = "Select Type\u2026";

            var buttonGroup = new VisualElement()
                .SetFlexDirection(FlexDirection.Column);
            buttonGroup.style.marginLeft = 12;
            _selectButton.style.marginLeft = 0;
            _openInIdeButton.style.marginLeft = 0;
            _openInIdeButton.style.marginTop = 4;
            buttonGroup.Add(_selectButton);
            buttonGroup.Add(_openInIdeButton);

            var titleRow = new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .SetAlignItems(Align.Center);

            var titleText = new VisualElement().SetFlexGrow(1);
            titleText.Add(_typeLabel);

            titleRow.Add(titleText);
            titleRow.Add(buttonGroup);

            header.Add(topRow);
            header.Add(titleRow);

            return header;
        }

        private VisualElement CreateDocScrollView()
        {
            var scroll = new ScrollView(ScrollViewMode.Vertical);
            scroll.SetFlexGrow(1);
            scroll.verticalScrollerVisibility = ScrollerVisibility.Auto;
            scroll.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

            _docContainer = new VisualElement()
                .SetFlexDirection(FlexDirection.Column)
                .SetFlexGrow(1);
            scroll.Add(_docContainer);

            _docContainer.Add(CreateEmptyState());
            return scroll;
        }

        private void UpdateHeaderForTab(TabEntry tab)
        {
            if (tab.IsEmpty)
            {
                ResetHeader();
                return;
            }

            if (_namespaceLabel != null)
            {
                if (!string.IsNullOrEmpty(tab.Namespace))
                {
                    _namespaceLabel.text = tab.Namespace;
                    _namespaceLabel.SetDisplay(DisplayStyle.Flex);
                }
                else
                {
                    _namespaceLabel.SetDisplay(DisplayStyle.None);
                }
            }

            if (_typeLabel != null)
            {
                _typeLabel.text = tab.TypeName;
                _typeLabel.RemoveFromClassList("doc-header-typename--empty");
            }

            _openInIdeButton?.SetDisplay(DisplayStyle.Flex);
        }

        private void ResetHeader()
        {
            _namespaceLabel?.SetDisplay(DisplayStyle.None);

            if (_typeLabel != null)
            {
                _typeLabel.text = "No type selected";
                _typeLabel.AddToClassList("doc-header-typename--empty");
            }

            _openInIdeButton?.SetDisplay(DisplayStyle.None);
        }

        // ── Type selection ────────────────────────────────────────────────────

        private void OpenTypeSelector()
        {
            var buttonBounds = _selectButton!.worldBound;
            var screenRect = new Rect(
                position.x + buttonBounds.x,
                position.y + buttonBounds.yMax + 22f,
                buttonBounds.width,
                0f);

            TypeSelectorWindow.Show(
                types: new[] { typeof(MonoBehaviour) },
                screenRect: screenRect,
                currentAqn: ActiveTab?.Aqn ?? string.Empty,
                onSelected: OnTypeSelected);
        }

        private void OnTypeSelected(string aqn)
        {
            if (_activeTabIndex >= 0 && _activeTabIndex < _tabs.Count)
            {
                // Navigate in-place within the current tab
                var type = Type.GetType(aqn);
                if (type == null) return;

                var tab = _tabs[_activeTabIndex];
                tab.Aqn       = aqn;
                tab.TypeName  = type.Name;
                tab.Namespace = GetTypeNamespace(type);
                tab.Doc       = LoadDocForType(type);

                // Refresh the tab button label
                var nameLabel = tab.TabButton?.Q<Label>(className: "doc-tab-name");
                if (nameLabel != null)
                {
                    nameLabel.text    = tab.TypeName;
                    nameLabel.tooltip = string.IsNullOrEmpty(tab.Namespace)
                        ? tab.TypeName
                        : $"{tab.Namespace}.{tab.TypeName}";
                }

                UpdateHeaderForTab(tab);
                RefreshDocDisplay();
                UpdateAddTabButtonVisibility();
            }
            else
            {
                OpenInTab(aqn);
            }
        }

        // ── Cref navigation ───────────────────────────────────────────────────

        private void OpenCrefInTab(string cref)
        {
            var type = ResolveCrefType(cref);
            if (type?.AssemblyQualifiedName is { } aqn)
                OpenInTab(aqn);
        }

        private static void OpenCrefInNewWindow(string cref)
        {
            var type = ResolveCrefType(cref);
            if (type?.AssemblyQualifiedName is { } aqn)
                OpenInNewWindow(aqn);
        }

        private static Type? ResolveCrefType(string cref)
        {
            var typeName = XmlDocParser.CrefToTypeName(cref);
            return string.IsNullOrEmpty(typeName) ? null : FindTypeByName(typeName);
        }

        private static void OpenCrefInIde(string cref)
        {
            var typeName = XmlDocParser.CrefToTypeName(cref);
            if (!string.IsNullOrEmpty(typeName))
                OpenTypeNameInIde(typeName);
        }

        private void OpenCurrentTypeInIde()
        {
            var type = ActiveTab != null ? Type.GetType(ActiveTab.Aqn) : null;
            if (type != null)
                OpenTypeNameInIde(type.Name);
        }

        private static void OpenTypeNameInIde(string typeName)
        {
            var guids = AssetDatabase.FindAssets($"t:Script {typeName}");
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (Path.GetFileNameWithoutExtension(assetPath) != typeName) continue;

                var asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
                if (asset != null)
                {
                    AssetDatabase.OpenAsset(asset);
                    return;
                }
            }
        }

        private void ShowCrefMenu(string cref)
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Open in Tab"),        false, () => OpenCrefInTab(cref));
            menu.AddItem(new GUIContent("Open in New Window"), false, () => OpenCrefInNewWindow(cref));
            menu.AddSeparator(string.Empty);
            menu.AddItem(new GUIContent("Open in IDE"),        false, () => OpenCrefInIde(cref));
            menu.ShowAsContext();
        }

        // ── Inheritdoc resolution ─────────────────────────────────────────────

        private static void ResolveInheritedDocs(Type type, TypeDocumentation doc)
        {
            var pending = doc.Members.Values
                .Where(m => m.InheritsDoc && m.Summary == null && m.SummaryXml == null)
                .ToList();

            if (pending.Count == 0) return;

            foreach (var baseType in EnumerateBaseTypes(type))
            {
                if (pending.Count == 0) break;

                var sourcePath = FindSourceFile(baseType.Name);
                if (sourcePath == null) continue;

                var baseDoc = _parser.ParseType(sourcePath);
                if (baseDoc == null) continue;

                for (var i = pending.Count - 1; i >= 0; i--)
                {
                    var member = pending[i];
                    if (!baseDoc.Members.TryGetValue(member.Name, out var baseMember)) continue;
                    if (baseMember.Summary == null && baseMember.SummaryXml == null) continue;

                    member.Summary       = baseMember.Summary;
                    member.SummaryXml    = baseMember.SummaryXml;
                    member.Remarks       = baseMember.Remarks;
                    member.RemarksXml    = baseMember.RemarksXml;
                    member.Returns       = baseMember.Returns;
                    member.InheritedFrom = baseType.Name;

                    foreach (var (k, v) in baseMember.Parameters)
                        member.Parameters.TryAdd(k, v);

                    foreach (var (k, v) in baseMember.TypeParameters)
                        member.TypeParameters.TryAdd(k, v);

                    pending.RemoveAt(i);
                }
            }
        }

        private static IEnumerable<Type> EnumerateBaseTypes(Type type)
        {
            var current = type.BaseType;
            while (current != null && current != typeof(object))
            {
                yield return current;
                current = current.BaseType;
            }
            foreach (var iface in type.GetInterfaces())
                yield return iface;
        }

        private static Type? FindTypeByName(string typeName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var type = assembly.GetTypes().FirstOrDefault(t => t.Name == typeName);
                    if (type != null) return type;
                }
                catch
                {
                    // Skip assemblies that can't be reflected.
                }
            }
            return null;
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static TypeDocumentation? LoadDocForType(Type type)
        {
            var sourcePath = FindSourceFile(type.Name);
            if (sourcePath == null) return null;

            var doc = _parser.ParseType(sourcePath);
            if (doc != null)
                ResolveInheritedDocs(type, doc);
            return doc;
        }

        private static string GetTypeNamespace(Type type)
        {
            var fullName = type.FullName ?? type.Name;
            var dot = fullName.LastIndexOf('.');
            return dot >= 0 ? fullName[..dot] : string.Empty;
        }

        private static string? FindSourceFile(string typeName)
        {
            var guids = AssetDatabase.FindAssets($"t:Script {typeName}");
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (Path.GetFileNameWithoutExtension(assetPath) == typeName)
                    return assetPath;
            }
            return null;
        }

        // ── Documentation display ─────────────────────────────────────────────

        private void RefreshDocDisplay()
        {
            if (_docContainer == null) return;
            _docContainer.Clear();

            var doc = ActiveTab?.Doc;

            if (doc == null)
            {
                _docContainer.Add(CreateEmptyState());
                return;
            }

            if (doc.SummaryXml != null)
                AddRichSection(_docContainer, "Summary", doc.SummaryXml);
            else if (!string.IsNullOrWhiteSpace(doc.Summary))
                AddPlainSection(_docContainer, "Summary", doc.Summary!);

            if (doc.RemarksXml != null)
                AddRichSection(_docContainer, "Remarks", doc.RemarksXml);
            else if (!string.IsNullOrWhiteSpace(doc.Remarks))
                AddPlainSection(_docContainer, "Remarks", doc.Remarks!);

            if (doc.Examples.Count > 0)
            {
                _docContainer.Add(CreateSectionHeader("Examples"));
                foreach (var example in doc.Examples)
                    _docContainer.Add(RenderExampleElement(example));
            }

            if (doc.Members.Count > 0)
            {
                _docContainer.Add(CreateSectionHeader("Members"));
                foreach (var (_, memberDoc) in doc.Members)
                    _docContainer.Add(CreateMemberCard(memberDoc));
            }
        }

        // ── Section helpers ───────────────────────────────────────────────────

        private void AddRichSection(VisualElement container, string title, XElement xml)
        {
            container.Add(CreateSectionHeader(title));
            container.Add(RenderXmlElement(xml));
        }

        private static void AddPlainSection(VisualElement container, string title, string text)
        {
            container.Add(CreateSectionHeader(title));
            container.Add(CreateTextLabel(text));
        }

        private static VisualElement CreateSectionHeader(string title)
        {
            var container = new VisualElement();
            container.AddToClassList("doc-section-header");

            var label = new Label(title.ToUpperInvariant());
            label.AddToClassList("doc-section-title");
            container.Add(label);

            var divider = new VisualElement();
            divider.AddToClassList("doc-section-divider");
            container.Add(divider);

            return container;
        }

        private static Label CreateTextLabel(string text)
        {
            var label = new Label(text);
            label.AddToClassList("doc-text");
            return label;
        }

        // ── Empty state ──────────────────────────────────────────────────────

        private VisualElement CreateEmptyState()
        {
            var container = new VisualElement();
            container.AddToClassList("doc-empty");

            var icon = new Label("{ }");
            icon.AddToClassList("doc-empty-icon");

            var title = new Label("Select a type to view its documentation");
            title.AddToClassList("doc-empty-title");

            var btn = new Button(OpenTypeSelector);
            btn.AddToClassList("doc-select-btn");
            btn.text = "Select Type\u2026";

            container.Add(icon);
            container.Add(title);
            container.Add(btn);
            return container;
        }

        // ── Example renderer ──────────────────────────────────────────────────

        private VisualElement RenderExampleElement(XElement example)
        {
            var container = new VisualElement()
                .SetFlexDirection(FlexDirection.Column)
                .SetMargin(bottom: 4);

            foreach (var node in example.Nodes())
            {
                switch (node)
                {
                    case XText textNode:
                    {
                        var text = textNode.Value.Trim();
                        if (!string.IsNullOrEmpty(text))
                            container.Add(CreateTextLabel(text));
                        break;
                    }
                    case XElement child:
                    {
                        var tag = child.Name.LocalName.ToLowerInvariant();
                        if (tag == "code")
                            container.Add(CreateCodeBlock(child.Value));
                        else
                            container.Add(RenderXmlElement(child));
                        break;
                    }
                }
            }

            return container;
        }

        private static VisualElement CreateCodeBlock(string code)
        {
            var label = new Label(HighlightCSharp(code.Trim()));
            label.AddToClassList("doc-code-block");
            label.enableRichText = true;
            label.style.whiteSpace = WhiteSpace.Normal;
            return label;
        }

        private static string HighlightCSharp(string code)
        {
            var lines = code.Replace("\r\n", "\n").Split('\n');
            var indentBuilder = new System.Text.StringBuilder();
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var trimmed = line.TrimStart(' ', '\t');
                var indentLen = line.Length - trimmed.Length;
                var nbspCount = 0;
                for (var j = 0; j < indentLen; j++)
                    nbspCount += line[j] == '\t' ? 2 : 1;
                indentBuilder.Append(new string('\u00A0', nbspCount));
                indentBuilder.Append(trimmed);
                if (i < lines.Length - 1)
                    indentBuilder.Append('\n');
            }

            return _csharpTokenRegex.Replace(indentBuilder.ToString(), match =>
            {
                if (match.Groups[1].Success || match.Groups[2].Success)
                    return $"<color=#6A9955>{match.Value}</color>";
                if (match.Groups[3].Success || match.Groups[4].Success || match.Groups[5].Success)
                    return $"<color=#CE9178>{match.Value}</color>";
                if (match.Groups[6].Success)
                    return $"<color=#569CD6>{match.Value}</color>";
                if (match.Groups[7].Success)
                    return $"<color=#DCDCAA>{match.Value}</color>";
                if (match.Groups[8].Success)
                    return $"<color=#4EC9B0>{match.Value}</color>";
                if (match.Groups[9].Success)
                    return $"<color=#DCDCAA>{match.Value}</color>";
                if (match.Groups[10].Success)
                    return $"<color=#4EC9B0>{match.Value}</color>";
                if (match.Groups[11].Success)
                    return $"<color=#B5CEA8>{match.Value}</color>";
                return match.Value;
            });
        }

        // ── Rich XML renderer ─────────────────────────────────────────────────

        private VisualElement RenderXmlElement(XElement xml)
        {
            var hasInlineRefs = xml.Descendants()
                .Any(e => e.Name.LocalName is "see" or "seealso");

            if (!hasInlineRefs)
                return CreateTextLabel(XmlDocParser.GetInnerTextStatic(xml));

            var flow = new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .SetFlexWrap(Wrap.Wrap)
                .SetAlignItems(Align.Center)
                .SetMargin(bottom: 4);

            RenderNodesInto(flow, xml);
            return flow;
        }

        private void RenderNodesInto(VisualElement container, XElement parent)
        {
            foreach (var node in parent.Nodes())
            {
                switch (node)
                {
                    case XText textNode:
                    {
                        var text = _whitespaceRun.Replace(textNode.Value, " ");
                        if (!string.IsNullOrEmpty(text))
                            container.Add(CreateInlineTextLabel(text));
                        break;
                    }
                    case XElement child:
                    {
                        var tag = child.Name.LocalName.ToLowerInvariant();
                        if (tag is "see" or "seealso")
                            container.Add(CreateCrefChip(child));
                        else if (tag is "code")
                            container.Add(CreateCodeBlock(child.Value));
                        else
                            RenderNodesInto(container, child);
                        break;
                    }
                }
            }
        }

        private static Label CreateInlineTextLabel(string text)
        {
            var label = new Label(text);
            label.AddToClassList("doc-text");
            label.style.marginTop = 0;
            label.style.marginBottom = 0;
            return label;
        }

        private Label CreateCrefChip(XElement element)
        {
            var cref = element.Attribute("cref")?.Value ?? string.Empty;
            var displayText = string.IsNullOrEmpty(element.Value.Trim())
                ? XmlDocParser.CrefToDisplayName(cref)
                : element.Value.Trim();

            var chip = new Label(displayText);
            chip.AddToClassList("doc-cref-chip");
            chip.tooltip = cref;

            if (!string.IsNullOrEmpty(cref))
                chip.RegisterCallback<ClickEvent>(_ => ShowCrefMenu(cref));

            return chip;
        }

        // ── Member card ───────────────────────────────────────────────────────

        private VisualElement CreateMemberCard(MemberDocumentation doc)
        {
            var card = new VisualElement();
            card.AddToClassList("doc-member-card");

            var nameRow = new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .SetAlignItems(Align.Center)
                .SetMargin(bottom: 6);

            var nameLabel = new Label(doc.Name);
            nameLabel.AddToClassList("doc-member-name");
            nameRow.Add(nameLabel);

            if (doc.InheritsDoc)
            {
                var badge = new Label("inheritdoc");
                badge.AddToClassList("doc-badge-inherited");
                nameRow.Add(badge);
            }

            if (!string.IsNullOrEmpty(doc.InheritedFrom))
            {
                var fromLabel = new Label($"from {doc.InheritedFrom}");
                fromLabel.AddToClassList("doc-inherited-from");
                nameRow.Add(fromLabel);
            }

            card.Add(nameRow);

            if (doc.SummaryXml != null)
                card.Add(RenderXmlElement(doc.SummaryXml));
            else if (!string.IsNullOrWhiteSpace(doc.Summary))
                card.Add(CreateTextLabel(doc.Summary!));

            if (doc.RemarksXml != null)
            {
                var remarks = RenderXmlElement(doc.RemarksXml);
                remarks.style.marginTop = 4;
                card.Add(remarks);
            }
            else if (!string.IsNullOrWhiteSpace(doc.Remarks))
                card.Add(CreateTextLabel(doc.Remarks!));

            if (doc.Parameters.Count > 0 || doc.TypeParameters.Count > 0)
            {
                var paramsContainer = new VisualElement().SetMargin(top: 6);
                foreach (var (paramName, paramText) in doc.Parameters)
                    paramsContainer.Add(CreateParamRow(paramName, paramText));
                foreach (var (typeName, typeText) in doc.TypeParameters)
                    paramsContainer.Add(CreateParamRow($"<{typeName}>", typeText));
                card.Add(paramsContainer);
            }

            if (!string.IsNullOrWhiteSpace(doc.Returns))
                card.Add(CreateReturnsRow(doc.Returns!));

            if (doc.SeeAlso.Count > 0)
            {
                var seeAlsoRow = new VisualElement()
                    .SetFlexDirection(FlexDirection.Row)
                    .SetFlexWrap(Wrap.Wrap)
                    .SetMargin(top: 6);

                foreach (var see in doc.SeeAlso)
                {
                    if (string.IsNullOrEmpty(see.Cref)) continue;
                    var chip = new Label(XmlDocParser.CrefToDisplayName(see.Cref));
                    chip.AddToClassList("doc-seealso-chip");
                    chip.tooltip = see.Cref;
                    seeAlsoRow.Add(chip);
                }

                card.Add(seeAlsoRow);
            }

            return card;
        }

        private static VisualElement CreateParamRow(string name, string description)
        {
            var row = new VisualElement();
            row.AddToClassList("doc-param-row");

            var nameLabel = new Label(name);
            nameLabel.AddToClassList("doc-param-name");

            var descLabel = new Label(description);
            descLabel.AddToClassList("doc-param-desc");

            row.Add(nameLabel);
            row.Add(descLabel);
            return row;
        }

        private static VisualElement CreateReturnsRow(string description)
        {
            var row = new VisualElement();
            row.AddToClassList("doc-param-row");

            var label = new Label("Returns");
            label.AddToClassList("doc-returns-label");

            var desc = new Label(description);
            desc.AddToClassList("doc-param-desc");

            row.Add(label);
            row.Add(desc);
            return row;
        }
    }
}
