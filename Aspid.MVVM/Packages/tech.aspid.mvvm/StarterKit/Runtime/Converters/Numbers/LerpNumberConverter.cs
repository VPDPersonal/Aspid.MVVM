using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a 0..1 position to a value in a range.
    /// </summary>
    /// <remarks>
    /// Computed in <see cref="double"/>; the int and long overloads truncate and saturate, so an
    /// integer position reaches only the ends of the range.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Lerp",
        Tooltip = "Converts a 0..1 position to a value in a range")]
    public sealed class LerpNumberConverter :
        ITwoWayConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        ITwoWayConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        ITwoWayConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        ITwoWayConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
    {
        [Tooltip("The value 0 maps to.")]
        [SerializeField] private float _from;

        [Tooltip("The value 1 maps to.")]
        [SerializeField] private float _to = 1f;

        [Tooltip("Hold the incoming position inside 0..1.")]
        [SerializeField] private bool _clamp = true;

        /// <remarks>Default: over 0..1.</remarks>
        public LerpNumberConverter() { }

        /// <param name="from">The value 0 maps to.</param>
        /// <param name="to">The value 1 maps to.</param>
        /// <param name="clamp">If <see langword="true"/>, holds the incoming position inside 0..1.</param>
        public LerpNumberConverter(float from, float to, bool clamp = true)
        {
            _from = from;
            _to = to;
            _clamp = clamp;
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
        /// Converts the specified position to a value in the range.
        /// </summary>
        /// <param name="value">The 0..1 position.</param>
        /// <returns>The value at that position.</returns>
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

        private double Apply(double value) =>
            RemapNumberConverter.Map(value, 0d, 1d, _from, _to, _clamp);
        #endregion

        #region Convert back
        /// <summary>
        /// Converts a value in the range back to its position.
        /// </summary>
        /// <param name="value">The value to locate.</param>
        /// <returns>Its 0..1 position. A degenerate range yields 0.</returns>
        public float ConvertBack(float value) => NumericSaturation.ToFloat(Undo(value));

        double ITwoWayConverter<double, double>.ConvertBack(double value) =>
            Undo(value);

        int ITwoWayConverter<int, int>.ConvertBack(int value) =>
            NumericSaturation.ToInt(Undo(value));

        long ITwoWayConverter<long, long>.ConvertBack(long value) =>
            NumericSaturation.ToLong(Undo(value));

        private double Undo(double value) =>
            RemapNumberConverter.Map(value, _from, _to, 0d, 1d, _clamp);
        #endregion
    }
}
