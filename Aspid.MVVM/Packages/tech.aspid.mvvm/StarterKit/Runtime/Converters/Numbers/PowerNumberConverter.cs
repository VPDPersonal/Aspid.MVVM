#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Raises a number to an authored exponent.
    /// </summary>
    /// <remarks>
    /// Preserving the sign makes the curve odd: -2 with exponent 2 gives -4. Off, a negative base with a fractional exponent is NaN.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Power",
        Tooltip = "Raises a number to an authored exponent")]
    public sealed class PowerNumberConverter : TwoWayNumberConverter
    {
        [Tooltip("The exponent the value is raised to. Zero cannot be reversed.")]
        [SerializeField] private float _exponent = 2f;

        [Tooltip("Raise the magnitude and put the sign back. Off, -2 raised to 2 gives 4.")]
        [SerializeField] private bool _preserveSign = true;

        [Tooltip("Returned from Convert Back when the exponent is zero.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private ConverterFallback<double> _convertBackFallback = new(0d, ConverterFailureMode.ReturnInput);

        /// <remarks>Default: squaring the value.</remarks>
        public PowerNumberConverter() { }

        /// <param name="exponent">The exponent the value is raised to. Zero cannot be reversed.</param>
        /// <param name="preserveSign">If <see langword="true"/>, raises the magnitude and puts the sign back.</param>
        /// <param name="convertBackFallback">
        /// Returned from <c>ConvertBack</c> when the exponent is zero. When omitted, returns the input value unchanged.
        /// </param>
        public PowerNumberConverter(
            float exponent,
            bool preserveSign = true,
            ConverterFallback<double>? convertBackFallback = null)
        {
            _exponent = exponent;
            _preserveSign = preserveSign;
            _convertBackFallback = convertBackFallback ?? _convertBackFallback;
        }

        /// <summary>
        /// Raises the number to the exponent.
        /// </summary>
        /// <param name="value">The number to raise.</param>
        /// <returns>The raised number.</returns>
        protected override double Apply(double value) =>
            Raise(value, _exponent);

        /// <summary>
        /// Raises the number to the reciprocal exponent.
        /// </summary>
        /// <param name="value">The number to transform back.</param>
        /// <returns>The number the forward pass was given, or the fallback for a zero exponent.</returns>
        protected override double Undo(double value)
        {
            if (_exponent is not 0f) return Raise(value, 1d / _exponent);

            return _convertBackFallback.Fail(
                converter: this,
                value: value,
                problem: "the exponent is zero, which maps every value to 1");
        }

        private double Raise(double value, double exponent)
        {
            if (!_preserveSign) return Math.Pow(value, exponent);
            if (value is 0d) return 0d;

            var magnitude = Math.Pow(Math.Abs(value), exponent);
            return value < 0d ? -magnitude : magnitude;
        }
    }
}
