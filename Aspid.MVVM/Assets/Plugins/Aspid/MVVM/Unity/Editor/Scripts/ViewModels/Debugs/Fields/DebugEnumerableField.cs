using UnityEngine;
using System.Collections;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugEnumerableField : VisualElement, IUpdatableDebugField
    {
        private const string StyleSheetPath = "Styles/Debug/Fields/aspid-debug-enumerable-field";
        
        private object _value;
        private bool _isExpanded;
        
        private readonly string _label;
        private readonly IFieldContext _context;
        private readonly List<IUpdatableDebugField> _updatableFields;
        
        public DebugEnumerableField(string label, IFieldContext context)
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
            
            if (_value is not IEnumerable)
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
            });
            
            this.AddChild(foldout);
        }

        private void BuildContent(VisualElement content)
        {
            var index = 0;
            var enumerable = (IEnumerable)_context.GetValue();
            
            // IEnumerable<T> or IEnumerable<KeyValuePair<TKey, TValue>>
            var itemType =  enumerable
                .GetType()
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                ?.GetGenericArguments()[0] ?? throw new System.Exception("Can't get enumerable item type");
                        
            foreach (var item in enumerable)
            {
                string itemLabel;
                IFieldContext childContext;

                if (itemType.IsGenericType && itemType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                {
                    var key = itemType.GetProperty(name: "Key")?.GetValue(item);

                    // Key can't be null
                    itemLabel = key?.ToString() ?? index.ToString();
                    childContext = new DictionaryElementContext(_context, key);
                }
                else
                {
                    itemLabel = index.ToString();
                    childContext = new CollectionElementContext(_context, enumerable, item, index);
                }
                
                var field = new DebugField(itemLabel, childContext);
                        
                _updatableFields.Add(field);
                content.AddChild(field.SetMargin(bottom: 5));

                index++;
            }
        }
    }
}