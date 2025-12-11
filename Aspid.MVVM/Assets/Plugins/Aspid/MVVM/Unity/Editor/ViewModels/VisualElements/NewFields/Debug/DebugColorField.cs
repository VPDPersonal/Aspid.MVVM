#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugColorField : ColorField, IUpdatableField
    {
        private readonly IFieldContext _context;
        
        internal DebugColorField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            this.SetValue((Color)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue() =>
            SetValueWithoutNotify((Color)_context.GetValue());
    }
}