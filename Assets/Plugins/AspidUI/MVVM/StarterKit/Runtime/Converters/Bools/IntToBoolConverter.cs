using System;
using UnityEngine;

namespace AspidUI.MVVM.StarterKit.Converters.Bools
{
    [Serializable]
    public sealed class IntToBoolConverter : IConverter<int, bool>
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