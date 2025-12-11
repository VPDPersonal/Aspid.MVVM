#nullable enable
using System;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugEnumField : VisualElement, IUpdatableField
    {
        private readonly EnumField? _enumField;
        private readonly IFieldContext _context;
        private readonly EnumFlagsField? _flagsField;
        
        internal DebugEnumField(string label, IFieldContext context)
        {
            _context = context;
            
            var type = context.MemberType;
            var value = context.GetValue();
            
            if (value is not Enum valueEnum)
            {
                this.AddChild(new DebugNullField(label, type));
                return;
            }
                
            if (type.IsDefined(typeof(FlagsAttribute), inherit: false))
            {
                _flagsField = new EnumFlagsField(label, valueEnum);
                _flagsField.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
                this.AddChild(_flagsField);
            }
            else
            {
                _enumField = new EnumField(label, valueEnum);
                _enumField.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
                this.AddChild(_enumField);
            }

            SetEnabled(!context.Member.IsReadonly());
        }
        
        public void UpdateValue()
        {
            if (_context.GetValue() is not Enum value) return;
            
            _enumField?.SetValueWithoutNotify(value);
            _flagsField?.SetValueWithoutNotify(value);
        }
    }
}