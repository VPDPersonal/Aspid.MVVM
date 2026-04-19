#nullable enable
using System;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class StringIdSelectorWindow : EditorWindow
    {
        private ToolbarSearchField _searchField = null!;
        private ListView _listView = null!;
        private Action<string>? _onSelected;
        private string[] _allIds = Array.Empty<string>();
        private readonly List<string> _filteredIds = new();
        private string _current = string.Empty;

        public static void Show(IReadOnlyList<string> ids, Rect screenRect, string current, Action<string> onSelected)
        {
            var window = CreateInstance<StringIdSelectorWindow>();
            window.Initialize(ids, screenRect, current, onSelected);
        }

        private void Initialize(IReadOnlyList<string> ids, Rect screenRect, string current, Action<string> onSelected)
        {
            _onSelected = onSelected;
            _current    = current ?? string.Empty;
            _allIds     = ids.ToArray();

            BuildUI();
            RefreshList(string.Empty);

            var size = new Vector2(Mathf.Max(250, screenRect.width), 250);
            ShowAsDropDown(screenRect, size);

            _searchField.Focus();
        }

        private void BuildUI()
        {
            _searchField = new ToolbarSearchField();
            _searchField.RegisterValueChangedCallback(e => RefreshList(e.newValue ?? string.Empty));
            _searchField.RegisterCallback<NavigationMoveEvent>(e =>
            {
                if (e.direction == NavigationMoveEvent.Direction.Down)
                    _listView.Focus();
            }, TrickleDown.TrickleDown);

            _listView = new ListView
            {
                selectionType        = SelectionType.Single,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                itemsSource          = _filteredIds,
            };

            _listView.SetMakeItem(CreateItem);
            _listView.SetBindItem(BindItem);
            _listView.itemsChosen += items => SelectItem(items.OfType<string>().FirstOrDefault());

            var container = new VisualElement()
                .AddStyleSheetsFromResource(Constants.StyleSheetPath)
                .AddClass(Constants.Selector.Container)
                .AddChild(_searchField)
                .AddChild(_listView);

            rootVisualElement.Add(container);
            rootVisualElement.RegisterCallback<KeyDownEvent>(OnKeyDown, TrickleDown.TrickleDown);
        }

        private VisualElement CreateItem()
        {
            return new Label().AddClass(Constants.Selector.Item);
        }

        private void BindItem(VisualElement element, int index)
        {
            if (index < 0 || index >= _filteredIds.Count) return;
            var label = (Label)element;
            var id    = _filteredIds[index];
            label.text = id;
            label.style.unityFontStyleAndWeight = id == _current ? FontStyle.Bold : FontStyle.Normal;
        }

        private void RefreshList(string search)
        {
            _filteredIds.Clear();
            _filteredIds.Add(Constants.NoneOption);

            foreach (var id in _allIds)
            {
                if (string.IsNullOrEmpty(search) || id.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    _filteredIds.Add(id);
            }

            _listView.Rebuild();
        }

        private void SelectItem(string? item)
        {
            if (item == null) return;
            _onSelected?.Invoke(item == Constants.NoneOption ? string.Empty : item);
            Close();
        }

        private void OnKeyDown(KeyDownEvent e)
        {
            if (e.keyCode == KeyCode.Escape)
                Close();
        }

        private void OnLostFocus() => Close();
    }
}
