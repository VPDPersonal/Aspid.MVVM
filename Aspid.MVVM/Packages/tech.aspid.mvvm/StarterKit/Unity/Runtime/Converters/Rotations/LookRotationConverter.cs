#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Builds a rotation that looks along a direction.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Rotation", Name = "Look Rotation", Tooltip = "Builds a rotation that looks along a direction")]
    public sealed class LookRotationConverter : IConverter<Vector3, Quaternion>
    {
        [Tooltip("Which way is up for the produced rotation.")]
        [SerializeField] private Vector3 _up = Vector3.up;

        [Tooltip("Drop the vertical component before looking, keeping the rotation level.")]
        [SerializeField] private bool _flatten;

        /// <remarks>Default: with world up.</remarks>
        public LookRotationConverter() { }

        /// <param name="up">Which way is up for the produced rotation.</param>
        /// <param name="flatten">Whether to drop the vertical component before looking.</param>
        public LookRotationConverter(Vector3 up, bool flatten = false)
        {
            _up = up;
            _flatten = flatten;
        }

        /// <summary>
        /// Builds a rotation looking along the specified direction.
        /// </summary>
        /// <param name="value">The direction to look along.</param>
        /// <returns>The rotation, or the identity for a zero-length direction.</returns>
        public Quaternion Convert(Vector3 value)
        {
            var direction = _flatten ? new Vector3(value.x, 0f, value.z) : value;

            // LookRotation warns and returns identity on a zero vector; checking first keeps the
            // console clean when a target has not been picked yet.
            return direction.sqrMagnitude <= Mathf.Epsilon
                ? Quaternion.identity
                : Quaternion.LookRotation(direction, _up);
        }
    }
}
