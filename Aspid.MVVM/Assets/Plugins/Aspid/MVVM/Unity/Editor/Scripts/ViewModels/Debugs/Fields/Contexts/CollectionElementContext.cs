using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class CollectionElementContext : IFieldContext
    {
        public object Target => _collection;
        
        public Type MemberType { get; }
        
        public MemberInfo Member => _parentContext.Member;
        
        public bool IsAlternativeColor { get; }

        public bool IsReadonly => Member.IsReadonly();
        
        private object _value;
        private readonly int _index;
        private readonly object _collection;
        private readonly IFieldContext _parentContext;

        public CollectionElementContext(
            IFieldContext parentContext, 
            object collection, 
            object value,
            int index)
        {
            _index = index;
            _value = value;
            _collection = collection;
            _parentContext = parentContext;
            IsAlternativeColor = !parentContext.IsAlternativeColor;
            MemberType = value?.GetType() ?? GetElementType(parentContext.MemberType) ?? typeof(object);
        }

        public object GetValue() => _value;

        public void SetValue(object value)
        {
            if (IsReadonly) return;
            
            _value = value;
            
            switch (_collection)
            {
                case Array array:
                    array.SetValue(value, _index);
                    break;
                case System.Collections.IList list:
                    list[_index] = value;
                    break;
            }
        }

        public bool IsDefined(Type attributeType, bool inherit = false) => 
            Member?.IsDefined(attributeType, inherit) ?? false;

        public T GetCustomAttribute<T>() where T : Attribute => 
            Member?.GetCustomAttribute<T>();

        private static Type GetElementType(Type collectionType)
        {
            if (collectionType.IsArray)
                return collectionType.GetElementType();
            
            if (collectionType.IsGenericType)
            {
                var genericArguments = collectionType.GetGenericArguments();
                if (genericArguments.Length == 1)
                    return genericArguments[0];
            }

            return null;
        }
    }
}
