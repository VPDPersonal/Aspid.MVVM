using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugUnityObjectField : ObjectField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        public DebugUnityObjectField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            objectType = context.MemberType;
            
            SetEnabled(!context.Member.IsReadonly());
            SetValueWithoutNotify((Object)context.GetValue());
            this.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
        }
        
        public void UpdateValue()
        {
            var newValue = _context.GetValue() as Object;
            if (EqualityComparer<Object>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}