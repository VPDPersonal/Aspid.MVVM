using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugEnumField : VisualElement, IUpdatableDebugField
    {
        private readonly EnumField _enumField;
        private readonly IFieldContext _context;
        private readonly EnumFlagsField _flagsField;
        
        public DebugEnumField(string label, IFieldContext context)
        {
            _context = context;
            
            var type = context.MemberType;
            var value = context.GetValue() as Enum;
            
            if (type.IsDefined(typeof(FlagsAttribute), inherit: false))
            {
                _flagsField = new EnumFlagsField(label, value);
                _flagsField.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
                _flagsField.SetValueWithoutNotify(value);
                
                Add(_flagsField);
            }
            else
            {
                _enumField = new EnumField(label, value);
                _enumField.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
                _enumField.SetValueWithoutNotify(value);
                
                Add(_enumField);
            }

            SetEnabled(!context.IsReadonly);
        }
        
        public void UpdateValue()
        {
            if (_context.GetValue() is not Enum newValue) return;
            
            if (_enumField is not null)
            {
                if (EqualityComparer<Enum>.Default.Equals(newValue, _enumField.value)) return;
                _enumField?.SetValueWithoutNotify(newValue);
            }
            else
            {
                if (EqualityComparer<Enum>.Default.Equals(newValue, _flagsField.value)) return;
                _flagsField?.SetValueWithoutNotify(newValue);
            }
        }
    }
}