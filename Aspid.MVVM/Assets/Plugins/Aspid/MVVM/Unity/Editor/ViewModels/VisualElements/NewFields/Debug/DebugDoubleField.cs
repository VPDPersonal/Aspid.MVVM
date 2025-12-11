#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugDoubleField : VisualElement, IUpdatableField
    {
        private readonly DoubleField? _field;
        private readonly AspidSlider? _slider;
        private readonly IFieldContext _context;
        
        internal DebugDoubleField(string label, IFieldContext context) :
            this(label, double.MinValue, double.MaxValue, (double)context.GetValue(), context) { }
        
        private DebugDoubleField(
            string label, 
            double minValue,
            double maxValue,
            double value, 
            IFieldContext context)
        {
            _context = context;
            
            var min = minValue;
            var max = maxValue;
            
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
                
                _slider = new AspidSlider(label, (float)min, (float)max).SetValue((float)value);
                _slider.RegisterValueChangedCallback(e => context.SetValue((double)e.newValue));
                this.AddChild(_slider);
                return;
            }
            
            _field = new DoubleField(label).SetValue(value);
            _field.RegisterValueChangedCallback(e =>
            {
                if (e.newValue < min) _field.value = min;
                else if (e.newValue > max) _field.value = max;
                else context.SetValue(e.newValue);
            });

            this.AddChild(_field);
            SetEnabled(!context.Member.IsReadonly());
        }
        
        public void UpdateValue()
        {
            var value = (double)_context.GetValue();
            if (_slider is not null) _slider.SetValueWithoutNotify((float)value);
            else _field?.SetValueWithoutNotify(value);
        }
    }
}