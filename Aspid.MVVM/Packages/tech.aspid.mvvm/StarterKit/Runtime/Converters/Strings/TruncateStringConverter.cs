using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Shortens a string that is too long to fit.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Truncate",
        Tooltip = "Shortens a string that is too long to fit")]
    public sealed class TruncateStringConverter : IConverter<string?, string?>
    {
        [Tooltip("The longest string allowed through, ellipsis included.")]
        [SerializeField] [Min(1)] private int _maxLength = 20;

        [Tooltip("Appended where the string was cut.")]
        [SerializeField] private string _ellipsis = "…";

        [Tooltip("Which end to cut from.")]
        [SerializeField] private TruncateSide _side = TruncateSide.End;

        [Tooltip("Cut at a space rather than mid-word. Honored by the End side only.")]
        [SerializeField] private bool _atWordBoundary;

        /// <remarks>Default: cutting at twenty characters.</remarks>
        public TruncateStringConverter() { }

        /// <param name="maxLength">
        /// The longest string allowed through, ellipsis included. A limit of zero or less is
        /// reported and leaves the string as it is.
        /// </param>
        /// <param name="side">Which end to cut from.</param>
        /// <param name="atWordBoundary">
        /// If <see langword="true"/>, cuts at a space rather than mid-word. Honored by
        /// <see cref="TruncateSide.End"/> only.
        /// </param>
        public TruncateStringConverter(int maxLength, TruncateSide side = TruncateSide.End, bool atWordBoundary = false)
        {
            _maxLength = maxLength;
            _side = side;
            _atWordBoundary = atWordBoundary;
        }

        /// <summary>
        /// Shortens the specified string if it exceeds the limit.
        /// </summary>
        /// <param name="value">The string to shorten.</param>
        /// <returns>
        /// The string, no longer than the limit; or the string unchanged when the limit is not
        /// positive or the side is not a declared value.
        /// </returns>
        /// <remarks>
        /// Cuts never split a surrogate pair — such a character is kept or dropped whole.
        /// </remarks>
        public string? Convert(string? value)
        {
            if (value is null) return null;

            // The constructor is free to hand in a limit no string can be shortened to.
            if (_maxLength <= 0)
            {
                this.LogError($"the length to cut at is not positive ({_maxLength})",
                    "Returning the string unshortened.");

                return value;
            }

            if (value.Length <= _maxLength) return value;

            var ellipsis = _ellipsis ?? string.Empty;

            // Nothing sensible to keep once the marker fills the budget.
            if (ellipsis.Length >= _maxLength) return ellipsis[..Head(ellipsis, _maxLength)];

            var keep = _maxLength - ellipsis.Length;

            return _side switch
            {
                TruncateSide.End => CutEnd(value, keep) + ellipsis,
                TruncateSide.Start => ellipsis + value[Tail(value, keep)..],
                TruncateSide.Middle => value[..Head(value, (keep + 1) / 2)] +
                    ellipsis +
                    value[Tail(value, keep / 2)..],
                _ => Undeclared(value)
            };
        }

        private string? Undeclared(string? value)
        {
            this.LogError($"the side {_side.Describe()} is not a declared {nameof(TruncateSide)}",
                "Returning the value unchanged.");

            return value;
        }

        // Looked for in the string itself: a cut copy would be thrown away whenever a space is found.
        private string CutEnd(string value, int keep)
        {
            var end = Head(value, keep);

            // Head can back the cut off to zero, and LastIndexOf throws on a negative start index.
            if (!_atWordBoundary || end == 0) return value[..end];

            var space = value.LastIndexOf(' ', end - 1);
            return value[..(space > 0 ? space : end)];
        }

        // Where the kept head ends: back off a lone surrogate half so the character is dropped whole.
        private static int Head(string value, int length) => SplitsAPair(value, length) ? length - 1 : length;

        // Where the kept tail starts, moved the other way for the same reason.
        private static int Tail(string value, int length)
        {
            var start = value.Length - length;
            return SplitsAPair(value, start) ? start + 1 : start;
        }

        private static bool SplitsAPair(string value, int index) =>
            index > 0
            && index < value.Length
            && char.IsLowSurrogate(value[index])
            && char.IsHighSurrogate(value[index - 1]);
    }
}
