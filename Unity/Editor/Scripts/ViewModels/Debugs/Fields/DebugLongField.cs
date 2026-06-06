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
            
            double min = minValue;
            double max = maxValue;
            var value = (long)GetValue();
            
            SetEnabled(!context.IsReadonly);

            if (NumberRestrictions.CalculateMinAndMax(context, ref min, ref max))
            {
                _slider = new AspidSliderInt(label, (int)min, (int)max);
                _slider.SetValueWithoutNotify((int)value);
                _slider.RegisterValueChangedCallback(e => context.SetValue((long)e.newValue));
                
                Add(_slider);
            }
            else
            {
                _field = new LongField(label);
                _field.SetValueWithoutNotify(value);
                _field.RegisterValueChangedCallback(e =>
                {
                    if (e.newValue < min) _field.value = (long)min;
                    else if (e.newValue > max) _field.value = (long)max;
                    else context.SetValue(e.newValue);
                });

                Add(_field);
            }
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