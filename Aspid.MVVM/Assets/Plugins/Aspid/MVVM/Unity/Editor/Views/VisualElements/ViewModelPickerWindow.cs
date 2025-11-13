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
    /// Pure UI Toolkit window that shows a searchable hierarchical list of ViewModel types.
    /// Replaces IMGUI AdvancedDropdown usage.
    /// </summary>
    public sealed class ViewModelPickerWindow : EditorWindow
    {
        private const string NoneChoice = "<None>";
        private const string GlobalNamespace = "<Global>";

        private Action<string, string> _onSelected;
        private string _currentAqn = string.Empty;

        // Panel-style navigation structures
        private Node _rootNode;
        private Node _currentNode;
        private readonly List<Node> _navStack = new();
        private readonly List<Node> _searchResults = new();
        private bool _searchMode;

        private ListView _list;
        private Label _titleLabel;
        private Button _backButton;
        private ToolbarSearchField _searchField;

        public static void Show(Rect screenRect, string? currentAqn, Action<string?, string> onSelected)
        {
            var window = CreateInstance<ViewModelPickerWindow>();
            window.Constructor(screenRect, currentAqn, onSelected);
        }

        private void Constructor(Rect screenRect, string? currentAqn, Action<string?, string> onSelected)
        {
            rootVisualElement.Add(BuildWindow());
            rootVisualElement.SetBorderRadius(5, 5, 5, 5);

            BuildHierarchy();
            NavigateToInitial();
            RefreshView();
            
            _onSelected = onSelected;
            _currentAqn = currentAqn ?? string.Empty;
            var size = new Vector2(Mathf.Max(350, screenRect.width), 320);
       
            ShowAsDropDown(screenRect, size);
            _searchField.Focus();
        }

        private VisualElement BuildWindow()
        {
            _backButton = new Button(NavigateBack)
                .SetText("←")
                .SetMargin(right: 4)
                .SetSize(width: 26, height: 20)
                .SetBackgroundColor(new Color(r: 0, g: 0, b: 0, a: 0))
                .SetBorderWidth(top: 0, bottom: 0, left: 0, right: 0)
                .SetBorderRadius(topLeft: 0, topRight: 0, bottomLeft: 0, bottomRight: 0);
            
            _titleLabel = new Label("Select ViewModel")
                .SetFlexGrow(1)
                .SetUnityFontStyleAndWeight(FontStyle.Bold);
            
            _searchField = new ToolbarSearchField()
                .SetMargin(bottom: 4)
                .SetPadding(right: 4)
                .SetSize(width: Length.Auto());
            _searchField.RegisterValueChangedCallback(e => ApplyFilter(e.newValue ?? string.Empty));
            
            _searchField.RegisterCallback<NavigationMoveEvent>(e =>
            {
                if (e.move == Vector2.down)
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    _list.Focus();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
            }, TrickleDown.TrickleDown);
            
            var header = new VisualElement()
                .SetBackgroundColor(new Color(0.1490196f, 0.1490196f, 0.1490196f))
                .SetAlignItems(Align.Center)
                .SetFlexDirection(FlexDirection.Row)
                .SetMargin(bottom: 4)
                .AddChild(_backButton)
                .AddChild(_titleLabel);
            
            _list = new ListView
                {
                    selectionType = SelectionType.Single,
                    virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                }
                .SetMakeItem(() =>
                {
                    var label = new Label().SetName("title")
                        .SetFlexGrow(1);

                    var arrow = new Label("›").SetName("arrow");
                    arrow.style.opacity = 0.6f;
                
                    return new VisualElement()
                        .AddChild(label)
                        .AddChild(arrow)
                        .SetSize(height: 20)
                        .SetAlignItems(Align.Center)
                        .SetPadding(left: 6, right: 6)
                        .SetFlexDirection(FlexDirection.Row);
                })
                .SetBindItem((element, i) =>
                {
                    var items = GetCurrentItems();
                    
                    if (i < 0 || i >= items.Count) return;
                    var node = items[i];
                    
                    element.Q<Label>(name: "title")
                        .SetText(node.Item.DisplayName)
                        .SetTooltip(node.Item.Tooltip);
                    
                    element.Q<Label>(name: "arrow")
                        .SetDisplay(node.HasChildren && !_searchMode ? DisplayStyle.Flex : DisplayStyle.None);
                });
            
            _list.itemsChosen += nodes =>
            {
                foreach (var obj in nodes)
                {
                    if (obj is Node node)
                    {
                        Activate(node);
                        break;
                    }
                }
            };
            
            var container = new VisualElement()
                .SetFlexGrow(1)
                .SetFlexDirection(FlexDirection.Column)
                .SetPadding(4, 4, 4, 4)
                .AddChild(header)
                .AddChild(_searchField)
                .AddChild(_list);

            var root = new VisualElement().AddChild(container);
            root.RegisterCallback<KeyDownEvent>(OnKeyDown, TrickleDown.TrickleDown);

            return root;
        }

        private void OnKeyDown(KeyDownEvent evt)
        {
            if (evt.keyCode == KeyCode.UpArrow)
            {
                if (_list.selectedIndex is 0)
                {
                    _searchField.Focus();
                }
            }
            else if (evt.keyCode == KeyCode.Escape)
            {
                if (_searchMode && !string.IsNullOrEmpty(_searchField.value))
                {
                    _searchField.value = string.Empty;
                }
                else
                {
                    Close();
                }
                
                evt.StopPropagation();
            }
            else if (evt.keyCode is KeyCode.RightArrow)
            {
                var index = _list.selectedIndex;
                var items = GetCurrentItems();
                if (index >= 0 && index < items.Count)
                {
                    if (items[index].HasChildren)
                    {
                        Activate(items[index]);    
                    }
                    
                    evt.StopPropagation();
                }
            }
            else if (evt.keyCode == KeyCode.LeftArrow)
            {
                if (_searchField.focusController.focusedElement == _searchField) return;

                if (_navStack.Count > 0)
                {
                    NavigateBack();
                    evt.StopPropagation();
                }
            }
        }

        private void ApplyFilter(string filter)
        {
            _searchMode = !string.IsNullOrEmpty(filter);
            if (_searchMode)
            {
                BuildSearchResults(filter.Trim());
            }
            RefreshView();
        }

        private void BuildHierarchy()
        {
            // Build data source
            var models = GatherViewModels();

            // Root node
            _rootNode = new Node(new Item("/", null, "/"));

            // None leaf at root
            _rootNode.Children.Add(new Node(new Item(NoneChoice, null, NoneChoice)));

            // Global group
            var globals = models.Where(m => m.Namespace == GlobalNamespace).OrderBy(m => m.Name).ToList();
            if (globals.Count > 0)
            {
                var globalGroup = new Node(new Item(GlobalNamespace, null, GlobalNamespace));
                // disambiguate duplicates by assembly within Global
                var counts = new Dictionary<string, int>();
                foreach (var vm in globals) counts[vm.Name] = counts.TryGetValue(vm.Name, out var c) ? c + 1 : 1;
                foreach (var vm in globals.OrderBy(v => v.Name))
                {
                    var display = counts[vm.Name] > 1 ? $"{vm.Name} ({vm.Assembly})" : vm.Name;
                    var vm2 = new ViewModelInfo
                    {
                        Name = vm.Name,
                        Namespace = vm.Namespace,
                        Assembly = vm.Assembly,
                        Aqn = vm.Aqn,
                        DisplayName = display,
                        Caption = display
                    };
                    var leaf = new Node(new Item(vm2.DisplayName, vm2.Aqn, vm2.Caption)
                    {
                        Tooltip = vm2.FullName
                    });
                    globalGroup.Children.Add(leaf);
                }
                _rootNode.Children.Add(globalGroup);
            }

            // Namespace trie for others
            var rootNs = new NsNode("");
            var nsMap = new Dictionary<string, List<ViewModelInfo>>();
            foreach (var vm in models.Where(vm => vm.Namespace != GlobalNamespace))
            {
                if (!nsMap.TryGetValue(vm.Namespace, out var list))
                {
                    list = new List<ViewModelInfo>();
                    nsMap[vm.Namespace] = list;
                }
                list.Add(vm);

                var cur = rootNs;
                foreach (var seg in vm.Namespace.Split('.'))
                    cur = cur.GetOrAdd(seg);
                cur.IsTerminal = true;
            }
            CompressChains(rootNs);

            foreach (var child in rootNs.Children.Values.OrderBy(n => n.Name, StringComparer.Ordinal))
            {
                var node = BuildNamespaceNode(child, prefix: string.Empty, accum: string.Empty, nsMap);
                _rootNode.Children.Add(node);
            }

            _currentNode = _rootNode;
            _navStack.Clear();
        }

        private static Node BuildNamespaceNode(NsNode node, string prefix, string accum, Dictionary<string, List<ViewModelInfo>> nsMap)
        {
            var nextPrefix = string.IsNullOrEmpty(prefix) ? node.Name : $"{prefix}.{node.Name}";
            var nextNs = string.IsNullOrEmpty(accum) ? node.Name : $"{accum}.{node.Name}";

            // DisplayName should be just the current segment, not the full path
            var res = new Node(new Item(node.Name, null, nextPrefix));

            if (node.IsTerminal && nsMap.TryGetValue(nextNs, out var vms))
            {
                // Disambiguate duplicates by assembly
                var counts = new Dictionary<string, int>();
                foreach (var vm in vms) counts[vm.Name] = counts.TryGetValue(vm.Name, out var c) ? c + 1 : 1;
                foreach (var vm in vms.OrderBy(v => v.Name))
                {
                    var display = counts[vm.Name] > 1 ? $"{vm.Name} ({vm.Assembly})" : vm.Name;
                    var caption = $"{nextNs}.{display}";
                    var vm2 = new ViewModelInfo
                    {
                        Name = vm.Name,
                        Namespace = vm.Namespace,
                        Assembly = vm.Assembly,
                        Aqn = vm.Aqn,
                        DisplayName = display,
                        Caption = caption
                    };
                    var leaf = new Node(new Item(vm2.DisplayName, vm2.Aqn, vm2.Caption)
                    {
                        Tooltip = vm2.FullName
                    });
                    res.Children.Add(leaf);
                }
            }

            foreach (var ch in node.Children.Values.OrderBy(n => n.Name, StringComparer.Ordinal))
            {
                res.Children.Add(BuildNamespaceNode(ch, nextPrefix, nextNs, nsMap));
            }

            // If this node has only one child, merge them
            if (res.Children.Count == 1)
            {
                var onlyChild = res.Children[0];
                // Only merge if child is not a leaf (has no Aqn) OR if it's a leaf and we want to flatten single ViewModels
                if (onlyChild.Item.Aqn == null)
                {
                    // Merge namespace nodes
                    res.Item.DisplayName = $"{res.Item.DisplayName}.{onlyChild.Item.DisplayName}";
                    res.Item.Caption = $"{res.Item.Caption}.{onlyChild.Item.Caption}";
                    res.Children.Clear();
                    foreach (var grandChild in onlyChild.Children)
                    {
                        res.Children.Add(grandChild);
                    }
                }
                else
                {
                    // Merge namespace with single ViewModel leaf
                    res.Item.DisplayName = $"{res.Item.DisplayName}.{onlyChild.Item.DisplayName}";
                    res.Item.Aqn = onlyChild.Item.Aqn;
                    res.Item.Caption = onlyChild.Item.Caption;
                    res.Item.Tooltip = onlyChild.Item.Tooltip;
                    res.Children.Clear();
                }
            }

            return res;
        }

        private void BuildSearchResults(string filter)
        {
            _searchResults.Clear();
            var f = filter.ToLowerInvariant();
            foreach (var leaf in EnumerateLeaves(_rootNode))
            {
                if (leaf.Item.MatchesFilter(f))
                {
                    // Show as flattened item with caption as full path
                    var clone = new Node(new Item(leaf.Item.Caption, leaf.Item.Aqn, leaf.Item.Caption)
                    {
                        Tooltip = leaf.Item.Tooltip
                    });
                    _searchResults.Add(clone);
                }
            }
        }

        private static IEnumerable<Node> EnumerateLeaves(Node node)
        {
            if (node is { HasChildren: false, Item: { Aqn: not null } })
            {
                yield return node;
                yield break;
            }
            if (node.HasChildren)
            {
                foreach (var ch in node.Children)
                {
                    foreach (var leaf in EnumerateLeaves(ch))
                        yield return leaf;
                }
            }
        }

        private void RefreshView()
        {
            _titleLabel.text = GetCurrentTitle();
            _backButton.SetEnabled(_navStack.Count > 0);
            RefreshList();
        }

        private void RefreshList()
        {
            _list.itemsSource = GetCurrentItems();
            _list.Rebuild();
        }

        private List<Node> GetCurrentItems() => _searchMode
            ? _searchResults
            : _currentNode.Children;

        private string GetCurrentTitle()
        {
            if (_searchMode) return "Search";
            if (_navStack.Count == 0) return "Select ViewModel";
            var parts = _navStack
                .Select(n => n.Item.DisplayName)
                .Concat(new[] { _currentNode.Item.DisplayName })
                .ToList();
            // Skip root
            if (parts.Count > 0 && parts[0] == "/") parts.RemoveAt(0);
            return string.Join("/", parts);
        }

        private void Activate(Node node)
        {
            if (node.HasChildren && !_searchMode)
            {
                _navStack.Add(_currentNode);
                _currentNode = node;
                RefreshView();
                _list.selectedIndex = 0;
            }
            else if (node.Item.Aqn == null && node.Item.DisplayName == NoneChoice)
            {
                SelectItem(node.Item);
            }
            else if (node.Item.Aqn != null)
            {
                SelectItem(node.Item);
            }
        }

        private void NavigateBack()
        {
            if (_navStack.Count == 0) return;
            
            _currentNode = _navStack[^1];
            _navStack.RemoveAt(_navStack.Count - 1);
            
            RefreshView();
        }

        private void NavigateToInitial()
        {
            if (string.IsNullOrEmpty(_currentAqn)) return;
            var path = new List<Node>();
            if (FindPathToAqn(_rootNode, _currentAqn, path))
            {
                // path includes leaf at the end
                if (path.Count > 1)
                {
                    // navigate to parent
                    for (int i = 1; i < path.Count - 1; i++)
                    {
                        _navStack.Add(_currentNode);
                        _currentNode = path[i];
                    }
                }
            }
        }

        private static bool FindPathToAqn(Node node, string aqn, List<Node> path)
        {
            path.Add(node);
            if (node.Item.Aqn == aqn)
                return true;
            if (node.HasChildren)
            {
                foreach (var ch in node.Children)
                {
                    if (FindPathToAqn(ch, aqn, path)) return true;
                }
            }
            path.RemoveAt(path.Count - 1);
            return false;
        }

        private void SelectItem(Item item)
        {
            _onSelected?.Invoke(item.Aqn, item.Aqn == null ? NoneChoice : item.Caption);
            Close();
        }

        private static List<ViewModelInfo> GatherViewModels()
        {
            var iViewModelType = typeof(IViewModel);
            var list = new List<ViewModelInfo>();
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types;
                try { types = asm.GetTypes(); }
                catch { continue; }
                foreach (var t in types)
                {
                    if (!t.IsClass || t.IsAbstract || !iViewModelType.IsAssignableFrom(t)) continue;
                    var ns = string.IsNullOrEmpty(t.Namespace) ? GlobalNamespace : t.Namespace;
                    list.Add(new ViewModelInfo
                    {
                        Name = t.Name,
                        Namespace = ns,
                        Assembly = t.Assembly.GetName().Name,
                        Aqn = t.AssemblyQualifiedName
                    });
                }
            }
            return list;
        }

        private static void CompressChains(NsNode node)
        {
            // First recursively compress all children
            foreach (var key in node.Children.Keys.ToList())
                CompressChains(node.Children[key]);

            // Then compress chains at this level
            var keys = node.Children.Keys.ToList();
            foreach (var key in keys)
            {
                if (!node.Children.TryGetValue(key, out var child)) continue;
                
                // Merge chain: while child is not terminal and has exactly one child
                while (!child.IsTerminal && child.Children.Count == 1)
                {
                    var only = child.Children.Values.First();
                    // Update the child's name by appending the only grandchild's name
                    child.Name = string.IsNullOrEmpty(child.Name) ? only.Name : $"{child.Name}.{only.Name}";
                    child.IsTerminal = only.IsTerminal;
                    child.Children.Clear();
                    foreach (var kv in only.Children)
                        child.Children[kv.Key] = kv.Value;
                }
                
                // If the key changed, update the dictionary
                if (child.Name != key)
                {
                    node.Children.Remove(key);
                    node.Children[child.Name] = child;
                }
            }
        }
        
        private sealed class Item
        {
            public string DisplayName { get; set; }
            public string Caption { get; set; }
            public string Aqn { get; set; }
            public string Tooltip { get; set; }
            public Item(string displayName, string aqn, string caption)
            {
                DisplayName = displayName;
                Aqn = aqn;
                Caption = caption;
                Tooltip = string.Empty;
            }
            public bool MatchesFilter(string filter)
            {
                if (string.IsNullOrEmpty(filter)) return true;
                var f = filter.ToLowerInvariant();
                return (DisplayName?.ToLowerInvariant().Contains(f) ?? false)
                       || (Caption?.ToLowerInvariant().Contains(f) ?? false)
                       || (Aqn?.ToLowerInvariant().Contains(f) ?? false);
            }
        }

        private sealed class Node
        {
            public Item Item { get; }
            public List<Node> Children { get; }
            public bool HasChildren => Children.Count > 0;
            public Node(Item item)
            {
                Item = item;
                Children = new List<Node>();
            }
        }

        private sealed class ViewModelInfo
        {
            public string Name { get; set; }
            public string Namespace { get; set; }
            public string Assembly { get; set; }
            public string Aqn { get; set; }
            public string DisplayName { get; set; }
            public string Caption { get; set; }

            public string FullName => !string.IsNullOrWhiteSpace(Namespace) ? $"{Namespace}.{Name}" : Name;
        }

        // Namespace trie node used to build grouped tree
        private sealed class NsNode
        {
            public string Name { get; set; }
            public bool IsTerminal { get; set; }
            public Dictionary<string, NsNode> Children { get; }
            public NsNode(string name)
            {
                Name = name;
                Children = new Dictionary<string, NsNode>(StringComparer.Ordinal);
            }
            public NsNode GetOrAdd(string name)
            {
                if (!Children.TryGetValue(name, out var child))
                {
                    child = new NsNode(name);
                    Children[name] = child;
                }
                return child;
            }
        }
    }
}
