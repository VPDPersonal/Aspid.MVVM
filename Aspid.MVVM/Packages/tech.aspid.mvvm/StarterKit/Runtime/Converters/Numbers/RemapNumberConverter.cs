#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Maps a number from one range onto another.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Remap",
        Tooltip = "Maps a number from one range onto another")]
    public sealed class RemapNumberConverter : TwoWayNumberConverter
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
        public RemapNumberConverter(
            float fromMin,
            float fromMax,
            float toMin,
            float toMax,
            bool clamp = true)
        {
            _toMin = toMin;
            _toMax = toMax;
            _clamp = clamp;
            _fromMin = fromMin;
            _fromMax = fromMax;
        }

        /// <summary>
        /// Maps the number from the incoming range onto the outgoing one.
        /// </summary>
        /// <param name="value">The number to map.</param>
        /// <returns>The mapped number. A degenerate incoming range yields the outgoing low end.</returns>
        protected override double Apply(double value) =>
            Map(value, _fromMin, _fromMax, _toMin, _toMax, _clamp);

        /// <summary>
        /// Maps the number back from the outgoing range onto the incoming one.
        /// </summary>
        /// <param name="value">The number to map back.</param>
        /// <returns>The number in the incoming range. A degenerate outgoing range yields its low end.</returns>
        protected override double Undo(double value) =>
            Map(value, _toMin, _toMax, _fromMin, _fromMax, _clamp);

        /// <inheritdoc cref="Map(double,double,double,double,double,bool)"/>
        internal static float Map(
            float value,
            float fromMin,
            float fromMax,
            float toMin,
            float toMax,
            bool clamp) =>
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
            if (span is 0d) return toMin;

            var t = (value - fromMin) / span;
            if (clamp) t = t < 0d ? 0d : t > 1d ? 1d : t;

            return toMin + (toMax - toMin) * t;
        }
    }
}
