using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IAnyBinder"/> that converts any bound value to a <see cref="string"/>
    /// and forwards it to a target setter.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="GenericToStringConverter{T}"/> for the conversion.
    /// </remarks>
    public sealed class AnyToStringCasterBinder : Binder, IAnyBinder
    {
        private readonly Action<string?> _setValue;
        private readonly IConverter<object?, string?> _converter;

        /// <param name="setValue">The action invoked with the converted <see cref="string"/> value.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public AnyToStringCasterBinder(Action<string?> setValue, BindMode mode = BindMode.OneWay)
            : this(setValue, new GenericToStringConverter<object>(), mode) { }

        /// <param name="setValue">The action invoked with the converted <see cref="string"/> value.</param>
        /// <param name="converter">The converter used to transform the incoming value to a <see cref="string"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="setValue"/> or <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public AnyToStringCasterBinder(Action<string?> setValue, IConverter<object?, string?> converter, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="string"/> and forwards it to the target setter.
        /// </summary>
        /// <typeparam name="T">The runtime type of the incoming value.</typeparam>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue<T>(T value) =>
            _setValue(_converter.Convert(value));
    }
}
