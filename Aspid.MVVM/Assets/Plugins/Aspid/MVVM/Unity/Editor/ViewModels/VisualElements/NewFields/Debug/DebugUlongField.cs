#nullable enable
using Aspid.UnityFastTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugUlongField : DebugDisableTextField, IUpdatableField
    {
        private readonly IFieldContext _context;
        
        internal DebugUlongField(string label, IFieldContext context) 
            : base(label)
        {
            _context = context;
            
            SetEnabled(false);
            this.SetValue(context.GetValue().ToString());
        }

        public void UpdateValue() =>
            SetValueWithoutNotify(_context.GetValue().ToString());
    }
}