using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugNullField : DebugDisableTextField
    {
        public DebugNullField(string label, Type type)
            : base(label)
        {
            var namespaceName = !string.IsNullOrWhiteSpace(type.Namespace)
                ? $"{type.Namespace}."
                : string.Empty;
                
            value = $"Null ({namespaceName}{type.Name})";
        }
    }
}