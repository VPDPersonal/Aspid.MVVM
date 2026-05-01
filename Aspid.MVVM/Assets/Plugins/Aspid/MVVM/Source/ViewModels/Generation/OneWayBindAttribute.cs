// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed <see cref="BaseBindAttribute"/> applied to fields of a type carrying <see cref="ViewModelAttribute"/>;
    /// directs the Source Generator to emit a bindable property locked to <see cref="BindMode.OneWay"/>.
    /// On <see langword="readonly"/> fields the mode resolves to <see cref="BindMode.OneTime"/>, matching
    /// <see cref="BindAttribute"/> with <see cref="BindMode.OneTime"/>.
    /// </summary>
    public sealed class OneWayBindAttribute : BaseBindAttribute { }
}
