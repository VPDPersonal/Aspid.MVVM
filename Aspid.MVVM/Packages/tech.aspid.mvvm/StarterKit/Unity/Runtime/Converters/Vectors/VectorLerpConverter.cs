#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Moves between two vectors by a 0..1 amount.
    /// </summary>
    /// <remarks>A marker travelling along a track as progress advances.</remarks>
    [Serializable]
    public sealed class VectorLerpConverter : IConverter<float, Vector3>
    {
        [Tooltip("The vector at 0.")]
        [SerializeField] private Vector3 _from;

        [Tooltip("The vector at 1.")]
        [SerializeField] private Vector3 _to = Vector3.one;

        [Tooltip("Hold the incoming amount inside 0..1.")]
        [SerializeField] private bool _clamp = true;

        /// <remarks>Default: going zero to one.</remarks>
        public VectorLerpConverter() { }

        /// <param name="from">The vector at 0.</param>
        /// <param name="to">The vector at 1.</param>
        public VectorLerpConverter(Vector3 from, Vector3 to)
        {
            _from = from;
            _to = to;
        }

        /// <summary>
        /// Reads the vector at the specified amount.
        /// </summary>
        /// <param name="value">The 0..1 amount.</param>
        /// <returns>The vector there.</returns>
        public Vector3 Convert(float value) =>
            _clamp ? Vector3.Lerp(_from, _to, value) : Vector3.LerpUnclamped(_from, _to, value);
    }
}
