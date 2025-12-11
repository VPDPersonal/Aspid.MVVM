#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class DebugLongField : VisualElement, IUpdatableField
    {
        private readonly LongField? _field;
        private readonly IFieldContext _context;
        private readonly AspidSliderInt? _slider;
        
        internal DebugLongField(string label, IFieldContext context) :
            this(label, long.MinValue, long.MaxValue, (long)context.GetValue(), context) { }
        
        protected DebugLongField(
            string label, 
            long minValue,
            long maxValue,
            long value, 
            IFieldContext context)
        {
            _context = context;
            
            var min = minValue;
            var max = maxValue;
            
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
                
                _slider = new AspidSliderInt(label, (int)min, (int)max).SetValue((int)value);
                _slider.RegisterValueChangedCallback(e => context.SetValue((long)e.newValue));
                this.AddChild(_slider);
                return;
            }
            
            _field = new LongField(label).SetValue(value);
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
            var value = (long)GetValue();
            if (_slider is not null) _slider.SetValueWithoutNotify((int)value);
            else _field?.SetValueWithoutNotify(value);
        }
        
        protected virtual object GetValue() =>
            _context.GetValue();
    }
}