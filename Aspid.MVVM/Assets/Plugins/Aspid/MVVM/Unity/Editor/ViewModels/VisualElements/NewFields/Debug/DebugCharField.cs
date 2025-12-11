#nullable enable
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugCharField : TextField, IUpdatableField
    {
        private readonly IFieldContext _context;
        
        internal DebugCharField(string label, IFieldContext context)
            : base(label)
        {
            maxLength = 1;
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            this.SetValue(context.GetValue().ToString());
            
            this.RegisterValueChangedCallback(e =>
            {
                if (!string.IsNullOrEmpty(e.newValue))
                    context.SetValue(e.newValue[0]);
            });
        }
        
        public void UpdateValue() =>
            SetValueWithoutNotify(_context.GetValue().ToString());
    }
}