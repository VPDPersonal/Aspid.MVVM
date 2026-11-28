// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Binds the marked field or property in <see cref="ViewModelAttribute">[ViewModel]</see> using <see cref="BindMode.OneWayToSource"/>.
    /// </summary>
    /// <remarks>
    /// Ignored on a <see langword="readonly"/> field and a get-only property.
    /// </remarks>
    /// <example>
    /// <code>
    /// [OneWayToSourceBind] private float _volume;
    /// </code>
    /// </example>
    public sealed class OneWayToSourceBindAttribute : BaseBindAttribute { }
}
