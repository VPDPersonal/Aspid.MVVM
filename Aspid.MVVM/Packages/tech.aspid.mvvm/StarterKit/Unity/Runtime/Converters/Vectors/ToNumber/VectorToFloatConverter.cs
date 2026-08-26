#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Measures one number out of a vector.
    /// </summary>
    /// <remarks>
    /// A component the bound vector does not carry — Z on a <see cref="Vector2"/>, W on anything
    /// narrower than a <see cref="Vector4"/> — is reported on every push and reads as zero.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/To Number",
        Name = "To Float",
        Tooltip = "Measures one number out of a vector")]
    public sealed class VectorToFloatConverter :
        IConverter<Vector3, float>,
        IConverter<Vector2, float>,
        IConverter<Vector4, float>
    {
        [Tooltip("Which number to take. Only the components the bound vector carries can be read.")]
        [SerializeField] private VectorComponent _component = VectorComponent.Magnitude;

        [Tooltip("The direction Dot measures along, read as far as the bound vector goes. " +
            "Keep it unit length to read a plain distance.")]
        [SerializeField] private Vector4 _dotAgainst = new(0f, 1f, 0f, 0f);

        /// <remarks>Default: measuring length.</remarks>
        public VectorToFloatConverter() { }

        /// <param name="component">Which number to take.</param>
        public VectorToFloatConverter(VectorComponent component)
        {
            _component = component;
        }

        /// <remarks>Selects <see cref="VectorComponent.Dot"/>.</remarks>
        /// <param name="dotAgainst">
        /// The direction to measure along, read as far as the bound vector goes. Keep it unit length
        /// to read a plain distance.
        /// </param>
        public VectorToFloatConverter(Vector4 dotAgainst)
        {
            _component = VectorComponent.Dot;
            _dotAgainst = dotAgainst;
        }

        /// <summary>
        /// Measures the specified vector.
        /// </summary>
        /// <param name="value">The vector to measure.</param>
        /// <returns>
        /// The measurement. <see cref="VectorComponent.Dot"/> is the raw dot product, so a unit
        /// direction reads as the signed distance along it and a longer one scales that reading. Reports
        /// an error and returns zero when the component is not one this vector carries.
        /// </returns>
        public float Convert(Vector3 value) => _component switch
        {
            VectorComponent.X => value.x,
            VectorComponent.Y => value.y,
            VectorComponent.Z => value.z,
            VectorComponent.Magnitude => value.magnitude,
            VectorComponent.SqrMagnitude => value.sqrMagnitude,
            VectorComponent.Dot => Vector3.Dot(value, _dotAgainst),
            VectorComponent.W => Missing("a Vector3"),
            _ => Undeclared()
        };

        float IConverter<Vector2, float>.Convert(Vector2 value) => _component switch
        {
            VectorComponent.X => value.x,
            VectorComponent.Y => value.y,
            VectorComponent.Magnitude => value.magnitude,
            VectorComponent.SqrMagnitude => value.sqrMagnitude,
            VectorComponent.Dot => Vector2.Dot(value, _dotAgainst),
            VectorComponent.Z or VectorComponent.W => Missing("a Vector2"),
            _ => Undeclared()
        };

        float IConverter<Vector4, float>.Convert(Vector4 value) => _component switch
        {
            VectorComponent.X => value.x,
            VectorComponent.Y => value.y,
            VectorComponent.Z => value.z,
            VectorComponent.W => value.w,
            VectorComponent.Magnitude => value.magnitude,
            VectorComponent.SqrMagnitude => value.sqrMagnitude,
            VectorComponent.Dot => Vector4.Dot(value, _dotAgainst),
            _ => Undeclared()
        };

        // Returning zero for a component the binding cannot carry would read as a real measurement.
        private float Missing(string width)
        {
            this.LogError(
                $"the component {_component.Describe()} is not one {width} carries",
                "Returning zero.");

            return 0f;
        }

        private float Undeclared()
        {
            this.LogError(
                $"the component {_component.Describe()} is not a declared {nameof(VectorComponent)}",
                "Returning zero.");

            return 0f;
        }
    }
}
