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
        private KeyValuePair<int, string>[] _allEntries = Array.Empty<KeyValuePair<int, string>>();
        private readonly List<KeyValuePair<int, string>> _filteredEntries = new();
        private string _current = string.Empty;

        public static void Show(IEnumerable<KeyValuePair<int, string>>? entries, Rect screenRect, string current, Action<string> onSelected)
        {
            var window = CreateInstance<StringIdSelectorWindow>();
            window.Initialize(entries, screenRect, current, onSelected);
        }

        private void Initialize(IEnumerable<KeyValuePair<int, string>>? entries, Rect screenRect, string? current, Action<string> onSelected)
        {
            _onSelected = onSelected;
            _current = current ?? string.Empty;
            _allEntries = entries?.ToArray() ?? Array.Empty<KeyValuePair<int, string>>();

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
                selectionType = SelectionType.Single,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                itemsSource = _filteredEntries,
            };

            _listView.SetMakeItem(CreateItem);
            _listView.SetBindItem(BindItem);
            _listView.itemsChosen += items => SelectItem(items.OfType<KeyValuePair<int, string>>().FirstOrDefault());

            var container = new VisualElement()
                .AddStyleSheetsFromResource(Constants.Selector.StyleSheetPath)
                .AddClass(Constants.Selector.Container)
                .AddChild(_searchField)
                .AddChild(_listView);

            rootVisualElement.Add(container);
            rootVisualElement.RegisterCallback<KeyDownEvent>(OnKeyDown, TrickleDown.TrickleDown);
        }

        private static VisualElement CreateItem()
        {
            var nameLabel = new Label().AddClass(Constants.Selector.ItemName);
            var idLabel = new Label().AddClass(Constants.Selector.ItemId);

            return new VisualElement()
                .AddClass(Constants.Selector.Item)
                .AddChild(nameLabel)
                .AddChild(idLabel);
        }

        private void BindItem(VisualElement element, int index)
        {
            if (index < 0 || index >= _filteredEntries.Count) return;

            var entry = _filteredEntries[index];
            var nameLabel = (Label)element[0];
            var idLabel = (Label)element[1];

            nameLabel.text = entry.Value;
            nameLabel.style.unityFontStyleAndWeight = entry.Value == _current ? FontStyle.Bold : FontStyle.Normal;

            var isNone = entry.Value == Constants.NoneOption;
            idLabel.text = isNone ? string.Empty : entry.Key.ToString();
            idLabel.SetDisplay(isNone ? DisplayStyle.None : DisplayStyle.Flex);
        }

        private void RefreshList(string search)
        {
            _filteredEntries.Clear();
            _filteredEntries.Add(new KeyValuePair<int, string>(0, Constants.NoneOption));

            foreach (var entry in _allEntries)
            {
                if (Matches(entry, search))
                    _filteredEntries.Add(entry);
            }

            _listView.Rebuild();
        }

        private static bool Matches(KeyValuePair<int, string> entry, string search)
        {
            if (string.IsNullOrEmpty(search)) return true;
            if (entry.Value != null && entry.Value.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) return true;
            return entry.Key.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void SelectItem(KeyValuePair<int, string> entry)
        {
            var name = entry.Value;
            if (name == null) return;
            _onSelected?.Invoke(name == Constants.NoneOption ? string.Empty : name);
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
