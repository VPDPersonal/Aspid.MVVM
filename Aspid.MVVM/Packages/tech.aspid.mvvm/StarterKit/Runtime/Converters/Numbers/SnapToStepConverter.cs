using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Snaps a number to the nearest multiple of a step.
    /// </summary>
    /// <remarks>
    /// Computed in <see cref="double"/>; the int and long overloads truncate and saturate, so a
    /// fractional step hands back a value that is not on the step.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Snap To Step",
        Tooltip = "Snaps a number to the nearest multiple of a step")]
    public sealed class SnapToStepConverter :
        IConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        IConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        IConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        IConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
    {
        [Tooltip("The size of one step. A value halfway between two steps goes to the even one.")]
        [SerializeField] private float _step = 1f;

        [Tooltip("Shifts where the steps fall.")]
        [SerializeField] private float _offset;

        /// <remarks>Default: snapping to whole numbers.</remarks>
        public SnapToStepConverter() { }

        /// <param name="step">
        /// The size of one step. A step of zero reports an error and passes the value through, and a
        /// value halfway between two steps goes to the even one, so 0.5 snaps to 0 and 1.5 to 2.
        /// </param>
        /// <param name="offset">Shifts where the steps fall.</param>
        public SnapToStepConverter(float step, float offset = 0f)
        {
            _step = step;
            _offset = offset;
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
        /// Snaps the specified value to the nearest step.
        /// </summary>
        /// <param name="value">The value to snap.</param>
        /// <returns>
        /// The nearest multiple of the step, where a value halfway between two goes to the even one,
        /// so 0.5 snaps to 0 and 1.5 to 2. A step of zero reports an error and returns the value
        /// unchanged.
        /// </returns>
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
        #endregion

        private double Apply(double value)
        {
            // Math.Round sends an exact half to the even step: 0.5 snaps to 0 and 1.5 to 2.
            if (_step != 0f) return Math.Round((value - _offset) / _step) * _step + _offset;

            this.LogError("the step is zero", "Returning the value unchanged.");
            return value;
        }
    }
}
