using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugVector2Field : Vector2Field, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugVector2Field(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.IsReadonly);
            SetValueWithoutNotify((Vector2)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = (Vector2)_context.GetValue();
            if (EqualityComparer<Vector2>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}