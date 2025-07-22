using System;
using UnityEngine.UIElements;

namespace Aspid.MVVM.Unity
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