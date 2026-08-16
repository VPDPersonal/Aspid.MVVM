using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The boolean operations <see cref="BoolLogicConverter"/> can apply.
    /// </summary>
    public enum LogicOperation
    {
        /// <summary>Both the bound value and the operand must be <see langword="true"/>.</summary>
        And,

        /// <summary>Either the bound value or the operand must be <see langword="true"/>.</summary>
        Or,

        /// <summary>Exactly one of the bound value and the operand must be <see langword="true"/>.</summary>
        Xor,

        /// <summary>The negation of <see cref="And"/>.</summary>
        Nand,

        /// <summary>The negation of <see cref="Or"/>.</summary>
        Nor,

        /// <summary>The negation of <see cref="Xor"/> — the two must agree.</summary>
        Xnor,
    }

    /// <summary>
    /// Combines a bound boolean with an authored one.
    /// </summary>
    /// <remarks>
    /// Useful for gating a binding on a design-time switch — a debug overlay bound to <c>IsVisible</c>
    /// but held off in a shipping scene — without the ViewModel knowing the scene exists.
    /// </remarks>
    [Serializable]
    public sealed class BoolLogicConverter : IConverter<bool, bool>
    {
        [Tooltip("How the bound value combines with the operand.")]
        [SerializeField] private LogicOperation _operation;

        [Tooltip("The authored value the bound one combines with.")]
        [SerializeField] private bool _operand;

        public BoolLogicConverter() { }

        /// <param name="operation">How the bound value combines with the operand.</param>
        /// <param name="operand">The authored value the bound one combines with.</param>
        public BoolLogicConverter(LogicOperation operation, bool operand)
        {
            _operation = operation;
            _operand = operand;
        }

        /// <summary>
        /// Combines the specified value with the authored operand.
        /// </summary>
        /// <param name="value">The bound boolean.</param>
        /// <returns>The result of the operation.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the operation is not a declared value.</exception>
        public bool Convert(bool value) => _operation switch
        {
            LogicOperation.And => value && _operand,
            LogicOperation.Or => value || _operand,
            LogicOperation.Xor => value ^ _operand,
            LogicOperation.Nand => !(value && _operand),
            LogicOperation.Nor => !(value || _operand),
            LogicOperation.Xnor => !(value ^ _operand),
            _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
        };
    }
}
