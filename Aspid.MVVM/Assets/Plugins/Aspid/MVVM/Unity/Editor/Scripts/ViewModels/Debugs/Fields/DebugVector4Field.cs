using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugVector4Field : Vector4Field, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugVector4Field(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            SetValueWithoutNotify((Vector4)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = (Vector4)_context.GetValue();
            if (EqualityComparer<Vector4>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}