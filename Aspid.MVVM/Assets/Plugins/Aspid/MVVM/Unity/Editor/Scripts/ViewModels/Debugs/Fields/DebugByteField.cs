using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugByteField : VisualElement, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        private readonly AspidSliderInt _slider;
        
        public DebugByteField(string label, IFieldContext context)
        {
            _context = context;
            
            var min = byte.MinValue;
            var max = byte.MaxValue;
            
            if (context.IsDefined(typeof(MinAttribute)))
            {
                var minAttribute = context.GetCustomAttribute<MinAttribute>();
                min = (byte)Mathf.Min(Mathf.Max(min, minAttribute.min), max);
            }
            if (context.IsDefined(typeof(RangeAttribute)))
            {
                var rangeAttribute = context.GetCustomAttribute<RangeAttribute>();
                max = (byte)Mathf.Min(max, rangeAttribute.max);
                min = (byte)Mathf.Min(Mathf.Max(min, rangeAttribute.min), max);
            }
            
            _slider = new AspidSliderInt(label, min, max);
            _slider.SetValueWithoutNotify((byte)context.GetValue());
            _slider.RegisterValueChangedCallback(e => context.SetValue((byte)e.newValue));
            
            Add(_slider);
        }
        
        public void UpdateValue()
        {
            var newValue = (byte)_context.GetValue();
            if (EqualityComparer<byte>.Default.Equals(newValue, (byte)_slider.Value)) return;
            
            _slider.Value = newValue;
        }
    }
}