using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugDoubleField : VisualElement, IUpdatableDebugField
    {
        private readonly DoubleField _field;
        private readonly AspidSlider _slider;
        private readonly IFieldContext _context;

        public DebugDoubleField(string label, IFieldContext context)
        {
            _context = context;
            
            var min = double.MinValue;
            var max = double.MaxValue;
            var value = (double)context.GetValue();
            
            SetEnabled(!context.IsReadonly);
            
            if (context.IsDefined(typeof(MinAttribute)))
            {
                var minAttribute = context.GetCustomAttribute<MinAttribute>();
                min = Mathf.Min(Mathf.Max((float)min, minAttribute.min), (float)max);
            }
            if (context.IsDefined(typeof(RangeAttribute)))
            {
                var rangeAttribute = context.GetCustomAttribute<RangeAttribute>();
                max = Mathf.Min((float)max, rangeAttribute.max);
                min = Mathf.Min(Mathf.Max((float)min, rangeAttribute.min), (float)max);
                
                _slider = new AspidSlider(label, (float)min, (float)max);
                _slider.SetValueWithoutNotify((float)value);
                _slider.RegisterValueChangedCallback(e => context.SetValue((double)e.newValue));
                
                Add(_slider);
                return;
            }

            _field = new DoubleField(label);
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
            var newValue = (double)_context.GetValue();

            if (_slider is not null)
            {
                if (EqualityComparer<float>.Default.Equals((float)newValue, _slider.Value)) return;
                _slider.Value = (float)newValue;
            }
            else
            {
                if (EqualityComparer<double>.Default.Equals(newValue, _field.value)) return;
                _field.value = newValue;
            }
        }
    }
}