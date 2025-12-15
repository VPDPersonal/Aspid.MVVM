using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DictionaryElementContext : IFieldContext
    {
        private readonly object _key;
        private readonly object _dictionary;
        private readonly IFieldContext _parentContext;

        public object Target =>
            _parentContext.GetValue();
        
        public Type MemberType { get; }
        
        public MemberInfo Member => 
            _parentContext.Member;
        
        public bool IsAlternativeColor { get; }

        public bool IsReadonly => false;

        public DictionaryElementContext(IFieldContext parentContext, object key)
        {
            _key = key;
            _parentContext = parentContext;
            
            IsAlternativeColor = !parentContext.IsAlternativeColor;
            
            var dictionaryType = parentContext.MemberType;
            var genericArgs = dictionaryType.GetGenericArguments();
            MemberType = typeof(KeyValuePair<,>).MakeGenericType(genericArgs);
        }

        public object GetValue()
        {
            var value = ((IDictionary)Target)[_key];
            return Activator.CreateInstance(MemberType, _key, value);
        }

        public void SetValue(object value) =>
            ((IDictionary)Target)[_key] = value;

        public bool IsDefined(Type attributeType, bool inherit = false) => 
            Member?.IsDefined(attributeType, inherit) ?? false;

        public T GetCustomAttribute<T>() where T : Attribute => 
            Member?.GetCustomAttribute<T>();
    }
}
