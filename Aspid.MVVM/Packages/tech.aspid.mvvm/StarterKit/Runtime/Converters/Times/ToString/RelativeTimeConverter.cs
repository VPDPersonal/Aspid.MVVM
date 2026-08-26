using System;
using System.Text;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes how long ago — or how far ahead — a moment is.
    /// </summary>
    /// <remarks>
    /// With more than one unit the whole quantity arrives as <c>{0}</c> and <c>{1}</c> is empty.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Time/To String",
        Name = "Relative Time",
        Tooltip = "Writes how long ago — or how far ahead — a moment is")]
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

        [Tooltip("Compare against UTC rather than local time. Match this to the bound moment.")]
        [SerializeField] private bool _useUtcNow;

        [Tooltip("How many units to write, largest first: 1 gives \"1h\", 2 gives \"1h 5m\". Past 1, {1} is empty.")]
        [SerializeField] [Min(1)] private int _maxUnits = 1;

        [Tooltip("Placed between the units when there is more than one.")]
        [SerializeField] private string _unitSeparator = " ";

        [Tooltip("The culture the amounts are written with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.InvariantCulture;

        [NonSerialized] private StringBuilder? _builder;

        /// <remarks>Default: with English defaults.</remarks>
        public RelativeTimeConverter() { }

        /// <param name="maxUnits">
        /// How many units to write, largest first. The ladder stops at days, so more than 4 writes 4.
        /// </param>
        /// <param name="culture">The culture the amounts are written with.</param>
        /// <param name="useUtcNow">
        /// Whether to compare against UTC rather than local time. Match this to the bound moment,
        /// or the result is out by the time zone.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="maxUnits"/> is below 1.
        /// </exception>
        public RelativeTimeConverter(
            int maxUnits,
            CultureInfoMode culture = CultureInfoMode.InvariantCulture,
            bool useUtcNow = false)
        {
            _culture = culture;
            _useUtcNow = useUtcNow;
            _maxUnits = maxUnits >= 1 ? maxUnits : throw new ArgumentOutOfRangeException(nameof(maxUnits));
        }

        // Min cannot fix a value serialized before it was added, and the ladder stops at days.
        private int MaxUnits => Math.Clamp(_maxUnits, 1, 4);
        
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
            var maxUnits = MaxUnits;

            return maxUnits > 1
                ? Several(magnitude, maxUnits, format, culture)
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

                if (written > 0)
                    _builder.Append(_unitSeparator);
                
                _builder.Append(amount.ToString(culture)).Append(Unit(index));
                written++;
            }

            // Defensive: above a second some component is always non-zero.
            return written == 0 
                ? _nowText 
                : string.Format(culture, format, _builder.ToString(), string.Empty);

        }

        // Days accumulate; the smaller units are components, so two of them read "1h 5m", not "1h 65m".
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
