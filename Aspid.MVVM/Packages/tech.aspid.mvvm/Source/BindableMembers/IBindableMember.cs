// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IReadOnlyBindableMember{T}"/> whose value can also be set.
    /// </summary>
    /// <typeparam name="T">The bound value type.</typeparam>
    public interface IBindableMember<T> : IReadOnlyBindableMember<T>
    {
        /// <summary>
        /// Sets the current value and raises <see cref="IReadOnlyBindableMember{T}.Changed"/>.
        /// </summary>
        public new T? Value { set; }
    }
}
