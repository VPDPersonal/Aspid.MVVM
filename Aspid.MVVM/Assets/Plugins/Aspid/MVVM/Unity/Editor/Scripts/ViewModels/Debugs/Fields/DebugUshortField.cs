// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugUshortField : DebugIntegerField
    {
        public DebugUshortField(string label, IFieldContext context)
            : base(label, ushort.MinValue, ushort.MaxValue, context) { }
        
        protected override object GetValue() =>
            (int)(ushort)base.GetValue();
    }
}