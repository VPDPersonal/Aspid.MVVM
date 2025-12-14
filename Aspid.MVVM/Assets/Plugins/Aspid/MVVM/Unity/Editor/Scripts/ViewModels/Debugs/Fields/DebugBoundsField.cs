using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugBoundsField : BoundsField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugBoundsField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            SetValueWithoutNotify((Bounds)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = (Bounds)_context.GetValue();
            if (EqualityComparer<Bounds>.Default.Equals(newValue, value)) return;
            
            value = newValue;
        }
    }
}