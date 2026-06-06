// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugUintField : DebugLongField
    {
        public DebugUintField(string label, IFieldContext context)
            : base(label, uint.MinValue, uint.MaxValue, context) { }

        protected override object GetValue() =>
            (long)(uint)base.GetValue();
    }
}