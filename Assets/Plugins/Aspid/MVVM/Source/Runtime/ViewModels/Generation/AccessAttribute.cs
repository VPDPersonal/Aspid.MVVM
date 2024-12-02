using System;

namespace Aspid.MVVM.ViewModels.Generation
{
    /// <summary>
    /// Marker attribute for fields within a class or structure marked with the <see cref="ViewModelAttribute"/>.
    /// Used by the Source Generator to change the access modifier of generated properties, which default to <c>private</c>.
    /// For this attribute to work correctly, the <see cref="BindAttribute"/> or <see cref="ReadOnlyBindAttribute"/> must also be present.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
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