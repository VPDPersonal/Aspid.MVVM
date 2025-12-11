#nullable enable
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugBoolField : Toggle, IUpdatableField
    {
        private readonly IFieldContext _context;
        
        internal DebugBoolField(string label, IFieldContext context)
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            this.SetValue((bool)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue() =>
            SetValueWithoutNotify((bool)_context.GetValue());
    }
}