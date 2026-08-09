using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes a number of seconds as a clock reading.
    /// </summary>
    /// <remarks>
    /// Rounding direction matters more than it looks: a floored timer shows <c>0:00</c> for a whole
    /// second before it fires, so a countdown usually wants <see cref="RoundMode.Ceil"/> while a
    /// stopwatch wants <see cref="RoundMode.Floor"/>.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Time", Name = "Seconds To Time String", Tooltip = "Writes a number of seconds as a clock reading")]
    public sealed class SecondsToTimeStringConverter :
        IConverter<float, string>,
        IConverter<double, string>,
        IConverter<int, string>
    {
        [Tooltip("Which units to show.")]
        [SerializeField] private TimeLayout _layout = TimeLayout.MinutesSeconds;

        [Tooltip("How to drop the fractional second. A countdown usually wants Ceil.")]
        [SerializeField] private RoundMode _rounding = RoundMode.Ceil;

        [Tooltip("The character between units.")]
        [SerializeField] private char _separator = ':';

        [Tooltip("Pad the leading unit to two digits.")]
        [SerializeField] private bool _padLeading = true;

        [Tooltip("Shown for a negative duration. When empty, negatives are treated as zero.")]
        [SerializeField] private string _negativeText = string.Empty;

        /// <remarks>Default: writing mm:ss.</remarks>
        public SecondsToTimeStringConverter() { }

        /// <param name="layout">Which units to show.</param>
        /// <param name="rounding">How to drop the fractional second.</param>
        /// <param name="padLeading">If <see langword="true"/>, pads the leading unit to two digits.</param>
        public SecondsToTimeStringConverter(
            TimeLayout layout,
            RoundMode rounding = RoundMode.Ceil,
            bool padLeading = true)
        {
            _layout = layout;
            _rounding = rounding;
            _padLeading = padLeading;
        }

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(float value) => Write(value);

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(double value) => Write(value);

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(int value) => Write(value);

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the rounding or layout is not a declared value.</exception>
        private string Write(double seconds)
        {
            if (seconds < 0d && !string.IsNullOrEmpty(_negativeText)) return _negativeText;

            var total = Math.Max(0L, Round(seconds));

            var days = total / 86400L;
            var hours = total % 86400L / 3600L;
            var minutes = total % 3600L / 60L;
            var secs = total % 60L;

            var layout = _layout is TimeLayout.Auto ? AutoLayout(days, hours) : _layout;

            return layout switch
            {
                TimeLayout.Seconds => Lead(total),
                TimeLayout.MinutesSeconds => Lead(total / 60L) + _separator + Two(secs),
                TimeLayout.HoursMinutesSeconds => Lead(total / 3600L) + _separator + Two(minutes) + _separator + Two(secs),
                TimeLayout.DaysHoursMinutesSeconds => Lead(days) + _separator + Two(hours) + _separator + Two(minutes) + _separator + Two(secs),
                _ => throw new ArgumentOutOfRangeException(nameof(_layout), _layout, null)
            };
        }

        private long Round(double seconds) => _rounding switch
        {
            RoundMode.Round => (long)Math.Round(seconds, MidpointRounding.AwayFromZero),
            RoundMode.Floor => (long)Math.Floor(seconds),
            RoundMode.Ceil => (long)Math.Ceiling(seconds),
            RoundMode.Truncate => (long)Math.Truncate(seconds),
            _ => throw new ArgumentOutOfRangeException(nameof(_rounding), _rounding, null)
        };

        private static TimeLayout AutoLayout(long days, long hours) => days > 0
            ? TimeLayout.DaysHoursMinutesSeconds
            : hours > 0
                ? TimeLayout.HoursMinutesSeconds
                : TimeLayout.MinutesSeconds;

        private string Lead(long value) =>
            _padLeading ? Two(value) : value.ToString(CultureInfo.InvariantCulture);

        private static string Two(long value) =>
            value.ToString("00", CultureInfo.InvariantCulture);
    }
}
