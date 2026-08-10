using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns a function into a converter.
    /// </summary>
    public static class ConverterExtensions
    {
        /// <summary>
        /// Wraps the specified function as an <see cref="IConverter{TFrom, TTo}"/>.
        /// </summary>
        /// <typeparam name="TFrom">The type the function accepts.</typeparam>
        /// <typeparam name="TTo">The type it returns.</typeparam>
        /// <param name="converter">The function to wrap.</param>
        /// <returns>A converter that calls the function.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="converter"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This is the one that stays. The <c>ToConvert</c> overloads on the
        /// <c>…SpecificExtensions</c> classes wrap a lambda as one of the named aliases and are
        /// deprecated along with them.
        /// </remarks>
        public static IConverter<TFrom?, TTo?> ToConvert<TFrom, TTo>(this Func<TFrom?, TTo?> converter) =>
            new GenericFuncConverter<TFrom, TTo>(converter);
    }
}
