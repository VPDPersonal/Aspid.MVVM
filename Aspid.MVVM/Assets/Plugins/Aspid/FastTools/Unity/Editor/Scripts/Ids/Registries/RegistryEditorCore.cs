#nullable enable
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    /// <summary>
    /// Builds the shared inspector UI for both Id registry types.
    /// UI-only — all storage access goes through <see cref="IRegistryAccessor"/>.
    /// </summary>
    internal sealed class RegistryEditorCore
    {
        private readonly IRegistryAccessor _accessor;

        private readonly List<EntryView> _viewModel = new();
        private Label? _emptyLabel;
        private ListView? _listView;
        private VisualElement? _listContainer;
        private VisualElement? _warningRow;
        private Label? _warningLabel;
        private TextField? _addInput;
        private Button? _addButton;
        private Label? _addErrorLabel;
        private string _searchQuery = string.Empty;

        private SortMode _sortMode = SortMode.RegistryOrder;
        private GroupMode _groupMode = GroupMode.None;
        private string _assetGuid = string.Empty;

        private string SortKey => $"Aspid.FastTools.Ids.Registry:{_assetGuid}:Sort";
        private string GroupKey => $"Aspid.FastTools.Ids.Registry:{_assetGuid}:Group";
        
        private string GroupExpandedKey(string group) =>
            $"Aspid.FastTools.Ids.Registry:{_assetGuid}:Group:{group}:Expanded";

        public RegistryEditorCore(IRegistryAccessor accessor)
        {
            _accessor = accessor;
        }

        public VisualElement Build()
        {
            var assetPath = AssetDatabase.GetAssetPath(_accessor.Target);
            
            _assetGuid = string.IsNullOrEmpty(assetPath)
                ? _accessor.Target.GetInstanceID().ToString()
                : AssetDatabase.AssetPathToGUID(assetPath);
            _sortMode = (SortMode)SessionState.GetInt(SortKey, (int)SortMode.RegistryOrder);
            _groupMode = (GroupMode)SessionState.GetInt(GroupKey, (int)GroupMode.None);

            var root = new VisualElement()
                .AddStyleSheetsFromResource(AspidStyles.DefaultStyleSheet)
                .AddStyleSheetsFromResource(Constants.Registry.StyleSheetPath)
                .AddClass("aspid-fasttools-inspector-container");

            root.Add(new AspidInspectorHeader(_accessor.Target.name, _accessor.Target)
            {
                Subtext = _accessor.Target.GetType().Name,
            });

            var typeContainer = new AspidBox()
                .SetMarginTop(5);

            typeContainer.Add(new AspidLabel("Type").SetMarginBottom(5));
            typeContainer.Add(new PropertyField(_accessor.TargetStructTypeProperty, label: string.Empty));

            var container = new AspidBox()
                .SetMarginTop(5);

            container.Add(BuildSectionTitle("IDs"));
            container.Add(BuildWarningRow());

            var searchField = new ToolbarSearchField();
            searchField.RegisterValueChangedCallback(e =>
            {
                _searchQuery = e.newValue ?? string.Empty;
                RebuildEntries();
            });
            container.Add(searchField);
            container.Add(BuildSortGroupToolbar());

            _listContainer = new VisualElement();
            container.Add(_listContainer);
            
            container.Add(BuildNextIdRow());
            container.Add(BuildAddRow());

            container.TrackSerializedObjectValue(_accessor.SerializedObject, _ => RebuildEntries());
            RebuildEntries();

            return root
                .AddChild(typeContainer)
                .AddChild(container);
        }

        private static VisualElement BuildSectionTitle(string text) =>
            new AspidLabel(text, new AspidLabelPreset()
                .SetTheme(ThemeStyle.Type.Light)
                .SetLabelSize(AspidLabelSizeStyle.Type.H5)
                .SetLineSize(AspidDividingLineSizeStyle.Type.Medium));

        private void RebuildEntries()
        {
            _viewModel.Clear();
            var count = _accessor.Count;
            
            var duplicates = new HashSet<string>();
            var seen = new HashSet<string>();
            for (var i = 0; i < count; i++)
            {
                var name = _accessor.GetName(i);
                if (!string.IsNullOrEmpty(name) && !seen.Add(name))
                    duplicates.Add(name);
            }

            var query = _searchQuery?.Trim() ?? string.Empty;
            for (var i = 0; i < count; i++)
            {
                var name = _accessor.GetName(i);
                var id = _accessor.GetId(i);
                if (!MatchesQuery(name, id, query)) continue;

                _viewModel.Add(new EntryView(i, name, id, duplicates.Contains(name)));
            }

            ApplySort(_viewModel);

            if (_listContainer != null)
            {
                _listContainer.Clear();
                if (_groupMode == GroupMode.None)
                {
                    _listView ??= BuildListView();
                    _listView.itemsSource = _viewModel;
                    _listContainer.Add(_listView);
                    _listView.Rebuild();
                }
                else
                {
                    RenderGroupedView();
                }
            }

            UpdateListScrollState();
            RefreshWarningRow();
            RevalidateAddRow();
        }

        private ListView BuildListView()
        {
            var list = new ListView
            {
                selectionType = SelectionType.None,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                itemsSource = _viewModel,
                reorderable = false,
                showBorder = false,
                showFoldoutHeader = false,
                showBoundCollectionSize = false,
                showAddRemoveFooter = false,
            };
            list.SetMakeItem(CreateEntryRow);
            list.SetBindItem(BindEntryRow);
            return list;
        }

        private VisualElement BuildSortGroupToolbar()
        {
            var row = new VisualElement().AddClass(Constants.Registry.Toolbar);

            var sort = new EnumField(_sortMode);
            sort.tooltip = "Sort order";
            sort.RegisterValueChangedCallback(e =>
            {
                _sortMode = (SortMode)e.newValue;
                SessionState.SetInt(SortKey, (int)_sortMode);
                RebuildEntries();
            });

            var group = new EnumField(_groupMode);
            group.tooltip = "Group entries by";
            group.RegisterValueChangedCallback(e =>
            {
                _groupMode = (GroupMode)e.newValue;
                SessionState.SetInt(GroupKey, (int)_groupMode);
                RebuildEntries();
            });

            row.Add(sort);
            row.Add(group);
            return row;
        }

        private void ApplySort(List<EntryView> list)
        {
            switch (_sortMode)
            {
                case SortMode.NameAZ:
                    list.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(a.Name, b.Name));
                    break;
                case SortMode.NameZA:
                    list.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(b.Name, a.Name));
                    break;
                case SortMode.IdAsc:
                    list.Sort((a, b) => a.Id.CompareTo(b.Id));
                    break;
                case SortMode.IdDesc:
                    list.Sort((a, b) => b.Id.CompareTo(a.Id));
                    break;
                case SortMode.RegistryOrder:
                default:
                    break;
            }
        }

        private void RenderGroupedView()
        {
            if (_listContainer == null) return;

            var buckets = new Dictionary<string, List<EntryView>>();
            foreach (var view in _viewModel)
            {
                var prefix = PrefixOf(view.Name);
                if (!buckets.TryGetValue(prefix, out var list))
                {
                    list = new List<EntryView>();
                    buckets[prefix] = list;
                }
                list.Add(view);
            }

            foreach (var kv in buckets)
            {
                var groupName = kv.Key;
                var items = kv.Value;

                var foldout = new Foldout
                {
                    text = $"{groupName} ({items.Count})",
                    value = SessionState.GetBool(GroupExpandedKey(groupName), defaultValue: true),
                }.AddClass(Constants.Registry.GroupFoldout);

                foldout.RegisterValueChangedCallback(e =>
                    SessionState.SetBool(GroupExpandedKey(groupName), e.newValue));

                var groupList = new ListView
                {
                    selectionType = SelectionType.None,
                    virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                    itemsSource = items,
                    reorderable = false,
                    showBorder = false,
                    showFoldoutHeader = false,
                    showBoundCollectionSize = false,
                    showAddRemoveFooter = false,
                };
                groupList.SetMakeItem(CreateEntryRow);
                groupList.SetBindItem((element, visibleIndex) =>
                {
                    if (visibleIndex < 0 || visibleIndex >= items.Count) return;
                    var view = items[visibleIndex];
                    ((IdRegistryEntryVisualElement)element).Bind(new IdRegistryEntryData(
                        originalIndex: view.OriginalIndex,
                        name: view.Name,
                        id: view.Id,
                        isDuplicate: view.IsDuplicate));
                });

                if (items.Count >= Constants.Registry.ScrollThreshold)
                {
                    const float height = Constants.Registry.MaxVisibleRows * Constants.Registry.RowHeight;
                    groupList.style.height = height;
                    groupList.style.maxHeight = height;
                }

                foldout.Add(groupList);
                _listContainer.Add(foldout);
            }
        }

        private static string PrefixOf(string name)
        {
            if (string.IsNullOrEmpty(name)) return "<ungrouped>";
            var underscore = name.IndexOf('_');
            var dash = name.IndexOf('-');
            var idx = underscore < 0 ? dash
                   : dash < 0 ? underscore
                   : Math.Min(underscore, dash);
            return idx <= 0 ? "<ungrouped>" : name.Substring(0, idx);
        }

        private void UpdateListScrollState()
        {
            if (_listView == null || _groupMode != GroupMode.None) return;

            if (_viewModel.Count >= Constants.Registry.ScrollThreshold)
            {
                const float height = Constants.Registry.MaxVisibleRows * Constants.Registry.RowHeight;
                _listView.style.height = height;
                _listView.style.maxHeight = height;
            }
            else
            {
                _listView.style.height = StyleKeyword.Null;
                _listView.style.maxHeight = StyleKeyword.Null;
            }
        }

        private enum SortMode { RegistryOrder, NameAZ, NameZA, IdAsc, IdDesc }
        private enum GroupMode { None, ByPrefix }

        private static bool MatchesQuery(string name, int id, string query)
        {
            if (string.IsNullOrEmpty(query)) return true;
            if (!string.IsNullOrEmpty(name) && name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) return true;
            return id.ToString().IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private VisualElement CreateEntryRow()
        {
            var row = new IdRegistryEntryVisualElement();
            row.NameFocusIn += OnRowNameFocusIn;
            row.NameChanging += OnRowNameChanging;
            row.NameCommitRequested += OnRowNameCommitRequested;
            row.DeleteRequested += OnRowDeleteRequested;
            return row;
        }

        private void BindEntryRow(VisualElement element, int visibleIndex)
        {
            if (visibleIndex < 0 || visibleIndex >= _viewModel.Count) return;
            var view = _viewModel[visibleIndex];
            ((IdRegistryEntryVisualElement)element).Bind(new IdRegistryEntryData(
                originalIndex: view.OriginalIndex,
                name: view.Name,
                id: view.Id,
                isDuplicate: view.IsDuplicate));
        }

        private void OnRowNameFocusIn(IdRegistryEntryVisualElement row, IdRegistryEntryData data)
        {
            if (data.IsDuplicate)
                row.SetError("Name already exists.");
        }

        private void OnRowNameChanging(IdRegistryEntryVisualElement row, IdRegistryEntryData data, string newValue)
        {
            var trimmed = newValue?.Trim() ?? string.Empty;

            if (trimmed == data.Name)
            {
                row.SetEditMode(false);
                row.ClearError();
                return;
            }

            var existing = CollectExistingNames(exceptIndex: data.OriginalIndex);
            if (IdRegistryValidator.IsValidName(trimmed, existing, out var error))
            {
                row.SetEditMode(true, canConfirm: true);
                row.ClearError();
            }
            else
            {
                row.SetEditMode(true, canConfirm: false);
                row.SetError(error!);
            }
        }

        private void OnRowNameCommitRequested(IdRegistryEntryVisualElement row, IdRegistryEntryData data, string rawValue)
        {
            var trimmed = rawValue?.Trim() ?? string.Empty;
            if (trimmed == data.Name || string.IsNullOrEmpty(trimmed)) return;

            var existing = CollectExistingNames(exceptIndex: data.OriginalIndex);
            if (!IdRegistryValidator.IsValidName(trimmed, existing, out _)) return;

            _accessor.Record($"Rename ID '{data.Name}' → '{trimmed}'");
            _accessor.SetName(data.OriginalIndex, trimmed);
            _accessor.Commit();
            row.SetEditMode(false);
            row.ClearError();
        }

        private void OnRowDeleteRequested(IdRegistryEntryVisualElement row, IdRegistryEntryData data)
        {
            var name = data.Name;
            if (!EditorUtility.DisplayDialog(
                    "Delete ID",
                    $"Delete '{name}'?\n\nAssets referencing this ID will display <Missing> until reassigned.",
                    "Delete",
                    "Cancel"))
                return;

            _accessor.Record($"Delete ID '{name}'");
            _accessor.RemoveAt(data.OriginalIndex);
            _accessor.Commit();
        }

        private VisualElement BuildNextIdRow()
        {
            var nextIdElement = new VisualElement()
                .AddClass(Constants.Registry.NextId);
            
            var row = new VisualElement();

            var fieldNextId = new PropertyField(_accessor.NextIdProperty)
                .SetTooltip("Id that will be assigned to the next Add operation. Manual override is allowed.");

            var warning = new Image
            {
                image = EditorGUIUtility.IconContent("console.warnicon.sml").image,
                tooltip = string.Empty,
            };

            fieldNextId.RegisterValueChangeCallback(e =>
            {
                var newValue = e.changedProperty.intValue;
                UpdateNextIdWarning(warning, newValue);

                _accessor.Record("Set Next ID");
                _accessor.Commit();
                RevalidateAddRow();
            });

            UpdateNextIdWarning(warning, _accessor.NextIdProperty.intValue);
            
            row.Add(fieldNextId);
            row.Add(warning);
            
            return nextIdElement.AddChild(row);
        }

        private void UpdateNextIdWarning(Image warning, int value)
        {
            var maxAssigned = _accessor.MaxAssignedId;
            var show = value <= maxAssigned && value >= 1;
            warning.SetEnabled(show);
            warning.tooltip = show
                ? $"Reusing ID {value} may silently remap references: assets that previously pointed to this ID will appear bound to the next name you create. Proceed only if you know these IDs are unused."
                : value < 1
                    ? "Next ID must be ≥ 1."
                    : string.Empty;
        }

        private bool NextIdCollides(int nextId)
        {
            if (nextId < 1) return false;
            var count = _accessor.Count;
            for (var i = 0; i < count; i++)
                if (_accessor.GetId(i) == nextId) return true;
            return false;
        }

        private void RevalidateAddRow()
        {
            if (_addInput == null || _addButton == null || _addErrorLabel == null) return;

            var val = _addInput.value?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(val))
            {
                _addButton.SetEnabled(false);
                _addErrorLabel.SetDisplay(DisplayStyle.None);
                return;
            }

            var existing = CollectExistingNames(exceptIndex: -1);
            if (!IdRegistryValidator.IsValidName(val, existing, out var err))
            {
                _addButton.SetEnabled(false);
                _addErrorLabel.text = err ?? string.Empty;
                _addErrorLabel.SetDisplay(DisplayStyle.Flex);
                return;
            }

            var nextId = _accessor.NextIdProperty.intValue;
            if (nextId < 1)
            {
                _addButton.SetEnabled(false);
                _addErrorLabel.text = "Next ID must be ≥ 1.";
                _addErrorLabel.SetDisplay(DisplayStyle.Flex);
                return;
            }

            if (NextIdCollides(nextId))
            {
                _addButton.SetEnabled(false);
                _addErrorLabel.text = $"Next ID {nextId} is already used — change it before adding.";
                _addErrorLabel.SetDisplay(DisplayStyle.Flex);
                return;
            }

            _addErrorLabel.SetDisplay(DisplayStyle.None);
            _addButton.SetEnabled(true);
        }

        private VisualElement BuildWarningRow()
        {
            var row = new VisualElement().AddClass(Constants.Registry.Warning);
            _warningRow = row;

            _warningLabel = new Label().AddClass(Constants.Registry.WarningLabel);
            var reviewButton = new Button { text = "Review" }.AddClass(Constants.Registry.WarningButton);
            reviewButton.clicked += ShowCleanUpDialog;

            row.Add(_warningLabel);
            row.Add(reviewButton);
            return row;
        }

        private void RefreshWarningRow()
        {
            if (_warningRow == null || _warningLabel == null) return;
            var summary = IdRegistryValidator.Summarize(_accessor);
            var visible = summary.Total > 0;
            _warningRow.EnableInClassList(Constants.Registry.WarningVisible, visible);
            if (visible) _warningLabel.text = summary.ToShortLabel();
        }

        private void ShowCleanUpDialog()
        {
            var summary = IdRegistryValidator.Summarize(_accessor);
            if (summary.Total == 0) return;

            var message = $"This will remove {summary.Total} invalid entr{(summary.Total == 1 ? "y" : "ies")}:\n"
                        + (summary.DuplicateCount > 0 ? $"  • {summary.DuplicateCount} duplicate name(s)\n" : string.Empty)
                        + (summary.EmptyCount > 0 ? $"  • {summary.EmptyCount} empty name(s)\n" : string.Empty)
                        + (summary.StructuralIssues > 0 ? "  • structural inconsistencies\n" : string.Empty)
                        + "\nProceed?";

            if (!EditorUtility.DisplayDialog("Clean up invalid entries", message, "Clean up", "Cancel"))
                return;

            _accessor.Record("Clean Up Invalid IDs");

            var seen = new HashSet<string>();
            var toRemove = new List<int>();
            for (var i = 0; i < _accessor.Count; i++)
            {
                var name = _accessor.GetName(i);
                if (string.IsNullOrEmpty(name) || !seen.Add(name))
                    toRemove.Add(i);
            }

            for (var i = toRemove.Count - 1; i >= 0; i--)
                _accessor.RemoveAt(toRemove[i]);

            _accessor.Commit();
        }

        private VisualElement BuildAddRow()
        {
            var wrapper = new VisualElement()
                .AddClass(Constants.Registry.Add);;

            var row = new VisualElement();
            _addInput = new TextField();

            _addButton = new Button { text = "+" };
            _addButton.SetEnabled(false);

            _addErrorLabel = new Label()
                .SetDisplay(DisplayStyle.None);

            _addInput.RegisterValueChangedCallback(_ => RevalidateAddRow());

            _addButton.clicked += () =>
            {
                var val = _addInput.value?.Trim();
                if (string.IsNullOrEmpty(val)) return;

                _accessor.Record($"Add ID '{val}'");
                _accessor.Add(val);
                _accessor.Commit();

                _addInput.SetValueWithoutNotify(string.Empty);
                RevalidateAddRow();

                RebuildEntries();
                var newIndex = _viewModel.FindIndex(v => v.Name == val);
                if (newIndex < 0 || _listView == null || _groupMode != GroupMode.None) return;
                _listView.schedule.Execute(() => _listView.ScrollToItem(newIndex)).StartingIn(0);
            };

            row.Add(_addInput);
            row.Add(_addButton);
            wrapper.Add(row);
            wrapper.Add(_addErrorLabel);
            return wrapper;
        }

        private HashSet<string> CollectExistingNames(int exceptIndex)
        {
            var set = new HashSet<string>();
            var count = _accessor.Count;
            for (var i = 0; i < count; i++)
            {
                if (i == exceptIndex) continue;
                var name = _accessor.GetName(i);
                if (!string.IsNullOrEmpty(name))
                    set.Add(name);
            }
            return set;
        }

        private readonly struct EntryView
        {
            public readonly int OriginalIndex;
            public readonly string Name;
            public readonly int Id;
            public readonly bool IsDuplicate;

            public EntryView(int originalIndex, string name, int id, bool isDuplicate)
            {
                OriginalIndex = originalIndex;
                Name = name;
                Id = id;
                IsDuplicate = isDuplicate;
            }
        }
    }
}
