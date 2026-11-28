// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Binds the marked field or property in <see cref="ViewModelAttribute">[ViewModel]</see> using <see cref="BindMode.OneWay"/>.
    /// </summary>
    /// <remarks>
    /// Ignored on a <see langword="readonly"/> field and a set-only property.
    /// </remarks>
    /// <example>
    /// <code>
    /// [OneWayBind] private int _score;
    /// </code>
    /// </example>
    public sealed class OneWayBindAttribute : BaseBindAttribute { }
}
