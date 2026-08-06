using System;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// UIToolkit list for a <c>[SerializeReference]</c> array/list whose field carries no <c>[TypeSelector]</c>: a
    /// bound <see cref="ListView"/> that renders every element as a <see cref="SerializeReferenceField"/> and whose
    /// "+" opens the type picker (appending a fresh typed instance, never a rid-aliased duplicate). With the
    /// attribute, Unity routes each element through the drawer and
    /// <see cref="SerializeReferenceListAddBehavior"/> retrofits the picker onto Unity's own ListView — without it
    /// there is no element drawer to route through, so this field rebuilds that list shape itself. Created by the
    /// public custom-editor facade (<see cref="SerializeReferenceEditorGUI.CreateList"/>).
    /// </summary>
    internal sealed class SerializeReferenceListField : VisualElement
    {
        private const string BlockClass = "aspid-fasttools-serialize-reference-list";

        // Persists the header foldout's expanded state across selection changes, like Unity's own PropertyField list.
        private const string ViewDataKeyPrefix = "aspid-fasttools-serialize-reference-list::";

        private readonly Type[] _baseTypes;
        private readonly SerializedProperty _property;

        public SerializeReferenceListField(string label, SerializedProperty property, Type elementType, Type[] baseTypes = null)
        {
            _property = property;
            _baseTypes = baseTypes;

            this.AddClass(BlockClass);

            var listView = new ListView
            {
                showBorder = true,
                reorderable = true,
                showFoldoutHeader = true,
                headerTitle = label,
                showAddRemoveFooter = true,
                showBoundCollectionSize = true,
                selectionType = SelectionType.Multiple,
                reorderMode = ListViewReorderMode.Animated,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                bindingPath = property.propertyPath,
                viewDataKey = ViewDataKeyPrefix + property.propertyPath,
                makeItem = () => new VisualElement(),
                bindItem = BindItem,
                unbindItem = (element, _) => element.Clear(),
            };

            // Picker-backed "+", single-object only (under a multi-object selection the native add stays and the
            // duplicate guard de-aliases the copies). Set before any attach, so the item fields' install attempt
            // short-circuits on it.
            var serializedObject = property.serializedObject;
            if (!serializedObject.isEditingMultipleObjects)
            {
                var target = serializedObject.targetObject;
                var arrayPath = property.propertyPath;
                listView.overridingAddButtonBehavior = (_, button) =>
                    SerializeReferenceListAddBehavior.OpenAppendPicker(target, arrayPath, elementType, _baseTypes, button);
            }

            this.AddChild(listView);

            // Self-bind: when built dynamically (a nested list inside an already-drawn reference) no ancestor Bind
            // pass will reach it; a second bind of the same path is a harmless no-op.
            listView.Bind(serializedObject);
        }

        private void BindItem(VisualElement element, int index)
        {
            element.Clear();

            var elementProperty = GetElementProperty(index);
            if (elementProperty is null) return;

            element.Add(new SerializeReferenceField(elementProperty.displayName, elementProperty, _baseTypes));
        }

        // Null while the view and the data disagree (a just-removed tail element, a torn-down SerializedObject
        // after an asset repair) — the next binding refresh rebuilds the rows, so a transient miss must only not throw.
        private SerializedProperty GetElementProperty(int index)
        {
            try
            {
                if (_property.serializedObject?.targetObject == null) return null;
                if (index < 0 || index >= _property.arraySize) return null;
                return _property.GetArrayElementAtIndex(index);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
