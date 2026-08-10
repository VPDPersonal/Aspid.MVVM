using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Rounds a number, in a way the caller chooses.
    /// </summary>
    /// <remarks>
    /// Rounding used to be an implicit truncation inside a cast, with no say in the matter. The
    /// direction is rarely arbitrary: a countdown floored shows 0:00 for a whole second before it
    /// fires, and a score truncated loses the point the player just earned.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Round Number", Tooltip = "Rounds a number, in a way the caller chooses")]
    public sealed class RoundNumberConverter : IConverterFloat, IConverterFloatToInt
    {
        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _mode;

        [Tooltip("How many decimal places to keep. Ignored when converting to int.")]
        [SerializeField] private int _digits;

        /// <remarks>Default: rounding to the nearest whole number.</remarks>
        public RoundNumberConverter() { }

        /// <param name="mode">Which way to drop the fraction.</param>
        /// <param name="digits">How many decimal places to keep.</param>
        public RoundNumberConverter(RoundMode mode, int digits = 0)
        {
            _mode = mode;
            _digits = digits;
        }

        /// <summary>
        /// Rounds the specified value to the configured number of decimal places.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <returns>The rounded value.</returns>
        public float Convert(float value)
        {
            if (_digits <= 0) return Apply(value);

            var scale = Mathf.Pow(10f, _digits);
            return Apply(value * scale) / scale;
        }

        int IConverter<float, int>.Convert(float value) => (int)Apply(value);

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        private float Apply(float value) => _mode switch
        {
            RoundMode.Round => Mathf.Round(value),
            RoundMode.Floor => Mathf.Floor(value),
            RoundMode.Ceil => Mathf.Ceil(value),
            RoundMode.Truncate => (float)Math.Truncate(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };
    }
}
