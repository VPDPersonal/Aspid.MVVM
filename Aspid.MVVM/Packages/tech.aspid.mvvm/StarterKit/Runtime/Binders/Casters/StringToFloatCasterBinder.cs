using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that parses a bound
    /// <see cref="string"/> as a <see langword="float"/> before forwarding it to a target setter.
    /// </summary>
    /// <remarks>
    /// A string that does not parse forwards the fallback value instead, and so does one that parses to
    /// <see cref="float.NaN"/> or an infinity — those are words <see cref="float"/> parsing accepts. Parsing follows
    /// <see cref="StringNumberParse.TryFloat"/>: the user's culture first, the invariant form second.
    /// </remarks>
    public sealed class StringToFloatCasterBinder : Binder, IBinder<string>
    {
        private readonly float _fallback;
        private readonly Action<float> _setValue;

        /// <param name="setValue">The action invoked with the parsed <see langword="float"/> value.</param>
        /// <param name="fallback">The value forwarded when the string cannot be parsed.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public StringToFloatCasterBinder(Action<float> setValue, float fallback = 0f, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();

            _fallback = fallback;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Parses <paramref name="value"/> and forwards the result to the target setter, or the fallback value when it
        /// does not parse.
        /// </summary>
        /// <param name="value">The source string value to parse and forward.</param>
        public void SetValue(string? value)
        {
            if (StringNumberParse.TryFloat(value, out var parsed))
            {
                _setValue(parsed);
                return;
            }

            this.LogError(value.Expected("a finite number"), $"Forwarding {_fallback} instead.");
            _setValue(_fallback);
        }
    }
}
