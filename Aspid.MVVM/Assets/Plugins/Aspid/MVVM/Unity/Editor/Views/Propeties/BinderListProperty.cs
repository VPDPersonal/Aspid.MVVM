using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class BinderListProperty
    {
        private readonly SerializedProperty _property;

        public int ArraySize
        {
            get => _property.arraySize;
            set
            {
                _property.arraySize = value;
                _property.serializedObject.ApplyModifiedProperties();
            }
        }
        
        public BinderListProperty(SerializedProperty property)
        {
            _property = property;
        }

        public BinderListElementProperty GetArrayElementAtIndex(int index) =>
            new(_property.GetArrayElementAtIndex(index));
    }
}