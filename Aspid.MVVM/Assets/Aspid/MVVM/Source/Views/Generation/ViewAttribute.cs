using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed marker <see cref="Attribute"/> that drives the Source Generator to emit an <see cref="IView"/>
    /// implementation for the decorated class or struct and to analyze code blocks within the type.
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewAttribute : Attribute
    {
        /// <summary>
        /// Indicates whether the Source Generator should emit binder fields for
        /// <see cref="IView{TViewModel}"/> bindable members that are not already declared on the View.
        /// Defaults to <see langword="true"/>. Set to <see langword="false"/> to suppress this generation
        /// for the decorated View — useful for Views that wire binders manually or do not need an
        /// inspector-driven layout.
        /// </summary>
        public bool AutoBinderFields { get; set; } = true;
    }
}
