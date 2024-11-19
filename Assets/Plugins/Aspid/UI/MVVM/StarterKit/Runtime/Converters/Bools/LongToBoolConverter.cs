using System;
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Converters.Bools
{
    [Serializable]
    public sealed class LongToBoolConverter : IConverterLongToBool
    {
        [SerializeField] private Comparisons _comparison;
        [SerializeField] private long _value;

        public LongToBoolConverter() { }
        
        public LongToBoolConverter(Comparisons comparison, long value)
        {
            _comparison = comparison;
            _value = value;
        }

        public bool Convert(long value) => _comparison switch
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