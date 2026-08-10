using Aspid.FastTools.Types;
using System;
using System.Text;
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
    /// <para>
    /// One unit is the usual answer and the one the two formats were written for — <c>{0}</c> the
    /// amount, <c>{1}</c> its name. Asking for more than one has nothing single to put in
    /// <c>{0}</c>, so the whole quantity goes there — "1h 5m" — and <c>{1}</c> arrives empty. The
    /// default formats read correctly either way, which is why they run the two together.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Time", Name = "Relative Time", Tooltip = "Writes how long ago — or how far ahead — a moment is")]
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

        [Tooltip("How many units to write, largest first: 1 gives \"1h\", 2 gives \"1h 5m\". Units "
            + "that are zero are passed over. Past 1 the whole quantity arrives as {0} and {1} is "
            + "empty.")]
        [SerializeField] private int _maxUnits = 1;

        [Tooltip("Placed between the units when there is more than one.")]
        [SerializeField] private string _unitSeparator = " ";

        [Tooltip("The culture the amounts are written with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.InvariantCulture;

        [NonSerialized] private StringBuilder? _builder;

        /// <remarks>Default: with English defaults.</remarks>
        public RelativeTimeConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeTimeConverter"/> class.
        /// </summary>
        /// <param name="maxUnits">How many units to write, largest first.</param>
        /// <param name="culture">The culture the amounts are written with.</param>
        /// <param name="useUtcNow">Whether to compare against UTC rather than local time.</param>
        public RelativeTimeConverter(
            int maxUnits,
            CultureInfoMode culture = CultureInfoMode.InvariantCulture,
            bool useUtcNow = false)
        {
            _maxUnits = maxUnits;
            _culture = culture;
            _useUtcNow = useUtcNow;
        }

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

            var format = delta.Ticks < 0L ? _pastFormat : _futureFormat;
            var culture = _culture.ToCultureInfo();

            return _maxUnits > 1
                ? Several(magnitude, Math.Min(_maxUnits, 4), format, culture)
                : Single(magnitude, format, culture);
        }

        private string Single(TimeSpan magnitude, string format, CultureInfo culture)
        {
            for (var index = 3; index > 0; index--)
            {
                var amount = Amount(magnitude, index);
                if (amount > 0L) return string.Format(culture, format, amount, Unit(index));
            }

            return string.Format(culture, format, (long)magnitude.TotalSeconds, Unit(0));
        }

        private string Several(TimeSpan magnitude, int maxUnits, string format, CultureInfo culture)
        {
            _builder ??= new StringBuilder();
            _builder.Clear();

            var written = 0;

            for (var index = 3; index >= 0 && written < maxUnits; index--)
            {
                var amount = Amount(magnitude, index);
                if (amount == 0L) continue;

                if (written > 0) _builder.Append(_unitSeparator);
                _builder.Append(amount.ToString(culture)).Append(Unit(index));
                written++;
            }

            // Under a second is already handled, so nothing written here means every component
            // rounded away — a fraction of a second either side of a whole one.
            if (written == 0) return _nowText;

            return string.Format(culture, format, _builder.ToString(), string.Empty);
        }

        // Days accumulate — a month is thirty-odd of them — while the smaller units are the
        // components of what is left, so that two of them read as "1h 5m" rather than "1h 65m".
        private static long Amount(TimeSpan magnitude, int index) => index switch
        {
            3 => (long)magnitude.TotalDays,
            2 => magnitude.Hours,
            1 => magnitude.Minutes,
            _ => magnitude.Seconds,
        };

        private string Unit(int index) =>
            _unitNames is { Length: > 0 } && index < _unitNames.Length ? _unitNames[index] : string.Empty;
    }
}
