using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that parses a bound
    /// <see cref="string"/> as a <typeparamref name="TEnum"/> member name and forwards it to a target setter.
    /// </summary>
    /// <typeparam name="TEnum">The enum type the string is parsed into.</typeparam>
    /// <remarks>
    /// Parsing follows <see cref="EnumNameParse.TryName{TEnum}"/>. A string that names no member is logged
    /// as an error and the fallback value is forwarded instead.
    /// </remarks>
    public sealed class StringToEnumCasterBinder<TEnum> : Binder, IBinder<string>
        where TEnum : struct, Enum
    {
        private readonly TEnum _fallback;
        private readonly Action<TEnum> _setValue;

        /// <param name="setValue">The action invoked with the parsed <typeparamref name="TEnum"/> value.</param>
        /// <param name="fallback">The value forwarded when the string names no member.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public StringToEnumCasterBinder(Action<TEnum> setValue, TEnum fallback = default, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();

            _fallback = fallback;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Parses <paramref name="value"/> and forwards the result, or the fallback value when it names no member.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(string? value)
        {
            if (EnumNameParse.TryName(value, out TEnum parsed))
            {
                _setValue(parsed);
                return;
            }

            this.LogError(value.Expected($"a member of {typeof(TEnum).Name}"), $"Forwarding {_fallback} instead.");
            _setValue(_fallback);
        }
    }
}
