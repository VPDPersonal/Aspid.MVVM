// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IBinder"/> that receives values of any type from the ViewModel.
    /// </summary>
    /// <remarks>
    /// A bindable member prefers <see cref="IBinder{T}"/> when a binder implements both.
    /// </remarks>
    public interface IAnyBinder : IBinder
    {
        /// <summary>
        /// Applies <paramref name="value"/> to the View.
        /// </summary>
        /// <typeparam name="T">The value type of the bindable member.</typeparam>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue<T>(T value);
    }
}
