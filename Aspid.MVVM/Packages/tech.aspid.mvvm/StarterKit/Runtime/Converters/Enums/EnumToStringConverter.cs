using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts an enum value to text.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being converted.</typeparam>
    /// <remarks>
    /// Labelling a state without this means either a switch in the ViewModel or a member name leaking
    /// into the UI. <see cref="EnumNameSource.InspectorName"/> reads the attribute Unity already uses
    /// for the same purpose in the Inspector, and <see cref="EnumNameSource.Description"/> the one a
    /// domain assembly can carry without a reference to UnityEngine.
    /// <para>
    /// The text is metadata, not formatting, so there is no culture setting here: a member name, an
    /// <c>InspectorName</c> and a <c>Description</c> are all authored once in the source, and
    /// <c>Enum.ToString</c> ignores any format provider it is handed. Text that has to follow the
    /// player's locale belongs in a string table — <c>LocalizedEnumConverter</c> reads one.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Enum", Name = "Enum To String", Tooltip = "Converts an enum value to text")]
    public sealed class EnumToStringConverter<TEnum> : IConverter<TEnum, string>
        where TEnum : struct, Enum
    {
        [Tooltip("Where the text comes from.")]
        [SerializeField] private EnumNameSource _source;

        [Tooltip("Returned for a value that is not a declared member. The Raw source never needs it — it writes such a value as its number.")]
        [SerializeField] private string _fallback = string.Empty;

        public EnumToStringConverter() { }

        /// <param name="source">Where the text comes from.</param>
        /// <param name="fallback">Returned for a value that is not a declared member.</param>
        public EnumToStringConverter(EnumNameSource source, string fallback = "")
        {
            _source = source;
            _fallback = fallback;
        }

        /// <summary>
        /// Converts the specified enum value to text.
        /// </summary>
        /// <param name="value">The enum value to convert.</param>
        /// <returns>
        /// The member's text, or the fallback when it is not a declared member — except under
        /// <see cref="EnumNameSource.Raw"/>, which writes such a value as its number.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the source is not a declared value.</exception>
        public string Convert(TEnum value)
        {
            // ToString boxes the value to reach Enum's override. It is also the only source that can
            // name a flag combination or an undeclared number, neither of which the metadata holds.
            if (_source is EnumNameSource.Raw) return value.ToString();

            var index = EnumMembers<TEnum>.IndexOf(value);
            return index < 0 ? _fallback : EnumMembers<TEnum>.Label(index, _source);
        }
    }
}
