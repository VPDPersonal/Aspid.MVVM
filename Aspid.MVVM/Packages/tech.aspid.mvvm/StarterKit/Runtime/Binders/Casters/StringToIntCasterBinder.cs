#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that parses a bound
    /// <see cref="string"/> as an <see langword="int"/> before forwarding it to a target setter.
    /// </summary>
    /// <remarks>
    /// The casters covered the direction into a string and not the one out of it, which is the direction an input
    /// field works in: a ViewModel holding an <see langword="int"/> could be shown in a text field and not filled
    /// from one.
    /// <para/>
    /// A string that does not parse forwards the fallback value instead. Parsing follows
    /// <see cref="StringNumberParse.TryInt"/>: the user's culture first, the invariant form second.
    /// </remarks>
    public sealed class StringToIntCasterBinder : Binder, IBinder<string>
    {
        private readonly int _fallback;
        private readonly Action<int> _setValue;

        /// <summary>
        /// Initializes a new instance of <see cref="StringToIntCasterBinder"/>.
        /// </summary>
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
        /// Parses <paramref name="value"/> and forwards the result to the target setter, or the fallback value when it
        /// does not parse.
        /// </summary>
        /// <param name="value">The source string value to parse and forward.</param>
        public void SetValue(string? value) =>
            _setValue(StringNumberParse.TryInt(value, out var parsed) ? parsed : _fallback);
    }
}
