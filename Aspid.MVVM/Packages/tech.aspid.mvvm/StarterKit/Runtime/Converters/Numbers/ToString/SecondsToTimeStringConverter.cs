#nullable enable
using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes a number of seconds as a clock reading.
    /// </summary>
    /// <remarks>A countdown usually wants <see cref="RoundMode.Ceil"/>, a stopwatch <see cref="RoundMode.Floor"/>.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Seconds To Time",
        Tooltip = "Writes a number of seconds as a clock reading")]
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

        [Tooltip("Shown for a duration still negative after rounding. When blank, negatives read as zero.")]
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

        private string Write(double seconds)
        {
            var rounded = Round(seconds);
            if (rounded < 0L && !string.IsNullOrWhiteSpace(_negativeText)) return _negativeText;

            var total = Math.Max(0L, rounded);

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
                _ => UndeclaredLayout(total, secs)
            };
        }

        private string UndeclaredLayout(long total, long secs)
        {
            this.LogError(
                problem: $"the layout {_layout.Describe()} is not a declared {nameof(TimeLayout)}",
                consequence: "Writing minutes and seconds.");

            return Lead(total / 60L) + _separator + Two(secs);
        }

        private long Round(double seconds) => _rounding switch
        {
            RoundMode.Round => (long)Math.Round(seconds, MidpointRounding.AwayFromZero),
            RoundMode.Floor => (long)Math.Floor(seconds),
            RoundMode.Ceil => (long)Math.Ceiling(seconds),
            RoundMode.Truncate => (long)Math.Truncate(seconds),
            _ => UndeclaredRounding(seconds)
        };

        private long UndeclaredRounding(double seconds)
        {
            this.LogError(
                problem: $"the rounding {_rounding.Describe()} is not a declared {nameof(RoundMode)}",
                consequence: "Rounding to the nearest second.");

            return (long)Math.Round(seconds, MidpointRounding.AwayFromZero);
        }

        private static TimeLayout AutoLayout(long days, long hours) => days > 0
            ? TimeLayout.DaysHoursMinutesSeconds
            : hours > 0
                ? TimeLayout.HoursMinutesSeconds
                : TimeLayout.MinutesSeconds;

        private string Lead(long value) => _padLeading
            ? Two(value)
            : value.ToString(CultureInfo.InvariantCulture);

        private static string Two(long value) =>
            value.ToString("00", CultureInfo.InvariantCulture);
    }
}
