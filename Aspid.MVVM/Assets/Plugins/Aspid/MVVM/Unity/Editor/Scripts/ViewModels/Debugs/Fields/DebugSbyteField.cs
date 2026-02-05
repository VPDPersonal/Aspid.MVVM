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
            
            double min = sbyte.MinValue;
            double max = sbyte.MaxValue;
            NumberRestrictions.CalculateMinAndMax(context, ref min, ref max);
            
            _slider = new AspidSliderInt(label, (sbyte)min, (sbyte)max);
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