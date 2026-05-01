using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed marker <see cref="Attribute"/> that drives the Source Generator to emit an <see cref="IView"/>
    /// implementation for the decorated class or struct and to analyze code blocks within the type.
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewAttribute : Attribute { }
}
