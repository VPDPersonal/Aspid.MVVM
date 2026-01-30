using System;
using System.Linq;
using UnityEditor;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [CustomEditor(typeof(EnumMonoBinder<>), editorForChildClasses: true)]
    public class EnumMonoBinderEditor : MonoBinderEditor
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
            base.OnCreatedInspectorGUI(root);
            
            root.IdDropdown.RegisterValueChangedCallback(_ => SyncEnumTypeFromRequireBinder());
            root.ViewDropdown.RegisterValueChangedCallback(_ => SyncEnumTypeFromRequireBinder());
            
            root.RegisterCallbackOnce<GeometryChangedEvent>(_ => DisableEnumTypeFieldIfNeeded(root));
        }
        
        private void SyncEnumTypeFromRequireBinder()
        {
            RequiredEnumType = null;
            DisableEnumTypeFieldIfNeeded(Root);
            
            if (ViewProperty?.objectReferenceValue is not IView view) return;
            if (string.IsNullOrWhiteSpace(IdProperty?.stringValue)) return;
            if (!view.TryGetRequireBinderFieldsById(IdProperty.stringValue, out var field)) return;
            
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
            propertyField?.SetEnabled(RequiredEnumType is null);
        }
    }
}
