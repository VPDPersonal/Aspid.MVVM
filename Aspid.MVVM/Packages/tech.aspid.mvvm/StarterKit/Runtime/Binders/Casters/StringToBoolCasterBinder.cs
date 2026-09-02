using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that converts a bound
    /// <see cref="string"/> to a <see cref="bool"/> and forwards it to a target setter.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="StringEmptyToBoolConverter"/>: an empty or <see langword="null"/> string maps to <see langword="true"/>.
    /// </remarks>
    public sealed class StringToBoolCasterBinder : Binder, IBinder<string>
    {
        private readonly Action<bool> _setValue;
        private readonly IConverter<string?, bool> _converter;

        /// <param name="setValue">The action invoked with the converted <see cref="bool"/> value.</param>
        /// <param name="isInvert">When <see langword="true"/>, the result is negated: a filled string maps to <see langword="true"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public StringToBoolCasterBinder(Action<bool> setValue, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : this(setValue, new StringEmptyToBoolConverter(isInvert), mode) { }

        /// <param name="setValue">The action invoked with the converted <see cref="bool"/> value.</param>
        /// <param name="converter">The converter used to transform a <see cref="string"/> to a <see cref="bool"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="setValue"/> or <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public StringToBoolCasterBinder(Action<bool> setValue, IConverter<string?, bool> converter, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="bool"/> and forwards it to the target setter.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(string? value) =>
            _setValue(_converter.Convert(value));
    }
}
