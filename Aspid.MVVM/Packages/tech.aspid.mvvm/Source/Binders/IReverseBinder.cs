using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IBinder"/> that reports values of <typeparamref name="T"/> from the View back to the ViewModel.
    /// </summary>
    /// <typeparam name="T">The reported value type.</typeparam>
    public interface IReverseBinder<out T> : IBinder
    {
        /// <summary>
        /// Occurs when the View produces a value for the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Gets the binding mode; <see cref="BindMode.TwoWay"/> unless the implementing type provides its own.
        /// </summary>
        BindMode IBinder.Mode => BindMode.TwoWay;
    }
}
