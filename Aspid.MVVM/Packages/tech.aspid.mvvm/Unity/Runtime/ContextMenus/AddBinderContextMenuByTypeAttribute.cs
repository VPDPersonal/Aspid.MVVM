using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Offers the decorated component in serialized property menus matching the property's value type.
    /// </summary>
    /// <remarks>Multiple applications support different value types. Applications are Editor-only.</remarks>
    /// <example>
    /// <code>
    /// [AddBinderContextMenuByType(typeof(float))]
    /// public sealed class FloatBinder : MonoBinder { }
    /// </code>
    /// </example>
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AddBinderContextMenuByTypeAttribute : Attribute
    {
        /// <summary>
        /// Gets the value type accepted by the property menu entry.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Creates an entry matched by serialized property value type.
        /// </summary>
        /// <param name="propertyType">The type assignable from the serialized property's value type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyType"/> is <see langword="null"/>.</exception>
        public AddBinderContextMenuByTypeAttribute(Type propertyType)
        {
            Type = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
        }
    }
}
