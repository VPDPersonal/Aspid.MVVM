#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugUshortField : DebugIntegerField
    {
        internal DebugUshortField(string label, IFieldContext context)
            : base(label, ushort.MinValue, ushort.MaxValue, (ushort)context.GetValue(), context) { }
        
        protected override object GetValue() =>
            (int)(ushort)base.GetValue();
    }
}