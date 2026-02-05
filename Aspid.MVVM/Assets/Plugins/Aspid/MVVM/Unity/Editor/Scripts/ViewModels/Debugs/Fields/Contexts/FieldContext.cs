using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class FieldContext : IFieldContext
    {
        public object Target { get; }
        
        public Type MemberType { get; }

        public MemberInfo Member { get; }
        
        public bool IsAlternativeColor { get; }

        public bool IsReadonly => Member.IsReadonly();

        public FieldContext(object target, MemberInfo member, bool isAlternativeColor = false)
        {
            Member = member;
            Target = target;
            MemberType = member.GetMemberType();
            IsAlternativeColor = isAlternativeColor;
        }
        
        public object GetValue() => 
            Member.GetValue(Target);

        public void SetValue(object value)
        {
            if (IsReadonly) return;
            Member.SetValue(Target, value);
        }

        public bool IsDefined(Type attributeType, bool inherit = false) =>
            Member.IsDefined(attributeType, inherit);
        
        public T GetCustomAttribute<T>() 
            where T : Attribute
        {
            return Member.GetCustomAttribute<T>();
        }
    }
}