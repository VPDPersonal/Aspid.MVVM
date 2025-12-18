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
            
            double min = byte.MinValue;
            double max = byte.MaxValue;

            NumberRestrictions.CalculateMinAndMax(context, ref min, ref max);
            
            _slider = new AspidSliderInt(label, (byte)min, (byte)max);
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