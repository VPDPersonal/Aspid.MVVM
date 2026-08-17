using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Maps a number from one range onto another.
    /// </summary>
    /// <remarks>
    /// The most common transformation in game UI: health onto a bar, temperature onto a gauge,
    /// distance onto an arrow. Without it the coefficient has to be worked out by hand and hidden in
    /// a two-link chain, where nobody can see what range it came from.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Remap Number", Tooltip = "Maps a number from one range onto another")]
    public sealed class RemapNumberConverter : ITwoWayConverter<float, float>
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

        /// <summary>
        /// Maps the specified value from the incoming range onto the outgoing one.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <returns>The mapped value. A degenerate incoming range yields the outgoing low end.</returns>
        public float Convert(float value) => Map(value, _fromMin, _fromMax, _toMin, _toMax, _clamp);

        /// <summary>
        /// Maps the specified value back from the outgoing range onto the incoming one.
        /// </summary>
        /// <param name="value">The value to map back.</param>
        /// <returns>The value in the incoming range.</returns>
        public float ConvertBack(float value) => Map(value, _toMin, _toMax, _fromMin, _fromMax, _clamp);

        internal static float Map(float value, float fromMin, float fromMax, float toMin, float toMax, bool clamp)
        {
            var span = fromMax - fromMin;
            if (span == 0f) return toMin;

            var t = (value - fromMin) / span;
            if (clamp) t = Mathf.Clamp01(t);

            return toMin + (toMax - toMin) * t;
        }
    }
}
