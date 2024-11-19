using System;
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Converters.Bools
{
    [Serializable]
    public sealed class FloatToBoolConverter : IConverterFloatToBool
    {
        [SerializeField] private Comparisons _comparison;
        [SerializeField] private float _value;

        public FloatToBoolConverter() { }

        public FloatToBoolConverter(Comparisons comparison, float value)
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
    }

}