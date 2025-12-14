using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugBoolField : Toggle, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugBoolField(string label, IFieldContext context)
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            SetValueWithoutNotify((bool)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = (bool)_context.GetValue();
            if (EqualityComparer<bool>.Default.Equals(newValue, value)) return;
            
            value = newValue;
        }
    }
}