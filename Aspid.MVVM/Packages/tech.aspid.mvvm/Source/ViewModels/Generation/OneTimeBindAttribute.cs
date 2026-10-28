// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates a <see cref="BindMode.OneTime"/> bindable member for the field or property of a <see cref="ViewModelAttribute">[ViewModel]</see> class.
    /// </summary>
    /// <remarks>
    /// Ignored on a set-only property.
    /// </remarks>
    /// <example>
    /// <code>
    /// [OneTimeBind] private readonly string _id;
    /// </code>
    /// </example>
    public sealed class OneTimeBindAttribute : BaseBindAttribute { }
}
