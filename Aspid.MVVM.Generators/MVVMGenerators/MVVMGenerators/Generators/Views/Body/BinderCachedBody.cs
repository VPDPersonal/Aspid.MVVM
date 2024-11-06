using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Generators.Views.Data.Members;

namespace MVVMGenerators.Generators.Views.Body;

public static class BinderCachedBody
{
    public static CodeWriter AppendCachedBinders(this CodeWriter code, in ViewDataSpan data)
    {
        return code
            .AppendProperties(data.ViewProperties)
            .AppendAsBinderMember(data.AsBinderMembers);
    }

    private static CodeWriter AppendProperties(this CodeWriter code, in ReadOnlySpan<PropertyBinderInView> properties)
    {
        foreach (var property in properties)
        {
            code.AppendLine($"private {property.Type} {property.CachedName};");
        }

        return code;
    }
    
    private static CodeWriter AppendAsBinderMember(this CodeWriter code, in ReadOnlySpan<AsBinderMemberInView> members)
    {
        foreach (var member in members)
        {
            code.AppendLine(member.Type is IArrayTypeSymbol 
                ? $"private {member.AsBinderType}[] {member.CachedName};"
                : $"private {member.AsBinderType} {member.CachedName};");
        }

        return code;
    }
}