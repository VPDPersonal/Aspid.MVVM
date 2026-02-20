using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugUlongField : DebugDisableTextField, IUpdatableDebugField
    {
        private readonly IFieldContext _context;
        
        internal DebugUlongField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            SetValueWithoutNotify(context.GetValue().ToString());
        }

        public void UpdateValue()
        {
            var newValue = _context.GetValue().ToString();
            if (EqualityComparer<string>.Default.Equals(newValue, value)) return;

            value = newValue;
        }
    }
}