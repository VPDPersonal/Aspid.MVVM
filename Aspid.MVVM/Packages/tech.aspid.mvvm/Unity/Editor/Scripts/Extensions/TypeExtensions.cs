#nullable enable
using System;
using System.Linq;
using UnityEngine;
using Aspid.FastTools;
using System.Reflection;
using System.Collections.Generic;

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
            // [SerializeReference] is the other way a private field becomes serialized — without it here, a
            // polymorphic field resolves to a null FieldInfo and the inspector skips it entirely.
            .Where(field => field.IsPublic || field.IsDefined(typeof(SerializeField)) || field.IsDefined(typeof(SerializeReference)))
            .ToDictionary(field => field.Name);

        /// <summary>
        /// Returns the members of <paramref name="type"/> and of its base classes, base class members first,
        /// so they come in declaration order, as the Unity inspector lists them.
        /// </summary>
        /// <param name="type">The type whose members to collect.</param>
        /// <param name="bindingFlags">The member filter. <see cref="BindingFlags.DeclaredOnly"/> is always added.</param>
        /// <param name="stopAt">The base class at which the walk stops, excluded; <see langword="null"/> walks to <see cref="object"/>.</param>
        /// <returns>The members of the whole class chain.</returns>
        internal static IReadOnlyList<MemberInfo> GetMembersInfosIncludingBaseClasses(this Type type, BindingFlags bindingFlags, Type? stopAt = null)
        {
            var typeChain = new List<Type>();

            for (var currentType = type; currentType is not null && currentType != stopAt; currentType = currentType.BaseType)
                typeChain.Add(currentType);

            typeChain.Reverse();
            bindingFlags |= BindingFlags.DeclaredOnly;

            var members = new List<MemberInfo>();
            foreach (var chainType in typeChain)
                members.AddRange(chainType.GetMembers(bindingFlags));

            return members;
        }

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