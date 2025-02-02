using System;

namespace Aspid.MVVM.Generation
{
    /// <summary>
    /// Marker attribute for fields within a class or structure marked with the <see cref="ViewModelAttribute"/>.
    /// Used by the Source Generator to generate a property based on the marked field.
    /// This attribute enforces <see cref="BindMode.OneWay"/> binding mode.
    /// For readonly fields, this attribute behaves identically to <see cref="BindAttribute"/> with <see cref="BindMode.OneTime"/> or <see cref="OneWayBindAttribute"/>/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class OneWayBindAttribute : BaseBindAttribute { }
}