using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Eases a value between two bounds with <see cref="Mathf.SmoothStep"/>.
    /// </summary>
    [Serializable]
    public sealed class SmoothStepConverter : IConverterFloat
    {
        [Tooltip("The value that maps to 0.")]
        [SerializeField] private float _from;

        [Tooltip("The value that maps to 1.")]
        [SerializeField] private float _to = 1f;

        /// <remarks>Default: over 0..1.</remarks>
        public SmoothStepConverter() { }

        /// <param name="from">The value that maps to 0.</param>
        /// <param name="to">The value that maps to 1.</param>
        public SmoothStepConverter(float from, float to)
        {
            _from = from;
            _to = to;
        }

        /// <summary>
        /// Eases the specified value.
        /// </summary>
        /// <param name="value">The value to ease.</param>
        /// <returns>The eased value.</returns>
        public float Convert(float value) => Mathf.SmoothStep(_from, _to, value);
    }
}
