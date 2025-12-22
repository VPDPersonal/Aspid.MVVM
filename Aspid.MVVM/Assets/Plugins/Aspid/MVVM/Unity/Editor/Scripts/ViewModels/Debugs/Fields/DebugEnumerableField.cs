using System.Linq;
using UnityEngine;
using System.Collections;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DebugEnumerableField : VisualElement, IUpdatableDebugField, ISearchableDebugField
    {
        private const string StyleSheetPath = "Styles/Debug/Fields/aspid-debug-enumerable-field";
        
        private object _value;
        private bool _isExpanded;
        private bool _isSearchMode;
        private Foldout _foldout;
        private VisualElement _content;
        
        private readonly string _label;
        private readonly IFieldContext _context;
        private readonly List<IUpdatableDebugField> _updatableFields;
        private readonly List<ISearchableDebugField> _searchableFields;
        
        public DebugEnumerableField(string label, IFieldContext context)
        {
            _label = label;
            _context = context;
            _updatableFields = new List<IUpdatableDebugField>();
            _searchableFields = new List<ISearchableDebugField>();
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

        #region Search
        public bool Search(string searchPath, string typeFilter = null)
        {
            _isSearchMode = true;
            
            // Expand the foldout to show nested fields during search
            if (_foldout is { value: false })
            {
                _foldout.SetValueWithoutNotify(true);
                _updatableFields.Clear();
                _searchableFields.Clear();
                _content?.Clear();
                
                BuildContent(_content);
            }
            
            // If searchPath is empty, search all nested fields with type filter if provided
            if (string.IsNullOrEmpty(searchPath))
            {
                if (!string.IsNullOrEmpty(typeFilter))
                {
                    // Type-only search: search all nested fields with the type filter
                    var anyMatch = false;
                    
                    foreach (var searchableField in _searchableFields)
                    {
                        if (searchableField.Search(searchPath, typeFilter))
                        {
                            anyMatch = true;
                        }
                    }
                    
                    return anyMatch;
                }
                
                // No filter - show all nested fields
                foreach (var searchableField in _searchableFields)
                    searchableField.ClearSearch();
                
                return true;
            }
            {
                // Search in all nested fields with both name and type filter
                var anyMatch = false;
                
                foreach (var searchableField in _searchableFields)
                {
                    if (searchableField.Search(searchPath, typeFilter))
                    {
                        anyMatch = true;
                    }
                }
            
                return anyMatch;
            }
        }
        
        public void ClearSearch()
        {
            if (!_isSearchMode) return;
            
            _isSearchMode = false;
            
            // Clear the search state in nested fields first
            foreach (var searchableField in _searchableFields)
                searchableField.ClearSearch();
            
            // Collapse back if it was expanded by search (not manually)
            if (_foldout is not null && !_isExpanded)
            {
                _foldout.SetValueWithoutNotify(false);
                _content?.Clear();
                _updatableFields.Clear();
                _searchableFields.Clear();
            }
        }
        #endregion

        private void Build(string label, object value, IFieldContext context)
        {
            _value = value;
            
            if (_value is not IEnumerable)
            {
                this.AddChild(new DebugNullField(label, context.MemberType));
                return;
            }
            
            _foldout = new Foldout()
                .SetValue(_isExpanded)
                .SetText(label);

            _content = new VisualElement();
            _foldout.AddChild(_content);
            
            if (_isExpanded) 
                BuildContent(_content);
            
            _foldout.RegisterValueChangedCallback(e =>
            {
                if (e.target != _foldout) return;
                    
                if (e.newValue)
                {
                    BuildContent(_content);
                }
                else
                {
                    _content.Clear();
                    _updatableFields.Clear();
                    _searchableFields.Clear();
                }
                
                _isExpanded = e.newValue;
            });
            
            this.AddChild(_foldout);
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
                _searchableFields.Add(field);
                content.AddChild(field.SetMargin(bottom: 5));

                index++;
            }
        }
    }
}