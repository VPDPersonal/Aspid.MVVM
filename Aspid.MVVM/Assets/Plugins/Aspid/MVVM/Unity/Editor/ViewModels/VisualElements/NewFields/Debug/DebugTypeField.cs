#nullable enable
using System;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugTypeField : VisualElement, IUpdatableField
    {
        private readonly string _label;
        private readonly IFieldContext _context;
        
        internal DebugTypeField(string label, IFieldContext context)
        {
            _label = label;
            _context = context;
            Build(label, context);
        }

        public void UpdateValue()
        {
            Clear();
            Build(_label, _context);
        }

        private void Build(string label, IFieldContext context)
        {
            var type = context.MemberType;
            var value = context.GetValue();
            
            if (value is null)
            {
                this.AddChild(new DebugNullField(label, type));
            }
            else
            {
                var namespaceName = !string.IsNullOrWhiteSpace(type.Namespace)
                    ? $"{type.Namespace}."
                    : string.Empty;
                
                this.AddChild(new DebugDisableTextField(label).SetValue(namespaceName + ((Type)value).Name));
            }
        }
    }
}