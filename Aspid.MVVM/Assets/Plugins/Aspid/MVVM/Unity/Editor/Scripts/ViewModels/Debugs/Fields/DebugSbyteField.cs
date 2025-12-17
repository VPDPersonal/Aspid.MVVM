using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugSbyteField : VisualElement, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        private readonly AspidSliderInt _slider;
        
        public DebugSbyteField(string label, IFieldContext context)
        {
            _context = context;
            
            var min = sbyte.MinValue;
            var max = sbyte.MaxValue;
            
            if (context.IsDefined(typeof(MinAttribute)))
            {
                var minAttribute = context.GetCustomAttribute<MinAttribute>();
                min = (sbyte)Mathf.Min(Mathf.Max(min, minAttribute.min), max);
            }
            if (context.IsDefined(typeof(RangeAttribute)))
            {
                var rangeAttribute = context.GetCustomAttribute<RangeAttribute>();
                max = (sbyte)Mathf.Min(max, rangeAttribute.max);
                min = (sbyte)Mathf.Min(Mathf.Max(min, rangeAttribute.min), max);
            }
            
            _slider = new AspidSliderInt(label, min, max);
            _slider.SetValueWithoutNotify((sbyte)context.GetValue());
            _slider.RegisterValueChangedCallback(e => context.SetValue((sbyte)e.newValue));
         
            Add(_slider);
        }
        
        public void UpdateValue()
        {
            var newValue = (sbyte)_context.GetValue();
            if (EqualityComparer<sbyte>.Default.Equals(newValue, (sbyte)_slider.Value)) return;
            
            _slider.Value = newValue;
        }
    }
}