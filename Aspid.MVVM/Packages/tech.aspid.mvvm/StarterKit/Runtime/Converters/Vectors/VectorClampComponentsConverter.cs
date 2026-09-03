#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Keeps every axis of a vector between two bounds.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector",
        Name = "Clamp Components",
        Tooltip = "Keeps every axis of a vector between two bounds")]
    public sealed class VectorClampComponentsConverter :
        IConverter<Vector2, Vector2>, IConverter<Vector3, Vector3>, IConverter<Vector4, Vector4>
    {
        [Tooltip("Lowest value per axis. Only the components the bound vector carries are read.")]
        [SerializeField] private Vector4 _min = new(-1f, -1f, -1f, -1f);

        [Tooltip("Highest value per axis. Only the components the bound vector carries are read.")]
        [SerializeField] private Vector4 _max = Vector4.one;

        /// <remarks>Default: clamping to ±1.</remarks>
        public VectorClampComponentsConverter() { }

        /// <param name="min">
        /// The lowest each axis is allowed to be. Only the components the bound vector carries are
        /// read, and bounds the wrong way round on an axis are reported and swapped.
        /// </param>
        /// <param name="max">
        /// The highest each axis is allowed to be. Only the components the bound vector carries are
        /// read, and bounds the wrong way round on an axis are reported and swapped.
        /// </param>
        public VectorClampComponentsConverter(
            Vector4 min,
            Vector4 max)
        {
            _min = min;
            _max = max;
        }

        /// <summary>
        /// Clamps every axis of the specified vector.
        /// </summary>
        /// <param name="value">The vector to clamp.</param>
        /// <returns>
        /// The clamped vector. An axis whose bounds are typed the wrong way round reports an error
        /// and is clamped to the swapped pair.
        /// </returns>
        public Vector3 Convert(Vector3 value)
        {
            ReportInvertedAxes(axes: 3);

            return new Vector3(
                ClampComponent(value.x, _min.x, _max.x),
                ClampComponent(value.y, _min.y, _max.y),
                ClampComponent(value.z, _min.z, _max.z));
        }

        Vector2 IConverter<Vector2, Vector2>.Convert(Vector2 value)
        {
            ReportInvertedAxes(axes: 2);

            return new Vector2(
                ClampComponent(value.x, _min.x, _max.x),
                ClampComponent(value.y, _min.y, _max.y));
        }

        Vector4 IConverter<Vector4, Vector4>.Convert(Vector4 value)
        {
            ReportInvertedAxes(axes: 4);

            return new Vector4(
                ClampComponent(value.x, _min.x, _max.x),
                ClampComponent(value.y, _min.y, _max.y),
                ClampComponent(value.z, _min.z, _max.z),
                ClampComponent(value.w, _min.w, _max.w));
        }

        private void ReportInvertedAxes(int axes)
        {
            var inverted = _min.x > _max.x
                || _min.y > _max.y
                || (axes > 2 && _min.z > _max.z)
                || (axes > 3 && _min.w > _max.w);

            if (!inverted) return;

            this.LogError(
                problem: $"the minimum {_min} is above the maximum {_max} on at least one axis",
                consequence: "Clamping those axes to the swapped bounds.");
        }

        // Mathf.Clamp with the bounds the wrong way round returns the minimum for every input.
        internal static float ClampComponent(float value, float min, float max) =>
            min <= max ? Mathf.Clamp(value, min, max) : Mathf.Clamp(value, max, min);
    }
}
