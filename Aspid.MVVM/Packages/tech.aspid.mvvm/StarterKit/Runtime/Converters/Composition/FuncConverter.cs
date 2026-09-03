#nullable enable
using System;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a function, or another converter's <c>Convert</c>, as an <see cref="IConverter{TFrom, TTo}"/>.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    [TypeSelectorDisplay(Hidden = true)]
    public class FuncConverter<TFrom, TTo> : IConverter<TFrom?, TTo?>
    {
        private readonly Func<TFrom?, TTo?> _converter;

        /// <param name="converter">The converter to wrap.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        public FuncConverter(IConverter<TFrom?, TTo?> converter)
            : this((converter ?? throw new ArgumentNullException(nameof(converter))).Convert) { }

        /// <param name="converter">The conversion function.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        public FuncConverter(Func<TFrom?, TTo?> converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts the value using the wrapped function.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public TTo? Convert(TFrom? value) => _converter(value);
    }
}
