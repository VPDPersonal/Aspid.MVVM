#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class NumberToBoolConverter : IConverterFloatToBool, IConverterDoubleToBool, IConverterIntToBool, IConverterLongToBool
    {
        [SerializeField] private Comparisons _comparison;
        [SerializeField] private float _value;

        public NumberToBoolConverter() { }

        public NumberToBoolConverter(Comparisons comparison, float value)
        {
            _comparison = comparison;
            _value = value;
        }
        
        public bool Convert(float value) => _comparison switch
        {
            Comparisons.Equal => Mathf.Approximately(_value, value),
            Comparisons.Inequality => !Mathf.Approximately(_value, value),
            Comparisons.LessThan => value < _value,
            Comparisons.GreaterThan => value > _value,
            Comparisons.LessThanOrEqual => value <= _value,
            Comparisons.GreaterThanOrEqual => value >= _value,
            _ => throw new ArgumentOutOfRangeException()
        };

        public bool Convert(double value) =>
            Convert((float)value);

        public bool Convert(int value) =>
            Convert((float)value);

        public bool Convert(long value) =>
            Convert((float)value);
    }
}