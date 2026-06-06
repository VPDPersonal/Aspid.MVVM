using System;
using System.ComponentModel;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed <see cref="Attribute"/> applied to fields of a type carrying <see cref="ViewModelAttribute"/>;
    /// directs the Source Generator to also raise the change event of the property named <see cref="PropertyName"/>
    /// when the decorated field changes. Requires a companion <see cref="BindAttribute"/>, <see cref="OneWayBindAttribute"/>,
    /// <see cref="TwoWayBindAttribute"/>, or <see cref="OneWayToSourceBindAttribute"/> on the same field.
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class BindAlsoAttribute : Attribute
    {
#if UNITY_EDITOR || DEBUG
        /// <summary>
        /// Gets the name of the generated property whose change event should also be triggered.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string PropertyName { get; }
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="BindAlsoAttribute"/> with the specified property name.
        /// </summary>
        /// <param name="propertyName">The name of the generated property whose change event should also be triggered.</param>
        public BindAlsoAttribute(string propertyName)
        {
#if UNITY_EDITOR || DEBUG
            PropertyName = propertyName;
#endif
        }
    }
}
