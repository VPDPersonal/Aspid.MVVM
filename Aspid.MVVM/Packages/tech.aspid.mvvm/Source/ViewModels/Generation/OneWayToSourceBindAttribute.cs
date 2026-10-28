// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates a <see cref="BindMode.OneWayToSource"/> bindable member for the field or property of a <see cref="ViewModelAttribute">[ViewModel]</see> class.
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
