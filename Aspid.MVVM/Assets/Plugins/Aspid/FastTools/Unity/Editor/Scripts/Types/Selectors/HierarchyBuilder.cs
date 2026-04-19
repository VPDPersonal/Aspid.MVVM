using System;
using System.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal static class HierarchyBuilder
    {
        public static TreeNode Build(Type[] types, TypeAllow allow)
        {
            var allTypes = TypeInfo.GetAllTypeInfos(types, allow);

            var root = new TreeNode("/");
            root.Children.Add(new TreeNode(Constants.NoneOption, null, Constants.NoneOption));

            AddGlobalNamespaceGroup(root, allTypes);
            AddNamespaceHierarchy(root, allTypes);

            return root;
        }

        private static void AddGlobalNamespaceGroup(TreeNode root, List<TypeInfo> types)
        {
            var globals = types
                .Where(type => type.Namespace == Constants.GlobalNamespace)
                .OrderBy(type => type.Name)
                .ToList();

            if (globals.Count is 0) return;
            var globalGroup = new TreeNode(Constants.GlobalNamespace);

            AddTypesWithDisambiguation(globalGroup, globals, includeNamespace: false);
            root.Children.Add(globalGroup);
        }

        private static void AddNamespaceHierarchy(TreeNode root, List<TypeInfo> types)
        {
            var namespacedTypes = types
                .Where(type => type.Namespace != Constants.GlobalNamespace)
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

            // Add types at this namespace level
            if (trieNode.IsTerminal && nsToTypes.TryGetValue(nextNamespace, out var typeInfos))
                AddTypesWithDisambiguation(node, typeInfos, includeNamespace: true, nextNamespace);

            // Add child namespaces
            foreach (var child in trieNode.Children.Values.OrderBy(n => n.Segment))
                node.Children.Add(BuildNamespaceNode(child, nextDisplay, nextNamespace, nsToTypes));

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
    }
}
