using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Snaps a number to the nearest multiple of a step.
    /// </summary>
    /// <remarks>
    /// A volume slider that moves in fives, a rotation that lands on 45° marks. Doing it in the
    /// ViewModel means the ViewModel knows how the control is drawn.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Snap To Step", Tooltip = "Snaps a number to the nearest multiple of a step")]
    public sealed class SnapToStepConverter : IConverterFloat
    {
        [Tooltip("The size of one step. A step of zero passes the value through.")]
        [SerializeField] private float _step = 1f;

        [Tooltip("Shifts where the steps fall.")]
        [SerializeField] private float _offset;

        /// <remarks>Default: snapping to whole numbers.</remarks>
        public SnapToStepConverter() { }

        /// <param name="step">The size of one step.</param>
        /// <param name="offset">Shifts where the steps fall.</param>
        public SnapToStepConverter(float step, float offset = 0f)
        {
            _step = step;
            _offset = offset;
        }

        /// <summary>
        /// Snaps the specified value to the nearest step.
        /// </summary>
        /// <param name="value">The value to snap.</param>
        /// <returns>The nearest multiple of the step, or the value unchanged when the step is zero.</returns>
        public float Convert(float value) =>
            _step == 0f ? value : Mathf.Round((value - _offset) / _step) * _step + _offset;
    }
}
