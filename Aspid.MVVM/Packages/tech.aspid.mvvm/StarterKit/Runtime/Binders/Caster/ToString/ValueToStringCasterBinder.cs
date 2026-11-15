#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that converts a <typeparamref name="T"/> value to a <see cref="string"/>
    /// and forwards it to a target setter.
    /// </summary>
    /// <typeparam name="T">The source value type produced by the ViewModel binding.</typeparam>
    /// <remarks>
    /// By default, uses <see cref="ValueToStringConverter{T}"/> with the given format string.
    /// </remarks>
    public sealed class ValueToStringCasterBinder<T> : Binder, IBinder<T>
    {
        private readonly Action<string?> _setValue;
        private readonly IConverter<T?, string?> _converter;

        /// <param name="setValue">The action invoked with the converted <see cref="string"/> value.</param>
        /// <param name="format">A composite format string passed to the default converter.</param>
        /// <param name="mode">The binding mode. Must be <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The mode is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.</exception>
        public ValueToStringCasterBinder(
            Action<string?> setValue,
            string format,
            BindMode mode = BindMode.OneWay)
            : this(setValue, new ValueToStringConverter<T>(format), mode) { }

        /// <param name="setValue">The action invoked with the converted <see cref="string"/> value.</param>
        /// <param name="converter">The converter used to transform a <typeparamref name="T"/> value to a <see cref="string"/>.</param>
        /// <param name="mode">The binding mode. Must be <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="setValue"/> or <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">The mode is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.</exception>
        public ValueToStringCasterBinder(
            Action<string?> setValue,
            IConverter<T?, string?> converter,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.EnsureForwardOnly();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="string"/> and forwards it to the target setter.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue(_converter.Convert(value));
    }
}
