using System;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class IdSelectorWindow : EditorWindow
    {
        private const int CreateEntryKey = -1;
        private const string StyleSheetPath = "UI/Ids/Aspid-FastTools-Id-Selector";
        private const string ItemClass = "aspid-fasttools-id-selector__item";
        private const string ItemNameClass = "aspid-fasttools-id-selector__item-name";
        private const string ItemIdClass = "aspid-fasttools-id-selector__item-id";
        private const string ErrorClass = "aspid-fasttools-id-selector__error";

        private Label _errorLabel;
        private ListView _listView;
        private ToolbarSearchField _searchField;

        private Type _idType;
        private string _currentName;
        private Action<string> _onSelected;
        private readonly List<KeyValuePair<int, string>> _filteredEntries = new();

        public static void Show(
            Rect screenRect,
            Type idType,
            string currentName,
            Action<string> onSelected)
        {
            var window = CreateInstance<IdSelectorWindow>();
            window.Initialize(screenRect, idType, currentName, onSelected);
        }

        private void Initialize(
            Rect screenRect,
            Type idType,
            string currentName,
            Action<string> onSelected)
        {
            _idType = idType;
            _currentName = currentName ?? string.Empty;
            _onSelected = onSelected;

            Build();
            RefreshList(string.Empty);

            var size = new Vector2(Mathf.Max(250, screenRect.width), 250);
            ShowAsDropDown(screenRect, size);

            _searchField.Focus();
        }

        private void OnLostFocus() =>
            Close();

        private void Build()
        {
            _searchField = BuildSearchField();

            _listView = new ListView
            {
                selectionType = SelectionType.Single,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                itemsSource = _filteredEntries,
            };

            _listView.SetMakeItem(CreateItem);
            _listView.SetBindItem(BindItem);
            _listView.itemsChosen += items => SelectItem(items.OfType<KeyValuePair<int, string>>().FirstOrDefault());

            _errorLabel = new Label()
                .AddClass(ErrorClass)
                .AddClass(ThemeStyle.LightClass)
                .AddClass(StatusStyle.ErrorClass)
                .SetDisplay(DisplayStyle.None);

            rootVisualElement
                .AddStyleSheetsFromResource(StyleSheetPath)
                .AddStyleSheetsFromResource(AspidStyles.DefaultStyleSheet)
                .AddChild(_searchField)
                .AddChild(_errorLabel)
                .AddChild(_listView);
            
            rootVisualElement.RegisterCallback<KeyDownEvent>(evt =>
            {
                if (evt.keyCode is KeyCode.Escape)
                    Close();
            },TrickleDown.TrickleDown);
            return;

            VisualElement CreateItem() => new VisualElement()
                .AddClass(ItemClass)
                .AddChild(new Label()
                    .AddClass(ItemNameClass)
                    .AddClass(ThemeStyle.LightnessClass))
                .AddChild(new Label()
                    .AddClass(ItemIdClass)
                    .AddClass(ThemeStyle.DarkClass));
        }

        private ToolbarSearchField BuildSearchField()
        {
            var field = new ToolbarSearchField()
                .AddValueChanged(e =>
                {
                    var value = (e.newValue ?? string.Empty).Trim();

                    if (!string.IsNullOrEmpty(value)
                        && !IdRegistryValidator.IsValidName(value, isTaken: null, out var error))
                    {
                        _errorLabel.text = error ?? string.Empty;
                        _errorLabel.SetDisplay(DisplayStyle.Flex);
                    }
                    else
                    {
                        _errorLabel.SetDisplay(DisplayStyle.None);
                    }

                    RefreshList(value);
                });

            field.RegisterCallback<NavigationMoveEvent>(
                callback: e =>
                {
                    if (e.direction is NavigationMoveEvent.Direction.Down)
                        _listView.Focus();
                },
                useTrickleDown: TrickleDown.TrickleDown);

            return field;
        }
        
        private void TryAdd(string nameId)
        {
            nameId = nameId?.Trim();
            if (string.IsNullOrEmpty(nameId)) return;

            var registry = IdRegistryResolver.GetOrCreate(_idType);
            if (!IdRegistryValidator.IsValidName(nameId, registry.Contains, out _)) return;

            Undo.RegisterCompleteObjectUndo(registry, "Add string id");

            var serializedObject = new SerializedObject(registry);
            var idsProp = serializedObject.FindProperty("_ids");
            var namesProp = serializedObject.FindProperty("_names");
            var nextIdProp = serializedObject.FindProperty("_nextId");

            var id = nextIdProp.intValue;
            nextIdProp.intValue = id + 1;

            var newIndex = idsProp.arraySize;
            idsProp.arraySize = newIndex + 1;
            namesProp.arraySize = newIndex + 1;
            idsProp.GetArrayElementAtIndex(newIndex).intValue = id;
            namesProp.GetArrayElementAtIndex(newIndex).stringValue = nameId;

            serializedObject.ApplyModifiedProperties();
            registry.InvalidateCache();
            EditorUtility.SetDirty(registry);
            AssetDatabase.SaveAssetIfDirty(registry);

            _onSelected?.Invoke(nameId);
            Close();
        }

        private void BindItem(VisualElement element, int index)
        {
            if (index < 0 || index >= _filteredEntries.Count) return;

            var entry = _filteredEntries[index];
            var nameLabel = (Label)element[0];
            var idLabel = (Label)element[1];

            if (entry.Key == CreateEntryKey)
            {
                nameLabel.text = $"Create \"{entry.Value}\"";
                nameLabel.style.unityFontStyleAndWeight = FontStyle.Italic;
                idLabel.SetDisplay(DisplayStyle.None);
                return;
            }

            nameLabel.text = entry.Value;
            nameLabel.style.unityFontStyleAndWeight = entry.Value == _currentName
                ? FontStyle.Bold
                : FontStyle.Normal;

            var isNone = entry.Value == Constants.NoneOption;
            idLabel.text = isNone ? string.Empty : entry.Key.ToString();
            idLabel.SetDisplay(isNone ? DisplayStyle.None : DisplayStyle.Flex);
        }

        private void RefreshList(string search)
        {
            _filteredEntries.Clear();
            var registry = IdRegistryResolver.Find(_idType);
            
            var canCreate = !string.IsNullOrEmpty(search) && 
                IdRegistryValidator.IsValidName(
                    input: search,
                    isTaken: registry is not null ? new Func<string, bool>(registry.Contains) : null,
                    error: out _);

            if (canCreate)
                _filteredEntries.Add(new KeyValuePair<int, string>(CreateEntryKey, search));

            _filteredEntries.Add(new KeyValuePair<int, string>(0, Constants.NoneOption));

            if (registry is not null)
            {
                foreach (var entry in registry)
                {
                    if (Matches(entry, search))
                        _filteredEntries.Add(entry);
                }
            }

            _listView.Rebuild();
        }

        private void SelectItem(KeyValuePair<int, string> entry)
        {
            if (entry.Value is null) return;

            if (entry.Key is CreateEntryKey)
            {
                TryAdd(entry.Value);
                return;
            }

            _onSelected?.Invoke(entry.Value == Constants.NoneOption ? string.Empty : entry.Value);
            Close();
        }
        
        private static bool Matches(KeyValuePair<int, string> entry, string search)
        {
            if (string.IsNullOrEmpty(search)) return true;
            if (entry.Value is not null && entry.Value.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) return true;

            return entry.Key.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
