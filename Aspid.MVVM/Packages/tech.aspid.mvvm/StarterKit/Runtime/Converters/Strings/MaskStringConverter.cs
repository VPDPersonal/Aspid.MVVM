using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Hides the middle of a string, keeping a few characters at each end.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Mask",
        Tooltip = "Hides the middle of a string, keeping a few characters at each end")]
    public sealed class MaskStringConverter : IConverter<string?, string?>
    {
        [Tooltip("How many characters to leave visible at the start.")]
        [SerializeField] [Min(0)] private int _visibleHead = 2;

        [Tooltip("How many characters to leave visible at the end.")]
        [SerializeField] [Min(0)] private int _visibleTail = 2;

        [Tooltip("The character the hidden part is written with.")]
        [SerializeField] private char _maskChar = '•';

        /// <remarks>Default: showing two characters at each end.</remarks>
        public MaskStringConverter() { }

        /// <param name="visibleHead">
        /// How many characters to leave visible at the start. Below zero is read as zero.
        /// </param>
        /// <param name="visibleTail">
        /// How many characters to leave visible at the end. Below zero is read as zero.
        /// </param>
        /// <param name="maskChar">The character the hidden part is written with.</param>
        public MaskStringConverter(int visibleHead, int visibleTail, char maskChar = '•')
        {
            _visibleHead = visibleHead;
            _visibleTail = visibleTail;
            _maskChar = maskChar;
        }

        /// <summary>
        /// Masks the middle of the specified string.
        /// </summary>
        /// <param name="value">The string to mask.</param>
        /// <returns>
        /// The masked string. A string too short to keep both ends is masked completely, so a short
        /// value never leaks by being left alone; a blank string, spaces included, comes back unmasked.
        /// </returns>
        /// <remarks>
        /// A visible count landing inside a surrogate pair hides the whole character.
        /// </remarks>
        public string? Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var head = Math.Max(0, _visibleHead);
            var tail = Math.Max(0, _visibleTail);

            // Both cuts move toward the hidden middle rather than splitting the pair they land in.
            if (SplitsAPair(value, head)) head--;
            if (SplitsAPair(value, value.Length - tail)) tail--;

            if (head + tail >= value.Length) return new string(_maskChar, value.Length);

            return value[..head] + new string(_maskChar, value.Length - head - tail) + value[^tail..];
        }

        // Whether a cut here would split a surrogate pair, leaving a lone half that renders as a box
        // and a fragment of the value the converter was asked to hide.
        private static bool SplitsAPair(string value, int index) =>
            index > 0
            && index < value.Length
            && char.IsLowSurrogate(value[index])
            && char.IsHighSurrogate(value[index - 1]);
    }
}
