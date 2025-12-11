#nullable enable
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugStringField : VisualElement, IUpdatableField
    {
        private TextField? _textField;
        private readonly string _label;
        private readonly IFieldContext _context;
        
        internal DebugStringField(string label, IFieldContext context)
        {
            _label = label;
            _context = context;
            
            SetEnabled(!context.Member.IsReadonly());
            Build(label, context);
        }
        
        public void UpdateValue()
        {
            if (_textField is not null)
            {
                _textField.SetValueWithoutNotify(_context.GetValue()?.ToString() ?? string.Empty);
            }
            else
            {
                Clear();
                Build(_label, _context);
            }
        }

        private void Build(string label, IFieldContext context)
        {
            var value = context.GetValue();
            
            if (!context.Member.IsReadonly())
            {
                _textField = new TextField(label).SetValue(value?.ToString() ?? string.Empty);
                _textField.RegisterValueChangedCallback(e => context.SetValue(e.newValue));
                this.AddChild(_textField);
            }
            else if (value is not null) this.AddChild(new DebugDisableTextField(label).SetValue(value.ToString()));
            else this.AddChild(new DebugNullField(label, typeof(string)));
        }
    }
}