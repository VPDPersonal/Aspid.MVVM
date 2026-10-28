// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates a <see cref="BindMode.OneWay"/> bindable member for the field or property of a <see cref="ViewModelAttribute">[ViewModel]</see> class.
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
