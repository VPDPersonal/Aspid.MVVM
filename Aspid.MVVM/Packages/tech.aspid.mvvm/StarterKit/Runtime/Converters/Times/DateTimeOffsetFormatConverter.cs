using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <remarks>
    /// An event window arrives from a server with the server's offset attached, and that offset is
    /// the part a <see cref="DateTime"/> cannot carry — converting to one either drops it or quietly
    /// re-reads the moment in the player's own zone. This keeps it, and lets the field choose which
    /// zone to show the moment in: the one it came with, the player's, or a fixed one the game picked
    /// so that everyone reads the same clock.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Time", Name = "Date Time Offset Format", Tooltip = "Formats a DateTimeOffset")]
    public sealed class DateTimeOffsetFormatConverter : IConverter<DateTimeOffset, string>
    {
        [Tooltip("A DateTimeOffset format string, for example dd.MM.yyyy HH:mm, or zzz for the offset itself.")]
        [SerializeField] private string _format = "g";

        [Tooltip("Show the moment in the player's own time zone.")]
        [SerializeField] private bool _toLocalTime;

        [Tooltip("Show the moment at the offset below instead of the one it arrived with. Takes "
            + "precedence over the local time option above.")]
        [SerializeField] private bool _useOffsetOverride;

        [Tooltip("Minutes east of UTC to show the moment at. Held inside ±14 hours, which is as far "
            + "as an offset is allowed to go.")]
        [SerializeField] private int _offsetMinutes;

        [Tooltip("The culture the date is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [NonSerialized] private bool _loggedFormatFailure;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeOffsetFormatConverter"/> class with the general format.
        /// </summary>
        public DateTimeOffsetFormatConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeOffsetFormatConverter"/> class.
        /// </summary>
        /// <param name="format">A <see cref="DateTimeOffset"/> format string.</param>
        /// <param name="toLocalTime">Whether to show the moment in the player's own time zone.</param>
        /// <param name="offsetOverride">
        /// The offset to show the moment at. When <see langword="null"/>, the moment keeps the offset
        /// it arrived with.
        /// </param>
        public DateTimeOffsetFormatConverter(string format, bool toLocalTime = false, TimeSpan? offsetOverride = null)
        {
            _format = format;
            _toLocalTime = toLocalTime;
            _useOffsetOverride = offsetOverride.HasValue;
            _offsetMinutes = (int)(offsetOverride?.TotalMinutes ?? 0d);
        }

        /// <summary>
        /// Formats the specified moment.
        /// </summary>
        /// <param name="value">The moment to format.</param>
        /// <returns>The formatted moment, or the default rendering when the format is unusable.</returns>
        public string Convert(DateTimeOffset value)
        {
            var moment = At(value);
            var culture = _culture.ToCultureInfo();

            if (string.IsNullOrWhiteSpace(_format)) return moment.ToString(culture);

            try
            {
                return moment.ToString(_format, culture);
            }
            catch (FormatException exception)
            {
                LogFormatFailure(exception);
                return moment.ToString(culture);
            }
        }

        private DateTimeOffset At(DateTimeOffset value)
        {
            if (!_useOffsetOverride) return _toLocalTime ? value.ToLocalTime() : value;

            // ToOffset throws past ±14 hours, and the offset is typed in rather than picked from a
            // list — a misconfigured field should show the wrong hour, not stop the binder.
            var minutes = Math.Clamp(_offsetMinutes, -840, 840);
            return value.ToOffset(TimeSpan.FromMinutes(minutes));
        }

        private void LogFormatFailure(FormatException exception)
        {
            if (_loggedFormatFailure) return;
            _loggedFormatFailure = true;

            Debug.LogError(
                $"{nameof(DateTimeOffsetFormatConverter)}: \"{_format}\" is not a DateTimeOffset format "
                + $"({exception.Message}). Falling back to the default rendering.");
        }
    }
}
