using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Marker attribute for fields within a class or structure marked with the <see cref="ViewModelAttribute"/>.
    /// Used by the Source Generator to generate an event call in the generated property named propertyName.
    /// For this attribute to work correctly, the <see cref="BindAttribute"/> or <see cref="OneWayBindAttribute"/>
    /// or <see cref="TwoWayBindAttribute"/> or <see cref="OneWayToSourceBindAttribute"/>
    /// must also be present.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class BindAlsoAttribute : Attribute
    {
        public BindAlsoAttribute(string propertyName) { }
    }
}