using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugVector3IntField : Vector3IntField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugVector3IntField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.IsReadonly);
            SetValueWithoutNotify((Vector3Int)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = (Vector3Int)_context.GetValue();
            if (EqualityComparer<Vector3Int>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}