#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugUintField : DebugLongField
    {
        internal DebugUintField(string label, IFieldContext context)
            : base(label, uint.MinValue, uint.MaxValue, (uint)context.GetValue(), context) { }

        protected override object GetValue() =>
            (long)(uint)base.GetValue();
    }
}