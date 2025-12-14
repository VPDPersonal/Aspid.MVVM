#nullable enable
using System.Reflection;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugCompositeField : VisualElement, IUpdatableDebugField
    {
        private object? _value;
        private readonly string _label;
        private readonly IFieldContext _context;
        private readonly List<IUpdatableDebugField> _updatableFields;
        
        internal DebugCompositeField(string label, IFieldContext context)
        {
            _label = label;
            _context = context;
            _updatableFields = new List<IUpdatableDebugField>();
            
            Build(label, context);
        }

        public void UpdateValue()
        {
            if (_value?.Equals(_context.GetValue()) is false)
            {
                Clear();
                _updatableFields.Clear();
                
                Build(_label, _context);   
            }
            else
            {
                foreach (var field in _updatableFields)
                {
                    field.UpdateValue();
                }
            }
        }

        private void Build(string label, IFieldContext context)
        {
            _value = context.GetValue();

            if (_value is null)
            {
                this.AddChild(new DebugNullField(label, context.MemberType));
                return;
            }
            
            var foldout = new Foldout()
                .SetValue(false)
                .SetText(label)
                .SetMargin(left: 12);

            foldout.Q("unity-content").SetMargin(left: 0);
            
            var content = new VisualElement();
            foldout.AddChild(content);
            
            foldout.RegisterValueChangedCallback(e =>
            {
                if (e.target != foldout) return;
                    
                if (e.newValue)
                {
                    foreach (var fieldInfo in _value.GetType().GetFieldInfosIncludingBaseClasses(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
                    {
                        var field = new DebugField(_value, fieldInfo, !context.IsAlternativeColor);
                        _updatableFields.Add(field);
                        
                        content.AddChild(field);
                    }
                }
                else
                {
                    content.Clear();
                    _updatableFields.Clear();
                }
            });
            
            this.AddChild(foldout);
        }
    }
}