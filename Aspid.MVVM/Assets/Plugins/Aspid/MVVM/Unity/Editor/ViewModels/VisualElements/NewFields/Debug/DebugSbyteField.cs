#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugSbyteField : VisualElement, IUpdatableField
    {
        private readonly IFieldContext _context;
        private readonly AspidSliderInt _slider;
        
        internal DebugSbyteField(string label, IFieldContext context)
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
            
            _slider = new AspidSliderInt(label, min, max).SetValue((sbyte)context.GetValue());
            _slider.RegisterValueChangedCallback(e => context.SetValue((sbyte)e.newValue));
            this.AddChild(_slider);
        }
        
        public void UpdateValue() =>
            _slider.SetValueWithoutNotify((sbyte)_context.GetValue());
    }
}