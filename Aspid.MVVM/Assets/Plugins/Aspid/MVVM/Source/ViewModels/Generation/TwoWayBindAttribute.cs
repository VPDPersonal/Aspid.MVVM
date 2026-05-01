// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed <see cref="BaseBindAttribute"/> applied to fields of a type carrying <see cref="ViewModelAttribute"/>;
    /// directs the Source Generator to emit a bindable property locked to <see cref="BindMode.TwoWay"/>.
    /// Cannot be applied to <see langword="readonly"/> fields.
    /// </summary>
    public sealed class TwoWayBindAttribute : BaseBindAttribute { }
}
