// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates a <see cref="BindMode.TwoWay"/> bindable member for the field or property of a <see cref="ViewModelAttribute">[ViewModel]</see> class.
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
