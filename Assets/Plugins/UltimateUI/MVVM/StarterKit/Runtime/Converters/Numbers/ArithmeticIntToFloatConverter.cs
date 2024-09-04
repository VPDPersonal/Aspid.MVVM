using System;
using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Converters.Numbers
{
    [Serializable]
    public sealed class ArithmeticIntToFloatConverter : IConverter<int, float>
    {
        [SerializeField] private NumberOperation _operation;
        [SerializeField] private float _coefficient;

        public ArithmeticIntToFloatConverter() { }

        public ArithmeticIntToFloatConverter(NumberOperation operation, float coefficient)
        {
            _operation = operation;
            _coefficient = coefficient;
        }

        public float Convert(int value) => _operation switch
        {
            NumberOperation.Plus => value + _coefficient,
            NumberOperation.Minus => value - _coefficient,
            NumberOperation.Division => value / _coefficient,
            NumberOperation.Multiply => value * _coefficient,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

}