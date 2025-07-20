using System;
using Aspid.CustomEditors;
using UnityEngine.UIElements;

namespace Aspid.MVVM.Unity
{
    internal sealed class TypeField : VisualElement
    {
        public TypeField(Type value, string label)
        {
            if (value is null)
            {
                this.AddChild(new NullField(label));
                return;
            }
            
            var namespaceName = !string.IsNullOrEmpty(value.Namespace) ? $"{value.Namespace}." : string.Empty;
            
            var field = new TextField(label)
                .SetValue($"{namespaceName}{value.Name}");
            field.SetEnabled(false);
            
            this.AddChild(field);
        }
    }
}