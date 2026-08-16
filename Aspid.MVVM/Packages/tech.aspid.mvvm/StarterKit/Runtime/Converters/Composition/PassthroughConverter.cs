using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Returns its input unchanged.
    /// </summary>
    /// <typeparam name="T">The type of the value passing through.</typeparam>
    /// <remarks>
    /// Useful as an explicit branch in <see cref="ConditionalConverter{T}"/> — one that reads as a
    /// deliberate no-op rather than an unfilled slot — and as a neutral element in code.
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
