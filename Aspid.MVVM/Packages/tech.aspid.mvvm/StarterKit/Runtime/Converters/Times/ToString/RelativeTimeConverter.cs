#nullable enable
using System;
using System.Text;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes how long ago, or how far ahead, a moment is.
    /// </summary>
    /// <remarks>With more than one unit the whole quantity arrives as <c>{0}</c> and <c>{1}</c> is empty.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Time/To String",
        Name = "Relative Time",
        Tooltip = "Writes how long ago, or how far ahead, a moment is")]
    public sealed class RelativeTimeConverter : IConverter<DateTime, string>
    {
        [Tooltip("Names for second, minute, hour, day; longer spans use days.")]
        [SerializeField] private string[] _unitNames = { "s", "m", "h", "d" };

        [Tooltip("A composite format for a past moment: {0} is the amount, {1} the unit.")]
        [SerializeField] private string _pastFormat = "{0}{1} ago";

        [Tooltip("A composite format for a future moment: {0} is the amount, {1} the unit.")]
        [SerializeField] private string _futureFormat = "in {0}{1}";

        [Tooltip("Shown when the moment is within a second of now.")]
        [SerializeField] private string _nowText = "now";

        [Tooltip("Measure an Unspecified-kind moment against UTC rather than local time.")]
        [SerializeField] private bool _useUtcNow;

        [Tooltip("How many units to write, largest first: 1 gives \"1h\", 2 gives \"1h 5m\".")]
        [SerializeField] [Range(1, 4)] private int _maxUnits = 1;

        [Tooltip("Placed between the units when there is more than one.")]
        [SerializeField] private string _unitSeparator = " ";

        [Tooltip("The culture the amounts are written with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.InvariantCulture;

        [NonSerialized] private StringBuilder? _builder;

        /// <remarks>Default: with English defaults.</remarks>
        public RelativeTimeConverter() { }

        /// <param name="maxUnits">How many units to write, largest first, 1 to 4.</param>
        /// <param name="culture">The culture the amounts are written with.</param>
        /// <param name="useUtcNow">Whether an <see cref="DateTimeKind.Unspecified"/> moment is measured against UTC rather than local time.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="maxUnits"/> is outside 1..4.</exception>
        public RelativeTimeConverter(
            int maxUnits,
            CultureInfoMode culture = CultureInfoMode.InvariantCulture,
            bool useUtcNow = false)
        {
            _culture = culture;
            _useUtcNow = useUtcNow;
            _maxUnits = maxUnits is >= 1 and <= 4 ? maxUnits : throw new ArgumentOutOfRangeException(nameof(maxUnits));
        }

        /// <summary>
        /// Writes how far the specified moment is from now.
        /// </summary>
        /// <param name="value">The moment to describe.</param>
        /// <returns>The description.</returns>
        public string Convert(DateTime value)
        {
            var delta = value - CurrentTime.For(value, _useUtcNow);
            var magnitude = delta.Duration();

            if (magnitude.TotalSeconds < 1d) return _nowText;

            var culture = _culture.ToCultureInfo();

            var format = delta.Ticks < 0L
                ? _pastFormat
                : _futureFormat;

            return _maxUnits > 1
                ? Several(magnitude, _maxUnits, format, culture)
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
                if (amount is 0L) continue;

                if (written > 0) _builder.Append(_unitSeparator);

                _builder.Append(amount.ToString(culture)).Append(Unit(index));
                written++;
            }

            return written is 0
                ? _nowText
                : string.Format(culture, format, _builder.ToString(), string.Empty);
        }

        private static long Amount(TimeSpan magnitude, int index) => index switch
        {
            3 => (long)magnitude.TotalDays,
            2 => magnitude.Hours,
            1 => magnitude.Minutes,
            _ => magnitude.Seconds,
        };

        private string Unit(int index)
        {
            if (_unitNames is { Length: > 0 } && index < _unitNames.Length)
                return _unitNames[index];

            this.LogError(
                problem: $"the unit names hold no name at index {index} (second, minute, hour, day)",
                consequence: "Writing an empty string for the unit.");

            return string.Empty;
        }
    }
}
