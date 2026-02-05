using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugRelayCommandField : RelayCommandField
    {
        public DebugRelayCommandField(string label, IFieldContext context)
            : base(label, context.Target, (FieldInfo)context.Member) { }
    }
}