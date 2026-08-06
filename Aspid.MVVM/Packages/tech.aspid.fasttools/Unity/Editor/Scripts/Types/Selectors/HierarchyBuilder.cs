using System;
using System.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal static class HierarchyBuilder
    {
        internal static TreeNode Build(
            Type[] types,
            TypeAllow allow,
            Func<Type, bool> filter = null,
            IEnumerable<Type> additionalTypes = null,
            bool includeNoneOption = true)
        {
            var allTypes = TypeInfo.GetAllTypeInfos(types, allow, filter, additionalTypes);

            var root = new TreeNode("/");

            // Generic-argument pages must yield a concrete type, so they omit the <None> entry.
            if (includeNoneOption)
                root.Children.Add(new TreeNode(TypeSelectorHelpers.NoneOption, null, TypeSelectorHelpers.NoneOption));

            // An explicit [TypeSelectorDisplay(Group)] replaces the namespace placement — a grouped type
            // appears only under its declared path, so it is excluded from the namespace passes entirely.
            var ungrouped = allTypes.Where(type => type.GroupPath is null).ToList();
            var grouped = allTypes.Where(type => type.GroupPath is not null).ToList();

            AddGlobalNamespaceGroup(root, ungrouped);
            AddNamespaceHierarchy(root, ungrouped);
            AddGroupHierarchy(root, grouped);

            SortNode(root);

            return root;
        }

        private static void AddGlobalNamespaceGroup(TreeNode root, List<TypeInfo> types)
        {
            var globals = types
                .Where(type => type.Namespace == TypeSelectorHelpers.GlobalNamespace)
                .ToList();

            if (globals.Count is 0) return;
            var globalGroup = new TreeNode(TypeSelectorHelpers.GlobalNamespace);

            AddTypesWithDisambiguation(globalGroup, globals);
            root.Children.Add(globalGroup);
        }

        // Every [TypeSelectorDisplay(Group)] path becomes a chain of container nodes; shared segments reuse one
        // node. Group nodes are never merged or flattened into the namespace trie — the author's path shows as spelled.
        private static void AddGroupHierarchy(TreeNode root, List<TypeInfo> types)
        {
            if (types.Count is 0) return;

            var nodesByPath = new Dictionary<string, TreeNode>(StringComparer.Ordinal);

            // No ordering here: SortNode(root) re-sorts every level afterwards, and node reuse is keyed on the
            // exact declared path (case-sensitive — the author's spelling is shown as written).
            foreach (var pathGroup in types.GroupBy(type => string.Join("/", type.GroupPath)))
            {
                var parent = root;
                var path = string.Empty;

                foreach (var segment in pathGroup.First().GroupPath)
                {
                    path = path.Length is 0 ? segment : $"{path}/{segment}";

                    if (!nodesByPath.TryGetValue(path, out var node))
                    {
                        node = new TreeNode(segment, null, path);
                        nodesByPath[path] = node;
                        parent.Children.Add(node);
                    }

                    parent = node;
                }

                AddTypesWithDisambiguation(parent, pathGroup.ToList(), captionPrefix: pathGroup.Key, separator: '/');
            }
        }

        private static void AddNamespaceHierarchy(TreeNode root, List<TypeInfo> types)
        {
            var namespacedTypes = types
                .Where(type => type.Namespace != TypeSelectorHelpers.GlobalNamespace)
                .ToList();

            var trie = BuildNamespaceTrie(namespacedTypes);

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
                    current = current.GetOrCreateChild(segment);

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

            if (trieNode.IsTerminal && nsToTypes.TryGetValue(nextNamespace, out var typeInfos))
                AddTypesWithDisambiguation(node, typeInfos, nextNamespace);

            foreach (var child in trieNode.Children.Values.OrderBy(n => n.Segment))
                node.Children.Add(BuildNamespaceNode(child, nextDisplay, nextNamespace, nsToTypes));

            return FlattenSingleChildChain(node);
        }

        private static TreeNode FlattenSingleChildChain(TreeNode node)
        {
            if (node.Children.Count != 1) return node;

            var onlyChild = node.Children[0];

            if (onlyChild.AssemblyQualifiedName == null)
            {
                node.DisplayName = $"{node.DisplayName}.{onlyChild.DisplayName}";
                node.Caption = onlyChild.Caption;
                node.Children.Clear();
                node.Children.AddRange(onlyChild.Children);
            }
            else
            {
                node.DisplayName = $"{node.DisplayName}.{onlyChild.DisplayName}";
                node.AssemblyQualifiedName = onlyChild.AssemblyQualifiedName;
                node.Caption = onlyChild.Caption;
                node.Tooltip = onlyChild.Tooltip;
                node.Icon = onlyChild.Icon;
                node.SearchName = onlyChild.SearchName;
                node.Children.Clear();
            }

            return node;
        }

        // One leaf per type, labelled by TypeInfo.Label. Label collisions are disambiguated with the assembly
        // suffix; collisions within one assembly (same Name override) fall back to the real type name. The caption
        // prefixes the label with the node's path (namespaces join with '.', explicit groups with '/').
        private static void AddTypesWithDisambiguation(
            TreeNode parent,
            List<TypeInfo> types,
            string captionPrefix = null,
            char separator = '.')
        {
            var labelCounts = types
                .GroupBy(type => type.Label)
                .ToDictionary(g => g.Key, g => g.Count());

            var labelAssemblyCounts = types
                .GroupBy(type => (type.Label, type.Assembly))
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var type in types)
            {
                var displayName = labelCounts[type.Label] switch
                {
                    1 => type.Label,
                    _ when labelAssemblyCounts[(type.Label, type.Assembly)] == 1 => $"{type.Label} ({type.Assembly})",
                    _ => type.Label == type.Name ? $"{type.Label} ({type.Assembly})" : $"{type.Label} ({type.Name})",
                };

                var caption = string.IsNullOrEmpty(captionPrefix)
                    ? displayName
                    : $"{captionPrefix}{separator}{displayName}";

                parent.Children.Add(CreateLeaf(type, displayName, caption));
            }
        }

        private static TreeNode CreateLeaf(TypeInfo type, string displayName, string caption = null) => new(displayName, type.AssemblyQualifiedName, caption ?? displayName)
        {
            Tooltip = type.Tooltip,
            Icon = type.Icon,
            SearchName = type.Name,
        };

        private static void SortNode(TreeNode node)
        {
            node.Children.Sort(CompareNodes);

            foreach (var child in node.Children)
                SortNode(child);
        }

        private static int CompareNodes(TreeNode left, TreeNode right)
        {
            // Keep <None> pinned to the top of the root list.
            var leftNone = left.DisplayName == TypeSelectorHelpers.NoneOption;
            var rightNone = right.DisplayName == TypeSelectorHelpers.NoneOption;

            if (leftNone != rightNone) return leftNone ? -1 : 1;
            return string.Compare(left.DisplayName, right.DisplayName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
