#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugAnimationCurveField : CurveField, IUpdatableField
    {
        private readonly IFieldContext _context;
        
        internal DebugAnimationCurveField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            this.SetValue((AnimationCurve)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue() =>
            SetValueWithoutNotify((AnimationCurve)_context.GetValue());
    }
}