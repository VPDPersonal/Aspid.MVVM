using System;

namespace Aspid.MVVM.Views.Generation
{
    /// <summary>
    /// Marker attribute for classes and structures.
    /// Used by the Source Generator to generate an implementation of the <see cref="IView"/> interface.
    /// It also allows the Source Generator to analyze code blocks within the marked class or structure.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewAttribute : Attribute { }
}