using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class DebugLongField : VisualElement, IUpdatableDebugField
    {
        private readonly LongField _field;
        private readonly IFieldContext _context;
        private readonly AspidSliderInt _slider;
        
        public DebugLongField(string label, IFieldContext context) :
            this(label, long.MinValue, long.MaxValue, context) { }
        
        protected DebugLongField(
            string label, 
            long minValue,
            long maxValue,
            IFieldContext context)
        {
            _context = context;
            
            var min = minValue;
            var max = maxValue;
            var value = (int)GetValue();
            
            SetEnabled(!context.Member.IsReadonly());
            
            if (context.IsDefined(typeof(MinAttribute)))
            {
                var minAttribute = context.GetCustomAttribute<MinAttribute>();
                min = (long)Mathf.Min(Mathf.Max(min, minAttribute.min), max);
            }
            if (context.IsDefined(typeof(RangeAttribute)))
            {
                var rangeAttribute = context.GetCustomAttribute<RangeAttribute>();
                max = (int)Mathf.Min(max, rangeAttribute.max);
                min = (int)Mathf.Min(Mathf.Max(min, rangeAttribute.min), max);

                _slider = new AspidSliderInt(label, (int)min, (int)max);
                _slider.SetValueWithoutNotify(value);
                _slider.RegisterValueChangedCallback(e => context.SetValue((long)e.newValue));
                
                Add(_slider);
                return;
            }

            _field = new LongField(label);
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
            var newValue = (long)GetValue();

            if (_slider is not null)
            {
                if (EqualityComparer<long>.Default.Equals(newValue, _slider.Value)) return;
                _slider.Value = (int)newValue;
            }
            else
            {
                if (EqualityComparer<long>.Default.Equals(newValue, _field.value)) return;
                _field.value = newValue;
            }
        }
        
        protected virtual object GetValue() =>
            _context.GetValue();
    }
}