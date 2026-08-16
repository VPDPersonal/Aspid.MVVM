#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Measures how far a position is from a target.
    /// </summary>
    /// <remarks>
    /// The "300 m" on a waypoint marker, or a proximity bar that fills as the player closes in.
    /// Without it the ViewModel has to hold a <see cref="Transform"/> and read the scene every time
    /// the distance is asked for, which is the one thing a ViewModel is meant not to do.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector Distance", Tooltip = "Measures how far a position is from a target")]
    public sealed class VectorDistanceConverter : IConverter<Vector3, float>
    {
        [Tooltip("The transform the distance is measured to. Leave it empty to measure to the point below.")]
        [SerializeField] private Transform? _target;

        [Tooltip("The position the distance is measured to when no transform is assigned.")]
        [SerializeField] private Vector3 _point;

        [Tooltip("Ignore the height difference, measuring along the ground only.")]
        [SerializeField] private bool _flattenY;

        /// <remarks>Default: measuring to the origin.</remarks>
        public VectorDistanceConverter() { }

        /// <remarks>Default: measuring to a point.</remarks>
        /// <param name="point">The position the distance is measured to.</param>
        /// <param name="flattenY">Whether to ignore the height difference.</param>
        public VectorDistanceConverter(Vector3 point, bool flattenY = false)
        {
            _point = point;
            _flattenY = flattenY;
        }

        /// <remarks>Default: measuring to a transform.</remarks>
        /// <param name="target">The transform the distance is measured to.</param>
        /// <param name="flattenY">Whether to ignore the height difference.</param>
        public VectorDistanceConverter(Transform target, bool flattenY = false)
        {
            _target = target;
            _flattenY = flattenY;
        }

        /// <summary>
        /// Measures the specified position against the target.
        /// </summary>
        /// <param name="value">The position to measure from.</param>
        /// <returns>The distance to the target, in world units.</returns>
        public float Convert(Vector3 value)
        {
            // Unity's overloaded == is deliberate: `is null` reports false for a destroyed object,
            // whose managed reference is still alive. An empty field is not a failure either — the
            // authored point is what the converter measures to then, so nothing is reported.
            var to = _target == null ? _point : _target.position;
            var offset = value - to;

            if (_flattenY) offset.y = 0f;
            return offset.magnitude;
        }
    }
}
