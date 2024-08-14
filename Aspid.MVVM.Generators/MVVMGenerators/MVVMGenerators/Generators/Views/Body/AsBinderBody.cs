using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Generators.Views.Data.Members;
using MVVMGenerators.Helpers.Extensions.Writer;

namespace MVVMGenerators.Generators.Views.Body;

public static class AsBinderBody
{
    public static CodeWriter AppendViewBinderCached(this CodeWriter code, in ViewDataSpan data)
    {
        code.AppendProperties(data.PropertyMembers)
            .AppendAsBinders(data.AsBinderMembers);
        
        return code;
    }

    private static CodeWriter AppendProperties(this CodeWriter code, in ReadOnlySpan<PropertyMember> properties)
    {
        code.AppendLoop(properties, property =>
        {
            code.AppendLine($"private {property.Type} {property.FieldName};");
        });

        return code;
    }
    
    private static CodeWriter AppendAsBinders(this CodeWriter code, in ReadOnlySpan<AsBinderMember> members)
    {
        code.AppendLoop(members, member =>
        {
            if (member.Type is IArrayTypeSymbol)
            {
                code.AppendLine($"private {member.AsBinderType}[] {member.BinderName};");
            }
            else
            {
                code.AppendLine($"private {member.AsBinderType} {member.BinderName};");
            }
        });

        return code;
    }
}