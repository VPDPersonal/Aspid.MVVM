using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Serialized binding identifier, value type name and binder slots for one View member.
    /// </summary>
    public readonly struct BinderListElementProperty
    {
        private readonly SerializedProperty _idProperty;
        private readonly SerializedProperty _assemblyQualifiedNameProperty;

        /// <summary>
        /// Gets or sets the binding identifier, applying changes immediately.
        /// </summary>
        public string Id
        {
            get => _idProperty.stringValue;
            set
            {
                _idProperty.stringValue = value;
                _idProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        /// <summary>
        /// Gets or sets the value type name, applying changes immediately.
        /// </summary>
        public string AssemblyQualifiedName
        {
            get => _assemblyQualifiedNameProperty.stringValue;
            set
            {
                _assemblyQualifiedNameProperty.stringValue = value;
                _assemblyQualifiedNameProperty.serializedObject.ApplyModifiedProperties();
            }
        }
        
        /// <summary>
        /// Gets the serialized binder slots.
        /// </summary>
        public SerializedProperty MonoBindersProperty { get; }
        
        /// <summary>
        /// Creates a wrapper for one serialized binder group.
        /// </summary>
        /// <param name="property">The binder group property.</param>
        public BinderListElementProperty(SerializedProperty property)
        {
            _idProperty =  property.FindPropertyRelative("_name");
            MonoBindersProperty = property.FindPropertyRelative("_monoBinders");
            _assemblyQualifiedNameProperty = property.FindPropertyRelative("_assemblyQualifiedName");
        }
    }
}