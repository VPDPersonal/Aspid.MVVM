using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The shape <see cref="SecondsToTimeStringConverter"/> writes a duration in.
    /// </summary>
    public enum TimeLayout
    {
        /// <summary>Seconds only.</summary>
        Seconds,

        /// <summary>mm:ss.</summary>
        MinutesSeconds,

        /// <summary>h:mm:ss.</summary>
        HoursMinutesSeconds,

        /// <summary>d:hh:mm:ss.</summary>
        DaysHoursMinutesSeconds,

        /// <summary>The shortest layout that fits the value.</summary>
        Auto,
    }

    /// <summary>
    /// Writes a number of seconds as a clock reading.
    /// </summary>
    /// <remarks>
    /// Rounding direction matters more than it looks: a floored timer shows <c>0:00</c> for a whole
    /// second before it fires, so a countdown usually wants <see cref="RoundMode.Ceil"/> while a
    /// stopwatch wants <see cref="RoundMode.Floor"/>.
    /// </remarks>
    [Serializable]
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

        /// <summary>
        /// Initializes a new instance of the <see cref="SecondsToTimeStringConverter"/> class writing mm:ss.
        /// </summary>
        public SecondsToTimeStringConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecondsToTimeStringConverter"/> class.
        /// </summary>
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

    /// <summary>
    /// Formats a <see cref="TimeSpan"/> with a real <see cref="TimeSpan"/> format string.
    /// </summary>
    /// <remarks>
    /// <see cref="TimeSpanToStringConverter"/> takes a <i>composite</i> format, so the obvious
    /// <c>mm\:ss</c> comes back as itself and the pattern has to be wrapped as <c>{0:mm\:ss}</c>.
    /// This takes the pattern directly, the way <see cref="TimeSpan.ToString(string)"/> does.
    /// </remarks>
    [Serializable]
    public sealed class TimeSpanFormatConverter : IConverterTimeSpanToString
    {
        [Tooltip(@"A TimeSpan format string, for example mm\:ss or hh\:mm\:ss.")]
        [SerializeField] private string _format = @"mm\:ss";

        [Tooltip("The culture the duration is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [NonSerialized] private bool _loggedFormatFailure;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanFormatConverter"/> class writing mm:ss.
        /// </summary>
        public TimeSpanFormatConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanFormatConverter"/> class.
        /// </summary>
        /// <param name="format">A <see cref="TimeSpan"/> format string.</param>
        public TimeSpanFormatConverter(string format)
        {
            _format = format;
        }

        /// <summary>
        /// Formats the specified duration.
        /// </summary>
        /// <param name="value">The duration to format.</param>
        /// <returns>The formatted duration, or the default rendering when the format is unusable.</returns>
        public string Convert(TimeSpan value)
        {
            if (string.IsNullOrWhiteSpace(_format)) return value.ToString();

            try
            {
                return value.ToString(_format, _culture.ToCultureInfo());
            }
            catch (FormatException exception)
            {
                LogFormatFailure(exception);
                return value.ToString();
            }
        }

        private void LogFormatFailure(FormatException exception)
        {
            if (_loggedFormatFailure) return;
            _loggedFormatFailure = true;

            Debug.LogError(
                $"{nameof(TimeSpanFormatConverter)}: \"{_format}\" is not a TimeSpan format "
                + $"({exception.Message}). Falling back to the default rendering.");
        }
    }

    /// <summary>
    /// Converts a number of seconds to a <see cref="TimeSpan"/>.
    /// </summary>
    [Serializable]
    public sealed class SecondsToTimeSpanConverter : ITwoWayConverter<float, TimeSpan>
    {
        /// <summary>
        /// Converts the specified seconds to a duration.
        /// </summary>
        /// <param name="value">The number of seconds.</param>
        /// <returns>The duration.</returns>
        public TimeSpan Convert(float value) => TimeSpan.FromSeconds(value);

        /// <summary>
        /// Converts a duration back to seconds.
        /// </summary>
        /// <param name="value">The duration.</param>
        /// <returns>The number of seconds.</returns>
        public float ConvertBack(TimeSpan value) => (float)value.TotalSeconds;
    }

    /// <summary>
    /// The unit <see cref="TimeSpanToNumberConverter"/> measures a duration in.
    /// </summary>
    public enum TimeUnit
    {
        /// <summary>Whole seconds within the minute.</summary>
        Seconds,

        /// <summary>The duration in seconds.</summary>
        TotalSeconds,

        /// <summary>The duration in minutes.</summary>
        TotalMinutes,

        /// <summary>The duration in hours.</summary>
        TotalHours,

        /// <summary>The duration in days.</summary>
        TotalDays,
    }

    /// <summary>
    /// Measures a <see cref="TimeSpan"/> as a number.
    /// </summary>
    /// <remarks>For feeding a duration into a slider or a fill amount.</remarks>
    [Serializable]
    public sealed class TimeSpanToNumberConverter : IConverter<TimeSpan, float>
    {
        [Tooltip("Which unit to measure in.")]
        [SerializeField] private TimeUnit _unit = TimeUnit.TotalSeconds;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanToNumberConverter"/> class measuring in seconds.
        /// </summary>
        public TimeSpanToNumberConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanToNumberConverter"/> class.
        /// </summary>
        /// <param name="unit">Which unit to measure in.</param>
        public TimeSpanToNumberConverter(TimeUnit unit)
        {
            _unit = unit;
        }

        /// <summary>
        /// Measures the specified duration.
        /// </summary>
        /// <param name="value">The duration to measure.</param>
        /// <returns>The measurement.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the unit is not a declared value.</exception>
        public float Convert(TimeSpan value) => _unit switch
        {
            TimeUnit.Seconds => value.Seconds,
            TimeUnit.TotalSeconds => (float)value.TotalSeconds,
            TimeUnit.TotalMinutes => (float)value.TotalMinutes,
            TimeUnit.TotalHours => (float)value.TotalHours,
            TimeUnit.TotalDays => (float)value.TotalDays,
            _ => throw new ArgumentOutOfRangeException(nameof(_unit), _unit, null)
        };
    }

    /// <summary>
    /// Converts a Unix timestamp to a <see cref="DateTime"/>.
    /// </summary>
    /// <remarks>A backend that speaks epoch seconds, which most do.</remarks>
    [Serializable]
    public sealed class UnixTimestampToDateTimeConverter : ITwoWayConverter<long, DateTime>
    {
        [Tooltip("The timestamp is in milliseconds rather than seconds.")]
        [SerializeField] private bool _milliseconds;

        [Tooltip("Produce a UTC time rather than a local one.")]
        [SerializeField] private bool _utc;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnixTimestampToDateTimeConverter"/> class reading local seconds.
        /// </summary>
        public UnixTimestampToDateTimeConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnixTimestampToDateTimeConverter"/> class.
        /// </summary>
        /// <param name="milliseconds">Whether the timestamp is in milliseconds.</param>
        /// <param name="utc">Whether to produce a UTC time.</param>
        public UnixTimestampToDateTimeConverter(bool milliseconds, bool utc = false)
        {
            _milliseconds = milliseconds;
            _utc = utc;
        }

        /// <summary>
        /// Converts the specified timestamp to a date and time.
        /// </summary>
        /// <param name="value">The timestamp.</param>
        /// <returns>The date and time.</returns>
        public DateTime Convert(long value)
        {
            var offset = _milliseconds
                ? DateTimeOffset.FromUnixTimeMilliseconds(value)
                : DateTimeOffset.FromUnixTimeSeconds(value);

            return _utc ? offset.UtcDateTime : offset.LocalDateTime;
        }

        /// <summary>
        /// Converts a date and time back to a timestamp.
        /// </summary>
        /// <param name="value">The date and time.</param>
        /// <returns>The timestamp.</returns>
        public long ConvertBack(DateTime value)
        {
            var offset = new DateTimeOffset(value.ToUniversalTime());
            return _milliseconds ? offset.ToUnixTimeMilliseconds() : offset.ToUnixTimeSeconds();
        }
    }

    /// <summary>
    /// Formats a <see cref="DateTime"/>.
    /// </summary>
    [Serializable]
    public sealed class DateTimeFormatConverter : IConverter<DateTime, string>
    {
        [Tooltip("A DateTime format string, for example dd.MM.yyyy or HH:mm.")]
        [SerializeField] private string _format = "g";

        [Tooltip("Convert to local time before formatting.")]
        [SerializeField] private bool _toLocalTime;

        [Tooltip("The culture the date is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeFormatConverter"/> class with the general format.
        /// </summary>
        public DateTimeFormatConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeFormatConverter"/> class.
        /// </summary>
        /// <param name="format">A <see cref="DateTime"/> format string.</param>
        /// <param name="toLocalTime">Whether to convert to local time before formatting.</param>
        public DateTimeFormatConverter(string format, bool toLocalTime = false)
        {
            _format = format;
            _toLocalTime = toLocalTime;
        }

        /// <summary>
        /// Formats the specified date and time.
        /// </summary>
        /// <param name="value">The date and time to format.</param>
        /// <returns>The formatted date.</returns>
        public string Convert(DateTime value)
        {
            var moment = _toLocalTime ? value.ToLocalTime() : value;
            var culture = _culture.ToCultureInfo();

            return string.IsNullOrWhiteSpace(_format) ? moment.ToString(culture) : moment.ToString(_format, culture);
        }
    }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeTimeConverter"/> class with English defaults.
        /// </summary>
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

    /// <summary>
    /// Compares a <see cref="DateTime"/> with a reference moment.
    /// </summary>
    /// <remarks>Gating on "the event has started" or "the cooldown has expired".</remarks>
    [Serializable]
    public sealed class DateTimeToBoolConverter : IConverter<DateTime, bool>
    {
        [Tooltip("How the bound moment is compared with the reference.")]
        [SerializeField] private Comparisons _comparison = Comparisons.GreaterThan;

        [Tooltip("Compare against the current time rather than the moment below.")]
        [SerializeField] private bool _compareToNow = true;

        [Tooltip("Use UTC when comparing against the current time.")]
        [SerializeField] private bool _useUtcNow;

        [Tooltip("Ticks of the moment compared against when not using the current time.")]
        [SerializeField] private long _referenceTicks;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeToBoolConverter"/> class comparing against now.
        /// </summary>
        public DateTimeToBoolConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeToBoolConverter"/> class.
        /// </summary>
        /// <param name="comparison">How the bound moment is compared with the reference.</param>
        /// <param name="reference">The moment compared against. When <see langword="null"/>, the current time is used.</param>
        public DateTimeToBoolConverter(Comparisons comparison, DateTime? reference = null)
        {
            _comparison = comparison;
            _compareToNow = reference is null;
            _referenceTicks = reference?.Ticks ?? 0L;
        }

        /// <summary>
        /// Compares the specified moment with the reference.
        /// </summary>
        /// <param name="value">The moment to compare.</param>
        /// <returns>The result of the comparison.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the comparison is not a declared value.</exception>
        public bool Convert(DateTime value)
        {
            var reference = _compareToNow
                ? (_useUtcNow ? DateTime.UtcNow : DateTime.Now)
                : new DateTime(_referenceTicks);

            var order = value.CompareTo(reference);

            return _comparison switch
            {
                Comparisons.Equal => order == 0,
                Comparisons.Inequality => order != 0,
                Comparisons.LessThan => order < 0,
                Comparisons.GreaterThan => order > 0,
                Comparisons.LessThanOrEqual => order <= 0,
                Comparisons.GreaterThanOrEqual => order >= 0,
                _ => throw new ArgumentOutOfRangeException(nameof(_comparison), _comparison, null)
            };
        }
    }
}
