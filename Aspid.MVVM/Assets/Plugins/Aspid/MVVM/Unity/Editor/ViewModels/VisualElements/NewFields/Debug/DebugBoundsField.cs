#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugBoundsField : BoundsField, IUpdatableField
    {
        private readonly IFieldContext _context;
        
        internal DebugBoundsField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            this.SetValue((Bounds)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue() =>
            SetValueWithoutNotify((Bounds)_context.GetValue());
    }
}