using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class BinderListProperty
    {
        private readonly SerializedObject _serializedObject;

        public int ArraySize
        {
            get => Property.arraySize;
            set
            {
                Property.arraySize = value;
                Property.serializedObject.ApplyModifiedProperties();
            }
        }
        
        private SerializedProperty Property =>
            _serializedObject.FindProperty("_bindersList");
        
        public BinderListProperty(SerializedObject serializedObject)
        {
            _serializedObject = serializedObject;
        }

        public BinderListElementProperty GetArrayElementAtIndex(int index) =>
            new(Property.GetArrayElementAtIndex(index));
    }
}