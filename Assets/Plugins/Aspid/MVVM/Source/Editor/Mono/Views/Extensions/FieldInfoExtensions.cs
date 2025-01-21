#nullable enable
using System;
using System.Reflection;

namespace Aspid.MVVM.Mono
{
    public static class FieldInfoExtensions
    {
        public static void SetValueFromCastValue<T>(this FieldInfo field, object obj, params T?[] value)
        {
            if (value.Length is 0)
            {
                field.SetValue(obj, null);
                return;
            }
            
            var isArray = field.FieldType.IsArray;
            var type = isArray ? field.FieldType.GetElementType()! : field.FieldType;
            
            var typedArray = Array.CreateInstance(type, value.Length);
            Array.Copy(value, typedArray, value.Length);
            
            field.SetValue(obj, isArray ? typedArray : typedArray.GetValue(0));
        }

        public static T[]? GetValueAsArray<T>(this FieldInfo field, object obj)
        {
            T[]? values;
                
            if (field.FieldType.IsArray) values = field.GetValue(obj) as T[];
            else values = field.GetValue(obj) is T binder ? new[] { binder } : null;

            return values;
        }
    }
}