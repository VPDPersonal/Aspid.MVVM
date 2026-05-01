using System;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed <see cref="Attribute"/> applied to fields of a type carrying <see cref="ViewModelAttribute"/>;
    /// overrides the default <see langword="private"/> access modifier of the generated property's get and set
    /// accessors. Requires a companion <see cref="BindAttribute"/>, <see cref="OneWayBindAttribute"/>,
    /// <see cref="TwoWayBindAttribute"/>, <see cref="OneTimeBindAttribute"/>, or <see cref="OneWayToSourceBindAttribute"/>
    /// on the same field.
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Field)]
    public sealed class AccessAttribute : Attribute
    {
        /// <summary>
        /// Access modifier for the get accessor.
        /// </summary>
        public Access Get { get; set; }
        
        /// <summary>
        /// Access modifier for the set accessor.
        /// </summary>
        public Access Set { get; set; }
        
        /// <summary>
        /// Sets the access modifier for generated properties. Defaults to <see cref="Access.Private"/>.
        /// </summary>
        /// <param name="access">Access modifier for the get and set accessors.</param>
        public AccessAttribute(Access access = Access.Private) { }
    }
}
