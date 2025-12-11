#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugVector3Field : Vector3Field, IUpdatableField
    {
        private readonly IFieldContext _context;
        
        internal DebugVector3Field(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            this.SetValue((Vector3)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue() =>
            SetValueWithoutNotify((Vector3)_context.GetValue());
    }
}