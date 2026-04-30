using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Extends <see cref="IBinder"/> with reverse data binding capability — propagating values from the View back to the ViewModel.
    /// </summary>
    /// <typeparam name="T">The type of value propagated to the ViewModel.</typeparam>
    public interface IReverseBinder<out T> : IBinder
    {
        /// <summary>
        /// Raised when the View's value changes and needs to be propagated back to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Gets the binding mode that determines the direction of data flow.
        /// Default is <see cref="BindMode.TwoWay"/>.
        /// </summary>
        BindMode IBinder.Mode => BindMode.TwoWay;
    }
}