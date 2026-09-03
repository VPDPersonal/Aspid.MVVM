#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns a function into a converter.
    /// </summary>
    public static class FuncConverterExtensions
    {
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverter{TFrom, TTo}"/>.
        /// </summary>
        /// <typeparam name="TFrom">The type the function accepts.</typeparam>
        /// <typeparam name="TTo">The type it returns.</typeparam>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter that calls the function.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        public static IConverter<TFrom?, TTo?> ToConverter<TFrom, TTo>(this Func<TFrom?, TTo?> converter) =>
            new FuncConverter<TFrom, TTo>(converter);
    }
}
