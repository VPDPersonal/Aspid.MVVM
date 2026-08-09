using System.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal sealed class NavigationController
    {
        // Section keys double as the section titles and as the lookup the view keys its section icon off, so they live
        // here as the single source of truth.
        internal const string FavoritesSection = "Favorites";
        internal const string RecentSection = "Recent";

        private TreeNode _currentNode;
        private readonly TreeNode _rootNode;
        private readonly bool _composeSections;

        private readonly List<TreeNode> _breadcrumbs = new();
        private readonly List<TreeNode> _searchResults = new();

        // Cached composition of the root page (None + Favorites/Recents sections + root children).
        private readonly List<TreeNode> _rootItems = new();

        // Sections (by title) the user has collapsed; their item rows are hidden from _visibleRootItems until expanded.
        private readonly HashSet<string> _collapsedSections = new();

        // _rootItems with collapsed sections' item rows filtered out — the list actually shown on the root page.
        private readonly List<TreeNode> _visibleRootItems = new();

        // All pickable type leaves in the current candidate set, keyed by assembly-qualified name.
        private readonly Dictionary<string, TreeNode> _typesByAqn = new();

        internal bool IsSearching { get; private set; }

        internal bool CanNavigateBack =>
            _breadcrumbs.Count > 0;

        /// <summary>
        /// Whether the picker is showing its root page (not searching, no breadcrumbs).
        /// </summary>
        /// <remarks>
        /// The Favorites and Recents sections are only composed here, and only when the controller was
        /// created with section composition enabled.
        /// </remarks>
        internal bool IsAtRoot =>
            !IsSearching && _breadcrumbs.Count is 0;

        /// <summary>
        /// The ancestor chain from the synthetic root down to (but excluding) <see cref="CurrentNode"/>;
        /// the breadcrumb bar reads this to render the clickable trail.
        /// </summary>
        /// <remarks>
        /// <c>Breadcrumbs[0]</c> is always the hidden <c>"/"</c> root; real ancestors start at index 1.
        /// </remarks>
        internal IReadOnlyList<TreeNode> Breadcrumbs => _breadcrumbs;

        /// <summary>
        /// The node whose children are currently listed (the deepest opened level).
        /// </summary>
        internal TreeNode CurrentNode => _currentNode;

        internal List<TreeNode> CurrentItems
        {
            get
            {
                if (IsSearching) return _searchResults;
                if (_composeSections && IsAtRoot) return _visibleRootItems;
                return _currentNode.Children;
            }
        }

        /// <param name="root">The root of the candidate hierarchy.</param>
        /// <param name="composeSections">
        /// When <see langword="true"/>, the root page is augmented with <c>★ Favorites</c> and
        /// <c>Recent</c> sections drawn from <see cref="TypeSelectorPreferences"/>; only the base
        /// (root) page of the picker enables this — generic-argument pages do not.
        /// </param>
        internal NavigationController(TreeNode root, bool composeSections = false)
        {
            _rootNode = root;
            _currentNode = root;
            _composeSections = composeSections;
            _breadcrumbs.Clear();

            if (!_composeSections) return;

            // Favorites and Recents open collapsed: they are a quick-access convenience, not the primary list, so the
            // root lands on the full type hierarchy and the user expands a section when they want it. Seeding the keys
            // here (rather than per-section) also collapses a section that only appears later — once its first favorite
            // or recent is recorded — while a user-driven expand survives, since RebuildRootItems never re-adds them.
            _collapsedSections.Add(FavoritesSection);
            _collapsedSections.Add(RecentSection);

            IndexTypeLeaves(root);
            RebuildRootItems();
        }

        internal void ApplySearch(string query)
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
                            node.Caption)
                        {
                            Tooltip = node.Tooltip,
                            Icon = node.Icon,
                            SearchName = node.SearchName,
                        });
                }
            }
        }

        internal void NavigateInto(TreeNode node)
        {
            _breadcrumbs.Add(_currentNode);
            _currentNode = node;
        }

        /// <summary>
        /// Pops breadcrumbs until exactly <paramref name="keep"/> remain, making the ancestor that sat at
        /// that depth the current node — a one-click jump up several levels for the breadcrumb bar.
        /// </summary>
        /// <remarks>
        /// <c>keep == 0</c> returns to the root page; a <paramref name="keep"/> at or above the current
        /// depth is a no-op.
        /// </remarks>
        internal void NavigateToDepth(int keep)
        {
            while (_breadcrumbs.Count > keep && CanNavigateBack)
                NavigateBack();
        }

        internal TreeNode NavigateBack()
        {
            if (!CanNavigateBack) return null;

            var previousNode = _currentNode;
            _currentNode = _breadcrumbs[^1];
            _breadcrumbs.RemoveAt(_breadcrumbs.Count - 1);
            return previousNode;
        }

        internal void NavigateToAssemblyQualifiedName(string aqn)
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

        /// <summary>
        /// Re-composes the root page after the favorites set changes (e.g. a star was toggled), so the
        /// Favorites section reflects the new state on the next refresh. No-op when this controller does
        /// not compose sections.
        /// </summary>
        internal void RefreshFavoritesSection()
        {
            if (_composeSections) RebuildRootItems();
        }

        /// <summary>
        /// Whether the section identified by <paramref name="sectionKey"/> is currently collapsed.
        /// </summary>
        internal bool IsSectionCollapsed(string sectionKey) =>
            sectionKey is not null && _collapsedSections.Contains(sectionKey);

        /// <summary>
        /// Toggles the collapsed state of the section identified by <paramref name="sectionKey"/> and re-filters the
        /// visible root composition. No-op when this controller does not compose sections.
        /// </summary>
        internal void ToggleSection(string sectionKey)
        {
            if (!_composeSections || string.IsNullOrEmpty(sectionKey)) return;

            if (!_collapsedSections.Remove(sectionKey))
                _collapsedSections.Add(sectionKey);

            RebuildVisibleRootItems();
        }

        private void RebuildRootItems()
        {
            _rootItems.Clear();

            // Pin the <None> option (always the first root child) to the very top.
            var noneOption = _rootNode.Children
                .FirstOrDefault(child => child.DisplayName == TypeSelectorHelpers.NoneOption);

            if (noneOption is not null)
                _rootItems.Add(noneOption);

            // Each section is an individual opt-out that leaves its stored preference data intact, so re-enabling
            // restores the same list. Favorites has an explicit per-user toggle; Recent needs none — a recents
            // capacity of 0 makes LoadRecents return nothing and an empty section is never composed.
            if (TypeSelectorSettings.ShowFavorites)
                AppendSection(FavoritesSection, TypeSelectorPreferences.LoadFavorites());

            AppendSection(RecentSection, TypeSelectorPreferences.LoadRecents());

            foreach (var child in _rootNode.Children)
            {
                if (child != noneOption)
                    _rootItems.Add(child);
            }

            RebuildVisibleRootItems();
        }

        // Section titles always stay visible so the user can expand a collapsed section again.
        private void RebuildVisibleRootItems()
        {
            _visibleRootItems.Clear();

            foreach (var node in _rootItems)
            {
                if (!node.IsSectionTitle && node.SectionKey is not null && _collapsedSections.Contains(node.SectionKey))
                    continue;

                _visibleRootItems.Add(node);
            }
        }

        private void AppendSection(string title, IReadOnlyList<string> assemblyQualifiedNames)
        {
            var rows = new List<TreeNode>();

            foreach (var aqn in assemblyQualifiedNames)
            {
                // Only surface types that are part of the current candidate set.
                if (!_typesByAqn.TryGetValue(aqn, out var source)) continue;

                rows.Add(new TreeNode(source.DisplayName, source.AssemblyQualifiedName, source.Caption)
                {
                    Tooltip = source.Tooltip,
                    Icon = source.Icon,
                    SearchName = source.SearchName,
                    SectionKey = title,
                });
            }

            if (rows.Count is 0) return;

            // The title carries its row count so the header can show how much a collapsed section holds.
            _rootItems.Add(new TreeNode(title) { Kind = TreeNodeKind.SectionTitle, SectionKey = title, TypeCount = rows.Count });
            _rootItems.AddRange(rows);
        }

        private void IndexTypeLeaves(TreeNode node)
        {
            if (node.AssemblyQualifiedName is not null)
                _typesByAqn[node.AssemblyQualifiedName] = node;

            foreach (var child in node.Children)
                IndexTypeLeaves(child);
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
