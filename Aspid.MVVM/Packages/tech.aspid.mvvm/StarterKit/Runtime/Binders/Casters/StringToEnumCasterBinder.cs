using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that parses a bound
    /// <see cref="string"/> as <typeparamref name="TEnum"/> before forwarding it to a target setter.
    /// </summary>
    /// <remarks>
    /// The direction a dropdown of names, a save file or a config value arrives in. Names are matched
    /// case-insensitively, because a value that came from text rarely matches the C# casing.
    /// <para/>
    /// A string that names no member forwards the fallback value instead. A numeric string is deliberately refused:
    /// <see cref="Enum.TryParse{TEnum}(string, bool, out TEnum)"/> accepts any number, including one no member has,
    /// and an enum holding an undefined value fails later and elsewhere.
    /// </remarks>
    /// <typeparam name="TEnum">The enum type the string is parsed into.</typeparam>
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
        /// Parses <paramref name="value"/> as <typeparamref name="TEnum"/> and forwards the result to the target
        /// setter, or the fallback value when it names no member.
        /// </summary>
        /// <param name="value">The source string value to parse and forward.</param>
        public void SetValue(string? value) =>
            _setValue(EnumCasterParse.TryName(value, out TEnum parsed) ? parsed : _fallback);
    }
}
