#nullable enable
using Aspid.FastTools.Types;
using System;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Names the flags a value carries.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being named.</typeparam>
    /// <remarks>
    /// <see cref="EnumToStringConverter{TEnum}"/> names one member and returns its fallback for a
    /// combination; this takes the value apart first and asks that one for each piece, so a member's
    /// <see cref="InspectorNameAttribute"/> reads the same here as it does there.
    /// <para>
    /// Members are read in the order <c>Enum.GetValues</c> returns them — by unsigned underlying value,
    /// never declaration order — and each consumes the bits it names. A composite member always sorts
    /// after its parts, so it is named only when the bits it covers are not declared members of their
    /// own, and a member covering one declared bit and one undeclared one is skipped, leaving that bit
    /// unnamed. On an enum not marked <see cref="FlagsAttribute"/> the value is named whole and the
    /// separator goes unused.
    /// </para>
    /// <para>
    /// The previous text is reused while the value is unchanged, so an edit to the separator or the
    /// empty text while the game is running reaches the View on the next value that differs.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Enum", Name = "Enum Flags To String", Tooltip = "Names the flags a value carries")]
    public sealed class EnumFlagsToStringConverter<TEnum> : IConverter<TEnum, string>
        where TEnum : struct, Enum
    {
        [Tooltip("Placed between the named flags. Unused on an enum not marked [Flags], where the value names one member rather than a set of bits.")]
        [SerializeField] private string _separator = ", ";

        [Tooltip("Where the name of each flag comes from.")]
        [SerializeField] private EnumNameSource _source;

        [Tooltip("Shown when the value names no flags.")]
        [SerializeField] private string _noneText = string.Empty;

        [NonSerialized] private StringBuilder? _builder;
        [NonSerialized] private EnumToStringConverter<TEnum>? _names;
        [NonSerialized] private EnumNameSource _namedSource;

        [NonSerialized] private bool _hasCache;
        [NonSerialized] private TEnum _cachedValue;
        [NonSerialized] private string _cachedText = string.Empty;

        /// <remarks>Default: joining with commas.</remarks>
        public EnumFlagsToStringConverter() { }

        /// <param name="separator">Placed between the named flags.</param>
        /// <param name="source">Where the name of each flag comes from.</param>
        /// <param name="noneText">Shown when the value names no flags.</param>
        public EnumFlagsToStringConverter(
            string separator,
            EnumNameSource source = EnumNameSource.Name,
            string noneText = "")
        {
            _separator = separator;
            _source = source;
            _noneText = noneText;
        }

        /// <summary>
        /// Names the flags the specified value carries.
        /// </summary>
        /// <param name="value">The value to take apart.</param>
        /// <returns>
        /// The names of the flags it carries, joined by the separator, or the empty text when it
        /// carries none. On an enum not marked <see cref="FlagsAttribute"/> the value names one
        /// member and the separator is unused. The same string is returned while the value is
        /// unchanged.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the name source is not a declared value.</exception>
        public string Convert(TEnum value)
        {
            if (_hasCache
                && _namedSource == _source
                && EqualityComparer<TEnum>.Default.Equals(_cachedValue, value))
                return _cachedText;

            _cachedText = Build(value);
            _cachedValue = value;
            _hasCache = true;

            return _cachedText;
        }

        private string Build(TEnum value)
        {
            var names = Names();

            // Reading a value bit by bit only means anything on an enum whose members are bits. On
            // any other enum the number is one member's value, and splitting it would name whichever
            // members happen to sit inside it — so the plain name is the only sensible reading.
            if (!EnumBits<TEnum>.IsFlags)
            {
                var single = names.Convert(value);
                return string.IsNullOrEmpty(single) ? _noneText : single;
            }

            var remaining = EnumBits<TEnum>.BitsOf(value);
            if (remaining == 0) return _noneText;

            _builder ??= new StringBuilder();
            _builder.Clear();

            var members = EnumBits<TEnum>.Values;
            var bits = EnumBits<TEnum>.Bits;
            var written = 0;

            for (var i = 0; i < members.Length; i++)
            {
                // A member declared as zero names the absence of every flag, which the empty text
                // covers above. Matched here it would be named by every value instead.
                if (bits[i] == 0) continue;
                if ((remaining & bits[i]) != bits[i]) continue;

                if (written > 0) _builder.Append(_separator);
                _builder.Append(names.Convert(members[i]));

                remaining &= ~bits[i];
                written++;
            }

            // Bits no member declares have no name to give. Writing the leftover number instead would
            // put something in the middle of a sentence that reads as a bug rather than as data.
            return written == 0 ? _noneText : _builder.ToString();
        }

        // Held rather than rebuilt so the reflection behind InspectorName and Description is paid
        // once. The source is checked because it is serialized, so the Inspector can change it
        // between two pushes.
        private EnumToStringConverter<TEnum> Names()
        {
            if (_names is not null && _namedSource == _source) return _names;

            _namedSource = _source;
            return _names = new EnumToStringConverter<TEnum>(_source);
        }
    }
}
