using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    [CustomEditor(typeof(IdRegistry))]
    internal sealed class StringIdRegistryEditor : Editor
    {
        private SerializedProperty _targetTypeProp;
        private SerializedProperty _entriesProp;

        private Label _countBadge;
        private Label _emptyLabel;

        private void OnEnable()
        {
            _targetTypeProp = serializedObject.FindProperty("_targetStructType");
            _entriesProp    = serializedObject.FindProperty("_entries");
        }

        private void OnDisable()
        {
            if (target == null) return;
            StringIdRegistryValidator.CleanUpInvalid(target);
        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement()
                .AddStyleSheetsFromResource(Constants.StyleSheetPath)
                .AddStyleSheetsFromResource(StyleClasses.DefaultStyleSheet)
                .AddClass(Constants.Registry.Root)
                .AddClass("aspid-fasttools-inspector-container");

            root.Add(new AspidInspectorHeader("None", target) { Subtext = "None" });
            
            var typeContainer = new VisualElement()
                .SetMarginTop(5)
                .AddClass("aspid-fasttools-dark")
                .AddClass("aspid-fasttools-background");

            var container = new VisualElement()
                .SetMarginTop(5)
                .AddClass("aspid-fasttools-dark")
                .AddClass("aspid-fasttools-background");

            typeContainer.Add(new AspidLabel("Type").SetMarginBottom(5));
            typeContainer.Add(new PropertyField(_targetTypeProp, label: string.Empty));
            container.Add(BuildSectionTitle("IDs"));

            var entriesContainer = new VisualElement();
            container.Add(entriesContainer);

            _emptyLabel = new Label("No IDs yet. Add one below.")
                .AddClass(Constants.Registry.Empty);
            container.Add(_emptyLabel);
            container.Add(BuildRegistryAddRow());

            container.TrackSerializedObjectValue(serializedObject, _ => RebuildEntries(entriesContainer));
            RebuildEntries(entriesContainer);

            return root
                .AddChild(typeContainer)
                .AddChild(container);
        }

        private VisualElement BuildSectionTitle(string text)
        {
            _countBadge = new Label("0").AddClass(Constants.Registry.CountBadge);

            var header = new VisualElement()
                .AddClass(Constants.Registry.SectionTitleHeader)
                .AddChild(new Label(text).AddClass(Constants.Registry.SectionTitleText))
                .AddChild(_countBadge);

            return new VisualElement()
                .AddClass(Constants.Registry.SectionTitle)
                .AddChild(header)
                .AddChild(new VisualElement().AddClass(Constants.Registry.SectionTitleLine));
        }

        private void RebuildEntries(VisualElement container)
        {
            container.Clear();
            var duplicates = StringIdRegistryValidator.GetDuplicates(_entriesProp);
            var count      = _entriesProp.arraySize;

            if (_countBadge != null)
                _countBadge.text = count.ToString();

            if (_emptyLabel != null)
                _emptyLabel.EnableInClassList(Constants.Registry.EmptyVisible, count == 0);

            for (int i = 0; i < count; i++)
            {
                var element     = _entriesProp.GetArrayElementAtIndex(i);
                var name        = element.FindPropertyRelative("Name").stringValue;
                var id          = element.FindPropertyRelative("Id").intValue;
                var isDuplicate = duplicates.Contains(name);
                container.Add(BuildRegistryEntryRow(i, name, id, isDuplicate));
            }
        }

        private VisualElement BuildRegistryEntryRow(int index, string name, int id, bool isDuplicate)
        {
            var container = new VisualElement().AddClass(Constants.Registry.Entry);
            container.EnableInClassList(Constants.Registry.EntryDuplicate, isDuplicate);

            var row = new VisualElement().AddClass(Constants.Registry.Row);

            var nameField = new TextField { value = name };
            nameField.AddClass(Constants.Registry.Name);
            row.Add(nameField);

            var idBadge = new Label(id.ToString()).AddClass(Constants.Registry.IdBadge);
            row.Add(idBadge);

            var deleteButton = new Button { text = "×" };
            deleteButton.AddClass(Constants.Registry.Delete);
            row.Add(deleteButton);

            container.Add(row);

            var errorLabel = new Label().AddClass(Constants.Drawer.Error);
            if (isDuplicate)
            {
                errorLabel.text = "Name already exists.";
                errorLabel.SetDisplay(DisplayStyle.Flex);
            }
            container.Add(errorLabel);

            nameField.RegisterCallback<FocusInEvent>(_ =>
            {
                if (StringIdRegistryValidator.HasDuplicate((IdRegistry)target, name))
                {
                    errorLabel.text = "Name already exists.";
                    errorLabel.SetDisplay(DisplayStyle.Flex);
                }
            });

            nameField.RegisterValueChangedCallback(e =>
            {
                var t        = e.newValue?.Trim() ?? string.Empty;
                var registry = (IdRegistry)target;
                if (string.IsNullOrEmpty(t))
                {
                    errorLabel.text = "Name cannot be empty.";
                    errorLabel.SetDisplay(DisplayStyle.Flex);
                }
                else if (StringIdRegistryValidator.HasDuplicate(registry, t) || (t != name && registry.Contains(t)))
                {
                    errorLabel.text = $"'{t}' already exists.";
                    errorLabel.SetDisplay(DisplayStyle.Flex);
                }
                else
                {
                    errorLabel.SetDisplay(DisplayStyle.None);
                }
            });

            nameField.RegisterCallback<FocusOutEvent>(_ =>
            {
                var t        = nameField.value?.Trim() ?? string.Empty;
                var registry = (IdRegistry)target;
                if (!string.IsNullOrEmpty(t) && t != name && !registry.Contains(t))
                {
                    serializedObject.ApplyModifiedProperties();
                    registry.Rename(name, t);
                    EditorUtility.SetDirty(registry);
                    serializedObject.Update();
                    errorLabel.SetDisplay(DisplayStyle.None);
                }
                else
                {
                    nameField.SetValueWithoutNotify(name);
                    if (StringIdRegistryValidator.HasDuplicate(registry, name))
                    {
                        errorLabel.text = "Name already exists.";
                        errorLabel.SetDisplay(DisplayStyle.Flex);
                    }
                    else
                    {
                        errorLabel.SetDisplay(DisplayStyle.None);
                    }
                }
            });

            deleteButton.clicked += () =>
            {
                TryDeleteEntry(index);
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            };

            return container;
        }

        private VisualElement BuildRegistryAddRow()
        {
            var row = new VisualElement().AddClass(Constants.Registry.AddRow);

            var inputField = new TextField();
            inputField.AddClass(Constants.Registry.AddInput);

            var addButton = new Button { text = "Add" };
            addButton.AddClass(Constants.Registry.AddButton);
            addButton.SetEnabled(false);

            inputField.RegisterValueChangedCallback(e =>
            {
                var val      = e.newValue?.Trim() ?? string.Empty;
                var registry = (IdRegistry)target;
                addButton.SetEnabled(!string.IsNullOrEmpty(val) && !registry.Contains(val));
            });

            addButton.clicked += () =>
            {
                var val = inputField.value?.Trim();
                if (string.IsNullOrEmpty(val)) return;
                var registry = (IdRegistry)target;
                serializedObject.ApplyModifiedProperties();
                registry.Add(val);
                EditorUtility.SetDirty(registry);
                serializedObject.Update();
                inputField.SetValueWithoutNotify(string.Empty);
                addButton.SetEnabled(false);
            };

            row.Add(inputField);
            row.Add(addButton);
            return row;
        }

        private void TryDeleteEntry(int index)
        {
            var nameProp     = _entriesProp.GetArrayElementAtIndex(index).FindPropertyRelative("Name");
            var nameToDelete = nameProp.stringValue;
            var usageCount   = StringIdUsageScanner.CountUsages(GetStructType(), nameToDelete);

            var message = usageCount == 0
                ? $"Delete '{nameToDelete}'?"
                : $"'{nameToDelete}' is used in {usageCount} asset(s).\n\nFields referencing this ID will show <Missing> after deletion.\n\nDelete anyway?";

            if (EditorUtility.DisplayDialog("Delete ID", message, "Delete", "Cancel"))
                _entriesProp.DeleteArrayElementAtIndex(index);
        }

        private Type GetStructType()
        {
            var aqn = _targetTypeProp.stringValue;
            return string.IsNullOrEmpty(aqn) ? null : Type.GetType(aqn, throwOnError: false);
        }
    }
}
