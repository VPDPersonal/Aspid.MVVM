using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Interface for creating reverse data binding from View to ViewModel.
    /// Reverse data binding is used to pass modified data from a View component back to the ViewModel.
    /// </summary>
    /// <typeparam name="T">The type of data passed during reverse binding.</typeparam>
    public interface IReverseBinder<out T> : IBinder
    {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event Action<T?>? ValueChanged;
        
        /// <summary>
        /// Indicates that reverse binding is enabled.
        /// The default value is <c>true</c>.
        /// </summary>
        bool IBinder.IsReverseEnabled => true;
    }
}