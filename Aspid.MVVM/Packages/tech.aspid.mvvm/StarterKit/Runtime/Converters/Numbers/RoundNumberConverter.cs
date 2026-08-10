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
    /// <para>
    /// The midpoint rule is separate from the direction because the two answer different questions.
    /// <see cref="MidpointRounding.ToEven"/> is what <c>Mathf.Round</c> does and what statistics
    /// wants — it does not drift upwards over many values. <see cref="MidpointRounding.AwayFromZero"/>
    /// is what a player expects when they see 2.5 become 3, and it is the only one worth having on a
    /// score or a price.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Round Number", Tooltip = "Rounds a number, in a way the caller chooses")]
    public sealed class RoundNumberConverter : IConverterFloat, IConverterFloatToInt
    {
        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _mode;

        [Tooltip("How many decimal places to keep. Ignored when converting to int.")]
        [SerializeField] private int _digits;

        [Tooltip("Where a value exactly halfway between two results goes. Only the Round mode "
            + "consults it: 2.5 becomes 2 under ToEven and 3 under AwayFromZero.")]
        [SerializeField] private MidpointRounding _midpoint = MidpointRounding.ToEven;

        /// <remarks>Default: rounding to the nearest whole number.</remarks>
        public RoundNumberConverter() { }

        /// <param name="mode">Which way to drop the fraction.</param>
        /// <param name="digits">How many decimal places to keep.</param>
        /// <param name="midpoint">Where a value exactly halfway between two results goes.</param>
        public RoundNumberConverter(
            RoundMode mode,
            int digits = 0,
            MidpointRounding midpoint = MidpointRounding.ToEven)
        {
            _mode = mode;
            _digits = digits;
            _midpoint = midpoint;
        }

        /// <summary>
        /// Rounds the specified value to the configured number of decimal places.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <returns>The rounded value.</returns>
        public float Convert(float value)
        {
            if (_digits <= 0) return (float)Apply(value);

            var scale = Math.Pow(10d, _digits);
            return (float)(Apply(value * scale) / scale);
        }

        // Saturating rather than casting: an out-of-range float in a plain (int) cast gives a result
        // the C# specification leaves undefined, so the same scene can round differently on two
        // platforms.
        int IConverter<float, int>.Convert(float value) => NumericSaturation.ToInt(Apply(value));

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        private double Apply(double value) => _mode switch
        {
            RoundMode.Round => Math.Round(value, _midpoint),
            RoundMode.Floor => Math.Floor(value),
            RoundMode.Ceil => Math.Ceiling(value),
            RoundMode.Truncate => Math.Truncate(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };
    }
}
