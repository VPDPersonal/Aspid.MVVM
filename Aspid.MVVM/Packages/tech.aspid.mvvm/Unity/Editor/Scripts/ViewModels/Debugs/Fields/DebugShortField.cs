// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugShortField : DebugIntegerField
    {
        public DebugShortField(string label, IFieldContext context)
            : base(label, short.MinValue, short.MaxValue, context) { }

        protected override object GetValue() =>
            (int)(short)base.GetValue();
    }
}