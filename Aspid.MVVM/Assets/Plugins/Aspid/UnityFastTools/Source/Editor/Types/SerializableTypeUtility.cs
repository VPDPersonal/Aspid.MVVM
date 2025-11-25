using System;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    internal static class SerializableTypeUtility
    {
        internal static Type GetGenericArgumentFromFieldType(FieldInfo field, out bool isGeneric)
        {
            var type = field.FieldType;
            
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                type = type.GetGenericArguments()[0];
            }
            else if (type.IsArray)
            {
                type = type.GetElementType();
            }

            if (type is not { IsGenericType: true })
            {
                isGeneric = false;
                return null;
            }

            isGeneric = true;
            var types = type.GetGenericArguments();
            return types is { Length: 1 } ? types[0] : null;
        }
    }
}