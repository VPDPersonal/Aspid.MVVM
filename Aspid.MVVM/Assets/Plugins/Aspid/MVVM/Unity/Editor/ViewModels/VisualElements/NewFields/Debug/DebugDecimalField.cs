#nullable enable
using Aspid.UnityFastTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugDecimalField : DebugDisableTextField, IUpdatableField
    {
        private readonly IFieldContext _context;
        
        internal DebugDecimalField(string label, IFieldContext context)
            : base(label)
        {
            UpdateValue();
        }

        public void UpdateValue() =>
            this.SetValue(_context.GetValue().ToString());
    }
}