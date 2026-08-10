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
    /// Converts numeric values by applying arithmetic operations with a coefficient.
    /// Supports multiple numeric types (int, float, double, long) with automatic type conversions.
    /// </summary>
    /// <remarks>
    /// Every operation is computed in <see cref="double"/> and cast to the declared return type, so
    /// the int and long overloads truncate toward zero rather than round.
    /// <see cref="NumberOperation.Division"/> by a zero coefficient reports an error and returns the
    /// input unchanged instead of producing an infinity.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Arithmetic Number", Tooltip = "Converts numeric values by applying arithmetic operations with a coefficient")]
    public sealed class ArithmeticNumberConverter :
        IConverterDouble, IConverterIntToDouble, IConverterLongToDouble, IConverterFloatToDouble,
        IConverterFloat, IConverterIntToFloat, IConverterLongToFloat, IConverterDoubleToFloat,
        IConverterInt, IConverterLongToInt, IConverterFloatToInt, IConverterDoubleToInt,
        IConverterLong, IConverterIntToLong, IConverterFloatToLong, IConverterDoubleToLong,
        ITwoWayConverter<double, double>, ITwoWayConverter<float, float>, ITwoWayConverter<int, int>, ITwoWayConverter<long, long>
    {
        [Tooltip("The number the operation is applied with. Division by zero returns the input unchanged.")]
        [SerializeField] private double _coefficient;

        [Tooltip("The arithmetic applied to the bound number.")]
        [SerializeField] private NumberOperation _operation;

        public ArithmeticNumberConverter() { }

        /// <param name="operation">The arithmetic operation to perform.</param>
        /// <param name="coefficient">The coefficient to use in the operation.</param>
        public ArithmeticNumberConverter(NumberOperation operation, double coefficient)
        {
            _operation = operation;
            _coefficient = coefficient;
        }
        
        #region Return int
        int IConverter<int, int>.Convert(int value) =>
            (int)Apply(value);
        
        int IConverter<long, int>.Convert(long value) =>
            (int)Apply(value);
        
        int IConverter<float, int>.Convert(float value) =>
            (int)Apply(value);
        
        int IConverter<double, int>.Convert(double value) =>
            (int)Apply(value);
        #endregion
        
        #region Return long
        long IConverter<long, long>.Convert(long value) => 
            (long)Apply(value);
        
        long IConverter<int, long>.Convert(int value) => 
            (long)Apply(value);
        
        long IConverter<float, long>.Convert(float value) => 
            (long)Apply(value);

        long IConverter<double, long>.Convert(double value) => 
            (long)Apply(value);
        #endregion

        #region Return float
        float IConverter<float, float>.Convert(float value) => 
            (float)Apply(value);
        
        float IConverter<int, float>.Convert(int value) => 
            (float)Apply(value);
        
        float IConverter<long, float>.Convert(long value) => 
            (float)Apply(value);
        
        float IConverter<double, float>.Convert(double value) => 
            (float)Apply(value);
        #endregion

        #region Return double
        double IConverter<double, double>.Convert(double value) => Apply(value);

        /// <summary>
        /// Applies the configured arithmetic to the specified number.
        /// </summary>
        /// <param name="value">The number to transform.</param>
        /// <returns>The result, in <see cref="double"/> whatever the caller's own numeric type.</returns>
        /// <remarks>
        /// Every overload of <c>Convert</c> lands here and casts the result. It is public because the
        /// alternative — reaching the arithmetic through
        /// <c>((IConverter&lt;double, double&gt;)converter).Convert(x)</c> — is a cast a reader has to
        /// decode, and this class did it fifteen times against itself.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the configured operation is not a declared value.</exception>
        public double Apply(double value) => _operation switch
        {
            NumberOperation.Plus => value + _coefficient,
            NumberOperation.Minus => value - _coefficient,
            NumberOperation.Division => Divide(value),
            NumberOperation.Multiply => value * _coefficient,
            NumberOperation.Modulo => Modulo(value),
            NumberOperation.Power => Math.Pow(value, _coefficient),
            NumberOperation.ReverseSubtract => _coefficient - value,
            NumberOperation.ReverseDivide => value != 0 ? _coefficient / value : value,
            _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
        };

        double IConverter<int, double>.Convert(int value) =>
            Apply(value);

        double IConverter<float, double>.Convert(float value) =>
            Apply(value);

        double IConverter<long, double>.Convert(long value) =>
            Apply(value);
        #endregion

        #region Convert back
        double ITwoWayConverter<double, double>.ConvertBack(double value) => 
            Undo(value);

        float ITwoWayConverter<float, float>.ConvertBack(float value) => 
            (float)Undo(value);

        int ITwoWayConverter<int, int>.ConvertBack(int value) => 
            (int)Undo(value);

        long ITwoWayConverter<long, long>.ConvertBack(long value) => 
            (long)Undo(value);

        /// <summary>
        /// Reverses <see cref="Apply"/>.
        /// </summary>
        /// <param name="value">The number to transform back.</param>
        /// <returns>
        /// The number the forward pass was given, or <paramref name="value"/> unchanged where the
        /// operation cannot be undone.
        /// </returns>
        /// <remarks>
        /// <see cref="NumberOperation.Modulo"/> discards which multiple the value came from, and a
        /// zero coefficient makes multiplication and division identities; both return the input.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the configured operation is not a declared value.</exception>
        public double Undo(double value) => _operation switch
        {
            NumberOperation.Plus => value - _coefficient,
            NumberOperation.Minus => value + _coefficient,
            NumberOperation.Division => value * _coefficient,
            NumberOperation.Multiply => Divide(value),
            NumberOperation.Power => _coefficient != 0 ? Math.Pow(value, 1d / _coefficient) : value,
            // Both are their own inverse: c - (c - x) is x, and c / (c / x) is x.
            NumberOperation.ReverseSubtract => _coefficient - value,
            NumberOperation.ReverseDivide => value != 0 ? _coefficient / value : value,
            // Modulo discards which multiple the value came from; there is nothing to undo it with.
            NumberOperation.Modulo => value,
            _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
        };
        #endregion
        
        private double Divide(double value)
        {
            if (_coefficient != 0)
                return value / _coefficient;

            LogDivideByZero();
            return value;
        }

        // C#'s % keeps the sign of the left operand, so -1 % 360 is -1 rather than 359 — which is
        // never what a wrapped angle or a cycling index wants.
        private double Modulo(double value)
        {
            if (_coefficient == 0)
            {
                LogDivideByZero();
                return value;
            }

            var remainder = value % _coefficient;
            return remainder < 0 ? remainder + Math.Abs(_coefficient) : remainder;
        }

        private static void LogDivideByZero() =>
            Debug.LogError($"{nameof(ArithmeticNumberConverter)}: division by zero coefficient. Returning the input value unchanged.");
    }
}