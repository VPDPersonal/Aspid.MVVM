#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class DebugIntegerField : VisualElement, IUpdatableField
    {
        private readonly IntegerField? _field;
        private readonly IFieldContext _context;
        private readonly AspidSliderInt? _slider;
        
        internal DebugIntegerField(string label, IFieldContext context) :
            this(label, int.MinValue, int.MaxValue, (int)context.GetValue(), context) { }
        
        protected DebugIntegerField(
            string label, 
            int minValue,
            int maxValue,
            int value, 
            IFieldContext context)
        {
            _context = context;
            
            var min = minValue;
            var max = maxValue;
            
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
                
                _slider = new AspidSliderInt(label, min, max).SetValue(value);
                _slider.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
                this.AddChild(_slider);
                
                return;
            }
            
            _field = new IntegerField(label).SetValue(value);
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
            var value = (int)GetValue();
            if (_slider is not null) _slider.SetValueWithoutNotify(value);
            else _field?.SetValueWithoutNotify(value);
        }
        
        protected virtual object GetValue() =>
            _context.GetValue();
    }
}