#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugByteField : VisualElement, IUpdatableField
    {
        private readonly IFieldContext _context;
        private readonly AspidSliderInt _slider;
        
        internal DebugByteField(string label, IFieldContext context)
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
            
            _slider = new AspidSliderInt(label, min, max).SetValue((byte)context.GetValue());
            _slider.RegisterValueChangedCallback(e => context.SetValue((byte)e.newValue));
            this.AddChild(_slider);
        }
        
        public void UpdateValue() =>
            _slider.SetValueWithoutNotify((byte)_context.GetValue());
    }
}