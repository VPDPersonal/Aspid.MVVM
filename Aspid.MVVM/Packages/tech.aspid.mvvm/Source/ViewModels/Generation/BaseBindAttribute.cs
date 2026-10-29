using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="Attribute"/> shared by <see cref="BindAttribute"/> and the bind attributes with a fixed mode.
    /// </summary>
    /// <remarks>
    /// The Source Generator recognizes the bind attributes by name, so a custom subclass generates nothing.
    /// </remarks>
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property)]
    public abstract class BaseBindAttribute : Attribute { }
}
