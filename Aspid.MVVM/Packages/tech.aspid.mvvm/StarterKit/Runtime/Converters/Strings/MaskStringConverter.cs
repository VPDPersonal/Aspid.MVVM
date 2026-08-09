using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Hides the middle of a string, keeping a few characters at each end.
    /// </summary>
    /// <remarks>Account identifiers, e-mail addresses and promo codes shown in a settings screen.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Mask String", Tooltip = "Hides the middle of a string, keeping a few characters at each end")]
    public sealed class MaskStringConverter : IConverterString
    {
        [Tooltip("How many characters to leave visible at the start.")]
        [SerializeField] private int _visibleHead = 2;

        [Tooltip("How many characters to leave visible at the end.")]
        [SerializeField] private int _visibleTail = 2;

        [Tooltip("The character the hidden part is written with.")]
        [SerializeField] private char _maskChar = '•';

        /// <remarks>Default: showing two characters at each end.</remarks>
        public MaskStringConverter() { }

        /// <param name="visibleHead">How many characters to leave visible at the start.</param>
        /// <param name="visibleTail">How many characters to leave visible at the end.</param>
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
        /// value never leaks by being left alone.
        /// </returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var head = Math.Max(0, _visibleHead);
            var tail = Math.Max(0, _visibleTail);

            if (head + tail >= value!.Length) return new string(_maskChar, value.Length);

            return value[..head] + new string(_maskChar, value.Length - head - tail) + value[^tail..];
        }
    }
}
