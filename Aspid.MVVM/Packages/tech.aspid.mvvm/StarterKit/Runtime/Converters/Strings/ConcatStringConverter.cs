using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a string in authored text, and takes that text back off.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Concat",
        Tooltip = "Wraps a string in authored text")]
    public sealed class ConcatStringConverter : ITwoWayConverter<string?, string?>
    {
        [Tooltip("Placed before the value.")]
        [SerializeField] private string _prefix = string.Empty;

        [Tooltip("Placed after the value.")]
        [SerializeField] private string _suffix = string.Empty;

        [Tooltip("Leave a blank value undecorated.")]
        [SerializeField] private bool _skipWhenEmpty = true;

        /// <remarks>Default: with no text to wrap the value in.</remarks>
        public ConcatStringConverter() { }

        /// <param name="prefix">Placed before the value.</param>
        /// <param name="suffix">Placed after the value.</param>
        /// <param name="skipWhenEmpty">If <see langword="true"/>, leaves a blank value undecorated.</param>
        public ConcatStringConverter(string prefix, string suffix, bool skipWhenEmpty = true)
        {
            _prefix = prefix;
            _suffix = suffix;
            _skipWhenEmpty = skipWhenEmpty;
        }

        /// <summary>
        /// Wraps the specified string.
        /// </summary>
        /// <param name="value">The string to wrap.</param>
        /// <returns>The wrapped string, or the value unchanged when it is blank and that is configured.</returns>
        public string? Convert(string? value)
        {
            if (_skipWhenEmpty && string.IsNullOrWhiteSpace(value)) return value;
            return _prefix + value + _suffix;
        }

        /// <summary>
        /// Takes the authored text back off the specified string.
        /// </summary>
        /// <param name="value">The string to undecorate.</param>
        /// <returns>
        /// The string without the prefix and the suffix; text carrying neither comes back unchanged.
        /// A value left with nothing between the two comes back as an empty string, never as
        /// <see langword="null"/>: <see cref="Convert"/> wraps a <see langword="null"/> and an empty
        /// value alike when blank values are not skipped, so the round trip cannot tell them apart.
        /// </returns>
        public string? ConvertBack(string? value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var start = 0;
            var end = value.Length;

            if (!string.IsNullOrEmpty(_prefix) && value.StartsWith(_prefix, StringComparison.Ordinal))
                start = _prefix.Length;

            // Measured against what is left after the prefix, so the two cannot claim the same characters.
            if (!string.IsNullOrEmpty(_suffix)
                && end - start >= _suffix.Length
                && string.CompareOrdinal(value, end - _suffix.Length, _suffix, 0, _suffix.Length) == 0)
                end -= _suffix.Length;

            return value[start..end];
        }
    }
}
