using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads an enum member out of text.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being read.</typeparam>
    /// <remarks>State names arriving from a backend or a configuration file.</remarks>
    [Serializable]
    public sealed class StringToEnumConverter<TEnum> : ITwoWayConverter<string?, TEnum>
        where TEnum : struct, Enum
    {
        [Tooltip("Match member names without regard to case.")]
        [SerializeField] private bool _ignoreCase = true;

        [Tooltip("Returned when the text names no member.")]
        [SerializeField] private TEnum _fallback;

        public StringToEnumConverter() { }

        /// <param name="fallback">Returned when the text names no member.</param>
        /// <param name="ignoreCase">Whether to match without regard to case.</param>
        public StringToEnumConverter(TEnum fallback, bool ignoreCase = true)
        {
            _fallback = fallback;
            _ignoreCase = ignoreCase;
        }

        /// <summary>
        /// Reads an enum member out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The member, or the fallback when the text names none.</returns>
        public TEnum Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return _fallback;

            // Enum.TryParse accepts a bare number and returns an undeclared member for it, which is
            // rarely what a name-shaped input means.
            return Enum.TryParse<TEnum>(value, _ignoreCase, out var parsed) && Enum.IsDefined(typeof(TEnum), parsed)
                ? parsed
                : _fallback;
        }

        /// <summary>
        /// Writes the specified member as text.
        /// </summary>
        /// <param name="value">The member to write.</param>
        /// <returns>Its name.</returns>
        public string ConvertBack(TEnum value) => value.ToString();
    }
}
