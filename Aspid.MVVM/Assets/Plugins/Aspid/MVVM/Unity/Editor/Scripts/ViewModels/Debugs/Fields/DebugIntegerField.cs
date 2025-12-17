using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class DebugIntegerField : VisualElement, IUpdatableDebugField
    {
        private readonly IntegerField _field;
        private readonly IFieldContext _context;
        private readonly AspidSliderInt _slider;
        
        public DebugIntegerField(string label, IFieldContext context) :
            this(label, int.MinValue, int.MaxValue, context) { }
        
        protected DebugIntegerField(
            string label, 
            int minValue,
            int maxValue,
            IFieldContext context)
        {
            _context = context;
            
            var min = minValue;
            var max = maxValue;
            var value = (int)GetValue();
            
            SetEnabled(!context.IsReadonly);
            
            if (context.IsDefined(typeof(MinAttribute)))
            {
                var minAttribute = context.GetCustomAttribute<MinAttribute>();
                min = (int)Mathf.Min(Mathf.Max(min, minAttribute.min), max);
            }
            if (context.IsDefined(typeof(RangeAttribute)))
            {
                var rangeAttribute = context.GetCustomAttribute<RangeAttribute>();
                max = (int)Mathf.Min(max, rangeAttribute.max);
                min = (int)Mathf.Min(Mathf.Max(min, rangeAttribute.min), max);

                _slider = new AspidSliderInt(label, min, max);
                _slider.SetValueWithoutNotify(value);
                _slider.RegisterValueChangedCallback(e => context.SetValue(e.newValue));

                Add(_slider);
                return;
            }

            _field = new IntegerField(label);
            _field.SetValueWithoutNotify(value);
            _field.RegisterValueChangedCallback(e =>
            {
                if (e.newValue < min) _field.value = min;
                else if (e.newValue > max) _field.value = max;
                else context.SetValue(e.newValue);
            });

            Add(_field);
        }
        
        public void UpdateValue()
        {
            var newValue = (int)GetValue();
            
            if (_slider is not null)
            {
                if (EqualityComparer<int>.Default.Equals(newValue, _slider.Value)) return;
                _slider.Value = newValue;
            }
            else
            {
                if (EqualityComparer<int>.Default.Equals(newValue, _field.value)) return;
                _field.value = newValue;
            }
        }
        
        protected virtual object GetValue() =>
            _context.GetValue();
    }
}