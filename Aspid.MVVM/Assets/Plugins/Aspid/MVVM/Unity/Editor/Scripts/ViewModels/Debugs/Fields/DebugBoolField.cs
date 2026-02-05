using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugBoolField : AspidToggle, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugBoolField(string label, IFieldContext context)
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.IsReadonly);
            SetValueWithoutNotify((bool)context.GetValue());
            OnValueChanged += e => context.SetValue(e);
        }
        
        public void UpdateValue()
        {
            var newValue = (bool)_context.GetValue();
            if (EqualityComparer<bool>.Default.Equals(newValue, Value)) return;
            
            Value = newValue;
        }
    }
}