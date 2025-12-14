using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugVector3Field : Vector3Field, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugVector3Field(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            SetValueWithoutNotify((Vector3)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = (Vector3)_context.GetValue();
            if (EqualityComparer<Vector3>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}