using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Maps a number from one range onto another.
    /// </summary>
    /// <remarks>
    /// Computed in <see cref="double"/>; the int and long overloads truncate and saturate, so a TwoWay
    /// integer binding over a fractional range drifts.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Remap",
        Tooltip = "Maps a number from one range onto another")]
    public sealed class RemapNumberConverter :
        ITwoWayConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        ITwoWayConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        ITwoWayConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        ITwoWayConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
    {
        [Tooltip("The low end of the incoming range.")]
        [SerializeField] private float _fromMin;

        [Tooltip("The high end of the incoming range.")]
        [SerializeField] private float _fromMax = 1f;

        [Tooltip("The low end of the outgoing range.")]
        [SerializeField] private float _toMin;

        [Tooltip("The high end of the outgoing range.")]
        [SerializeField] private float _toMax = 1f;

        [Tooltip("Hold the result inside the outgoing range.")]
        [SerializeField] private bool _clamp = true;

        /// <remarks>Default: mapping 0..1 onto 0..1.</remarks>
        public RemapNumberConverter() { }

        /// <param name="fromMin">The low end of the incoming range.</param>
        /// <param name="fromMax">The high end of the incoming range.</param>
        /// <param name="toMin">The low end of the outgoing range.</param>
        /// <param name="toMax">The high end of the outgoing range.</param>
        /// <param name="clamp">If <see langword="true"/>, holds the result inside the outgoing range.</param>
        public RemapNumberConverter(float fromMin, float fromMax, float toMin, float toMax, bool clamp = true)
        {
            _fromMin = fromMin;
            _fromMax = fromMax;
            _toMin = toMin;
            _toMax = toMax;
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
        /// Maps the specified value from the incoming range onto the outgoing one.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <returns>The mapped value. A degenerate incoming range yields the outgoing low end.</returns>
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
            Map(value, _fromMin, _fromMax, _toMin, _toMax, _clamp);
        #endregion

        #region Convert back
        /// <summary>
        /// Maps the specified value back from the outgoing range onto the incoming one.
        /// </summary>
        /// <param name="value">The value to map back.</param>
        /// <returns>The value in the incoming range. A degenerate outgoing range yields its low end.</returns>
        public float ConvertBack(float value) => NumericSaturation.ToFloat(Undo(value));

        double ITwoWayConverter<double, double>.ConvertBack(double value) =>
            Undo(value);

        int ITwoWayConverter<int, int>.ConvertBack(int value) =>
            NumericSaturation.ToInt(Undo(value));

        long ITwoWayConverter<long, long>.ConvertBack(long value) =>
            NumericSaturation.ToLong(Undo(value));

        private double Undo(double value) =>
            Map(value, _toMin, _toMax, _fromMin, _fromMax, _clamp);
        #endregion

        /// <inheritdoc cref="Map(double,double,double,double,double,bool)"/>
        internal static float Map(float value, float fromMin, float fromMax, float toMin, float toMax, bool clamp) =>
            // The cast is what picks the double overload below; without it this resolves to itself.
            NumericSaturation.ToFloat(Map((double)value, fromMin, fromMax, toMin, toMax, clamp));

        /// <summary>
        /// Maps a value from one range onto another.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <param name="fromMin">The low end of the incoming range.</param>
        /// <param name="fromMax">The high end of the incoming range.</param>
        /// <param name="toMin">The low end of the outgoing range.</param>
        /// <param name="toMax">The high end of the outgoing range.</param>
        /// <param name="clamp">Whether to hold the result inside the outgoing range.</param>
        /// <returns>The mapped value. A degenerate incoming range yields <paramref name="toMin"/>.</returns>
        internal static double Map(double value, double fromMin, double fromMax, double toMin, double toMax, bool clamp)
        {
            var span = fromMax - fromMin;
            if (span == 0d) return toMin;

            var t = (value - fromMin) / span;
            // Mathf.Clamp01 has no double form; this is what it does, a NaN failing both comparisons.
            if (clamp) t = t < 0d ? 0d : t > 1d ? 1d : t;

            return toMin + (toMax - toMin) * t;
        }
    }
}
