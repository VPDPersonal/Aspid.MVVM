using System;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    // Rendering the chrome around the list: the item list refresh, the empty/footer hints, the breadcrumb trail
    // and the inline error band. The footer-hint text itself is composed by the pure TypeSelectorFooterHint.
    internal sealed partial class TypeSelectorView
    {
        private const string CrumbClass = BlockClass + "__breadcrumb";
        private const string CrumbCurrentModifier = CrumbClass + "--current";
        private const string CrumbLinkModifier = CrumbClass + "--link";
        private const string CrumbSeparatorClass = BlockClass + "__breadcrumb-separator";

        private void RefreshView()
        {
            RebuildBreadcrumbs();

            var items = Nav.CurrentItems;
            _listView.itemsSource = items;
            _listView.RefreshItems();

            var hasItems = items.Count > 0;
            _listView.SetDisplay(hasItems ? DisplayStyle.Flex : DisplayStyle.None);
            _emptyHint.text = hasItems ? string.Empty : BuildEmptyHintText();
            _emptyHint.SetDisplay(hasItems ? DisplayStyle.None : DisplayStyle.Flex);

            UpdateFooterHint();
        }

        private string BuildEmptyHintText() =>
            Nav.IsSearching
                ? $"No types match '{_searchField.value}'."
                : "Nothing here.";

        private void UpdateFooterHint()
        {
            if (_footerHint is null) return;

            var node = SelectedNode();
            var sectionCollapsed = node is { IsSectionTitle: true } && Nav.IsSectionCollapsed(node.SectionKey);

            _footerHint.text = TypeSelectorFooterHint.Build(
                searchFocused: IsSearchFocused(focusController?.focusedElement),
                selected: node,
                isSelectedSectionCollapsed: sectionCollapsed,
                isSearching: Nav.IsSearching,
                searchChromeOpen: _searchChromeOpen,
                canNavigateBack: Nav.CanNavigateBack,
                hasParentPage: _pages.Count > 1);
        }

        private void RebuildBreadcrumbs()
        {
            _breadcrumbBar.Clear();

            if (Nav.IsSearching)
            {
                AddCrumb("Search", isCurrent: true, action: null);
                return;
            }

            var page = _pages[^1];

            if (page.IsBase && !Nav.CanNavigateBack)
            {
                AddCrumb("Select Type", isCurrent: true, action: null);
                return;
            }

            var crumbs = new List<(string Label, Action Action, bool IsCurrent)>();
            var atContextRoot = !Nav.CanNavigateBack;

            if (page.IsBase)
            {
                crumbs.Add(("Types", () => JumpToDepth(0), false));
            }
            else
            {
                // Generic-argument context (e.g. "Modifier<?>"): from the page's own root it pops back to the previous
                // page; once drilled into a namespace it jumps back to this page's root listing instead. It reads as
                // the current location only while nothing deeper is open.
                crumbs.Add((page.TitlePrefix, atContextRoot ? PopPage : () => JumpToDepth(0), atContextRoot));
            }

            // Real ancestors live at Breadcrumbs[1..] (index 0 is the hidden "/" root).
            var trail = Nav.Breadcrumbs;
            for (var i = 1; i < trail.Count; i++)
            {
                var depth = i;
                crumbs.Add((trail[i].DisplayName, () => JumpToDepth(depth), false));
            }

            if (Nav.CanNavigateBack)
                crumbs.Add((Nav.CurrentNode.DisplayName, null, true));

            for (var i = 0; i < crumbs.Count; i++)
            {
                AddCrumb(crumbs[i].Label, crumbs[i].IsCurrent, crumbs[i].Action);
                if (i < crumbs.Count - 1) AddSeparator();
            }
        }

        private void AddCrumb(string text, bool isCurrent, Action action)
        {
            var crumb = new Label(text)
                .AddClass(CrumbClass)
                .EnableInClass(CrumbCurrentModifier, isCurrent)
                .SetTooltip(text);

            // Clickability tracks the action, not the current flag: the generic-context crumb is both "here" and an
            // escape hatch, so it can be the current crumb yet still navigate.
            if (action is not null)
            {
                crumb.AddClass(CrumbLinkModifier);
                crumb.RegisterCallback<ClickEvent>(evt =>
                {
                    action();

                    // Keep the click off the breadcrumb bar's open-search handler — a navigable crumb navigates only.
                    evt.StopPropagation();
                });
            }

            _breadcrumbBar.AddChild(crumb);
        }

        private void AddSeparator() =>
            _breadcrumbBar.AddChild(new Label("›")
                .AddClass(CrumbSeparatorClass)
                .SetPickingMode(PickingMode.Ignore));

        private void JumpToDepth(int keep)
        {
            HideError();
            Nav.NavigateToDepth(keep);
            RefreshView();
            SelectFirstItem();
        }

        private void ShowError(string message)
        {
            if (_errorLabel is null) return;

            _errorLabel.text = message;
            _errorLabel.style.display = DisplayStyle.Flex;
        }

        private void HideError()
        {
            if (_errorLabel is not null)
                _errorLabel.style.display = DisplayStyle.None;
        }
    }
}
