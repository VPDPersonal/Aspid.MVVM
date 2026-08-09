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
    public class ArithmeticNumberConverter :
        IConverterDouble, IConverterIntToDouble, IConverterLongToDouble, IConverterFloatToDouble,
        IConverterFloat, IConverterIntToFloat, IConverterLongToFloat, IConverterDoubleToFloat,
        IConverterInt, IConverterLongToInt, IConverterFloatToInt, IConverterDoubleToInt,
        IConverterLong, IConverterIntToLong, IConverterFloatToLong, IConverterDoubleToLong,
        ITwoWayConverter<double, double>,
        ITwoWayConverter<float, float>,
        ITwoWayConverter<int, int>,
        ITwoWayConverter<long, long>
    {
        [SerializeField] private NumberOperation _operation;
        [SerializeField] private double _coefficient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticNumberConverter"/> class with default settings.
        /// </summary>
        public ArithmeticNumberConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticNumberConverter"/> class.
        /// </summary>
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
            _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
        };

        private double Divide(double value)
        {
            if (_coefficient != 0)
                return value / _coefficient;

            Debug.LogError($"{nameof(ArithmeticNumberConverter)}: division by zero coefficient. Returning the input value unchanged.");
            return value;
        }

        double IConverter<int, double>.Convert(int value) =>
            ((IConverter<double, double>)this).Convert(value);
        
        double IConverter<float, double>.Convert(float value) =>
            ((IConverter<double, double>)this).Convert(value);
        
        double IConverter<long, double>.Convert(long value) =>
            ((IConverter<double, double>)this).Convert(value);
        #endregion

        #region Convert back
        // Only the same-type conversions are reversible: a binder in a reverse mode is constrained to
        // IConverter<TProperty, TProperty>, and the cross-type overloads narrow, which cannot be undone.
        double ITwoWayConverter<double, double>.ConvertBack(double value) => Undo(value);

        float ITwoWayConverter<float, float>.ConvertBack(float value) => (float)Undo(value);

        int ITwoWayConverter<int, int>.ConvertBack(int value) => (int)Undo(value);

        long ITwoWayConverter<long, long>.ConvertBack(long value) => (long)Undo(value);

        private double Undo(double value) => _operation switch
        {
            NumberOperation.Plus => value - _coefficient,
            NumberOperation.Minus => value + _coefficient,
            NumberOperation.Division => value * _coefficient,
            NumberOperation.Multiply => UndoMultiply(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
        };

        // Mirrors Divide: a zero coefficient makes the forward pass an identity, so undoing it is one too.
        private double UndoMultiply(double value)
        {
            if (_coefficient != 0) return value / _coefficient;

            LogDivideByZero();
            return value;
        }
        #endregion
    }
}