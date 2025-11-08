#nullable enable
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static class TypeExtensions
    {
        public static Type? GetExplicitInterface(this Type implementingType, PropertyInfo property)
        {
            foreach (var inter in implementingType.GetInterfaces())
            {
                var map = implementingType.GetInterfaceMap(inter);
                
                for (var i = 0; i < map.InterfaceMethods.Length; i++)
                {
                    var targetMethod = map.TargetMethods[i];
                    
                    if (property.GetMethod != null && targetMethod == property.GetMethod 
                        || property.SetMethod != null && targetMethod == property.SetMethod)
                    {
                        return inter;
                    }
                }
            }

            return null;
        }
        
        public static IEnumerable<FieldInfo> GetFieldInfosIncludingBaseClasses(this Type type, BindingFlags bindingFlags, Type? baseType = null) =>
            GetMembersInfosIncludingBaseClasses(type, bindingFlags, baseType).OfType<FieldInfo>();
        
        public static IEnumerable<PropertyInfo> GetPropertyInfosIncludingBaseClasses(this Type type, BindingFlags bindingFlags, Type? baseType = null) =>
            GetMembersInfosIncludingBaseClasses(type, bindingFlags, baseType).OfType<PropertyInfo>();
        
        public static IReadOnlyList<MemberInfo> GetMembersInfosIncludingBaseClasses(this Type type, BindingFlags bindingFlags, Type? baseType = null)
        {
            if (type.BaseType == typeof(object)) 
                return type.GetMembers(bindingFlags);

            var currentType = type;
            var memberInfoList = new List<MemberInfo>();
            
            while (currentType != (baseType ?? typeof(object)))
            {
                if (currentType is null) break;
                
                memberInfoList.AddRange(currentType.GetMembers(bindingFlags | BindingFlags.DeclaredOnly));
                currentType = currentType.BaseType;
            }

            return memberInfoList;
        }
        
        public static Type GetUnitySerializableType(this FieldInfo field) =>
            GetUnitySerializableType(field.FieldType, field.Name);

        public static Type GetUnitySerializableType(this PropertyInfo propertyInfo) =>
            GetUnitySerializableType(propertyInfo.PropertyType, propertyInfo.Name);

        private static Type GetUnitySerializableType(Type? type, string name)
        {
            if (type is null) throw ThrowException();
            
            while (type.IsArray || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                type = type.IsArray
                    ? type.GetElementType()
                    : type.GetGenericArguments()[0];

                if (type is null) throw ThrowException();
            }
            
            return type;
            
            NullReferenceException ThrowException() =>
                throw new NullReferenceException($"Member {name} of type {type} cannot be null.");
        }
    }
}