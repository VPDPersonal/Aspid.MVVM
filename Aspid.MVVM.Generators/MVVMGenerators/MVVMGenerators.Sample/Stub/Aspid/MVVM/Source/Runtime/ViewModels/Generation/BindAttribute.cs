using System;

namespace Aspid.MVVM.Generation
{
    /// <summary>
    /// Marker attribute for fields within a class or structure marked with the <see cref="ViewModelAttribute"/>.
    /// Used by the Source Generator to generate a property based on the marked field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BindAttribute : Attribute
    {
        // public bool IsReverse { get; private set; }
        //
        // public bool IsReverseError { get; private set; }
    }

    public enum BindError
    {
        None,
        Always,
        OnlyEditor,
    }
    
    public enum BindingMode
    {
        None,
        OneWay,
        TwoWay,
        OneTime,
        OneWayToSource,
    }
}