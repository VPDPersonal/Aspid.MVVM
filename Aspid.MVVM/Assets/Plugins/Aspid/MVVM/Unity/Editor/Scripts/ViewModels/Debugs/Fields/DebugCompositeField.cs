using UnityEngine;
using System.Reflection;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class DebugCompositeField : VisualElement, IUpdatableDebugField, ISearchableDebugField
    {
        private const string StyleSheetPath = "Styles/Debug/Fields/aspid-debug-composite-field";
        
        private bool _isExpanded;
        private Foldout _foldout;
        private bool _isSearchMode;
        private VisualElement _content;
        
        private readonly string _label;
        private readonly List<IUpdatableDebugField> _updatableFields;
        private readonly List<ISearchableDebugField> _searchableFields;

        public object Value { get; private set; }
        
        protected IFieldContext Context { get; private set; }
        
        public DebugCompositeField(string label, IFieldContext context)
        {
            _label = label;
            Context = context;
            _updatableFields = new List<IUpdatableDebugField>();
            _searchableFields = new List<ISearchableDebugField>();
            styleSheets.Add(styleSheet: Resources.Load<StyleSheet>(StyleSheetPath));
            
            Build(label, context.GetValue(), context);
        }

        public void UpdateValue()
        {
            var newValue = Context.GetValue();
            
            if (!EqualityComparer<object>.Default.Equals(newValue, Value))
            {
                Clear();
                _updatableFields.Clear();
                
                Build(_label, newValue, Context);   
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
            Value = value;

            if (value is null)
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

        protected virtual void BuildContent(VisualElement content)
        {
            var type = Value.GetType();
            var fields = type.GetFieldInfosIncludingBaseClasses(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            
            foreach (var fieldInfo in fields)
            {
                BuildDebugField(content, fieldInfo);
            }
        }

        protected void BuildDebugField(VisualElement content, FieldInfo fieldInfo)
        {
            var field = new DebugField(Value, fieldInfo, !Context.IsAlternativeColor);
            
            _updatableFields.Add(field);
            _searchableFields.Add(field);
            
            content.AddChild(field);
        }
        
        public bool Search(string searchPath)
        {
            _isSearchMode = true;
            
            // Expand the foldout to show nested fields during search
            if (_foldout != null && !_foldout.value)
            {
                _foldout.SetValueWithoutNotify(true);
                _updatableFields.Clear();
                _searchableFields.Clear();
                _content?.Clear();
                BuildContent(_content);
            }
            
            // If searchPath is empty, show all nested fields (e.g., from "h." query)
            if (string.IsNullOrEmpty(searchPath))
            {
                foreach (var searchableField in _searchableFields)
                {
                    searchableField.ClearSearch();
                }
                return true;
            }
            
            // Search in all nested fields
            var anyMatch = false;
            foreach (var searchableField in _searchableFields)
            {
                if (searchableField.Search(searchPath))
                {
                    anyMatch = true;
                }
            }
            
            return anyMatch;
        }
        
        public void ClearSearch()
        {
            if (!_isSearchMode) return;
            
            _isSearchMode = false;
            
            // Clear search state in nested fields first
            foreach (var searchableField in _searchableFields)
            {
                searchableField.ClearSearch();
            }
            
            // Collapse back if it was expanded by search (not manually)
            if (_foldout != null && !_isExpanded)
            {
                _foldout.SetValueWithoutNotify(false);
                _content?.Clear();
                _updatableFields.Clear();
                _searchableFields.Clear();
            }
        }
    }
}