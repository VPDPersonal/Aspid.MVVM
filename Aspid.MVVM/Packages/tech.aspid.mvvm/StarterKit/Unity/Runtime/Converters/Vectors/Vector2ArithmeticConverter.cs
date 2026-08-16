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
    /// Combines a bound 2D vector with an authored one.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart of <see cref="Vector3ArithmeticConverter"/>. Anything authored in two
    /// dimensions — an anchored position, a size delta, a sprite offset — had to widen to
    /// <see cref="Vector3"/> and back to do arithmetic at all.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector2 Arithmetic", Tooltip = "Combines a bound 2D vector with an authored one")]
    public sealed class Vector2ArithmeticConverter : IConverterVector2
    {
        [Tooltip("What to do with the operand.")]
        [SerializeField] private VectorOperation _operation = VectorOperation.Add;

        [Tooltip("The vector the bound one is combined with.")]
        [SerializeField] private Vector2 _operand;

        public Vector2ArithmeticConverter() { }

        /// <param name="operation">What to do with the operand.</param>
        /// <param name="operand">The vector the bound one is combined with.</param>
        public Vector2ArithmeticConverter(VectorOperation operation, Vector2 operand)
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
        public Vector2 Convert(Vector2 value) => _operation switch
        {
            VectorOperation.Add => value + _operand,
            VectorOperation.Subtract => value - _operand,
            VectorOperation.Scale => Vector2.Scale(value, _operand),
            VectorOperation.Divide => Divide(value, _operand),
            VectorOperation.Reflect => Vector2.Reflect(value, _operand),
            _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
        };

        // Matches the Vector3 converter: a zero axis in the operand leaves that axis alone rather
        // than producing an infinity.
        private static Vector2 Divide(Vector2 value, Vector2 operand) => new(
            operand.x == 0f ? value.x : value.x / operand.x,
            operand.y == 0f ? value.y : value.y / operand.y);
    }
}
