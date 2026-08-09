using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    // Search-chrome toggling and the full keyboard model: type-to-search, arrow navigation, the Escape ladder,
    // Space-to-favorite (with its submit suppression) and the directional/submit event guards.
    internal sealed partial class TypeSelectorView
    {
        private void UpdateSearchChrome()
        {
            _searchChromeOpen = _searchFieldFocused || !string.IsNullOrEmpty(_searchField.value);

            _breadcrumbBar.SetDisplay(_searchChromeOpen ? DisplayStyle.None : DisplayStyle.Flex);
            _searchButton.SetDisplay(_searchChromeOpen ? DisplayStyle.None : DisplayStyle.Flex);
            _searchField.SetDisplay(_searchChromeOpen ? DisplayStyle.Flex : DisplayStyle.None);
        }

        private void OpenSearch(string initialText = null)
        {
            _searchChromeOpen = true;
            _breadcrumbBar.SetDisplay(DisplayStyle.None);
            _searchButton.SetDisplay(DisplayStyle.None);
            _searchField.SetDisplay(DisplayStyle.Flex);

            // Seeding the value works immediately, but the field was just un-hidden — its display only resolves on the
            // next layout pass, and a display:none element silently refuses Focus(). Defer the focus (and caret-to-end)
            // to that pass so the field reliably takes focus and any further typing lands in it.
            if (!string.IsNullOrEmpty(initialText))
                _searchField.value = initialText;

            _searchField.schedule.Execute(() =>
            {
                // The deferred tick can land after the embedding host collapsed the panel; bail if we are detached.
                if (_searchField.panel is null) return;

                _searchField.Focus();
                TryMoveSearchCursorToEnd();
            });

            UpdateFooterHint();
        }

        private void CollapseSearch()
        {
            ResetSearchField();
            FocusPicker();
        }

        private static void SuppressDirectionalNavigation(NavigationMoveEvent evt)
        {
            switch (evt.direction)
            {
                case NavigationMoveEvent.Direction.Up:
                case NavigationMoveEvent.Direction.Down:
                case NavigationMoveEvent.Direction.Left:
                case NavigationMoveEvent.Direction.Right:
                    evt.StopPropagation();
                    break;
            }
        }

        private void SuppressFavoriteSubmit(NavigationSubmitEvent evt)
        {
            if (!_suppressNextSubmit) return;

            _suppressNextSubmit = false;
            evt.StopPropagation();
        }

        private void HandleKeyDown(KeyDownEvent evt)
        {
            // Any real key press other than the favorite Space (and other than the paired character event, keyCode None)
            // cancels a pending favorite-submit suppression, so a later Enter still chooses normally.
            if (evt.keyCode != KeyCode.Space && evt.keyCode != KeyCode.None)
                _suppressNextSubmit = false;

            if (!_searchChromeOpen && IsTypingCharacter(evt))
            {
                OpenSearch(evt.character.ToString());
                evt.StopPropagation();
                return;
            }

            switch (evt.keyCode)
            {
                case KeyCode.UpArrow:
                    if (HandleUpArrow()) evt.StopPropagation();
                    break;

                case KeyCode.DownArrow:
                    if (HandleDownArrow()) evt.StopPropagation();
                    break;

                case KeyCode.Escape:
                    HandleEscapeKey();
                    evt.StopPropagation();
                    break;

                case KeyCode.Space:
                    if (!IsSearchFocused(focusController?.focusedElement))
                    {
                        HandleToggleFavoriteKey();
                        _suppressNextSubmit = true;
                        evt.StopPropagation();
                    }
                    break;

                case KeyCode.RightArrow:
                    if (!IsSearchFocused(focusController?.focusedElement) && HandleRightArrow())
                        evt.StopPropagation();
                    break;

                case KeyCode.LeftArrow:
                    if (!IsSearchFocused(focusController?.focusedElement) && HandleLeftArrow())
                        evt.StopPropagation();
                    break;
            }
        }

        // A KeyDownEvent that represents a printable character typed without a command/control/alt chord. Unity raises a
        // separate character event (keyCode == None, character set) alongside the keyCode event, so keying off the
        // character keeps arrows/Esc/Enter — which carry no printable character — on the navigation path below.
        private static bool IsTypingCharacter(KeyDownEvent evt)
        {
            var c = evt.character;

            if (c == '\0' || char.IsControl(c) || char.IsWhiteSpace(c)) return false;
            return !evt.ctrlKey && !evt.commandKey && !evt.altKey;
        }

        private bool HandleUpArrow()
        {
            var focused = focusController?.focusedElement;

            if (IsSearchFocused(focused))
                return TryMoveSearchCursorToStart();

            if (!IsListFocused(focused)) return false;

            var target = FindSelectableIndex(_listView.selectedIndex - 1, step: -1);

            if (target < 0)
            {
                OpenSearch();
                return true;
            }

            SetSelectedIndex(target);
            return true;
        }

        private bool HandleDownArrow()
        {
            var focused = focusController?.focusedElement;

            if (IsSearchFocused(focused))
            {
                if (TryMoveSearchCursorToEnd()) return true;
                if (Nav.CurrentItems is not { Count: > 0 }) return false;

                _listView.Focus();

                var first = FindSelectableIndex(0, step: 1);
                if (first >= 0) SetSelectedIndex(first);

                return true;
            }

            if (!IsListFocused(focused)) return false;

            var target = FindSelectableIndex(_listView.selectedIndex + 1, step: 1);
            if (target >= 0) SetSelectedIndex(target);

            return true;
        }

        private bool IsSearchFocused(Focusable focused) =>
            focused == _searchField || IsDescendantOf(focused as VisualElement, _searchField);

        private bool IsListFocused(Focusable focused) =>
            focused == _listView || IsDescendantOf(focused as VisualElement, _listView);

        private bool TryMoveSearchCursorToStart()
        {
            var input = _searchField.Q<TextField>();
            if (input is null) return false;

            if (input.cursorIndex == 0 && input.selectIndex == 0) return false;

            input.SelectRange(0, 0);
            return true;
        }

        private bool TryMoveSearchCursorToEnd()
        {
            var input = _searchField.Q<TextField>();
            if (input is null) return false;

            var length = input.value?.Length ?? 0;
            if (input.cursorIndex == length && input.selectIndex == length) return false;

            input.SelectRange(length, length);
            return true;
        }

        private void HandleEscapeKey()
        {
            // A three-step ladder: the first Esc clears a running query and keeps the field open and focused for a retype
            // (re-asserted here so the rung behaves the same whether focus sits in the field or has moved to the results);
            // a second (empty field) collapses the header back to the trail; a third (trail showing) dismisses the picker.
            if (!string.IsNullOrEmpty(_searchField.value))
            {
                _searchField.value = string.Empty;
                OpenSearch();
                return;
            }

            if (_searchChromeOpen)
            {
                CollapseSearch();
                return;
            }

            _onDismiss?.Invoke();
        }

        private bool HandleRightArrow()
        {
            var node = SelectedNode();
            if (node is null) return false;

            if (node.IsSectionTitle)
            {
                if (!Nav.IsSectionCollapsed(node.SectionKey)) return false;
                ToggleSectionKeepSelection(node);
                return true;
            }

            if (node.HasChildren && !Nav.IsSearching)
            {
                NavigateInto(node);
                return true;
            }

            return false;
        }

        private bool HandleLeftArrow()
        {
            if (SelectedNode() is { IsSectionTitle: true } section)
            {
                if (Nav.IsSectionCollapsed(section.SectionKey)) return false;
                ToggleSectionKeepSelection(section);
                return true;
            }

            if (!Nav.CanNavigateBack && _pages.Count <= 1) return false;

            NavigateBack();
            return true;
        }

        private bool HandleToggleFavoriteKey()
        {
            if (SelectedNode() is not { IsType: true } node) return false;

            ToggleFavorite(node);

            // Favoriting at the root recomposes the list (the Favorites section moves), so keep the highlight on the row
            // the user acted on where it survives the rebuild.
            var index = Nav.CurrentItems.IndexOf(node);
            if (index >= 0) SetSelectedIndex(index);

            return true;
        }

        private void HandleSearchChanged(string query)
        {
            Nav.ApplySearch(query);
            UpdateSearchChrome();
            RefreshView();
        }

        private void HandleItemChosen(IEnumerable<object> items)
        {
            var node = items.OfType<TreeNode>().FirstOrDefault();

            if (node is not null)
                ActivateNode(node);
        }
    }
}
