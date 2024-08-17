using System;
using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.StarterKit.Logical;
using UltimateUI.MVVM.StarterKit.Converters.Number;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Casters
{
    public class NumberToBoolCasterBinder : MonoBinder, INumberBinder
    {
        [SerializeField] private bool _isConvert;
        [SerializeField] private FloatConverter _converter;
        
        [SerializeField] private Comparisons _comparisons;
        [SerializeField] private float _value;

        [SerializeField] private UnityEvent<bool> _casted;

        public void SetValue(int value) =>
            _casted?.Invoke(Compare(ConvertValue(value)));

        public void SetValue(long value) =>
            _casted?.Invoke(Compare(ConvertValue(value)));

        public void SetValue(float value) =>
            _casted?.Invoke(Compare(ConvertValue(value)));

        public void SetValue(double value) =>
            _casted?.Invoke(Compare(ConvertValue((float)value)));
        
        private float ConvertValue(float value) =>
            _isConvert ? _converter.Convert(value) : value;
        
        private bool Compare(float value) => _comparisons switch
        {
            Comparisons.Equal => Mathf.Approximately(value, _value),
            Comparisons.Inequality => !Mathf.Approximately(value, _value),
            Comparisons.LessThan => value < _value,
            Comparisons.GreaterThan => value > _value,
            Comparisons.LessThanOrEqual => value <= _value,
            Comparisons.GreaterThanOrEqual => value >= _value,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}