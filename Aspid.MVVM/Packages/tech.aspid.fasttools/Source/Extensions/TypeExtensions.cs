#nullable enable
using System;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Reflection
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns the members of <paramref name="type"/> and its base classes in declaration order (base → derived),
        /// matching the Unity inspector's traversal order.
        /// </summary>
        /// <param name="type">The type to inspect.</param>
        /// <param name="bindingFlags">The binding flags used to filter members. <see cref="BindingFlags.DeclaredOnly"/> is forced on internally to avoid duplicate members from base classes.</param>
        /// <param name="stopAt">Optional ancestor type at which to stop walking the chain. When <c>null</c>, walks all the way to the root type.</param>
        /// <returns>A flat list of <see cref="MemberInfo"/> instances ordered from the topmost base class down to <paramref name="type"/>.</returns>
        public static IReadOnlyList<MemberInfo> GetMembersInfosIncludingBaseClasses(this Type type, BindingFlags bindingFlags, Type? stopAt = null)
        {
            var currentType = type;
            var typeChain = new List<Type>();

            while (currentType != stopAt)
            {
                if (currentType is null) break;
                
                typeChain.Add(currentType);
                currentType = currentType.BaseType;
            }

            // Iterate base → derived so members appear in declaration order (matches Unity inspector)
            typeChain.Reverse();

            if (!bindingFlags.HasFlag(BindingFlags.DeclaredOnly))
                bindingFlags |= BindingFlags.DeclaredOnly;

            var members = new List<MemberInfo>();
            foreach (var t in typeChain)
                members.AddRange(t.GetMembers(bindingFlags));

            return members;
        }
    }
}
