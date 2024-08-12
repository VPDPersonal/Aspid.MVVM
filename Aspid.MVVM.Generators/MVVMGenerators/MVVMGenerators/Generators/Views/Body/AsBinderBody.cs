using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Generators.Views.Data.Members;
using MVVMGenerators.Helpers.Extensions.Writer;

namespace MVVMGenerators.Generators.Views.Body;

public static class AsBinderBody
{
    public static CodeWriter AppendViewBinderCached(this CodeWriter code, ViewData data)
    {
        var readOnlyData = new ReadOnlyViewData(data);

        code.AppendProperties(readOnlyData.PropertyMembers)
            .AppendAsBinders(readOnlyData.AsBinderMembers);
        
        return code;
    }

    private static CodeWriter AppendProperties(this CodeWriter code, ReadOnlySpan<PropertyMember> properties)
    {
        code.AppendLoop(properties, property =>
        {
            code.AppendLine($"private {property.Type} {property.FieldName};");
        });

        return code;
    }
    
    private static CodeWriter AppendAsBinders(this CodeWriter code, ReadOnlySpan<AsBinderMember> members)
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