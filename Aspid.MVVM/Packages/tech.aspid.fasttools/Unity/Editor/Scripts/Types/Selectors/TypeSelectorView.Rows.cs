using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    // Row factory and per-row binding — leading icon/glyph, title, favorite toggle and the drill-in arrow —
    // plus the section-header collapse/expand interaction wired onto each row.
    internal sealed partial class TypeSelectorView
    {
        private const string ItemClass = BlockClass + "__item";
        private const string ItemInSectionClass = ItemClass + "--in-section";
        private const string ItemCurrentModifier = ItemClass + "--current";
        private const string ItemIconClass = BlockClass + "__item-icon";
        private const string ItemGlyphClass = BlockClass + "__item-glyph";
        private const string ItemTitleClass = BlockClass + "__item-title";
        private const string ItemCheckClass = BlockClass + "__item-check";
        private const string ItemCountClass = BlockClass + "__item-count";
        private const string ItemArrowClass = BlockClass + "__item-arrow";
        private const string ItemDividerClass = BlockClass + "__item-divider";
        private const string ItemContentClass = BlockClass + "__item-content";
        private const string RowAfterPinnedModifier = BlockClass + "__item--after-pinned";
        private const string SectionTitleClass = BlockClass + "__section-title";
        private const string FavoriteToggleClass = BlockClass + "__favorite-toggle";
        private const string FavoriteToggleOnModifier = FavoriteToggleClass + "--favorite-on";
        private const string ItemIconCollapsedModifier = ItemIconClass + "--collapsed";

        private const string TypeFallbackIcon = "d_cs Script Icon";
        private const string ScriptableObjectFallbackIcon = "d_ScriptableObject Icon";
        private const string ContainerFallbackIcon = "d_Folder Icon";
        private const string ContainerOpenFallbackIcon = "d_FolderOpened Icon";
        private const string FavoritesCollapsedIcon = "d_Favorite";
        private const string FavoritesExpandedIcon = "d_Favorite Icon";
        private const string RecentCollapsedIcon = "d_UnityEditor.HistoryWindow";
        private const string RecentExpandedIcon = "d_UnityEditor.HistoryWindow";

        private VisualElement CreateListItem()
        {
            // The pinned-block divider rides inside the row shell (absolutely positioned in its extra top zone,
            // shown only via --after-pinned) rather than as a border: a border-top would curve along the content's
            // rounded corners. All visuals live on the content wrapper pinned to the shell's bottom — see the
            // stylesheet's Item section for why the shell itself must stay bare.
            var divider = new VisualElement()
                .AddClass(ItemDividerClass)
                .SetPickingMode(PickingMode.Ignore);

            var icon = new Image()
                .AddClass(ItemIconClass)
                .SetPickingMode(PickingMode.Ignore);

            var glyph = new Label()
                .AddClass(ItemGlyphClass)
                .SetPickingMode(PickingMode.Ignore);

            var label = new Label()
                .AddClass(ItemTitleClass);

            var check = new Label(TypeSelectorHelpers.Check)
                .AddClass(ItemCheckClass)
                .SetPickingMode(PickingMode.Ignore);

            var count = new Label()
                .AddClass(ItemCountClass)
                .SetPickingMode(PickingMode.Ignore);

            var favorite = new Button()
                .AddClass(FavoriteToggleClass)
                .SetText(TypeSelectorHelpers.StarEmpty);

            var arrow = new Label("›")
                .AddClass(ItemArrowClass);

            var content = new VisualElement()
                .AddClass(ItemContentClass)
                .AddChild(icon)
                .AddChild(glyph)
                .AddChild(label)
                .AddChild(check)
                .AddChild(count)
                .AddChild(favorite)
                .AddChild(arrow);

            var row = new VisualElement()
                .AddClass(ItemClass)
                .AddChild(divider)
                .AddChild(content);

            row.RegisterCallback<ClickEvent>(OnRowClicked);

            return row;
        }

        private void BindListItem(VisualElement element, int index)
        {
            var items = _pages.Count > 0 ? Nav.CurrentItems : null;

            if (items is null) return;
            if (index < 0 || index >= items.Count) return;

            var node = items[index];
            var isSectionTitle = node.IsSectionTitle;

            // OnRowClicked reads this to know which node it operates on after row recycling.
            element.userData = node;

            var isCurrent = IsCurrentValue(node);

            element.EnableInClass(SectionTitleClass, isSectionTitle);
            element.EnableInClass(ItemInSectionClass, !isSectionTitle && node.SectionKey is not null);
            element.EnableInClass(ItemCurrentModifier, isCurrent);

            // A non-reorderable ListView adds the item class straight onto the makeItem element (no per-row
            // wrapper), so the divider modifier goes on the row root itself.
            element.EnableInClass(RowAfterPinnedModifier, IsFirstRowAfterPinnedBlock(items, index));

            element.SetPickingMode(PickingMode.Position);

            element.Q<Label>(className: ItemTitleClass)
                .SetText(node.DisplayName)
                .SetTooltip(node.Tooltip);

            BindLeading(element.Q<Image>(className: ItemIconClass), element.Q<Label>(className: ItemGlyphClass), node, index == _listView.selectedIndex);
            BindFavorite(element.Q<Button>(className: FavoriteToggleClass), node);

            element.Q<Label>(className: ItemCheckClass)
                .SetDisplay(isCurrent ? DisplayStyle.Flex : DisplayStyle.None);

            var typeCount = TypeCountFor(node);
            element.Q<Label>(className: ItemCountClass)
                .SetText(typeCount > 0 ? typeCount.ToString() : string.Empty)
                .SetDisplay(typeCount > 0 ? DisplayStyle.Flex : DisplayStyle.None);

            element.Q<Label>(className: ItemArrowClass)
                .SetDisplay(node.HasChildren && !Nav.IsSearching
                    ? DisplayStyle.Flex
                    : DisplayStyle.None);
        }

        // "Current" = the value the field already holds (the type by its AQN, or <None> for an empty field).
        // Base page only: a coincidental match on a generic-argument page is not the field's value.
        private bool IsCurrentValue(TreeNode node)
        {
            if (!_pages[^1].IsBase) return false;

            // Null = the host has no current-value concept (a list "+" append, a missing-type Fix, the bulk
            // project picker) — nothing wears the check there; only an EMPTY STRING rightly marks <None>.
            if (_currentAqn is null) return false;

            return _currentAqn.Length > 0
                ? node.IsType && node.AssemblyQualifiedName == _currentAqn
                : node.IsSelectable && node.DisplayName == TypeSelectorHelpers.NoneOption;
        }

        // Containers surface how many pickable types they hold; section titles carry their composed row
        // count. Type leaves show no counter, and search results are a flat type list with none either.
        private int TypeCountFor(TreeNode node)
        {
            if (Nav.IsSearching) return 0;
            return node.IsSectionTitle || node.HasChildren ? node.TypeCount : 0;
        }

        // True for the first ordinary root category after the pinned block (<None> plus the Favorites/Recent
        // sections) — the row carrying the divider. Only the base root page composes a pinned block.
        private bool IsFirstRowAfterPinnedBlock(List<TreeNode> items, int index)
        {
            if (!Nav.IsAtRoot || index <= 0 || index >= items.Count) return false;
            if (IsPinnedRow(items[index])) return false;

            return IsPinnedRow(items[index - 1]);
        }

        private static bool IsPinnedRow(TreeNode node) =>
            node.IsSectionTitle || node.SectionKey is not null || node.DisplayName == TypeSelectorHelpers.NoneOption;

        private void BindLeading(Image icon, Label glyph, TreeNode node, bool isSelected)
        {
            if (node.DisplayName == TypeSelectorHelpers.NoneOption)
            {
                icon.SetDisplay(DisplayStyle.None);
                glyph.SetText(TypeSelectorHelpers.None).SetDisplay(DisplayStyle.Flex);
                return;
            }

            glyph.SetDisplay(DisplayStyle.None);

            var sectionCollapsed = false;
            Texture texture;

            if (node.IsSectionTitle)
            {
                sectionCollapsed = Nav.IsSectionCollapsed(node.SectionKey);
                texture = TypeSelectorIconResolver.Resolve(SectionIcon(node.SectionKey, sectionCollapsed));
            }
            else
            {
                texture = TypeSelectorIconResolver.Resolve(node.Icon);

                if (texture is null)
                {
                    if (node.IsType)
                    {
                        texture = ResolveTypeFallbackIcon(node.AssemblyQualifiedName);
                    }
                    else
                    {
                        var fallback = node.HasChildren
                            ? (isSelected ? ContainerOpenFallbackIcon : ContainerFallbackIcon)
                            : null;
                        texture = TypeSelectorIconResolver.Resolve(fallback);
                    }
                }
            }

            icon
                .EnableInClass(ItemIconCollapsedModifier, sectionCollapsed)
                .SetImage(texture)
                .SetDisplay(texture is not null ? DisplayStyle.Flex : DisplayStyle.None);
        }

        // Type rows get the icon Unity itself paints for the type: AssetPreview.GetMiniTypeThumbnail honors a custom
        // icon assigned on the script's .meta (like the Aspid scripts) and yields the ScriptableObject icon for
        // ScriptableObject-derived types; everything without one falls through to the C# script icon. Results are cached
        // per assembly-qualified name to keep row binding cheap; a destroyed cached texture is dropped so a
        // later-imported / re-assigned icon is picked up on the next bind.
        private static readonly Dictionary<string, Texture> _typeFallbackCache = new();

        private static Texture ResolveTypeFallbackIcon(string assemblyQualifiedName)
        {
            if (string.IsNullOrEmpty(assemblyQualifiedName))
                return TypeSelectorIconResolver.Resolve(TypeFallbackIcon);

            if (_typeFallbackCache.TryGetValue(assemblyQualifiedName, out var cached))
            {
                if (cached) return cached;
                _typeFallbackCache.Remove(assemblyQualifiedName);
            }

            var texture = LoadTypeFallbackIcon(assemblyQualifiedName);

            if (texture is not null)
                _typeFallbackCache[assemblyQualifiedName] = texture;

            return texture;
        }

        private static Texture LoadTypeFallbackIcon(string assemblyQualifiedName)
        {
            var type = Type.GetType(assemblyQualifiedName);
            if (type is not null)
            {
                var thumbnail = AssetPreview.GetMiniTypeThumbnail(type);
                if (thumbnail is not null) return thumbnail;

                // Safety net when Unity has no cached thumbnail for the type yet.
                if (typeof(ScriptableObject).IsAssignableFrom(type))
                    return TypeSelectorIconResolver.Resolve(ScriptableObjectFallbackIcon);
            }

            return TypeSelectorIconResolver.Resolve(TypeFallbackIcon);
        }

        private static string SectionIcon(string sectionKey, bool collapsed)
        {
            if (sectionKey == NavigationController.RecentSection)
                return collapsed ? RecentCollapsedIcon : RecentExpandedIcon;

            return collapsed ? FavoritesCollapsedIcon : FavoritesExpandedIcon;
        }

        private void BindFavorite(Button favorite, TreeNode node)
        {
            // Replace any handler bound to a previously recycled row.
            favorite.clickable = new Clickable(() => ToggleFavorite(node));

            if (!node.IsType)
            {
                favorite.SetDisplay(DisplayStyle.None);
                return;
            }

            var isFavorite = TypeSelectorPreferences.IsFavorite(node.AssemblyQualifiedName);

            favorite
                .SetDisplay(DisplayStyle.Flex)
                .SetText(isFavorite ? TypeSelectorHelpers.StarFilled : TypeSelectorHelpers.StarEmpty)
                .EnableInClass(FavoriteToggleOnModifier, isFavorite);
        }

        private void OnRowClicked(ClickEvent evt)
        {
            if (evt.currentTarget is not VisualElement row) return;
            if (row.userData is not TreeNode node || !node.IsSectionTitle) return;

            ToggleSectionKeepSelection(node);
            evt.StopPropagation();
        }

        // Collapses/expands a section and re-selects its header at the new index, so a keyboard or mouse toggle leaves
        // the highlight on the section the user is acting on (the list rebuilds, so the index can shift).
        private void ToggleSectionKeepSelection(TreeNode sectionTitle)
        {
            Nav.ToggleSection(sectionTitle.SectionKey);
            RefreshView();

            var index = Nav.CurrentItems.IndexOf(sectionTitle);
            if (index >= 0) SetSelectedIndex(index);

            UpdateFooterHint();
        }
    }
}
