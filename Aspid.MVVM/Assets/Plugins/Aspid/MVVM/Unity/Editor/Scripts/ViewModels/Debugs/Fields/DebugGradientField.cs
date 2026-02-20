using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugGradientField : GradientField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugGradientField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.IsReadonly);
            SetValueWithoutNotify(context.GetValue() as Gradient);
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = _context.GetValue() as Gradient;
            if (EqualityComparer<Gradient>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}