using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugFloatField : VisualElement, IUpdatableDebugField
    {
        private readonly FloatField _field;
        private readonly AspidSlider _slider;
        private readonly IFieldContext _context;
        
        public DebugFloatField(string label, IFieldContext context)
        {
            _context = context;
            
            var min = float.MinValue;
            var max = float.MaxValue;
            var value = (float)context.GetValue();
            
            SetEnabled(!context.Member.IsReadonly());
            
            if (context.IsDefined(typeof(MinAttribute)))
            {
                var minAttribute = context.GetCustomAttribute<MinAttribute>();
                min = Mathf.Min(Mathf.Max(min, minAttribute.min), max);
            }
            if (context.IsDefined(typeof(RangeAttribute)))
            {
                var rangeAttribute = context.GetCustomAttribute<RangeAttribute>();
                max = Mathf.Min(max, rangeAttribute.max);
                min = Mathf.Min(Mathf.Max(min, rangeAttribute.min), max);

                _slider = new AspidSlider(label, min, max);
                _slider.SetValueWithoutNotify(value);
                _slider.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
                
                Add(_slider);
                return;
            }

            _field = new FloatField(label);
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
            var newValue = (float)_context.GetValue();

            if (_slider is not null)
            {
                if (EqualityComparer<float>.Default.Equals(newValue, _slider.Value)) return;
                _slider.Value = newValue;
            }
            else
            {
                if (EqualityComparer<float>.Default.Equals(newValue, _field.value)) return;
                _field.value = newValue;
            }
        }
    }
}