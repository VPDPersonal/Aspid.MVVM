using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IBinder"/> that reports untyped values from the View back to the ViewModel.
    /// </summary>
    /// <remarks>
    /// Built-in bindable members throw <see cref="ArgumentException"/> when a reported value has an incompatible type.
    /// </remarks>
    public interface IAnyReverseBinder : IBinder
    {
        /// <summary>
        /// Occurs when the View produces a value for the ViewModel.
        /// </summary>
        public event Action<object>? ValueChanged;

        /// <summary>
        /// Gets the binding mode; <see cref="BindMode.TwoWay"/> unless the implementing type provides its own.
        /// </summary>
        BindMode IBinder.Mode => BindMode.TwoWay;
    }
}
