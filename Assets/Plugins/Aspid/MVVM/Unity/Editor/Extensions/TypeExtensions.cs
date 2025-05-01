#nullable enable
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Aspid.MVVM.Unity
{
    public static class TypeExtensions
    {
        public static FieldInfo[] GetFieldInfosIncludingBaseClasses(this Type type, BindingFlags bindingFlags)
        {
            if (type.BaseType == typeof(object)) 
                return type.GetFields(bindingFlags);

            var currentType = type;
            var fieldInfoList = new List<FieldInfo>();
            
            while (currentType != typeof(object))
            {
                if (currentType is null) break;
                
                fieldInfoList.AddRange(currentType.GetFields(bindingFlags));
                currentType = currentType.BaseType;
            }
            
            return fieldInfoList.ToArray();
        }
    }
}