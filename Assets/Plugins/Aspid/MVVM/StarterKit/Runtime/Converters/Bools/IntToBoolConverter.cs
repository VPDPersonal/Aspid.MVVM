using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class IntToBoolConverter : IConverterIntToBool
    {
        [SerializeField] private Comparisons _comparison;
        [SerializeField] private int _value;

        public IntToBoolConverter() { }
        
        public IntToBoolConverter(Comparisons comparison, int value)
        {
            _comparison = comparison;
            _value = value;
        }
        
        public bool Convert(int value) => _comparison switch
        {
            Comparisons.Equal => _value == value,
            Comparisons.Inequality => value != _value,
            Comparisons.LessThan => value < _value,
            Comparisons.GreaterThan => value > _value,
            Comparisons.LessThanOrEqual => value <= _value,
            Comparisons.GreaterThanOrEqual => value >= _value,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}