#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns an angle into a direction.
    /// </summary>
    /// <remarks>Placing a marker on a radial HUD.</remarks>
    [Serializable]
    public sealed class AngleToDirectionConverter : IConverter<float, Vector2>
    {
        [Tooltip("The angle is in degrees rather than radians.")]
        [SerializeField] private bool _degrees = true;

        [Tooltip("How long the produced direction is.")]
        [SerializeField] private float _magnitude = 1f;

        /// <remarks>Default: producing a unit vector.</remarks>
        public AngleToDirectionConverter() { }

        /// <param name="magnitude">How long the produced direction is.</param>
        public AngleToDirectionConverter(float magnitude)
        {
            _magnitude = magnitude;
        }

        /// <summary>
        /// Turns the specified angle into a direction.
        /// </summary>
        /// <param name="value">The angle.</param>
        /// <returns>The direction.</returns>
        public Vector2 Convert(float value)
        {
            var radians = _degrees ? value * Mathf.Deg2Rad : value;
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * _magnitude;
        }
    }
}
