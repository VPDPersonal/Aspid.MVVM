using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugCharField : TextField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugCharField(string label, IFieldContext context)
            : base(label)
        {
            maxLength = 1;
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            SetValueWithoutNotify(context.GetValue().ToString());
            
            this.RegisterValueChangedCallback(e =>
            {
                if (!string.IsNullOrEmpty(e.newValue))
                    context.SetValue(e.newValue[0]);
            });
        }
        
        public void UpdateValue()
        {
            var newValue = (char)_context.GetValue();
            if (EqualityComparer<char>.Default.Equals(newValue, value[0])) return;
            
            value = newValue.ToString();
        }
    }
}