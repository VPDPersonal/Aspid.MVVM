using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a read-only bindable member that exposes a value and notifies listeners when the value changes.
    /// </summary>
    /// <typeparam name="T">The type of the value being exposed.</typeparam>
    public interface IReadOnlyBindableMember<out T> : IReadOnlyValueBindableMember<T>
    {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event Action<T?>? Changed;
    }
}