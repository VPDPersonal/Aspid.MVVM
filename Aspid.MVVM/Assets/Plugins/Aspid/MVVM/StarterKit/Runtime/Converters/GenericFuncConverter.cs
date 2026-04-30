using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Adapter converter that wraps a function or converter interface for use as an <see cref="IConverter{TFrom, TTo}"/>.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    public class GenericFuncConverter<TFrom, TTo> : IConverter<TFrom?, TTo?>
    {
        private readonly Func<TFrom?, TTo?> _converter;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericFuncConverter{TFrom, TTo}"/> class.
        /// </summary>
        /// <param name="converter">The converter interface implementation to wrap.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="converter"/> is <c>null</c>.</exception>
        public GenericFuncConverter(IConverter<TFrom?, TTo?> converter)
            : this(converter.Convert) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericFuncConverter{TFrom, TTo}"/> class.
        /// </summary>
        /// <param name="converter">The conversion function.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="converter"/> is <c>null</c>.</exception>
        public GenericFuncConverter(Func<TFrom?, TTo?> converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts the specified value using the wrapped function.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public TTo? Convert(TFrom? value) => _converter(value);
    }
}