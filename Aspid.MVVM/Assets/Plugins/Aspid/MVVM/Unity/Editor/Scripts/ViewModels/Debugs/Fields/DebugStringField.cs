using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugStringField : VisualElement, IUpdatableDebugField
    {
        private string _value;
        private readonly string _label;
        private readonly bool _isReadonly;
        private readonly IFieldContext _context;
        
        public DebugStringField(string label, IFieldContext context)
        {
            _label = label;
            _context = context;
            _isReadonly = !context.Member.IsReadonly();
            
            SetEnabled(_isReadonly);
            Build(label, context.GetValue()?.ToString(), context);
        }
        
        public void UpdateValue()
        {
            var newValue = _context.GetValue()?.ToString();
            if (EqualityComparer<string>.Default.Equals(_value, newValue)) return;
            
            Clear();
            Build(_label, newValue, _context);
        }

        private void Build(string label, string value, IFieldContext context)
        {
            _value = value;
            
            if (!_isReadonly)
            {
                var textField = new TextField(label);
                textField.SetValueWithoutNotify(_value ?? string.Empty);
                textField.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
                
                Add(textField);
            }
            else if (_value is not null)
            {
                var field = new DebugDisableTextField(label);
                field.SetValueWithoutNotify(_value);
                
                Add(field);
            }
            else Add(new DebugNullField(label, typeof(string)));
        }
    }
}