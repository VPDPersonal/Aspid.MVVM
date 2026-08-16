using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Repeats a piece of text once per count.
    /// </summary>
    /// <remarks>Star ratings and pip counters without an array of sprites.</remarks>
    [Serializable]
    public sealed class RepeatStringConverter : IConverter<int, string>
    {
        [Tooltip("The text repeated once per count.")]
        [SerializeField] private string _unit = "★";

        [Tooltip("The text used for the remainder up to the maximum. When empty, nothing is added.")]
        [SerializeField] private string _emptyUnit = "☆";

        [Tooltip("The total number of units. Zero means no maximum.")]
        [SerializeField] private int _max = 5;

        /// <remarks>Default: with five stars.</remarks>
        public RepeatStringConverter() { }

        /// <param name="unit">The text repeated once per count.</param>
        /// <param name="max">The total number of units. Zero means no maximum.</param>
        /// <param name="emptyUnit">The text used for the remainder up to the maximum.</param>
        public RepeatStringConverter(string unit, int max = 0, string emptyUnit = "")
        {
            _unit = unit;
            _max = max;
            _emptyUnit = emptyUnit;
        }

        /// <summary>
        /// Repeats the unit the specified number of times.
        /// </summary>
        /// <param name="value">How many units to write.</param>
        /// <returns>The repeated text.</returns>
        public string Convert(int value)
        {
            var filled = Math.Max(0, _max > 0 ? Math.Min(value, _max) : value);
            var builder = new System.Text.StringBuilder();

            for (var i = 0; i < filled; i++) builder.Append(_unit);

            if (_max > 0 && !string.IsNullOrEmpty(_emptyUnit))
                for (var i = filled; i < _max; i++)
                    builder.Append(_emptyUnit);

            return builder.ToString();
        }
    }
}
