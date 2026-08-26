using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a 0..1 fraction to a percentage, or the other way round.
    /// </summary>
    /// <remarks>
    /// Rounding discards the places below a whole percent, so a TwoWay binding that rounds does not
    /// round-trip: 0.735 goes out as 74 and comes back as 0.74.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Normalized To Percent",
        Tooltip = "Converts a 0..1 fraction to a percentage, or the other way round")]
    public sealed class NormalizedPercentConverter :
        ITwoWayConverter<float, float>,
        ITwoWayConverter<double, double>
    {
        [Tooltip("Round the percentage to a whole number. A rounded percentage no longer converts " +
            "back to the fraction it came from.")]
        [SerializeField] private bool _round;

        [Tooltip("Convert a percentage to a fraction instead.")]
        [SerializeField] private bool _isInvert;

        /// <remarks>Default: fraction to percent, keeping the fractional percent.</remarks>
        public NormalizedPercentConverter() { }

        /// <param name="round">
        /// If <see langword="true"/>, rounds the percentage to a whole number, which no longer converts
        /// back to the fraction it came from.
        /// </param>
        /// <param name="isInvert">If <see langword="true"/>, converts a percentage to a fraction instead.</param>
        public NormalizedPercentConverter(bool round, bool isInvert = false)
        {
            _round = round;
            _isInvert = isInvert;
        }

        /// <summary>
        /// Converts the specified value in the authored direction.
        /// </summary>
        /// <param name="value">The 0..1 fraction, or the percentage when inverted.</param>
        /// <returns>The percentage, or the 0..1 fraction when inverted. Not clamped.</returns>
        public float Convert(float value) => _isInvert
            ? ToFraction(value)
            : ToPercent(value);

        /// <summary>
        /// Converts a value back in the opposite direction.
        /// </summary>
        /// <param name="value">The percentage, or the 0..1 fraction when inverted.</param>
        /// <returns>The 0..1 fraction, or the percentage when inverted. Not clamped.</returns>
        public float ConvertBack(float value) => _isInvert
            ? ToPercent(value)
            : ToFraction(value);

        double IConverter<double, double>.Convert(double value) => _isInvert
            ? ToFraction(value)
            : ToPercent(value);

        double ITwoWayConverter<double, double>.ConvertBack(double value) => _isInvert
            ? ToPercent(value)
            : ToFraction(value);

        // Rounding belongs to the percent, whichever direction produces it.
        private float ToPercent(float value) => (float)ToPercent((double)value);

        private static float ToFraction(float value) => value / 100f;

        // Math.Round and Mathf.Round both send a half to the even neighbour.
        private double ToPercent(double value)
        {
            var percent = value * 100d;
            return _round ? Math.Round(percent) : percent;
        }

        private static double ToFraction(double value) => value / 100d;
    }
}
