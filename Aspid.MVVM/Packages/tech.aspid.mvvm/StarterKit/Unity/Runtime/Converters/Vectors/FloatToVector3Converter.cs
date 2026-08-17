#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes one number into the chosen axes of a vector.
    /// </summary>
    /// <remarks>
    /// Uniform scale from one number, a slider driving only Y, a bar growing along X. The binders
    /// currently hard-code this fan-out, so the choice of axes is theirs rather than the author's.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Float To Vector3", Tooltip = "Writes one number into the chosen axes of a vector")]
    public sealed class FloatToVector3Converter : IConverter<float, Vector3>
    {
        [Tooltip("Which axes the number is written into.")]
        [SerializeField] private AxisMask _axes = AxisMask.All;

        [Tooltip("The value used for the axes the number does not write.")]
        [SerializeField] private Vector3 _base = Vector3.one;

        /// <remarks>Default: writing every axis.</remarks>
        public FloatToVector3Converter() { }

        /// <param name="axes">Which axes the number is written into.</param>
        /// <param name="base">The value used for the axes the number does not write.</param>
        public FloatToVector3Converter(AxisMask axes, Vector3 @base = default)
        {
            _axes = axes;
            _base = @base;
        }

        /// <summary>
        /// Writes the specified number into the chosen axes.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The vector.</returns>
        public Vector3 Convert(float value) => new(
            _axes.HasFlag(AxisMask.X) ? value : _base.x,
            _axes.HasFlag(AxisMask.Y) ? value : _base.y,
            _axes.HasFlag(AxisMask.Z) ? value : _base.z);
    }
}
