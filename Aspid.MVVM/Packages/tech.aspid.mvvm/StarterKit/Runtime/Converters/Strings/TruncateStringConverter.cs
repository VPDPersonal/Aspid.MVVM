using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Shortens a string that is too long to fit.
    /// </summary>
    /// <remarks>
    /// Player names, chat lines and item descriptions arrive at whatever length their author chose,
    /// and a fixed-width control will either clip them without warning or push its neighbours out of
    /// place.
    /// </remarks>
    [Serializable]
    public sealed class TruncateStringConverter : IConverterString
    {
        [Tooltip("The longest string allowed through, ellipsis included.")]
        [SerializeField] private int _maxLength = 20;

        [Tooltip("Appended where the string was cut.")]
        [SerializeField] private string _ellipsis = "…";

        [Tooltip("Which end to cut from.")]
        [SerializeField] private TruncateSide _side = TruncateSide.End;

        [Tooltip("Cut at a space rather than mid-word.")]
        [SerializeField] private bool _atWordBoundary;

        /// <remarks>Default: cutting at twenty characters.</remarks>
        public TruncateStringConverter() { }

        /// <param name="maxLength">The longest string allowed through, ellipsis included.</param>
        /// <param name="side">Which end to cut from.</param>
        /// <param name="atWordBoundary">If <see langword="true"/>, cuts at a space rather than mid-word.</param>
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
        /// <returns>The string, no longer than the limit.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the side is not a declared value.</exception>
        public string? Convert(string? value)
        {
            if (value is null || _maxLength <= 0 || value.Length <= _maxLength) return value;

            var ellipsis = _ellipsis ?? string.Empty;

            // Nothing sensible to keep once the marker fills the budget.
            if (ellipsis.Length >= _maxLength) return ellipsis[.._maxLength];

            var keep = _maxLength - ellipsis.Length;

            return _side switch
            {
                TruncateSide.End => Cut(value, keep) + ellipsis,
                TruncateSide.Start => ellipsis + value[^keep..],
                TruncateSide.Middle => value[..((keep + 1) / 2)] + ellipsis + value[^(keep / 2)..],
                _ => throw new ArgumentOutOfRangeException(nameof(_side), _side, null)
            };
        }

        private string Cut(string value, int keep)
        {
            var head = value[..keep];
            if (!_atWordBoundary) return head;

            var space = head.LastIndexOf(' ');
            return space > 0 ? head[..space] : head;
        }
    }
}
