using System;
using System.Linq;
using System.Reflection;
using UltimateUI.MVVM.Unity;
using System.Collections.Generic;

namespace UltimateUI.MVVM.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<FieldInfo> GetMonoBinderFields(this Type type, BindingFlags bindingFlags) => 
            type.GetFieldInfosIncludingBaseClasses(bindingFlags).Where(field =>
            {
                var fieldType = field.FieldType;
                return typeof(MonoBinder).IsAssignableFrom(fieldType) || typeof(MonoBinder[]).IsAssignableFrom(fieldType);
            });
        
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