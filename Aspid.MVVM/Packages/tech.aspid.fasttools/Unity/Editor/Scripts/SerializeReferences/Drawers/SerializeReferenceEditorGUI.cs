using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The custom-editor entry point to the SerializeReference dropdown field: draws a <c>[SerializeReference]</c>
    /// property with the package's type-dropdown UI from an editor's own code, no <c>[TypeSelector]</c> attribute
    /// needed. This is how a custom editor offers the same fields Unity's own inspector would need
    /// <c>[TypeSelector]</c> for: <see cref="CreateField"/> / <see cref="CreateList"/> from
    /// <c>CreateInspectorGUI</c>, <see cref="DrawFieldLayout"/> from an IMGUI <c>OnInspectorGUI</c> (lists there:
    /// <see cref="SerializeReferenceIMGUIList.Draw"/>).
    /// </summary>
    /// <example>
    /// <code>
    /// public override VisualElement CreateInspectorGUI()
    /// {
    ///     var root = new VisualElement();
    ///     root.Add(SerializeReferenceEditorGUI.CreateField(serializedObject.FindProperty("_weapon")));
    ///     root.Add(SerializeReferenceEditorGUI.CreateList(serializedObject.FindProperty("_modifiers")));
    ///     return root;
    /// }
    /// </code>
    /// </example>
    public static class SerializeReferenceEditorGUI
    {
        /// <summary>
        /// Builds the dropdown field for a single <c>[SerializeReference]</c> property: a foldout whose header
        /// carries the type dropdown (backed by the hierarchical type picker) and whose content hosts the assigned
        /// instance's fields, with the package's usual notices (missing type, shared reference, mixed selection).
        /// </summary>
        /// <param name="property">A managed-reference property of the editor's <see cref="SerializedObject"/>.</param>
        /// <param name="label">Field label; the property's display name when omitted.</param>
        /// <param name="baseTypes">Optional base types narrowing the picker below the field's declared type,
        /// mirroring the <c>[TypeSelector(...)]</c> arguments.</param>
        /// <exception cref="ArgumentException">The property is not a managed reference.</exception>
        public static VisualElement CreateField(SerializedProperty property, string label = null, params Type[] baseTypes)
        {
            if (property is null)
                throw new ArgumentNullException(nameof(property));

            return property.propertyType is not SerializedPropertyType.ManagedReference
                ? throw new ArgumentException("CreateField expects a [SerializeReference] managed-reference property; for a list/array of them use CreateList.", nameof(property))
                : new SerializeReferenceField(label ?? property.displayName, property, baseTypes);
        }

        /// <summary>
        /// Builds the list for a <c>[SerializeReference]</c> array/list property: every element renders as the
        /// dropdown field and the "+" opens the type picker, appending a fresh typed instance (never a rid-aliased
        /// duplicate of the last element).
        /// </summary>
        /// <param name="property">An array/list property whose elements are managed references.</param>
        /// <param name="label">Header label; the property's display name when omitted.</param>
        /// <param name="baseTypes">Optional base types narrowing the picker below the declared element type,
        /// mirroring the <c>[TypeSelector(...)]</c> arguments.</param>
        /// <exception cref="ArgumentException">The property is not a managed-reference array/list.</exception>
        public static VisualElement CreateList(SerializedProperty property, string label = null, params Type[] baseTypes)
        {
            if (property is null) throw new ArgumentNullException(nameof(property));
            if (!IsManagedReferenceArray(property))
                throw new ArgumentException("CreateList expects an array/list property whose elements are [SerializeReference] managed references.", nameof(property));

            return new SerializeReferenceListField(
                label ?? property.displayName,
                property,
                GetElementType(property),
                baseTypes);
        }

        // SerializedProperty.arrayElementType for a [SerializeReference] array/list — the only array shape whose
        // elements are managed references.
        private const string ManagedReferenceElementPrefix = "managedReference<";

        /// <summary>
        /// True when <paramref name="property"/> is an array/list whose elements are managed references.
        /// </summary>
        private static bool IsManagedReferenceArray(SerializedProperty property) =>
            property.isArray &&
            property.arrayElementType.StartsWith(ManagedReferenceElementPrefix, StringComparison.Ordinal);

        /// <summary>
        /// The declared element type of a managed-reference list/array — what constrains the add-picker on a list
        /// that may currently be empty (a non-empty list's elements resolve their own field type). Read from the
        /// reflected field's array/List&lt;T&gt; shape; falls back to the first element's declared typename, then to
        /// <see cref="object"/>.
        /// </summary>
        private static Type GetElementType(SerializedProperty property)
        {
            if (property.GetFieldInfo() is { } field)
            {
                var elementType = field.FieldType.GetCollectionElementTypeOrSelf();
                if (elementType != field.FieldType) return elementType;
            }

            return property.arraySize > 0
                ? SerializeReferenceHelpers.GetFieldType(property.GetArrayElementAtIndex(0))
                : typeof(object);
        }

        /// <summary>
        /// IMGUI twin of <see cref="CreateField"/> for an <c>OnInspectorGUI</c>-based editor: reserves the layout rect
        /// and draws the same dropdown field into it. Lists have their own IMGUI entry,
        /// <see cref="SerializeReferenceIMGUIList.Draw"/>.
        /// </summary>
        /// <param name="property">A managed-reference property of the editor's <see cref="SerializedObject"/>.</param>
        /// <param name="label">Field label; the property's display name when omitted.</param>
        /// <param name="baseTypes">Optional base types narrowing the picker below the field's declared type,
        /// mirroring the <c>[TypeSelector(...)]</c> arguments.</param>
        /// <exception cref="ArgumentException">The property is not a managed reference.</exception>
        public static void DrawFieldLayout(SerializedProperty property, GUIContent label = null, params Type[] baseTypes)
        {
            if (property is null) throw new ArgumentNullException(nameof(property));
            if (property.propertyType is not SerializedPropertyType.ManagedReference)
                throw new ArgumentException("DrawFieldLayout expects a [SerializeReference] managed-reference property; for a list/array of them use SerializeReferenceIMGUIList.Draw.", nameof(property));

            label ??= new GUIContent(property.displayName);

            var height = SerializeReferenceIMGUIPropertyDrawer.GetHeight(property);
            var rect = EditorGUILayout.GetControlRect(hasLabel: true, height);
            SerializeReferenceIMGUIPropertyDrawer.Draw(rect, label, property, baseTypes);
        }
    }
}
