using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugBoundsIntField : BoundsIntField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugBoundsIntField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.IsReadonly);
            SetValueWithoutNotify((BoundsInt)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = (BoundsInt)_context.GetValue();
            if (EqualityComparer<BoundsInt>.Default.Equals(newValue, value)) return;
            
            value = newValue;
        }
    }
}