using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Excludes the field or property of a <see cref="ViewAttribute">[View]</see> class from the generated binding code.
    /// </summary>
    /// <example>
    /// <code>
    /// [IgnoreBind]
    /// [SerializeField] private MonoBinder[] _previewBinders;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class IgnoreBindAttribute : Attribute { }
}
