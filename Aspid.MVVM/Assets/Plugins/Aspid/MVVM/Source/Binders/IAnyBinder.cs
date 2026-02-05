// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Interface extending <see cref="IBinder"/> to allow setting a value of any type.
    /// </summary>
    public interface IAnyBinder : IBinder
    {
        /// <summary>
        /// Sets a value of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="value">The value to be set.</param>
        public void SetValue<T>(T value);
    }
}