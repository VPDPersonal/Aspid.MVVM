// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Binds the marked field or property in <see cref="ViewModelAttribute">[ViewModel]</see> using <see cref="BindMode.OneTime"/>.
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
