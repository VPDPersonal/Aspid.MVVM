using System;
using UnityEngine;

namespace AspidUI.MVVM.StarterKit.Converters.Numbers
{
    [Serializable]
    public sealed class ArithmeticLongConverter : IConverterLongToLong
    {
        [SerializeField] private NumberOperation _operation;
        [SerializeField] private long _coefficient;

        public ArithmeticLongConverter() { }

        public ArithmeticLongConverter(NumberOperation operation, long coefficient)
        {
            _operation = operation;
            _coefficient = coefficient;
        }

        public long Convert(long value) => _operation switch
        {
            NumberOperation.Plus => value + _coefficient,
            NumberOperation.Minus => value - _coefficient,
            NumberOperation.Division => value / _coefficient,
            NumberOperation.Multiply => value * _coefficient,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}