using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Helpers.Descriptions;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Generators.Views.Data.Members;

namespace MVVMGenerators.Generators.Views.Body;

public static class BinderCachedBody
{
    private const string GeneratedAttribute = General.GeneratedCodeViewAttribute;
    
    private static readonly string EditorBrowsableAttribute = $"[{Classes.EditorBrowsableAttribute.Global}({Classes.EditorBrowsableState.Global}.Never)]";
    
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
            code.AppendLine(EditorBrowsableAttribute)
                .AppendLine(GeneratedAttribute)
                .AppendLine($"private {property.Type.ToDisplayStringGlobal()} {property.CachedName};")
                .AppendLine();
        }

        return code;
    }
    
    private static CodeWriter AppendAsBinderMember(this CodeWriter code, in ReadOnlySpan<AsBinderMemberInView> members)
    {
        foreach (var member in members)
        {
            code.AppendLine(EditorBrowsableAttribute)
                .AppendLine(GeneratedAttribute)
                .AppendLine(member.Type is IArrayTypeSymbol
                    ? $"private {member.AsBinderType}[] {member.CachedName};"
                    : $"private {member.AsBinderType} {member.CachedName};")
                .AppendLine();
        }

        return code;
    }
}