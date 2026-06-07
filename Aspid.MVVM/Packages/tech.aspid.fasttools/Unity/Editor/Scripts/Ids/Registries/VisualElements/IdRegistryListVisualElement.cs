using System;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal sealed class IdRegistryListVisualElement : VisualElement
    {
        private const string UngroupedKey = "<ungrouped>";

        private ListView _flatListView;
        private List<IdRegistryEntryData> _currentItems;
        private RegistryGroupMode _currentGroupMode = RegistryGroupMode.None;

        private Func<string, bool> _getFoldoutExpanded;
        private Action<string, bool> _setFoldoutExpanded;

        public event Action<IdRegistryEntryVisualElement, IdRegistryEntryData> RowNameFocusIn;
        public event Action<IdRegistryEntryVisualElement, IdRegistryEntryData, string> RowNameChanging;
        public event Action<IdRegistryEntryVisualElement, IdRegistryEntryData, string> RowNameCommitRequested;
        public event Action<IdRegistryEntryVisualElement, IdRegistryEntryData> RowDeleteRequested;

        public void Bind(
            List<IdRegistryEntryData> items,
            RegistryGroupMode groupMode,
            Func<string, bool> getFoldoutExpanded,
            Action<string, bool> setFoldoutExpanded)
        {
            _currentItems = items;
            _currentGroupMode = groupMode;
            _getFoldoutExpanded = getFoldoutExpanded;
            _setFoldoutExpanded = setFoldoutExpanded;

            Clear();

            if (groupMode == RegistryGroupMode.None)
            {
                _flatListView ??= BuildEntryListView(BindFlat);
                _flatListView.itemsSource = items;
                Add(_flatListView);
                _flatListView.Rebuild();
                ApplyScrollHeight(_flatListView, items.Count);
            }
            else
            {
                RenderGrouped(items);
            }
        }

        public void ScrollToItem(int index)
        {
            if (_currentGroupMode != RegistryGroupMode.None) return;
            if (_flatListView == null) return;
            if (index < 0 || _currentItems == null || index >= _currentItems.Count) return;

            _flatListView.schedule.Execute(() => _flatListView.ScrollToItem(index)).StartingIn(0);
        }

        private void RenderGrouped(List<IdRegistryEntryData> items)
        {
            var buckets = new Dictionary<string, List<IdRegistryEntryData>>();
            foreach (var view in items)
            {
                var prefix = PrefixOf(view.Name);
                if (!buckets.TryGetValue(prefix, out var list))
                {
                    list = new List<IdRegistryEntryData>();
                    buckets[prefix] = list;
                }
                list.Add(view);
            }

            foreach (var kv in buckets)
            {
                var groupName = kv.Key;
                var groupItems = kv.Value;

                var foldout = new Foldout
                {
                    text = $"{groupName} ({groupItems.Count})",
                    value = _getFoldoutExpanded?.Invoke(groupName) ?? true,
                }.AddClass(Constants.Registry.GroupFoldout);

                foldout.RegisterValueChangedCallback(e =>
                    _setFoldoutExpanded?.Invoke(groupName, e.newValue));

                var bucketList = BuildEntryListView((element, visibleIndex) =>
                    BindBucket(element, visibleIndex, groupItems));
                bucketList.itemsSource = groupItems;
                ApplyScrollHeight(bucketList, groupItems.Count);

                foldout.Add(bucketList);
                Add(foldout);
            }
        }

        private ListView BuildEntryListView(Action<VisualElement, int> binder)
        {
            var list = new ListView
            {
                selectionType = SelectionType.None,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                reorderable = false,
                showBorder = false,
                showFoldoutHeader = false,
                showBoundCollectionSize = false,
                showAddRemoveFooter = false,
            };

            list.AddToClassList(Constants.Registry.List);
            list.SetMakeItem(CreateEntryRow);
            list.SetBindItem(binder);
            return list;
        }

        private VisualElement CreateEntryRow()
        {
            var row = new IdRegistryEntryVisualElement();
            row.NameFocusIn += (r, d) => RowNameFocusIn?.Invoke(r, d);
            row.NameChanging += (r, d, v) => RowNameChanging?.Invoke(r, d, v);
            row.NameCommitRequested += (r, d, v) => RowNameCommitRequested?.Invoke(r, d, v);
            row.DeleteRequested += (r, d) => RowDeleteRequested?.Invoke(r, d);
            return row;
        }

        private void BindFlat(VisualElement element, int visibleIndex)
        {
            if (_currentItems == null) return;
            BindBucket(element, visibleIndex, _currentItems);
        }

        private static void BindBucket(VisualElement element, int visibleIndex, List<IdRegistryEntryData> source)
        {
            if (visibleIndex < 0 || visibleIndex >= source.Count) return;
            ((IdRegistryEntryVisualElement)element).Bind(source[visibleIndex]);
        }

        private static void ApplyScrollHeight(ListView list, int count)
        {
            if (count >= Constants.Registry.ScrollThreshold)
            {
                const float height = Constants.Registry.MaxVisibleRows * Constants.Registry.RowHeight;
                list.style.height = height;
                list.style.maxHeight = height;
            }
            else
            {
                list.style.height = StyleKeyword.Null;
                list.style.maxHeight = StyleKeyword.Null;
            }
        }

        private static string PrefixOf(string name)
        {
            if (string.IsNullOrEmpty(name)) return UngroupedKey;
            var underscore = name.IndexOf('_');
            var dash = name.IndexOf('-');
            var idx = underscore < 0 ? dash
                   : dash < 0 ? underscore
                   : Math.Min(underscore, dash);
            return idx <= 0 ? UngroupedKey : name.Substring(0, idx);
        }
    }
}
