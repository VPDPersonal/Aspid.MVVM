using System.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal sealed class NavigationController
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

            return string.Join("/", _breadcrumbs
                .Select(node => node.DisplayName)
                .Append(_currentNode.DisplayName)
                .Where(name => name is not "/"));
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

        public TreeNode NavigateBack()
        {
            if (!CanNavigateBack) return null;

            var previousNode = _currentNode;
            _currentNode = _breadcrumbs[^1];
            _breadcrumbs.RemoveAt(_breadcrumbs.Count - 1);
            return previousNode;
        }

        public void NavigateToAssemblyQualifiedName(string aqn)
        {
            var path = new List<TreeNode>();
            if (!FindPathToAssemblyQualifiedName(_rootNode, aqn, path) || path.Count < 2) return;

            // FindPathToAssemblyQualifiedName builds path leaf-to-root; reverse for root-to-leaf traversal
            path.Reverse();

            // Navigate into each node from root's child down to the target's parent
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
                    yield return leaf;
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
