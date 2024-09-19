using System;
using System.Reflection;
using System.Collections.Generic;

namespace AspidUI.MVVM.Extensions
{
    public static class TypeExtensions
    {
        public static FieldInfo[] GetFieldInfosIncludingBaseClasses(this Type type, BindingFlags bindingFlags)
        {
            if (type.BaseType == typeof(object)) return type.GetFields(bindingFlags);

            var currentType = type;
            var fieldInfoList = new List<FieldInfo>();
            
            while (currentType != typeof(object))
            {
                if (currentType == null) break;
                
                fieldInfoList.AddRange(currentType.GetFields(bindingFlags));
                currentType = currentType.BaseType;
            }
            
            return fieldInfoList.ToArray();
        }
    }
}