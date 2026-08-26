using System;
using System.Text;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Repeats a piece of text once per count.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Repeat",
        Tooltip = "Repeats a piece of text once per count")]
    public sealed class RepeatStringConverter : IConverter<int, string>
    {
        // With no maximum the count comes from the ViewModel, and a runaway one would build that many.
        private const int CountCeiling = 1000;

        [Tooltip("The text repeated once per count.")]
        [SerializeField] private string _unit = "★";

        [Tooltip("The text used for the remainder up to the maximum. When empty, nothing is added.")]
        [SerializeField] private string _emptyUnit = "☆";

        [Tooltip("The total number of units. Zero means no maximum, with the count capped at 1000.")]
        [SerializeField] [Min(0)] private int _max = 5;

        /// <remarks>Default: with five stars.</remarks>
        public RepeatStringConverter() { }

        /// <param name="unit">The text repeated once per count.</param>
        /// <param name="max">
        /// The total number of units, five when left out. Zero means no maximum; the count is then
        /// capped at 1000 units and the cap is reported as an error.
        /// </param>
        /// <param name="emptyUnit">The text used for the remainder up to the maximum.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="unit"/> is <see langword="null"/>.
        /// </exception>
        public RepeatStringConverter(string unit, int max = 5, string? emptyUnit = "")
        {
            _max = max;
            _emptyUnit = emptyUnit ?? string.Empty;
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        /// <summary>
        /// Repeats the unit the specified number of times.
        /// </summary>
        /// <param name="value">How many units to write.</param>
        /// <returns>The repeated text.</returns>
        public string Convert(int value)
        {
            var bounded = _max > 0;
            var filled = Math.Max(0, bounded ? Math.Min(value, _max) : Capped(value));
            var remainder = bounded && !string.IsNullOrEmpty(_emptyUnit) ? _max - filled : 0;

            // Counted as a long: an absurd maximum overflows the int product into a negative capacity.
            var capacity = (long)filled * _unit.Length + (long)remainder * _emptyUnit.Length;
            var builder = new StringBuilder((int)Math.Min(capacity, int.MaxValue));

            for (var i = 0; i < filled; i++) builder.Append(_unit);
            for (var i = 0; i < remainder; i++) builder.Append(_emptyUnit);

            return builder.ToString();
        }

        private int Capped(int value)
        {
            if (value <= CountCeiling) return value;

            this.LogError(
                problem: $"the count {value} is past the {CountCeiling}-unit ceiling that applies when no maximum is set",
                consequence: $"Writing {CountCeiling} units.");

            return CountCeiling;
        }
    }
}
