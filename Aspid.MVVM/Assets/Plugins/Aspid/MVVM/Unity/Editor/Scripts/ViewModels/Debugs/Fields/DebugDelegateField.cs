using System;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugDelegateField : VisualElement, IUpdatableDebugField
    {
        private object _value;
        private readonly string _label;
        private readonly IFieldContext _context;
        
        public DebugDelegateField(string label, IFieldContext context)
        {
            _label = label;
            _context = context;

            Build(_label, context.GetValue(), _context);
        }

        public void UpdateValue()
        {
            var newValue = _context.GetValue();
            if (EqualityComparer<object>.Default.Equals(_value, newValue)) return;
            
            Clear();
            Build(_label, _value, _context);
        }

        private void Build(string label, object value, IFieldContext context)
        {
            _value = value;
            
            if (value is null)
            {
                Add(new DebugNullField(label, context.MemberType));
            }
            else
            {
                Add(new AspidDelegateField(label, value as Delegate));
            }
        }
    }
}