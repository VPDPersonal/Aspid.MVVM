#nullable enable
using System;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugDelegateField : VisualElement
    {
        internal DebugDelegateField(string label, IFieldContext context)
        {
            var value = context.GetValue();

            if (value is null)
            {
                this.AddChild(new DebugNullField(label, context.MemberType));
            }
            else
            {
                this.AddChild(new AspidDelegateField(label, value as Delegate));
            }
        }
    }
}