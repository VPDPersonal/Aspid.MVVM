#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes one number into the chosen axes of a vector.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Vector",
        Name = "Float To Vector",
        Tooltip = "Writes one number into the chosen axes of a vector")]
    public sealed class FloatToVectorConverter :
        IConverter<float, Vector3>,
        IConverter<float, Vector2>,
        IConverter<float, Vector4>
    {
        [Tooltip("Which axes the number is written into.")]
        [SerializeField] private AxisMask _axes = AxisMask.All;

        [Tooltip("Values for the axes the number does not write.")]
        [SerializeField] private Vector4 _base = Vector4.one;

        /// <remarks>Default: writing every axis.</remarks>
        public FloatToVectorConverter() { }

        /// <param name="axes">Which axes the number is written into.</param>
        /// <param name="base">
        /// The value used for the axes the number does not write, read as far as the bound vector goes.
        /// </param>
        public FloatToVectorConverter(
            AxisMask axes,
            Vector4 @base = default)
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
            Axis(AxisMask.X, value, _base.x),
            Axis(AxisMask.Y, value, _base.y),
            Axis(AxisMask.Z, value, _base.z));

        Vector2 IConverter<float, Vector2>.Convert(float value) => new(
            Axis(AxisMask.X, value, _base.x),
            Axis(AxisMask.Y, value, _base.y));

        Vector4 IConverter<float, Vector4>.Convert(float value) => new(
            Axis(AxisMask.X, value, _base.x),
            Axis(AxisMask.Y, value, _base.y),
            Axis(AxisMask.Z, value, _base.z),
            Axis(AxisMask.W, value, _base.w));

        private float Axis(AxisMask axis, float value, float fallback) =>
            (_axes & axis) != 0 ? value : fallback;
    }
}
