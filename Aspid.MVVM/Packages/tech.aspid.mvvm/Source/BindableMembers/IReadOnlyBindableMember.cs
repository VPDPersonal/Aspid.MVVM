using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IReadOnlyValueBindableMember{T}"/> that reports value changes.
    /// </summary>
    /// <typeparam name="T">The bound value type.</typeparam>
    public interface IReadOnlyBindableMember<out T> : IReadOnlyValueBindableMember<T>
    {
        /// <summary>
        /// Occurs when the value changes.
        /// </summary>
        public event Action<T?>? Changed;
    }
}
