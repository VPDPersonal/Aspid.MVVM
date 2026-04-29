using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    public class InlineStyle<T>
    {
        private readonly Action<T, T> _onSet;
        
        public T Value { get; private set; }
        
        public bool IsInline { get; private set; }
        
        public InlineStyle(T value, Action<T, T> onSet = null)
        {
            Value = value;
            _onSet = onSet;
            IsInline = false;

            onSet?.Invoke(default, Value);
        }

        public void SetInlineValue(T value)
        {
            _onSet?.Invoke(Value, value);

            Value = value;
            IsInline = true;
        }

        public void SetDefaultValue(T value)
        {
            if (IsInline)  return;
            
            _onSet?.Invoke(Value, value);
            Value = value;
        }

        public static implicit operator T(InlineStyle<T> inlineStyle) => inlineStyle.Value;
    }
}
