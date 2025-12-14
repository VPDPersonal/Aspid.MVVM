using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugRectIntField : RectIntField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugRectIntField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            SetValueWithoutNotify((RectInt)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = (RectInt)_context.GetValue();
            if (EqualityComparer<RectInt>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}