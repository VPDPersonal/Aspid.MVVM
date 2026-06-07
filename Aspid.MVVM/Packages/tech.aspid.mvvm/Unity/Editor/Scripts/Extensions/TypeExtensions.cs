#nullable enable
using System;
using System.Linq;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using Aspid.FastTools.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Editor extension methods for <see cref="System.Type"/> providing display name formatting,
    /// explicit interface resolution, and recursive member enumeration across base class hierarchies.
    /// </summary>
    public static class TypeExtensions
    {
        public static string GetTypeDisplayName(this Type type)
        {
            if (!type.IsGenericType)
            {
                return !string.IsNullOrWhiteSpace(type.Namespace)
                    ? $"{type.Namespace}.{type.Name}"
                    : type.Name;
            }

            var genericTypeName = type.Name;
            var tickIndex = genericTypeName.IndexOf('`');
            if (tickIndex >= 0) genericTypeName = genericTypeName[..tickIndex];

            var genericArguments = type.GetGenericArguments()
                .Select(GetTypeDisplayName);

            var prefix = !string.IsNullOrWhiteSpace(type.Namespace)
                ? $"{type.Namespace}.{genericTypeName}"
                : genericTypeName;

            return $"{prefix}<{string.Join(", ", genericArguments)}>";
        }
        
        public static Type GetUnitySerializableType(this FieldInfo field) =>
            GetUnitySerializableType(field.FieldType, field.Name);

        public static Type GetUnitySerializableType(this PropertyInfo propertyInfo) =>
            GetUnitySerializableType(propertyInfo.PropertyType, propertyInfo.Name);
        
        internal static Dictionary<string, FieldInfo> GetInstanceFieldMap(this Type type) => type
            .GetMembersInfosIncludingBaseClasses(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .OfType<FieldInfo>()
            .Where(field => field.IsPublic || field.IsDefined(typeof(SerializeField)))
            .ToDictionary(field => field.Name);

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