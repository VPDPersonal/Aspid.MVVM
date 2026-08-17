#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a bound vector with an authored one.
    /// </summary>
    /// <remarks>
    /// The vector counterpart of <see cref="ArithmeticNumberConverter"/>, which only ever existed for
    /// scalars.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector3 Arithmetic", Tooltip = "Combines a bound vector with an authored one")]
    public sealed class Vector3ArithmeticConverter : IConverterVector3
    {
        [Tooltip("What to do with the operand.")]
        [SerializeField] private VectorOperation _operation = VectorOperation.Add;

        [Tooltip("The vector the bound one is combined with.")]
        [SerializeField] private Vector3 _operand;

        public Vector3ArithmeticConverter() { }

        /// <param name="operation">What to do with the operand.</param>
        /// <param name="operand">The vector the bound one is combined with.</param>
        public Vector3ArithmeticConverter(VectorOperation operation, Vector3 operand)
        {
            _operation = operation;
            _operand = operand;
        }

        /// <summary>
        /// Combines the specified vector with the authored operand.
        /// </summary>
        /// <param name="value">The vector to combine.</param>
        /// <returns>The combined vector.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the operation is not a declared value.</exception>
        public Vector3 Convert(Vector3 value) => _operation switch
        {
            VectorOperation.Add => value + _operand,
            VectorOperation.Subtract => value - _operand,
            VectorOperation.Scale => Vector3.Scale(value, _operand),
            VectorOperation.Divide => Divide(value, _operand),
            VectorOperation.Reflect => Vector3.Reflect(value, _operand),
            _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
        };

        // A zero axis in the operand leaves that axis alone rather than producing an infinity, for
        // the same reason the scalar converter degrades instead of dividing by zero.
        private static Vector3 Divide(Vector3 value, Vector3 operand) => new(
            operand.x == 0f ? value.x : value.x / operand.x,
            operand.y == 0f ? value.y : value.y / operand.y,
            operand.z == 0f ? value.z : value.z / operand.z);
    }
}
