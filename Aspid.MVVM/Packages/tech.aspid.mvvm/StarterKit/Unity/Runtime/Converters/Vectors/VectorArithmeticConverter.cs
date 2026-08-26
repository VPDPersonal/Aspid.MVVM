#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a bound vector with an authored one.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector",
        Name = "Arithmetic",
        Tooltip = "Combines a bound vector with an authored one")]
    public sealed class VectorArithmeticConverter :
        IConverter<Vector2, Vector2>, IConverter<Vector3, Vector3>, IConverter<Vector4, Vector4>
    {
        [Tooltip("What to do with the operand.")]
        [SerializeField] private VectorOperation _operation = VectorOperation.Add;

        [Tooltip("The vector the bound one is combined with. Only the components the bound vector " +
            "carries are read.")]
        [SerializeField] private Vector4 _operand;

        /// <remarks>Default: adds a zero vector, leaving the value unchanged.</remarks>
        public VectorArithmeticConverter() { }

        /// <param name="operation">What to do with the operand.</param>
        /// <param name="operand">
        /// The vector the bound one is combined with. Only the components the bound vector carries
        /// are read.
        /// </param>
        public VectorArithmeticConverter(VectorOperation operation, Vector4 operand)
        {
            _operation = operation;
            _operand = operand;
        }

        /// <summary>
        /// Combines the specified vector with the authored operand.
        /// </summary>
        /// <param name="value">The vector to combine.</param>
        /// <returns>
        /// The combined vector. Reports an error and returns the value unchanged when the operation
        /// is not a declared value.
        /// </returns>
        public Vector3 Convert(Vector3 value)
        {
            // Vector2 and Vector3 keep Unity's own Reflect rather than the formula written below.
            if (_operation is VectorOperation.Reflect)
                return Vector3.Reflect(value, new Vector3(_operand.x, _operand.y, _operand.z));

            var combined = Combine(new Vector4(value.x, value.y, value.z, 0f));
            return new Vector3(combined.x, combined.y, combined.z);
        }

        Vector2 IConverter<Vector2, Vector2>.Convert(Vector2 value)
        {
            if (_operation is VectorOperation.Reflect)
                return Vector2.Reflect(value, new Vector2(_operand.x, _operand.y));

            var combined = Combine(new Vector4(value.x, value.y, 0f, 0f));
            return new Vector2(combined.x, combined.y);
        }

        Vector4 IConverter<Vector4, Vector4>.Convert(Vector4 value) => Combine(value);

        // A narrower vector arrives with zeros where it has no components, so the operand's are dropped.
        private Vector4 Combine(Vector4 value) => _operation switch
        {
            VectorOperation.Add => value + _operand,
            VectorOperation.Subtract => value - _operand,
            VectorOperation.Scale => Vector4.Scale(value, _operand),
            VectorOperation.Divide => Divide(value, _operand),
            VectorOperation.Reflect => Reflect(value, _operand),
            _ => Undeclared(value)
        };

        private Vector4 Undeclared(Vector4 value)
        {
            this.LogError(
                $"the operation {_operation.Describe()} is not a declared {nameof(VectorOperation)}",
                "Returning the value unchanged.");

            return value;
        }

        // A zero axis in the operand leaves that axis alone rather than producing an infinity.
        private static Vector4 Divide(Vector4 value, Vector4 operand) => new(
            operand.x == 0f ? value.x : value.x / operand.x,
            operand.y == 0f ? value.y : value.y / operand.y,
            operand.z == 0f ? value.z : value.z / operand.z,
            operand.w == 0f ? value.w : value.w / operand.w);

        // Unity has no Vector4.Reflect. This is the formula its narrower ones use, with the normal
        // taken raw, so one longer than unit scales the reflected part.
        private static Vector4 Reflect(Vector4 value, Vector4 normal) =>
            normal * (-2f * Vector4.Dot(normal, value)) + value;
    }
}
