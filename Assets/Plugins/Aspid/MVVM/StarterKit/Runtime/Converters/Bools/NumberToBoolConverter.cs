using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class NumberToBoolConverter : 
        IConverterFloatToBool,
        IConverterDoubleToBool,
        IConverterIntToBool,
        IConverterLongToBool
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField] 
#endif
        private Comparisons _comparison;
        
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField] 
#endif
        private float _value;

        public NumberToBoolConverter() { }

        public NumberToBoolConverter(Comparisons comparison, float value)
        {
            _comparison = comparison;
            _value = value;
        }
        
        public bool Convert(float value) => _comparison switch
        {
            Comparisons.LessThan => value < _value,
            Comparisons.GreaterThan => value > _value,
            Comparisons.LessThanOrEqual => value <= _value,
            Comparisons.GreaterThanOrEqual => value >= _value,
            Comparisons.Equal => Approximately(_value, value),
            Comparisons.Inequality => Approximately(_value, value),
            _ => throw new ArgumentOutOfRangeException()
        };

        public bool Convert(double value) =>
            Convert((float)value);

        public bool Convert(int value) =>
            Convert((float)value);

        public bool Convert(long value) =>
            Convert((float)value);
        
        private static bool Approximately(double a, double b) =>
            Math.Abs(b - a) < Math.Max(1E-06f * Math.Max(Math.Abs(a), Math.Abs(b)), float.Epsilon * 8f);
    }
}