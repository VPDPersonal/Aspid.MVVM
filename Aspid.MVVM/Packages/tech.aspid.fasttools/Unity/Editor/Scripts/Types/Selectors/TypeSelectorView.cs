using System;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// The hierarchical type selector as a host-agnostic <see cref="VisualElement"/>: search, keyboard
    /// navigation, namespace drill-down and the generic-argument resolution flow all live here.
    /// <see cref="TypeSelectorWindow"/> hosts it as a dropdown; embedding hosts (e.g. the Repair References
    /// window) add it inline and collapse it through the dismiss callback.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Selecting an open generic definition (injected via <see cref="TypeSelectorFilter.AdditionalTypes"/>) is not a final selection;
    /// instead it drills into an argument-selection flow — one hierarchical page per type parameter,
    /// reusing the same search/keyboard/navigation — and emits the constructed closed type once every argument
    /// is resolved. The argument flow stays dormant unless open generics are present, so the ordinary
    /// type-selection contract is unchanged.
    /// </para>
    /// <para>
    /// The implementation is split across partial files by concern: this file owns construction and the shared
    /// state; <c>.Rows</c> binds list rows; <c>.Input</c> handles the search chrome and keyboard; <c>.Navigation</c>
    /// drives drill-down and selection; <c>.Generics</c> hosts the argument-resolution page stack; <c>.View</c>
    /// renders breadcrumbs, the footer hint and errors.
    /// </para>
    /// </remarks>
    internal sealed partial class TypeSelectorView : VisualElement
    {
        private const string StyleSheetPath = "UI/Types/Aspid-FastTools-TypeSelector";

        // The static skeleton lives in a UXML cloned in BuildUI. It keeps a distinct base name from the stylesheet so
        // AddStyleSheetsFromResource's Resources.Load<StyleSheet> on StyleSheetPath stays unambiguous (a same-named
        // VisualTreeAsset would shadow the StyleSheet). The code keeps only the classes it toggles/queries at runtime;
        // the skeleton's own classes live in the UXML.
        private const string UxmlResourcePath = "UI/Types/Aspid-FastTools-TypeSelector-View";

        private const string BlockClass = "aspid-fasttools-type-selector";
        private const string HeaderClass = BlockClass + "__header";
        private const string HeaderSearchFocusedModifier = HeaderClass + "--search-focused";

        private const string HeaderName = "type-selector-header";
        private const string BreadcrumbBarName = "type-selector-breadcrumb-bar";
        private const string SearchButtonName = "type-selector-search-button";
        private const string SearchFieldName = "type-selector-search-field";
        private const string ErrorName = "type-selector-error";
        private const string ListName = "type-selector-list";
        private const string EmptyHintName = "type-selector-empty-hint";
        private const string FooterHintName = "type-selector-footer-hint";
        private const string SettingsButtonName = "type-selector-settings-button";

        private VisualElement _header;
        private VisualElement _breadcrumbBar;
        private Button _searchButton;
        private ListView _listView;
        private Label _errorLabel;
        private Label _emptyHint;
        private Label _footerHint;
        private Button _settingsButton;
        private ToolbarSearchField _searchField;

        private bool _searchFieldFocused;
        private bool _searchChromeOpen;

        // Space toggles a favorite, but it is also a navigation submit key — so the same press raises a NavigationSubmit
        // that would choose (and close on) the row. This arms the submit suppressor for that one event.
        private bool _suppressNextSubmit;

        private readonly List<PickerPage> _pages = new();

        private readonly Action _onDismiss;
        private readonly Action<string> _onSelected;
        private readonly Func<Type, bool> _argumentFilter;
        private readonly Type[] _fieldTypes;
        private readonly string _currentAqn;

        private NavigationController Nav => _pages[^1].Navigation;

        /// <summary>
        /// Creates a type selector view.
        /// </summary>
        /// <param name="filter">Defines which types the selector offers: base types, kind constraints, the per-type predicate, extra entries and the open-generic argument predicate. See <see cref="TypeSelectorFilter"/>.</param>
        /// <param name="currentAqn">Assembly-qualified name of the currently selected type, used to pre-navigate to that type's location. Pass <c>null</c> or empty to start at the root.</param>
        /// <param name="onSelected">Callback invoked with the assembly-qualified name of the selected type, or <c>null</c> if the user chose <c>&lt;None&gt;</c>. When an open generic is resolved, the assembly-qualified name of the constructed closed type is passed.</param>
        /// <param name="onDismiss">Invoked when the selector is done — after a selection is emitted, or when the user cancels with Escape. The host closes its window or collapses the inline panel here.</param>
        internal TypeSelectorView(
            TypeSelectorFilter filter = default,
            string currentAqn = "",
            Action<string> onSelected = null,
            Action onDismiss = null)
        {
            var types = filter.Types ?? new[] { typeof(object) };

            _onDismiss = onDismiss;
            _onSelected = onSelected;
            _argumentFilter = filter.ArgumentFilter;
            // Null and "" mean DIFFERENT things and both flow through unchanged: null = the host has no current-value
            // concept at all (a list "+" append, a missing-type Fix, the bulk project picker), "" = the field exists
            // and currently holds <None>. Only the latter may put the current-value check on the <None> row.
            _currentAqn = currentAqn;
            _fieldTypes = types;

            BuildUI();

            var hierarchy = HierarchyBuilder.Build(types, filter.Allow, filter.Predicate, filter.AdditionalTypes);
            var navigation = new NavigationController(hierarchy, composeSections: true);

            if (!string.IsNullOrWhiteSpace(_currentAqn))
                navigation.NavigateToAssemblyQualifiedName(_currentAqn);

            _pages.Add(new PickerPage
            {
                Navigation = navigation,
                TitlePrefix = null,
                ConstraintType = types.Length > 0 ? types[0] : typeof(object),
                OnPicked = closed => Emit(closed?.AssemblyQualifiedName),
                IsBase = true,
            });

            RefreshView();
            PreselectCurrent();
        }

        // Highlights the row for the currently selected type on open — the view has pre-navigated to that type's
        // namespace, so its row is in view and an immediate Enter re-confirms the same value. When the current value
        // is <None> ("") or its type is absent (a missing type keeps the navigation at the root), the pinned <None>
        // row is selected instead, so Enter re-confirms/clears rather than committing an arbitrary first row. Only a
        // null current value (the host has no current-value concept) leaves the selection empty and Enter inert.
        // FocusPicker scrolls the selection into view once the list is laid out.
        private void PreselectCurrent()
        {
            if (_currentAqn is null) return;

            var items = Nav.CurrentItems;

            if (!string.IsNullOrEmpty(_currentAqn))
            {
                for (var i = 0; i < items.Count; i++)
                {
                    if (items[i].IsType && items[i].AssemblyQualifiedName == _currentAqn)
                    {
                        _listView.selectedIndex = i;
                        return;
                    }
                }
            }

            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].IsSelectable && items[i].DisplayName == TypeSelectorHelpers.NoneOption)
                {
                    _listView.selectedIndex = i;
                    return;
                }
            }
        }

        /// <summary>
        /// Gives the picker keyboard focus so the arrow keys navigate and any printable key starts a search (the search
        /// field stays collapsed until then). Call after the view is attached to a panel.
        /// </summary>
        internal void FocusPicker()
        {
            if (Nav.CurrentItems.Count > 0)
            {
                // A just-shown ListView silently refuses Focus() until its display resolves on the next layout pass
                // (the same constraint OpenSearch documents for the search field), so defer the focus to that pass.
                // The current value's row is pre-selected in PreselectCurrent (no arbitrary first-row selection that an
                // immediate Enter could commit); scroll it into view once the list is laid out.
                _listView.schedule.Execute(() =>
                {
                    if (_listView.panel is null) return;

                    _listView.Focus();
                    if (_listView.selectedIndex >= 0) _listView.ScrollToItem(_listView.selectedIndex);
                });

                return;
            }

            Focus();
        }

        private void BuildUI()
        {
            focusable = true;

            this.AddAspidThemeStyleSheets()
                .AddStyleSheetsFromResource(StyleSheetPath)
                .AddClass(BlockClass);

            Resources.Load<VisualTreeAsset>(UxmlResourcePath).CloneTree(this);

            _header = this.Q<VisualElement>(HeaderName);
            _breadcrumbBar = this.Q<VisualElement>(BreadcrumbBarName);
            _searchButton = this.Q<Button>(SearchButtonName);
            _searchField = this.Q<ToolbarSearchField>(SearchFieldName);
            _errorLabel = this.Q<Label>(ErrorName);
            _listView = this.Q<ListView>(ListName);
            _emptyHint = this.Q<Label>(EmptyHintName);
            _footerHint = this.Q<Label>(FooterHintName);
            _settingsButton = this.Q<Button>(SettingsButtonName);

            _searchButton.clicked += () => OpenSearch();

            // Settings live outside the picker, so the selector is done: dismiss first (the dropdown host would lose
            // focus and close anyway; embedded hosts collapse), then land the user on the window's Settings tab.
            _settingsButton.clicked += () =>
            {
                _onDismiss?.Invoke();
                SerializeReferences.Editors.TabWindow.OpenSettings();
            };
            _breadcrumbBar.RegisterCallback<ClickEvent>(_ => OpenSearch());

            WireSearchField();
            WireListView();

            UpdateSearchChrome();

            RegisterCallback<KeyDownEvent>(HandleKeyDown, TrickleDown.TrickleDown);

            // The ListView drives its own selection from NavigationMoveEvent — a separate event from the KeyDownEvent
            // handled above. Left to fire it would advance the selection a second time on top of ours and skip a row,
            // so the directional moves are swallowed here and our KeyDown handler stays the single arrow navigator.
            RegisterCallback<NavigationMoveEvent>(SuppressDirectionalNavigation, TrickleDown.TrickleDown);

            RegisterCallback<NavigationSubmitEvent>(SuppressFavoriteSubmit, TrickleDown.TrickleDown);

            RegisterCallback<FocusInEvent>(_ => UpdateFooterHint());
        }

        private void WireSearchField()
        {
            _searchField.RegisterValueChangedCallback(e => HandleSearchChanged(e.newValue ?? string.Empty));

            _searchField.RegisterCallback<FocusInEvent>(_ =>
            {
                _searchFieldFocused = true;
                _header.EnableInClass(HeaderSearchFocusedModifier, true);

                _listView.ClearSelection();

                UpdateSearchChrome();
                UpdateFooterHint();
            });

            _searchField.RegisterCallback<FocusOutEvent>(evt =>
            {
                // Focus moving within the field (text input ↔ its clear button) is not a real blur — keep it open.
                if (evt.relatedTarget is VisualElement next && IsDescendantOf(next, _searchField)) return;

                _searchFieldFocused = false;
                _header.EnableInClass(HeaderSearchFocusedModifier, false);
                UpdateSearchChrome();
                UpdateFooterHint();
            });
        }

        private void WireListView()
        {
            _listView.SetMakeItem(CreateListItem);
            _listView.SetBindItem(BindListItem);
            _listView.itemsChosen += HandleItemChosen;

            // Re-bind the visible rows on every selection change so the selected folder can swap to its opened icon
            // (selection only toggles a USS class otherwise; the leading image is set in code, not USS).
            _listView.selectedIndicesChanged += _ =>
            {
                _listView.RefreshItems();
                UpdateFooterHint();
            };
        }

        private TreeNode SelectedNode()
        {
            var items = Nav.CurrentItems;
            var index = _listView.selectedIndex;
            return index >= 0 && index < items.Count ? items[index] : null;
        }

        private int FindSelectableIndex(int start, int step)
        {
            var items = _pages.Count > 0 ? Nav.CurrentItems : null;
            if (items is null) return -1;

            for (var i = start; i >= 0 && i < items.Count; i += step)
            {
                if (items[i].IsSelectable || items[i].HasChildren || items[i].IsSectionTitle)
                    return i;
            }

            return -1;
        }

        private void SetSelectedIndex(int index)
        {
            _listView.selectedIndex = index;
            _listView.ScrollToItem(index);
        }

        private static bool IsDescendantOf(VisualElement element, VisualElement ancestor)
        {
            for (var current = element; current is not null; current = current.parent)
                if (current == ancestor) return true;

            return false;
        }
    }
}
