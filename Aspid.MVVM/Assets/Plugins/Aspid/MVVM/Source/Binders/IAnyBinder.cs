// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Extends <see cref="IBinder"/> with the ability to receive values of any type from the ViewModel.
    /// </summary>
    public interface IAnyBinder : IBinder
    {
        /// <summary>
        /// Sets the bound property to <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">The type of value received from the ViewModel.</typeparam>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue<T>(T value);
    }
}