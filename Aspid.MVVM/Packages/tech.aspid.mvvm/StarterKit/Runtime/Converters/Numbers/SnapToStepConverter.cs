using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Snaps a number to the nearest multiple of a step.
    /// </summary>
    /// <remarks>An exact half goes to the even step: 0.5 snaps to 0, 1.5 to 2.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Snap To Step",
        Tooltip = "Snaps a number to the nearest multiple of a step")]
    public sealed class SnapToStepConverter : NumberConverter
    {
        [Tooltip("The size of one step. Zero passes the value through.")]
        [SerializeField] private float _step = 1f;

        [Tooltip("Shifts where the steps fall.")]
        [SerializeField] private float _offset;

        /// <remarks>Default: snapping to whole numbers.</remarks>
        public SnapToStepConverter() { }

        /// <param name="step">The size of one step. Zero reports an error and passes the value through.</param>
        /// <param name="offset">Shifts where the steps fall.</param>
        public SnapToStepConverter(
            float step,
            float offset = 0f)
        {
            _step = step;
            _offset = offset;
        }

        /// <summary>
        /// Snaps the number to the nearest step.
        /// </summary>
        /// <param name="value">The number to snap.</param>
        /// <returns>The nearest multiple of the step. A zero step reports an error and returns the value unchanged.</returns>
        protected override double Apply(double value)
        {
            if (_step is not 0f) return Math.Round((value - _offset) / _step) * _step + _offset;

            this.LogError(
                problem: "the step is zero",
                consequence: "Returning the value unchanged.");

            return value;
        }
    }
}
