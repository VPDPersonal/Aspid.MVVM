using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class ArithmeticIntConverter : IConverterIntToInt
    {
        [SerializeField] private NumberOperation _operation;
        [SerializeField] private int _coefficient;

        public ArithmeticIntConverter() { }

        public ArithmeticIntConverter(NumberOperation operation, int coefficient)
        {
            _operation = operation;
            _coefficient = coefficient;
        }

        public int Convert(int value) => _operation switch
        {
            NumberOperation.Plus => value + _coefficient,
            NumberOperation.Minus => value - _coefficient,
            NumberOperation.Division => value / _coefficient,
            NumberOperation.Multiply => value * _coefficient,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

}