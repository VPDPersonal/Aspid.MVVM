using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Closes <see cref="StringToEnumCasterMonoBinder{TEnum}"/> over <see cref="BindMode"/> — the one-line subclass a
    /// project writes for its own enum, and what makes the abstract binder addable as a component.
    /// </summary>
    public sealed class StringToBindModeCasterMonoBinder : StringToEnumCasterMonoBinder<BindMode> { }
}
