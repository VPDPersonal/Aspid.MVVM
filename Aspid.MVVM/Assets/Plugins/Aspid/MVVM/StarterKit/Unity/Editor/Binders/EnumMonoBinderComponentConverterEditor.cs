using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [CustomEditor(typeof(EnumMonoBinder<,,>), editorForChildClasses: true)]
    public class EnumMonoBinderComponentConverterEditor : MonoBinderEditor
    {
        private Type _requiredEnumType;
        private SerializedProperty _enumTypeProperty;

        private Type RequiredEnumType
        {
            get => _requiredEnumType;
            set
            {
                _requiredEnumType = value;
                DisableEnumTypeFieldIfNeeded(Root);
            }
        }

        protected override void OnEnabled() =>
            SyncEnumTypeFromRequireBinder();
        
        protected override void FindProperties()
        {
            base.FindProperties();
            
            var enumValuesProperty = serializedObject.FindProperty("_enumValues");
            _enumTypeProperty = enumValuesProperty.FindPropertyRelative("_enumType");
        }

        protected override void OnCreatedInspectorGUI(MonoBinderVisualElement root)
        {
            root.IdDropdown.RegisterValueChangedCallback(_ => SyncEnumTypeFromRequireBinder());
            root.ViewDropdown.RegisterValueChangedCallback(_ => SyncEnumTypeFromRequireBinder());
            
            root.RegisterCallback<GeometryChangedEvent>(_ => DisableEnumTypeFieldIfNeeded(root));
        }
        
        private void SyncEnumTypeFromRequireBinder()
        {
            RequiredEnumType = null;
            DisableEnumTypeFieldIfNeeded(Root);
            
            if (ViewProperty?.Value is null) return;
            if (string.IsNullOrWhiteSpace(IdProperty?.Value)) return;
            if (!ViewProperty.Value.TryGetRequireBinderFieldsById(IdProperty.Value, out var field)) return;
            
            var enumType = field!.RequiredTypes.FirstOrDefault(t => t.IsEnum);
            if (enumType is null) return;

            RequiredEnumType = enumType;
            
            var assemblyQualifiedName = enumType.AssemblyQualifiedName;
            if (_enumTypeProperty.stringValue == assemblyQualifiedName) return;
            
            _enumTypeProperty.SetStringAndApply(assemblyQualifiedName);
            DisableEnumTypeFieldIfNeeded(Root);
        }

        private void DisableEnumTypeFieldIfNeeded(VisualElement root)
        {
            if (root is null) return;

            var propertyField = root.Query<PropertyField>()
                .Where(propertyField => propertyField.bindingPath is "_enumValues._enumType")
                .First();
            
            propertyField?.Bind(serializedObject);
            propertyField?.Q<Button>()?.SetEnabled(RequiredEnumType is null);
        }
    }
}