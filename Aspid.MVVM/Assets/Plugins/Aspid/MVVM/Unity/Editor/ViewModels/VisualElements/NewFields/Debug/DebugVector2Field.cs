#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugVector2Field : Vector2Field, IUpdatableField
    {
        private readonly IFieldContext _context;
        
        internal DebugVector2Field(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            this.SetValue((Vector2)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue() =>
            SetValueWithoutNotify((Vector2)_context.GetValue());
    }
}