using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates a serializable binder from the annotated <see cref="MonoBinder"/> class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GenerateSerializableBinderAttribute : Attribute { }
}
