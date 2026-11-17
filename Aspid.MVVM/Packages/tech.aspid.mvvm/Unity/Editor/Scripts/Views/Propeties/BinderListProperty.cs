using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Serialized binder groups of a <see cref="MonoView"/>.
    /// </summary>
    public sealed class BinderListProperty
    {
        private readonly SerializedObject _serializedObject;

        /// <summary>
        /// Gets or sets the group count, applying changes immediately.
        /// </summary>
        public int ArraySize
        {
            get => Property.arraySize;
            set
            {
                Property.arraySize = value;
                Property.serializedObject.ApplyModifiedProperties();
            }
        }
        
        /// <summary>
        /// Gets the serialized binder group array.
        /// </summary>
        public SerializedProperty Property =>
            _serializedObject.FindProperty("_bindersList");
        
        /// <summary>
        /// Creates a wrapper for the View binder groups.
        /// </summary>
        /// <param name="serializedObject">The serialized View.</param>
        public BinderListProperty(SerializedObject serializedObject)
        {
            _serializedObject = serializedObject;
        }
        
        /// <summary>
        /// Applies pending serialized changes to the View.
        /// </summary>
        public void ApplyModifiedProperties() =>
            _serializedObject.ApplyModifiedProperties();

        /// <summary>
        /// Returns the binder group at an index.
        /// </summary>
        /// <param name="index">The zero-based group index.</param>
        /// <returns>The serialized group wrapper.</returns>
        public BinderListElementProperty GetArrayElementAtIndex(int index) =>
            new(Property.GetArrayElementAtIndex(index));

        /// <summary>
        /// Deletes a serialized binder group without applying the change.
        /// </summary>
        /// <param name="index">The zero-based group index.</param>
        public void DeleteArrayElementAtIndex(int index) =>
            Property.DeleteArrayElementAtIndex(index);
    }
}