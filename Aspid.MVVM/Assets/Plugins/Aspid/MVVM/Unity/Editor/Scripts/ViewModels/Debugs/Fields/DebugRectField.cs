using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugRectField : RectField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugRectField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            SetValueWithoutNotify((Rect)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = (Rect)_context.GetValue();
            if (EqualityComparer<Rect>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}