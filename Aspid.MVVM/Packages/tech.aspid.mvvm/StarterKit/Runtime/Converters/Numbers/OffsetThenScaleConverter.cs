using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Adds a constant to a number and scales the sum.
    /// </summary>
    /// <remarks>
    /// Computed in <see cref="double"/>; the int and long overloads truncate and saturate, so a TwoWay
    /// integer binding with a fractional offset or scale drifts.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Offset Then Scale",
        Tooltip = "Adds a constant to a number and scales the sum")]
    public sealed class OffsetThenScaleConverter :
        ITwoWayConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        ITwoWayConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        ITwoWayConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        ITwoWayConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
    {
        [Tooltip("Added to the value first.")]
        [SerializeField] private float _offset;

        [Tooltip("Multiplies the sum. A scale of zero cannot be reversed.")]
        [SerializeField] private float _scale = 1f;

        [Tooltip("Returned from Convert Back when the scale is zero.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private ConverterFallback<double> _convertBackFallback = new(0d, ConverterFailureMode.ReturnInput);

        /// <remarks>Default: no offset and a scale of one.</remarks>
        public OffsetThenScaleConverter() { }

        /// <param name="offset">Added to the value first.</param>
        /// <param name="scale">
        /// Multiplies the sum. A scale of zero cannot be reversed: reversing reports an error and
        /// falls back.
        /// </param>
        /// <param name="convertBackFallback">
        /// Returned from <see cref="ConvertBack"/> when the scale is zero.
        /// When omitted, returns the input value unchanged.
        /// </param>
        public OffsetThenScaleConverter(float offset, float scale = 1f, ConverterFallback<double>? convertBackFallback = null)
        {
            _offset = offset;
            _scale = scale;
            _convertBackFallback = convertBackFallback ?? _convertBackFallback;
        }

        #region Return int
        int IConverter<int, int>.Convert(int value) =>
            NumericSaturation.ToInt(Apply(value));

        int IConverter<long, int>.Convert(long value) =>
            NumericSaturation.ToInt(Apply(value));

        int IConverter<float, int>.Convert(float value) =>
            NumericSaturation.ToInt(Apply(value));

        int IConverter<double, int>.Convert(double value) =>
            NumericSaturation.ToInt(Apply(value));
        #endregion

        #region Return long
        long IConverter<long, long>.Convert(long value) =>
            NumericSaturation.ToLong(Apply(value));

        long IConverter<int, long>.Convert(int value) =>
            NumericSaturation.ToLong(Apply(value));

        long IConverter<float, long>.Convert(float value) =>
            NumericSaturation.ToLong(Apply(value));

        long IConverter<double, long>.Convert(double value) =>
            NumericSaturation.ToLong(Apply(value));
        #endregion

        #region Return float
        /// <summary>
        /// Adds the offset to the specified value and scales the sum.
        /// </summary>
        /// <param name="value">The value to transform.</param>
        /// <returns>The transformed value.</returns>
        public float Convert(float value) => NumericSaturation.ToFloat(Apply(value));

        float IConverter<int, float>.Convert(int value) =>
            NumericSaturation.ToFloat(Apply(value));

        float IConverter<long, float>.Convert(long value) =>
            NumericSaturation.ToFloat(Apply(value));

        float IConverter<double, float>.Convert(double value) =>
            NumericSaturation.ToFloat(Apply(value));
        #endregion

        #region Return double
        double IConverter<double, double>.Convert(double value) => Apply(value);

        double IConverter<int, double>.Convert(int value) =>
            Apply(value);

        double IConverter<long, double>.Convert(long value) =>
            Apply(value);

        double IConverter<float, double>.Convert(float value) =>
            Apply(value);

        private double Apply(double value) => (value + _offset) * _scale;
        #endregion

        #region Convert back
        /// <summary>
        /// Reverses <see cref="Convert"/>.
        /// </summary>
        /// <param name="value">The value to transform back.</param>
        /// <returns>
        /// The value the forward pass was given. A zero scale reports an error and returns
        /// the fallback.
        /// </returns>
        public float ConvertBack(float value) => NumericSaturation.ToFloat(Undo(value));

        double ITwoWayConverter<double, double>.ConvertBack(double value) =>
            Undo(value);

        int ITwoWayConverter<int, int>.ConvertBack(int value) =>
            NumericSaturation.ToInt(Undo(value));

        long ITwoWayConverter<long, long>.ConvertBack(long value) =>
            NumericSaturation.ToLong(Undo(value));

        private double Undo(double value)
        {
            if (_scale != 0f) return value / _scale - _offset;

            return _convertBackFallback.Fail(
                converter: this,
                value: value,
                problem: "the scale is zero, which discards the value the forward pass was given");
        }
        #endregion
    }
}
