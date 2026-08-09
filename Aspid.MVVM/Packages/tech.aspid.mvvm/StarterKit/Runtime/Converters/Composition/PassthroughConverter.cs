using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Returns its input unchanged.
    /// </summary>
    /// <typeparam name="T">The type of the value passing through.</typeparam>
    /// <remarks>
    /// An explicit no-op reads differently from an empty slot: it says the author considered the
    /// conversion and chose none. Useful as a placeholder branch in
    /// <see cref="ConditionalConverter{T}"/> and as a neutral element when a converter is assembled
    /// from code.
    /// </remarks>
    [Serializable]
    public sealed class PassthroughConverter<T> : IConverter<T, T>
    {
        /// <summary>
        /// Returns the specified value unchanged.
        /// </summary>
        /// <param name="value">The value to pass through.</param>
        /// <returns>The same value.</returns>
        public T Convert(T value) => value;
    }
}
