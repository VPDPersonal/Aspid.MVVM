using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// UI Toolkit window for selecting ViewModel types from a hierarchical, searchable list.
    /// </summary>
    public sealed class ViewModelPickerWindow : EditorWindow
    {
        private const string NoneOption = "<None>";
        private const string GlobalNamespace = "<Global>";
        
        private Label _titleLabel;
        private Button _backButton;
        private ListView _listView;
        private ToolbarSearchField _searchField;
        
        private string _currentAqn = string.Empty;
        private Action<string, string> _onSelected;
        
        private readonly NavigationController _navigation = new();
        
        public static void Show(Rect screenRect, string currentAqn, Action<string, string> onSelected)
        {
            var window = CreateInstance<ViewModelPickerWindow>();
            window.Initialize(screenRect, currentAqn, onSelected);
        }

        #region Initialization
        private void Initialize(Rect screenRect, string currentAqn, Action<string, string> onSelected)
        {
            _onSelected = onSelected;
            _currentAqn = currentAqn ?? string.Empty;

            BuildUI();
            BuildDataModel();
            NavigateToCurrentSelection();
            
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
                var items = _navigation.GetCurrentItems();
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

        private void BuildDataModel()
        {
            var viewModels = TypeInfoScanner.GetAllTypeInfos();
            var hierarchy = HierarchyBuilder.Build(viewModels);
            _navigation.Initialize(hierarchy);
            RefreshView();
        }

        private void NavigateToCurrentSelection()
        {
            if (!string.IsNullOrEmpty(_currentAqn))
            {
                _navigation.NavigateToAqn(_currentAqn);
                RefreshView();
            }
        }
        #endregion
        
        #region Event Handlers

        private void HandleKeyDown(KeyDownEvent evt)
        {
            switch (evt.keyCode)
            {
                case KeyCode.UpArrow:
                    if (_listView.selectedIndex == 0)
                        _searchField.Focus();
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
            if (_navigation.IsSearching && !string.IsNullOrEmpty(_searchField.value))
                _searchField.value = string.Empty;
            else
                Close();
        }

        private void HandleRightArrow()
        {
            var items = _navigation.GetCurrentItems();
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
            if (node != null)
                ActivateNode(node);
        }
        #endregion

        #region Navigation

        private void ActivateNode(TreeNode node)
        {
            if (node.HasChildren && !_navigation.IsSearching)
                NavigateInto(node);
            else if (node.IsSelectable)
                SelectNode(node);
        }

        private void NavigateInto(TreeNode node)
        {
            _navigation.NavigateInto(node);
            RefreshView();
            _listView.selectedIndex = 0;
        }

        private void NavigateBack()
        {
            if (_navigation.CanNavigateBack())
            {
                _navigation.NavigateBack();
                RefreshView();
            }
        }

        private void SelectNode(TreeNode node)
        {
            var displayText = node.AssemblyQualifiedName == null ? NoneOption : node.Caption;
            _onSelected?.Invoke(node.AssemblyQualifiedName, displayText);
            Close();
        }

        #endregion

        #region View Updates

        private void RefreshView()
        {
            _titleLabel.text = _navigation.GetCurrentTitle();
            _backButton.SetEnabled(_navigation.CanNavigateBack());
            
            _listView.itemsSource = _navigation.GetCurrentItems();
            _listView.Rebuild();
        }

        #endregion

        #region Navigation Controller

        private class NavigationController
        {
            private TreeNode _rootNode;
            private TreeNode _currentNode;
            private readonly List<TreeNode> _breadcrumbs = new();
            private readonly List<TreeNode> _searchResults = new();

            public bool IsSearching { get; private set; }

            public void Initialize(TreeNode root)
            {
                _rootNode = root;
                _currentNode = root;
                _breadcrumbs.Clear();
            }

            public void ApplySearch(string query)
            {
                IsSearching = !string.IsNullOrEmpty(query);
                
                if (IsSearching)
                {
                    _searchResults.Clear();
                    var filter = query.Trim().ToLowerInvariant();
                    
                    foreach (var leaf in EnumerateLeaves(_rootNode))
                    {
                        if (leaf.MatchesFilter(filter))
                            _searchResults.Add(leaf.CreateSearchResult());
                    }
                }
            }

            public List<TreeNode> GetCurrentItems()
            {
                return IsSearching ? _searchResults : _currentNode.Children;
            }

            public string GetCurrentTitle()
            {
                if (IsSearching) return "Search";
                if (_breadcrumbs.Count == 0) return "Select ViewModel";

                var parts = _breadcrumbs
                    .Select(n => n.DisplayName)
                    .Append(_currentNode.DisplayName)
                    .Where(n => n != "/")
                    .ToList();

                return string.Join("/", parts);
            }

            public void NavigateInto(TreeNode node)
            {
                _breadcrumbs.Add(_currentNode);
                _currentNode = node;
            }

            public void NavigateBack()
            {
                if (_breadcrumbs.Count > 0)
                {
                    _currentNode = _breadcrumbs[^1];
                    _breadcrumbs.RemoveAt(_breadcrumbs.Count - 1);
                }
            }

            public bool CanNavigateBack() => _breadcrumbs.Count > 0;

            public void NavigateToAqn(string aqn)
            {
                var path = new List<TreeNode>();
                if (FindPathToAqn(_rootNode, aqn, path) && path.Count > 1)
                {
                    // Navigate to parent of the target
                    for (int i = 1; i < path.Count - 1; i++)
                    {
                        _breadcrumbs.Add(_currentNode);
                        _currentNode = path[i];
                    }
                }
            }

            private static bool FindPathToAqn(TreeNode node, string aqn, List<TreeNode> path)
            {
                path.Add(node);
                
                if (node.AssemblyQualifiedName == aqn)
                    return true;

                foreach (var child in node.Children)
                {
                    if (FindPathToAqn(child, aqn, path))
                        return true;
                }

                path.RemoveAt(path.Count - 1);
                return false;
            }

            private static IEnumerable<TreeNode> EnumerateLeaves(TreeNode node)
            {
                if (!node.HasChildren && node.AssemblyQualifiedName != null)
                {
                    yield return node;
                }
                else
                {
                    foreach (var child in node.Children)
                    {
                        foreach (var leaf in EnumerateLeaves(child))
                            yield return leaf;
                    }
                }
            }
        }

        #endregion

        #region Data Model

        private class TreeNode
        {
            public string DisplayName { get; set; }
            public string Caption { get; set; }
            public string AssemblyQualifiedName { get; set; }
            public string Tooltip { get; set; }
            public List<TreeNode> Children { get; }

            public bool HasChildren => Children.Count > 0;
            public bool IsSelectable => AssemblyQualifiedName != null || DisplayName == NoneOption;

            public TreeNode(string displayName, string aqn = null, string caption = null)
            {
                DisplayName = displayName;
                AssemblyQualifiedName = aqn;
                Caption = caption ?? displayName;
                Tooltip = string.Empty;
                Children = new List<TreeNode>();
            }

            public bool MatchesFilter(string filter)
            {
                if (string.IsNullOrEmpty(filter)) return true;
                
                return DisplayName?.ToLowerInvariant().Contains(filter) == true
                    || Caption?.ToLowerInvariant().Contains(filter) == true
                    || AssemblyQualifiedName?.ToLowerInvariant().Contains(filter) == true;
            }

            public TreeNode CreateSearchResult()
            {
                return new TreeNode(Caption, AssemblyQualifiedName, Caption)
                {
                    Tooltip = this.Tooltip
                };
            }
        }

        private class TypeInfo
        {
            public readonly string Name;
            public readonly string Assembly;
            public readonly string Namespace;
            public readonly string AssemblyQualifiedName;
            
            public string FullName => string.IsNullOrWhiteSpace(Namespace) 
                ? Name 
                : $"{Namespace}.{Name}";

            public TypeInfo(Type type)
            {
                Name = type.Name;
                Assembly = type.Assembly.GetName().Name;
                AssemblyQualifiedName = type.AssemblyQualifiedName;
                Namespace = string.IsNullOrEmpty(type.Namespace) ? GlobalNamespace : type.Namespace;
            }
        }

        #endregion

        #region Hierarchy Builder

        private static class HierarchyBuilder
        {
            public static TreeNode Build(List<TypeInfo> viewModels)
            {
                var root = new TreeNode("/");
                root.Children.Add(new TreeNode(NoneOption, null, NoneOption));

                AddGlobalNamespaceGroup(root, viewModels);
                AddNamespaceHierarchy(root, viewModels);

                return root;
            }

            private static void AddGlobalNamespaceGroup(TreeNode root, List<TypeInfo> viewModels)
            {
                var globals = viewModels
                    .Where(vm => vm.Namespace == GlobalNamespace)
                    .OrderBy(vm => vm.Name)
                    .ToList();

                if (globals.Count == 0) return;

                var globalGroup = new TreeNode(GlobalNamespace);
                AddViewModelsWithDisambiguation(globalGroup, globals, includeNamespace: false);
                root.Children.Add(globalGroup);
            }

            private static void AddNamespaceHierarchy(TreeNode root, List<TypeInfo> viewModels)
            {
                var namespacedVMs = viewModels
                    .Where(vm => vm.Namespace != GlobalNamespace)
                    .ToList();

                var trie = BuildNamespaceTrie(namespacedVMs);
                CompressNamespaceTrie(trie);

                var nsToVMs = namespacedVMs
                    .GroupBy(vm => vm.Namespace)
                    .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var child in trie.Children.Values.OrderBy(n => n.Segment))
                {
                    var node = BuildNamespaceNode(child, "", "", nsToVMs);
                    root.Children.Add(node);
                }
            }

            private static NamespaceTrieNode BuildNamespaceTrie(List<TypeInfo> viewModels)
            {
                var root = new NamespaceTrieNode("");

                foreach (var vm in viewModels)
                {
                    var current = root;
                    foreach (var segment in vm.Namespace.Split('.'))
                    {
                        current = current.GetOrCreateChild(segment);
                    }
                    current.IsTerminal = true;
                }

                return root;
            }

            private static TreeNode BuildNamespaceNode(
                NamespaceTrieNode trieNode,
                string displayPrefix,
                string fullNamespace,
                Dictionary<string, List<TypeInfo>> nsToVMs)
            {
                var nextDisplay = string.IsNullOrEmpty(displayPrefix) 
                    ? trieNode.Segment 
                    : $"{displayPrefix}.{trieNode.Segment}";
                
                var nextNamespace = string.IsNullOrEmpty(fullNamespace)
                    ? trieNode.Segment
                    : $"{fullNamespace}.{trieNode.Segment}";

                var node = new TreeNode(trieNode.Segment, null, nextDisplay);

                // Add ViewModels at this namespace level
                if (trieNode.IsTerminal && nsToVMs.TryGetValue(nextNamespace, out var vms))
                {
                    AddViewModelsWithDisambiguation(node, vms, includeNamespace: true, nextNamespace);
                }

                // Add child namespaces
                foreach (var child in trieNode.Children.Values.OrderBy(n => n.Segment))
                {
                    node.Children.Add(BuildNamespaceNode(child, nextDisplay, nextNamespace, nsToVMs));
                }

                // Flatten single-child chains
                return FlattenSingleChildChain(node);
            }

            private static TreeNode FlattenSingleChildChain(TreeNode node)
            {
                if (node.Children.Count != 1) return node;

                var onlyChild = node.Children[0];

                // Merge namespace nodes
                if (onlyChild.AssemblyQualifiedName == null)
                {
                    node.DisplayName = $"{node.DisplayName}.{onlyChild.DisplayName}";
                    node.Caption = $"{node.Caption}.{onlyChild.Caption}";
                    node.Children.Clear();
                    node.Children.AddRange(onlyChild.Children);
                }
                // Merge namespace with single ViewModel
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

            private static void AddViewModelsWithDisambiguation(
                TreeNode parent,
                List<TypeInfo> viewModels,
                bool includeNamespace,
                string namespacePath = "")
            {
                var nameCounts = viewModels
                    .GroupBy(vm => vm.Name)
                    .ToDictionary(g => g.Key, g => g.Count());

                foreach (var vm in viewModels.OrderBy(vm => vm.Name))
                {
                    var needsAssembly = nameCounts[vm.Name] > 1;
                    var displayName = needsAssembly ? $"{vm.Name} ({vm.Assembly})" : vm.Name;
                    
                    var caption = includeNamespace
                        ? $"{namespacePath}.{displayName}"
                        : displayName;

                    var leaf = new TreeNode(displayName, vm.AssemblyQualifiedName, caption)
                    {
                        Tooltip = vm.FullName
                    };
                    
                    parent.Children.Add(leaf);
                }
            }

            private static void CompressNamespaceTrie(NamespaceTrieNode node)
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

        private class NamespaceTrieNode
        {
            public string Segment { get; set; }
            public bool IsTerminal { get; set; }
            public Dictionary<string, NamespaceTrieNode> Children { get; }

            public NamespaceTrieNode(string segment)
            {
                Segment = segment;
                Children = new Dictionary<string, NamespaceTrieNode>(StringComparer.Ordinal);
            }

            public NamespaceTrieNode GetOrCreateChild(string segment)
            {
                if (!Children.TryGetValue(segment, out var child))
                {
                    child = new NamespaceTrieNode(segment);
                    Children[segment] = child;
                }
                return child;
            }
        }

        #endregion
        
        private static class TypeInfoScanner
        {
            public static List<TypeInfo> GetAllTypeInfos()
            {
                // TODO Aspid.UnityFastTools – Get base type from attribute.
                var baseType = typeof(IViewModel);
                var result = new List<TypeInfo>();

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    try
                    {
                        result.AddRange( assembly.GetTypes()
                            .Where(type => baseType.IsAssignableFrom(type))
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
    }
}