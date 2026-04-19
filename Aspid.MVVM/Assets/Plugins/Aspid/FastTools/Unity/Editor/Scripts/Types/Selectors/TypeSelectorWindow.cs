using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// Editor window that displays a hierarchical type selector dropdown, allowing the user to browse and select a <see cref="System.Type"/> from a filtered list.
    /// </summary>
    public sealed class TypeSelectorWindow : EditorWindow
    {
        private const string StylesheetPath = "Styles/Aspid-FastTools-TypeSelectorWindow";
        private const string HeaderClass = "aspid-fasttools-type-selector-header";
        private const string ItemTitleClass = "aspid-fasttools-type-selector-item-title";
        private const string ItemArrowClass = "aspid-fasttools-type-selector-item-arrow";

        private Label _titleLabel;
        private Button _backButton;
        private ListView _listView;
        private ToolbarSearchField _searchField;
        private NavigationController _navigation;

        private Action<string> _onSelected;
        private string _currentAqn = string.Empty;

        /// <summary>
        /// Opens the type selector window as a dropdown anchored to <paramref name="screenRect"/>.
        /// </summary>
        /// <param name="screenRect">The screen-space rectangle the dropdown is anchored to.</param>
        /// <param name="types">Base types used to filter which concrete types are shown. Only types assignable to all entries are listed.</param>
        /// <param name="currentAqn">Assembly-qualified name of the currently selected type, used to pre-navigate to that type's location. Pass <c>null</c> or empty to start at the root.</param>
        /// <param name="allow">Which type kinds are included in the list. Defaults to <c>TypeAllow.None</c>.</param>
        /// <param name="onSelected">Callback invoked with the assembly-qualified name of the selected type, or <c>null</c> if the user chose <c>&lt;None&gt;</c>.</param>
        public static void Show(
            Rect screenRect,
            Type[] types = null,
            string currentAqn = "",
            TypeAllow allow = TypeAllow.None,
            Action<string> onSelected = null)
        {
            var window = CreateInstance<TypeSelectorWindow>();
            window.Initialize(
                screenRect,
                types,
                currentAqn,
                allow,
                onSelected);
        }

        #region Initialization
        private void Initialize(
            Rect screenRect,
            Type[] types,
            string currentAqn,
            TypeAllow allow,
            Action<string> onSelected)
        {
            _onSelected = onSelected;
            _currentAqn = currentAqn ?? string.Empty;

            BuildUI();

            var hierarchy = HierarchyBuilder.Build(types, allow);
            InitializeNavigation(hierarchy, _currentAqn);

            RefreshView();

            var size = new Vector2(Mathf.Max(350, screenRect.width), 320);
            ShowAsDropDown(screenRect, size);
            
            _searchField.Focus();
        }

        private void BuildUI()
        {
            _searchField = CreateSearchField();
            _listView = CreateListView();

            rootVisualElement
                .AddStyleSheetsFromResource(StylesheetPath)
                .AddChild(CreateHeader())
                .AddChild(_searchField)
                .AddChild(_listView);

            rootVisualElement.RegisterCallback<KeyDownEvent>(HandleKeyDown, TrickleDown.TrickleDown);
            return;

            VisualElement CreateHeader()
            {
                _titleLabel = new Label(string.Empty);
                _backButton = new Button(NavigateBack).SetText("←");

                return new VisualElement()
                    .AddClass(HeaderClass)
                    .AddChild(_backButton)
                    .AddChild(_titleLabel);
            }

            ToolbarSearchField CreateSearchField()
            {
                var field = new ToolbarSearchField();

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
                    .AddClass(ItemTitleClass);

                var arrow = new Label("›")
                    .AddClass(ItemArrowClass);

                return new VisualElement()
                    .AddChild(label)
                    .AddChild(arrow);
            }

            void BindListItem(VisualElement element, int index)
            {
                var items = _navigation?.CurrentItems;

                if (items is null) return;
                if (index < 0 || index >= items.Count) return;

                var node = items[index];
                element.Q<Label>(className: ItemTitleClass)
                    .SetText(node.DisplayName)
                    .SetTooltip(node.Tooltip);

                element.Q<Label>(className: ItemArrowClass)
                    .SetDisplay(node.HasChildren && !_navigation.IsSearching
                        ? DisplayStyle.Flex
                        : DisplayStyle.None);
            }
        }

        private void InitializeNavigation(TreeNode hierarchy, string currentAqn)
        {
            _navigation = new NavigationController(hierarchy);

            if (!string.IsNullOrWhiteSpace(currentAqn))
                _navigation.NavigateToAssemblyQualifiedName(currentAqn);
        }
        #endregion

        #region KeyDown Handlers
        private void HandleKeyDown(KeyDownEvent evt)
        {
            switch (evt.keyCode)
            {
                case KeyCode.UpArrow:
                    if (_listView.selectedIndex is 0)
                        _searchField.Focus();

                    evt.StopPropagation();
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
            if (_navigation.IsSearching && !string.IsNullOrWhiteSpace(_searchField.value))
                _searchField.value = string.Empty;
            else Close();
        }

        private void HandleRightArrow()
        {
            var items = _navigation.CurrentItems;
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

            if (node is not null)
                ActivateNode(node);
        }
        #endregion

        #region Navigation
        private void ActivateNode(TreeNode node)
        {
            if (node.HasChildren && !_navigation.IsSearching) NavigateInto(node);
            else if (node.IsSelectable) SelectNode(node);
        }

        private void NavigateInto(TreeNode node)
        {
            _navigation.NavigateInto(node);
            RefreshView();

            _listView.selectedIndex = 0;
            _listView.ScrollToItem(0);
        }

        private void NavigateBack()
        {
            if (!_navigation.CanNavigateBack) return;

            var previousNode = _navigation.NavigateBack();
            RefreshView();

            var index = _navigation.CurrentItems.IndexOf(previousNode);
            _listView.selectedIndex = index >= 0 ? index : 0;
            _listView.ScrollToItem(_listView.selectedIndex);
        }

        private void SelectNode(TreeNode node)
        {
            _onSelected?.Invoke(node.AssemblyQualifiedName);
            Close();
        }
        #endregion

        private void RefreshView()
        {
            _titleLabel.text = _navigation.GetCurrentTitle();
            _backButton.SetEnabled(_navigation.CanNavigateBack);

            _listView.itemsSource = _navigation.CurrentItems;
            _listView.Rebuild();
        }
    }
}
