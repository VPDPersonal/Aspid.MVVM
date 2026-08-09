using Aspid.FastTools.Types;
using System;
using UnityEngine;

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
    public class ArithmeticNumberConverter :
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
            (int)((IConverter<double, double>)this).Convert(value);
        
        int IConverter<long, int>.Convert(long value) =>
            (int)((IConverter<double, double>)this).Convert(value);
        
        int IConverter<float, int>.Convert(float value) =>
            (int)((IConverter<double, double>)this).Convert(value);
        
        int IConverter<double, int>.Convert(double value) =>
            (int)((IConverter<double, double>)this).Convert(value);
        #endregion
        
        #region Return long
        long IConverter<long, long>.Convert(long value) => 
            (long)((IConverter<double, double>)this).Convert(value);
        
        long IConverter<int, long>.Convert(int value) => 
            (long)((IConverter<double, double>)this).Convert(value);
        
        long IConverter<float, long>.Convert(float value) => 
            (long)((IConverter<double, double>)this).Convert(value);

        long IConverter<double, long>.Convert(double value) => 
            (long)((IConverter<double, double>)this).Convert(value);
        #endregion

        #region Return float
        float IConverter<float, float>.Convert(float value) => 
            (float)((IConverter<double, double>)this).Convert(value);
        
        float IConverter<int, float>.Convert(int value) => 
            (float)((IConverter<double, double>)this).Convert(value);
        
        float IConverter<long, float>.Convert(long value) => 
            (float)((IConverter<double, double>)this).Convert(value);
        
        float IConverter<double, float>.Convert(double value) => 
            (float)((IConverter<double, double>)this).Convert(value);
        #endregion

        #region Return double
        double IConverter<double, double>.Convert(double value) => _operation switch
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
            ((IConverter<double, double>)this).Convert(value);
        
        double IConverter<float, double>.Convert(float value) =>
            ((IConverter<double, double>)this).Convert(value);
        
        double IConverter<long, double>.Convert(long value) =>
            ((IConverter<double, double>)this).Convert(value);
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

        private double Undo(double value) => _operation switch
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