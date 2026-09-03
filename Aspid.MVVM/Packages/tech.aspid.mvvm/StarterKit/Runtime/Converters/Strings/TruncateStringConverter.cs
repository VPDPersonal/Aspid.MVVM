#nullable enable
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
        [SerializeField] private string? _ellipsis = "…";

        [Tooltip("Which end to cut from.")]
        [SerializeField] private TruncateSide _side = TruncateSide.End;

        [Tooltip("Cut at a space rather than mid-word. Honored by the End side only.")]
        [SerializeField] private bool _atWordBoundary;

        /// <remarks>Default: cutting at twenty characters.</remarks>
        public TruncateStringConverter() { }

        /// <param name="maxLength">The longest string allowed through, ellipsis included.</param>
        /// <param name="side">Which end to cut from.</param>
        /// <param name="atWordBoundary">If <see langword="true"/>, cuts at a space rather than mid-word. <see cref="TruncateSide.End"/> only.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxLength"/> is not positive.</exception>
        public TruncateStringConverter(
            int maxLength,
            TruncateSide side = TruncateSide.End,
            bool atWordBoundary = false)
        {
            _side = side;
            _atWordBoundary = atWordBoundary;
            _maxLength = maxLength > 0 ? maxLength : throw new ArgumentOutOfRangeException(nameof(maxLength));
        }

        /// <summary>
        /// Shortens the specified string if it exceeds the limit.
        /// </summary>
        /// <param name="value">The string to shorten.</param>
        /// <returns>The string, no longer than the limit. An undeclared side reports an error and returns the value unchanged.</returns>
        /// <remarks>Cuts never split a surrogate pair.</remarks>
        public string? Convert(string? value)
        {
            if (value is null) return null;
            if (value.Length <= _maxLength) return value;

            var ellipsis = _ellipsis ?? string.Empty;
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
            this.LogError(
                problem: $"the side {_side.Describe()} is not a declared {nameof(TruncateSide)}",
                consequence: "Returning the value unchanged.");

            return value;
        }

        private string CutEnd(string value, int keep)
        {
            var end = Head(value, keep);

            if (!_atWordBoundary || end == 0) return value[..end];

            var space = value.LastIndexOf(' ', end - 1);
            return value[..(space > 0 ? space : end)];
        }

        private static int Head(string value, int length) => SplitsAPair(value, length)
            ? length - 1
            : length;

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
