#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Rounds every axis of a vector.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector",
        Name = "Round",
        Tooltip = "Rounds every axis of a vector")]
    public sealed class VectorRoundConverter :
        IConverter<Vector2, Vector2>, IConverter<Vector3, Vector3>, IConverter<Vector4, Vector4>
    {
        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _mode;

        [Tooltip("The size of one grid step. Zero rounds to whole numbers.")]
        [SerializeField] [Min(0f)] private float _step;

        /// <remarks>Default: rounding to whole numbers.</remarks>
        public VectorRoundConverter() { }

        /// <param name="mode">Which way to drop the fraction.</param>
        /// <param name="step">
        /// The size of one grid step. Zero rounds to whole numbers. A negative step reports an error
        /// and its size is used.
        /// </param>
        public VectorRoundConverter(
            RoundMode mode,
            float step = 0f)
        {
            _mode = mode;
            _step = step;
        }

        /// <summary>
        /// Rounds every axis of the specified vector.
        /// </summary>
        /// <param name="value">The vector to round.</param>
        /// <returns>
        /// The rounded vector. A negative grid step reports an error and snaps to a grid of its size.
        /// Reports an error and returns the value unchanged when the mode is not a declared value.
        /// </returns>
        public Vector3 Convert(Vector3 value) => TryReadStep(out var step)
            ? new Vector3(
                Apply(value.x, step),
                Apply(value.y, step),
                Apply(value.z, step))
            : value;

        Vector2 IConverter<Vector2, Vector2>.Convert(Vector2 value) => TryReadStep(out var step)
            ? new Vector2(
                Apply(value.x, step),
                Apply(value.y, step))
            : value;

        Vector4 IConverter<Vector4, Vector4>.Convert(Vector4 value) => TryReadStep(out var step)
            ? new Vector4(
                Apply(value.x, step),
                Apply(value.y, step),
                Apply(value.z, step),
                Apply(value.w, step))
            : value;

        // Read once per push so a misconfigured setting is reported once, not per axis.
        private bool TryReadStep(out float step)
        {
            step = 1f;

            if (_mode is not (RoundMode.Round or RoundMode.Floor or RoundMode.Ceil or RoundMode.Truncate))
            {
                ReportUndeclaredMode();
                return false;
            }

            step = Step();
            return true;
        }

        private void ReportUndeclaredMode() =>
            this.LogError(
                problem: $"the mode {_mode.Describe()} is not a declared {nameof(RoundMode)}",
                consequence: "Returning the value unchanged.");

        private float Step()
        {
            if (_step is 0f) return 1f;
            if (_step > 0f) return _step;

            // A negative step would mirror the rounding: Floor walks the value up and Ceil down.
            this.LogError(
                problem: $"the grid step {_step} is negative",
                consequence: "Snapping to a grid of its size.");

            return -_step;
        }

        private float Apply(float value, float step)
        {
            var scaled = value / step;

            var rounded = _mode switch
            {
                RoundMode.Round => Mathf.Round(scaled),
                RoundMode.Floor => Mathf.Floor(scaled),
                RoundMode.Ceil => Mathf.Ceil(scaled),
                RoundMode.Truncate => (float)Math.Truncate(scaled),
                // Unreachable: an undeclared mode is screened out in TryReadStep.
                _ => scaled
            };

            return rounded * step;
        }
    }
}
