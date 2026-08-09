using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// Fluent extension methods for <see cref="SerializedProperty"/> providing chainable wrappers
    /// around <see cref="SerializedObject"/> synchronization and typed value setters.
    /// </summary>
    public static partial class SerializePropertyExtensions
    {
        /// <summary>
        /// Calls <see cref="SerializedObject.Update"/> on the property's serialized object and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">The property whose serialized object should be updated.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T Update<T>(this T property)
            where T : SerializedProperty
        {
            property.serializedObject.Update();
            return property;
        }

        /// <summary>
        /// Calls <see cref="SerializedObject.UpdateIfRequiredOrScript"/> on the property's serialized object and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">The property whose serialized object should be conditionally updated.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T UpdateIfRequiredOrScript<T>(this T property)
            where T : SerializedProperty
        {
            property.serializedObject.UpdateIfRequiredOrScript();
            return property;
        }

        /// <summary>
        /// Calls <see cref="SerializedObject.ApplyModifiedProperties"/> on the property's serialized object and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">The property whose serialized object changes should be applied.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T ApplyModifiedProperties<T>(this T property)
            where T : SerializedProperty
        {
            property.serializedObject.ApplyModifiedProperties();
            return property;
        }

        /// <summary>
        /// Calls <see cref="SerializedObject.ApplyModifiedPropertiesWithoutUndo"/> on the property's serialized object and returns the property for chaining.
        /// </summary>
        /// <typeparam name="T">Concrete <see cref="SerializedProperty"/> type.</typeparam>
        /// <param name="property">The property whose serialized object changes should be applied without registering an undo step.</param>
        /// <returns>The same <paramref name="property"/> instance.</returns>
        public static T ApplyModifiedPropertiesWithoutUndo<T>(this T property)
            where T : SerializedProperty
        {
            property.serializedObject.ApplyModifiedPropertiesWithoutUndo();
            return property;
        }

        /// <summary>
        /// Returns a copy of the property backed by its own new <see cref="SerializedObject"/>,
        /// independent of the source and safe to store.
        /// </summary>
        /// <remarks>
        /// Use when the property must outlive the original <see cref="SerializedObject"/>,
        /// e.g. cached on a long-lived UIToolkit element or captured in a deferred callback.
        /// The new <see cref="SerializedObject"/> is owned by the caller and is never disposed by this method.
        /// The copy reads the target's current serialized state: changes pending on the source
        /// without <see cref="SerializedObject.ApplyModifiedProperties"/> are not visible to it.
        /// </remarks>
        /// <param name="property">Source property — its <see cref="SerializedProperty.propertyPath"/> is reused.</param>
        /// <returns>A new <see cref="SerializedProperty"/> at the same path on a new <see cref="SerializedObject"/>.</returns>
        public static SerializedProperty Persistent(this SerializedProperty property) =>
            new SerializedObject(property.serializedObject.targetObjects).FindProperty(property.propertyPath);
    }
}
