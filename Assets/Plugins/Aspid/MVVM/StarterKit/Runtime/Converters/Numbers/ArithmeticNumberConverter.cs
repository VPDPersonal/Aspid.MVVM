using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class ArithmeticNumberConverter : 
        IConverterDouble, IConverterIntToDouble, IConverterLongToDouble, IConverterFloatToDouble,
        IConverterFloat, IConverterIntToFloat, IConverterLongToFloat, IConverterDoubleToFloat,
        IConverterInt, IConverterLongToInt, IConverterFloatToInt, IConverterDoubleToInt,
        IConverterLong, IConverterIntToLong, IConverterFloatToLong, IConverterDoubleToLong
    {
        [SerializeField] private NumberOperation _operation;
        [SerializeField] private double _coefficient;

        public ArithmeticNumberConverter() { }

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
            NumberOperation.Division => value / _coefficient,
            NumberOperation.Multiply => value * _coefficient,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        double IConverter<int, double>.Convert(int value) =>
            ((IConverter<double, double>)this).Convert(value);
        
        double IConverter<float, double>.Convert(float value) =>
            ((IConverter<double, double>)this).Convert(value);
        
        double IConverter<long, double>.Convert(long value) =>
            ((IConverter<double, double>)this).Convert(value);
        #endregion
    }
}