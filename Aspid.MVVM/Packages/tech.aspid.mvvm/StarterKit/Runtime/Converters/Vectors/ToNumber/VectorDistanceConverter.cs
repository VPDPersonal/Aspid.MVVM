#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Measures how far a position is from a target.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/To Number",
        Name = "Distance",
        Tooltip = "Measures how far a position is from a target")]
    public sealed class VectorDistanceConverter :
        IConverter<Vector3, float>,
        IConverter<Vector2, float>
    {
        [Tooltip("The transform the distance is measured to. Leave it empty to measure to the point instead.")]
        [SerializeField] private Transform? _target;

        [Tooltip("The position the distance is measured to when no transform is assigned.")]
        [SerializeField] private Vector3 _point;

        [Tooltip("Ignore the height difference, measuring along the ground only.")]
        [SerializeField] private bool _flattenY;

        /// <remarks>Default: measuring to the origin.</remarks>
        public VectorDistanceConverter() { }

        /// <param name="point">The position the distance is measured to.</param>
        /// <param name="flattenY">Whether to ignore the height difference.</param>
        public VectorDistanceConverter(
            Vector3 point,
            bool flattenY = false)
        {
            _point = point;
            _flattenY = flattenY;
        }

        /// <param name="target">The transform the distance is measured to.</param>
        /// <param name="flattenY">Whether to ignore the height difference.</param>
        public VectorDistanceConverter(
            Transform target,
            bool flattenY = false)
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
            var to = _target == null ? _point : _target.position;
            var offset = value - to;

            if (_flattenY) offset.y = 0f;
            return offset.magnitude;
        }

        float IConverter<Vector2, float>.Convert(Vector2 value)
        {
            var to = _target == null ? _point : _target.position;
            return (value - new Vector2(to.x, to.y)).magnitude;
        }
    }
}
