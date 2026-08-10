using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that converts values of type <typeparamref name="T"/> to a <see cref="string"/>
    /// using a configurable converter before forwarding them to a target setter.
    /// </summary>
    /// <typeparam name="T">The source value type produced by the ViewModel binding.</typeparam>
    /// <remarks>
    /// A <see cref="GenericToString{T}"/> with an optional format string is used by default.
    /// </remarks>
    /// <include file="XmlExampleDoc-Casters-1.1.0.xml" path="doc//member[@name='GenericToStringCasterBinder{1}']/*" />
    public sealed class GenericToStringCasterBinder<T> : Binder, IBinder<T>
    {
        private readonly Action<string?> _setValue;
        private readonly IConverter<T?, string?> _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="GenericToStringCasterBinder{T}"/> using a
        /// <see cref="GenericToString{T}"/> converter with the specified format string.
        /// </summary>
        /// <param name="setValue">The action invoked with the converted <see cref="string"/> value.</param>
        /// <param name="format">A composite format string passed to the underlying converter.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public GenericToStringCasterBinder(Action<string?> setValue, string format, BindMode mode = BindMode.OneWay)
            : this(setValue, new GenericToString<T>(format), mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="GenericToStringCasterBinder{T}"/> with a custom converter.
        /// </summary>
        /// <param name="setValue">The action invoked with the converted <see cref="string"/> value.</param>
        /// <param name="converter">The converter used to transform a <typeparamref name="T"/> value to a <see cref="string"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="setValue"/> or <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        public GenericToStringCasterBinder(Action<string?> setValue, IConverter<T?, string?> converter, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="string"/> and forwards the result to the target setter.
        /// </summary>
        /// <param name="value">The source value to convert and forward.</param>
        public void SetValue(T? value) =>
            _setValue(_converter.Convert(value));
    }
}