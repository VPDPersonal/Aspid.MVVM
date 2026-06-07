using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IAnyBinder"/> that converts any bound value to a <see cref="string"/>
    /// using a configurable converter before forwarding it to a target setter.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="GenericToString{T}"/> for the conversion.
    /// A custom <see cref="IConverter{TFrom,TTo}"/> can be supplied for specialized formatting.
    /// </remarks>
    /// <include file="XmlExampleDoc-Casters-1.1.0.xml" path="doc//member[@name='AnyToStringCasterBinder']/*" />
    public sealed class AnyToStringCasterBinder : Binder, IAnyBinder
    {
        private readonly Action<string?> _setValue;
        private readonly IConverter<object?, string?> _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="AnyToStringCasterBinder"/> using the default
        /// <see cref="GenericToString{T}"/> converter.
        /// </summary>
        /// <param name="setValue">The action invoked with the converted <see cref="string"/> value.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public AnyToStringCasterBinder(Action<string?> setValue, BindMode mode = BindMode.OneWay)
            : this(setValue, new GenericToString<object>(), mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AnyToStringCasterBinder"/> with a custom converter.
        /// </summary>
        /// <param name="setValue">The action invoked with the converted <see cref="string"/> value.</param>
        /// <param name="converter">The converter used to transform the incoming value to a <see cref="string"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="setValue"/> or <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        public AnyToStringCasterBinder(Action<string?> setValue, IConverter<object?, string?> converter, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="string"/> and forwards the result to the target setter.
        /// </summary>
        /// <typeparam name="T">The runtime type of the incoming value.</typeparam>
        /// <param name="value">The value to convert and forward.</param>
        public void SetValue<T>(T value) =>
            _setValue(_converter.Convert(value));
    }
}