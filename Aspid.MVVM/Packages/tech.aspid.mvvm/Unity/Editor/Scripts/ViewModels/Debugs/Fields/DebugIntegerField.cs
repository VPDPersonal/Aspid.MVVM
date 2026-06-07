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
            
            double min = minValue;
            double max = maxValue;
            var value = (int)GetValue();
            
            SetEnabled(!context.IsReadonly);

            if (NumberRestrictions.CalculateMinAndMax(context, ref min, ref max))
            {
                _slider = new AspidSliderInt(label, (int)min, (int)max);
                _slider.SetValueWithoutNotify(value);
                _slider.RegisterValueChangedCallback(e => context.SetValue(e.newValue));

                Add(_slider);
            }
            else
            {
                _field = new IntegerField(label);
                _field.SetValueWithoutNotify(value);
                _field.RegisterValueChangedCallback(e =>
                {
                    if (e.newValue < min) _field.value = (int)min;
                    else if (e.newValue > max) _field.value = (int)max;
                    else context.SetValue(e.newValue);
                });

                Add(_field);
            }
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