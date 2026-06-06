using System;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugTypeField : VisualElement, IUpdatableDebugField
    {
        private Type _value;
        private readonly string _label;
        private readonly IFieldContext _context;
        
        public DebugTypeField(string label, IFieldContext context)
        {
            _label = label;
            _context = context;
            _value = _context.GetValue() as Type;
            
            Build(label, _value, context);
        }

        public void UpdateValue()
        {
            var newValue = _context.GetValue() as Type;
            if (EqualityComparer<Type>.Default.Equals(newValue, _value)) return;
            
            Clear();
            _value = newValue;
            Build(_label, newValue, _context);
        }

        private void Build(string label, Type value, IFieldContext context)
        {
            var type = context.MemberType;
            
            if (value is null)
            {
                Add(new DebugNullField(label, type));
            }
            else
            {
                var namespaceName = !string.IsNullOrWhiteSpace(value.Namespace)
                    ? $"{value.Namespace}."
                    : string.Empty;
                
                var textField = new DebugDisableTextField(label);
                textField.SetValueWithoutNotify(namespaceName + value.Name);
                Add(textField);
            }
        }
    }
}