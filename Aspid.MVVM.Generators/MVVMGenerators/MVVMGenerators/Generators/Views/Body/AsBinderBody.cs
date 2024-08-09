using System;
using Microsoft.CodeAnalysis;
using MVVMGenerators.Helpers;
using MVVMGenerators.Generators.Views.Data;
using MVVMGenerators.Helpers.Extensions.Symbols;
using MVVMGenerators.Helpers.Extensions.Writer;

namespace MVVMGenerators.Generators.Views.Body;

public static class AsBinderBody
{
    public static CodeWriter AppendViewBinderFields(this CodeWriter code, ViewData data)
    {
        var readOnlyData = new ReadOnlyViewData(data);
        code.AppendFields(readOnlyData.AsBinderFields)
            .AppendProperties(readOnlyData.AsBinderProperty);
        
        return code;
    }

    private static CodeWriter AppendFields(this CodeWriter code, in ReadOnlySpan<AsBinderMember<IFieldSymbol>> fields)
    {
        return code.AppendLoop(fields, field =>
        {
            code.AppendLine($"private {field.BinderType} {field.Member.Name}Binder;");
        });
    }

    private static CodeWriter AppendProperties(this CodeWriter code, in ReadOnlySpan<AsBinderMember<IPropertySymbol>> properties)
    {
        return code.AppendLoop(properties, property =>
        {
            code.AppendLine($"private {property.BinderType} {property.Member.GetFieldName()}Binder;");
        });
    }
}