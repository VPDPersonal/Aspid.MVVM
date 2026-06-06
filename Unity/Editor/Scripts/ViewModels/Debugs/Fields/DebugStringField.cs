using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugStringField : VisualElement, IUpdatableDebugField
    {
        private string _value;
        private TextField _textField;
        
        private readonly string _label;
        private readonly bool _isReadonly;
        private readonly IFieldContext _context;

        public string Value => _textField is null
            ? _value
            : _textField.value;

        public DebugStringField(string label, IFieldContext context)
        {
            _label = label;
            _context = context;
            _isReadonly = context.IsReadonly;
            
            Build(label, context.GetValue()?.ToString(), context);
        }
        
        public void UpdateValue()
        {
            var newValue = _context.GetValue()?.ToString();
            if (EqualityComparer<string>.Default.Equals(Value, newValue)) return;
            if (string.IsNullOrWhiteSpace(newValue) && string.IsNullOrWhiteSpace(_value)) return;
            
            Clear();
            Build(_label, newValue, _context);
        }

        private void Build(string label, string value, IFieldContext context)
        {
            _value = value;
            
            if (!_isReadonly)
            {
                _textField = new TextField(label);
                _textField.SetValueWithoutNotify(_value ?? string.Empty);
                _textField.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
                
                Add(_textField);
            }
            else if (_value is not null)
            {
                _textField = new DebugDisableTextField(label);
                _textField.SetValueWithoutNotify(_value);
                
                Add(_textField);
            }
            else
            {
                _textField = null;
                Add(new DebugNullField(label, typeof(string)) { isReadOnly = _isReadonly});
            }
        }
    }
}