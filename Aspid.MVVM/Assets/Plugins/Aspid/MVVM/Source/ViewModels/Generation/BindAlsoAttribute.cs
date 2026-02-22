using System;
using System.ComponentModel;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
namespace Aspid.MVVM
{
    /// <summary>
    /// Marker attribute for fields within a class or structure marked with the <see cref="ViewModelAttribute"/>.
    /// Used by the Source Generator to generate an event call in the generated property named propertyName.
    /// For this attribute to work correctly, the <see cref="BindAttribute"/> or <see cref="OneWayBindAttribute"/>
    /// or <see cref="TwoWayBindAttribute"/> or <see cref="OneWayToSourceBindAttribute"/>
    /// must also be present.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class BindAlsoAttribute : Attribute
    {
#if UNITY_EDITOR || DEBUG
        /// <summary>
        /// The name of the generated property whose change event should also be triggered.
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