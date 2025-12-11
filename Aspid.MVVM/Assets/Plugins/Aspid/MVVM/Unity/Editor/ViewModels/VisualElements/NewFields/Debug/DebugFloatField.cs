#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugFloatField : VisualElement, IUpdatableField
    {
        private readonly FloatField? _field;
        private readonly AspidSlider? _slider;
        private readonly IFieldContext _context;
        
        internal DebugFloatField(string label, IFieldContext context) :
            this(label, float.MinValue, float.MaxValue, (float)context.GetValue(), context) { }
        
        private DebugFloatField(
            string label, 
            float minValue,
            float maxValue,
            float value, 
            IFieldContext context)
        {
            _context = context;
            
            var min = minValue;
            var max = maxValue;
            
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
                
                _slider = new AspidSlider(label, min, max).SetValue(value);
                _slider.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
                this.AddChild(_slider);
                return;
            }
            
            _field = new FloatField(label).SetValue(value);
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
            var value = (float)_context.GetValue();
            if (_slider is not null) _slider.SetValueWithoutNotify(value);
            else _field?.SetValueWithoutNotify(value);
        }
    }
}