using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    /// <summary>
    /// Builds the inspector UI for <see cref="IdRegistry"/>. Acts as the sole orchestrator:
    /// owns the <see cref="SerializedObject"/>, the view-model, and the mutation cycle
    /// (<see cref="Record"/> → SerializedProperty edits → <see cref="Commit"/>).
    /// UI is split into dedicated <c>IdRegistry*VisualElement</c> components that emit
    /// events; this class wires them and performs all <see cref="IdRegistry"/> mutations.
    /// </summary>
    internal sealed class RegistryEditorCore
    {
        private readonly IdRegistry _registry;
        private readonly SerializedObject _serializedObject;
        private readonly SerializedProperty _idsProp;
        private readonly SerializedProperty _namesProp;
        private readonly SerializedProperty _nextIdProp;
        private readonly SerializedProperty _targetStructTypeProp;

        private readonly List<IdRegistryEntryData> _viewModel = new();

        private IdRegistryListVisualElement _list;
        private IdRegistryToolbarVisualElement _toolbar;
        private IdRegistryAddRowVisualElement _addRow;
        private IdRegistryNextIdRowVisualElement _nextIdRow;
        private IdRegistryWarningVisualElement _warning;

        private string _assetGuid = string.Empty;
        private string _searchQuery = string.Empty;
        private RegistryGroupMode _groupMode = RegistryGroupMode.None;
        private RegistrySortMode _sortMode = RegistrySortMode.RegistryOrder;

        // Invalidated to null inside RebuildEntries; rebuilt lazily by EnsureNameOccurrencesCache.
        private Dictionary<string, int> _nameOccurrencesCache;

        public RegistryEditorCore(IdRegistry registry)
        {
            _registry = registry;
            _serializedObject = new SerializedObject(registry);
            _idsProp = _serializedObject.FindProperty("_ids");
            _namesProp = _serializedObject.FindProperty("_names");
            _nextIdProp = _serializedObject.FindProperty("_nextId");
            _targetStructTypeProp = _serializedObject.FindProperty("_targetStructType");
        }

        public VisualElement Build()
        {
            var assetPath = AssetDatabase.GetAssetPath(_registry);
            _assetGuid = string.IsNullOrEmpty(assetPath)
                ? _registry.GetInstanceID().ToString()
                : AssetDatabase.AssetPathToGUID(assetPath);

            _sortMode = (RegistrySortMode)SessionState.GetInt(SortKey, (int)RegistrySortMode.RegistryOrder);
            _groupMode = (RegistryGroupMode)SessionState.GetInt(GroupKey, (int)RegistryGroupMode.None);

            var root = new VisualElement()
                .AddStyleSheetsFromResource(AspidStyles.DefaultStyleSheet)
                .AddStyleSheetsFromResource(Constants.Registry.StyleSheetPath)
                .AddClass("aspid-fasttools-inspector-container");

            root.Add(new AspidInspectorHeader(_registry.name + " (Beta)", _registry)
            {
                Subtext = _registry.GetType().Name,
            });

            var typeContainer = new AspidBox()
                .SetMarginTop(5);

            typeContainer.AddChild(new AspidLabel("Type").SetMarginBottom(5));
            typeContainer.AddChild(new PropertyField(_targetStructTypeProp, label: string.Empty));

            var container = new AspidBox()
                .SetMarginTop(5);

            container.AddChild(BuildSectionTitle("IDs"));

            _warning = new IdRegistryWarningVisualElement();
            _warning.ReviewRequested += ShowCleanUpDialog;
            container.AddChild(_warning);

            var searchField = new ToolbarSearchField();
            searchField.RegisterValueChangedCallback(e =>
            {
                _searchQuery = e.newValue ?? string.Empty;
                RebuildEntries();
            });
            container.AddChild(searchField);

            _toolbar = new IdRegistryToolbarVisualElement(_sortMode, _groupMode);
            _toolbar.SortChanged += OnSortChanged;
            _toolbar.GroupChanged += OnGroupChanged;
            container.AddChild(_toolbar);

            _list = new IdRegistryListVisualElement();
            _list.RowNameFocusIn += OnRowNameFocusIn;
            _list.RowNameChanging += OnRowNameChanging;
            _list.RowNameCommitRequested += OnRowNameCommitRequested;
            _list.RowDeleteRequested += OnRowDeleteRequested;
            container.AddChild(_list);

            _nextIdRow = new IdRegistryNextIdRowVisualElement(_nextIdProp, ResolveNextIdWarning);
            _nextIdRow.ValueChanged += OnNextIdValueChanged;
            container.AddChild(_nextIdRow);

            _addRow = new IdRegistryAddRowVisualElement(ValidateAddRow);
            _addRow.AddRequested += OnAddRowAddRequested;
            container.AddChild(_addRow);
            
            container.TrackSerializedObjectValue(_serializedObject, _ => RebuildEntries());
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

        // -------- view-model --------

        private void RebuildEntries()
        {
            _viewModel.Clear();
            _nameOccurrencesCache = null;
            var count = Count;

            var duplicates = new HashSet<string>();
            var seen = new HashSet<string>();
            for (var i = 0; i < count; i++)
            {
                var name = GetName(i);
                if (!string.IsNullOrEmpty(name) && !seen.Add(name))
                    duplicates.Add(name);
            }

            var query = _searchQuery?.Trim() ?? string.Empty;
            for (var i = 0; i < count; i++)
            {
                var name = GetName(i);
                var id = GetId(i);
                if (!MatchesQuery(name, id, query)) continue;

                _viewModel.Add(new IdRegistryEntryData(i, name, id, duplicates.Contains(name)));
            }

            ApplySort(_viewModel);

            _list?.Bind(_viewModel, _groupMode, GetFoldoutExpanded, SetFoldoutExpanded);
            _warning?.Bind(IdRegistryValidator.Summarize(Count, GetName));
            _addRow?.Revalidate();
        }

        private void ApplySort(List<IdRegistryEntryData> list)
        {
            switch (_sortMode)
            {
                case RegistrySortMode.NameAscending:
                    list.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(a.Name, b.Name));
                    break;
                case RegistrySortMode.NameDescending:
                    list.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(b.Name, a.Name));
                    break;
                case RegistrySortMode.IdAscending:
                    list.Sort((a, b) => a.Id.CompareTo(b.Id));
                    break;
                case RegistrySortMode.IdDescending:
                    list.Sort((a, b) => b.Id.CompareTo(a.Id));
                    break;
                case RegistrySortMode.RegistryOrder:
                default:
                    break;
            }
        }

        private static bool MatchesQuery(string name, int id, string query)
        {
            if (string.IsNullOrEmpty(query)) return true;
            if (!string.IsNullOrEmpty(name) && name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) return true;
            return id.ToString().IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private Func<string, bool> IsTakenExcluding()
        {
            var occurrences = EnsureNameOccurrencesCache();
            return name => occurrences.ContainsKey(name);
        }

        private Func<string, bool> IsTakenExcluding(int exceptIndex)
        {
            var occurrences = EnsureNameOccurrencesCache();

            string exceptName = null;
            if (exceptIndex >= 0 && exceptIndex < Count)
            {
                var n = GetName(exceptIndex);
                if (!string.IsNullOrEmpty(n)) exceptName = n;
            }

            return name =>
            {
                if (!occurrences.TryGetValue(name, out var count)) return false;
                if (exceptName != null && exceptName == name) return count > 1;
                return true;
            };
        }

        private Dictionary<string, int> EnsureNameOccurrencesCache()
        {
            if (_nameOccurrencesCache != null)
                return _nameOccurrencesCache;

            var count = Count;
            var cache = new Dictionary<string, int>();

            for (var i = 0; i < count; i++)
            {
                var name = GetName(i);
                if (string.IsNullOrEmpty(name)) continue;

                cache.TryGetValue(name, out var c);
                cache[name] = c + 1;
            }

            return _nameOccurrencesCache = cache;
        }

        // -------- component handlers --------

        private void OnSortChanged(RegistrySortMode mode)
        {
            _sortMode = mode;
            SessionState.SetInt(SortKey, (int)mode);
            RebuildEntries();
        }

        private void OnGroupChanged(RegistryGroupMode mode)
        {
            _groupMode = mode;
            SessionState.SetInt(GroupKey, (int)mode);
            RebuildEntries();
        }

        private void OnNextIdValueChanged(int _)
        {
            _registry.InvalidateCache();
            _addRow?.Revalidate();
        }

        private void OnAddRowAddRequested(string name)
        {
            Record($"Add ID '{name}'");
            Add(name);
            Commit();

            _addRow.Reset();
            RebuildEntries();

            var newIndex = _viewModel.FindIndex(v => v.Name == name);
            if (newIndex >= 0) _list.ScrollToItem(newIndex);
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

            if (IdRegistryValidator.IsValidName(trimmed, IsTakenExcluding(data.OriginalIndex), out var error))
            {
                row.SetEditMode(true, canConfirm: true);
                row.ClearError();
            }
            else
            {
                row.SetEditMode(true, canConfirm: false);
                row.SetError(error);
            }
        }

        private void OnRowNameCommitRequested(IdRegistryEntryVisualElement row, IdRegistryEntryData data, string rawValue)
        {
            var trimmed = rawValue?.Trim() ?? string.Empty;
            if (trimmed == data.Name || string.IsNullOrEmpty(trimmed)) return;

            if (!IdRegistryValidator.IsValidName(trimmed, IsTakenExcluding(data.OriginalIndex), out _)) return;

            Record($"Rename ID '{data.Name}' → '{trimmed}'");
            SetName(data.OriginalIndex, trimmed);
            Commit();
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

            Record($"Delete ID '{name}'");
            RemoveAt(data.OriginalIndex);
            Commit();
        }
        
        private AddRowValidation ValidateAddRow(string trimmed)
        {
            if (!IdRegistryValidator.IsValidName(trimmed, IsTakenExcluding(), out var err))
                return AddRowValidation.Invalid(err ?? string.Empty);

            var nextId = _nextIdProp.intValue;
            if (nextId < 1)
                return AddRowValidation.Invalid("Next ID must be ≥ 1.");

            return NextIdCollides(nextId)
                ? AddRowValidation.Invalid($"Next ID {nextId} is already used — change it before adding.")
                : AddRowValidation.Valid();

        }

        private NextIdWarning ResolveNextIdWarning(int value)
        {
            var maxAssigned = MaxAssignedId;
            var show = value <= maxAssigned && value >= 1;

            if (show)
                return NextIdWarning.Visible(
                    $"Reusing ID {value} may silently remap references: assets that previously pointed to this ID will appear bound to the next name you create. Proceed only if you know these IDs are unused.");

            return value < 1
                ? NextIdWarning.Hidden("Next ID must be ≥ 1.")
                : NextIdWarning.Hidden();
        }

        private bool NextIdCollides(int nextId)
        {
            if (nextId < 1) return false;
            var count = Count;
            for (var i = 0; i < count; i++)
                if (GetId(i) == nextId) return true;
            return false;
        }

        // -------- clean-up flow --------

        private void ShowCleanUpDialog()
        {
            var summary = IdRegistryValidator.Summarize(Count, GetName);
            if (summary.Total == 0) return;

            var message = $"This will remove {summary.Total} invalid entr{(summary.Total == 1 ? "y" : "ies")}:\n"
                        + (summary.DuplicateCount > 0 ? $"  • {summary.DuplicateCount} duplicate name(s)\n" : string.Empty)
                        + (summary.EmptyCount > 0 ? $"  • {summary.EmptyCount} empty name(s)\n" : string.Empty)
                        + "\nProceed?";

            if (!EditorUtility.DisplayDialog("Clean up invalid entries", message, "Clean up", "Cancel"))
                return;

            Record("Clean Up Invalid IDs");

            // Single source of truth for invalidity criteria — see EnumerateInvalidIndices.
            var toRemove = new List<int>(EnumerateInvalidIndices());
            for (var i = toRemove.Count - 1; i >= 0; i--)
                RemoveAt(toRemove[i]);

            Commit();
        }

        private IEnumerable<int> EnumerateInvalidIndices()
        {
            var count = Count;
            var seen = new HashSet<string>();

            for (var i = 0; i < count; i++)
            {
                var name = GetName(i);

                if (string.IsNullOrEmpty(name))
                {
                    yield return i;
                    continue;
                }

                if (!seen.Add(name))
                    yield return i;
            }
        }

        // -------- SessionState keys --------

        private string SortKey => $"Aspid.FastTools.Ids.Registry:{_assetGuid}:Sort";
        private string GroupKey => $"Aspid.FastTools.Ids.Registry:{_assetGuid}:Group";
        private string GroupExpandedKey(string group) =>
            $"Aspid.FastTools.Ids.Registry:{_assetGuid}:Group:{group}:Expanded";

        private bool GetFoldoutExpanded(string group) =>
            SessionState.GetBool(GroupExpandedKey(group), defaultValue: true);

        private void SetFoldoutExpanded(string group, bool expanded) =>
            SessionState.SetBool(GroupExpandedKey(group), expanded);

        // -------- storage / mutation cycle --------
        // Every mutation goes Record → SerializedProperty edits → Commit.
        // Skipping Record breaks Undo. Skipping Commit leaves the runtime cache stale.

        private int Count => _idsProp.arraySize;

        private int MaxAssignedId
        {
            get
            {
                var max = 0;
                var count = Count;
                for (var i = 0; i < count; i++)
                {
                    var id = GetId(i);
                    if (id > max) max = id;
                }
                return max;
            }
        }

        private int GetId(int index) =>
            _idsProp.GetArrayElementAtIndex(index).intValue;

        private string GetName(int index) =>
            _namesProp.GetArrayElementAtIndex(index).stringValue;

        private void Add(string name)
        {
            var id = _nextIdProp.intValue;
            _nextIdProp.intValue = id + 1;

            var newIndex = _idsProp.arraySize;

            _idsProp.SetArraySize(newIndex + 1);
            _namesProp.SetArraySize(newIndex + 1);

            _idsProp.GetArrayElementAtIndex(newIndex).intValue = id;
            _namesProp.GetArrayElementAtIndex(newIndex).stringValue = name;
        }

        private void SetName(int index, string name) =>
            _namesProp.GetArrayElementAtIndex(index).stringValue = name;

        private void RemoveAt(int index)
        {
            _idsProp.DeleteArrayElementAtIndex(index);
            _namesProp.DeleteArrayElementAtIndex(index);
        }

        private void Record(string operationName) =>
            Undo.RegisterCompleteObjectUndo(_registry, operationName);

        private void Commit()
        {
            _serializedObject.ApplyModifiedProperties();
            _registry.InvalidateCache();

            EditorUtility.SetDirty(_registry);
        }
    }
}
