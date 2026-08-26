using System;
using System.Text;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Names the flags a value carries.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being named.</typeparam>
    /// <remarks>
    /// Each named flag consumes its bits, so a composite member is named only when its parts are not
    /// declared members. On an enum not marked <see cref="FlagsAttribute"/> the value is named whole.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Enum/To String",
        Name = "Flags",
        Tooltip = "Names the flags a value carries")]
    public class EnumFlagsToStringConverter<TEnum> : IConverter<TEnum, string>, ISerializationCallbackReceiver
        where TEnum : struct, Enum
    {
        [Tooltip("Placed between the named flags. Unused on an enum not marked [Flags].")]
        [SerializeField] private string _separator = ", ";

        [Tooltip("Where the name of each flag comes from.")]
        [SerializeField] private EnumNameSource _source;

        [Tooltip("Shown when the value names no flags. " +
            "Unused under the Raw source on an enum not marked [Flags], which writes an undeclared value as its number.")]
        [SerializeField] private string _noneText = string.Empty;

        [NonSerialized] private StringBuilder? _builder;
        [NonSerialized] private EnumToStringConverter<TEnum>? _names;

        [NonSerialized] private bool _hasCache;
        [NonSerialized] private TEnum _cachedValue;
        [NonSerialized] private string _cachedText = string.Empty;

        /// <remarks>Default: joining with commas.</remarks>
        public EnumFlagsToStringConverter() { }

        /// <param name="separator">
        /// Placed between the named flags. Unused on an enum not marked <see cref="FlagsAttribute"/>.
        /// </param>
        /// <param name="source">Where the name of each flag comes from.</param>
        /// <param name="noneText">
        /// Shown when the value names no flags. Unused under <see cref="EnumNameSource.Raw"/> on an
        /// enum not marked <see cref="FlagsAttribute"/>, which writes an undeclared value as its
        /// number.
        /// </param>
        public EnumFlagsToStringConverter(
            string separator,
            EnumNameSource source = EnumNameSource.Name,
            string noneText = "")
        {
            _source = source;
            _noneText = noneText;
            _separator = separator;
        }

        /// <summary>
        /// Names the flags the specified value carries.
        /// </summary>
        /// <param name="value">The value to take apart.</param>
        /// <returns>
        /// The flag names joined by the separator, or the none text when it carries none. A bit no
        /// member declares is dropped without a report.
        /// </returns>
        public string Convert(TEnum value)
        {
            if (_hasCache && EqualityComparer<TEnum>.Default.Equals(_cachedValue, value))
                return _cachedText;

            _cachedText = Build(value);
            _cachedValue = value;
            _hasCache = true;

            return _cachedText;
        }

        private string Build(TEnum value)
        {
            var names = Names();

            // Splitting a non-flags enum would name whichever members happen to sit inside its number.
            if (!EnumBits<TEnum>.IsFlags)
            {
                var single = names.Convert(value);
                return string.IsNullOrWhiteSpace(single) ? _noneText : single;
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
                // A zero member would otherwise be named by every value.
                if (bits[i] == 0) continue;
                if ((remaining & bits[i]) != bits[i]) continue;

                if (written > 0) _builder.Append(_separator);
                _builder.Append(names.Convert(members[i]));

                remaining &= ~bits[i];
                written++;
            }

            // Bits no member declares have no name to give.
            return written == 0
                ? _noneText
                : _builder.ToString();
        }

        private EnumToStringConverter<TEnum> Names() =>
            _names ??= new EnumToStringConverter<TEnum>(_source);

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _names = null;
            _hasCache = false;
        }
    }
}
