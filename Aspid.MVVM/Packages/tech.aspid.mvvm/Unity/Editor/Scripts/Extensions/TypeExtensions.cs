#nullable enable
using System;
using System.Linq;
using UnityEngine;
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
        /// Returns the members of the type and of its base classes, ordered from the topmost base class down to the type.
        /// </summary>
        /// <param name="type">The type whose members are collected.</param>
        /// <param name="bindingFlags">The binding flags used to filter members. <see cref="BindingFlags.DeclaredOnly"/> is always added.</param>
        /// <param name="stopAt">The base type at which the walk stops, excluded; <c>null</c> walks up to the root type.</param>
        /// <returns>The members in declaration order, as the Unity Inspector shows them.</returns>
        internal static IReadOnlyList<MemberInfo> GetMembersInfosIncludingBaseClasses(this Type type, BindingFlags bindingFlags, Type? stopAt = null)
        {
            var currentType = type;
            var typeChain = new List<Type>();

            while (currentType is not null && currentType != stopAt)
            {
                typeChain.Add(currentType);
                currentType = currentType.BaseType;
            }

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