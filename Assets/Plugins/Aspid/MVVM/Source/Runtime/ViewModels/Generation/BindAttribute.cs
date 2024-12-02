using System;

namespace Aspid.MVVM.ViewModels.Generation
{
    /// <summary>
    /// Marker attribute for fields within a class or structure marked with the <see cref="ViewModelAttribute"/>.
    /// Used by the Source Generator to generate a property based on the marked field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BindAttribute : Attribute { }
}