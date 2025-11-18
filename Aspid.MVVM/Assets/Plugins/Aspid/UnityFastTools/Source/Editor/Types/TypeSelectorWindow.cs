using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    // TODO Aspid.UnityFastTools - Refactor
    // TODO Aspid.UnityFastTools - Write summary
    public sealed class TypeSelectorWindow : EditorWindow
    {
        private const string NoneOption = "<None>";
        private const string GlobalNamespace = "<Global>";
        
        private Label _titleLabel;
        private Button _backButton;
        private ListView _listView;
        private ToolbarSearchField _searchField;
        private NavigationController _navigation;
        
        private Action<string> _onSelected;
        private string _currentAqn = string.Empty;
        
        public static void Show(Type type, Rect screenRect, string currentAqn, Action<string> onSelected)
        {
            var window = CreateInstance<TypeSelectorWindow>();
            window.Initialize(type, screenRect, currentAqn, onSelected);
        }

        #region Initialization
        private void Initialize(Type type, Rect screenRect, string currentAqn, Action<string> onSelected)
        {
            _onSelected = onSelected;
            _currentAqn = currentAqn ?? string.Empty;

            BuildUI();
            
            var hierarchy = HierarchyBuilder.Build(type);
            InitializeNavigation(hierarchy, _currentAqn);
            
            RefreshView();
            
            var size = new Vector2(Mathf.Max(350, screenRect.width), 320);
            ShowAsDropDown(screenRect, size);
            
            _searchField.Focus();
        }

        private void BuildUI()
        {
            rootVisualElement.Add(CreateWindowLayout());
            return;

            VisualElement CreateWindowLayout()
            {
                var header = CreateHeader();
                _searchField = CreateSearchField();
                _listView = CreateListView();

                var container = new VisualElement()
                    .SetFlexGrow(1)
                    .SetFlexDirection(FlexDirection.Column)
                    .SetPadding(top: 4, bottom: 4, left: 4, right: 4)
                    .AddChild(header)
                    .AddChild(_searchField)
                    .AddChild(_listView);

                var root = new VisualElement().AddChild(container);
                root.RegisterCallback<KeyDownEvent>(HandleKeyDown, TrickleDown.TrickleDown);
                
                return root;
            }

            VisualElement CreateHeader()
            {
                _backButton = new Button(NavigateBack)
                    .SetText("←")
                    .SetMargin(right: 4)
                    .SetSize(width: 26, height: 20)
                    .SetBackgroundColor(Color.clear)
                    .SetBorderWidth(top: 0, bottom: 0, left: 0, right: 0);

                _titleLabel = new Label("Select ViewModel")
                    .SetFlexGrow(1)
                    .SetUnityFontStyleAndWeight(FontStyle.Bold);

                return new VisualElement()
                    .SetSize(height: 20)
                    .SetMinSize(height: 20)
                    .SetMargin(top: -3, left: -3, right: -3)
                    .SetBackgroundColor(new Color(0.149f, 0.149f, 0.149f))
                    .SetAlignItems(Align.Center)
                    .SetFlexDirection(FlexDirection.Row)
                    .SetMargin(bottom: 4)
                    .AddChild(_backButton)
                    .AddChild(_titleLabel);
            }

            ToolbarSearchField CreateSearchField()
            {
                var field = new ToolbarSearchField()
                    .SetMargin(bottom: 4)
                    .SetPadding(right: 4)
                    .SetSize(width: Length.Auto());

                field.RegisterValueChangedCallback(e => HandleSearchChanged(e.newValue ?? string.Empty));
                field.RegisterCallback<NavigationMoveEvent>(e =>
                {
                    if (e.move == Vector2.down)
                        _listView?.Focus();
                }, TrickleDown.TrickleDown);

                return field;
            }

            ListView CreateListView()
            {
                var list = new ListView
                {
                    selectionType = SelectionType.Single,
                    virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                };

                list.SetMakeItem(CreateListItem);
                list.SetBindItem(BindListItem);
                list.itemsChosen += HandleItemChosen;

                return list;
            }

            VisualElement CreateListItem()
            {
                var label = new Label()
                    .SetName("title")
                    .SetFlexGrow(1);

                var arrow = new Label("›")
                    .SetName("arrow");
                arrow.style.opacity = 0.6f;

                return new VisualElement()
                    .AddChild(label)
                    .AddChild(arrow)
                    .SetSize(height: 20)
                    .SetAlignItems(Align.Center)
                    .SetPadding(left: 6, right: 6)
                    .SetFlexDirection(FlexDirection.Row);
            }

            void BindListItem(VisualElement element, int index)
            {
                var items = _navigation?.CurrentItems;
                
                if (items is null) return;
                if (index < 0 || index >= items.Count) return;

                var node = items[index];
                element.Q<Label>("title")
                    .SetText(node.DisplayName)
                    .SetTooltip(node.Tooltip);

                element.Q<Label>("arrow")
                    .SetDisplay(node.HasChildren && !_navigation.IsSearching
                        ? DisplayStyle.Flex
                        : DisplayStyle.None);
            }
        }
        
        private void InitializeNavigation(TreeNode hierarchy, string currentAqn)
        {
            _navigation = new NavigationController(hierarchy);
            
            if (!string.IsNullOrWhiteSpace(currentAqn))
                _navigation.NavigateToAssemblyQualifiedName(currentAqn);
        }
        #endregion
        
        #region KeyDown Hadnlers
        private void HandleKeyDown(KeyDownEvent evt)
        {
            switch (evt.keyCode)
            {
                case KeyCode.UpArrow:
                    if (_listView.selectedIndex is 0)
                        _searchField.Focus();
                    
                    evt.StopPropagation();
                    break;

                case KeyCode.Escape:
                    HandleEscapeKey();
                    evt.StopPropagation();
                    break;

                case KeyCode.RightArrow:
                    HandleRightArrow();
                    evt.StopPropagation();
                    break;

                case KeyCode.LeftArrow:
                    if (_searchField.focusController.focusedElement != _searchField)
                    {
                        NavigateBack();
                        evt.StopPropagation();
                    }
                    break;
            }
        }

        private void HandleEscapeKey()
        {
            if (_navigation.IsSearching && !string.IsNullOrWhiteSpace(_searchField.value))
                _searchField.value = string.Empty;
            else Close();
        }

        private void HandleRightArrow()
        {
            var items = _navigation.CurrentItems;
            var index = _listView.selectedIndex;
            
            if (index >= 0 && index < items.Count && items[index].HasChildren)
                NavigateInto(items[index]);
        }

        private void HandleSearchChanged(string query)
        {
            _navigation.ApplySearch(query);
            RefreshView();
        }

        private void HandleItemChosen(IEnumerable<object> items)
        {
            var node = items.OfType<TreeNode>().FirstOrDefault();
            
            if (node is not null) 
                ActivateNode(node);
        }
        #endregion

        #region Navigation

        private void ActivateNode(TreeNode node)
        {
            if (node.HasChildren && !_navigation.IsSearching) NavigateInto(node);
            else if (node.IsSelectable) SelectNode(node);
        }

        private void NavigateInto(TreeNode node)
        {
            _navigation.NavigateInto(node);
            RefreshView();
            
            _listView.selectedIndex = 0;
        }

        private void NavigateBack()
        {
            if (_navigation.CanNavigateBack)
            {
                _navigation.NavigateBack();
                RefreshView();
            }
        }

        private void SelectNode(TreeNode node)
        {
            _onSelected?.Invoke(node.AssemblyQualifiedName);
            Close();
        }
        #endregion
        
        private void RefreshView()
        {
            _titleLabel.text = _navigation.GetCurrentTitle();
            _backButton.SetEnabled(_navigation.CanNavigateBack);
            
            _listView.itemsSource = _navigation.CurrentItems;
            _listView.Rebuild();
        }

        #region Hierarchy Builder
        private static class HierarchyBuilder
        {
            public static TreeNode Build(Type type)
            {
                // TODO Aspid.UnityFastTools – Get base type from attribute.
                var types = TypeInfoScanner.GetAllTypeInfos(type);
                
                var root = new TreeNode("/");
                root.Children.Add(new TreeNode(NoneOption, null, NoneOption));

                AddGlobalNamespaceGroup(root, types);
                AddNamespaceHierarchy(root, types);

                return root;
            }

            private static void AddGlobalNamespaceGroup(TreeNode root, List<TypeInfo> types)
            {
                var globals = types
                    .Where(type => type.Namespace == GlobalNamespace)
                    .OrderBy(type => type.Name)
                    .ToList();

                if (globals.Count is 0) return;
                var globalGroup = new TreeNode(GlobalNamespace);
                
                AddTypesWithDisambiguation(globalGroup, globals, includeNamespace: false);
                root.Children.Add(globalGroup);
            }

            private static void AddNamespaceHierarchy(TreeNode root, List<TypeInfo> types)
            {
                var namespacedTypes = types
                    .Where(type => type.Namespace != GlobalNamespace)
                    .ToList();

                var trie = BuildNamespaceTrie(namespacedTypes);
                CompressNamespaceTrie(trie);

                var nsToTypes = namespacedTypes
                    .GroupBy(type => type.Namespace)
                    .ToDictionary(group => group.Key, group => group.ToList());

                foreach (var child in trie.Children.Values.OrderBy(n => n.Segment))
                {
                    var node = BuildNamespaceNode(child, string.Empty, string.Empty, nsToTypes);
                    root.Children.Add(node);
                }
            }

            private static NamespaceNode BuildNamespaceTrie(List<TypeInfo> types)
            {
                var root = new NamespaceNode(string.Empty);

                foreach (var type in types)
                {
                    var current = root;
                    
                    foreach (var segment in type.Namespace.Split('.'))
                    {
                        current = current.GetOrCreateChild(segment);
                    }
                    
                    current.IsTerminal = true;
                }

                return root;
            }

            private static TreeNode BuildNamespaceNode(
                NamespaceNode trieNode,
                string displayPrefix,
                string fullNamespace,
                Dictionary<string, List<TypeInfo>> nsToTypes)
            {
                var nextDisplay = string.IsNullOrEmpty(displayPrefix) 
                    ? trieNode.Segment 
                    : $"{displayPrefix}.{trieNode.Segment}";
                
                var nextNamespace = string.IsNullOrEmpty(fullNamespace)
                    ? trieNode.Segment
                    : $"{fullNamespace}.{trieNode.Segment}";

                var node = new TreeNode(trieNode.Segment, null, nextDisplay);

                // Add ViewModels at this namespace level
                if (trieNode.IsTerminal && nsToTypes.TryGetValue(nextNamespace, out var types))
                {
                    AddTypesWithDisambiguation(node, types, includeNamespace: true, nextNamespace);
                }

                // Add child namespaces
                foreach (var child in trieNode.Children.Values.OrderBy(n => n.Segment))
                {
                    node.Children.Add(BuildNamespaceNode(child, nextDisplay, nextNamespace, nsToTypes));
                }

                // Flatten single-child chains
                return FlattenSingleChildChain(node);
            }

            private static TreeNode FlattenSingleChildChain(TreeNode node)
            {
                if (node.Children.Count != 1) return node;

                var onlyChild = node.Children[0];
                
                if (onlyChild.AssemblyQualifiedName == null)
                {
                    node.DisplayName = $"{node.DisplayName}.{onlyChild.DisplayName}";
                    node.Caption = $"{node.Caption}.{onlyChild.Caption}";
                    node.Children.Clear();
                    node.Children.AddRange(onlyChild.Children);
                }
                else
                {
                    node.DisplayName = $"{node.DisplayName}.{onlyChild.DisplayName}";
                    node.AssemblyQualifiedName = onlyChild.AssemblyQualifiedName;
                    node.Caption = onlyChild.Caption;
                    node.Tooltip = onlyChild.Tooltip;
                    node.Children.Clear();
                }

                return node;
            }

            private static void AddTypesWithDisambiguation(
                TreeNode parent,
                List<TypeInfo> types,
                bool includeNamespace,
                string namespacePath = "")
            {
                var nameCounts = types
                    .GroupBy(type => type.Name)
                    .ToDictionary(g => g.Key, g => g.Count());

                foreach (var type in types.OrderBy(type => type.Name))
                {
                    var needsAssembly = nameCounts[type.Name] > 1;
                    var displayName = needsAssembly ? $"{type.Name} ({type.Assembly})" : type.Name;
                    
                    var caption = includeNamespace
                        ? $"{namespacePath}.{displayName}"
                        : displayName;

                    var leaf = new TreeNode(displayName, type.AssemblyQualifiedName, caption)
                    {
                        Tooltip = type.FullName
                    };
                    
                    parent.Children.Add(leaf);
                }
            }

            private static void CompressNamespaceTrie(NamespaceNode node)
            {
                // Recursively compress children first
                foreach (var child in node.Children.Values.ToList())
                {
                    CompressNamespaceTrie(child);
                }

                // Compress chains at this level
                foreach (var key in node.Children.Keys.ToList())
                {
                    if (!node.Children.TryGetValue(key, out var child)) continue;

                    // Merge non-terminal nodes with single child
                    while (!child.IsTerminal && child.Children.Count == 1)
                    {
                        var grandchild = child.Children.Values.First();
                        child.Segment = $"{child.Segment}.{grandchild.Segment}";
                        child.IsTerminal = grandchild.IsTerminal;
                        child.Children.Clear();
                        
                        foreach (var kv in grandchild.Children)
                            child.Children[kv.Key] = kv.Value;
                    }

                    // Update dictionary if segment changed
                    if (child.Segment != key)
                    {
                        node.Children.Remove(key);
                        node.Children[child.Segment] = child;
                    }
                }
            }
        }

        private class NamespaceNode
        {
            public string Segment { get; set; }
            
            public bool IsTerminal { get; set; }
            
            public Dictionary<string, NamespaceNode> Children { get; }

            public NamespaceNode(string segment)
            {
                Segment = segment;
                Children = new Dictionary<string, NamespaceNode>(StringComparer.Ordinal);
            }

            public NamespaceNode GetOrCreateChild(string segment)
            {
                if (!Children.TryGetValue(segment, out var child))
                {
                    child = new NamespaceNode(segment);
                    Children[segment] = child;
                }
                
                return child;
            }
        }
        #endregion

        #region TypeInfo
        private sealed class TypeInfo
        {
            public readonly string Name;
            public readonly string Assembly;
            public readonly string FullName;
            public readonly string Namespace;
            public readonly string AssemblyQualifiedName;
            
            public TypeInfo(Type type)
            {
                Name = type.Name;
                FullName = type.FullName;
                Assembly = type.Assembly.GetName().Name;
                AssemblyQualifiedName = type.AssemblyQualifiedName;
                Namespace = string.IsNullOrEmpty(type.Namespace) ? GlobalNamespace : type.Namespace;
            }
        }
        
        private static class TypeInfoScanner
        {
            public static List<TypeInfo> GetAllTypeInfos(Type baseType)
            {
                var result = new List<TypeInfo>();

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    try
                    {
                        result.AddRange(assembly.GetTypes()
                            .Where(t => baseType.IsAssignableFrom(t) && 
                                !t.IsDefined(typeof(CompilerGeneratedAttribute), false) &&
                                !t.Name.Contains("<") &&
                                !t.Name.Contains(">"))
                            .Select(type => new TypeInfo(type)));
                    }
                    catch
                    {
                        // ignored
                    }
                }

                return result;
            }
        }
        #endregion
        
        private class TreeNode
        {
            public string Caption { get; set; }
            
            public string Tooltip { get; set; }
            
            public List<TreeNode> Children { get; }
            
            public string DisplayName { get; set; }
            
            public string AssemblyQualifiedName { get; set; }

            public bool HasChildren => Children.Count > 0;
            
            public bool IsSelectable => AssemblyQualifiedName is not null || DisplayName == NoneOption;

            public TreeNode(string displayName, string assemblyQualifiedName = null, string caption = null)
            {
                DisplayName = displayName;
                AssemblyQualifiedName = assemblyQualifiedName;
                Caption = caption ?? displayName;
                Tooltip = string.Empty;
                Children = new List<TreeNode>();
            }

            public bool MatchesFilter(string filter)
            {
                if (string.IsNullOrWhiteSpace(filter))
                    return true;

                if (DisplayName?.ToLowerInvariant().Contains(filter) == true)
                    return true;
                
                if (Caption?.ToLowerInvariant().Contains(filter) == true)
                    return true;
                
                if (AssemblyQualifiedName?.ToLowerInvariant().Contains(filter) == true)
                    return true;

                return false;
            }
        }
        
        private sealed class NavigationController
        {
            private TreeNode _currentNode;
            private readonly TreeNode _rootNode;
            
            private readonly List<TreeNode> _breadcrumbs = new();
            private readonly List<TreeNode> _searchResults = new();

            public bool IsSearching { get; private set; }
            
            public bool CanNavigateBack =>
                _breadcrumbs.Count > 0;
            
            public List<TreeNode> CurrentItems =>
                IsSearching ? _searchResults : _currentNode.Children;

            public NavigationController(TreeNode root)
            {
                _rootNode = root;
                _currentNode = root;
                _breadcrumbs.Clear();
            }

            public string GetCurrentTitle()
            {
                if (IsSearching) return "Search";
                if (_breadcrumbs.Count is 0) return "Select Type";

                var parts = _breadcrumbs
                    .Select(node => node.DisplayName)
                    .Append(_currentNode.DisplayName)
                    .Where(node => node is not "/")
                    .ToList();

                return string.Join("/", parts);
            }
            
            public void ApplySearch(string query)
            {
                IsSearching = !string.IsNullOrWhiteSpace(query);
                
                if (IsSearching)
                {
                    _searchResults.Clear();
                    var filter = query?.Trim().ToLowerInvariant();
                    
                    foreach (var node in EnumerateLeaves(_rootNode))
                    {
                        if (node.MatchesFilter(filter))
                            _searchResults.Add(new TreeNode(
                                displayName: node.Caption, 
                                node.AssemblyQualifiedName, 
                                node.Caption));
                    }
                }
            }

            public void NavigateInto(TreeNode node)
            {
                _breadcrumbs.Add(_currentNode);
                _currentNode = node;
            }

            public void NavigateBack()
            {
                if (!CanNavigateBack) return;
                
                _currentNode = _breadcrumbs[^1];
                _breadcrumbs.RemoveAt(_breadcrumbs.Count - 1);
            }

            public void NavigateToAssemblyQualifiedName(string aqn)
            {
                var path = new List<TreeNode>();
                if (!FindPathToAssemblyQualifiedName(_rootNode, aqn, path) || path.Count < 2) return;
                
                // Navigate to parent of the target
                for (var i = 1; i < path.Count - 1; i++)
                {
                    _breadcrumbs.Add(_currentNode);
                    _currentNode = path[i];
                }
            }

            private static IEnumerable<TreeNode> EnumerateLeaves(TreeNode node)
            {
                if (!node.HasChildren && node.AssemblyQualifiedName is not null)
                {
                    yield return node;
                }
                else
                {
                    foreach (var leaf in node.Children.SelectMany(EnumerateLeaves))
                    {
                        yield return leaf;
                    }
                }
            }
            
            private static bool FindPathToAssemblyQualifiedName(TreeNode node, string assemblyQualifiedName, List<TreeNode> path)
            {
                if (node.AssemblyQualifiedName == assemblyQualifiedName
                    || node.Children.Any(child => FindPathToAssemblyQualifiedName(child, assemblyQualifiedName, path)))
                {
                    path.Add(node);
                    return true;
                }

                return false;
            }
        }
    }
}