#nullable enable
using System;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools
{
    public static class TypeExtensions
    {
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
