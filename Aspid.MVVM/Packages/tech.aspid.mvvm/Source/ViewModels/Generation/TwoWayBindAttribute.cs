// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Binds the marked field or property in <see cref="ViewModelAttribute">[ViewModel]</see> using <see cref="BindMode.TwoWay"/>.
    /// </summary>
    /// <remarks>
    /// Ignored on a <see langword="readonly"/> field and a get-only or set-only property.
    /// </remarks>
    /// <example>
    /// <code>
    /// [TwoWayBind] private string _name;
    /// </code>
    /// </example>
    public sealed class TwoWayBindAttribute : BaseBindAttribute { }
}
