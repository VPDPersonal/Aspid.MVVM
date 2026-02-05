using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugAnimationCurveField : CurveField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugAnimationCurveField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.IsReadonly);
            SetValueWithoutNotify(context.GetValue() as AnimationCurve);
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = _context.GetValue() as AnimationCurve;
            if (EqualityComparer<AnimationCurve>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}