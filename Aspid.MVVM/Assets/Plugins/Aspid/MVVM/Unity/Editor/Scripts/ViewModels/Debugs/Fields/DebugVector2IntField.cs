using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugVector2IntField : Vector2IntField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        internal DebugVector2IntField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.IsReadonly);
            SetValueWithoutNotify((Vector2Int)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }

        public void UpdateValue()
        {
            var newValue = (Vector2Int)_context.GetValue();
            if (EqualityComparer<Vector2Int>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}