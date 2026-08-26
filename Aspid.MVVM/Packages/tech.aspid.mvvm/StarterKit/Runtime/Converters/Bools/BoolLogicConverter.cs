using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a bound boolean with an authored one.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Bool",
        Name = "Logic",
        Tooltip = "Combines a bound boolean with an authored one")]
    public sealed class BoolLogicConverter : ITwoWayConverter<bool, bool>
    {
        [Tooltip("How the bound value combines with the operand.")]
        [SerializeField] private LogicOperation _operation;

        [Tooltip("The authored value the bound one combines with.")]
        [SerializeField] private bool _operand;

        [Tooltip("Returned when the operation is undeclared or cannot be undone.")]
        [SerializeField] private ConverterFallback<bool> _fallback = new(false, ConverterFailureMode.ReturnInput);

        private BoolLogicConverter() { }

        /// <param name="operation">How the bound value combines with the operand.</param>
        /// <param name="operand">The authored value the bound one combines with.</param>
        /// <param name="fallback">
        /// Returned when the operation is undeclared or cannot be undone.
        /// When omitted, returns the input value unchanged.
        /// </param>
        public BoolLogicConverter(
            LogicOperation operation,
            bool operand,
            ConverterFallback<bool>? fallback = null)
        {
            _operand = operand;
            _operation = operation;
            _fallback = fallback ?? _fallback;
        }

        /// <summary>
        /// Combines the specified value with the authored operand.
        /// </summary>
        /// <param name="value">The bound boolean.</param>
        /// <returns>The result of the operation, or the fallback when the operation is undeclared.</returns>
        public bool Convert(bool value) => _operation switch
        {
            LogicOperation.And => value && _operand,
            LogicOperation.Or => value || _operand,
            LogicOperation.Xor => value ^ _operand,
            LogicOperation.Nand => !(value && _operand),
            LogicOperation.Nor => !(value || _operand),
            LogicOperation.Xnor => !(value ^ _operand),
            _ => Undeclared(value)
        };

        /// <summary>
        /// Restores the bound value from the combined one.
        /// </summary>
        /// <param name="value">The combined boolean coming back from the View.</param>
        /// <returns>The value the forward pass was given, or the fallback where the operation discards it.</returns>
        /// <remarks>
        /// Only <see cref="LogicOperation.Xor"/> and <see cref="LogicOperation.Xnor"/> undo for either
        /// operand; the other four fall back for one of the two.
        /// </remarks>
        public bool ConvertBack(bool value) => _operation switch
        {
            LogicOperation.Xor => value ^ _operand,
            LogicOperation.Xnor => !(value ^ _operand),
            LogicOperation.And => _operand ? value : Unrecoverable(value),
            LogicOperation.Nand => _operand ? !value : Unrecoverable(value),
            LogicOperation.Or => _operand ? Unrecoverable(value) : value,
            LogicOperation.Nor => _operand ? Unrecoverable(value) : !value,
            _ => Undeclared(value)
        };

        private bool Undeclared(bool value) => _fallback.Fail(
            converter: this,
            value: value,
            problem: $"the operation {_operation.Describe()} is not a declared {nameof(LogicOperation)}");

        private bool Unrecoverable(bool value) => _fallback.Fail(
            converter: this,
            value: value,
            problem: $"{_operation} with a {(_operand ? "true" : "false")} operand cannot be undone");
    }
}
