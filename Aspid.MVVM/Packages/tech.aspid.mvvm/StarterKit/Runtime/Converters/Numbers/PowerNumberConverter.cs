using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Raises a number to an authored exponent.
    /// </summary>
    /// <remarks>
    /// The sign is preserved by default because the alternative is a NaN: <c>Math.Pow</c> of a negative
    /// base and a fractional exponent has no real answer, and a stat that briefly goes negative is
    /// normal. Preserving it makes the curve odd — -2 with exponent 2 gives -4, not 4 — so turn it off
    /// for plain arithmetic.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Power Number", Tooltip = "Raises a number to an authored exponent")]
    public sealed class PowerNumberConverter :
        ITwoWayConverter<float, float>,
        ITwoWayConverter<double, double>
    {
        [Tooltip("The exponent the value is raised to.")]
        [SerializeField] private float _exponent = 2f;

        [Tooltip("Raise the magnitude and put the sign back, so a negative value stays negative "
            + "instead of turning into a NaN. With this off, -2 raised to 2 gives 4 rather than -4.")]
        [SerializeField] private bool _preserveSign = true;

        /// <remarks>Default: squaring the value.</remarks>
        public PowerNumberConverter() { }

        /// <param name="exponent">The exponent the value is raised to.</param>
        /// <param name="preserveSign">
        /// If <see langword="true"/>, raises the magnitude and puts the sign back.
        /// </param>
        public PowerNumberConverter(float exponent, bool preserveSign = true)
        {
            _exponent = exponent;
            _preserveSign = preserveSign;
        }

        /// <summary>
        /// Raises the specified value to the configured exponent.
        /// </summary>
        /// <param name="value">The value to raise.</param>
        /// <returns>The raised value.</returns>
        public double Convert(double value) => Raise(value, _exponent);

        /// <inheritdoc cref="Convert(double)"/>
        public float Convert(float value) => (float)Raise(value, _exponent);

        /// <summary>
        /// Reverses <see cref="Convert(double)"/> by raising the value to the reciprocal exponent.
        /// </summary>
        /// <param name="value">The value to transform back.</param>
        /// <returns>
        /// The value the forward pass was given, or <paramref name="value"/> unchanged when the
        /// exponent is zero — every input maps to 1, so there is nothing to recover it from.
        /// </returns>
        public double ConvertBack(double value) =>
            _exponent == 0f ? value : Raise(value, 1d / _exponent);

        /// <inheritdoc cref="ConvertBack(double)"/>
        public float ConvertBack(float value) =>
            _exponent == 0f ? value : (float)Raise(value, 1d / _exponent);

        private double Raise(double value, double exponent)
        {
            if (!_preserveSign) return Math.Pow(value, exponent);
            if (value == 0d) return 0d;

            var magnitude = Math.Pow(Math.Abs(value), exponent);
            return value < 0d ? -magnitude : magnitude;
        }
    }
}
