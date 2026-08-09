using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.SerializeReferences.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    [CustomPropertyDrawer(typeof(TypeSelectorAttribute))]
    internal sealed class TypeSelectorPropertyDrawer : PropertyDrawer
    {
        private IReadOnlyList<string> _constraintWarnings;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var warnings = GetConstraintWarnings(property);
            var noticeHeight = GetConstraintNoticeHeight(warnings);

            var fieldRect = position;
            fieldRect.height = position.height - noticeHeight;
            DrawField(fieldRect, property, label);

            if (noticeHeight <= 0f) return;

            var noticeRect = new Rect(position.x, fieldRect.yMax + EditorGUIUtility.standardVerticalSpacing,
                position.width, EditorGUIUtility.singleLineHeight);
            SerializeReferenceIMGUIPropertyDrawer.DrawRequiredNotice(noticeRect, GetNoticeMessage(warnings), GetNoticeDetail(warnings));
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            GetFieldHeight(property) + GetConstraintNoticeHeight(GetConstraintWarnings(property));

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var field = CreateField(property, out var applyResolvedTypes);

            // Without string arguments the constraint is static — nothing to re-resolve, no warnings possible.
            var typeSelectorAttribute = (TypeSelectorAttribute)attribute;
            if (typeSelectorAttribute.AssemblyQualifiedNames.Length is 0) return field;

            var container = new VisualElement().AddChild(field);
            var notice = new SerializeReferenceNotice();

            // A string argument may reference a member of the target object whose value changes while the
            // inspector is open; every change to the object re-resolves the constraint and pushes the fresh
            // base types and warnings into the field (the IMGUI path re-resolves per OnGUI instead).
            container.TrackSerializedObjectValue(property.serializedObject, OnSerializedObjectChanged);
            UpdateNotice();

            return container;

            void OnSerializedObjectChanged(SerializedObject serializedObject)
            {
                if (serializedObject.targetObject == null) return;

                applyResolvedTypes();
                UpdateNotice();
            }

            void UpdateNotice()
            {
                var warnings = _constraintWarnings;
                if (warnings.Count == 0)
                {
                    notice.RemoveFromHierarchy();
                    return;
                }

                notice.Set(
                    message: GetNoticeMessage(warnings),
                    actionText: string.Empty,
                    detail: GetNoticeDetail(warnings),
                    onAction: null);

                if (notice.parent is null) container.AddChild(notice);
            }
        }

        private void DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            if (TryGetSerializableTypeContainer(property, out var nameProperty, out var genericBaseType))
            {
                TypeIMGUIPropertyDrawer.Draw(
                    position: position,
                    property: nameProperty,
                    label: label,
                    allow: GetTypeAllow(),
                    types: GetSerializableTypeBaseTypes(property, genericBaseType));
                return;
            }

            ThrowExceptionIfInvalidProperty(property);

            if (property.propertyType == SerializedPropertyType.ManagedReference)
            {
                SerializeReferenceIMGUIPropertyDrawer.Draw(
                    position: position,
                    label: label,
                    property: property,
                    baseTypes: GetTypesFromAttribute(property));
                return;
            }

            TypeIMGUIPropertyDrawer.Draw(
                position: position,
                property: property,
                label: label,
                allow: GetTypeAllow(),
                types: GetTypesFromAttribute(property));
        }

        // applyResolvedTypes re-resolves the constraint and pushes the result into the created field,
        // so member-referenced base types stay live while the inspector is open (see CreatePropertyGUI).
        private VisualElement CreateField(SerializedProperty property, out Action applyResolvedTypes)
        {
            if (TryGetSerializableTypeContainer(property, out var nameProperty, out var genericBaseType))
            {
                var element = TypeUIToolkitPropertyDrawer.Draw(
                    label: preferredLabel,
                    property: nameProperty,
                    allow: GetTypeAllow(),
                    types: GetSerializableTypeBaseTypes(property, genericBaseType),
                    field: out var containerTypeField);

                applyResolvedTypes = () => containerTypeField.Types = GetSerializableTypeBaseTypes(property, genericBaseType);
                return element;
            }

            ThrowExceptionIfInvalidProperty(property);

            if (property.propertyType == SerializedPropertyType.ManagedReference)
            {
                var element = SerializeReferenceUIToolkitPropertyDrawer.Draw(
                    label: preferredLabel,
                    property: property,
                    baseTypes: GetTypesFromAttribute(property),
                    field: out var referenceField);

                applyResolvedTypes = () => referenceField.SetBaseTypes(GetTypesFromAttribute(property));
                return element;
            }

            var stringElement = TypeUIToolkitPropertyDrawer.Draw(
                label: preferredLabel,
                property: property,
                allow: GetTypeAllow(),
                types: GetTypesFromAttribute(property),
                field: out var stringTypeField);

            applyResolvedTypes = () => stringTypeField.Types = GetTypesFromAttribute(property);
            return stringElement;
        }

        private float GetFieldHeight(SerializedProperty property)
        {
            if (TryGetSerializableTypeContainer(property, out var nameProperty, out _))
                return TypeIMGUIPropertyDrawer.GetHeight(nameProperty);

            return property.propertyType == SerializedPropertyType.ManagedReference
                ? SerializeReferenceIMGUIPropertyDrawer.GetHeight(property)
                : TypeIMGUIPropertyDrawer.GetHeight(property);
        }

        private static float GetConstraintNoticeHeight(IReadOnlyList<string> warnings) =>
            warnings.Count > 0
                ? EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight
                : 0f;

        private static string GetNoticeMessage(IReadOnlyList<string> warnings) =>
            warnings.Count == 1
                ? "TypeSelector constraint could not be resolved"
                : $"{warnings.Count} TypeSelector constraints could not be resolved";

        private static string GetNoticeDetail(IReadOnlyList<string> warnings) => string.Join("\n", warnings);

        private TypeAllow GetTypeAllow()
        {
            var typeSelectorAttribute = (TypeSelectorAttribute)attribute;
            return typeSelectorAttribute.Allow;
        }

        private bool TryGetSerializableTypeContainer(SerializedProperty property, out SerializedProperty nameProperty, out Type genericBaseType)
        {
            nameProperty = null;
            genericBaseType = null;

            if (property.propertyType is not SerializedPropertyType.Generic) return false;
            if (fieldInfo is null) return false;

            if (!SerializableTypeUtility.TryGetBaseType(fieldInfo.FieldType, out var baseType)) return false;

            genericBaseType = baseType == typeof(object) ? null : baseType;
            nameProperty = property.FindPropertyRelative("_assemblyQualifiedName");
            return nameProperty is not null;
        }

        private Type[] GetSerializableTypeBaseTypes(SerializedProperty property, Type genericBaseType)
        {
            var attributeTypes = GetTypesFromAttribute(property);
            if (genericBaseType is null) return attributeTypes;

            var types = new List<Type>(attributeTypes.Length + 1) { genericBaseType };
            types.AddRange(attributeTypes);
            return types.ToArray();
        }

        private Type[] GetTypesFromAttribute(SerializedProperty property)
        {
            var typeSelectorAttribute = (TypeSelectorAttribute)attribute;

            if (typeSelectorAttribute.AssemblyQualifiedNames.Length is 0)
            {
                _constraintWarnings = Array.Empty<string>();
                return Array.Empty<Type>();
            }

            var resolution = TypeSelectorConstraintResolver.Resolve(
                property.serializedObject.targetObject, typeSelectorAttribute.AssemblyQualifiedNames);

            // Overwrite (never ??=): a member-referenced constraint re-resolves while the inspector is
            // open, and the warnings must follow the latest resolution rather than freeze on the first.
            _constraintWarnings = resolution.Warnings;
            return resolution.Types;
        }

        private IReadOnlyList<string> GetConstraintWarnings(SerializedProperty property)
        {
            if (_constraintWarnings is null) GetTypesFromAttribute(property);
            return _constraintWarnings;
        }

        private static void ThrowExceptionIfInvalidProperty(SerializedProperty property)
        {
            if (property.propertyType is not (SerializedPropertyType.String or SerializedPropertyType.ManagedReference))
            {
                throw new ArgumentException(
                    "[TypeSelector] can only be applied to a string field, a SerializableType / SerializableType<T> field, " +
                    "or a [SerializeReference] managed-reference field.",
                    nameof(property));
            }
        }
    }
}
