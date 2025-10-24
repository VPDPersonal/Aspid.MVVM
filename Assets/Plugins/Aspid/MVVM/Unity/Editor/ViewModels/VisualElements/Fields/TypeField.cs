using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class TypeField : TextField
    {
        public TypeField(Type type, string label)
        {
            this.label = label;
            value = type is null 
                ? "null" 
                : $"{type.Namespace?.TrimEnd('.')}{type.Name}";

            SetEnabled(false);
        }
    }
}