using System;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static class MemberInfoExtensions
    {
        public static Type GetMemberType(this MemberInfo member) => member switch
        {
            FieldInfo fieldInfo => fieldInfo.FieldType,
            PropertyInfo propertyInfo => propertyInfo.PropertyType,
            _ => throw new ArgumentException("Can't get the type of a " + member.GetType().Name)
        };
        
        public static bool IsReadonly(this MemberInfo member) => member switch
        {
            FieldInfo fieldInfo => fieldInfo.IsInitOnly,
            PropertyInfo propertyInfo => !propertyInfo.CanWrite,
            _ => throw new ArgumentException("Can't check readonly status of a " + member.GetType().Name)
        };
        
        public static void SetValue(this MemberInfo member, object obj, object value)
        {
            switch (member)
            {
                case FieldInfo fieldInfo:
                    fieldInfo.SetValue(obj, value);
                    return;
                
                case PropertyInfo propertyInfo:
                    {
                        var method = propertyInfo.GetSetMethod(nonPublic: true);
                        if (method is null) throw new ArgumentException($"Property {member.Name} has no setter");
                
                        method.Invoke(obj, new[]{ value });
                        return;
                    }
                
                default: throw new ArgumentException("Can't set the value of a " + member.GetType().Name);
            }
        }
        
        public static object GetValue(this MemberInfo member, object obj) => member switch
        {
            FieldInfo fieldInfo => fieldInfo.GetValue(obj),
            PropertyInfo propertyInfo => propertyInfo.GetGetMethod(true).Invoke(obj, null),
            _ => throw new ArgumentException("Can't get the value of a " + member.GetType().Name)
        };
    }
}