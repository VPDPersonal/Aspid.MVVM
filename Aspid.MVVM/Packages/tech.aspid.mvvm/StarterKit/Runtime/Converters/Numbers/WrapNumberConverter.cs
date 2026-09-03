using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Folds a number back into a range instead of clamping it.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Wrap",
        Tooltip = "Folds a number back into a range instead of clamping it")]
    public sealed class WrapNumberConverter : NumberConverter
    {
        [Tooltip("How to fold a value that leaves the range.")]
        [SerializeField] private NumberWrapMode _mode;

        [Tooltip("The low end of the range.")]
        [SerializeField] private float _min;

        [Tooltip("The high end of the range. Equal to the minimum, the range pins the value.")]
        [SerializeField] private float _max = 1f;

        /// <remarks>Default: over 0..1.</remarks>
        public WrapNumberConverter() { }

        /// <param name="mode">How to fold a value that leaves the range.</param>
        /// <param name="min">The low end of the range. Inverted bounds report an error and are swapped.</param>
        /// <param name="max">The high end of the range. Equal to <paramref name="min"/>, the range pins the value.</param>
        public WrapNumberConverter(
            NumberWrapMode mode,
            float min,
            float max)
        {
            _min = min;
            _max = max;
            _mode = mode;
        }

        /// <summary>
        /// Folds the number into the range.
        /// </summary>
        /// <param name="value">The number to fold.</param>
        /// <returns>
        /// The folded number. Inverted bounds report an error and fold into the swapped range;
        /// an undeclared mode reports an error and returns the value unchanged.
        /// </returns>
        protected override double Apply(double value)
        {
            if (Mathf.Approximately(_min, _max)) return _min;

            var (min, max) = ((double)_min, (double)_max);

            if (min > max)
            {
                this.LogError(
                    problem: $"the minimum {_min} is above the maximum {_max}",
                    consequence: "Folding into the swapped bounds.");

                (min, max) = (max, min);
            }

            var span = max - min;

            return _mode switch
            {
                NumberWrapMode.Repeat => min + Repeat(value - min, span),
                NumberWrapMode.PingPong => min + PingPong(value - min, span),
                _ => Undeclared(value)
            };
        }

        // Mathf.Repeat in double; the clamp catches a fold landing a hair outside the span.
        private static double Repeat(double value, double length)
        {
            var folded = value - Math.Floor(value / length) * length;
            return Math.Min(Math.Max(folded, 0d), length);
        }

        private static double PingPong(double value, double length) =>
            length - Math.Abs(Repeat(value, length * 2d) - length);

        private double Undeclared(double value)
        {
            this.LogError(
                problem: $"the mode {_mode.Describe()} is not a declared {nameof(NumberWrapMode)}",
                consequence: "Returning the value unchanged.");

            return value;
        }
    }
}
