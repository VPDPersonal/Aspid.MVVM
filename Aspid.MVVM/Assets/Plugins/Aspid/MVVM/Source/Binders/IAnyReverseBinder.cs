using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Extends <see cref="IBinder"/> with reverse data binding capability — propagating non-generic values from the View back to the ViewModel.
    /// </summary>
    /// <remarks>
    /// Use with caution: since <see cref="ValueChanged"/> carries a non-generic <see langword="object"/>,
    /// the type contract between the View and ViewModel is not enforced at compile time.
    /// A type mismatch will only be detected at runtime.
    /// </remarks>
    public interface IAnyReverseBinder : IBinder
    {
        /// <summary>
        /// Raised when the View's value changes and needs to be propagated back to the ViewModel.
        /// </summary>
        public event Action<object>? ValueChanged;

        /// <summary>
        /// Gets the binding mode that determines the direction of data flow.
        /// Default is <see cref="BindMode.TwoWay"/>.
        /// </summary>
        BindMode IBinder.Mode => BindMode.TwoWay;
    }
}