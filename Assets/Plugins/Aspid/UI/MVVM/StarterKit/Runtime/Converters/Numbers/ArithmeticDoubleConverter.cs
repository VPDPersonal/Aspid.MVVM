using System;
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Converters.Numbers
{
    [Serializable]
    public sealed class ArithmeticDoubleConverter : IConverterDoubleToDouble
    {
        [SerializeField] private NumberOperation _operation;
        [SerializeField] private double _coefficient;

        public ArithmeticDoubleConverter() { }

        public ArithmeticDoubleConverter(NumberOperation operation, double coefficient)
        {
            _operation = operation;
            _coefficient = coefficient;
        }

        public double Convert(double value) => _operation switch
        {
            NumberOperation.Plus => value + _coefficient,
            NumberOperation.Minus => value - _coefficient,
            NumberOperation.Division => value / _coefficient,
            NumberOperation.Multiply => value * _coefficient,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}