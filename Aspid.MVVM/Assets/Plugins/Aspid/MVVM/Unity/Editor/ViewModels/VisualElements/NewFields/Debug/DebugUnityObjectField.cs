#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugUnityObjectField : ObjectField, IUpdatableField
    {
        private readonly IFieldContext _context;
        
        internal DebugUnityObjectField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            objectType = context.MemberType;
            
            SetEnabled(!context.Member.IsReadonly());
            this.SetValue((Object)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue() =>
            SetValueWithoutNotify((Object)_context.GetValue());
    }
}