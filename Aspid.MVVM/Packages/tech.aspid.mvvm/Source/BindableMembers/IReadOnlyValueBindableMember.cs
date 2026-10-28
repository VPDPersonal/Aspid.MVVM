// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IBinderAdder"/> that exposes its current value.
    /// </summary>
    /// <typeparam name="T">The bound value type.</typeparam>
    public interface IReadOnlyValueBindableMember<out T> : IBinderAdder
    {
        /// <summary>
        /// Gets the current value.
        /// </summary>
        public T? Value { get; }
    }
}
