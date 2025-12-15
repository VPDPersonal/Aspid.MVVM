using UnityEngine;
using System.Reflection;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugCompositeField : VisualElement, IUpdatableDebugField
    {
        private const string StyleSheetPath = "Styles/Debug/Fields/aspid-debug-composite-field";
        
        private object _value;
        private bool _isExpanded;
        
        private readonly string _label;
        private readonly IFieldContext _context;
        private readonly List<IUpdatableDebugField> _updatableFields;
        
        public DebugCompositeField(string label, IFieldContext context)
        {
            _label = label;
            _context = context;
            _updatableFields = new List<IUpdatableDebugField>();
            styleSheets.Add(styleSheet: Resources.Load<StyleSheet>(StyleSheetPath));
            
            Build(label, context.GetValue(), context);
        }

        public void UpdateValue()
        {
            var newValue = _context.GetValue();
            
            if (!EqualityComparer<object>.Default.Equals(newValue, _value))
            {
                Clear();
                _updatableFields.Clear();
                
                Build(_label, newValue, _context);   
            }
            else
            {
                foreach (var field in _updatableFields)
                {
                    field.UpdateValue();
                }
            }
        }

        private void Build(string label, object value, IFieldContext context)
        {
            _value = value;

            if (value is null)
            {
                this.AddChild(new DebugNullField(label, context.MemberType));
                return;
            }

            var foldout = new Foldout()
                .SetValue(_isExpanded)
                .SetText(label);
            
            var content = new VisualElement();
            foldout.AddChild(content);
            
            if (_isExpanded) 
                BuildContent(content);
            
            foldout.RegisterValueChangedCallback(e =>
            {
                if (e.target != foldout) return;
                    
                if (e.newValue)
                {
                    BuildContent(content);
                }
                else
                {
                    content.Clear();
                    _updatableFields.Clear();
                }
                
                _isExpanded = e.newValue;
            });
            
            this.AddChild(foldout);
        }

        private void BuildContent(VisualElement content)
        {
            var type = _value.GetType();
            var fields = type.GetFieldInfosIncludingBaseClasses(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            
            foreach (var fieldInfo in fields)
            {
                var field = new DebugField(_value, fieldInfo, !_context.IsAlternativeColor);
                _updatableFields.Add(field);
                        
                content.AddChild(field);
            }
        }
    }
}