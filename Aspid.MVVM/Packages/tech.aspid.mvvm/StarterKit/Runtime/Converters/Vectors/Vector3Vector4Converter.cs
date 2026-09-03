#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Widens a vector to four components, and narrows one back by dropping a component.
    /// </summary>
    /// <remarks>
    /// The round trip returns the vector it was given only while the dropped component is the one the
    /// widening wrote.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector",
        Name = "Vector3 To Vector4",
        Tooltip = "Widens a vector to four components, and narrows one back by dropping a component")]
    public sealed class Vector3Vector4Converter :
        ITwoWayConverter<Vector3, Vector4>,
        ITwoWayConverter<Vector4, Vector3>
    {
        [Tooltip("The value written into the fourth component.")]
        [SerializeField] private float _w;

        [Tooltip("Which component is left out on the way back. The other three keep their order.")]
        [SerializeField] private Vector4Component _drop = Vector4Component.W;

        /// <remarks>Default: writing a zero fourth component and dropping it again.</remarks>
        public Vector3Vector4Converter() { }

        /// <param name="w">The value written into the fourth component.</param>
        /// <param name="drop">
        /// Which component is left out on the way back. When omitted, the fourth one.
        /// </param>
        public Vector3Vector4Converter(
            float w,
            Vector4Component drop = Vector4Component.W)
        {
            _w = w;
            _drop = drop;
        }

        /// <summary>
        /// Widens the specified vector.
        /// </summary>
        /// <param name="value">The vector to widen.</param>
        /// <returns>The four-component vector.</returns>
        public Vector4 Convert(Vector3 value) =>
            new(value.x, value.y, value.z, _w);

        /// <summary>
        /// Narrows the specified vector by dropping the configured component.
        /// </summary>
        /// <param name="value">The vector to narrow.</param>
        /// <returns>
        /// The three-component vector. Reports an error and drops W when the component is not a
        /// declared <see cref="Vector4Component"/> value.
        /// </returns>
        public Vector3 ConvertBack(Vector4 value) => _drop switch
        {
            Vector4Component.X => new Vector3(value.y, value.z, value.w),
            Vector4Component.Y => new Vector3(value.x, value.z, value.w),
            Vector4Component.Z => new Vector3(value.x, value.y, value.w),
            Vector4Component.W => new Vector3(value.x, value.y, value.z),
            _ => Undeclared(value)
        };

        Vector3 IConverter<Vector4, Vector3>.Convert(Vector4 value) =>
            ConvertBack(value);

        Vector4 ITwoWayConverter<Vector4, Vector3>.ConvertBack(Vector3 value) =>
            Convert(value);

        private Vector3 Undeclared(Vector4 value)
        {
            this.LogError(
                problem: $"the component {_drop.Describe()} is not a declared {nameof(Vector4Component)}",
                consequence: "Dropping W.");

            return new Vector3(value.x, value.y, value.z);
        }
    }
}
