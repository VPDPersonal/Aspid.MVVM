using System;

namespace Aspid.MVVM.Generation
{
    /// <summary>
    /// Marker attribute for classes and structures.
    /// Used by the Source Generator to generate the implementation of the <see cref="IViewModel"/> interface.
    /// Also allows the Source Generator to analyze code blocks within the marked class or structure.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewModelAttribute : Attribute { }
}