#nullable enable
using System.Collections;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugEnumerableField : VisualElement, IUpdatableField
    {
        private object? _value;
        private readonly string _label;
        private readonly IFieldContext _context;
        private readonly List<IUpdatableField> _updatableFields;
        
        internal DebugEnumerableField(string label, IFieldContext context)
        {
            _label = label;
            _context = context;
            _updatableFields = new List<IUpdatableField>();
            
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
            
            if (_value is not IEnumerable enumerable)
            {
                this.AddChild(new DebugNullField(label, context.MemberType));
                return;
            }
            
            var foldout = new Foldout()
                .SetValue(false)
                .SetText(label)
                .SetMargin(left: 12);

            var content = new VisualElement();
            foldout.AddChild(content);
            
            foldout.RegisterValueChangedCallback(e =>
            {
                if (e.target != foldout) return;
                    
                if (e.newValue)
                {
                    var index = 0;
                        
                    foreach (var item in enumerable)
                    {
                        var itemLabel = index.ToString();

                        if (item is not null)
                        {
                            var itemType = item.GetType();
                            
                            if (itemType.IsGenericType && itemType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                            {
                                var key = itemType.GetProperty("Key")?.GetValue(item);
                                if (key is not null)
                                {
                                    itemLabel = key.ToString();
                                }
                            }
                        }
                        
                        var childContext = new CollectionElementContext(context, enumerable, item, index);
                        var field = new DebugField(label: itemLabel, childContext);
                        
                        _updatableFields.Add(field);
                        content.AddChild(field.SetMargin(bottom: 5));

                        index++;
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