using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes how long ago — or how far ahead — a moment is.
    /// </summary>
    /// <remarks>
    /// Mail, inboxes, friend lists. The unit names are authored so the text can be translated without
    /// touching code; the default set is English.
    /// </remarks>
    [Serializable]
    public sealed class RelativeTimeConverter : IConverter<DateTime, string>
    {
        [Tooltip("Names for second, minute, hour, day. Longer spans use days.")]
        [SerializeField] private string[] _unitNames = { "s", "m", "h", "d" };

        [Tooltip("A composite format for a past moment: {0} is the amount, {1} the unit.")]
        [SerializeField] private string _pastFormat = "{0}{1} ago";

        [Tooltip("A composite format for a future moment: {0} is the amount, {1} the unit.")]
        [SerializeField] private string _futureFormat = "in {0}{1}";

        [Tooltip("Shown when the moment is within a second of now.")]
        [SerializeField] private string _nowText = "now";

        [Tooltip("Compare against UTC rather than local time.")]
        [SerializeField] private bool _useUtcNow;

        /// <remarks>Default: with English defaults.</remarks>
        public RelativeTimeConverter() { }

        /// <summary>
        /// Writes how far the specified moment is from now.
        /// </summary>
        /// <param name="value">The moment to describe.</param>
        /// <returns>The description.</returns>
        public string Convert(DateTime value)
        {
            var now = _useUtcNow ? DateTime.UtcNow : DateTime.Now;
            var delta = value - now;
            var magnitude = delta.Duration();

            if (magnitude.TotalSeconds < 1d) return _nowText;

            var (amount, unit) = magnitude.TotalSeconds switch
            {
                < 60d => ((long)magnitude.TotalSeconds, Unit(0)),
                < 3600d => ((long)magnitude.TotalMinutes, Unit(1)),
                < 86400d => ((long)magnitude.TotalHours, Unit(2)),
                _ => ((long)magnitude.TotalDays, Unit(3)),
            };

            var format = delta.Ticks < 0 ? _pastFormat : _futureFormat;
            return string.Format(CultureInfo.InvariantCulture, format, amount, unit);
        }

        private string Unit(int index) =>
            _unitNames is { Length: > 0 } && index < _unitNames.Length ? _unitNames[index] : string.Empty;
    }
}
