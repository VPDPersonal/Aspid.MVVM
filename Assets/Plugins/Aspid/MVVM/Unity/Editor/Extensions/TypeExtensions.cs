#nullable enable
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Aspid.MVVM.Unity
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
        
        public static IEnumerable<FieldInfo> GetFieldInfosIncludingBaseClasses(this Type type, BindingFlags bindingFlags) =>
            GetMembersInfosIncludingBaseClasses(type, bindingFlags).OfType<FieldInfo>();
        
        public static IEnumerable<PropertyInfo> GetPropertyInfosIncludingBaseClasses(this Type type, BindingFlags bindingFlags) =>
            GetMembersInfosIncludingBaseClasses(type, bindingFlags).OfType<PropertyInfo>();
        
        public static IEnumerable<System.Reflection.MemberInfo> GetMembersInfosIncludingBaseClasses(this Type type, BindingFlags bindingFlags)
        {
            if (type.BaseType == typeof(object)) 
                return type.GetMembers(bindingFlags);

            var currentType = type;
            var memberInfoList = new List<System.Reflection.MemberInfo>();
            
            while (currentType != typeof(object))
            {
                if (currentType is null) break;
                
                memberInfoList.AddRange(currentType.GetMembers(bindingFlags));
                currentType = currentType.BaseType;
            }

            return memberInfoList;
        }
    }
}