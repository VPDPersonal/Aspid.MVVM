#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugShortField : DebugIntegerField
    {
        internal DebugShortField(string label, IFieldContext context)
            : base(label, short.MinValue, short.MaxValue, (short)context.GetValue(), context) { }

        protected override object GetValue() =>
            (int)(short)base.GetValue();
    }
}