using System;
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public readonly struct BinderListElementProperty
    {
        private readonly SerializedProperty _idProperty;
        private readonly SerializedProperty _assemblyQualifiedNameProperty;

        public string Id
        {
            get => _idProperty.stringValue;
            set
            {
                _idProperty.stringValue = value;
                _idProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        public string AssemblyQualifiedName
        {
            get => _assemblyQualifiedNameProperty.stringValue;
            set
            {
                _assemblyQualifiedNameProperty.stringValue = value;
                _assemblyQualifiedNameProperty.serializedObject.ApplyModifiedProperties();
            }
        }
        
        public SerializedProperty MonoBindersProperty { get; }
        
        public BinderListElementProperty(SerializedProperty property)
        {
            _idProperty =  property.FindPropertyRelative("_name");
            MonoBindersProperty = property.FindPropertyRelative("_monoBinders");
            _assemblyQualifiedNameProperty = property.FindPropertyRelative("_assemblyQualifiedName");
        }
    }
}