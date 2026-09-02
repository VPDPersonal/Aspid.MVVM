using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that parses a bound
    /// <see cref="string"/> as an <see langword="int"/> and forwards it to a target setter.
    /// </summary>
    /// <remarks>
    /// Parsing follows <see cref="StringNumberParse.TryInt"/>. A string that does not parse is logged as an error
    /// and the fallback value is forwarded instead.
    /// </remarks>
    public sealed class StringToIntCasterBinder : Binder, IBinder<string>
    {
        private readonly int _fallback;
        private readonly Action<int> _setValue;

        /// <param name="setValue">The action invoked with the parsed <see langword="int"/> value.</param>
        /// <param name="fallback">The value forwarded when the string cannot be parsed.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public StringToIntCasterBinder(Action<int> setValue, int fallback = 0, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();

            _fallback = fallback;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Parses <paramref name="value"/> and forwards the result, or the fallback value when it does not parse.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(string? value)
        {
            if (StringNumberParse.TryInt(value, out var parsed))
            {
                _setValue(parsed);
                return;
            }

            this.LogError(value.Expected("a whole number"), $"Forwarding {_fallback} instead.");
            _setValue(_fallback);
        }
    }
}
