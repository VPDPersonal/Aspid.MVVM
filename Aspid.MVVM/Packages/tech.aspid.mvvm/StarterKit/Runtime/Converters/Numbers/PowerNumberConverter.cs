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
    /// The sign is preserved by default because the alternative is a NaN: <c>Math.Pow</c> of a negative
    /// base and a fractional exponent has no real answer. Preserving it makes the curve odd — -2 with
    /// exponent 2 gives -4, not 4.
    /// <para>
    /// Computed in <see cref="double"/>; the int and long overloads truncate and saturate, so a TwoWay
    /// integer binding with a fractional exponent drifts.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Power",
        Tooltip = "Raises a number to an authored exponent")]
    public sealed class PowerNumberConverter :
        ITwoWayConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        ITwoWayConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        ITwoWayConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        ITwoWayConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
    {
        [Tooltip("The exponent the value is raised to. An exponent of zero cannot be reversed.")]
        [SerializeField] private float _exponent = 2f;

        [Tooltip("Raise the magnitude and put the sign back rather than returning a NaN. Off, -2 raised " +
            "to 2 gives 4.")]
        [SerializeField] private bool _preserveSign = true;

        [Tooltip("Returned from Convert Back when the exponent is zero.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private ConverterFallback<double> _convertBackFallback = new(0d, ConverterFailureMode.ReturnInput);

        private const string ZeroExponentProblem = "the exponent is zero, which maps every value to 1";

        /// <remarks>Default: squaring the value.</remarks>
        public PowerNumberConverter() { }

        /// <param name="exponent">
        /// The exponent the value is raised to. An exponent of zero cannot be reversed: reversing
        /// reports an error and falls back.
        /// </param>
        /// <param name="preserveSign">
        /// If <see langword="true"/>, raises the magnitude and puts the sign back, so a negative value
        /// stays negative instead of turning into a NaN.
        /// </param>
        /// <param name="convertBackFallback">
        /// Returned from <see cref="ConvertBack(double)"/> when the exponent is zero.
        /// When omitted, returns the input value unchanged.
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

        #region Return int
        int IConverter<int, int>.Convert(int value) =>
            NumericSaturation.ToInt(Raise(value, _exponent));

        int IConverter<long, int>.Convert(long value) =>
            NumericSaturation.ToInt(Raise(value, _exponent));

        int IConverter<float, int>.Convert(float value) =>
            NumericSaturation.ToInt(Raise(value, _exponent));

        int IConverter<double, int>.Convert(double value) =>
            NumericSaturation.ToInt(Raise(value, _exponent));
        #endregion

        #region Return long
        long IConverter<long, long>.Convert(long value) =>
            NumericSaturation.ToLong(Raise(value, _exponent));

        long IConverter<int, long>.Convert(int value) =>
            NumericSaturation.ToLong(Raise(value, _exponent));

        long IConverter<float, long>.Convert(float value) =>
            NumericSaturation.ToLong(Raise(value, _exponent));

        long IConverter<double, long>.Convert(double value) =>
            NumericSaturation.ToLong(Raise(value, _exponent));
        #endregion

        #region Return float
        /// <inheritdoc cref="Convert(double)"/>
        public float Convert(float value) => NumericSaturation.ToFloat(Raise(value, _exponent));

        float IConverter<int, float>.Convert(int value) =>
            NumericSaturation.ToFloat(Raise(value, _exponent));

        float IConverter<long, float>.Convert(long value) =>
            NumericSaturation.ToFloat(Raise(value, _exponent));

        float IConverter<double, float>.Convert(double value) =>
            NumericSaturation.ToFloat(Raise(value, _exponent));
        #endregion

        #region Return double
        /// <summary>
        /// Raises the specified value to the configured exponent.
        /// </summary>
        /// <param name="value">The value to raise.</param>
        /// <returns>The raised value.</returns>
        public double Convert(double value) => Raise(value, _exponent);

        double IConverter<int, double>.Convert(int value) =>
            Raise(value, _exponent);

        double IConverter<long, double>.Convert(long value) =>
            Raise(value, _exponent);

        double IConverter<float, double>.Convert(float value) =>
            Raise(value, _exponent);
        #endregion

        #region Convert back
        /// <summary>
        /// Reverses <see cref="Convert(double)"/> by raising the value to the reciprocal exponent.
        /// </summary>
        /// <param name="value">The value to transform back.</param>
        /// <returns>
        /// The value the forward pass was given. A zero exponent reports an error and returns
        /// the fallback.
        /// </returns>
        public double ConvertBack(double value)
        {
            if (_exponent != 0f) return Raise(value, 1d / _exponent);

            return _convertBackFallback.Fail(
                converter: this,
                value: value,
                problem: ZeroExponentProblem);
        }

        /// <inheritdoc cref="ConvertBack(double)"/>
        public float ConvertBack(float value) =>
            NumericSaturation.ToFloat(ConvertBack((double)value));

        int ITwoWayConverter<int, int>.ConvertBack(int value) =>
            NumericSaturation.ToInt(ConvertBack((double)value));

        long ITwoWayConverter<long, long>.ConvertBack(long value) =>
            NumericSaturation.ToLong(ConvertBack((double)value));
        #endregion

        private double Raise(double value, double exponent)
        {
            if (!_preserveSign) return Math.Pow(value, exponent);
            if (value == 0d) return 0d;

            var magnitude = Math.Pow(Math.Abs(value), exponent);
            return value < 0d ? -magnitude : magnitude;
        }
    }
}
