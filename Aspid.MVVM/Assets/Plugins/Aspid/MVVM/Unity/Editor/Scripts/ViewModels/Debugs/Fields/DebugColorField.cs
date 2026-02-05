using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugColorField : ColorField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugColorField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.IsReadonly);
            SetValueWithoutNotify((Color)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = (Color)_context.GetValue();
            if (EqualityComparer<Color>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}