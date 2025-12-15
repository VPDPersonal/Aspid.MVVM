using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal interface IFieldContext
    {
        public object Target { get; }
        
        public Type MemberType { get; }
        
        public MemberInfo Member { get; }
        
        public bool IsReadonly { get; }
        
        public bool IsAlternativeColor { get; }
        
        public object GetValue();
        
        public void SetValue(object value);

        public bool IsDefined(Type attributeType, bool inherit = false);

        public T GetCustomAttribute<T>()
            where T : Attribute;
    }
}
