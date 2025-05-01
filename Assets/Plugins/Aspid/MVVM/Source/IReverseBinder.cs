using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Interface for creating reverse data binding from View to ViewModel.
    /// Reverse data binding is used to pass modified data from a View component back to the ViewModel.
    /// The binding mode is determined by the <see cref="BindMode"/> property, where the default mode is <c>TwoWay</c>.
    /// </summary>
    /// <typeparam name="T">The type of data passed during reverse binding.</typeparam>
    public interface IReverseBinder<out T> : IBinder
    {
        /// <summary>
        /// Event triggered when the value changes in the View and is propagated back to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;
        
        /// <summary>
        /// The binding mode that defines the direction of data flow.
        /// In the case of reverse binding, this is set to <c>TwoWay</c>, allowing updates to flow in both directions.
        /// </summary>
        BindMode IBinder.Mode => BindMode.TwoWay;
    }
}